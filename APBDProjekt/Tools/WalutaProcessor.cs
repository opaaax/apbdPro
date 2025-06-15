using System.Text.Json.Serialization;

namespace APBDProjekt.Tools;


public class WalutaResponse
{
    [JsonPropertyName("rates")]
    public Dictionary<string, decimal> Rates { get; set; }
}

public class WalutaProcessor : IWalutaProcessor
{
    public async Task<decimal> ProcessCurrency(decimal amount, string currency)
    {
        using var client = new HttpClient();
        
        var response = await client.GetAsync("https://open.er-api.com/v6/latest/PLN");
        var result = await response.Content.ReadFromJsonAsync<WalutaResponse>();
        if (result != null)
        {
            var rate = result.Rates[currency];
        
            return rate*amount;
        }
        else
        {
            throw new Exception("Bad request for currency exchange rate API.");
        }
    }
}