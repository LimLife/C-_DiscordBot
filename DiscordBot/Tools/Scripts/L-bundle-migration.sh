#!/bin/bash
set -euo pipefail

BUNDLE_NAME="migration-bundle.exe"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"


if [ ! -f "$SCRIPT_DIR/L-utils.sh" ]; then
  echo "❌ Файл утилит не найден: $SCRIPT_DIR/L-utils.sh"
  exit 1
fi

source "$SCRIPT_DIR/L-utils.sh"

read_config


ROOT_DIR="$(get_root_dir)"
BUNDLE_PATH="$SCRIPT_DIR/../MigrationsBundles/$BUNDLE_NAME"
INFRASTRUCTURE_PROJECT="$(find_project_path "$INFRASTRUCTURE_PROJECT_NAME" "$ROOT_DIR")"
STARTUP_PROJECT="$(find_project_path "$STARTUP_PROJECT_NAME" "$ROOT_DIR")"

if [ ! -f "$STARTUP_PROJECT" ]; then
  echo "❌ Не найден проект: $STARTUP_PROJECT"
  exit 1
fi

if [ ! -f "$INFRASTRUCTURE_PROJECT" ]; then
  echo "❌ Не найден проект: $INFRASTRUCTURE_PROJECT"
  exit 1
fi

mkdir -p "$(dirname "$BUNDLE_PATH")"

if [ -f "$BUNDLE_PATH" ]; then
  echo "⚠️ Bundle уже существует: $BUNDLE_PATH"
  read -p "❓ Перезаписать его? (y/n): " confirm
  if [ "$confirm" != "y" ]; then
    exit 0
  fi
fi

dotnet ef migrations bundle \
  --self-contained \
  --output "$BUNDLE_PATH" \
  --project "$INFRASTRUCTURE_PROJECT" \
  --startup-project "$STARTUP_PROJECT"

echo "✅ Bundle успешно создан: $BUNDLE_PATH"