using Microsoft.AspNetCore.Mvc;
using workBar.Server.Models;

namespace workBar.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSettings()
        {
            // Retrieve settings from storage
            var settings = new UserSettings
            {
                Theme = "default",
                // Add other settings
            };
            return Ok(settings);
        }

        [HttpPost]
        public IActionResult SaveSettings([FromBody] UserSettings settings)
        {
            try
            {
                // Save settings to storage
                return Ok(new { message = "Settings saved successfully." });
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
