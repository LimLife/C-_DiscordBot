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

echo ℹ️ Сбрасываем базу данных...
dotnet ef database drop --force ^
  --project "%INFRA_PROJECT%" ^
  --startup-project "%STARTUP_PROJECT%"

echo ℹ️ Применение миграций...
dotnet ef database update ^
  --project "%INFRA_PROJECT%" ^
  --startup-project "%STARTUP_PROJECT%"

IF %ERRORLEVEL% EQU 0 (
    echo ✅ База данных успешно сброшена и миграции применены
) ELSE (
    echo ❌ Ошибка при сбросе базы данных или применении миграций
    exit /b 1
)
