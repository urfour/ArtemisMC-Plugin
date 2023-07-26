using Artemis.Core;
using Artemis.Plugins.Games.Minecraft.Prerequisites;

namespace Artemis.Plugins.Games.Minecraft;

public class Bootstrapper : PluginBootstrapper
{
    public override void OnPluginLoaded(Plugin plugin)
    {
        AddPluginPrerequisite(new FabricPrerequisite(plugin));
        AddPluginPrerequisite(new ModPrerequisite(plugin));
    }

    public override void OnPluginEnabled(Plugin plugin)
    {
        
    }
    
    public override void OnPluginDisabled(Plugin plugin)
    {
        
    }
}
