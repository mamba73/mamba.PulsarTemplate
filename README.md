
# mamba.PulsarTemplate

---
A high-level modular template for Pulsar plugins, featuring a robust configuration system, session-based logging, and dynamic command handling.

```
**C#**: 4.7.2+ / .NET Framework 4.8  
**Space Engineers**: 1.204+  
**Platform**: Pulsar Plugin Loader  
**Author**: mamba  
**Version**: 0.0.2
```

---
## 🚀 Project Status: 

| Feature | Status | Notes |
| :--- | :---: | :--- |
| **Modular Core** | ✅ Done | Clean separation of Logic, Models, and Utils. |
| **Config Merge** | ✅ Done | Automatically updates Config.xml without data loss. |
| **Session Logs** | ✅ Done | Timestamped daily/session logs in archive folder. |
| **Hot-Reload** | ✅ Done | Reload configuration via chat command in real-time. |

---
## 🌟 Key Features
* **Configurable Command Prefix**: Change the command trigger (default `/pulsar`) via XML.
* **Auto-Merging Config**: Developers can add new fields; the plugin will append them to existing user files on start.
* **Triple-Output Logging**: Logs to custom files, Space Engineers main log, and the console.
* **Lifecycle Ready**: Pre-built Init, Update, and Dispose patterns for stable performance.

---
## ⌨️ Controls & Configuration

All settings are managed via `Storage/mamba.PulsarTemplate/Config.xml`.

| Input / Command | Action |
| :--- | :--- |
| `{Prefix} reload` | **Reload Config** - Applies XML changes instantly in-game. |
| `{Prefix} info` | **Version Check** - Displays current plugin build info. |
| `Numeric 0` | **Action Key** - Default configurable trigger for custom logic. |

> [!TIP]
> Use the **reload** command after manually editing the Config.xml to see changes without restarting Space Engineers.

---
## 🤝 Contributing
Maintain compatibility with .NET Framework 4.8.  
All code and comments must be in **English**.  

---
## ☕ Support
If you like this project and want to support development:  
[Buy Me a Coffee ☕](https://buymeacoffee.com/mamba73)

*Developed by [mamba73](https://github.com/mamba73).*
