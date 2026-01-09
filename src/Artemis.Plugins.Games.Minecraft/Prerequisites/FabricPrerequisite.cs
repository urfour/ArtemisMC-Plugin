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
        
        private List<PluginPrerequisiteAction> _installActions;
        public override List<PluginPrerequisiteAction> InstallActions => _installActions;
        
        private List<PluginPrerequisiteAction> _uninstallActions;
        public override List<PluginPrerequisiteAction> UninstallActions => _uninstallActions;

        private readonly MinecraftPluginConfiguration _configuration;
        private readonly Plugin _plugin;
        private string FabricPath;

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
            
             // Logic mapping version to Fabric loader version
             // Simplification: assuming specific loader versions for demo, or using a map
            string loaderVersion = "0.18.4"; // Default fallback
            if (version == "1.21.11") loaderVersion = "0.18.4";
            else if (version == "1.21.10") loaderVersion = "0.18.4";
            else if (version == "1.20.1") loaderVersion = "0.14.25";
             
            FabricPath = Path.Combine(new string[] {
                baseFolder ?? string.Empty,
                ".minecraft",
                "versions",
                $"fabric-loader-{loaderVersion}-{version}"
            });
           
           // TODO: Update InstallActions dynamically? 
           // Prerequisite actions are usually static. If they need to be dynamic, we might need to recreate them or use a dynamic action.
           // For now, let's hardcode the actions based on the CURRENT config at construction?
           // Limitation: If user changes config at runtime, InstallActions here won't update unless we recreate the Prerequisite.
           
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
