namespace workBar.Server.Models
{
    public class ButtonConfig
    {
        public required string Id { get; set; }
        public required string Icon { get; set; }
        public required string ActionType { get; set; }
        public required ActionConfig ActionConfig { get; set; }
    }
}