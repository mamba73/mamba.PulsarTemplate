// Plugin/Core/PluginCore.cs
using PulsarTemplate.Config;

namespace PulsarTemplate.Core
{
    /*
     * PluginCore
     * ----------
     * Central logic coordinator for the plugin.
     * Keeps MainPlugin clean and minimal.
     *
     * Add your Init / Update / Dispose logic here.
     */
    public static class PluginCore
    {
        private static PluginConfig _config;

        public static void Init(PluginConfig config)
        {
            _config = config;
            // TODO: Initialize subsystems here
        }

        public static void Update()
        {
            // TODO: Per-frame logic here
        }

        public static void Dispose()
        {
            // TODO: Cleanup here
        }
    }
}
