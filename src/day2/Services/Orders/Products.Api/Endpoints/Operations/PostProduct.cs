using Wolverine;

namespace Products.Api.Endpoints.Operations;

public static class PostProduct
{
    public static async Task<IResult> AddProductToInventoryAsync(ProductCreateRequest request,
        IMessageBus messaging,
        CancellationToken token)
    {
        var command = new CreateProduct
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
        };

        await messaging.PublishAsync(command);
        return TypedResults.Ok(new ProductCreateResponse
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
        });

    }
}

internal class CreateProduct
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
}

public record ProductCreateRequest
{
    public string Name { get;set;  } = string.Empty;
    public decimal Price { get;set; }
    public int Quantity { get; set; }
}

public record ProductCreateResponse
{
    public Guid Id { get;set; }
    public string Name { get;set; }
    public decimal Price { get;set; }
}
