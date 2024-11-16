public class ScriptConfig
{
    public required string ScriptPath { get; set; }
    public required string Interpreter { get; set; }
    public required string[] Arguments { get; set; }
}

public class ActionConfig
{
    public required string Path { get; set; }
    public required string[] Arguments { get; set; }
}

public class UserSettings
{
    public required string Theme { get; set; }
    // Add other properties
}

public class Action
{
    public required string Id { get; set; }
    public required string Icon { get; set; }
    public required string ActionType { get; set; }
    public required ActionConfig ActionConfig { get; set; }
}

public class Application
{
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public required string PreviewImagePath { get; set; }
}