using Artemis.Core;
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
            //
            // Résumé :
            //     Gets or sets the target directory
            public string Target { get; }

            //
            // Résumé :
            //     Creates a new instance of a folder action
            //
            // Paramètres :
            //   name:
            //     The name of the action
            //
            //   target:
            //     The target folder to create
            public CreateFolderAction(string name, string target)
                : base(name)
            {
                Target = target;
                base.ProgressIndeterminate = true;
            }

            public override async Task Execute(CancellationToken cancellationToken)
            {
                base.ShowProgressBar = true;
                base.Status = "Creating " + Target;
                await Task.Run(delegate
                {
                    Directory.CreateDirectory(Target);
                }, cancellationToken);
                base.ShowProgressBar = false;
                base.Status = "Created " + Target;
            }
        }

        public override string Name => "ArtemisMC Mod";
        public override string Description => "ArtemisMC needs to be installed";
        public override List<PluginPrerequisiteAction> InstallActions { get; }
        public override List<PluginPrerequisiteAction> UninstallActions { get; }
        private string ModFilename { get; set; }
        private string SpecialFolder { get; set; }
        public override bool IsMet() {
            return Path.Exists(ModFilename);
        }
        public ModPrerequisite(Plugin plugin)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SpecialFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
            else
            {
                SpecialFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }
            ModFilename = Path.Combine(
                SpecialFolder,
                ".minecraft",
                "mods",
                $"artemismc-{plugin.Info.Version}.jar"
            );
            InstallActions = new List<PluginPrerequisiteAction>()
            {
                new CreateFolderAction(
                    "Create mods folder",
                    Path.Combine(
                        SpecialFolder, ".minecraft", "mods")
                ),
                new DownloadFileAction(
                    "Download ArtemisMC mod",
                    $"https://github.com/urfour/ArtemisMC/releases/latest/download/artemismc-{plugin.Info.Version}.jar",
                    ModFilename
               )
            };

            UninstallActions = new List<PluginPrerequisiteAction>()
            {
                new DeleteFileAction("Delete mod", ModFilename)
            };

        }
    }
}
