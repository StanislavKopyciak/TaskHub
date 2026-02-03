# TaskHub

TaskHub — це навчальний проєкт для керування завданнями (tasks), реалізований на платформі **.NET**.

Проєкт дозволяє створювати, переглядати, редагувати та відмічати завдання як виконані або невиконані для конкретного користувача.

---

## 🚀 Основний функціонал

- Авторизація користувачів
- Створення та редагування завдань
- Перегляд списку завдань
- Фільтрація завдань за статусом (Completed / NotCompleted)
- Відмітка завдання як виконаного
- Розділення логіки за шарами (Clean Architecture)

---

## 🧱 Архітектура

Проєкт побудований з використанням принципів **Clean Architecture**:

- **TaskHub.Core**  
  Доменно-орієнтований шар: сутності, enum-и, інтерфейси

- **TaskHub.Application**  
  Бізнес-логіка, CQRS (Commands / Queries), Handlers, Validators, DTO

- **TaskHub.Infrastructure**  
  Робота з базою даних, Entity Framework Core, репозиторії

- **TaskHub.MVC**  
  ASP.NET Core MVC, контролери та Razor Views

---

## 🛠 Використані технології

- **C# / .NET**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **CQRS (Command / Query Responsibility Segregation)**
- **MediatR**
- **FluentValidation**
- **AutoMapper**
- **SQL Server**
- **Git & GitHub**

---

## 📦 Основні патерни та підходи

- CQRS (Commands / Queries)
- Repository Pattern
- Dependency Injection
- Separation of Concerns
- Fluent Validation для валідації даних
- MediatR як реалізація Mediator Pattern

---

## 🧪 Статус проєкту

Проєкт знаходиться у стадії активної розробки та використовується з навчальною метою для закріплення навичок роботи з:
- архітектурою .NET застосунків
- патернами проєктування
- роботою з базою даних
- Git та GitHub

---

## 📄 Автор

**Stanislav Kopyciak**

