FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
WORKDIR "/src/examples/Logging"
RUN dotnet restore "Logging.csproj"
RUN dotnet build "Logging.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Logging.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logging.dll"]
