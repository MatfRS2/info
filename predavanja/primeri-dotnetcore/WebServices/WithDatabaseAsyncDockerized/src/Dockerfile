FROM microsoft/aspnetcore:2.0-stretch AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-stretch AS build
WORKDIR /src
COPY ["WithDatabaseAsyncDockerized/src/WithDatabaseAsyncDockerized.csproj", "WithDatabaseAsyncDockerized/src/"]
COPY ["WithDatabaseAsyncDockerized/src/WithDatabaseAsyncDockerized.xml", "WithDatabaseAsyncDockerized/src/"]
RUN dotnet restore "WithDatabaseAsyncDockerized/src/WithDatabaseAsyncDockerized.csproj"
COPY . .
WORKDIR "/src/WithDatabaseAsyncDockerized/src"
RUN dotnet build "WithDatabaseAsyncDockerized.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WithDatabaseAsyncDockerized.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY /WithDatabaseAsyncDockerized/src/WithDatabaseAsyncDockerized.xml .
ENTRYPOINT ["dotnet", "WithDatabaseAsyncDockerized.dll"]
