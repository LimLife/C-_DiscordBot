@echo off
setlocal enabledelayedexpansion

set "BUNDLE_NAME=migration-bundle.exe"
set "SCRIPT_DIR=%~dp0"
set "BUNDLE_PATH=%SCRIPT_DIR%..\MigrationsBundles\%BUNDLE_NAME%"
set "INFRA_PROJECT=%SCRIPT_DIR%..\..\..\DiscordBot.Infrastructure\DiscordBot.Infrastructure.csproj"
set "STARTUP_PROJECT=%SCRIPT_DIR%..\..\DiscordBot.Host.csproj"

if not exist "%STARTUP_PROJECT%" (
    echo ❌ Не найден проект: %STARTUP_PROJECT%
    exit /b 1
)

if not exist "%INFRA_PROJECT%" (
    echo ❌ Не найден проект: %INFRA_PROJECT%
    exit /b 1
)

if not exist "%SCRIPT_DIR%..\MigrationsBundles" (
    mkdir "%SCRIPT_DIR%..\MigrationsBundles"
)

if exist "%BUNDLE_PATH%" (
    echo ⚠️ Bundle уже существует: %BUNDLE_PATH%
    set /p confirm="❓ Перезаписать его? (y/n): "
    if /i not "!confirm!"=="y" (
        echo 🚫 Отмена.
        exit /b 0
    )
)

dotnet ef migrations bundle ^
    --self-contained ^
    --output "%BUNDLE_PATH%" ^
    --project "%INFRA_PROJECT%" ^
    --startup-project "%STARTUP_PROJECT%"

echo ✅ Bundle успешно создан: %BUNDLE_PATH%
