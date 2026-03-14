using System;
using System.IO;
using System.Xml.Serialization;
using VRage.Input;
using mamba.PulsarTemplate.Plugin.Utils;

namespace mamba.PulsarTemplate.Plugin.Models
{
    public class Config
    {
        // ===================================================================
        // VERSIONING & METADATA
        // ===================================================================
        public string PluginVersion { get; set; } = "1.0.0";
        public string ProjectName { get; set; } = "PulsarTemplate";

        // ===================================================================
        // GLOBAL SETTINGS
        // ===================================================================
        public bool Debug { get; set; } = true;
        public bool LogChat { get; set; } = true;
        public string CommandPrefix { get; set; } = "/pulsar"; // Configurable prefix

        // ===================================================================
        // PATH SETTINGS
        // ===================================================================
        public string StorageFolder { get; set; } = "mamba.PulsarTemplate";
        public string LogSubFolder { get; set; } = "log";

        // ===================================================================
        // INPUT SETTINGS
        // ===================================================================
        public MyKeys MenuKey { get; set; } = MyKeys.NumPad0;

        // ===================================================================
        // CORE LOGIC: LOADING & SAVING
        // ===================================================================

        /// <summary>
        /// Loads the configuration from disk. If the file exists, it merges stored 
        /// values with current defaults to ensure new fields are included.
        /// </summary>
        public static Config LoadOrCreate(string folderPath)
        {
            string filePath = Path.Combine(folderPath, "Config.xml");
            Config defaultConfig = new Config();

            try
            {
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                if (!File.Exists(filePath))
                {
                    defaultConfig.Save(folderPath);
                    return defaultConfig;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    Config loadedConfig = (Config)serializer.Deserialize(reader);

                    // The "Merge" step: loadedConfig has user values, 
                    // but we save it back immediately to write any new default fields 
                    // that were added during development.
                    loadedConfig.Save(folderPath);
                    return loadedConfig;
                }
            }
            catch (Exception ex)
            {
                // We don't have LoggerUtil initialized yet when this runs in Init, 
                // so we fallback to a simple string or SE Log.
                return defaultConfig;
            }
        }

        /// <summary>
        /// Serializes the current configuration object to an XML file.
        /// </summary>
        public void Save(string folderPath)
        {
            try
            {
                string filePath = Path.Combine(folderPath, "Config.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, this);
                }
            }
            catch (Exception)
            {
                // Silent fail to prevent game crashes
            }
        }

        public string GetStoragePath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SpaceEngineers", "Storage", StorageFolder
            );
        }
    }
}