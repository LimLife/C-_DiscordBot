#!bin/bash

set -euo pipefail


SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
CONFIG_FILE="${SCRIPT_DIR}/CONFIG_FILE"

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
get_root_dir() {
  git rev-parse --show-toplevel 2>/dev/null || pwd
}

find_project_path() {
  local project_name="$1"
  local root_dir="$2"
  find "$root_dir" -type f -name "$project_name" | head -n 1
}