#!/bin/bash

set -euo pipefail

if [[ $# -lt 1 ]]; then
  echo "❌ Использование: $0 <migration_name_in_snake_case>"
  exit 1
fi

MIGRATION_ID="$1"


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

if ! check_exist_migration_name "$MIGRATION_ID" "$INFRASTRUCTURE_DIR"; then
  exit 1
fi 

if ! OUTPUT=$(dotnet ef database update "$MIGRATION_ID" \
  --project "$INFRASTRUCTURE_PROJECT" \
  --startup-project "$STARTUP_PROJECT" 2>&1); then
  echo -e "\e[31m❌ Ошибка при применении миграции с ID $MIGRATION_ID:\e[0m"
  echo "$OUTPUT"
  exit 1
else
  echo -e "\e[32m✅ Миграция с ID $MIGRATION_ID успешно применена:\e[0m"
  echo "$OUTPUT"
fi
