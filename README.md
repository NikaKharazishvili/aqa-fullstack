# aqa-fullstack

**C# automation suite** — API, UI, DB testing with **Integration + Unit + Smoke** coverage.
**Clean architecture**, **OOP**, **POM**, **DRY**.
**Fully portable** — clone & run. Built in **VS Code**.
**Tech Stack:** .NET 9, C# 12, NUnit, Selenium, RestSharp, Moq, MySQL, Json.

---

## Features
- **Integration Tests** → Real API, browser, DB
- **Unit Tests (mocked JSON)** → Fast, offline, validate logic
- **Smoke Tests** → "Is API alive?"
- **Folder-based namespaces** → Scalable
- **DTOs + Constants** → Type-safe, clean

---

## Project Structure
- aqa-fullstack/
- ├── ApiTests/        → RestSharp + unit mocks (reqres.in)
- ├── DbTests/         → MySql.Data + unit mocks (db4free.net)
- ├── UiTests/         → Selenium (practice-automation.com)
- └── Shared/Utils.cs

---

## Run Tests
```bash
# Warning: It is recommended to run Unit and Integration tests separately
# to avoid occasional unit test failures due to timing conflicts
dotnet test --filter "Category=Integration"  # Integration tests only (All three testings have it)
dotnet test --filter "Category=Unit"         # Unit tests only (API and DB have it)
dotnet test --filter "Category=Smoke"        # Smoke tests only (All three testings have it)

dotnet test --filter "Category=Ui"           # UI only (Integration)
dotnet test --filter "Category=Api"          # API only (Unit + Integration)
dotnet test --filter "Category=Db"           # DB only (Unit + Integration)
```