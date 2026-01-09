using Artemis.Plugins.Games.Minecraft.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using ReactiveUI.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.Minecraft.Views
{
    public partial class MinecraftPathDialogView : ReactiveUserControl<MinecraftPathDialogViewModel>
    {
        public MinecraftPathDialogView()
        {
            InitializeComponent();
            this.WhenActivated(disposables =>
            {
                if (ViewModel != null)
                {
                    ViewModel.RequestClose += (_, _) =>
                    {
                         var window = this.VisualRoot as Avalonia.Controls.Window;
                         window?.Close();
                    };
                }
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
