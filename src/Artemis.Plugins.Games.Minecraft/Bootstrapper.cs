using Artemis.Core;
using Artemis.Plugins.Games.Minecraft.ViewModels;
using Artemis.Plugins.Games.Minecraft.DataModels;
using Artemis.Plugins.Games.Minecraft.Prerequisites;
using System;
using System.IO;

using Artemis.UI.Shared;

namespace Artemis.Plugins.Games.Minecraft;

public class Bootstrapper : PluginBootstrapper {
    public static MinecraftPluginConfiguration Configuration { get; private set; }

    public override void OnPluginEnabled(Plugin plugin)
    {
        MinecraftPluginConfiguration configuration = new MinecraftPluginConfiguration();
        Configuration = configuration;
        
        plugin.ConfigurationDialog = new PluginConfigurationDialog<MinecraftPathDialogViewModel>();
        
        if (string.IsNullOrEmpty(configuration.MinecraftPath.Value))
        {
             if (Environment.OSVersion.Platform == PlatformID.Win32NT)
             {
                 configuration.MinecraftPath.Value = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    ".minecraft"
                );
             }
             else
             {
                 configuration.MinecraftPath.Value = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".minecraft"
                );
             }
             configuration.MinecraftPath.Save();
        }

        AddPluginPrerequisite(new ConfigurationPrerequisite(plugin, configuration));
        AddPluginPrerequisite(new FabricPrerequisite(plugin, configuration));
        AddPluginPrerequisite(new ModPrerequisite(plugin, configuration));
    }
}
