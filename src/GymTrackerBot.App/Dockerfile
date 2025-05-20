FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GymTrackerBot.App/GymTrackerBot.App.csproj", "src/GymTrackerBot.App/"]
COPY ["src/GymTrackerBot.Telegram/GymTrackerBot.Telegram.csproj", "src/GymTrackerBot.Telegram/"]
COPY ["src/GymTrackerBot.Domain/GymTrackerBot.Domain.csproj", "src/GymTrackerBot.Domain/"]
COPY ["src/GymTrackerBot.Infrastructure/GymTrackerBot.Infrastructure.csproj", "src/GymTrackerBot.Infrastructure/"]
RUN dotnet restore "src/GymTrackerBot.App/GymTrackerBot.App.csproj"
COPY . .
WORKDIR "/src/src/GymTrackerBot.App"
RUN dotnet build "GymTrackerBot.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GymTrackerBot.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymTrackerBot.App.dll"]
