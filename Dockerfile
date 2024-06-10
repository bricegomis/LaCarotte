#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/Carottes.API/Carottes.API.csproj", "."]
RUN dotnet restore "./Carottes.API.csproj"
COPY ["API/Carottes.API/", "."]
WORKDIR "/src/."
RUN dotnet build "./Carottes.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Carottes.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Build stage for the frontend
FROM node:22-alpine AS build-front
WORKDIR /app
COPY Client/Web/package*.json ./
RUN npm ci
COPY Client/Web .
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-front /app/dist/spa ./wwwroot
ENTRYPOINT ["dotnet", "Carottes.API.dll"]