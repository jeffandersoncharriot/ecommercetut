﻿FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ECommerce.Api.Search/ECommerce.Api.Search.csproj", "ECommerce.Api.Search/"]
RUN dotnet restore "ECommerce.Api.Search/ECommerce.Api.Search.csproj"
COPY . .
WORKDIR "/src/ECommerce.Api.Search"
RUN dotnet build "ECommerce.Api.Search.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Api.Search.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Api.Search.dll"]
