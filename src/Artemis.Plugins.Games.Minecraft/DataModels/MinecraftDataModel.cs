using Artemis.Core.Modules;
using Artemis.Plugins.Games.Minecraft.DataModels;
using System.Collections.Generic;

namespace Artemis.Plugins.Modules.Minecraft.DataModels;

public class MinecraftDataModel : DataModel
{
    public MinecraftDataModel()
    {
        Infos = new GameInfos();
    }
    public GameInfos Infos { get; set; }
}