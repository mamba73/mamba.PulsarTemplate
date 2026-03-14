using System;
using Sandbox.ModAPI;
using VRage.Game;
using mamba.PulsarTemplate.Plugin.Models;
using mamba.PulsarTemplate.Plugin.Utils;

namespace mamba.PulsarTemplate.Plugin.Logic
{
    public static class CommandHandler
    {
        private static Config _config;
        private static Action _onReloadRequested;

        public static void Init(Config config, Action onReloadRequested)
        {
            _config = config;
            _onReloadRequested = onReloadRequested;
            MyAPIGateway.Utilities.MessageEntered += OnMessageEntered;
            LoggerUtil.LogInfo($"Command Handler active with prefix: {_config.CommandPrefix}");
        }

        public static void Dispose()
        {
            if (MyAPIGateway.Utilities != null)
                MyAPIGateway.Utilities.MessageEntered -= OnMessageEntered;
        }

        private static void OnMessageEntered(string messageText, ref bool sendToOthers)
        {
            if (!messageText.StartsWith(_config.CommandPrefix, StringComparison.OrdinalIgnoreCase))
                return;

            sendToOthers = false;
            string[] parts = messageText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                ShowNotification($"Usage: {_config.CommandPrefix} [reload|info]", MyFontEnum.White);
                return;
            }

            string cmd = parts[1].ToLower();
            if (cmd == "reload")
            {
                _onReloadRequested?.Invoke();
                ShowNotification("Configuration reloaded.", MyFontEnum.Green);
            }
            else if (cmd == "info")
            {
                MyAPIGateway.Utilities.ShowMessage(_config.ProjectName, $"Version: {_config.PluginVersion}");
            }
        }

        public static void ShowNotification(string text, string font = MyFontEnum.Green, int duration = 3000)
        {
            MyAPIGateway.Utilities.ShowNotification(text, duration, font);
        }
    }
}