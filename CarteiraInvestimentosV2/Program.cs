using CarteiraInvestimentosV2.Database;
using CarteiraInvestimentosV2.Entities;
using CarteiraInvestimentosV2.Services;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

BsonClassMap.RegisterClassMap<Customer>(map =>
{
    map.AutoMap();
    map.MapIdMember(c => c.Id);
});

BsonClassMap.RegisterClassMap<Transaction>(map =>
{
    map.AutoMap();
    map.MapIdMember(t => t.Id);
});


builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetValue<string>("CarteiraInvestimentosAPI:ConnectionString");
    
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();





var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

