using System;
using Artemis.Core;
using Artemis.Plugins.Games.Minecraft.DataModels;
using Artemis.UI.Shared;
using Artemis.UI.Shared.Services;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using System.IO;
using Avalonia.Platform.Storage;

namespace Artemis.Plugins.Games.Minecraft.ViewModels
{
    public class MinecraftPathDialogViewModel : PluginConfigurationViewModel
    {
        private readonly MinecraftPluginConfiguration _configuration;
        private readonly IWindowService _windowService;
        private string _minecraftPath;
        private string _selectedVersion;

        public MinecraftPathDialogViewModel(Plugin plugin, IWindowService windowService) : base(plugin)
        {
            _configuration = Bootstrapper.Configuration;
            _windowService = windowService;
            
            _minecraftPath = _configuration.MinecraftPath.Value;
            _selectedVersion = _configuration.TargetVersion.Value;
            
            SelectPath = ReactiveCommand.CreateFromTask(ExecuteSelectPath);
            Save = ReactiveCommand.Create(ExecuteSave);
            
            // Available versions
            AvailableVersions = new ObservableCollection<string>
            {
                "1.21.11",
                "1.20.1", 
                "1.19.2"
            };
            
            if (!AvailableVersions.Contains(_selectedVersion))
            {
                _selectedVersion = AvailableVersions[0];
            }
        }

        public string MinecraftPath
        {
            get => _minecraftPath;
            set => this.RaiseAndSetIfChanged(ref _minecraftPath, value);
        }

        public string SelectedVersion
        {
            get => _selectedVersion;
            set => this.RaiseAndSetIfChanged(ref _selectedVersion, value);
        }

        public ObservableCollection<string> AvailableVersions { get; }

        public ReactiveCommand<Unit, Unit> SelectPath { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public event EventHandler RequestClose;

        private async Task ExecuteSelectPath()
        {
            var folders = await _windowService.CreateOpenFolderDialog()
                .WithTitle("Select Minecraft folder")
                .ShowAsync();
                
            if (folders != null)
            {
                MinecraftPath = folders.ToString();
            }
        }

        private void ExecuteSave()
        {
            _configuration.MinecraftPath.Value = MinecraftPath;
            _configuration.TargetVersion.Value = SelectedVersion;
            _configuration.MinecraftPath.Save();
            _configuration.TargetVersion.Save();
            RequestClose?.Invoke(this, EventArgs.Empty);
        
        }
    }
}
