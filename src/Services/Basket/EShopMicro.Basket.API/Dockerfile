#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Basket/EShopMicro.Basket.API/EShopMicro.Basket.API.csproj", "Services/Basket/EShopMicro.Basket.API/"]
COPY ["BuildingBlocks/EShopMicro.EventBus.Messages/EShopMicro.EventBus.Messages.csproj", "BuildingBlocks/EShopMicro.EventBus.Messages/"]
RUN dotnet restore "Services/Basket/EShopMicro.Basket.API/EShopMicro.Basket.API.csproj"
COPY . .
WORKDIR "/src/Services/Basket/EShopMicro.Basket.API"
RUN dotnet build "EShopMicro.Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShopMicro.Basket.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShopMicro.Basket.API.dll"]