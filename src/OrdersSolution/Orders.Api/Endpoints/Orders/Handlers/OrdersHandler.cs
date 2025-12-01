using Orders.Api.Endpoints.Orders.Operation;

namespace Orders.Api.Endpoints.Orders.Handlers
{
    public class OrdersHandler(ILogger<OrdersHandler> logger)
    {
        public async Task HandleAsync(ProcessOrder command)
        {
            logger.LogInformation("Got an order! for {show}, amount: {amt:c}", command.Cart.CustomerName, command.Cart.Amount);
        }
    }
}
