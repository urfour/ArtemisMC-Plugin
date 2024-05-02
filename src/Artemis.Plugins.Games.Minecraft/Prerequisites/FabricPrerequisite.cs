using Artemis.Core;
using Artemis.Plugins.Games.Minecraft.Actions;
using Avalonia.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace Artemis.Plugins.Games.Minecraft.Prerequisites
{
    public class FabricPrerequisite : PluginPrerequisite
    {
        public override string Name => "Fabric";
        public override string Description => "Fabric needs to be installed";
        public override List<PluginPrerequisiteAction> InstallActions { get; }
        public override List<PluginPrerequisiteAction> UninstallActions { get; }
        private string FabricPath { get; set; }
        public override bool IsMet()
        {
            return Path.Exists(FabricPath);
        }
        public FabricPrerequisite(Plugin plugin, string specialFolder)
        {
            FabricPath = Path.Combine(
                specialFolder,
                ".minecraft",
                "versions",
                "fabric-loader-0.14.25-1.20.1"
           );

            InstallActions = new List<PluginPrerequisiteAction>()
            {
                new DownloadFileAction(
                    "Download Fabric Loader",
                    $"https://maven.fabricmc.net/net/fabricmc/fabric-installer/1.0.1/fabric-installer-1.0.1.jar",
                    Path.Combine(plugin.Directory.FullName, "fabric-installer.jar")
               ),
               new ExecuteFileAction(
                   "Install Fabric Loader",
                   "java",
                   $"-jar {Path.Combine(plugin.Directory.FullName, "fabric-installer.jar")} client -loader 0.14.25 -mcversion 1.20.1")  
            };

            UninstallActions = new List<PluginPrerequisiteAction>()
            {
                new DeleteFolderAction("Delete Fabric profile", FabricPath)
            };
        }
    }
}
