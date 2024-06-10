//using DoList.API.Manager;
//using DoList.API.Models;
//using DoList.API.Services;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace DoList.API.Test
//{
//    public class DoListManagerTest
//    {
//        private readonly IDoListManager _doListManager;
//        private readonly Mock<IMongoDBService> _mongoDbService;
//        private readonly Mock<IDateTimeProvider> _datetimeProvider;
//        private const string _profileId = "123456789012345678901234";
//        private const string _profileLogin = "test";
//        private Profile _currentProfile;
//        private DateTimeOffset _now;

//        public DoListManagerTest()
//        {
//            var logger = Mock.Of<ILogger<IDoListManager>>();

//            _mongoDbService = new Mock<IMongoDBService>();

//            _datetimeProvider = new Mock<IDateTimeProvider>();
//            _now = DateTimeOffset.UtcNow;
//            _datetimeProvider.Setup(_ => _.GetNow()).Returns(_now);

//            _doListManager = new DoListManager(logger, _mongoDbService.Object, _datetimeProvider.Object);
            
//            _currentProfile = new Profile
//            {
//                Id = _profileId,
//                Login = _profileLogin,
//                ScoreTotal = 0,
//            };
//            _doListManager.CurrentProfile = _currentProfile;
//        }

//        [Fact]
//        public async Task FinishDoItem()
//        {
//            var id = Guid.NewGuid().ToString();
//            var doItem = new DoItem
//            {
//                Id = id,
//                Title = "test",
//                Points = 10,
//                Schedule = ScheduleType.Daily.ToString(),
//                ProfileId = "fake profile id"
//            };

//            // Mock
//            _mongoDbService.Setup(_ => _.GetDoItem(id)).ReturnsAsync(doItem);

//            // Before finishing
//            Assert.Equal(false, doItem.IsFinished);
//            Assert.Empty(doItem.History);
//            Assert.Equal(0, _currentProfile.ScoreTotal);

//            await _doListManager.FinishDoItem(doItem);

//            // Check updates has been called
//            _mongoDbService.Verify(_ => _.UpdateDoItem(doItem), Times.Once());
//            _mongoDbService.Verify(_ => _.UpdateProfile(_currentProfile), Times.Once());

//            Assert.NotEmpty(doItem.History);
//            Assert.Equal(_profileId, doItem.ProfileId);
//            Assert.Equal(10, _currentProfile.ScoreTotal);
//            Assert.Equal(true, doItem.IsFinished);
//            Assert.Equal(_now, doItem.DateFinished);
//            Assert.Equal(_now, doItem.DateUpdated);
//        }

//        [Fact]
//        public async Task FinishDoItem_InvalidData()
//        {
//            var id = Guid.NewGuid().ToString();
//            var doItem = new DoItem
//            {
//                Id = id,
//                Points = 10,
//                Title = "test",
//                Schedule = ScheduleType.Daily.ToString(),
//                ProfileId = "fake profile id"
//            };

//            // No item found
//            await _doListManager.FinishDoItem(doItem);

//            // Check updates has been called
//            _mongoDbService.Verify(x => x.UpdateDoItem(It.IsAny<DoItem>()), Times.Never);
//            _mongoDbService.Verify(x => x.UpdateProfile(It.IsAny<Profile>()), Times.Never);

//            // Null item passed
//            await _doListManager.FinishDoItem(null);

//            _doListManager.CurrentProfile = null;
//            _mongoDbService.Setup(_ => _.GetDoItem(id)).ReturnsAsync(doItem);
//            await _doListManager.FinishDoItem(doItem);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        [Fact]
//        public async Task UpdateDoItem()
//        {
//            var id = Guid.NewGuid().ToString();
//            var doItem = new DoItem
//            {
//                Id = id,
//                Points = 10,
//                Title = "test",
//                Schedule = ScheduleType.Daily.ToString(),
//                ProfileId = "fake profile id"
//            };

//            // Mock
//            _mongoDbService.Setup(_ => _.GetDoItem(id)).ReturnsAsync(doItem);

//            // Before finishing
//            Assert.NotEqual(_now, doItem.DateUpdated);
            
//            await _doListManager.UpdateDoItem(doItem);

//            // Check updates has been called
//            _mongoDbService.Verify(_ => _.UpdateDoItem(doItem), Times.Once());

//            Assert.Equal(_now, doItem.DateUpdated);
//            Assert.Equal(_profileId, doItem.ProfileId);
//        }

//        [Fact]
//        public async Task UpdateDoItem_InvalidData()
//        {
//            var id = Guid.NewGuid().ToString();
//            var doItem = new DoItem
//            {
//                Id = id,
//                Points = 10,
//                Title = "test",
//                Schedule = ScheduleType.Daily.ToString(),
//                ProfileId = "fake profile id"
//            };

//            // No item found
//            await _doListManager.UpdateDoItem(doItem);

//            // Null item passed
//            await _doListManager.UpdateDoItem(null);

//            _doListManager.CurrentProfile = null;
//            _mongoDbService.Setup(_ => _.GetDoItem(id)).ReturnsAsync(doItem);
//            await _doListManager.UpdateDoItem(doItem);
//        }

//        [Fact]
//        public async Task RefreshDailyDoItems()
//        {
//            var doItems = new List<DoItem>()
//            {
//                new() {
//                    Id = Guid.NewGuid().ToString(),
//                    Schedule = ScheduleType.Daily.ToString(),
//                    IsFinished = true,
//                    Points = 50,
//                },
//                new() {
//                    Id = Guid.NewGuid().ToString(),
//                    Schedule = ScheduleType.Daily.ToString(),
//                    IsFinished = false,
//                    Points = 50,
//                }
//            };
//            _mongoDbService.Setup(_ => _.GetDailyDoItems()).ReturnsAsync(doItems);

//            await _doListManager.RefreshDailyDoItems();

//            // The first one is finished, so bonus will be applied
//            Assert.Equal(60, doItems[0].Points);

//            // The second one is not finished, so bonus will not be applied and reset to default
//            Assert.Equal(10, doItems[1].Points);
//        }
//    }
//}
