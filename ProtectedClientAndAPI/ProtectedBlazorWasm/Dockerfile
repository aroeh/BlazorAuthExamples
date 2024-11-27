FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ProtectedBlazorWasm/ProtectedBlazorWasm.csproj", "ProtectedBlazorWasm/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "ProtectedBlazorWasm/ProtectedBlazorWasm.csproj"

COPY ["ProtectedBlazorWasm/.", "./ProtectedBlazorWasm/"]
COPY ["Shared/.", "./Shared/"]

WORKDIR "/src/ProtectedBlazorWasm"
RUN dotnet build "ProtectedBlazorWasm.csproj" -c $BUILD_CONFIGURATION

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app

FROM nginx:alpine
WORKDIR /usr/share/nginx/html

COPY --from=publish /app/wwwroot .
COPY ["ProtectedBlazorWasm/nginx.conf", "/etc/nginx/nginx.conf"]
