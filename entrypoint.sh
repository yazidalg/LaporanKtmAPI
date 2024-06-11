#!/bin/bash
set -e
# Run migrations
dotnet ef database update
# Run the application
exec dotnet lapora-ktm-api.dll