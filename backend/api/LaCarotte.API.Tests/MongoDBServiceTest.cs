using DoList.API.Models;
using DoList.API.Services;
using Microsoft.Extensions.Logging;
using Moq;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;

namespace DoList.API.Test
{
    public class MongoDBServiceTest : IDisposable
    {
        private readonly IMongoDBService _mongoDBService;

        private string _profileId;
        const string _profileLogin = "user 1";

        #region Ctor & Dispose
        public MongoDBServiceTest()
        {
            var configBuilder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true)
                    .Build();

            _profileId = ObjectId.GenerateNewId().ToString();
            var host = configBuilder["MongoDB:Host"];
            var port = configBuilder["MongoDB:Port"];
            var login = configBuilder["MongoDB:Login"];
            var password = configBuilder["MongoDB:Password"];

            var logger = Mock.Of<ILogger<MongoDBService>>();
            var dateTimeProvider = Mock.Of<IDateTimeProvider>();
            var connectionString = $"mongodb://{login}:{password}@{host}:{port}/";
            _mongoDBService = new MongoDBService(logger, dateTimeProvider, connectionString, "DbList_UnitTest");
            CreateUser1().GetAwaiter().GetResult();
        }
        private async Task CreateUser1()
        {
            var user = await _mongoDBService.GetProfile(_profileLogin);
            Assert.Null(user);
            await _mongoDBService.CreateProfile(new Profile
            {
                Id = _profileId,
                Login = "user 1",
                ScoreTotal = 0,
                ScoreWeek = 0,
                DoItemsIds = new List<string>()
            });

            user = await _mongoDBService.GetProfile(_profileLogin);
            Assert.NotNull(user);
            Assert.Equal(_profileId, user.Id);
        }
        public void Dispose()
        {
            DeleteUser1().GetAwaiter().GetResult();
        }
        private async Task DeleteUser1()
        {
            var user = await _mongoDBService.GetProfile(_profileLogin);
            Assert.NotNull(user);

            var result = await _mongoDBService.DeleteProfile(user);
            Assert.True(result);

            user = await _mongoDBService.GetProfile(_profileLogin);
            Assert.Null(user);
        }
        #endregion

        [Fact]
        public async Task UpdateProfile_AddPoint()
        {
            var user = await _mongoDBService.GetProfile(_profileLogin);
            Assert.NotNull(user);
            
            user.ScoreTotal += 10;
            var result = await _mongoDBService.UpdateProfile(user);
            Assert.True(result);

            user = await _mongoDBService.GetProfile(_profileLogin);
            Assert.Equal(10, user.ScoreTotal);
        }
    }
}
