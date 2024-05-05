#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ChatServer.API/ChatServer.API.csproj", "ChatServer.API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["ChatApi/Domain.csproj", "ChatApi/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "./ChatServer.API/ChatServer.API.csproj"
COPY . .
WORKDIR "/src/ChatServer.API"
RUN dotnet build "./ChatServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChatServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/certificates
COPY ChatServer.API/certificates/aspnetapp.pfx /app/certificates/

ENTRYPOINT ["dotnet", "ChatServer.API.dll"]

