
using System;
using System.Text.Json;
using Unit8_SRP_F24.Contracts;

namespace Unit8_SRP_F24;

public class RestfulTradeDataProvider : ITradeDataProvider
{
    string url;
    ILogger logger;

    public RestfulTradeDataProvider(string url, ILogger logger)
    {
        this.url = url;
        this.logger = logger;
    }

    public IEnumerable<string> ReadTradeData()
    {
        logger.LogMessage("Reading trade data from Restful API: " + url);
        // -------------- Add your code here to fetch trade data from the URL and return it as an IEnumerable<string> --------------
        return ReadTradeDataAsync().GetAwaiter().GetResult();
    }

    private async Task<IEnumerable<string>> ReadTradeDataAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogMessage("Error reading trade data from Restful API");
                throw new HttpRequestException(
                    "Failed to retrieve trade data: " + response.StatusCode);
            }

            string jsonData = await response.Content.ReadAsStringAsync();

            List<string> tradeData =
                JsonSerializer.Deserialize<List<string>>(jsonData)
                ?? new List<string>();

            return tradeData;
        }
    }
}
