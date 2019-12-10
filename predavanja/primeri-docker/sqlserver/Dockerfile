#
# BUILD COMMAND (EXAMPLE)
# docker build -t mssql-server-image .
#
# RUN COMMAND (EXAMPLE)
#  docker run -dit --name mssql-server-container -p 34533:14533 mssql-server-image

FROM microsoft/mssql-server-linux:2017-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=DuskoDugousko123
ENV MSSQL_PID=Developer

WORKDIR /src

COPY DockerServices/sqlserver/initialize-database.sql ./
COPY DockerServices/sqlserver/setup-database.sh ./
COPY DockerServices/sqlserver/entrypoint.sh ./

# Grant permissions for the setup-database and entrypoint shell scripts to be executable
RUN chmod +x ./setup-database.sh
RUN chmod +x ./entrypoint.sh

CMD bash ./entrypoint.sh