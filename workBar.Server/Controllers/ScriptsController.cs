using Microsoft.AspNetCore.Mvc;
using workBar.Server.Models;
using workBar.Server.Services;

namespace workBar.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScriptsController : ControllerBase
    {
        private readonly IScriptService _scriptService;

        public ScriptsController(IScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        [HttpPost("execute")]
        public IActionResult ExecuteScript([FromBody] ScriptConfig config)
        {
            try
            {
                _scriptService.ExecuteScript(config);
                return Ok(new { message = "Script executed successfully." });
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
