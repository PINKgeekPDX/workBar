namespace workBar.Server.Models
{
    public class AppLaunchConfig
    {
        public string Path { get; set; }
        public string Arguments { get; set; }
        public bool RunAsAdmin { get; set; }
    }
}
