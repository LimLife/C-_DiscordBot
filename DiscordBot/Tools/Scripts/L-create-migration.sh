#!/bin/bash

set -euo pipefail

if [[ $# -lt 1 ]]; then
  echo "❌ Использование: $0 <migration_name_in_snake_case>"
  exit 1
fi

RAW_NAME="$1"
MIGRATION_NAME=$(echo "$RAW_NAME" | tr '[:upper:]' '[:lower:]' | tr ' ' '_' | tr '-' '_')


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


INFRASTRUCTURE_DIR="$(dirname "$INFRASTRUCTURE_PROJECT")"

if check_duplicate_migration_name "$MIGRATION_NAME" "$INFRASTRUCTURE_DIR"; then
  echo "🚫 Миграция не будет создана есть дубликат $MIGRATION_NAME ."
  exit 1
fi

echo "ℹ️ Создание миграции: $MIGRATION_NAME"
dotnet ef migrations add "$MIGRATION_NAME" \
  --project "$INFRASTRUCTURE_PROJECT" \
  --startup-project "$STARTUP_PROJECT"

if [ $? -eq 0 ]; then
  echo "✅ Миграция успешно создана: $MIGRATION_NAME"
else
  echo "❌ Ошибка при создании миграции."
  exit 1
fi
