# Task Management API – Clean Architecture

## 📋 Project Overview
A task management system built using Clean Architecture principles.  
Developed with **ASP.NET Core 7.0**, **Entity Framework Core**, and **JWT Authentication**.

## 🏗️ Architecture Layers

| Layer | Responsibility |
|-------|----------------|
| **Clean.Core** (Domain) | Entities, DTOs, Repository Interfaces, Service Interfaces |
| **Clean.Data** (Infrastructure) | EF Core DataContext, Repository Implementations, Migrations |
| **Clean.Service** (Application) | Business logic implementations, AutoMapper Profiles, JWT Generation |
| **Clean.API** (Presentation) | Controllers, Middleware components, Application configuration |

## 🔒 Security
- **JWT** based authentication
- **User data isolation** (each user sees only their own data)
- **DTO validation** attributes

## 🚀 Getting Started

### Requirements
- **.NET 7** SDK
- **SQL Server**
- **Visual Studio 2022** or VS Code

### Setup
```bash
git clone [repository-url]
cd Clean
```
Update connection string in `appsettings.json`

**Apply Database Migrations:**
```bash
dotnet ef database update --project Clean.Data --startup-project Clean.API
```

**Run the API:**
```bash
dotnet run --project Clean.API
```

## 📚 Main API Endpoints

### Authentication
- `POST /api/auth/login`
- `POST /api/auth/register`

### Tasks
- `GET /api/task`
- `POST /api/task`
- `PUT /api/task/{id}`
- `DELETE /api/task/{id}`

### Categories
- `GET /api/category`
- `POST /api/category`

> All task and category endpoints require **Bearer Token** authentication

## 🛡️ Security

- JWT Token-based authentication
- User data isolation
- Input validation with Data Annotations

## 🔧 Middlewares Included
- **TimeBasedAccessMiddleware** – blocks requests during configured hours
- **RequestCounterMiddleware** – counts incoming HTTP requests

## 📊 Database
- **Entity Framework Core** (Code-First)
- **SQL Server**
- Users own their Categories and Tasks (foreign key isolation)

## 📄 License
Educational project – All rights reserved.