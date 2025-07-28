# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

ScoutVenture is a Scout Group Management System for DPSG (Deutsche Pfadfinderschaft Sankt Georg). It consists of an Angular frontend and ASP.NET Core backend with PostgreSQL database.

### Application Goal

The primary purpose of ScoutVenture is to enable parents to register their children for scout events. Key aspects:

- **Parent Registration**: Parents sign up as users in the application
- **Child Data Source**: Children's data comes from NAMI (DPSG's central member database) - ScoutVenture does NOT manage children directly
- **Parent-Child Assignment**: Administrators link children from NAMI to their parent users in the app
- **Event Registration**: Parents can view their assigned children and register them for events
- **Future Expansion**: Additional child data (e.g., medical information for camping trips) can be added

### User Roles

1. **Parents/Adult Members**: Can view assigned children, see child details, and register children for events
   - Users 18+ years old are considered adults and can register themselves for events
   - In this case, they are assigned a "child" entry with their own name, allowing self-registration
2. **Administrators**: Can create events, manage registrations, assign children to parents, and integrate with NAMI

## Development Commands

### Frontend Development
```bash
cd frontend/ScoutVenture
npm install           # Install dependencies
npm start             # Start dev server at http://0.0.0.0:4200
npm run build         # Production build
npm run lint          # Run ESLint
npm test              # Run unit tests with Karma/Jasmine
npm run ci:test       # Run tests with coverage in headless mode
```

### Backend Development
```bash
cd asp-net/ScoutVenture
dotnet restore        # Restore NuGet packages
dotnet build          # Build solution
dotnet run --project ScoutVenture  # Run API
```

### Development Environment
```bash
cd tools/dev
docker-compose up     # Start PostgreSQL, Traefik, and Mailpit
```

## Architecture

### Backend Structure
The backend follows a layered architecture with clear separation of concerns:

- **ScoutVenture/** - Web API layer with controllers and startup configuration
- **Core/** - Business logic and application services
- **CoreContracts/** - Domain models, DTOs, and service interfaces
- **PostgresAdapter/** - Data access layer using Entity Framework Core
- **NamiClient/** - Integration client for DPSG NaMi API
- **SmtpAdapter/** - Email service implementation
- **AppSettings/** - Configuration models for structured settings

### Frontend Structure
Angular application with feature modules:

- **auth/** - Authentication flow (login, register, password reset, email confirmation)
- **main/** - Core application features
  - **administration/** - User management, NaMi integration
  - **events/** - Event management and registrations
  - **person/** - Member/person management
- **shared/** - Cross-cutting concerns (guards, models, utilities, API client)
- **menu/** - Navigation components

### Key Technical Decisions

1. **Authentication**: JWT Bearer tokens with OpenID Connect support
2. **Database**: PostgreSQL with Entity Framework Core, uses database views for complex queries
3. **API Documentation**: Swagger/OpenAPI via Swashbuckle
4. **Frontend State**: Angular services with RxJS observables
5. **Email**: SMTP adapter with configurable providers (Mailpit for development)

### Configuration

The application uses hierarchical configuration with environment-specific overrides:
- Environment variables prefixed with `scoutventure.`
- Command-line arguments
- appsettings.json files

Key configuration areas:
- Database connection (`scoutventure.db.*`)
- OAuth2/OIDC settings (`scoutventure.oauth.*`)
- Application URLs (`scoutventure.uri`)
- NaMi integration (`scoutventure.nami.url`)

### Development Workflow

1. Start infrastructure: `docker-compose up` in `tools/dev/`
2. Run backend: `dotnet run` in `asp-net/ScoutVenture/ScoutVenture/`
3. Run frontend: `npm start` in `frontend/ScoutVenture/`
4. Access application at http://localhost:4200
5. Access Mailpit at http://localhost:8025
6. Database is available at localhost:5432

### Database Migrations

Entity Framework Core migrations are used:
```bash
cd asp-net/ScoutVenture/PostgresAdapter
dotnet ef migrations add MigrationName --startup-project ../ScoutVenture
dotnet ef database update --startup-project ../ScoutVenture
```

### API Integration Points

1. **DPSG NaMi API**: Member synchronization via NamiClient
2. **OAuth2 Provider**: Authentication via OIDC (Keycloak, Authentik, or Zitadel)
3. **Email Service**: SMTP for user notifications

### Testing Approach

- Frontend: Jasmine/Karma for unit tests
- Backend: No test projects currently configured
- Run frontend tests: `npm test` or `npm run ci:test` for CI mode

### Security Considerations

- OAuth2/OIDC for authentication
- JWT tokens for API authorization
- Email confirmation for new users
- Password reset flow with secure tokens
- Monitoring endpoints should be protected by reverse proxy