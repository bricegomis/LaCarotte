using LaCarotte.API.Manager;
using LaCarotte.API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LaCarotte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly ICarotteManager _carotteManager;

        public ProfileController(ILogger<ProfileController> logger, ICarotteManager carotteManager)
        {
            _logger = logger;
            _carotteManager = carotteManager;
        }

        [HttpGet(Name = "GetProfile")]
        [SwaggerOperation(OperationId = "GetProfile")]
        public ActionResult<Profile> GetCurrentProfile()
        {
            _logger.LogDebug("GetCurrentProfile");

            var profile = _carotteManager.GetProfile();

            if (profile == null)
            {
                return NotFound($"CurrentProfile not initialized");
            }

            return Ok(profile);
        }
    }
}
