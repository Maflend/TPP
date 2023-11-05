#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TPP.Bot.Host/TPP.Bot.Host.csproj", "TPP.Bot.Host/"]
COPY ["TPP.MessageBus.Shared/TPP.MessageBus.Shared.csproj", "TPP.MessageBus.Shared/"]
RUN dotnet restore "./TPP.Bot.Host/./TPP.Bot.Host.csproj"
COPY . .
WORKDIR "/src/TPP.Bot.Host"
RUN dotnet build "./TPP.Bot.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TPP.Bot.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TPP.Bot.Host.dll"]