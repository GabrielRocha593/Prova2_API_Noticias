# Use a imagem oficial do SDK do .NET 6 como ponto de partida
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Define o diretório de trabalho dentro do contêiner
WORKDIR /app

# Copie o arquivo .csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copie o restante do código-fonte da aplicação
COPY . .

# Publicar a aplicação
RUN dotnet publish -c Release -o out

# Use a imagem oficial do ASP.NET 6 como imagem de destino
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Defina o diretório de trabalho dentro do contêiner
WORKDIR /app

# Copie os arquivos publicados da imagem anterior
COPY --from=build /app/out .

# Exponha a porta em que a aplicação está ouvindo (substitua pela porta correta da sua aplicação)
EXPOSE 80

# Especifique o comando de inicialização da aplicação
ENTRYPOINT ["dotnet", "UsuariosAPI.dll"]