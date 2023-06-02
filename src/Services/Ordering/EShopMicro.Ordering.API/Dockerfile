#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Ordering/EShopMicro.Ordering.API/EShopMicro.Ordering.API.csproj", "Services/Ordering/EShopMicro.Ordering.API/"]
COPY ["Services/Ordering/EShopMicro.Ordering.Application/EShopMicro.Ordering.Application.csproj", "Services/Ordering/EShopMicro.Ordering.Application/"]
COPY ["Services/Ordering/EShopMicro.Ordering.Domain/EShopMicro.Ordering.Domain.csproj", "Services/Ordering/EShopMicro.Ordering.Domain/"]
COPY ["Services/Ordering/EShopMicro.Ordering.Infrastructure/EShopMicro.Ordering.Infrastructure.csproj", "Services/Ordering/EShopMicro.Ordering.Infrastructure/"]
COPY ["BuildingBlocks/EShopMicro.EventBus.Messages/EShopMicro.EventBus.Messages.csproj", "BuildingBlocks/EShopMicro.EventBus.Messages/"]
RUN dotnet restore "Services/Ordering/EShopMicro.Ordering.API/EShopMicro.Ordering.API.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/EShopMicro.Ordering.API"
RUN dotnet build "EShopMicro.Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShopMicro.Ordering.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShopMicro.Ordering.API.dll"]