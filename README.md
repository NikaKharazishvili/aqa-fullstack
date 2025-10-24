# aqa-fullstack

This is a **fullstack QA automation framework** built in C# to demonstrate professional software testing practices.
It covers **UI**, **API**, and **DB** layers using **OOP** and **POM** for clean, scalable, and maintainable code.

**The project follows**:
- **clean architecture principles**
- **folder-based namespaces**
- **DRY code**
- **cross-project code reuse** via a dedicated `Shared` class library containing reusable utilities (`Print`, `Wait`) and base test logic

**Packages used**:
- **NUnit** – test runner and assertions
- **Selenium WebDriver** – browser automation on `something.com` (UITests)
- **RestSharp** – fluent REST API testing on `reqres.in` (APITests)
- **MySql.Data** – direct SQL validation on `db4free.net` (DBTests)

**Fully portable** — No setup. Works anywhere. Clone and run.