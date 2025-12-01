using Marten;
using Microsoft.Extensions.Hosting;
using Orders.Api.Endpoints.Orders;
using Orders.Api.Endpoints.Orders.Services;
using Wolverine;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(options =>
{
    options.Policies.UseDurableLocalQueues();
    options.Policies.UseDurableInboxOnAllListeners();
});
builder.Services.AddMarten()
    .IntegrateWithWolverine()
    .UseNpgsqlDataSource()
    .UseLightweightSessions();

builder.AddNpgsqlDataSource("orders");

builder.Services.AddOrders();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
builder.Services.AddValidation();
builder.AddServiceDefaults();

if(builder.Environment.IsDevelopment())
{
    builder.Services.RunWolverineInSoloMode();
}

var app = builder.Build(); // one world above the build, one after.


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapOrders(); // This will add all the operations for the "/orders" resource.
 
app.Run();
