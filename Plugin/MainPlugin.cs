using System;
using System.IO;
using VRage.Plugins;
using Sandbox.ModAPI;
using mamba.PulsarTemplate.Plugin.Models;
using mamba.PulsarTemplate.Plugin.Utils;
using mamba.PulsarTemplate.Plugin.Logic;

namespace mamba.PulsarTemplate.Plugin
{
    public class MainPlugin : IPlugin
    {
        private Config _config;
        private bool _sessionInitialized = false;
        private string _storagePath;

        public void Init(object gameInstance)
        {
            _storagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                        "SpaceEngineers", "Storage", "mamba.PulsarTemplate");

            ReloadConfig(); // Initial load
            LoggerUtil.LogInfo("Plugin core initialized.");
        }

        /// <summary>
        /// Reloads the config from disk and re-initializes dependent utilities.
        /// </summary>
        private void ReloadConfig()
        {
            _config = Config.LoadOrCreate(_storagePath);
            LoggerUtil.Init(_config);

            // Re-bind command handler if it was already initialized
            if (_sessionInitialized)
            {
                CommandHandler.Dispose();
                CommandHandler.Init(_config, ReloadConfig);
            }

            LoggerUtil.LogDebug("Configuration reloaded and applied.");
        }

        public void Update()
        {
            if (MyAPIGateway.Session == null || MyAPIGateway.Input == null) return;

            if (!_sessionInitialized)
            {
                HandleSessionInit();
                _sessionInitialized = true;
            }
        }

        private void HandleSessionInit()
        {
            CommandHandler.Init(_config, ReloadConfig);
            MyAPIGateway.Utilities.ShowMessage(_config.ProjectName, "Template started");
            LoggerUtil.LogSuccess("Session Initialization complete.");
        }

        public void Dispose()
        {
            if (_config != null) _config.Save(_storagePath);
            CommandHandler.Dispose();
        }
    }
}