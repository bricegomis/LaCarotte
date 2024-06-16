using LaCarotte.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaCarotte.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryItemService _historyItemService;

        public HistoryController(IHistoryItemService historyItemService)
        {
            _historyItemService = historyItemService;
        }

        [HttpGet]
        public IActionResult GetHistoryItems()
        {
            var historyItems = _historyItemService.ListHistoryItems();
            return Ok(historyItems);
        }
    }
}