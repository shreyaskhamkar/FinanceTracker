# FinanceTracker

A comprehensive personal finance tracking application built with .NET 9, Entity Framework Core, and SQLite. Track your expenses, categorize spending, and analyze your financial habits with detailed analytics.

## Features

- **User Authentication** - Secure registration and login system
- **Expense Management** - Create, read, update, and delete expenses
- **Category Tracking** - Organize expenses by categories (Food, Transport, Shopping, Bills, Entertainment, Health, Other)
- **Analytics Dashboard** - View monthly summaries and category-wise spending breakdowns
- **RESTful API** - Full API support for frontend integration
- **Docker Support** - Containerized deployment

## Tech Stack

- **Backend**: .NET 9.0
- **Framework**: ASP.NET Core Web API
- **Database**: SQLite with Entity Framework Core
- **Authentication**: JWT-based authentication
- **Architecture**: Clean Architecture (Domain-Driven Design)

## Project Structure

```
FinanceTracker/
├── FinanceTracker.Api/              # Web API layer
│   ├── Controllers/                 # API controllers
│   │   ├── AuthController.cs       # Authentication endpoints
│   │   ├── ExpensesController.cs   # Expense CRUD endpoints
│   │   └── AnalyticsController.cs  # Analytics endpoints
│   ├── Program.cs                  # Application entry point
│   └── appsettings.json            # Configuration
├── FinanceTracker.Application/      # Application layer
│   ├── DTOs/                        # Data Transfer Objects
│   └── Interfaces/                 # Repository interfaces
├── FinanceTracker.Domain/          # Domain layer
│   └── Entities/                    # Domain entities
│       ├── User.cs
│       ├── Expense.cs
│       └── ExpenseCategory.cs
└── FinanceTracker.Infrastructure/  # Infrastructure layer
    ├── Data/                        # Database context
    ├── Repositories/               # Repository implementations
    └── Migrations/                  # EF Core migrations
```

## API Endpoints

### Authentication

| Method | Endpoint             | Description             |
| ------ | -------------------- | ----------------------- |
| POST   | `/api/auth/register` | Register a new user     |
| POST   | `/api/auth/login`    | Login and get JWT token |

### Expenses

| Method | Endpoint             | Description               |
| ------ | -------------------- | ------------------------- |
| GET    | `/api/expenses`      | Get all expenses for user |
| GET    | `/api/expenses/{id}` | Get expense by ID         |
| POST   | `/api/expenses`      | Create new expense        |
| PUT    | `/api/expenses/{id}` | Update expense            |
| DELETE | `/api/expenses/{id}` | Delete expense            |

### Analytics

| Method | Endpoint                                   | Description            |
| ------ | ------------------------------------------ | ---------------------- |
| GET    | `/api/analytics/monthly/{year}/{month}`    | Get monthly summary    |
| GET    | `/api/analytics/categories/{year}/{month}` | Get category breakdown |

## Setup Instructions

### Prerequisites

- .NET 9.0 SDK
- SQL Server (optional, SQLite used by default)

### Local Development

1. Clone the repository

```bash
git clone <repository-url>
cd FinanceTracker
```

2. Restore dependencies

```bash
dotnet restore
```

3. Run the application

```bash
dotnet run --project FinanceTracker.Api
```

4. The API will be available at `https://localhost:7000`

### Database

The application uses SQLite by default and will create the database automatically on first run. To use SQL Server instead:

1. Update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FinanceTracker;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

2. Run migrations:

```bash
dotnet ef database update --project FinanceTracker.Infrastructure
```

## Docker

### Building the Docker Image

```bash
docker build -t financetracker .
```

### Running the Container

```bash
docker run -d -p 8080:8080 --name financetracker-app financetracker
```

The application will be available at `http://localhost:8080`

### Using Docker Compose

Create a `docker-compose.yml` file:

```yaml
version: "3.8"

services:
  financetracker:
    build: .
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Data Source=financetracker.db
    volumes:
      - financetracker-data:/app/data

volumes:
  financetracker-data:
```

Run with Docker Compose:

```bash
docker-compose up -d
```

### Docker Environment Variables

| Variable                               | Description                | Default                       |
| -------------------------------------- | -------------------------- | ----------------------------- |
| `ASPNETCORE_ENVIRONMENT`               | Runtime environment        | Development                   |
| `ConnectionStrings__DefaultConnection` | Database connection string | Data Source=financetracker.db |

## Configuration

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=financetracker.db"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyForJwtTokenGeneration",
    "Issuer": "FinanceTracker",
    "Audience": "FinanceTrackerUsers",
    "ExpiryMinutes": 60
  }
}
```

**Note**: In production, use environment variables or a secrets manager for sensitive values like the JWT key.

## Models

### User

| Field        | Type   | Description       |
| ------------ | ------ | ----------------- |
| Id           | int    | Unique identifier |
| Username     | string | Unique username   |
| Email        | string | User email        |
| PasswordHash | string | Hashed password   |

### Expense

| Field       | Type            | Description         |
| ----------- | --------------- | ------------------- |
| Id          | int             | Unique identifier   |
| UserId      | int             | Foreign key to User |
| Amount      | decimal         | Expense amount      |
| Description | string          | Expense description |
| Category    | ExpenseCategory | Category enum       |
| Date        | DateTime        | Expense date        |
| CreatedAt   | DateTime        | Creation timestamp  |

### ExpenseCategory (Enum)

- Food
- Transport
- Shopping
- Bills
- Entertainment
- Health
- Other

## Example Usage

### Register User

```bash
curl -X POST https://localhost:7000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"username": "john", "email": "john@example.com", "password": "Password123!"}'
```

### Login

```bash
curl -X POST https://localhost:7000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email": "john@example.com", "password": "Password123!"}'
```

### Create Expense (with JWT token)

```bash
curl -X POST https://localhost:7000/api/expenses \
  -H "Authorization: Bearer <YOUR_JWT_TOKEN>" \
  -H "Content-Type: application/json" \
  -d '{"amount": 50.00, "description": "Groceries", "category": "Food", "date": "2026-03-19"}'
```

## License

MIT License
