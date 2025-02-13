FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY [".", "."]

RUN dotnet restore "./Produto/Produto.API/Produto.API.csproj"
COPY . .
WORKDIR "/src/Produto/Produto.API"

RUN dotnet build "./Produto.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Produto.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

RUN mkdir -p /app/certificates
COPY config/certificates/ /app/certificates

ENTRYPOINT ["dotnet", "Produto.API.dll"]