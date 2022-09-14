# Курсовой проект по курсу 'PostgreSQL'

За основу проекта был взят русскоязычный веб-сайт в формате системы тематических коллективных блогов https://habr.com/
***
### 1. Анализ бизнес-логики
Проанализировав бизнес-логику приложения было выделено 11 таблиц для хранения данных приложения:

1. Публикации - `publications`
2. Хабы (наборов тематических статей) - `hubs`
3. Связи публикаций с хабами (многие ко многим) - `hubs_publications` 
4. Комментарии - `comments`
5. Статусы комментариев - `comment_statuses`
6. Пользователи - `users`
7. Профили пользователей (связь с таблицей 'users' - один к одному) - `profiles`
8. Закладки пользователей - `bookmarks`
9. Изображения (для автарок и обложек статей) - `images`
10. Подписка на уведомления - `notifications`
11. Типы уведомлений - `notification_types`

Код создания таблиц приведен в скрипте `1. Create tables.sql`

### 2. Тестовые данные
Тестовые данные сгенерированы с помощью внешнего сервиса https://generatedata.com/

Для создания дампов использовал утилиту pg_dump: 
```sql
pg_dump hubs > hubs.dump.sql
```
Дамп БД с данными содержится в файле `2.2. Hubs.dump.sql`

### 3. Внешние ключи
Внешние ключи создавались командой `ALTER TABLE <tablename> ADD CONSTRAINT`:
```sql
ALTER TABLE bookmarks
    ADD CONSTRAINT bookmarks_user_id_fk
    FOREIGN KEY (user_id)
    REFERENCES users(id);
```
Полный скрипт создания внешних ключей приведен в файле `3. ForeignKey.sql`
### 3. Диаграмма отношений
Создание диагрыммы отношений осуществлялось с помощью [pgAdmin 4](https://www.pgadmin.org/).
![ERD](https://github.com/orlfi/GeekBrains/blob/Postgresql.Project/10.Postgresql/Project/4.%20ERD.png)