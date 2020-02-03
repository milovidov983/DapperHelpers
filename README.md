# DapperHelpers

## Введение


### ПРОБЛЕМА

Как однажды сказал один старый DBA, есть два типа проектов: первый тип это на котором пока еще используют ORM и второй, это на котором уже поняли что надо писать чистый SQL потому что ни одна существующая ORM не дает им такой производительности как SQL.

Это одна сторона, а вторая заключается том, что ни одна ORM не реализует все возможности конкретной реализации базы данных.


### РЕШЕНИЕ

Писать чистый SQL внутри проекта или пистать ханимые процедуры. Процедуры это не наш путь, но с SQL внутри кода можно побробовать, но его поддержка будет сказиваться на времени разработки, как бы сделать так что бы SQL в нашем проекте был такой же поддерживаемый и гибкий к рефакторингу как и остальной код? 



## ЦЕЛЬ

Писать SQL внутри кода и не переживать о проблемах связанных с его поддержкой.


Перед вами набор расширений для Dappera, который позволяет безопасно писать SQL внутри вашего проекта, этот SQL будет типизирован так же как любой другой код, и при рефакторинге вам не надо будет искать все использования имени поля во всех SQL запросах, данное расширение сделает все за вас, главное позаботится заранее и все подготовить.


## Быстрый старт


### Инсталяция

#### Nuget

Адрес Nuget пакета
https://www.nuget.org/packages/DapperHelpers

Наиболее простой способ начать использовтаь DapperHelpers устанвоить его через Nuget.

Просто введите следующую команду в консоли диспетчера пакетов в Visual Studio:


```
PM> Install-Package DapperHelpers
```


### Примеры


В следующих примерах будет использоваться class User, определенный как:

```csharp
namespace ExampleProject {
    public class User {
        public const string TableName = "Users";

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
```

Обратите внимание что каждый объект отображаемый на вашу таблицу в базе данных должен содержать в себе поле с названием таблицы, в данном случае это `TableName = "Users"`, это необходимо что бы мы могли обращатся к ней внутри запроса.

Далее определим класс с опсианием нашей таблицы, для удобного обращения к ней из любой части приложения.
Обратите внимание что в качестве базы данных мы используем Postgresql

```csharp
namespace ExampleProject {
    using DapperHelpers;
    using DapperHelpers.Models;
    using Npgsql;
    using System;
    using System.Data;

	public class Database : IDisposable {
        /// Создаем предстваление таблицы из объекта User
		public readonly Table<User> UsersTable = TableExtentions.Create<User>(User.TableName);

		public Database() {
			ActiveConnection = GetConnection();
		}

		public IDbConnection ActiveConnection { get; private set; }

		public IDbConnection GetConnection() {
			return new NpgsqlConnection(Settings.Instance.GetConnectionString());
		}

		public void Dispose() {
			ActiveConnection?.Dispose();
		}
	}
}
```


## Простая операция вставки

```csharp
// Шаг 1. Используем класс который мы определили выше
using var db = new Database();

// Шаг 2. Открываем содение к базе
db.ActiveConnection.Open();

// Шаг 3. Создаем транзакцию
using var tnx = db.ActiveConnection.BeginTransaction();

// Шаг 4. Создаем объект для вставки
var userBob = new User {
    Name = "Bob",
    Email = "bob@example.com",
    RegisteredAt = DateTime.UtcNow
}

// Шаг 5. Используем DapperHelpers для написания SQl запроса
var sql = database.UsersTable
    .Exclude(f=>f.Id) // Так мы исключаем из запроса Id что бы база могла его назначить сама
    .Query(x => $@"
                    insert into {x.Name} 
                    ({x.SelectInsert()})
                    values
                    ({x.Insert()})
                    returning {x.FieldShort(f=>f.Id)} 
                "
);

// Шаг 6. Выполняем запрос
var id = await database.ActiveConnection.QuerySingleAsync<int>(sql, user);

// Шаг 7. закрывем транзакцию
tnx.Commit();

```
**Сгенерированный SQL**

```sql
insert into "Users"
("Name","Email","RegisteredAt")
values
(@Name,@Email,@RegisteredAt)
returning "Id"
```


## Простая операция обновления

*Шаги 1-3 и 7 опущены для простоты*

```csharp
var newUser = new User {
    Id = 1,
    Name = "new name",
    Email = "other@example.com"
};

var sql = database.UsersTable
    .Exclude(f=>f.RegisteredAt)
    .Query(x => $@"
                update {x.Name} 
                set {x.Update()}
                where {x.Field(f=>f.Id)} = @{nameof(User.Id)}
                "
);
await database.ActiveConnection.ExecuteAsync(sql, newUser);
```
