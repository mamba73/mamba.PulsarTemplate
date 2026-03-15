// Plugin/MainPlugin.cs
using VRage.Plugins;
using PulsarTemplate.Config;
using PulsarTemplate.Core;

// Ovo je stub interfejs da kompajler ne kuka.
// Kad dodaš pravi Pulsar/PB DLL, ovo možeš ukloniti.
namespace PulsarTemplate
{
    public interface IMyPluginConfig { }
}

namespace PulsarTemplate
{
    /*
     * MainPlugin
     * ----------
     * Entry point for the Space Engineers client plugin.
     * Registered automatically by Pulsar via IPlugin.
     *
     * Keep this file minimal — delegate all logic to PluginCore.
     */
    public class MainPlugin : IPlugin
    {
        private static PluginConfig _config = new PluginConfig();

        public IMyPluginConfig GetPluginConfig()
        {
            return new PluginConfigBridge(_config);
        }

        public void Init(object gameInstance)
        {
            PluginCore.Init(_config);
        }

        public void Update()
        {
            PluginCore.Update();
        }

        public void Dispose()
        {
            PluginCore.Dispose();
        }
    }
}