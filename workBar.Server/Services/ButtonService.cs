﻿using System;
using System.Diagnostics;
using workBar.Server.Models;

namespace workBar.Server.Services
{
    public class ButtonService : IButtonService
    {
        public void LaunchApplication(AppLaunchConfig config)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = config.Path,
                Arguments = config.Arguments,
                UseShellExecute = true,
                Verb = config.RunAsAdmin ? "runas" : null,
            };

            Process.Start(startInfo);
        }

        public void ExecuteScript(ScriptConfig config)
        {
            string interpreterPath = GetInterpreterPath(config.Interpreter);

            var startInfo = new ProcessStartInfo
            {
                FileName = interpreterPath,
                Arguments = $"{config.ScriptPath} {config.Arguments}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            var process = new Process
            {
                StartInfo = startInfo,
            };

            process.Start();
            process.WaitForExit();
        }

        private string GetInterpreterPath(string interpreter)
        {
            return interpreter.ToLower() switch
            {
                "powershell" => "powershell.exe",
                "cmd" => "cmd.exe",
                "python" => "python.exe",
                _ => throw new ArgumentException("Invalid interpreter specified."),
            };
        }
    }
}
