namespace workBar.Server.Models
{
    public class ScriptConfig
    {
        public string ScriptPath { get; set; }
        public string Interpreter { get; set; } // "powershell", "cmd", "python"
        public string Arguments { get; set; }
    }
}
