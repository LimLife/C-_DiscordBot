#!/bin/bash

set -e

MIGRATION_NAME=$1
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
INFRA_PROJECT="$SCRIPT_DIR/../../../DiscordBot.Infrastructure/DiscordBot.Infrastructure.csproj"
STARTUP_PROJECT="$SCRIPT_DIR/../../DiscordBot.Host.csproj"

if [ -z "$MIGRATION_NAME" ]; then
  echo "❌ Не указано имя миграции."
  exit 1
fi

if [ ! -f "$STARTUP_PROJECT" ]; then
  echo "❌ Не найден проект: $STARTUP_PROJECT"
  exit 1
fi
if [ ! -f "$INFRA_PROJECT" ]; then
  echo "❌ Не найден проект: $INFRA_PROJECT"
  exit 1
fi

echo "ℹ️ Создание миграции: $MIGRATION_NAME"
dotnet ef migrations add "$MIGRATION_NAME" \
  --project "$INFRA_PROJECT" \
  --startup-project "$STARTUP_PROJECT"

if [ $? -eq 0 ]; then
  echo "✅ Миграция успешно создана: $MIGRATION_NAME"
else
  echo "❌ Ошибка при создании миграции."
  exit 1
fi
