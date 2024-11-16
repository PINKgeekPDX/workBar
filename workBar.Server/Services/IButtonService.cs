using workBar.Server.Models;

namespace workBar.Server.Services
{
    public interface IButtonService
    {
        void LaunchApplication(AppLaunchConfig config);
        void ExecuteScript(ScriptConfig config);
    }
}
