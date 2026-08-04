using CarteiraInvestimentosAPI.Adapters;
using CarteiraInvestimentosAPI.Adapters.Infrastructure.ExternalServices;
using CarteiraInvestimentosAPI.Adapters.Infrastructure.Repositories;
using CarteiraInvestimentosAPI.Database;
using CarteiraInvestimentosAPI.Domain.Entities;
using CarteiraInvestimentosAPI.Domain.Services;
using CarteiraInvestimentosAPI.Domain.Services.Ports;
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
    //map.SetIgnoreExtraElements(true); 
    map.MapIdMember(c => c.Id);
    map.MapField("_assets").SetElementName("Assets");
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
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IFinancialMarketService, BrapiService>();



builder.Services.AddControllers();

// Exceptions 
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

