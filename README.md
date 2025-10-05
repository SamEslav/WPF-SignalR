# WPF-SignalR
Application client-server to update data in real time

# Client-Server Project with ASP.NET Core and WPF (MVVM)

This repository contains:
- **ASP.NET Core server** with SignalR and CRUD using Entity Framework Core.
- **WPF client (MVVM)** that connects to the server via SignalR and enables real-time CRUD.

---

## Prerequisites
- Visual Studio 2022 or higher.
- .NET 8 SDK
- SQL Server (local or remote).
- Git installed.

---

## Required NuGet packages
### Server:
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.AspNetCore.SignalR`

### WPF client:
- `Microsoft.AspNetCore.SignalR.Client`

Install with:

dotnet restore

## Notes
- Repository has a folder **Client2** to add an extra client
- Solution It's configurate to startup multiple porjects
- Application just add, delete and edit countries and number of people
