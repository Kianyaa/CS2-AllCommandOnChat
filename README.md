
# CS2-AllCommandOnChat

**CS2 plugin for print all available command that provide in the config plugin file**

![2025-03-25 201827](https://github.com/user-attachments/assets/28ab2baf-8045-474e-84ac-95a3203e4008) <br>

## Features

- config all command name and description in json format file
- support color text in chat

## Requirements
- [MetaMod](https://cs2.poggu.me/metamod/installation)
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp)

## Installation

download latest relase version from [Releases Latest](https://github.com/Kianyaa/CS2-AllCommandOnChat/releases/tag/Latest)
and extract zip file and paste `CommandList.dll` on `addons\counterstrikesharp\plugins\CommandList` folder <br><br>
Restart the server and the plugin config file will generate at `addons\counterstrikesharp\configs\plugins\CommandList`

## Example of config file
```json
{
  "CommandList": {
    "command1": {
      "Command": "!Help",
      "Description": "Print all available command to console"
    },
    "command2": {
      "Command": "!stopsound [0-2]",
      "Description": "stop gun sound"
    },
    "command3": {
      "Command": "!hide [range]",
      "Description": "Hide teammate in specific range between 0-1000"
    }
  },
  "ConfigVersion": 1
}
```

## Credits
* Challengermode Dev Team for impletement colorsay in CS2 chat
    
