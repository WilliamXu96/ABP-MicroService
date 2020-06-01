FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /src/publish ./
ENTRYPOINT ["dotnet", "AuthServer.Host.dll"]