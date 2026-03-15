// Config/PluginConfigBridge.cs
using System;
using System.Reflection;

namespace PulsarTemplate.Config
{
    /// <summary>
    /// PluginConfigBridge serves as a bridge between the PluginConfig class and the IMyPluginConfig interface expected by the Pulsar API.
    /// It automatically maps all public fields and properties from PluginConfig to itself, allowing for easy access and validation. 
    /// Automatically checks for null values and basic type validation.
    /// Every public field and property from _config is mapped here.
    /// </summary>
    public class PluginConfigBridge : IMyPluginConfig
    {
        private readonly PluginConfig _config;

        public PluginConfigBridge(PluginConfig config)
        {
            _config = config;
            AutoMapConfig();
        }

        /// <summary>
        /// Automatic mapping of all public fields and properties from _config to this bridge class.
        /// </summary>
        private void AutoMapConfig()
        {
            Type configType = _config.GetType();
            Type bridgeType = this.GetType();

            // Map fields
            foreach (var field in configType.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                var bridgeField = bridgeType.GetField(field.Name, BindingFlags.Public | BindingFlags.Instance);
                if (bridgeField != null && bridgeField.FieldType == field.FieldType)
                {
                    bridgeField.SetValue(this, field.GetValue(_config));
                }
            }

            // Map property
            foreach (var prop in configType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var bridgeProp = bridgeType.GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance);
                if (bridgeProp != null && bridgeProp.PropertyType == prop.PropertyType && bridgeProp.CanWrite)
                {
                    bridgeProp.SetValue(this, prop.GetValue(_config));
                }
            }
        }

        /// <summary>
        /// Validation of the configuration - checks for null values and basic types
        /// </summary>
        public bool Validate(out string errors)
        {
            errors = "";
            Type configType = _config.GetType();

            foreach (var prop in configType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = prop.GetValue(_config);
                if (value == null)
                {
                    errors += $"Property '{prop.Name}' ne smije biti null.\n";
                }
            }

            foreach (var field in configType.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = field.GetValue(_config);
                if (value == null)
                {
                    errors += $"Field '{field.Name}' ne smije biti null.\n";
                }
            }

            return string.IsNullOrEmpty(errors);
        }
    }
}