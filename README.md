# BlazorPractice
I created the following App to familiarize myself with creating web apps using Blazor as well as to practice and further my understanding or c#. 

The following requirements were planned:
	1. Tailwind Integration
	2. Page and user authorization and authentication
	3. Database management with Postgres Sql
	4. Basic view of table data
	5. Basic crud operations performed by an external api/controller

This is a learning opportunity so any suggestions are welcome!

Struggles along the way:
	1. Tailwind integration > Had to install the cli using node and add a prefix in tailwind.config to resolve
	2. Controller Authorization > For some reason the identity cookie was not being attached meaning auth alwasy failed. 
		There was also weird behaviour where instead of giving a 302 redirect code the logs retruned a 200 with the redirect page as html content.
		This was resolved by manually adding the cookie to the html context during the request and adjust Program.cs to redirect in stead of return 200.

Future Enhancements:
	1. Clean up code with better coding practices
	2. Clean up logging so it's not so cluttered
	3. Expand UI functionality with more advanced service calls.

## Prerequisites
- .NET 8.0 SDK
- Node.js & npm (v16+)
- Docker (for PostgreSQL)
- Git
- Visual Studio 2022

## Setup
1. Open Project and restore .NET pacakges using 'dotnet restore' in the terminal
2. Ensure docker is installed and start PostgreSQL in docker using the following in the terminal:
	'docker run --name practiceapp-db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=admin123 -e POSTGRES_DB=practiceapp -p 5432:5432 -d postgres:16'
3. Verify the default connection in appsettings.json:
    "DefaultConnection": "Host=localhost;Port=5432;Database=practiceapp;Username=postgres;Password=admin123"
4. Apply Migrations using 'dotnet ef database update' in the terminal
5. Install Tailwind and node dependencies using 'npm install' in the terminal.
	If Css doesn't work correctly on app load open a separate terminal, navigate to the project folder directory and run 'npm run watch-css' and reload the project.
6. Run the App using 'dotnet run' in the terminal or hit the launch button in visual studio.