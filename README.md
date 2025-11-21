# C# Full-Stack Test Automation Suite**

**API • UI • DB** tests with **Integration • Unit • Smoke** coverage

![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?logo=.net)  
![C# 12](https://img.shields.io/badge/C%23-12-239120?logo=csharp)  
![NUnit](https://img.shields.io/badge/NUnit-4.2-006400)  
![Selenium · RestSharp · SQLite](https://img.shields.io/badge/Tools-Selenium%20%7C%20RestSharp%20%7C%20SQLite(MySQL example included but disabled)-000000)

---

## Key Features:
- Clean architecture (interface-based clients, DTOs, POM)
- Parallel execution (all fixtures + safe UI concurrency via `[LevelOfParallelism(4)]`)
- Zero setup (WebDriverManager auto-downloads drivers, in-memory DB, embedded JSON fixtures)
- Fully portable (clone & run)

---

## Project Structure:
- aqa-fullstack/
- ├── ApiTests/        → RestSharp + full CRUD/auth + Moq unit tests (reqres.in)
- ├── DbTests/         → In-memory SQLite tests + disabled real MySQL example
- ├── UiTests/         → Selenium + POM + config-driven data (practice-automation.com)
- └── Shared/Utils.cs

---

## Run Tests
```bash
dotnet test                                  # Everything in parallel
dotnet test --filter "Category=Integration"  # Real API + DB + UI calls
dotnet test --filter "Category=Unit"         # Pure API unit(Moq) tests
dotnet test --filter "Category=Smoke"        # Quick API health check
dotnet test --filter "Category=Api|Ui|Db"    # By layer
```