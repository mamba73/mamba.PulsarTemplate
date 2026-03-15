using System;
using System.IO;
using System.Xml.Serialization;
using VRage.Input;

namespace mamba.PulsarTemplate.Plugin.Models
{
    public class Config
    {
        public string PluginVersion { get; set; } = "0.0.3";
        public string ProjectName { get; set; } = "PulsarTemplate";
        public bool Debug { get; set; } = true;
        public string CommandPrefix { get; set; } = "/pulsar";
        public string StorageFolder { get; set; } = "mamba.PulsarTemplate";
        public string LogSubFolder { get; set; } = "log";
        public MyKeys MenuKey { get; set; } = MyKeys.NumPad0;

        public static Config LoadOrCreate(string folderPath)
        {
            string filePath = Path.Combine(folderPath, "Config.xml");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            if (!File.Exists(filePath))
            {
                Config newConfig = new Config();
                newConfig.Save(folderPath);
                return newConfig;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    Config loaded = (Config)serializer.Deserialize(reader);
                    loaded.Save(folderPath);
                    return loaded;
                }
            }
            catch
            {
                // Removed 'ex' to prevent CS0168 warning
                return new Config();
            }
        }

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
            catch { }
        }

        public string GetStoragePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                "SpaceEngineers", "Storage", StorageFolder);
        }
    }
}