# PAccountant (Personal Accountant)

##  Description
**PAccountant** is a comprehensive personal finance management web application designed to help individuals effectively track, manage, and analyze their financial activities. It provides a robust set of tools for monitoring daily transactions, organizing budgets, recording assets, tracking liabilities, and managing loans with their respective repayment schedules.

This application is built with a focus on maintainability, scalability, and code clarity, making it easier for developers to understand and extend its capabilities over time.

---

## Features
1. **User Accounts Management**: Keep track of various financial accounts (e.g., Bank, Cash, Savings).
2. **Transaction Tracking**: Log daily income and expenses categorized systematically.
3. **Budgeting System**: Plan financial allocations across different transaction categories on a monthly/yearly basis.
4. **Wealth Management**:
   - **Assets**: Record and monitor the value of assets you own.
   - **Liabilities**: Track debts and financial obligations.
5. **Loan Management**: Setup and monitor loans, along with their granular repayment schedules.

---

## Tools Used
The project utilizes modern frameworks and robust libraries to deliver a seamless developer and user experience:

- **.NET 10**: The core framework providing high performance and cross-platform compatibility.
- **C# 13**: The primary programming language utilized across the entire application stack.
- **Blazor (Web Project)**: A modern web UI framework used to build interactive and responsive user interfaces natively in C#.
- **MudBlazor (v9.0.0)**: A powerful Material Design component library used for rapidly building beautiful, responsive Blazor interfaces.
- **Entity Framework Core (v10.0.3)**: A lightweight, extensible, and cross-platform Object-Relational Mapper (ORM) used for secure and easy database interactions.
- **SQL Server**: The primary relational database used to store users' financial data reliably.

---

## Architecture & Workflow
PAccountant is structured using the **Clean Architecture** pattern to achieve separation of concerns, making the system highly testable and independent of the UI and databases.

### Project Structure Breakdown:

#### 1. `Core.Domain` (The Core)
This is the heart of the application. It contains all the enterprise business rules and **Entities** such as `Account`, `Budget`, `Asset`, `Liability`, `Loan`, `Transaction`, `User`, etc.
* **Workflow**: No dependencies on external layers. It represents purely "What" the business logic is.

#### 2. `Core.Application` (Use Cases)
This layer contains the application-specific business rules. It defines the interfaces, commands, queries, and services that act upon the Core Entities.
* **Workflow**: Depends only on the Domain. It orchestrates the flow of data to and from the entities.

#### 3. `Infrastructure` (Data & External Services)
This layer implements the interfaces defined in the Application layer. It handles all data persistence concerns.
* **Workflow**: Contains the Entity Framework Core `DbContext` configurations and migrations. It manages communication with the SQL Server database.

#### 4. `Web` (Presentation Layer)
This is the topmost layer where the user interacts with the application.
* **Workflow**: Contains the Blazor components, Pages, and MudBlazor integrations. It strictly calls services defined in the Application layer rather than directly talking to the database, ensuring clean UI decoupling.

### How Data Flows (Example: Creating a Transaction):
1. **User Action**: The user inputs transaction details on a Web UI Blazor page and clicks "Submit".
2. **Web Layer**: Gathers the input and passes it down to an Application use-case/service.
3. **Application Layer**: Receives the command, applies any business logic, maps it to a `Transaction` Domain Entity, and uses a repository abstraction.
4. **Infrastructure Layer**: The repository implementation translates the domain request into an EF Core operation and saves it to the SQL Server database.

---

## Getting Started

### Prerequisites:
- .NET 10.0 SDK
- SQL Server (LocalDB or a dedicated instance)

### Installation & Execution:
1. **Clone the repository** to your local machine.
2. **Navigate to the solution folder**:
   ```bash
   cd PAccountant
   ```
3. **Restore Packages**:
   ```bash
   dotnet restore
   ```
4. **Apply Database Migrations** (Ensure your connection string in `appsettings.json` is correctly pointing to your SQL Server instance):
   ```bash
   dotnet ef database update --project Infrastructure --startup-project Web
   ```
5. **Run the Application**:
   Navigate into the `Web` directory and run via CLI:
   ```bash
   cd Web
   dotnet watch
   ```
6. Open your browser and navigate to the localhost URL provided in your terminal (typically `https://localhost:7000` or similar).
