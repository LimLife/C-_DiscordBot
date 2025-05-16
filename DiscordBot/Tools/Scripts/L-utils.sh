#!/bin/bash

set -euo pipefail


SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
CONFIG_FILE="${SCRIPT_DIR}/CONFIG_FILE"
MIGRATION_FOLDER_NAME="Migrations"
read_config() {
  if [ ! -f "$CONFIG_FILE" ]; then
    echo "❌ Конфигурационный файл не найден: $CONFIG_FILE"
    exit 1
  fi

  source "$CONFIG_FILE"
  if [ -z "$INFRASTRUCTURE_PROJECT_NAME" ] || [ -z "$STARTUP_PROJECT_NAME" ]; then
    echo "❌ Не указаны имена проектов в конфигурации."
    exit 1
  fi
}

check_duplicate_migration_name() {
  local migration_base_name="$1"     
  local infrastructure_dir="$2"

  local migrations_dir="${infrastructure_dir}/$MIGRATION_FOLDER_NAME"

  if [ ! -d "$migrations_dir" ]; then
    echo "⚠️ Папка миграций не найдена, будет создана при миграции: $migrations_dir"
    return 1
  fi

  local existing
  existing=$(find "$migrations_dir" -type f -name "*_${migration_base_name}.cs")

  if [[ -n "$existing" ]]; then
    echo "❌ Миграция с именем '${migration_base_name}' уже существует:"
    echo "$existing"
    return 0
  fi

  return 1
}
check_exist_migration_name() {
  local migration_name="$1"     
  local infrastructure_dir="$2"

  local migrations_dir="${infrastructure_dir}/$MIGRATION_FOLDER_NAME"

  if [ ! -d "$migrations_dir" ]; then
    echo "⚠️ Папка миграций не найдена: $migrations_dir"
    return 1
  fi

  local existing
  existing=$(find "$migrations_dir" -type f -name "${migration_name}.cs")

  if [[ -n "$existing" ]]; then
    return 0 
  else
    echo "⚠️ Файл миграции не найден: $migration_name"
    return 1 
  fi
}

get_root_dir() {
  git rev-parse --show-toplevel 2>/dev/null || pwd
}

find_project_path() {
  local project_name="$1"
  local root_dir="$2"
  find "$root_dir" -type f -name "$project_name" | head -n 1
}