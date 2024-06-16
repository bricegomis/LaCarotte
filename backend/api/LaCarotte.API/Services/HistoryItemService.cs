using LaCarotte.API.Models;
using Nest;

namespace LaCarotte.API.Services
{
    public class HistoryItemService(ElasticClient elasticClient) : IHistoryItemService
    {
        private readonly ElasticClient _elasticClient = elasticClient;

        public async Task AddHistoryItem(HistoryItem item)
        {
            var response = await _elasticClient.IndexDocumentAsync(item);
            if (!response.IsValid)
            {
                throw new Exception($"Failed to index document: {response.DebugInformation}");
            }
        }

        public async Task<IList<HistoryItem>> ListHistoryItems()
        {
            var searchResponse = await _elasticClient.SearchAsync<HistoryItem>(s => s
                .MatchAll()
                .Size(10000));

            return [.. searchResponse.Documents];
        }

        public async Task CreateOldHIstory(List<Carotte> carottes)
        {
            foreach(var carotte in carottes)
            {
                if (carotte.History == null) continue;

                foreach (var date in carotte.History)
                {
                    await _elasticClient.IndexDocumentAsync(new HistoryItem
                    {
                        Date = date,
                        Item = carotte,
                        Points = carotte.IsReward == true ? carotte.Points ?? 0 : -carotte.Points ?? 0
                    });
                }
            }
        }
    }
}