#Requires -RunAsAdministrator
<#
.SYNOPSIS
    Removes the VbeAddin COM registration. Run before re-registering a renamed/moved DLL.
    Close Excel before running.
#>

$ErrorActionPreference = "Stop"

$regasm = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe"
$dll    = Join-Path $PSScriptRoot "..\src\VbeAddin\bin\Debug\net48\VbeAddin.dll"
$dll    = [System.IO.Path]::GetFullPath($dll)

if (Test-Path $dll) {
    Write-Host "Unregistering COM classes from: $dll"
    & $regasm $dll /unregister
}

$progId = "VbeAddin.Connect"
foreach ($suffix in @("Addins", "Addins64")) {
    $key = "HKCU:\SOFTWARE\Microsoft\VBA\VBE\6.0\$suffix\$progId"
    if (Test-Path $key) {
        Remove-Item -Path $key -Force
        Write-Host "Removed: $key"
    }
}

Write-Host "Unregistration complete."
