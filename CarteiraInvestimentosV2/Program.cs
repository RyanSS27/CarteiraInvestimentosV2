using CarteiraInvestimentosV2.Database;
using CarteiraInvestimentosV2.Entities;
using CarteiraInvestimentosV2.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

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
builder.Services.AddScoped<ICustomerService, CustomerService>();


builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

