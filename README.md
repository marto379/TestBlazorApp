Blazor Item Management Project

Overview:
This project is a Blazor web application that allows users to manage items by adding, editing, and deleting them. The application uses Dapper as an ORM (Object-Relational Mapper) to interact with a Microsoft SQL Server (MSSQL) database.

Configuration
AppSettings
Open appsettings.json and configure your database connection string:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
  }
}

Project Structure:
Page: Contains the Blazor components for the UI.
Services: Contains the business logic and interacts with the repository.
Repositories: Contains the Dapper implementation for database operations.
Models: Contains the data models representing the database entities.

Usage:
Adding Items
Navigate to the "Add Item" page, fill in the form, and submit to add a new item.

Editing Items
Navigate to the "Items" page, select an item to edit, modify the details, and save changes.

Deleting Items
Navigate to the "Items" page, select an item to delete, and confirm the deletion.
