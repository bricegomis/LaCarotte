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

        // public IList<HistoryItemByDay> GetHistoryItemsByDay()
        // {
        //     var searchResponse = _elasticClient.Search<HistoryItem>(s => s
        //         .Aggregations(agg => agg
        //             .DateHistogram("by_day", dh => dh
        //                 .Field(f => f.Date)
        //                 .CalendarInterval(DateMathTimeUnit.Day)
        //                 .Aggregations(childAgg => childAgg
        //                     .Terms("items", t => t
        //                         .Field(f => f.Item)
        //                         .Size(10000)
        //                     )
        //                 )
        //             )
        //         )
        //     );

        //     var buckets = searchResponse.Aggregations.DateHistogram("by_day").Buckets;
        //     var result = new List<HistoryItemByDay>();

        //     foreach (var bucket in buckets)
        //     {
        //         var day = bucket.Key;
        //         var items = bucket.Terms("items").Buckets.Select(b => b.Key).ToList();
        //         result.Add(new HistoryItemByDay { Day = day, Items = items });
        //     }

        //     return result;
        // }
    }

    public class HistoryItemByDay
    {
        public DateTime Day { get; set; }
        public IList<string> Items { get; set; }
    }

}