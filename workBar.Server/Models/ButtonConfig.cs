namespace workBar.Server.Models
{
    public class ButtonConfig
    {
        public string Id { get; set; }
        public string Icon { get; set; }
        public bool Customizable { get; set; }
        public string ActionType { get; set; }
        public object ActionConfig { get; set; }
    }
}
