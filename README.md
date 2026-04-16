# VBE AI Assistant

A GitHub Copilot-style AI assistant docked directly inside the **Excel VBA Editor (VBE)** — built for RPA programmers.

![Panel docked in VBE beside Project Explorer]

---

## What it does

- Opens a persistent panel inside the VBA Editor (not Excel's ribbon or task pane)
- Lets you ask questions about VBA / RPA code and get answers from a local LLM
- Runs entirely offline — no cloud API keys required

### Phase 1 (current)
- Dockable tool window in the VBE
- Question input, Send button, answer output
- Connects to any local OpenAI-compatible LLM server

### Planned
- Read the active VBA module and send it as context
- Detect and explain compile errors
- Edit code directly from the chat
- Snapshot / history of conversations

---

## Requirements

### Windows machine
- Windows 10 / 11
- Microsoft Excel 2016 or 365 (32-bit recommended)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) — *.NET desktop development* workload
- .NET Framework 4.8 (ships with Windows 10+)
- PowerShell 5+

### Local LLM server (pick one)
| Server | Default URL | Install |
|--------|-------------|---------|
| [LM Studio](https://lmstudio.ai/) | `http://localhost:1234/v1` | Download + load a model |
| [Ollama](https://ollama.com/) | `http://localhost:11434/v1` | `winget install Ollama.Ollama` |

---

## Setup

### 1. Clone and build

```bash
git clone https://github.com/Bbvmex/TesteExcelAI.git
cd TesteExcelAI
```

Open `TesteExcelAI.sln` in Visual Studio 2022, set the platform to **x86**, then **Build → Build Solution**.

### 2. Configure the LLM endpoint

Copy `config.example.json` to `%APPDATA%\VbeAddin\config.json` and edit:

```json
{
  "BaseUrl": "http://localhost:1234/v1",
  "Model": "your-model-name"
}
```

If the file is missing it is created automatically with LM Studio defaults on first launch.

### 3. Register the add-in

**Close Excel first**, then run in an Administrator PowerShell:

```powershell
.\tools\Register-Dev.ps1
```

This registers the COM classes and tells VBE to load the add-in at startup.

### 4. Use it

1. Start your local LLM server
2. Open Excel → `Alt+F11` to open the VBA Editor
3. The **AI Assistant** panel appears docked next to the Project Explorer
4. Type a question and click **Send**

To unregister (e.g. before a clean rebuild):

```powershell
.\tools\Unregister-Dev.ps1
```

---

## Project structure

```
TesteExcelAI/
├── src/VbeAddin/
│   ├── Connect.cs               # COM entry point (IDTExtensibility2)
│   ├── ToolWindowHost.cs        # Creates and docks the VBE tool window
│   ├── UI/AiAssistantControl.cs # WinForms panel (question / answer UI)
│   └── AI/
│       ├── LlmApiClient.cs      # HTTP client → local LLM
│       ├── LlmModels.cs         # OpenAI-format request/response types
│       └── AddinConfig.cs       # Reads config.json
├── tools/
│   ├── Register-Dev.ps1         # Register COM + VBE add-in entry
│   └── Unregister-Dev.ps1
└── docs/DEVELOPMENT.md          # Full dev guide + troubleshooting
```

---

## Troubleshooting

See [`docs/DEVELOPMENT.md`](docs/DEVELOPMENT.md) for a full troubleshooting table. Quick checks:

- **Panel missing** → VBE → Add-Ins → Add-In Manager → verify "AI Assistant" is checked
- **"Cannot create ActiveX component"** → re-run `Register-Dev.ps1` as Administrator with an x86 build
- **Send hangs** → confirm the LLM server is running before opening Excel
