namespace workBar.Server.Services
{
    public interface IScriptService
    {
        Task<string> ExecuteScript(string scriptPath, string interpreter, string[] arguments);
    }
}