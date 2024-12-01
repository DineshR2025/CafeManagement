# CafeManagement
-- Steps to Run the Application
-- Database Setup
1. Clone the repository from GitHub.
2. Database: Ensure you are using SQL Server version 2016 or later.
3. Open the SQL Query window and execute the following files in order:
   DB_Script.sql: To set up the database structure.
   Sample_Data.sql: To seed the database with sample data.

--Backend Setup
1. Navigate to the CafeManagement_Backend folder and open the file CafeManagement.sln in Visual Studio.
2. Install .NET Core Runtime 8 if not already installed.
3. Run the project CafeManagement.WebApi.
4. Confirm that the Swagger window opens successfully.

--Frontend Setup

1. Install Node.js version v22.11.0 if not already installed.
2. Open the CafeManagement_Frontend folder in Visual Studio Code.
3. Open the terminal, switch to Vite CLI, and run the following commands:
   npm install: To install the required dependencies.
   npm run dev: To start the development server.
4.The application will launch locally at: http://localhost:5173/