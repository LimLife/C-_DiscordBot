@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

SET SCRIPT_DIR=%~dp0
SET INFRA_PROJECT=%SCRIPT_DIR%..\..\..\DiscordBot.Infrastructure\DiscordBot.Infrastructure.csproj
SET STARTUP_PROJECT=%SCRIPT_DIR%..\..\DiscordBot.Host.csproj

IF NOT EXIST "%STARTUP_PROJECT%" (
    echo ❌ Не найден проект: %STARTUP_PROJECT%
    exit /b 1
)

IF NOT EXIST "%INFRA_PROJECT%" (
    echo ❌ Не найден проект: %INFRA_PROJECT%
    exit /b 1
)

echo ℹ️ Проверка состояния миграций...
dotnet ef migrations list ^
  --project "%INFRA_PROJECT%" ^
  --startup-project "%STARTUP_PROJECT%"

IF %ERRORLEVEL% EQU 0 (
    echo ✅ Миграции успешно проверены
) ELSE (
    echo ❌ Ошибка при проверке миграций
    exit /b 1
)
