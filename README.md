# EmloyeeManagementSystem

## Connection String Information

The application uses a SQL Server database to store its data. Below is the connection string used to connect to the database:
Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
Replace myServerAddress, myDataBase, myUsername, and myPassword with your actual database server address, database name, username, and password, respectively.
in my case i have connection string as
Server=JACKIEE420\MSSQLSERVER01;Database=EmloyeeManagementSystemDB3;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;

## Adding a Migration

To add a migration, run the following command in the Package Manager Console

add-migration migrationName

Replace migrationName with a descriptive name for your migration.

Updating the Database
After adding a migration, you need to update the database to apply the changes. Run the following command in the Package Manager Console

update-database

This command will apply any pending migrations to the database.


## User and Role Setup

During application startup, roles and users are initialized to ensure proper functioning of the system.
These Users will be generated automatically.

Admin User:
Username: admin@admin.com
Password: Admin@123
Role: Admin
Name: adminFirst adminLast

Employee User:
Username: employee@employee.com
Password: Employee@123
Role: Employee
Name: employeeFirst employeeLast

Using the login information above, you may log in.

These users and roles are essential for the proper functioning of the application. Ensure that they are set up correctly for your environment.
you can add new users with register page.
new user will automatically add as Employee User.
