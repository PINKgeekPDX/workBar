namespace workBar.Server.Models
{
    public class ScriptExecutionRequest
    {
        public required string ScriptPath { get; set; }
        public required string Interpreter { get; set; }
        public string[]? Arguments { get; set; }
    }
}