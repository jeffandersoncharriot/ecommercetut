﻿FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Ecommerce.Api.Products/Ecommerce.Api.Products.csproj", "Ecommerce.Api.Products/"]
RUN dotnet restore "Ecommerce.Api.Products/Ecommerce.Api.Products.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Api.Products"
RUN dotnet build "Ecommerce.Api.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.Products.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.Products.dll"]
