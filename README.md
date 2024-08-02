# WorkforceHub

```markdown

## Project Overview

WorkforceHub is an API designed to manage employee data across different categories of workers: hourly employees,
salaried supervisors, and managers with business expenses. This project demonstrates proficiency in .NET Core APIs,
SQL database interactions, and enterprise-level application structuring.

## Table of Contents

- [Project Setup](#project-setup)
- [Database Schema](#database-schema)
- [API Endpoints](#api-endpoints)
- [Project Folder Structure](#project-folder-structure)
- [Setup and Configuration](#setup-and-configuration)
- [Running the Project](#running-the-project)
- [Testing](#testing)
- [Restoring the Database](#restoring-the-database)
- [Contributing](#contributing)
- [License](#license)

## Project Setup

### Clone the Repository

```bash
git clone https://github.com/Gouthami-10/WorkforceHub.git
cd WorkforceHub
```

### Open in Visual Studio

- Open the `WorkforceHub.sln` solution file in Visual Studio.

### Configure Database

- Update the connection string in `appsettings.json` with your SQL Server details.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=WorkforceHubDB;Trusted_Connection=True;"
  }
}
```

### Install Dependencies

- Ensure all required NuGet packages are installed using the NuGet Package Manager in Visual Studio.

## Database Schema

**Normalization:**
- A single `Person` table is created to normalize the common attributes of employees, supervisors, and managers.
- Individual tables for `Employees`, `Supervisors`, and `Managers` extend the `Person` table, implementing a type hierarchy that is connected via foreign keys to the `PersonID`.

### SQL Scripts for Database Creation

```sql
-- Create Person Table
CREATE TABLE Person (
    PersonID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(30),
    LastName VARCHAR(50),
    Address VARCHAR(100)
);

-- Create Employees Table
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    PayPerHour DECIMAL(5,2),
    FOREIGN KEY (EmployeeID) REFERENCES Person(PersonID)
);

-- Create Supervisors Table
CREATE TABLE Supervisors (
    SupervisorID INT PRIMARY KEY,
    AnnualSalary DECIMAL(10,2),
    FOREIGN KEY (SupervisorID) REFERENCES Person(PersonID)
);

-- Create Managers Table
CREATE TABLE Managers (
    ManagerID INT PRIMARY KEY,
    AnnualSalary DECIMAL(10,2),
    MaxExpenseAmount DECIMAL(10,2),
    FOREIGN KEY (ManagerID) REFERENCES Person(PersonID)
);
```

## API Endpoints

**Base URL:** `/api/employees`

- **GET `/api/employees`**
  - Description: Retrieves a list of all employees, supervisors, and managers.
  - Response: `200 OK` with a JSON array of employee details.

- **POST `/api/employees`**
  - Description: Adds a new employee, supervisor, or manager.
  - Request Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "address": "string",
      "payPerHour": 0, // Only for employees
      "annualSalary": 0, // Only for supervisors and managers
      "maxExpenseAmount": 0, // Only for managers
      "role": "string" // "Employee", "Supervisor", or "Manager"
    }
    ```
  - Response: `200 OK` with a success message.

## Project Folder Structure

```
WorkforceHub/
│
├── WorkforceHub.API/
│   ├── Controllers/
│   ├── Models/
│   └── appsettings.json
│
├── WorkforceHub.BusinessLogic/
│   ├── Interfaces/
│   └── Services/
│
├── WorkforceHub.DataAccess/
│   ├── Entities/
│   └── DbContext.cs
│
├── WorkforceHub.Models/
│   └── DTOs/
│
└── WorkforceHub.Tests/
    └── ControllerTests/
```

## Setup and Configuration

- **Visual Studio 2019 or later**
- **.NET 6.0 SDK**
- **SQL Server 2019 or later**

## Running the Project

1. Open the solution in Visual Studio.
2. Build the solution to restore NuGet packages.
3. Set `WorkforceHub.API` as the startup project.
4. Run the application.

## Testing

- Navigate to `https://localhost:<port>/swagger` to view and test the API endpoints via Swagger UI.
- Run unit tests through the Test Explorer in Visual Studio.

## Restoring the Database

If you need to restore the database from a `.bak` file, follow these steps:

### Using SQL Server Management Studio (SSMS)

1. **Open SSMS and connect to your database server.**
2. **Right-click on 'Databases' and select 'Restore Database...'.**
3. **Select 'Device' and then click the '...' button to browse for the `.bak` file.**
4. **Click 'Add' and navigate to the location of your `.bak` file. Select the file and click 'OK'.**
5. **Back in the Restore Database window, check the 'Restore' box next to the database in the backup set.**
6. **Click 'OK' to start the restoration process.**

### Using SQL Command

Alternatively, you can use the following SQL command to restore the database:

```sql
RESTORE DATABASE YourDatabaseName
FROM DISK = 'Path_to_your_backup_file.bak'
WITH MOVE 'YourDatabaseName_Data' TO 'Path_to_your_data_file.mdf',
MOVE 'YourDatabaseName_Log' TO 'Path_to_your_log_file.ldf',
REPLACE;
```

## Contributing

Please refer to the [GitHub repository](https://github.com/Gouthami-10/WorkforceHub.git) for details on contributing to this project.

## License

This project is licensed under the MIT License.
```

