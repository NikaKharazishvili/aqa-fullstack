# aqa-fullstack

This is a **fullstack QA automation framework** (combination of projects) built in C# to demonstrate professional software testing practices.
It covers **UI**, **API**, and **DB** testing using **OOP** and **POM** for clean, scalable, and maintainable code.

**The project follows**:
- **clean architecture principles**
- **folder-based namespaces**
- **DRY code**
- **cross-project code reuse** via a dedicated `Shared` class library containing reusable utilities

**Packages used**:
- **NUnit** – test runner and assertions
- **Selenium WebDriver** – browser automation on `something.com` (UITests)
- **RestSharp** – fluent REST API testing on `reqres.in` (APITests)
- **MySql.Data** – direct SQL validation on `db4free.net` (DBTests)

**Fully portable** — Clone and run.
Developed in **VS Code** with **C# Dev Kit** and **NuGet Package Manager** extensions.