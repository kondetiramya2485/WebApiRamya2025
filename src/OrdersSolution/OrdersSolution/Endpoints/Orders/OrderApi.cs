using Orders.Api.Endpoints.Orders.Services;
using System.Numerics;

namespace Orders.Api.Endpoints.Orders
{
    public static class OrderApi
    {
        extension(IServiceCollection services) 
        { 
            public IServiceCollection AddOrders()
            {
                services.AddScoped<CardProcessor>();
                return services; 
            }
        }
        extension(IEndpointRouteBuilder builder)
        {
            public IEndpointRouteBuilder MapOrders()
            {
                var ordersGroup = builder.MapGroup("/orders");
                ordersGroup.MapGet("/", () => "Your Orders");

                ordersGroup.MapPost("/", async (ShoppingCartRequest request, CardProcessor processor, CancellationToken token) =>
                {
                    await Task.Delay(1000);

                    var cardTask = processor.ProcessCardAsync("someone", token);

                    var order = new Order
                    {
                        Id = Guid.NewGuid(),
                        TotalOrderIeee754Comparer = 300
                    };
                    return TypedResults.Ok(order);
                });
                return builder;
            }            
        }
    }
}
