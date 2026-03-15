// Plugin/MainPlugin.cs
using VRage.Plugins;
using PulsarTemplate.Config;
using PulsarTemplate.Core;

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
