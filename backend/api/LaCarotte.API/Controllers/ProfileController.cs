using DoList.API.Manager;
using DoList.API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IDoListManager _doListManager;

        public ProfileController(ILogger<ProfileController> logger, IDoListManager doListManager)
        {
            _logger = logger;
            _doListManager = doListManager;
        }

        [HttpGet(Name = "GetProfile")]
        [SwaggerOperation(OperationId = "GetProfile")]
        public ActionResult<Profile> GetCurrentProfile()
        {
            _logger.LogDebug("GetCurrentProfile");

            var profile = _doListManager.CurrentProfile;

            if (profile == null)
            {
                return NotFound($"CurrentProfile not initialized");
            }

            return Ok(profile);
        }
    }
}
