# Gaia Project - Backend API 🚀

This is the backend server for the Gaia Project, developed as a home assignment for a full-stack developer position. It is a robust, dynamic calculator API built with **.NET Core (C#)** and **Entity Framework Core**, utilizing a SQL Server database.

🔗 **Frontend Repository:** https://github.com/AvivHalevy1/GaiaProject/tree/main/gaia-client

## ✨ Key Features
* **Dynamic Operation Factory:** Implements the Open/Closed Principle. The API scans the assembly via Reflection to load operations dynamically.
*  Adding a new math operation requires zero changes to the core controller or factory logic.
* **Database Integration (Bonus Achieved):** Uses Entity Framework Core to save execution history to a SQL Server database.
* **Advanced Statistics:** Retrieves the last 3 operations and the current monthly execution count per operation type.
* **Global Exception Handling:** Utilizes custom Middleware for clean, centralized error handling without polluting the controllers.
* **Async/Await:** Fully asynchronous database operations to ensure high performance and prevent thread-blocking.

## 🛠️ Tech Stack
* **Framework:** .NET (C#)
* **Database:** MSSQL / SQL Server Express / Developer
* **ORM:** Entity Framework Core

## 🚀 Getting Started

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download)
* SQL Server Express (or any local SQL Server instance)

### Installation & Setup
1. Clone the repository:
   ```bash
   git clone [Your-Backend-Repo-URL]
