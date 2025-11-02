# aqa-fullstack

**C# automation suite** — API, UI, DB testing with **Integration + Unit + Smoke** coverage.

**Clean architecture**, **OOP**, **POM**, **DRY**, **DTOs**, **Folder-based namespaces**.

**Tech Stack:** .NET 9, C# 12, NUnit, Selenium, RestSharp, Moq, MySQL, Json.

**Fully portable** — clone & run. Built in **VS Code**.

---

## Project Structure
- aqa-fullstack/
- ├── ApiTests/        → RestSharp + Unit Mocks (reqres.in)
- ├── DbTests/         → MySql.Data + Unit Mocks (db4free.net)
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