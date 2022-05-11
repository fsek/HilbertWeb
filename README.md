# HilbertWeb
Test repository for potential new fsek backend

## Versions
.NET 6 (net6.0)

C# 10

PostgreSQL 14

## Setup
The project first requries you to clone the code and restore NuGet packages (this is done automatically in Visual Studio).
Then the database needs to be setup, we use a PostgreSQL database

### Setup Windows/macOS
1. Install Visual Studio (not code, although it works with the linux instructions)
2. `git clone https://github.com/LinderVII/HilbertWeb`
3. Open Solution
4. Build -> Build Solution, it should automatically restore NuGet pacakages

#### Setup Linux
https://code.visualstudio.com/docs/languages/dotnet
1. Install dotnet CLI
2. `git clone https://github.com/LinderVII/HilbertWeb`
3. Open folder in VSCode
4. `dotnet restore`
5. `dotnet run`

### Setup database
1. Download and install PostgreSQL 14
 - Windows: https://www.postgresqltutorial.com/postgresql-getting-started/install-postgresql/
 - macOS: https://www.postgresqltutorial.com/postgresql-getting-started/install-postgresql-macos/
 - Linux: https://www.postgresqltutorial.com/postgresql-getting-started/install-postgresql-linux/
2. When installed, set default password to dablord1337 (we will probably add this to a non-versioned file instead in future so you can set your own pass :))

## TODO, this shouldnt be here
Move viewmodels to other project

move auth to other project
