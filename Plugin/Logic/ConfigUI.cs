using System;
using System.Text;
using Sandbox.Graphics.GUI;
using Sandbox.Game.Gui;
using VRage.Utils;
using VRageMath;
using mamba.PulsarTemplate.Plugin.Models;

namespace mamba.PulsarTemplate.Plugin.Logic
{
    public class ConfigUI : MyGuiScreenDebugBase
    {
        private Config _config;
        private string _storagePath;

        public ConfigUI(Config config, string storagePath) : base(
            new Vector2(0.5f, 0.5f),      // Position
            new Vector2(0.35f, 0.4f),     // Size
            MyGuiConstants.SCREEN_BACKGROUND_COLOR,
            true)
        {
            _config = config;
            _storagePath = storagePath;
            RecreateControls(true);
        }

        public override string GetFriendlyName() => "PulsarTemplateConfigUI";

        public override void RecreateControls(bool constructor)
        {
            base.RecreateControls(constructor);

            // Set menu title
            AddCaption(_config.ProjectName + " Settings", Color.White.ToVector4());

            // Move position down - SE uses m_currentPosition
            m_currentPosition.Y += 0.02f;

            // --- Section: General Settings ---
            // Fix: checkbox callback uses MyGuiControlCheckbox instead of bool
            AddCheckBox("Enable Debug Mode",
                _config.Debug,
                (MyGuiControlCheckbox cb) =>
                {
                    _config.Debug = cb.IsChecked;
                    _config.Save(_storagePath);
                }
            );

            // --- Section: Example ---
            AddCheckBox("Enable Logging",
                true,
                (MyGuiControlCheckbox cb) =>
                {
                    // Custom logic here
                }
            );

            m_currentPosition.Y += 0.02f;

            // Fix: AddSubcaption takes string, not StringBuilder, and uses MyStringId for fonts
            AddSubcaption($"Version: {_config.PluginVersion}", Color.Gray.ToVector4());
        }
    }
}