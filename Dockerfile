# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar a solução
COPY ServidorPublico.sln ./

# Copiar todos os projetos na estrutura correta
COPY ServidorPublico.API/ServidorPublico.API.csproj ./ServidorPublico.API/
COPY ServidorPublico.Application/ServidorPublico.Application.csproj ./ServidorPublico.Application/
COPY ServidorPublico.Domain/ServidorPublico.Domain.csproj ./ServidorPublico.Domain/
COPY ServidorPublico.Infrastructure/ServidorPublico.Infrastructure.csproj ./ServidorPublico.Infrastructure/

# Restaurar os pacotes
RUN dotnet restore

# Copiar todo o restante do código
COPY . .

# Build e publish
WORKDIR /src/ServidorPublico.API
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "ServidorPublico.API.dll"]
