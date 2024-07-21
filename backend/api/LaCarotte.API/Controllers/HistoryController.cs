using LaCarotte.API.Manager;
using LaCarotte.API.Models;
using LaCarotte.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaCarotte.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryItemService _historyItemService;
        private readonly IMongoDBService _mongoDBService;
        private readonly ICarotteManager _carotteManager;

        public HistoryController(IHistoryItemService historyItemService,
                                 IMongoDBService mongoDBService,
                                 ICarotteManager carotteManager)
        {
            _historyItemService = historyItemService;
            _mongoDBService = mongoDBService;
            _carotteManager = carotteManager;
        }

        [HttpGet]
        public async Task<List<HistoryItem>> GetHistoryItems()
        {
            var profile = _carotteManager.GetProfile();
            var historyItems = await _mongoDBService.GetAllHistoryItems(profile.Id);
            return historyItems;
        }
    }
}