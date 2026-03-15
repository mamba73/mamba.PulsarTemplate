# build.py — Mamba Build Tool

> Automates version management, MSBuild compilation, and deployment  
> for Pulsar Space Engineers plugins.

---

## Quick start

```
python build.py           # Standard build & deploy
python build.py --help    # Show all switches
```

---

## Switch reference

| Switch | Description |
| :--- | :--- |
| *(no switch)* | Increment patch version → regenerate XML manifest → MSBuild Release/x64 → deploy to Pulsar AppData |
| `--fix-namespace <Name>` | Auto-detect current namespace and replace with `<Name>` in all `.cs` files and `.csproj` |
| `--help` | Print this switch table |

---

## What happens on a standard build

1. **Version bump** — reads `version.txt`, increments patch segment (`1.0.5 → 1.0.6`), writes back.
2. **XML manifest** — deletes stale manifest, recreates from template with new version and git commit hash.
3. **Config.cs injection** — updates `PluginVersion` default value to match.
4. **MSBuild** — runs `Restore;Rebuild` in `Release / x64`.
5. **Deploy** — copies DLL, XML, source tree, and `Pulsar.Shared.dll` to:
   ```
   %APPDATA%\Pulsar\Legacy\Local\<ProjectName>\
   ```
6. **README update** — bumps `**Version**: x.x.x` line in `README.md`.

---

## `--fix-namespace` behaviour

The switch performs **auto-detection** before making any change:

1. Reads `<RootNamespace>` from the `.csproj`.
2. Scans all `.cs` files under `Plugin/` for declared namespace names.
3. If the target namespace is already in use → **no changes made**.
4. Otherwise replaces the old namespace string in every `.cs` file and updates `<RootNamespace>` in the `.csproj`.

Every file is logged individually:

```
[NS][FIXED] Plugin\MainPlugin.cs
[NS][SKIP]  Plugin\Config\Config.cs — no occurrences of 'PulsarTemplate'
[NS] Done — 3 file(s) updated.
```

---

## Log files

All build runs write to `doc\logs\` (created automatically):

| File | Content |
| :--- | :--- |
| `2026-03-15_120000_build.log` | Full timestamped run log |
| `2026-03-15_120000_build_ERRORS.log` | Compact error-only log (generated on build failure) |
| `2026-03-15_120000_build__fix-namespace.log` | Log for namespace fix runs |

The error log strips noise and keeps only lines containing `error`, `warning`, `failed`, or `fatal` — keeping it minimal for quick AI or human diagnosis.

---

## Configuration (top of build.py)

| Variable | Purpose |
| :--- | :--- |
| `PROJECT_ID` | GitHub plugin ID (`author/repo`) |
| `PROJECT_AUTHOR` | Author name injected into manifest |
| `PROJECT_DESCRIPTION` | Plugin description in manifest |
| `PROJECT_TOOLTIP` | Short tooltip in manifest |
| `MSBUILD_PATH` | Full path to `MSBuild.exe` — adjust for your VS edition |
| `LOG_DIR` | Log output directory (default: `doc\logs`) |

---

## Requirements

- Python 3.8+
- MSBuild (Visual Studio Build Tools or full VS)
- Git (optional — used for commit hash in manifest; falls back to `local-build`)

---

*Part of [mamba.PulsarTemplate](https://github.com/mamba73/mamba.PulsarTemplate)*
