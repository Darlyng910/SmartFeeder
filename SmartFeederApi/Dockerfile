# Etapa 1: Build (Construcción)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar todo
COPY . ./

# Restaurar dependencias
RUN dotnet restore SmartFeederApi.csproj

# Compilar en modo Release
RUN dotnet build SmartFeederApi.csproj -c Release -o /app/build

# Etapa 2: Publish (Publicación)
FROM build AS publish
RUN dotnet publish SmartFeederApi.csproj -c Release -o /app/publish

# Etapa 3: Runtime (Ejecución)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiar archivos publicados desde la etapa Publish
COPY --from=publish /app/publish .

# Ejecutar app
ENTRYPOINT ["dotnet", "SmartFeederApi.dll"]