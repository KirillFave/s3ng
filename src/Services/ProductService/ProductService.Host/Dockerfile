#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 50052

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/ProductService/ProductService.Host/ProductService.Host.csproj", "Services/ProductService/ProductService.Host/"]
COPY ["Services/ProductService/DataAccess/ProductService.EntityFramework/ProductService.EntityFramework.csproj", "Services/ProductService/DataAccess/ProductService.EntityFramework/"]
COPY ["Services/ProductService/ProductService.Core/ProductService.Core.csproj", "Services/ProductService/ProductService.Core/"]
COPY ["Services/ProductService/DataAccess/Repositories.Implementations/ProductService.Repositories.Implementstions.csproj", "Services/ProductService/DataAccess/Repositories.Implementations/"]
COPY ["Services/ProductService/Services/Repositories.Abstractions/ProductService.Repositories.Abstractions.csproj", "Services/ProductService/Services/Repositories.Abstractions/"]
COPY ["Services/ProductService/Services/ProductService/ProductService.ProductService.csproj", "Services/ProductService/Services/ProductService/"]
COPY ["Services/ProductService/Services/Contracts/ProductService.Contracts.csproj", "Services/ProductService/Services/Contracts/"]
COPY ["Services/ProductService/Services/Service.Abstractions/ProductService.Services.Abstractions.csproj", "Services/ProductService/Services/Service.Abstractions/"]
RUN dotnet restore "./Services/ProductService/ProductService.Host/./ProductService.Host.csproj"
COPY . .
WORKDIR "/src/Services/ProductService/ProductService.Host"
RUN dotnet build "./ProductService.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductService.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.Host.dll"]
