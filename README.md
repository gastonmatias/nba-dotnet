# Nba-dotnet
WebAPI que proporciona información sobre jugadores, equipos y conferencias de la nba 2023-2024

# Levantar bd (linux)
```
systemctl start docker
docker start sqlserver
```
# Migrations
```
# primera
dotnet ef migrations add Initial

# ...posteriores
dotnet ef migrations add NombreMigracion

# sincronizar bd
dotnet ef database update
```

# Levantar app .net CLI
```bash
dotnet run

# modo dev
dotnet watch
```
