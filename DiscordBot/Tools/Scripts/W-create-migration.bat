@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

SET MIGRATION_NAME=%1
SET SCRIPT_DIR=%~dp0
SET INFRA_PROJECT=%SCRIPT_DIR%..\..\..\DiscordBot.Infrastructure\DiscordBot.Infrastructure.csproj
SET STARTUP_PROJECT=%SCRIPT_DIR%..\..\DiscordBot.Host.csproj

IF "%MIGRATION_NAME%"=="" (
    echo ❌ Не указано имя миграции.
    exit /b 1
)

IF NOT EXIST "%STARTUP_PROJECT%" (
    echo ❌ Не найден проект: %STARTUP_PROJECT%
    exit /b 1
)

IF NOT EXIST "%INFRA_PROJECT%" (
    echo ❌ Не найден проект: %INFRA_PROJECT%
    exit /b 1
)

echo ℹ️ Создание миграции: %MIGRATION_NAME%
dotnet ef migrations add %MIGRATION_NAME% ^
  --project "%INFRA_PROJECT%" ^
  --startup-project "%STARTUP_PROJECT%"

IF %ERRORLEVEL% EQU 0 (
    echo ✅ Миграция успешно создана: %MIGRATION_NAME%
) ELSE (
    echo ❌ Ошибка при создании миграции.
    exit /b 1
)
