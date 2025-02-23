### **TrackIt.Tests** — Unit и Integration тесты
Проект для тестирования всего приложения: юнит-тесты для сервисов и интеграционные тесты для репозиториев и API.

- **Application.Tests**:
    - Тесты для сервисов, обработчиков команд/запросов.
    - Пример: тесты для `UserService`, `TransactionService`.

- **Infrastructure.Tests**:
    - Тесты для репозиториев, миграций, интеграции с базой данных.
    - Пример: тесты для `UserRepository`, `TransactionRepository`.

- **API.Tests**:
    - Тесты для контроллеров и взаимодействия с API.
    - Пример: тесты для `UserController`, `TransactionController`.

**Примечание:** Тесты в **Infrastructure.Tests** и **Application.Tests** должны изолировать компоненты друг от друга, тестируя отдельные слои приложения.
