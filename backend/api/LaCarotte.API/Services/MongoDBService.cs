using DoList.API.Models;
using MongoDB.Driver;

namespace DoList.API.Services
{
    public interface IMongoDBService
    {
        Task<List<DoItem>> GetAllDoItems(string profileId);
        Task<DoItem> GetDoItem(string id);
        Task CreateDoItem(DoItem doItem);
        Task UpdateDoItem(DoItem doItem);
        Task DeleteDoItem(string id);

        Task<List<CarrotItem>> GetAllCarrotItems(string profileId);
        Task<CarrotItem> GetCarrotItem(string id);
        Task CreateCarrotItem(CarrotItem carrotItem);
        Task UpdateCarrotItem(CarrotItem carrotItem);
        Task DeleteCarrotItem(string id);

        Task<Profile> GetProfile(string login);
        Task CreateProfile(Profile profile);
        Task<bool> UpdateProfile(Profile profile);
        Task<bool> DeleteProfile(Profile profile);
        Task<List<DoItem>> GetDailyDoItems();
        Task AttachAllDoItems(string id);
    }

    public class MongoDBService : IMongoDBService
    {
        private readonly IMongoCollection<DoItem> _doItemsCollection;
        private readonly IMongoCollection<CarrotItem> _carrotItemsCollection;
        private readonly IMongoCollection<Profile> _profileCollection;
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

            string doItemsCollectionName = "DoItems";
            string carrotItemsCollectionName = "CarrotItems";
            string profileCollectionName = "Profiles";

            var database = _client.GetDatabase(dbName);
            _doItemsCollection = database.GetCollection<DoItem>(doItemsCollectionName);
            _profileCollection = database.GetCollection<Profile>(profileCollectionName);
            _carrotItemsCollection = database.GetCollection<CarrotItem>(carrotItemsCollectionName);
        }

        #region DoItems
        public async Task<List<DoItem>> GetAllDoItems(string profileId)
        {
            var doItemsCursor = await _doItemsCollection.FindAsync(_ => _.ProfileId == profileId);
            var doItems = await doItemsCursor.ToListAsync();
            return doItems;
        }
        public async Task<List<DoItem>> GetOpenedDoItems(string profileId)
        {
            var doItemsCursor = await _doItemsCollection.FindAsync(_ => _.IsFinished == false && _.ProfileId == profileId);
            var doItems = await doItemsCursor.ToListAsync();
            return doItems;
        }

        public async Task<DoItem> GetDoItem(string id)
        {
            return await _doItemsCollection.Find(x => x.Id == id).FirstAsync();
        }

        public async Task CreateDoItem(DoItem doItem)
        {
            await _doItemsCollection.InsertOneAsync(doItem);
        }

        public async Task UpdateDoItem(DoItem doItem)
        {
            await _doItemsCollection.ReplaceOneAsync(_ => _.Id == doItem.Id, doItem);
        }

        public async Task DeleteDoItem(string id)
        {
            await _doItemsCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<DoItem>> GetDailyDoItems()
        {
            var doItemsCursor = await _doItemsCollection
                .FindAsync(_ => _.Schedule == ScheduleType.Daily.ToString());
            var doItems = await doItemsCursor.ToListAsync();
            return doItems;
        }

        public async Task AttachAllDoItems(string id)
        {
            await _doItemsCollection.UpdateManyAsync(_ => true, new UpdateDefinitionBuilder<DoItem>().Set(x => x.ProfileId, id));
        }
        #endregion

        #region Profiles
        public async Task CreateProfile(Profile profile)
        {
            await _profileCollection.InsertOneAsync(profile);
        }

        public async Task<Profile> GetProfile(string login)
        {
            _logger.LogInformation($"GetProfil : {login}");

            // Récupérer le profil avec le login spécifié
            var profile = await _profileCollection.Find(x => x.Login == login).FirstOrDefaultAsync();

            //if (profile != null)
            //{
            //    profile.DoItems = await _doItemsCollection.Find(x => x.Id != null && profile.DoItemsIds.Contains(x.Id))?.ToListAsync();
            //}

            return profile;
        }

        public async Task<Profile?> GetProfileByDoItemId(string doItemId)
        {
            var doItem = await _doItemsCollection.Find(x => x.Id == doItemId).FirstOrDefaultAsync();
            if (doItem != null)
            {
                return await _profileCollection.Find(x => x.Id == doItem.ProfileId).FirstOrDefaultAsync();
            }
            return null;
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

        #region CarrotItems
        public async Task<List<CarrotItem>> GetAllCarrotItems(string profileId)
        {
            var cursor = await _carrotItemsCollection.FindAsync(_ => _.ProfileId == profileId);
            var items = await cursor.ToListAsync();
            return items;
        }

        public async Task<CarrotItem> GetCarrotItem(string id)
        {
            return await _carrotItemsCollection.Find(x => x.Id == id).FirstAsync();
        }

        public async Task CreateCarrotItem(CarrotItem item)
        {
            await _carrotItemsCollection.InsertOneAsync(item);
        }

        public async Task UpdateCarrotItem(CarrotItem item)
        {
            item.DateUpdated = _dateTimeProvider.GetNow();
            await _carrotItemsCollection.ReplaceOneAsync(_ => _.Id == item.Id, item);
        }

        public async Task DeleteCarrotItem(string id)
        {
            await _carrotItemsCollection.DeleteOneAsync(x => x.Id == id);
        }
        #endregion
    }
}
