# C# Full-Stack Test Automation Suite

**API • UI • DB** tests with **Integration • Unit • Smoke** coverage

![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?logo=.net)
![C# 12](https://img.shields.io/badge/C%23-12-239120?logo=csharp)
![NUnit](https://img.shields.io/badge/NUnit-4.2-006400)
![Tools](https://img.shields.io/badge/Tools-Selenium%20·%20RestSharp%20·%20Moq%20·%20Newtonsoft.Json%20·%20SQLite/MySQL-000000)

---

## Key Features:
- Clean architecture (interface-based clients, DTOs, POM)
- Parallel execution (all fixtures + safe UI concurrency via `[LevelOfParallelism(4)]`)
- Cross-platform compatible (auto-downloads drivers, in-memory DB, embedded JSON fixtures)
- Zero setup & CI/CD-ready (true clone & run — no external dependencies)

---

## Project Structure:
- **ApiTests/**: RestSharp clients for `reqres.in` API. Covers full CRUD, auth, delays. Unit tests use Moq + embedded JSON mocks for isolation
- **DbTests/**: Data integrity tests on a sample "game_accounts" DB using in-memory SQLite (default) + optional real MySQL (ignored by default)
- **UiTests/**: Selenium POM for `practice-automation.com`. Config-driven (embedded appsettings.json) for browser/headless/data
- **Shared/**: Common utils

---

## Run Tests
```bash
dotnet test                                  # Everything in parallel
dotnet test --filter "Category=Integration"  # UI + API + DB calls
dotnet test --filter "Category=Unit"         # Pure API unit(Moq) tests
dotnet test --filter "Category=Smoke"        # Quick API health check
dotnet test --filter "Category=Api|Ui|Db"    # By layer
```