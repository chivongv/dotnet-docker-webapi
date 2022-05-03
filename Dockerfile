FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build

WORKDIR /app

COPY *.sln .
COPY *.csproj .

RUN dotnet restore

COPY . .

RUN dotnet publish -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/publish /app

ENTRYPOINT ["dotnet", "/app/dotnet-docker-webapi.dll"]