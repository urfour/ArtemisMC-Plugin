using Artemis.Core;
using Artemis.Plugins.Games.Minecraft.Actions;
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
        public FabricPrerequisite(Plugin plugin)
        {
            FabricPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                ".minecraft",
                "versions",
<<<<<<< Updated upstream
                "fabric-loader-0.14.21-1.20.1"
            );
=======
                "fabric-loader-0.14.25-1.20.1"
           );

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                   $"-jar {Path.Combine(plugin.Directory.FullName, "fabric-installer.jar")} client")
=======
                   $"-jar {Path.Combine(plugin.Directory.FullName, "fabric-installer.jar")} client -loader 0.14.25 -mcversion 1.20.1")
>>>>>>> Stashed changes
            };

            UninstallActions = new List<PluginPrerequisiteAction>()
            {
                new DeleteFolderAction("Delete Fabric profile", FabricPath)
            };
        }
    }
}
