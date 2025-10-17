FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["LaCarotte.API/LaCarotte.API.csproj", "LaCarotte.API/"]
RUN dotnet restore "LaCarotte.API/LaCarotte.API.csproj"
COPY . .
WORKDIR "/src/LaCarotte.API"
RUN dotnet build "LaCarotte.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "LaCarotte.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LaCarotte.API.dll"]
