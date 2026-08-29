# Setup Commands

All commands used to set up the Fairbnb project.

## .NET SDK
```bash
# Check installed version
dotnet --version
```

## Backend (from Fairbnb/ root)
```bash
# Create Web API project
dotnet new webapi -n Fairbnb.Api -o backend/Fairbnb.Api

# Create test project
dotnet new xunit -n Fairbnb.Api.Tests -o backend/Fairbnb.Api.Tests

# Link test project to API project (from backend/Fairbnb.Api.Tests/)
dotnet add reference ../Fairbnb.Api/Fairbnb.Api.csproj

# Install EF Core packages (from backend/Fairbnb.Api/)
# We originally planned to use SQL Server, but SQL Server has no native Mac version
# (it requires Docker). Since we already had PostgreSQL installed via Homebrew, we
# switched to PostgreSQL instead. The EF Core setup is almost identical.
# dotnet add package Microsoft.EntityFrameworkCore.SqlServer   # <-- original plan
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL        # <-- what we use
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Install EF Core CLI tool (global, one-time)
dotnet tool install --global dotnet-ef
```

## EF Core Migrations (from backend/Fairbnb.Api/)
```bash
# Create a migration
dotnet ef migrations add <MigrationName>

# Apply migration to the database
dotnet ef database update

# Undo last migration (if not yet applied)
dotnet ef migrations remove
```

## Frontend (from frontend/)
```bash
# Create React + Vite project
npm create vite@latest . -- --template react

# Install dependencies
npm install
```

## Run commands
```bash
# Run backend (from backend/Fairbnb.Api/)
dotnet run

# Run frontend (from frontend/)
npm run dev

# Run tests (from backend/Fairbnb.Api.Tests/)
dotnet test
```

## PostgreSQL
```bash
# Start PostgreSQL (one-time, then runs automatically on boot)
brew services start postgresql@14

# Create the database (one-time)
psql -U $(whoami) -d postgres -c "CREATE DATABASE fairbnb;"
```

## Shell setup
```bash
# Add dotnet to PATH (one-time)
echo 'export PATH="$PATH:/usr/local/share/dotnet"' >> ~/.zshrc
source ~/.zshrc
```
