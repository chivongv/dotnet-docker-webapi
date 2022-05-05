## Getting started

```bash
docker-compose up
```

```bash
docker-compose down -v
```

## Debug

Debugging api

```bash
docker compose up api
```

Debugging db

```bash
docker compose up db
```

## Migrations
Create and update database

```bash
dotnet ef migrations add CreateDatabase
dotnet ef database update CreateDatabase
```

Revert and remove migrations

```bash
dotnet ef database update -c ProductContext 0
dotnet ef migrations remove
```

## Packages
Adding packages

```bash
dotnet add . package Npgsql.EntityFrameworkCore.Postgresql
```

## Secrets

- [For production](https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-6.0)