#!/bin/sh
echo "Waiting for Postgres..."
until pg_isready -h raindb -p 5432 -U postgres; do
  echo "Postgres not ready yet..."
  sleep 2
done
echo "Postgres is ready!"
exec dotnet RainTrackerApi.dll