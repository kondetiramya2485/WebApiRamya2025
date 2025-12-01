using Microsoft.AspNetCore.Http.HttpResults;
using Orders.Api.Endpoints.Orders.Services;
using Wolverine;

namespace Orders.Api.Endpoints.Orders.Operation;

public record ProcessOrder(Guid Id, ShoppingCartRequest Cart);
public static class Post
{
    public static async Task<Ok<OrderResponse>> AddOrderAsync(ShoppingCartRequest request, IMessageBus messageBus, CancellationToken token)
    {
        var orderId = Guid.NewGuid();

        await messageBus.PublishAsync(new ProcessOrder(orderId, request));

        var order = new OrderResponse
        {
            Id = orderId,
            Status = OrderStatus.Complete
        };
        return TypedResults.Ok(order); // the caller only gets this.
    }
}

