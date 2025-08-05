#!/bin/bash

# Exit on error
set -e

# Get current timestamp for migration name
timestamp=$(date +"%Y%m%d%H%M%S")
migration_name="Update$timestamp"

echo "➡️  Creating migration: $migration_name"
dotnet ef migrations add "$migration_name"

echo "✅ Migration created."
echo "🚀 Running application..."
dotnet run
