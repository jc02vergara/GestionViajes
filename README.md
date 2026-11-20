# Sistema de Gestión de Viajes (.NET)

Proyecto web en **C# / ASP.NET Core 8 (MVC)** para gestionar **destinos**, **turistas** y **reservas de viajes**.

## Tablas

### Destinos
- `DestinoId` (PK, autoincremental)
- `Nombre`
- `Pais`
- `Descripcion`
- `Costo`

### Turistas
- `TuristaId` (PK, autoincremental)
- `Nombre`
- `Apellido`
- `Email`
- `Telefono`

### Reservas
- `ReservaId` (PK, autoincremental)
- `DestinoId` (FK -> Destinos)
- `TuristaId` (FK -> Turistas)
- `FechaReserva`
- `FechaViaje`
- `Personas`
- `Total`
- `Estado`

## Requisitos
- .NET SDK 8.0

## Ejecución
```bash
cd GestionViajes
dotnet run
```
Abrir la URL indicada (por defecto `http://localhost:5000`) y navegar por el menú.

La base de datos `GestionViajesDb.sqlite` se crea automáticamente al iniciar.

## Versionamiento
Proyecto subido a GitHub.