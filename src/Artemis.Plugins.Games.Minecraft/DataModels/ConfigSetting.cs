namespace Artemis.Plugins.Games.Minecraft.DataModels
{
    public class ConfigSetting<T>
    {
        public T Value { get; set; }

        public ConfigSetting(T defaultValue)
        {
            Value = defaultValue;
        }

        public void Save()
        {
            // Placeholder: Implement actual saving logic or revert to PluginSetting when API is known
        }
    }
}
