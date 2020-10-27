FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HelloWeb/HelloWeb.csproj", "HelloWeb/"]
RUN dotnet restore "HelloWeb/HelloWeb.csproj"
COPY . .
WORKDIR "/src/HelloWeb"
RUN dotnet build "HelloWeb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HelloWeb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HelloWeb.dll"]