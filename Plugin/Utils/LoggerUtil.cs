using System;
using System.IO;
using mamba.PulsarTemplate.Plugin.Models;
using VRage.Utils;

namespace mamba.PulsarTemplate.Plugin.Utils
{
    public static class LoggerUtil
    {
        private static string _logFile = null;
        private static Config _config = null;
        private static readonly object _lock = new object();
        private static string _prefix;

        public static void Init(Config config)
        {
            _config = config;
            _prefix = $"[{config.ProjectName}]";

            try
            {
                string logDir = Path.Combine(config.GetStoragePath(), config.LogSubFolder, "archive");
                if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);

                string date = DateTime.Now.ToString("yyyy-MM-dd");
                string time = DateTime.Now.ToString("HHmmss");

                string fileName = $"{date}_{time}_{config.ProjectName}_v{config.PluginVersion}.log";
                _logFile = Path.Combine(logDir, fileName);

                LogInfo("Logger initialized.");
            }
            catch (Exception ex)
            {
                MyLog.Default.WriteLine($"{_prefix} Logger failure: {ex.Message}");
            }
        }

        public static void LogInfo(string message) => WriteLog("INFO   ", message);
        public static void LogError(string message) => WriteLog("ERROR  ", message);
        public static void LogSuccess(string message) => WriteLog("SUCCESS", message);
        public static void LogDebug(string message) { if (_config?.Debug == true) WriteLog("DEBUG  ", message); }

        private static void WriteLog(string level, string message)
        {
            if (_logFile == null) return;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string line = $"[{timestamp}] [{level}] {message}";

            MyLog.Default.WriteLine($"{_prefix} {line}");
            try
            {
                lock (_lock) File.AppendAllText(_logFile, line + Environment.NewLine);
            }
            catch { }
        }
    }
}