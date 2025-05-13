#!/bin/bash

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
INFRA_PROJECT="$SCRIPT_DIR/../../../DiscordBot.Infrastructure/DiscordBot.Infrastructure.csproj"
STARTUP_PROJECT="$SCRIPT_DIR/../../DiscordBot.Host.csproj"

if [ ! -f "$STARTUP_PROJECT" ]; then
  echo "❌ Не найден проект: $STARTUP_PROJECT"
  exit 1
fi
if [ ! -f "$INFRA_PROJECT" ]; then
  echo "❌ Не найден проект: $INFRA_PROJECT"
  exit 1
fi

echo "ℹ️ Применение миграций..."
dotnet ef database update --project "$INFRA_PROJECT" --startup-project "$STARTUP_PROJECT"

if [ $? -eq 0 ]; then
  echo "✅ Миграции успешно применены"
else
  echo "❌ Ошибка при применении миграций"
  exit 1
fi
