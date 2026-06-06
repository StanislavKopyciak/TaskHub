# TaskHub

TaskHub — це навчальний проєкт для керування завданнями (tasks), реалізований на платформі **.NET**.

Проєкт дозволяє створювати, переглядати, редагувати та відмічати завдання як виконані або невиконані для конкретного користувача.

---

## 🚀 Основний функціонал

### Авторизація та безпека

* Реєстрація користувачів
* Вхід у систему
* Верифікація Email через Gmail API
* Повторна відправка коду підтвердження Email
* JWT Authentication
* Refresh Token Authentication
* Автоматичне оновлення Access Token через Middleware
* Зберігання токенів у HttpOnly Cookies

### Керування завданнями

* Створення завдань
* Редагування завдань
* Видалення завдань
* Перегляд списку завдань
* Фільтрація завдань за статусом (Completed / NotCompleted)
* Відмітка завдання як виконаного або невиконаного

### Архітектура

* Розділення логіки за принципами Clean Architecture
* CQRS для обробки команд та запитів
* Валідація даних через FluentValidation
* Dependency Injection

---

## 🧱 Архітектура

Проєкт побудований з використанням принципів **Clean Architecture**:

### TaskHub.Core

Доменно-орієнтований шар:

* Entities
* Enums
* Domain Models

### TaskHub.Application

Шар бізнес-логіки:

* Commands & Queries
* Handlers
* DTO
* Validators
* Interfaces

### TaskHub.Infrastructure

Інфраструктурний шар:

* Entity Framework Core
* SQL Server
* Репозиторії
* Email Service (Gmail API)
* JWT & Refresh Token Services

### TaskHub.MVC

Презентаційний шар:

* ASP.NET Core MVC
* Controllers
* Razor Views
* Middleware
* Cookie Management

---

## 🛠 Використані технології

* C#
* .NET
* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* MediatR
* AutoMapper
* FluentValidation
* JWT Authentication
* Gmail API
* Cookie Authentication
* LINQ
* Git & GitHub

---

## 📦 Основні патерни та підходи

* Clean Architecture
* CQRS (Command / Query Responsibility Segregation)
* Repository Pattern
* Mediator Pattern (MediatR)
* Dependency Injection
* Middleware Pipeline
* Fluent Validation
* Separation of Concerns

---

## 🔐 Реалізовані механізми безпеки

* JWT Access Tokens
* Refresh Tokens
* Email Verification
* HttpOnly Cookies
* Automatic Token Refresh
* Password Validation
* Access Control для користувацьких даних

---

## 🧪 Статус проєкту

Проєкт знаходиться у стадії активної розробки та використовується з навчальною метою для закріплення навичок роботи з:

* архітектурою .NET застосунків
* патернами проєктування
* ASP.NET Core MVC
* Entity Framework Core
* Authentication & Authorization
* роботою з базою даних
* Git та GitHub

---

## 📄 Автор

**Stanislav Kopyciak**
