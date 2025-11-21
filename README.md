# C# Full-Stack Test Automation Suite*

**C# Full-Stack Test Automation Suite** - **API • UI • DB** testings with **Integration • Unit • Smoke** coverage

**Key Features**
- Tech Stack: .NET 9, C# 12, NUnit, Selenium, RestSharp, Moq, Newtonsoft.Json, SQLite(MySQL example included but disabled)
- Clean architecture (interface-based clients, DTOs, POM)
- Parallel execution (all fixtures + safe UI concurrency via `[LevelOfParallelism(4)]`)
- Zero setup (`WebDriverManager` auto-downloads drivers, in-memory DB, embedded JSON fixtures)
- Fully portable (clone & run)

---
## Project Structure
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