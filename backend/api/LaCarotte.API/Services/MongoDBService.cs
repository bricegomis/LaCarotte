using LaCarotte.API.Models;
using MongoDB.Driver;

namespace LaCarotte.API.Services
{
    public class MongoDBService : IMongoDBService
    {
        private readonly IMongoCollection<Carotte> _carotteCollection;
        private readonly IMongoCollection<Profile> _profileCollection;
        private readonly IMongoCollection<HistoryItem> _historyCollection;
        private readonly MongoClient _client;

        private readonly ILogger _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public MongoDBService(ILogger logger,
                              IDateTimeProvider dateTimeProvider,
                              string connectionString,
                              string dbName)
        {
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
            _client = new MongoClient(connectionString);

            string carotteCollectionName = "Carottes";
            string historyCollectionName = "HistoryItems";
            string profileCollectionName = "Profiles";

            var database = _client.GetDatabase(dbName);
            _profileCollection = database.GetCollection<Profile>(profileCollectionName);
            _carotteCollection = database.GetCollection<Carotte>(carotteCollectionName);
            _historyCollection = database.GetCollection<HistoryItem>(historyCollectionName);
        }

        public async Task<List<Carotte>> GetAllCarottes(string profileId)
        {
            var carotteCursor = await _carotteCollection.FindAsync(_ => _.ProfileId == profileId);
            var carotte = await carotteCursor.ToListAsync();
            return carotte;
        }

        public async Task<Carotte> GetCarotte(string id)
        {
            return await _carotteCollection.Find(x => x.Id == id).FirstAsync();
        }

        public async Task CreateCarotte(Carotte carotte)
        {
            carotte.DateCreated = _dateTimeProvider.GetNow();
            carotte.DateUpdated = _dateTimeProvider.GetNow();
            await _carotteCollection.InsertOneAsync(carotte);
        }

        public async Task UpdateCarotte(Carotte carotte)
        {
            carotte.DateUpdated = _dateTimeProvider.GetNow();
            await _carotteCollection.ReplaceOneAsync(_ => _.Id == carotte.Id, carotte);
        }

        public async Task DeleteCarotte(string id)
        {
            await _carotteCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task CreateHistory(HistoryItem historyItem)
        {
            await _historyCollection.InsertOneAsync(historyItem);
        }

        public async Task<List<HistoryItem>> GetAllHistoryItems(string profileId)
        {
            var carotteCursor = await _historyCollection.FindAsync(_ => _.ProfileId == profileId);
            var list = await carotteCursor.ToListAsync();
            return list;
        }

        #region Profiles
        public async Task CreateProfile(Profile profile)
        {
            await _profileCollection.InsertOneAsync(profile);
        }

        public async Task<Profile> GetProfile(string login)
        {
            _logger.LogDebug($"GetProfil : {login}");
            return await _profileCollection.Find(x => x.Login == login).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProfile(Profile profile)
        {
            var filter = Builders<Profile>.Filter.Eq("Id", profile.Id);
            var result = await _profileCollection.ReplaceOneAsync(filter, profile);
            return result.IsAcknowledged && result.MatchedCount == 1;
        }

        public async Task<bool> DeleteProfile(Profile profile)
        {
            var filter = Builders<Profile>.Filter.Eq("Id", profile.Id);
            var result = await _profileCollection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount == 1;
        }
        #endregion
    }
}
