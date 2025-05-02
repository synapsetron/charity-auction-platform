# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Базовый образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем файлы проекта и восстанавливаем зависимости
COPY ["CharityAuction.WebAPI/CharityAuction.WebAPI.csproj", "CharityAuction.WebAPI/"]
RUN dotnet restore "./CharityAuction.WebAPI/CharityAuction.WebAPI.csproj"

# Копируем весь код
COPY . .
WORKDIR "/src/CharityAuction.WebAPI"

# Собираем проект
RUN dotnet build "./CharityAuction.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CharityAuction.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный контейнер для запуска
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CharityAuction.WebAPI.dll"]
