# Recuperar SDK do site da Microsoft
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copiar CSPROJ e restaurar via NuGet
COPY *.csproj ./
RUN dotnet restore

# Copiar os arquivos do projeto e criar lançamento
COPY . ./
RUN dotnet publish -c Release -o out

# Gerar imagem 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "FuncionalHealthChallenge.dll" ]