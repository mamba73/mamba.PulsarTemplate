// Plugin/Config/Config.cs
namespace PulsarTemplate.Config
{
    /*
     * PluginConfig
     * ------------
     * Plugin configuration container.
     * Class is named PluginConfig (not Config) to avoid collision
     * with the PulsarTemplate.Config namespace itself.
     *
     * All public fields are automatically surfaced in the GUI
     * by AutoConfigUI. Supported field types:
     *   bool   -> checkbox
     *   int    -> numeric field
     *   string -> textbox
     *   enum   -> dropdown
     *
     * Add new fields freely — no other registration required.
     */
    public class PluginConfig
    {
        public string PluginVersion { get; set; } = "0.0.38";

        public bool Debug = true;

        public int ScanRange = 500;

        public string CommandPrefix = "/pulsar";

        public LogLevel LogLevel = LogLevel.Info;
    }

    public enum LogLevel
    {
        Off,
        Error,
        Info,
        Debug
    }
}
