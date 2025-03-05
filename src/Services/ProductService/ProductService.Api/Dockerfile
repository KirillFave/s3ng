FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 50052

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/ProductService/ProductService.Api/ProductService.Api.csproj", "Services/ProductService/ProductService.Api/"]
COPY ["Services/ProductService/ProductService.Application/ProductService.Application.csproj", "Services/ProductService/ProductService.Application/"]
COPY ["Services/ProductService/ProductService.Domain/ProductService.Domain.csproj", "Services/ProductService/ProductService.Domain/"]
COPY ["Services/ProductService/ProductService.Infrastructure/ProductService.Infrastructure.csproj", "Services/ProductService/ProductService.Infrastructure/"]
COPY ["SharedLibrary/SharedLibrary.csproj", "SharedLibrary/"]
RUN dotnet restore "./Services/ProductService/ProductService.Api/./ProductService.Api.csproj"
COPY . .
WORKDIR "/src/Services/ProductService/ProductService.Api"
RUN dotnet build "./ProductService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.Api.dll"]
