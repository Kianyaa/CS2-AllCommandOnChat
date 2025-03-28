using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Logging;
using CounterStrikeSharp.API.Modules.Utils;

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
    public override string ModuleVersion => "1.0.1";
    public override string ModuleAuthor => "Kianya";
    public override string ModuleDescription => "A simple plugin that shows all commands available via chat";

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
    [CommandHelper(whoCanExecute: CommandUsage.CLIENT_ONLY)]
    private void ShowAvailableCommands(CCSPlayerController? player, CommandInfo commandInfo)
    {
        if (player != null && player.IsValid && player.PlayerPawn.IsValid &&
            player.Connected == PlayerConnectedState.PlayerConnected)
        {
            // Sending a header with color codes
            player?.PrintToChat(
                $" {ChatColors.Green}[Command] {ChatColors.Default} -  {ChatColors.Green} [Description]");

            foreach (var command in Config.CommandList)
            {
                // Using color codes for command output
                player?.PrintToChat(
                    $" {ChatColors.LightBlue}{command.Value.Command} {ChatColors.Default}:  {command.Value.Description}");

            }
        }

        // If the player is not valid, we will log the error
        else
        {
            Logger.LogError("Player is not valid");
        }

        return;
    }
}

