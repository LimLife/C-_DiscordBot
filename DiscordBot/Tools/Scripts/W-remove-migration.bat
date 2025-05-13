@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

SET SCRIPT_DIR=%~dp0
SET INFRA_PROJECT=%SCRIPT_DIR%..\..\..\DiscordBot.Infrastructure\DiscordBot.Infrastructure.csproj

IF NOT EXIST "%INFRA_PROJECT%" (
    echo ❌ Не найден проект: %INFRA_PROJECT%
    exit /b 1
)

echo ℹ️ Удаление последней миграции...
dotnet ef migrations remove ^
  --project "%INFRA_PROJECT%"

IF %ERRORLEVEL% EQU 0 (
    echo ✅ Последняя миграция успешно удалена
) ELSE (
    echo ❌ Ошибка при удалении миграции
    exit /b 1
)
