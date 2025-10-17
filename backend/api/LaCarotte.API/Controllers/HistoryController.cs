using LaCarotte.API.Manager;
using LaCarotte.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaCarotte.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        // private readonly IMongoDBService _mongoDBService;
        // private readonly ICarotteManager _carotteManager;

        // public HistoryController(IMongoDBService mongoDBService,
        //                          ICarotteManager carotteManager)
        // {
        //     _mongoDBService = mongoDBService;
        //     _carotteManager = carotteManager;
        // }

        // [HttpGet]
        // public async Task<List<HistoryItem>> GetHistoryItems()
        // {
        //     var profile = _carotteManager.GetProfile();
        //     var historyItems = await _mongoDBService.GetAllHistoryItems(profile.Id);
        //     return historyItems;
        // }
    }
}