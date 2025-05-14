#!/bin/bash

set -euo pipefail

MIGRATION_NAME=$1
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

if [ ! -f "$SCRIPT_DIR/L-utils.sh" ]; then
  echo "❌ Файл утилит не найден: $SCRIPT_DIR/L-utils.sh"
  exit 1
fi

source "$SCRIPT_DIR/L-utils.sh"

read_config


ROOT_DIR="$(get_root_dir)"

INFRASTRUCTURE_PROJECT="$(find_project_path "$INFRASTRUCTURE_PROJECT_NAME" "$ROOT_DIR")"
STARTUP_PROJECT="$(find_project_path "$STARTUP_PROJECT_NAME" "$ROOT_DIR")"

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
