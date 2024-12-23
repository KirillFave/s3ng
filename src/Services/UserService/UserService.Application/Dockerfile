FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5005

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/UserService/UserService.Application/UserService.Application.csproj", "UserService/UserService.Application/"]
COPY ["Services/UserService/UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService/UserService.Infrastructure/"]
COPY ["Services/UserService/UserService.Domain/UserService.Domain.csproj", "UserService/UserService.Domain/"]
RUN dotnet restore "Services/UserService/UserService.Application/UserService.Application.csproj"
COPY . .
WORKDIR "/src/UserService"
RUN dotnet build "./UserService.Application.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserService.Application.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.dll"]
