FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ProtectedApi/ProtectedApi.csproj", "ProtectedApi/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "ProtectedApi/ProtectedApi.csproj"

COPY ["ProtectedApi/.", "./ProtectedApi/"]
COPY ["Shared/.", "./Shared/"]

WORKDIR "/src/ProtectedApi"
RUN dotnet build "./ProtectedApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProtectedApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProtectedApi.dll"]