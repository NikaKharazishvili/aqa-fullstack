# aqa-fullstack

**C# automation suite** — API, UI, DB testing with **Integration + Unit + Smoke** coverage.

**Clean architecture**, **OOP**, **POM**, **DRY**, **DTOs**, **Folder-based namespaces**.

**Tech Stack:** .NET 9, C# 12, NUnit, Selenium, RestSharp, MySQL, Moq, Json.

**Fully portable** — clone & run, no extra setup required. Built in **VS Code**.

---

## Project Structure
- aqa-fullstack/
- ├── ApiTests/        → RestSharp + Unit Mocks (reqres.in)
- ├── DbTests/         → Sqlite (local SQL file)
- ├── UiTests/         → Selenium (practice-automation.com)
- └── Shared/Utils.cs

---

## Run Tests
```bash
# Warning: It is recommended to run Unit and Integration tests separately
# to avoid occasional unit test failures due to timing conflicts
dotnet test --filter "Category=Integration"  # Integration tests only (All three testings have it)
dotnet test --filter "Category=Smoke"        # Smoke tests only (Only Ui and Api testings have it)
dotnet test --filter "Category=Unit"         # Unit tests only (Only Api testings have it)

dotnet test --filter "Category=Ui"           # UI only (Integration + Smoke)
dotnet test --filter "Category=Api"          # API only (Integration + Smoke + Unit)
dotnet test --filter "Category=Db"           # DB only (Integration + Smoke)
```