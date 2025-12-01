using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent);

var ordersDb = postgres.AddDatabase("orders");

var externalService = builder.AddExternalService("todos", "https://jsonplaceholder.typicode.com");


var ordersApi = builder.AddProject<Orders_Api>("OrdersApi")
    .WithReference(ordersDb)
    .WithReference(externalService)
    .WaitFor(ordersDb);


builder.Build().Run();
