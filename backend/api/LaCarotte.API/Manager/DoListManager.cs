using DoList.API.Models;
using DoList.API.Services;

namespace DoList.API.Manager
{
    public interface IDoListManager
    {
        Task<DoItem> GetDoItem(string id);
        Task<List<DoItem>> GetAllDoItems();
        Task FinishDoItem(DoItem doItem);
        Task UpdateDoItem(DoItem doItem);
        Task DeleteDoItem(string id);
        Task CreateDoItem(DoItem doItem);
        Profile? CurrentProfile { get; set; }
        Task RefreshDailyDoItems();
        Task AttachAllDoItems();
        Task<List<CarrotItem>> GetAllCarrotItems();
        Task FinishCarrotItem(CarrotItem item);
    }

    public class DoListManager(ILogger<IDoListManager> logger,
                               IMongoDBService mongoDBService,
                               IDateTimeProvider dateTimeProvider) : BackgroundService, IDoListManager
    {
        private const string Login = "Test";
        private readonly IMongoDBService _mongoDBService = mongoDBService;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
        private readonly ILogger<IDoListManager> _logger = logger;
        public Profile? CurrentProfile { get; set; }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await InitDefaultProfile();
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task InitDefaultProfile()
        {
            _logger.LogInformation("========================> Starting init default profile");
            CurrentProfile = await _mongoDBService.GetProfile(Login);
            if (CurrentProfile == null)
            {
                _logger.LogInformation("========================> CurrentProfilenull, creating new one");
                await _mongoDBService.CreateProfile(new Profile
                {
                    Login = Login,
                    ScoreTotal = 0,
                    ScoreWeek = 0,
                    DateCreated = _dateTimeProvider.GetNow(),
                    DateLastConnection = _dateTimeProvider.GetNow()
                });
                CurrentProfile = await _mongoDBService.GetProfile(Login);
            }
            else
            {
                _logger.LogInformation("========================> CurrentProfilenull found, updating dateLastConnection");
                CurrentProfile.DateLastConnection = _dateTimeProvider.GetNow();
                await _mongoDBService.UpdateProfile(CurrentProfile);
            }

            _logger.LogInformation("========================> CurrentProfilenull finished");
        }

        public async Task FinishDoItem(DoItem doItemFrom)
        {
            if (doItemFrom is null || doItemFrom.Id is null)
            {
                _logger.LogError("doItem is null");
                return;
            }
            if (CurrentProfile is null)
            {
                _logger.LogError("CurrentProfile is null");
                return;
            }
            var doItem = await _mongoDBService.GetDoItem(doItemFrom.Id);
            if (doItem is null)
            {
                _logger.LogError($"doItem not found with id {doItemFrom.Id}");
                return;
            }

            // Force the profilId to ensure it's not update from the client
            doItem.ProfileId = CurrentProfile.Id;
            doItem.IsFinished = true;
            doItem.DateFinished = _dateTimeProvider.GetNow();
            doItem.DateUpdated = _dateTimeProvider.GetNow();

            if (doItem.History is null)
                doItem.History = [];
            doItem.History.Add(_dateTimeProvider.GetNow());
            doItem.HistoryBonus += 1;

            CurrentProfile.ScoreTotal += doItem.Points ?? 0;
            CurrentProfile.ScoreWeek += doItem.Points ?? 0;

            await _mongoDBService.UpdateDoItem(doItem);
            await _mongoDBService.UpdateProfile(CurrentProfile);
        }

        public async Task UpdateDoItem(DoItem doItem)
        {
            if (doItem is null)
            {
                _logger.LogError("doItem is null");
                return;
            }
            if (CurrentProfile is null)
            {
                _logger.LogError("CurrentProfile is null");
                return;
            }

            doItem.ProfileId = CurrentProfile.Id;// Force the profilId to ensure it's not update from the client
            doItem.DateUpdated = _dateTimeProvider.GetNow();
            await _mongoDBService.UpdateDoItem(doItem);
        }

        public async Task DeleteDoItem(string id)
        {
            if (CurrentProfile is null) return;
            // Check the doItem online to be sure
            var doItem = await _mongoDBService.GetDoItem(id);
            if (doItem is null || doItem.Id is null) return;
            if (doItem.ProfileId != CurrentProfile.Id)
            {
                _logger.LogWarning("Try to delete a doItem with different profileId than currentProfile");
                return;
            }

            CurrentProfile.DoItemsIds.Remove(doItem.Id);
            await _mongoDBService.DeleteDoItem(doItem.Id);
        }

        public async Task CreateDoItem(DoItem doItem)
        {
            _logger.LogInformation("CreateDoItem");
            if (CurrentProfile == null || doItem is null
                || doItem.Id is null)
            {
                _logger.LogError("CurrentProfile not defined");
                return;
            }
            doItem.ProfileId = CurrentProfile.Id;
            doItem.DateCreated = _dateTimeProvider.GetNow();
            doItem.DateUpdated = _dateTimeProvider.GetNow();
            doItem.IsFinished = false;
            await _mongoDBService.CreateDoItem(doItem);

            if (CurrentProfile.DoItemsIds is null)
                CurrentProfile.DoItemsIds = [];
            CurrentProfile.DoItemsIds.Add(doItem.Id);
            await _mongoDBService.UpdateProfile(CurrentProfile);
        }

        public async Task<DoItem> GetDoItem(string id)
        {
            return await _mongoDBService.GetDoItem(id);
        }

        public async Task<List<DoItem>> GetAllDoItems()
        {
            if (CurrentProfile is null || CurrentProfile.Id is null) return new List<DoItem>();
            return await _mongoDBService.GetAllDoItems(CurrentProfile.Id);
        }

        public async Task RefreshDailyDoItems()
        {
            var doItems = await _mongoDBService.GetDailyDoItems();
            foreach (var doItem in doItems)
            {
                if (doItem.IsFinished is true)
                {
                    doItem.HistoryBonus = doItem.HistoryBonus + 1;
                }
                else
                {
                    doItem.HistoryBonus = 0;
                }
                doItem.IsFinished = false;
                await _mongoDBService.UpdateDoItem(doItem);
            }
        }

        public async Task AttachAllDoItems()
        {
            if (CurrentProfile is null || CurrentProfile.Id is null) return;
            await _mongoDBService.AttachAllDoItems(CurrentProfile.Id);
        }

        public async Task<List<CarrotItem>> GetAllCarrotItems()
        {
            if (CurrentProfile is null || CurrentProfile.Id is null) return new List<CarrotItem>();
            return await _mongoDBService.GetAllCarrotItems(CurrentProfile.Id);
        }

        public async Task FinishCarrotItem(CarrotItem item)
        {
            if (CurrentProfile is null || CurrentProfile.Id is null) return;

            item.DateUpdated = _dateTimeProvider.GetNow();
            if (item.History is null) item.History = new List<DateTimeOffset>();
            item.History.Add(_dateTimeProvider.GetNow());

            CurrentProfile.ScoreTotal -= item.Points ?? 0;
            CurrentProfile.ScoreWeek -= item.Points ?? 0;

            await _mongoDBService.UpdateProfile(CurrentProfile);

            await _mongoDBService.UpdateCarrotItem(item);
        }
    }
}