#!/usr/bin/env bash
set -euo pipefail # makes bash fail directly whenever a command fails or when variable could not be expanded.

echo "Wait for the database to accept connections."
while true; do nc -z -v -w3 ${DATABASE_HOST} ${DATABASE_PORT} && echo "Database is accepting connections now..." && break || sleep 3; done

echo "Run flyway migrate."
flyway migrate