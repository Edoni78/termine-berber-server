# 1. Stage për build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopjo solution dhe restore paketat
COPY BerberTermine/ BerberTermine/
WORKDIR /src/BerberTermine/BerberTermine
RUN dotnet restore BerberTermine.csproj

# Build projekti
RUN dotnet publish BerberTermine.csproj -c Release -o /app/publish

# 2. Stage për runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Eksporto portin standard
EXPOSE 8080

# Startup
ENTRYPOINT ["dotnet", "BerberTermine.dll"]
