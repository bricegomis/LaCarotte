using DoList.API.Manager;
using DoList.API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;

namespace DoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoItemController : ControllerBase
    {
        private readonly ILogger<DoItemController> _logger;
        private readonly IDoListManager _doListManager;

        public DoItemController(ILogger<DoItemController> logger,
                                IDoListManager doListManager)
        {
            _logger = logger;
            _doListManager = doListManager;
        }

        [HttpGet("attachAllDoItems")]
        [SwaggerOperation(OperationId = "attachAllDoItems")]
        public async Task AttachAllDoItems()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _logger.LogDebug("GetDoIteDailyResetmsAsync start");
            await _doListManager.AttachAllDoItems();

            stopwatch.Stop();
            _logger.LogDebug("DailyReset: {stopwatch.ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
        }

        [HttpGet("dailyreset")]
        [SwaggerOperation(OperationId = "DailyReset")]
        public async Task DailyReset()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _logger.LogDebug("GetDoIteDailyResetmsAsync start");
            await _doListManager.RefreshDailyDoItems();

            stopwatch.Stop();
            _logger.LogDebug("DailyReset: {stopwatch.ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
        }

        [HttpGet(Name = "GetAll")]
        [SwaggerOperation(OperationId = "GetAll")]
        public async Task<IEnumerable<DoItem>> GetDoItemsAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _logger.LogDebug("GetDoItemsAsync start");
            var doItems = await _doListManager.GetAllDoItems();

            stopwatch.Stop();
            _logger.LogDebug("GetDoItemsAsync: {stopwatch.ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);

            return doItems;
        }

        [HttpGet("{id}", Name = "GetById")]
        [Produces(typeof(DoItem))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(DoItem))]
        public async Task<DoItem> GetDoItem(string id)
        {
            _logger.LogDebug("GetDoItem called with id: {id}", id);
            return await _doListManager.GetDoItem(id);
        }

        [HttpPost(Name = "Create")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        public async Task CreateDoItem(DoItem item)
        {
            _logger.LogDebug("CreateDoItem called with id: {id}", item?.Id ?? "null");
            if (item == null) return;
            await _doListManager.CreateDoItem(item);
        }

        [HttpPut(Name = "Update")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        public async Task UpdateDoItem(DoItem item)
        {
            _logger.LogDebug("UpdateDoItem called with id: {id}", item?.Id ?? "null");
            if (item == null) return;
            await _doListManager.UpdateDoItem(item);
        }

        [HttpPut("finish", Name = "Finish")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        public async Task FinishDoItem(DoItem item)
        {
            _logger.LogDebug("FinishDoItem called with id: {id}", item?.Id ?? "null");
            if (item == null) return;
            await _doListManager.FinishDoItem(item);
        }

        [HttpDelete("{id}", Name = "Delete")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        public async Task DeleteDoItem(string id)
        {
            _logger.LogDebug("DeleteDoItem called with id: {id}", id ?? "null");
            if (id == null) return;
            await _doListManager.DeleteDoItem(id);
        }
    }
}