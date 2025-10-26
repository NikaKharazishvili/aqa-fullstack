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
dotnet test                                 # All tests
dotnet test --filter "Category=Unit"        # Unit only
dotnet test --filter "Category=Integration" # Integration only
```