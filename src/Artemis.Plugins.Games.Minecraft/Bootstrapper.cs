using Artemis.Core;
using Artemis.Plugins.Games.Minecraft.Prerequisites;
using System;
using System.IO;

namespace Artemis.Plugins.Games.Minecraft;

public class Bootstrapper : PluginBootstrapper {
    public override void OnPluginEnabled(Plugin plugin)
    {
        string specialFolder;
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        else
        {
            specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
        if (Path.Exists(Path.Combine(specialFolder, ".minecraft")))
        {
            AddPluginPrerequisite(new FabricPrerequisite(plugin, specialFolder));
            AddPluginPrerequisite(new ModPrerequisite(plugin, specialFolder));
        }
    }
}
