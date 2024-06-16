using LaCarotte.API.Models;

namespace LaCarotte.API.Services
{
    public interface IHistoryItemService
    {
        Task AddHistoryItem(HistoryItem item);
        Task<IList<HistoryItem>> ListHistoryItems();
        Task CreateOldHIstory(List<Carotte> carottes);
    }

}