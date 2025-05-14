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
INFRASTRUCTURE_PROJECT="$(find_project_path "$INFRASTRUCTURE_PROJECT_NAME" "$ROOT_DIR")"



if [ ! -f "$INFRASTRUCTURE_PROJECT" ]; then
  echo "❌ Не найден проект: $INFRASTRUCTURE_PROJECT"
  exit 1
fi

echo "ℹ️ Удаление последней миграции..."
dotnet ef migrations remove --project "$INFRASTRUCTURE_PROJECT"

if [ $? -eq 0 ]; then
  echo "✅ Последняя миграция успешно удалена"
else
  echo "❌ Ошибка при удалении миграции"
  exit 1
fi
