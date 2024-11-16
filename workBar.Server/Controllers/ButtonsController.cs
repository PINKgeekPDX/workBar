using Microsoft.AspNetCore.Mvc;
using workBar.Server.Models;
using workBar.Server.Services;

namespace workBar.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ButtonsController : ControllerBase
    {
        private readonly IButtonService _buttonService;

        public ButtonsController(IButtonService buttonService)
        {
            _buttonService = buttonService;
        }

        [HttpPost("launch-app")]
        public IActionResult LaunchApp([FromBody] AppLaunchConfig config)
        {
            try
            {
                _buttonService.LaunchApplication(config);
                return Ok(new { message = "Application launched successfully." });
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("execute-script")]
        public IActionResult ExecuteScript([FromBody] ScriptConfig config)
        {
            try
            {
                _buttonService.ExecuteScript(config);
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
