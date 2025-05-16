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


if ! OUTPUT=$(dotnet ef migrations remove --project "$INFRASTRUCTURE_PROJECT" 2>&1); then
  echo -e "\e[31m❌ Ошибка при удалении миграции:\e[0m"
  echo "$OUTPUT"
  exit 1
else
  echo -e "\e[32m✅ Последняя миграция успешно удалена:\e[0m"
  echo "$OUTPUT"
fi
