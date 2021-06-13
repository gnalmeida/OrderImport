#!/bin/bash

set -e
run_cmd="dotnet run --server.urls http://*:8000"

until dotnet ef database update -s ./src/OrderImport.API/OrderImport.API.csproj  -p ./src/OrderImport.Infra.Data/OrderImport.Infra.Data.csproj; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"
exec $run_cmd