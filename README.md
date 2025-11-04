# Clean Architecture Task Management API

## 📋 Project Overview

A task management system built with Clean Architecture principles using ASP.NET Core 7.0, Entity Framework Core, and JWT Authentication.

## 🏗️ Architecture

The project implements Clean Architecture with 4 layers:

### Clean.Core (Domain Layer)
- **Entities**: Core business models (User, TaskItem, Category, JwtToken)
- **DTOs**: Data Transfer Objects with validation attributes
- **Repositories**: Data access interfaces
- **Services**: Business logic interfaces

### Clean.Data (Infrastructure Layer)
- **DataContext**: Entity Framework context
- **Repositories**: Data access implementations
- **Migrations**: Database migrations

### Clean.Service (Application Layer)
- **Services**: Business logic implementations
- **Mapping**: AutoMapper profiles
- **Security**: JWT token generation

### Clean.API (Presentation Layer)
- **Controllers**: API endpoints
- **Middlewares**: Custom middleware components
- **Configuration**: Application settings

## 🔒 Security Features

### Authentication & Authorization
- JWT Token-based authentication
- User isolation (users see only their data)
- Input validation with Data Annotations

### Input Validation
- Required field validation
- String length validation
- Email format validation
- Regular expression validation

## 🚀 Getting Started

### Prerequisites
- .NET 7.0 SDK
- SQL Server / SQL Server Express
- Visual Studio 2022 or VS Code

### Installation Steps

1. **Clone the repository**
```bash
git clone [repository-url]
cd Clean
```

2. **Update Connection String**
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=YOUR_SERVER;Database=TaskManagerDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

3. **Run Migrations**
```bash
dotnet ef database update --project Clean.Data --startup-project Clean.API
```

4. **Install Dependencies**
```bash
dotnet restore
```

5. **Run the Application**
```bash
dotnet run --project Clean.API
```

## 📚 API Documentation

### Authentication Endpoints

#### POST /api/auth/login
**Request:**
```json
{
  "username": "string (3-50 chars)",
  "password": "string (6+ chars)"
}
```

**Response:**
```json
{
  "token": "jwt_token_string",
  "user": {
    "id": 1,
    "username": "john_doe",
    "email": "john@example.com"
  }
}
```

#### POST /api/auth/register
**Request:**
```json
{
  "username": "string (3-50 chars, alphanumeric + underscore)",
  "email": "valid email address",
  "password": "string (6+ chars)"
}
```

### Task Management Endpoints

#### GET /api/task
- **Authorization**: Bearer Token required
- **Description**: Get all tasks for authenticated user

#### POST /api/task
**Request:**
```json
{
  "title": "string (1-50 chars, required)",
  "description": "string (max 500 chars)",
  "priority": "Low|Medium|High",
  "categoryId": "positive integer"
}
```

#### PUT /api/task/{id}
- **Authorization**: Bearer Token required
- **Description**: Update task (user can only update their own tasks)

#### DELETE /api/task/{id}
- **Authorization**: Bearer Token required
- **Description**: Delete task (user can only delete their own tasks)

### Category Management Endpoints

#### GET /api/category
- **Authorization**: Bearer Token required
- **Description**: Get all categories for authenticated user

#### POST /api/category
**Request:**
```json
{
  "name": "string (required)",
  "color": "string (hex color)"
}
```

## 🛡️ Security

- JWT Token-based authentication
- User data isolation
- Input validation with Data Annotations

## 🔧 Middleware Components

### TimeBasedAccessMiddleware
- Blocks access during specified hours (2:00-5:00 AM by default)
- Configurable in appsettings.json

### RequestCounterMiddleware
- Counts HTTP requests
- Logs for monitoring purposes

## 📊 Database

- Entity Framework Core with Code-First approach
- SQL Server database
- User isolation with foreign key relationships

## 🧪 Testing

- XUnit framework ready
- Run tests: `dotnet test`



## 🚀 Deployment

### Production Considerations
- Environment-specific appsettings
- HTTPS enforcement
- Database connection security
- Secret management

## 📞 Support

For questions and support, contact the project developer.

## 📄 License

Educational project - All rights reserved.