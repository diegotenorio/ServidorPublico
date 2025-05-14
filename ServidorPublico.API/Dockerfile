

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ServidorPublico.API/ServidorPublico.API.csproj", "ServidorPublico.API/"]
COPY ["ServidorPublico.Application/ServidorPublico.Application.csproj", "ServidorPublico.Application/"]
COPY ["ServidorPublico.Domain/ServidorPublico.Domain.csproj", "ServidorPublico.Domain/"]
COPY ["ServidorPublico.Infrastructure/ServidorPublico.Infrastructure.csproj", "ServidorPublico.Infrastructure/"]
RUN dotnet restore "./ServidorPublico.API/ServidorPublico.API.csproj"
COPY . .
WORKDIR "/src/ServidorPublico.API"
RUN dotnet build "./ServidorPublico.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ServidorPublico.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServidorPublico.API.dll"]