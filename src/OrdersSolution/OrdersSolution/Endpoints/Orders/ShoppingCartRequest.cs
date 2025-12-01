namespace Orders.Api.Endpoints.Orders;

public class ShoppingCartRequest
{
    string CustomerName { get; set; }
    decimal Price { get; set; }
}