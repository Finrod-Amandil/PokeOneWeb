#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PokeOneWeb/PokeOneWeb.WebApi.csproj", "PokeOneWeb/"]
COPY ["PokeOneWeb.Shared/PokeOneWeb.Shared.csproj", "PokeOneWeb.Shared/"]
COPY ["PokeOneWeb.Data/PokeOneWeb.Data.csproj", "PokeOneWeb.Data/"]
RUN dotnet restore "PokeOneWeb/PokeOneWeb.WebApi.csproj"
COPY . .
WORKDIR "/src/PokeOneWeb"
RUN dotnet build "PokeOneWeb.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokeOneWeb.WebApi.csproj" -c Release -o /app/publish

FROM base AS finals
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokeOneWeb.WebApi.dll"]