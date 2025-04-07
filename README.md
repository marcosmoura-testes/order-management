
# Order Management

## Overview
This project is an order management system built with .NET 8 and Entity Framework Core. It includes a SQL Server database for storing order, product, and dealer information.

## Prerequisites
- Docker
- .NET 8 SDK

## Getting Started

### Step 1: Set up Docker
1. Ensure Docker is installed and running on your machine.
2. Navigate to the **root project directory**.
3. Run the following command to start the SQL Server container:
   ```bash
   docker-compose up -d
   ```
4. Verify that the SQL Server container is running.

### Step 2: Set up the Database
1. Create the initial migration:
   ```bash
   dotnet ef migrations add InitialCreate --project src/Infrastructure --startup-project src/WebAPI
   ```
2. Update the database to the latest version:
   ```bash
   dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
   ```

### Step 3: Run the Application
1. Open the solution in Visual Studio or your preferred IDE.
2. Set `WebAPI` as the startup project.
3. Run the application.

### Step 4: Access the API
1. Open your browser and navigate to:
   ```
   http://localhost:5067
   ```
   to access the Swagger UI and explore the API endpoints.

## Project Structure

The solution is organized into the following layers:

### Core
- `src/Application`: Contains the use cases and business rules.
- `src/Domain`: Contains entity classes and interfaces.
- `src/Infrastructure`: Contains the data access layer, including Entity Framework Core setup and repositories.

### Presentation
- `src/WebAPI`: Contains the Web API project, including controllers and middleware.

### Services
- `src/ProcessSupplyOrders`: Contains the service for processing supply orders.
- `src/RequestSupplyOrders`: Contains the service for sending supply orders from dealer requests.

### Solution Items
- `docker-compose.yml`: Docker Compose file for setting up required services.
- `README.md`: Documentation for the solution.

## Configuration
- The database connection string is configured in `appsettings.json` and can be overridden using environment variables.

## License
This project is licensed under the MIT License.
