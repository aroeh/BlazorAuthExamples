FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProtectedWebApi.csproj", "ProtectedWebApi/"]
RUN dotnet restore "./ProtectedWebApi/ProtectedWebApi.csproj"
COPY . ProtectedWebApi/
WORKDIR "/src/ProtectedWebApi"
RUN dotnet build "./ProtectedWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProtectedWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProtectedWebApi.dll"]