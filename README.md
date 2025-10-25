# aqa-fullstack

**C# automation suite** — API, UI, DB testing with **Unit + Integration** coverage.  
**Clean architecture**, **OOP**, **POM**, **DRY**, **cross-project reuse**, **real JSON in unit tests**.

Fully portable — **clone & run**. Built in **VS Code** with **C# Dev Kit** + **NuGet Package Manager** extensions.

---

## Features
- **Unit Tests (Moq)** → Fast, offline, 100% reliable
- **Integration Tests** → Real API, browser, DB
- **Folder-based namespaces** → Scalable
- **DTOs + Constants** → Type-safe, clean
- **Shared.Utils** → Reusable methods
- **Real JSON captured** → Unit tests mimic live API

---

## Project Structure
- aqa-fullstack/
- ├── ApiTests/        → RestSharp + Moq (reqres.in)
- ├── DbTests/         → MySql.Data + Moq (db4free.net)
- ├── UiTests/         → Selenium + POM (practice-automation.com)
- └── Shared/          → Utils.cs

---

## Run Tests
```bash
dotnet test                                 # All tests
dotnet test --filter "Category=Unit"        # Unit only
dotnet test --filter "Category=Integration" # Integration only
```