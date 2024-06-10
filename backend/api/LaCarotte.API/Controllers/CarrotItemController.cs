using DoList.API.Manager;
using DoList.API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;

namespace DoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrotItemController : ControllerBase
    {
        private readonly ILogger<CarrotItemController> _logger;
        private readonly IDoListManager _doListManager;

        public CarrotItemController(ILogger<CarrotItemController> logger,
                                IDoListManager doListManager)
        {
            _logger = logger;
            _doListManager = doListManager;
        }

        //[HttpGet("attachAllCarrotItems")]
        //[SwaggerOperation(OperationId = "attachAllCarrotItems")]
        //public async Task AttachAllCarrotItems()
        //{
        //    var stopwatch = new Stopwatch();
        //    stopwatch.Start();

        //    _logger.LogDebug("GetDoIteDailyResetmsAsync start");
        //    await _doListManager.AttachAllCarrotItems();

        //    stopwatch.Stop();
        //    _logger.LogDebug("DailyReset: {stopwatch.ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
        //}

        [HttpGet(Name = "[controller]-GetAll")]
        [SwaggerOperation(OperationId = "[controller]-GetAll")]
        public async Task<IEnumerable<CarrotItem>> GetCarrotItemsAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _logger.LogDebug("GetCarrotItemsAsync start");
            var carrotItems = await _doListManager.GetAllCarrotItems();

            stopwatch.Stop();
            _logger.LogDebug("GetCarrotItemsAsync: {stopwatch.ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);

            return carrotItems;
        }

        //[HttpGet("{id}", Name = "GetById")]
        //[Produces(typeof(CarrotItem))]
        //[SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(CarrotItem))]
        //public async Task<CarrotItem> GetCarrotItem(string id)
        //{
        //    _logger.LogDebug("GetCarrotItem called with id: {id}", id);
        //    return await _doListManager.GetCarrotItem(id);
        //}

        //[HttpPost(Name = "Create")]
        //[SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        //public async Task CreateCarrotItem(CarrotItem item)
        //{
        //    _logger.LogDebug("CreateCarrotItem called with id: {id}", item?.Id ?? "null");
        //    await _doListManager.CreateCarrotItem(item);
        //}

        //[HttpPut(Name = "Update")]
        //[SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        //public async Task UpdateCarrotItem(CarrotItem item)
        //{
        //    _logger.LogDebug("UpdateCarrotItem called with id: {id}", item?.Id ?? "null");
        //    await _doListManager.UpdateCarrotItem(item);
        //}

        [HttpPut("finish", Name = "FinishCarrot")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        public async Task FinishCarrotItem(CarrotItem item)
        {
            if (item == null) return;
            _logger.LogDebug("FinishCarrotItem called with id: {id}", item.Id);
            await _doListManager.FinishCarrotItem(item);
        }

        //[HttpDelete("{id}", Name = "Delete")]
        //[SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        //public async Task DeleteCarrotItem(string id)
        //{
        //    _logger.LogDebug("DeleteCarrotItem called with id: {id}", id ?? "null");
        //    await _doListManager.DeleteCarrotItem(id);
        //}
    }
}