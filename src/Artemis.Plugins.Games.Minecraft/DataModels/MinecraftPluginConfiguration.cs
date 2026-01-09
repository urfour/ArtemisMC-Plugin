using Artemis.Core;
using System.ComponentModel;

namespace Artemis.Plugins.Games.Minecraft.DataModels
{
    public class MinecraftPluginConfiguration
    {
        public ConfigSetting<string> MinecraftPath { get; set; } = new ConfigSetting<string>(string.Empty);
        public ConfigSetting<string> TargetVersion { get; set; } = new ConfigSetting<string>(string.Empty);
    }
}
