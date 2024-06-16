using LaCarotte.API.Manager;
using LaCarotte.API.Models;
using LaCarotte.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;

namespace LaCarotte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarotteController : ControllerBase
    {
        private readonly ILogger<CarotteController> _logger;
        private readonly ICarotteManager _carotteManager;

        public CarotteController(ILogger<CarotteController> logger,
                                ICarotteManager carotteManager)
        {
            _logger = logger;
            _carotteManager = carotteManager;
        }

        [HttpGet(Name = "[controller]-GetAll")]
        [SwaggerOperation(OperationId = "[controller]-GetAll")]
        public async Task<IEnumerable<Carotte>> GetcarotteAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _logger.LogDebug("GetcarotteAsync start");
            var carottes = await _carotteManager.GetAllCarottes();

            stopwatch.Stop();
            _logger.LogDebug("GetcarotteAsync: {stopwatch.ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);

            return carottes;
        }

        [HttpGet("{id}", Name = "GetById")]
        [Produces(typeof(Carotte))]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(Carotte))]
        public async Task<Carotte> GetCarotte(string id)
        {
            _logger.LogDebug("GetCarotte called with id: {id}", id);
            return await _carotteManager.GetCarotte(id);
        }

        [HttpPost(Name = "Create")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        public async Task CreateCarotte(Carotte item)
        {
            _logger.LogDebug("CreateCarotte called with id: {id}", item?.Id ?? "null");
            if (item == null) return;
            await _carotteManager.CreateCarotte(item);
        }

        [HttpPut(Name = "Update")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        public async Task UpdateCarotte(Carotte item)
        {
            _logger.LogDebug("UpdateCarotte called with id: {id}", item?.Id ?? "null");
            if (item == null) return;
            await _carotteManager.UpdateCarotte(item);
        }

        [HttpPut("finish", Name = "Finish")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        public async Task FinishCarotte(Carotte item)
        {
            _logger.LogDebug("FinishCarotte called with id: {id}", item?.Id ?? "null");
            if (item == null) return;
            await _carotteManager.FinishCarotte(item);
        }

        [HttpDelete("{id}", Name = "Delete")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        public async Task DeleteCarotte(string id)
        {
            _logger.LogDebug("DeleteCarotte called with id: {id}", id ?? "null");
            if (id == null) return;
            await _carotteManager.DeleteCarotte(id);
        }
    }
}