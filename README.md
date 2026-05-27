# Warsaw Beauty Salon Explorer API

REST API application for managing beauty salons in Warsaw.

The project was built using ASP.NET Core Web API with Entity Framework Core and SQLite.  
The application supports CRUD operations, local JSON seed import, and experimental OpenStreetMap integration.

---

# Features

- Get all salons
- Get salon by ID
- Create new salon
- Update salon
- Delete salon
- Import salons from local JSON file
- Experimental OpenStreetMap import
- Swagger API documentation
- SQLite database support
- Repository-Service-Controller architecture

---

# Tech Stack

## Backend

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

## Architecture

- Repository Pattern
- Service Layer
- DTO Pattern
- Dependency Injection
  
---

# Project Structure

```text
Controllers/
DTOs/
Data/
Models/
Repositories/
Services/
Migrations/
```

Getting Started

# Clone Repository

```text
git clone https://github.com/MarcinKielbik/WarsawBeautySalonExplorer

```

# Restore Dependencies

```text
dotnet restore

```

# Build Application

```text 
dotnet build

```








# Run Application 

```text
dotnet run

```
Swagger UI:

http://localhost:5227/swagger









