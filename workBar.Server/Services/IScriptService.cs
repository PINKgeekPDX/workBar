using workBar.Server.Models;

namespace workBar.Server.Services
{
    public interface IScriptService
    {
        void ExecuteScript(ScriptConfig config);
    }
}
