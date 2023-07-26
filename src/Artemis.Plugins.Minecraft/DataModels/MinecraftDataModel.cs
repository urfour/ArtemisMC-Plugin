using Artemis.Core.Modules;
using Artemis.Plugins.Minecraft.DataModels;
using System.Collections.Generic;

namespace Artemis.Plugins.Modules.Minecraft.DataModels;

public class MinecraftDataModel : DataModel
{
    public MinecraftDataModel()
    {
        LastUpdate = "";
        Infos = new GameInfos();
    }
    public GameInfos Infos { get; set; }
    public string LastUpdate { get; set; }
}