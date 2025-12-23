using Artemis.Plugins.Games.Minecraft.ViewModels;
using Avalonia.Markup.Xaml;
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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
