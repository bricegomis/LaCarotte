using LaCarotte.API.Models;
using LaCarotte.API.Services;

namespace LaCarotte.API.Manager
{
    public class CarotteManager(ILogger<ICarotteManager> logger,
                               IMongoDBService mongoDBService,
                               IHistoryItemService historyItemService,
                               IDateTimeProvider dateTimeProvider) : BackgroundService, ICarotteManager
    {
        private const string Login = "Test";
        private readonly IMongoDBService _mongoDBService = mongoDBService;
        private readonly IHistoryItemService _historyItemService = historyItemService;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
        private readonly ILogger<ICarotteManager> _logger = logger;
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
                _logger.LogInformation("========================> CurrentProfile null, creating new one");
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
                _logger.LogInformation("========================> CurrentProfile found, updating dateLastConnection");
                CurrentProfile.DateLastConnection = _dateTimeProvider.GetNow();
                await _mongoDBService.UpdateProfile(CurrentProfile);
            }

            _logger.LogInformation("========================> CurrentProfile init finished");
        }

        public async Task FinishCarotte(Carotte carotteFrom)
        {
            if (carotteFrom is null || carotteFrom.Id is null)
            {
                _logger.LogError("carotte is null");
                return;
            }
            if (CurrentProfile is null)
            {
                _logger.LogError("CurrentProfile is null");
                return;
            }
            var carotte = await _mongoDBService.GetCarotte(carotteFrom.Id);
            if (carotte is null)
            {
                _logger.LogError($"carotte not found with id {carotteFrom.Id}");
                return;
            }

            // Force the profilId to ensure it's not update from the client
            carotte.ProfileId = CurrentProfile.Id;
            carotte.DateUpdated = _dateTimeProvider.GetNow();

            if (carotte.History is null)
                carotte.History = [];
            carotte.History.Add(_dateTimeProvider.GetNow());
            carotte.HistoryBonus += 1;// TODO: reduce if bad ?

            if (carotte.Points.HasValue) {
                var pts = carotte.IsReward == true ? carotte.Points.Value : -carotte.Points.Value;
                CurrentProfile.ScoreTotal += pts;
                CurrentProfile.ScoreWeek += pts;
                CurrentProfile.ScoreDay += pts;
            }

            await _mongoDBService.UpdateCarotte(carotte);
            await _mongoDBService.UpdateProfile(CurrentProfile);

            var history = new HistoryItem
            {
                Item = carotte,
                Date = _dateTimeProvider.GetNow(),
                Points = carotte.IsReward == true ? carotte.Points ?? 0 : -carotte.Points ?? 0,
                ProfileId = CurrentProfile.Id
            };
            await _mongoDBService.CreateHistory(history);

            // To Elastic
            await _historyItemService.AddHistoryItem(history);
        }

        public async Task UpdateCarotte(Carotte carotte)
        {
            if (carotte is null)
            {
                _logger.LogError("carotte is null");
                return;
            }
            if (CurrentProfile is null)
            {
                _logger.LogError("CurrentProfile is null");
                return;
            }

            carotte.ProfileId = CurrentProfile.Id;// Force the profilId to ensure it's not update from the client
            carotte.DateUpdated = _dateTimeProvider.GetNow();
            await _mongoDBService.UpdateCarotte(carotte);
        }

        public async Task DeleteCarotte(string id)
        {
            if (CurrentProfile is null) return;
            // Check the carotte online to be sure
            var carotte = await _mongoDBService.GetCarotte(id);
            if (carotte is null || carotte.Id is null) return;
            if (carotte.ProfileId != CurrentProfile.Id)
            {
                _logger.LogWarning("Try to delete a carotte with different profileId than currentProfile");
                return;
            }

            CurrentProfile.carotteIds.Remove(carotte.Id);
            await _mongoDBService.DeleteCarotte(carotte.Id);
        }

        public async Task CreateCarotte(Carotte carotte)
        {
            _logger.LogInformation("CreateCarotte");
            if (CurrentProfile == null || carotte is null
                || carotte.Id is null)
            {
                _logger.LogError("CurrentProfile not defined");
                return;
            }
            carotte.ProfileId = CurrentProfile.Id;
            carotte.DateCreated = _dateTimeProvider.GetNow();
            carotte.DateUpdated = _dateTimeProvider.GetNow();
            await _mongoDBService.CreateCarotte(carotte);

            if (CurrentProfile.carotteIds is null)
                CurrentProfile.carotteIds = [];
            CurrentProfile.carotteIds.Add(carotte.Id);
            await _mongoDBService.UpdateProfile(CurrentProfile);
        }

        public async Task<Carotte> GetCarotte(string id)
        {
            return await _mongoDBService.GetCarotte(id);
        }

        public async Task<List<Carotte>> GetAllCarottes()
        {
            if (CurrentProfile is null || CurrentProfile.Id is null) return [];
            return await _mongoDBService.GetAllCarottes(CurrentProfile.Id);
        }

        public Profile? GetProfile()
        {
            return CurrentProfile;
        }
    }
}