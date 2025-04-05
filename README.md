# Order Management

## Overview
This project is an order management system built with .NET 8 and Entity Framework Core. It includes a SQL Server database for storing order, product, and dealer information.

## Prerequisites
- Docker
- .NET 8 SDK

## Getting Started

### Step 1: Set up the Database
1. Ensure Docker is installed and running on your machine.
2. Navigate to the project directory  and executing this comand `docker-compoise -d` e aguarde o container subir
3. Verify that the SQL Server container is running by executing:
4. Once the container is up and running, connect to the SQL Server instance using your preferred SQL client (e.g., SQL Server Management Studio, Azure Data Studio).
5. Execute the SQL script located in `Cria e Carrega Banco.sql` to create and populate the database.

### Step 2: Set up the Application
1. Ensure you have the .NET 8 SDK installed.
2. Navigate to the `src/WebAPI` directory.
3. Run the application using the following command:
### Step 3: Access the API
1. Open your browser and navigate to `http://localhost:5000/swagger` to access the Swagger UI and explore the API endpoints.

## Project Structure
- `src/Infrastructure`: Contains the data access layer, including the Entity Framework Core setup and repositories.
- `src/WebAPI`: Contains the web API project, including controllers and middleware.

## Configuration
- The database connection string is configured in `appsettings.json` and can be overridden using environment variables.

## Docker Compose
- The `docker-compose.yml` file sets up a SQL Server container for the project.

## License
This project is licensed under the MIT License.
# Order Management

## Overview
This project is an order management system built with .NET 8 and Entity Framework Core. It includes a SQL Server database for storing order, product, and dealer information.

## Prerequisites
- Docker
- .NET 8 SDK

## Getting Started

### Step 1: Set up the Database
1. Ensure Docker is installed and running on your machine.
2. Navigate to the project directory and run the following command to start the SQL Server container:
3. Verify that the SQL Server container is running by executing:
4. Once the container is up and running, connect to the SQL Server instance using your preferred SQL client (e.g., SQL Server Management Studio, Azure Data Studio).
5. Execute the SQL script located in `CreateAndLoadDatabase.sql` to create and populate the database.

### Step 2: Set up the Application
1. Ensure you have the .NET 8 SDK installed.
2. Navigate to the Solucion directory.
3. Open the Solution using Visual Studio or Another IDE:
### Step 3: Access the API
1. Open your browser and navigate to `http://localhost:5000/swagger` to access the Swagger UI and explore the API endpoints.

## Project Structure
- `src/Application`: Contains the Use Case with the business rules 
- `src/Domin`: Contains the Entity class and interfaces
- `src/Infrastructure`: Contains the data access layer, including the Entity Framework Core setup and repositories.
- `src/WebAPI`: Contains the web API project, including controllers and middleware.

## Configuration
- The database connection string is configured in `appsettings.json` and can be overridden using environment variables.

## Docker Compose
- The `docker-compose.yml` file sets up a SQL Server container for the project.

## License
This project is licensed under the MIT License.
