#!/bin/bash

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
INFRA_PROJECT="$SCRIPT_DIR/../../../DiscordBot.Infrastructure/DiscordBot.Infrastructure.csproj"

if [ ! -f "$INFRA_PROJECT" ]; then
  echo "❌ Не найден проект: $INFRA_PROJECT"
  exit 1
fi

echo "ℹ️ Удаление последней миграции..."
dotnet ef migrations remove --project "$INFRA_PROJECT"

if [ $? -eq 0 ]; then
  echo "✅ Последняя миграция успешно удалена"
else
  echo "❌ Ошибка при удалении миграции"
  exit 1
fi
