FROM mcr.microsoft.com/mssql/server:2022-latest

COPY ./wait-and-run.sh /wait-and-run.sh
COPY ./CreateDatabaseAndSeed.sql /CreateDatabaseAndSeed.sql
# Ideally it must NOT be copied when NOT running integration tests
COPY ./CreateDatabaseAndSeed.integration-test.sql /CreateDatabaseAndSeed.integration-test.sql

ENV INTEG_TEST=0

CMD /wait-and-run.sh INTEG_TEST
