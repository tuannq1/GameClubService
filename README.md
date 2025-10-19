# GameClubService

A full-stack web application for managing game clubs and events. This service allows users to create, manage, and organize gaming clubs and their associated events.
## Overview

GameClubService is a modern web application designed to help gaming communities organize clubs and manage events. The application features a RESTful API backend built with ASP.NET Core and a dynamic frontend built with Angular.

## Technology Stack

### Back-End

- **Framework**: ASP.NET Core (Minimal API)
- **Database**: SQLite with Entity Framework Core
- **ORM**: Entity Framework Core
- **API Documentation**: Swagger/OpenAPI
- **Architecture**: Clean Architecture (Domain, Infrastructure, API layers)

### Front-End

- **Framework**: Angular 20.3.0
- **Language**: TypeScript 5.9.2
- **Styling**: SCSS
- **HTTP Client**: RxJS
- **Testing**: Jasmine + Karma

## Architecture

The project follows Clean Architecture principles with clear separation of concerns:

```
GameClubService/
├── src/back-end/
│   ├── GameClubService.API/         # Presentation Layer
│   │   ├── ExceptionHandlers/       # Global exception handling
│   │   └── MappingEndpoints.cs      # API endpoint mappings
│   ├── GameClubService.Domain/      # Domain Layer
│   │   ├── Entities/                # Domain entities (Club, Event)
│   │   └── Interfaces/              # Repository interfaces
│   └── GameClubService.Infrastructure/ # Infrastructure Layer
│       ├── Data/                    # DbContext
│       ├── Migrations/              # EF Core migrations
│       └── Repositories/            # Repository implementations
└── src/front-end/                   # Angular Application
    └── src/app/
        ├── pages/                   # Feature pages
        │   ├── clubs/               # Club management
        │   └── events/              # Event management
        └── services/                # HTTP services
```

## Prerequisites

Before running the application, ensure you have the following installed:

### Back-End Requirements

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
- Visual Studio 2022 / VS Code / Rider (optional)

### Front-End Requirements

- [Node.js](https://nodejs.org/) (v20.x or later)
- [npm](https://www.npmjs.com/) (comes with Node.js)
- [Angular CLI](https://angular.io/cli) (v20.3.6)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd GameClubService
```

### 2. Back-End Setup

#### Navigate to the API project

```bash
cd src/back-end/GameClubService.API
```

#### Restore dependencies

```bash
dotnet restore
```

#### Apply database migrations

```bash
dotnet ef database update
```

#### Run the API

```bash
dotnet run
```

The API will start on `https://localhost:5001` (or the port specified in `launchSettings.json`).

**Swagger UI** will be available at: `https://localhost:5001` (in Development mode)

### 3. Front-End Setup

#### Navigate to the front-end directory

```bash
cd src/front-end
```

#### Install dependencies

```bash
npm install
```

#### Start the development server

```bash
npm start
```

The Angular application will start on `http://localhost:4200`.

### 4. Access the Application

- **Frontend**: http://localhost:4200
- **Backend API**: https://localhost:5141
- **Swagger Documentation**: https://localhost:5141 (Development mode)

## 📁 Project Structure

### Back-End Structure

```
GameClubService.API/
├── ExceptionHandlers/           # Custom exception handlers
├── MappingEndpoints.cs          # API endpoint mappings
├── Program.cs                   # Application entry point
├── appsettings.json             # Configuration
└── gameclub.db                  # SQLite database

GameClubService.Domain/
├── Common/                      # Shared enums and constants
├── Entities/                    # Domain models (Club, Event)
└── Interfaces/                  # Repository contracts

GameClubService.Infrastructure/
├── Data/                        # EF Core DbContext
├── Migrations/                  # Database migrations
├── Repositories/                # Repository implementations
└── DependencyInjection.cs       # Service registration
```

### Front-End Structure

```
src/app/
├── pages/
│   ├── clubs/
│   │   ├── club-form/           # Club creation/editing
│   │   └── club-list/           # Club listing
│   └── events/
│       ├── event-form/          # Event creation/editing
│       └── event-list/          # Event listing
├── services/
│   ├── club.ts                  # Club HTTP service
│   └── event.ts                 # Event HTTP service
├── app.routes.ts                # Application routing
└── app.ts                       # Root component
```

## 📚 API Documentation

### API Endpoints

The API follows RESTful conventions. Access the Swagger UI at `https://localhost:5141` for complete API documentation.

#### Endpoints

- `GET /api/clubs` - Get clubs
- `POST /api/clubs` - Create new club
- `GET /api/clubs/{clubId}/events` - Get events by clubId
- `POST /api/clubs/{clubId}/events` - Create new event

#### Event Endpoints

- Event endpoints follow similar patterns (implementation may vary)

### CORS Configuration

The API is configured with CORS to allow requests from the Angular frontend:

```csharp
AllowAnyOrigin()
AllowAnyMethod()
AllowAnyHeader()
```

**Note**: For production, configure specific origins instead of `AllowAnyOrigin()`.

## 💻 Development Guidelines

### Back-End Development

#### 1. Adding New Entities

1. Create the entity in `GameClubService.Domain/Entities/`
2. Add the repository interface in `GameClubService.Domain/Interfaces/`
3. Implement the repository in `GameClubService.Infrastructure/Repositories/`
4. Add the DbSet to `GameClubDbContext.cs`
5. Create and apply migrations:
   ```bash
   dotnet ef migrations add <MigrationName>
   dotnet ef database update
   ```

#### 2. Adding New Endpoints

1. Create endpoint mappings in a new file (e.g., `MappingEventEndpoints.cs`)
2. Register endpoints in `Program.cs`
3. Follow RESTful conventions for URL patterns

#### 3. Exception Handling

- Use the global exception handler in `ExceptionHandlers/GlobalExceptionHandler.cs`
- Custom exceptions should be handled appropriately
- Return proper HTTP status codes

#### 4. Database Migrations

```bash
# Create a new migration
dotnet ef migrations add <MigrationName> --project src/back-end/GameClubService.Infrastructure --startup-project src/back-end/GameClubService.API

# Apply migrations
dotnet ef database update --project src/back-end/GameClubService.Infrastructure --startup-project src/back-end/GameClubService.API

# Remove last migration (if not applied)
dotnet ef migrations remove --project src/back-end/GameClubService.Infrastructure --startup-project src/back-end/GameClubService.API
```



