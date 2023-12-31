﻿using System.Collections.Generic;
using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.Minecraft.DataModels;
using Artemis.Plugins.Modules.Minecraft.DataModels;

namespace Artemis.Plugins.Games.Modules.Minecraft;

[PluginFeature(Name = "Minecraft")]
public class MinecraftModule : Module<MinecraftDataModel>
{
    private readonly IWebServerService _webServerService;
    public MinecraftModule(IWebServerService webServerService)
    {
        _webServerService = webServerService;
    }
    public override List<IModuleActivationRequirement> ActivationRequirements { get; }
        = new() { new ProcessActivationRequirement("javaw"), new ProcessActivationRequirement("java") };

    public override void Enable()
    {
        _webServerService.AddResponsiveJsonEndPoint<GameInfos>(this, "Minecraft", rep =>
        {
            DataModel.Infos.Player = rep.Player;
            DataModel.Infos.World = rep.World;
            DataModel.Infos.Gui = rep.Gui;
            return DataModel.Infos;
        });
    }

    public override void Disable()
    {
        
    }

    public override void Update(double deltaTime)
    {
        DataModel.LastUpdate = $"{deltaTime} seconds";
    }

}