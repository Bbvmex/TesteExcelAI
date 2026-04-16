#Requires -RunAsAdministrator
<#
.SYNOPSIS
    Registers the VbeAddin COM add-in for the Excel VBA Editor (dev mode).
    Run this after every build. Close Excel before running.
#>

$ErrorActionPreference = "Stop"

$regasm = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe"
$dll    = Join-Path $PSScriptRoot "..\src\VbeAddin\bin\Debug\net48\VbeAddin.dll"
$dll    = [System.IO.Path]::GetFullPath($dll)

if (-not (Test-Path $dll)) {
    Write-Error "DLL not found at: $dll`nBuild the solution first (x86, Debug)."
}

Write-Host "Registering COM classes from: $dll"
& $regasm $dll /codebase /tlb
if ($LASTEXITCODE -ne 0) { throw "RegAsm failed with exit code $LASTEXITCODE" }

# Register the add-in in the VBE add-in list (HKCU, no admin needed for this part)
$progId = "VbeAddin.Connect"
foreach ($suffix in @("Addins", "Addins64")) {
    $key = "HKCU:\SOFTWARE\Microsoft\VBA\VBE\6.0\$suffix\$progId"
    New-Item -Path $key -Force | Out-Null
    Set-ItemProperty -Path $key -Name "FriendlyName"    -Value "AI Assistant"
    Set-ItemProperty -Path $key -Name "Description"     -Value "Local LLM AI assistant for VBA development"
    Set-ItemProperty -Path $key -Name "LoadBehavior"    -Value 3 -Type DWord
    Set-ItemProperty -Path $key -Name "CommandLineSafe" -Value 0 -Type DWord
    Write-Host "Registered: $key"
}

Write-Host ""
Write-Host "Done. Open Excel -> Alt+F11 to launch the VBA Editor."
Write-Host "The 'AI Assistant' panel should appear docked next to Project Explorer."
