using System.Diagnostics;

namespace workBar.Server.Services
{
    public class ScriptService : IScriptService
    {
        public async Task<string> ExecuteScript(string scriptPath, string interpreter, string[] arguments)
        {
            using var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = interpreter,
                Arguments = $"{scriptPath} {string.Join(" ", arguments)}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            return output;
        }
    }
}