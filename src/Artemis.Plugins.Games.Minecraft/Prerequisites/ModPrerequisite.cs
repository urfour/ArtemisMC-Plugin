using Artemis.Core;
using Artemis.Plugins.Games.Minecraft.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.Minecraft.Prerequisites
{
    public class ModPrerequisite : PluginPrerequisite
    {
        public class CreateFolderAction : PluginPrerequisiteAction
        {
            public string Target { get; }

            public CreateFolderAction(string name, string target)
                : base(name)
            {
                Target = target;
                ProgressIndeterminate = true;
            }

            public override async Task Execute(CancellationToken cancellationToken)
            {
                ShowProgressBar = true;
                Status = "Creating " + Target;
                await Task.Run(delegate
                {
                    Directory.CreateDirectory(Target);
                }, cancellationToken);
                ShowProgressBar = false;
                Status = "Created " + Target;
            }
        }

        public override string Name => "ArtemisMC Mod";
        public override string Description => "ArtemisMC needs to be installed";
        
        private List<PluginPrerequisiteAction> _installActions = new();
        public override List<PluginPrerequisiteAction> InstallActions => _installActions;
        
        private List<PluginPrerequisiteAction> _uninstallActions = new();
        public override List<PluginPrerequisiteAction> UninstallActions => _uninstallActions;
        public ModPrerequisite(Plugin plugin, MinecraftPluginConfiguration configuration)
        {
            _plugin = plugin;
            _configuration = configuration;
            Reevaluate();
        }

        private readonly MinecraftPluginConfiguration _configuration;
        private readonly Plugin _plugin;
        private string ModFilename = string.Empty;

        public override bool IsMet() {
             if (string.IsNullOrEmpty(_configuration.MinecraftPath.Value)) return false;
             Reevaluate();
             return Path.Exists(ModFilename);
        }

        private void Reevaluate()
        {
            var baseFolder = _configuration.MinecraftPath.Value;
            var version = _configuration.TargetVersion.Value;
            
            string modVersion = _plugin.Info.Version; 

            ModFilename = Path.Combine(new string[] {
                baseFolder ?? string.Empty,
                ".minecraft",
                "mods",
                $"artemismc-{modVersion}-{version}.jar"
            }); 
            
            _installActions = new List<PluginPrerequisiteAction>()
            {
                new CreateFolderAction(
                    "Create mods folder",
                    Path.Combine(baseFolder ?? string.Empty, ".minecraft", "mods")
                ),
                new DownloadFileAction(
                    "Download ArtemisMC mod",
                    $"https://github.com/urfour/ArtemisMC-Fabric/releases/latest/download/artemismc-{modVersion}-{version}.jar",
                    ModFilename
               )
            };

            _uninstallActions = new List<PluginPrerequisiteAction>()
            {
                new DeleteFileAction("Delete mod", ModFilename)
            };

        }
    }
}
