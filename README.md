# aqa-fullstack

**C# automation suite** — API, UI, DB testing with **Unit + Integration** coverage.  
**Clean architecture**, **OOP**, **POM**, **DRY**.
**Tech Stack:** .NET 9, C# 12, NUnit, Selenium, RestSharp, Moq, MySQL.

Fully portable — **clone & run**. Built in **VS Code**.

---

## Features
- **Unit Tests (mocked JSON)** → Fast, offline, validate parsing & business logic; uses pre-saved API responses
- **Integration Tests** → Real API, browser, DB
- **Folder-based namespaces** → Scalable
- **DTOs + Constants** → Type-safe, clean
- **Shared.Utils** → Reusable methods

---

## Project Structure
- aqa-fullstack/
- ├── ApiTests/        → RestSharp + unit mocks (reqres.in)
- ├── DbTests/         → MySql.Data (db4free.net)
- ├── UiTests/         → Selenium (practice-automation.com)
- └── Shared/Utils.cs

---

## Run Tests
```bash
# Warning:** It is recommended to run Unit and Integration tests **separately** to avoid occasional unit test failures due to timing conflicts
dotnet test                                  # All tests
dotnet test --filter "Category=Unit"         # Unit tests only (API and DB have it)
dotnet test --filter "Category=Integration"  # Integration tests only (All three testings have it)

dotnet test --filter "Category=Ui"           # UI only (Integration)
dotnet test --filter "Category=Api"          # API only (Unit + Integration)
dotnet test --filter "Category=Db"           # DB only (Unit + Integration)
```