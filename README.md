# BlazorPractice

## Setup
1. Install .NET 6.0 SDK, Docker, and `dotnet-ef` 6.0.36.
2. Run `dotnet restore`.
3. Start PostgreSQL: `docker run --name practiceapp-db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=admin123 -e POSTGRES_DB=practiceapp -p 5432:5432 -d postgres:16`.
4. Apply migrations: `dotnet ef database update`.
5. Run: `dotnet run`.