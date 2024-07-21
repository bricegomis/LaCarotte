using LaCarotte.API.Models;

namespace LaCarotte.API.Services
{
    public interface IMongoDBService
    {
        Task<List<Carotte>> GetAllCarottes(string profileId);
        Task<Carotte> GetCarotte(string id);
        Task CreateCarotte(Carotte carotte);
        Task UpdateCarotte(Carotte carotte);
        Task DeleteCarotte(string id);

        Task<Profile> GetProfile(string login);
        Task CreateProfile(Profile profile);
        Task<bool> UpdateProfile(Profile profile);
        Task<bool> DeleteProfile(Profile profile);

        Task CreateHistory(HistoryItem history);
        Task<List<HistoryItem>> GetAllHistoryItems(string profileId);
    }
}
