using Artemis.Core;
using Artemis.Plugins.Games.Minecraft.Actions;
using Artemis.Plugins.Games.Minecraft.DataModels;
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
        
        private List<PluginPrerequisiteAction> _installActions = new();
        public override List<PluginPrerequisiteAction> InstallActions => _installActions;
        
        private List<PluginPrerequisiteAction> _uninstallActions = new();
        public override List<PluginPrerequisiteAction> UninstallActions => _uninstallActions;

        private readonly MinecraftPluginConfiguration _configuration;
        private readonly Plugin _plugin;
        private string FabricPath = string.Empty;

        public FabricPrerequisite(Plugin plugin, MinecraftPluginConfiguration configuration)
        {
            _plugin = plugin;
            _configuration = configuration;
            Reevaluate();
        }

        public override bool IsMet()
        {
            if (string.IsNullOrEmpty(_configuration.MinecraftPath.Value)) return false;
            
            Reevaluate();
            return Path.Exists(FabricPath);
        }

        private void Reevaluate()
        {
            var baseFolder = _configuration.MinecraftPath.Value;
            var version = _configuration.TargetVersion.Value;
            
            string loaderVersion = "0.18.4";
            if (version == "1.21.11") loaderVersion = "0.18.4";
            else if (version == "1.21.10") loaderVersion = "0.18.4";
            else if (version == "1.20.1") loaderVersion = "0.14.25";
             
            FabricPath = Path.Combine(new string[] {
                baseFolder ?? string.Empty,
                "versions",
                $"fabric-loader-{loaderVersion}-{version}"
            });
           
            _installActions = new List<PluginPrerequisiteAction>()
            {
                new DownloadFileAction(
                    "Download Fabric Loader",
                    $"https://maven.fabricmc.net/net/fabricmc/fabric-installer/1.1.1/fabric-installer-1.1.1.jar",
                    Path.Combine(_plugin.Directory.FullName, "fabric-installer.jar")
               ),
               new ExecuteFileAction(
                   "Install Fabric Loader",
                   "java",
                   $"-jar {Path.Combine(_plugin.Directory.FullName, "fabric-installer.jar")} client -loader {loaderVersion} -mcversion {version}")
            };
            
             _uninstallActions = new List<PluginPrerequisiteAction>()
            {
                new DeleteFolderAction("Delete Fabric profile", FabricPath)
            };
        }
    }
}
