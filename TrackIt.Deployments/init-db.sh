#!/bin/bash
set -e

echo "CREATING DATABASE trackit"
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" <<-EOSQL
    CREATE DATABASE trackit;
EOSQL

echo "CREATING DATABASE keycloak"
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" <<-EOSQL
    CREATE DATABASE keycloak;
EOSQL