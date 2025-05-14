#!/bin/bash

set -euo pipefail

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

echo "ℹ️ Применение миграций..."
dotnet ef database update --project "$INFRASTRUCTURE_PROJECT" --startup-project "$STARTUP_PROJECT"

if [ $? -eq 0 ]; then
  echo "✅ Миграции успешно применены"
else
  echo "❌ Ошибка при применении миграций"
  exit 1
fi
