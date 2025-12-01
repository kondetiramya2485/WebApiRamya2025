namespace Orders.Api.Endpoints.Orders.Services;

public class CardProcessor
{
    public async Task<string> ProcessCardAsync(string customername, CancellationToken token)
    {
        await Task.Delay(1000);
        return "3333339999";
    }
}
