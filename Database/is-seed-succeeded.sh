#!/bin/bash

echo "Checking if seed script succeeded..."

for i in {1..50};
do
    /opt/mssql-tools18/bin/sqlcmd -S database -U sa -P Dometrain#123 -C -Q "SELECT 1" > /dev/null 2>&1
    if [ $? -eq 0 ]
    then
        echo "SQL Server is ready."
        break
    else
        echo "Not ready yet..."
        sleep 1
    fi
done

# Run the SQL script
if [ $INTEG_TEST -eq 1 ]
then
    echo "/CreateDatabaseAndSeed.integration-test.sql"
    /opt/mssql-tools18/bin/sqlcmd -S database -U sa -P Dometrain#123 -C -d master -i /CreateDatabaseAndSeed.integration-test.sql
else
    echo "/CreateDatabaseAndSeed.sql"
    /opt/mssql-tools18/bin/sqlcmd -S database -U sa -P Dometrain#123 -C -d master -i /CreateDatabaseAndSeed.sql
fi
