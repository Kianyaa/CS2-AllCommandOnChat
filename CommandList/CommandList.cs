using System.Text.RegularExpressions;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Logging;

namespace CommandList;


public class CommandConfig
{
    public string Command { get; set; } = "";
    public string Description { get; set; } = "";
}

public class Config : BasePluginConfig
{
    public Dictionary<string, CommandConfig> CommandList { get; set; } = new();
}

public class CommandListPlugin : BasePlugin, IPluginConfig<Config>
{
    public override string ModuleName => "CommandListPlugin";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "Kianya";
    public override string ModuleDescription => "A simple plugin that shows all commands available via console";

    public Config Config { get; set; } = new Config();

    public override void Load(bool hotReload)
    {
        Logger.LogInformation("CommandListPlugin Loaded");
        AddCommand("css_command", "Shows all available commands", ShowAvailableCommands);
        AddCommand("css_help", "Shows all available commands", ShowAvailableCommands);
    }

    public override void Unload(bool hotReload)
    {
        Logger.LogInformation("CommandListPlugin Unloaded");
    }

    // Method to handle the parsing of the config file
    public void OnConfigParsed(Config config)
    {
        Config = config;
    }

    // Method to show all available commands
    private void ShowAvailableCommands(CCSPlayerController? player, CommandInfo commandInfo)
    {
        // Sending a header with color codes
        commandInfo.ReplyToCommand(GetColoredText("{green}[Command Name]  :  [Description]{default}"));

        foreach (var command in Config.CommandList)
        {
            // Using color codes for command output
            commandInfo.ReplyToCommand($"{GetColoredText($"{{lightblue}}{command.Value.Command}{{default}}")}  :  {GetColoredText($"{{white}}{command.Value.Description}{{default}}")}");
        }
    }

    // Method to replace color codes with CS2 color codes
    private static string GetColoredText(string message)
    {
        Dictionary<string, int> colorMap = new()
        {
            { "{default}", 1 },
            { "{white}", 1 },
            { "{darkred}", 2 },
            { "{purple}", 3 },
            { "{green}", 4 },
            { "{lightgreen}", 5 },
            { "{slimegreen}", 6 },
            { "{red}", 7 },
            { "{grey}", 8 },
            { "{yellow}", 9 },
            { "{invisible}", 10 },
            { "{lightblue}", 11 },
            { "{blue}", 12 },
            { "{lightpurple}", 13 },
            { "{pink}", 14 },
            { "{fadedred}", 15 },
            { "{gold}", 16 }
        };

        string pattern = "{(\\w+)}"; // Matches {word}
        string replaced = Regex.Replace(message, pattern, match =>
        {
            string colorCode = match.Groups[1].Value;
            if (colorMap.TryGetValue("{" + colorCode + "}", out int replacement))
            {
                return Convert.ToChar(replacement).ToString(); // Get the color code for CS2
            }
            return match.Value; // If color not found, leave it unchanged
        });

        return $"\u200B{replaced}"; // Non-breaking space hack for ensuring colors work
    }
}

