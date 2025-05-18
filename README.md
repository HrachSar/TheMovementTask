# Product & Category Management API

A RESTful API built with .NET 7, Entity Framework Core (Code-First), and SQLite to manage products and categories with a many-to-many relationship. 
Each product belongs to 2 or 3 categories, and the API includes full CRUD operations, pagination, DTOs, validation, Swagger documentation, and service/repository patterns.

## Features

- Full CRUD for Products and Categories
- Products can belong to 2 or 3 categories (many-to-many)
- DTOs for clean API contracts
- FluentValidation for request validation
- Pagination for both product and category listings
- Swagger UI for easy API testing
- EF Core (Code-First with Migrations)
- SQLite for lightweight database
- Repository and Service patterns for clean architecture

## Technology Stack

- .NET 7
- Entity Framework Core
- SQLite
- AutoMapper
- FluentValidation
- Swagger / Swashbuckle
- C# 

## API Endpoints
## Category

 - GET /api/categories – Paginated list

 - GET /api/categories/{id} – Get by ID

 - POST /api/categories – Create

 - PUT /api/categories/{id} – Update

 - DELETE /api/categories/{id} – Delete

## Product

  -  GET /api/products – Paginated list with category info

  -  GET /api/products/{id} – Get by ID

  -  POST /api/products – Create with 2-3 categories

  -  PUT /api/products/{id} – Update product and categories

  -  DELETE /api/products/{id} – Delete

## 1. Run the API:

 - dotnet run

## 2. Open Swagger in your browser at:

 - http://localhost:PORT/swagger


##




