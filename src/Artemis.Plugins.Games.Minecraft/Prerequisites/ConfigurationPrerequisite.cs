using Artemis.Core;
using Artemis.Plugins.Games.Minecraft.DataModels;
using Artemis.Plugins.Games.Minecraft.ViewModels;
using Artemis.UI.Shared.Services;
using Avalonia.Threading;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.Minecraft.Prerequisites
{
    public class ConfigurationPrerequisite : PluginPrerequisite
    {
        public override string Name => "Configuration";
        public override string Description => "Select Minecraft Version and Path";
        public override List<PluginPrerequisiteAction> InstallActions { get; }
        public override List<PluginPrerequisiteAction> UninstallActions { get; }

        private readonly MinecraftPluginConfiguration _configuration;

        public ConfigurationPrerequisite(Plugin plugin, MinecraftPluginConfiguration configuration)
        {
            _configuration = configuration;
            InstallActions = new List<PluginPrerequisiteAction>
            {
                 new ConfigurePrerequisiteAction("Open Settings", plugin)
            };
            UninstallActions = new List<PluginPrerequisiteAction>(){ };
        }

        public override bool IsMet()
        {
             return !string.IsNullOrEmpty(_configuration.MinecraftPath.Value) && !string.IsNullOrEmpty(_configuration.TargetVersion.Value);
        }
    }

    public class ConfigurePrerequisiteAction : PluginPrerequisiteAction
    {
        private readonly Plugin _plugin;

        public ConfigurePrerequisiteAction(string name, Plugin plugin) : base(name)
        {
            _plugin = plugin;
            ShowProgressBar = false;
        }

        public override async Task Execute(CancellationToken cancellationToken)
        {
             await Dispatcher.UIThread.InvokeAsync(async () => {
                 var windowService = _plugin.Resolve<IWindowService>(); // Keep resolving to ensure dependencies inside VM work
                 
                 // Manually create the view and viewmodel
                 var vm = new MinecraftPathDialogViewModel(_plugin, windowService);
                 var view = new Artemis.Plugins.Games.Minecraft.Views.MinecraftPathDialogView();
                 view.ViewModel = vm;

                 // Create a hosting window
                 var window = new Avalonia.Controls.Window
                 {
                     Content = view,
                     Title = "Minecraft Configuration",
                     Width = 600, 
                     Height = 400,
                     WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner
                 };
                 
                 // Handle Close Request
                 vm.RequestClose += (_, _) => window.Close();

                 // Get Main Window
                 if (Avalonia.Application.Current?.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow != null)
                 {
                     await window.ShowDialog(desktop.MainWindow);
                 }
                 else
                 {
                     window.Show();
                 }
             });
        }
    }
}
