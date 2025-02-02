# Staff Management Web Application

## Overview

This is a staff management web application built with Blazor WebAssembly. The application allows users to create an account and manage various aspects of staff administration, including departments, employees, vacations, and sanctions.

## Features

- User authentication and account management
- Department management
- Location management
- Employee management
- Vacation time records
- Sanctions tracking

## Technologies Used

- **Blazor WebAssembly** - Frontend framework
- **ASP.NET Core** - Backend API
- **Entity Framework Core** - Database management
- **Microsoft SQL Server** (currently hosted on a local machine, next step: migrate to Azure SQL Database)

## Installation

1. Clone the repository:
2. Navigate to the project directory:
   ```sh
   cd StaffTrackApp
   ```
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Update the database:
   ```sh
   dotnet ef database update
   ```
5. Run the application:
   ```sh
   dotnet run
   ```

## Configuration

- Update `appsettings.json` under `StaffTrackApp.Server` with the correct database connection string

## Usage

1. Register a new account or log in.
2. Role based access control (Admin and User)
3. Add / Update user profile image.
4. Create and manage departments.
5. Add employees and assign them to departments.
6. Track vacation records.
7. Record and track sanctions.
8. Printing feature and Export to PDF or Excel.
9. 

## Future Enhancements

- Idempotency
- Vacation request and approval
- Host the web appliation
- etc



