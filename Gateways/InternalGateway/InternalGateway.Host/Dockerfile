FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /src/publish ./
ENTRYPOINT ["dotnet", "InternalGateway.Host.dll"]
