Adesso World League – Group Draw API

This project is a solution for the Adesso .NET Code Challenge, implementing a complete group-draw system for a fictional Adesso World League tournament.
The goal is to distribute 32 teams from 8 countries into 4 or 8 groups, following strict draw rules similar to UEFA-style group draws.

📌 Tech Stack

.NET 8 Web API

Entity Framework Core

Clean Architecture

Domain-Driven Design (DDD) principles

SQL Server (EF Migrations)

Dependency Injection / Layered Architecture

🚀 Challenge Requirements & How They Were Implemented
✔ 1. Must be a .NET Core Web API

→ The project contains a clean API layer exposing /api/draw.

✔ 2. Exactly 32 teams from 8 countries (4 teams each)

→ Seed data is added in the Infrastructure layer using Fluent API configurations.

✔ 3. Group Count must be 4 or 8

→ Validated in the controller and service level.

4 groups → 8 teams per group

8 groups → 4 teams per group

✔ 4. A group cannot contain more than one team from the same country

Example:
If Group A contains "Adesso Istanbul (Turkey)",
Group A cannot select another Turkish team.

→ Implemented using per-group country tracking (usedCountriesPerGroup).

✔ 5. A team can belong to only one group

If "Adesso Istanbul" is placed into Group A,
it must not appear in Groups B, C, D...

→ Implemented using global team pool logic.
Selected teams are removed from the global pool.

✔ 6. Draw must be ROUND-BASED (UEFA-style)

Round 1:
A → 1st team
B → 1st team
C → 1st team
D → 1st team

Round 2:
A → 2nd team
B → 2nd team
C → 2nd team
D → 2nd team
... and so on.

→ Implemented with nested loops (round then group).

✔ 7. Drawer name must be provided

→ DrawRequestDto.DrawerName
→ Saved as Draw.DrawerName in DB.

✔ 8. Result of the draw must be saved into the database

The following entities are persisted:

Draw

Groups

GroupTeams

EF Core automatically inserts the full object graph.

✔ 9. API returns group results

Response structure matches the challenge’s expectation:

{
  "drawer": "Aleyna Selek",
  "date": "2025-11-16T10:00:00",
  "groups": [
    {
      "groupName": "A",
      "teams": [
        { "id": 1, "name": "Adesso Istanbul", "countryName": "Turkey" },
        ...
      ]
    }
  ]
}

🧠 Core Draw Algorithm Overview

The draw logic follows these principles:

🔹 Global Team Pool

Teams are removed once selected → prevents duplicates.

🔹 Per-Group Country Filter

A group cannot select a second team from the same country.

🔹 Round-Based Selection

Ensures distribution order exactly as described in the challenge.

🔹 Randomized Draw

Randomness is handled via single Random instance.

🔹 Guaranteed Equal Distribution

32 / groupCount → determines exact number of teams per group.

📂 Project Architecture (Clean Architecture)
AdessoWorldLeague/
│
├── Api/                     # Web API Layer
│   ├── Controllers/
│   ├── Program.cs
│   └── appsettings.json
│
├── Application/             # Business Rules / DTOs / Services
│   ├── DTOs/
│   └── Services/
│
├── Domain/                  # Entities (Country, Team, Group, Draw...)
│
├── Infrastructure/          # EF DbContext, Migrations, Seed Data
│   ├── AdessoWorldLeagueDbContext.cs
│   ├── Configurations/
│   └── Migrations/
│
└── README.md

🛠 How to Run
1. Restore packages
dotnet restore

2. Apply migrations
dotnet ef database update --project Infrastructure --startup-project Api

3. Start API
dotnet run --project Api


API will be available at:

👉 http://localhost:5000/api/draw

👉 Swagger enabled by default

📬 API Endpoint
POST /api/draw
Request
{
  "groupCount": 4,
  "drawerName": "Aleyna Selek"
}

Response

Returns all groups with drawn teams.

🧪 Testing the Draw Logic

The algorithm is 100% deterministic according to constraints:

No duplicate teams across groups

No duplicate countries inside a group

Correct round order

Correct group sizes

Optional test extensions:

IRandomProvider for deterministic tests

Unit tests for country constraints

Tests for distribution correctness

🏁 Conclusion

This project delivers:

✔ A clean Web API
✔ A fully correct UEFA-style draw algorithm
✔ Complete database persistence
✔ Clean architecture and DDD structuring
✔ Professional-level code organization
✔ Clear response contract
