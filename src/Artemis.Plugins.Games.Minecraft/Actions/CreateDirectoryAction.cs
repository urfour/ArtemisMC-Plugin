using Artemis.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.Minecraft.Actions
{
    public class CreateDirectoryAction : PluginPrerequisiteAction
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
        public CreateDirectoryAction(string name, string target)
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

}
