# VbeAddin — Development Guide

## Prerequisites

- **Windows** (required — COM add-ins are Windows-only)
- **Visual Studio 2022** with ".NET desktop development" workload
- **Excel 365 or 2016+** installed (32-bit Office recommended; 64-bit also works)
- **PowerShell 5+**
- A local LLM server running (LM Studio or Ollama)

---

## Build

1. Open `TesteExcelAI.sln` in Visual Studio 2022
2. Set platform to **x86** (not AnyCPU)
3. **Build → Build Solution**
4. Output: `src/VbeAddin/bin/Debug/net48/VbeAddin.dll`

---

## Configure Local LLM

Create `%APPDATA%\VbeAddin\config.json`:

```json
{
  "BaseUrl": "http://localhost:1234/v1",
  "Model": "your-model-name"
}
```

| Server   | Default BaseUrl                  |
|----------|----------------------------------|
| LM Studio | `http://localhost:1234/v1`      |
| Ollama    | `http://localhost:11434/v1`     |

If the file doesn't exist it is created automatically with LM Studio defaults on first run.

---

## Register (dev cycle)

**Always close Excel before registering.**

```powershell
# Run as Administrator
cd tools
.\Register-Dev.ps1
```

This does two things:
1. Runs `RegAsm.exe /codebase` to register COM classes from the DLL
2. Writes `HKCU\SOFTWARE\Microsoft\VBA\VBE\6.0\Addins\VbeAddin.Connect` with `LoadBehavior=3`

To undo registration (e.g., before rebuilding to a different path):
```powershell
.\Unregister-Dev.ps1
```

---

## Test

1. Close Excel, run `Register-Dev.ps1`
2. Open Excel → `Alt+F11` → VBA Editor opens
3. The **AI Assistant** panel should appear docked next to the Project Explorer
4. Type a VBA question → click **Send** → answer appears below
5. If the panel doesn't appear: **Add-Ins menu → Add-In Manager** → verify "AI Assistant" is listed and checked

---

## Debugging

Attach the Visual Studio debugger to the Excel process:

1. Open Excel (do not attach before)
2. **Debug → Attach to Process → EXCEL.EXE**
3. Set breakpoints in `Connect.cs` `OnConnection`
4. Open VBA Editor (Alt+F11) — breakpoint fires

---

## Common Issues

| Symptom | Likely cause | Fix |
|---------|-------------|-----|
| Add-in not in Add-In Manager | HKCU key missing | Re-run `Register-Dev.ps1` |
| "Cannot create ActiveX component" | `AiAssistantControl` not COM-registered or bitness mismatch | Use 32-bit RegAsm path; ensure x86 build |
| Panel doesn't dock, floats instead | Project Explorer window not found | Harmless — just drag to dock manually |
| HTTP call never returns | Local LLM not running | Start LM Studio/Ollama before opening Excel |
| Deadlock on shutdown | `.Result` / `.Wait()` used somewhere | Ensure all awaits are `async`/`await` not blocking |
