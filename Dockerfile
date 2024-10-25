﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Epal.Api/Epal.Api.csproj", "Epal.Api/"]
COPY ["Epal.Application/Epal.Application.csproj", "Epal.Application/"]
COPY ["Epal.Infrastructure/Epal.Infrastructure.csproj", "Epal.Infrastructure/"]
COPY ["Epal.Domain/Epal.Domain.csproj", "Epal.Domain/"]
COPY ["Epal.Tests/Epal.Tests.csproj", "Epal.Tests/"]
RUN dotnet restore "Epal.Api/Epal.Api.csproj"
COPY . .
WORKDIR "/src/Epal.Api"
RUN dotnet build "Epal.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Epal.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Epal.Api.dll"]
