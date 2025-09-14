# ------------------------------------------------------------------

# Project Documentation

# Blazor Server Application

## Overview
.NET Blazor Server application with Entity Framework Core and SQL Server integration.

## Tech Stack
- .NET 8+ Blazor Server
- Entity Framework Core
- SQL Server
- Bootstrap/CSS for styling
- SignalR (built-in with Blazor Server)

## Architecture
- Server-side rendering with real-time UI updates
- Entity Framework for data access
- SQL Server for data persistence
- Component-based UI architecture

## Key Features
- Real-time UI updates via SignalR
- Server-side C# logic
- Database integration with EF Core
- Responsive web design

## Development Notes
- Use async/await patterns for database operations
- Follow Blazor component lifecycle best practices
- Implement proper error handling and logging
- Use dependency injection for services

# ------------------------------------------------------------------

# Coding Standards

## General .NET Guidelines
- Use PascalCase for public members and types
- Use camelCase for private fields and local variables
- Use async/await for all I/O operations
- Implement proper exception handling

## Blazor Specific
- Component names should be PascalCase
- Use `@code` blocks for component logic
- Implement IDisposable when needed
- Use `StateHasChanged()` judiciously

## Entity Framework
- Use DbContext with dependency injection
- Implement repository pattern where appropriate
- Always use async methods for database operations

## Database
- Use meaningful table and column names
- Implement proper indexing
- Use foreign key constraints
- Follow normalization principles

# ------------------------------------------------------------------

# Development Setup

## Prerequisites
- .NET 9+ SDK
- SQL Server (LocalDB, Express, or full version)
- Visual Studio Code

# ------------------------------------------------------------------