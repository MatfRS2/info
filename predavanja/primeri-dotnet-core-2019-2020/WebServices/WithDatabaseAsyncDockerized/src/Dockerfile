FROM microsoft/aspnetcore:2.0-stretch AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-stretch AS build
WORKDIR /src
COPY ["/src/WithDatabaseAsyncDockerized.csproj", "src/"]
COPY ["/src/WithDatabaseAsyncDockerized.xml", "src/"]
RUN dotnet restore "src/WithDatabaseAsyncDockerized.csproj"
COPY . .
WORKDIR "/src/src"
RUN dotnet build "WithDatabaseAsyncDockerized.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WithDatabaseAsyncDockerized.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY /src/WithDatabaseAsyncDockerized.xml .
ENTRYPOINT ["dotnet", "WithDatabaseAsyncDockerized.dll"]
