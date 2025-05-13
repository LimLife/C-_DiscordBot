#!/bin/bash

set -e

BUNDLE_NAME="migration-bundle.exe"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
BUNDLE_PATH="$SCRIPT_DIR/../MigrationsBundles/$BUNDLE_NAME"
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

mkdir -p "$(dirname "$BUNDLE_PATH")"

if [ -f "$BUNDLE_PATH" ]; then
  echo "⚠️ Bundle уже существует: $BUNDLE_PATH"
  read -p "❓ Перезаписать его? (y/n): " confirm
  if [ "$confirm" != "y" ]; then
    echo "🚫 Отмена."
    exit 0
  fi
fi

dotnet ef migrations bundle \
  --self-contained \
  --output "$BUNDLE_PATH" \
  --project "$INFRA_PROJECT" \
  --startup-project "$STARTUP_PROJECT"

echo "✅ Bundle успешно создан: $BUNDLE_PATH"
