FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HelloWebMvcAdo/HelloWebMvcAdo.csproj", "HelloWebMvcAdo/"]
RUN dotnet restore "HelloWebMvcAdo/HelloWebMvcAdo.csproj"
COPY . .
WORKDIR "/src/HelloWebMvcAdo"
RUN dotnet build "HelloWebMvcAdo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HelloWebMvcAdo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HelloWebMvcAdo.dll"]
