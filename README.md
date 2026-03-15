# mamba.PulsarTemplate

> **A production-ready starter template for Pulsar Space Engineers plugins.**  
> Includes a portable ConfigUI system you can drop into any existing plugin project.

```
C#: .NET Framework 4.8 (LangVersion latest)
Space Engineers: 1.204+
Platform: Pulsar Plugin Loader
Author: mamba
Version: 0.0.1
```

---

## 🚀 What's included

| Module | Description |
| :--- | :--- |
| `MainPlugin` | Minimal IPlugin entry point — delegates all logic to PluginCore |
| `PluginCore` | Central Init / Update / Dispose coordinator |
| `Config` | Public fields auto-surfaced in GUI via AutoConfigUI |
| `AutoConfigUI` | Reflection-based GUI generator (portable, zero config) |
| `PluginConfigBridge` | Connects Config to the SE plugin settings screen |
| `build.py` | Full build, version, deploy, and namespace automation |

---

## 📦 Using this as a new plugin

1. Clone / copy the repository.
2. Rename the `.csproj` file to `YourAuthor.YourPlugin.csproj`.
3. Run the namespace fixer:
   ```
   python build.py --fix-namespace YourPlugin
   ```
   This auto-detects the current namespace and replaces it everywhere,
   including the `.csproj` `<RootNamespace>`.
4. Edit `build.py` top section (`PROJECT_ID`, `PROJECT_AUTHOR`, etc.).
5. Build and deploy:
   ```
   python build.py
   ```

---

## 🔌 Installing ConfigUI into an existing plugin

Copy these two files into your project's `Plugin/ConfigUI/` folder:

```
Plugin/ConfigUI/AutoConfigUI.cs
Plugin/ConfigUI/PluginConfigBridge.cs
```

**Step 1 — Fix the namespace**  
Open both files and change `PulsarTemplate.ConfigUI` → `YourPlugin.ConfigUI`.

**Step 2 — Use in MainPlugin**

```csharp
// In your MainPlugin or wherever you want to expose settings:
using YourPlugin.ConfigUI;

// Open settings screen manually, e.g. on a hotkey:
MyGuiSandbox.AddScreen(new AutoConfigUI(_config));

// Or wire up the bridge for the SE plugin settings panel:
var bridge = new PluginConfigBridge(_config, "My Plugin", "Configure behaviour.");
bridge.Draw(); // called when SE opens your plugin settings
```

**Step 3 — Config requirements**  
Your `Config` class must have **public fields** (not properties) for each setting:

```csharp
public class Config
{
    public bool EnableFeature = true;   // -> checkbox
    public int  MaxRange      = 500;    // -> numeric textbox
    public string Prefix      = "/cmd"; // -> textbox
    public MyEnum Mode        = MyEnum.Auto; // -> dropdown
}
```

No registration, no attributes — reflection handles the rest.

---

## 🔧 Build script quick reference

```
python build.py                              # bump version, build, deploy
python build.py --fix-namespace MyPlugin     # rename namespace in all .cs files
python build.py --help                       # show full switch table
```

Logs are written to `doc\logs\` automatically.

---

## 🤝 Contributing

- Target `.NET Framework 4.8`
- All code and comments in **English**
- Maintain compatibility with Pulsar Plugin Loader

---

## ☕ Support

[Buy Me a Coffee ☕](https://buymeacoffee.com/mamba73)  
*Developed by [mamba73](https://github.com/mamba73).*
