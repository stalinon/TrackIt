### **TrackIt.API** — ASP.NET Core Web API (входная точка)
Проект **API** будет отвечать за взаимодействие с пользователем через HTTP-запросы. Это будет единственный публичный интерфейс, через который внешние клиенты будут взаимодействовать с системой.

- **Controllers**:
    - Контроллеры, которые обрабатывают HTTP-запросы и делегируют их в бизнес-логику.
    - Пример: `UserController`, `TransactionController`, `CategoryController`.

- **Middleware**:
    - Паттерны, связанные с обработкой запросов, такие как аутентификация, логирование и обработка ошибок.
    - Пример: `ErrorHandlingMiddleware`, `AuthenticationMiddleware`.

- **Extensions**:
    - Местоположение для расширений конфигурации, таких как авторизация, настройка Swagger и другие вспомогательные функции.
    - Пример: `AuthExtension`, `SwaggerExtension`.

- **Program.cs**:
    - Главная точка для настройки сервиса и DI-контейнера.
    - Здесь настраиваются все зависимости, такие как сервисы из **Application** и **Infrastructure**.

**Примечание:** Контроллеры в **API** должны использовать сервисы из **Application** для бизнес-логики. Репозитории и работа с базой данных должны быть скрыты в **Infrastructure**.

```
TrackIt.sln
│
├── 📂 TrackIt.Application  # Бизнес-логика (CQRS, Use Cases, DTO)
│   ├── Features
│   ├── Interfaces
│   ├── DTOs
│   ├── Common
│   ├── DependencyInjection.cs
│
├── 📂 TrackIt.Domain  # Сущности и абстракции
│   ├── Entities
│   ├── Enums
│   ├── Common
│
├── 📂 TrackIt.Infrastructure  # Работа с БД (PostgreSQL), провайдеры, сервисы
│   ├── Persistence (EF Core + миграции)
│   ├── Repositories (Unit of Work + Repository)
│   ├── Services (Email, Telegram)
│   ├── Configurations (Настройки)
│
├── 📂 TrackIt.API  # Входная точка (ASP.NET Core Web API)
│   ├── Controllers
│   ├── Middleware
│   ├── Extensions (Auth, Swagger)
│   ├── Program.cs
│
├── 📂 TrackIt.TelegramBot  # Бот для уведомлений
│   ├── Handlers
│   ├── Services
│   ├── BotStartup.cs
```