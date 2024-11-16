namespace workBar.Server.Models
{
    public class AppLaunchConfig
    {
        public required string Path { get; set; }
        public required string[] Arguments { get; set; }
    }
}