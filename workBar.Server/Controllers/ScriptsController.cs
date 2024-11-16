using Microsoft.AspNetCore.Mvc;
using workBar.Server.Services;

namespace workBar.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScriptsController : ControllerBase
    {
        private readonly IScriptService _scriptService;

        public ScriptsController(IScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        [HttpPost("execute")]
        public async Task<IActionResult> ExecuteScript([FromBody] ScriptExecutionRequest request)
        {
            try
            {
                var result = await _scriptService.ExecuteScript(
                    request.ScriptPath,
                    request.Interpreter,
                    request.Arguments ?? Array.Empty<string>()
                );
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}