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

После генерации пришлось поправить данные вручную с помощью скрипта `2.1. Correct fake data.pgsql`

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

### 4. Диаграмма отношений
Создание диагрыммы отношений осуществлялось с помощью [pgAdmin 4](https://www.pgadmin.org/).
![ERD](https://github.com/orlfi/GeekBrains/blob/Postgresql.Project/10.Postgresql/Project/4.%20ERD.png)

### 5. Запросы с использованием подзапросов
5.1. Выбрать пользователей с количеством публикаций больше среднего. Данный запрос использует коррелирующий и некоррелирующий подзапрос.
```sql
WITH users_publications_count AS (
    SELECT author_id, COUNT(*) AS publications_count
    FROM publications
    GROUP BY author_id
)
SELECT 
    (
        SELECT users.name 
            FROM users 
            WHERE users.id = users_publications_count.author_id
    ) AS user_name,
    publications_count, 
    (
        SELECT AVG(publications_count) 
            FROM users_publications_count
    ) AS avg_count
FROM users_publications_count
WHERE publications_count > (
    SELECT AVG(publications_count) 
    FROM users_publications_count
)
ORDER BY publications_count DESC;
```
Результат выполнения запроса:
![Subquery1](https://github.com/orlfi/GeekBrains/blob/Postgresql.Project/10.Postgresql/Project/5.1.%20Result.png)


5.2. Выбрать хабы, у которых имеются комментарии в публикациях, а также пользователей, оставивших максимальное количество комментрариев в этих хабах.
```sql
WITH user_comments_by_hubs AS (
SELECT 
    hub_id,
    user_id,
	count(comments.publication_id) AS comments_count
FROM hubs_publications
JOIN publications 
	ON publications.id = hubs_publications.publication_id
JOIN comments
	ON publications.id= comments.publication_id
GROUP BY hub_id, user_id
)
SELECT DISTINCT
	hubs.name AS hub_name, 
    FIRST_VALUE(users.name) OVER (PARTITION BY hubs.name ORDER BY
        comments_count DESC) AS user_name,
    FIRST_VALUE(comments_count) OVER (PARTITION BY hubs.name ORDER BY
        comments_count DESC) AS max_comments_count
FROM user_comments_by_hubs
JOIN hubs
    ON hub_id = hubs.id
JOIN users
    ON user_id = users.id
ORDER BY hubs.name;
```
Результат выполнения запроса:
![Subquery2](https://github.com/orlfi/GeekBrains/blob/Postgresql.Project/10.Postgresql/Project/5.2.%20Result.png)

### 6. Запросы с использованием объединения JOIN
6.1. Выбрать публикации из раздела 'Разработка игр'.
Вывести заголовок публикации, имя автора и количество комментариев к публикациям 
за последние 10 месяцев. Отсортировать результат по количеству комментариев по убыванию.
```sql
SELECT 
	hubs.name AS hub_name, 
	title, 
	users.name AS user_name, 
	count(comments.publication_id) AS comments_count
FROM hubs_publications
JOIN publications 
	ON publications.id = hubs_publications.publication_id
JOIN hubs 
	ON hubs.id = hub_id
JOIN users 
	ON publications.author_id = users.id
LEFT JOIN comments
	ON publications.id= comments.publication_id
WHERE hubs.name = 'Разработка игр' 
    AND comments.created_at > NOW() - interval '10 month' 
GROUP BY hubs.id, publications.id, users.id
ORDER BY comments_count DESC;
```
Скрипт приведен в файле `6.1. Join.sql`

Результат выполнения запроса:
![Join1](https://github.com/orlfi/GeekBrains/blob/Postgresql.Project/10.Postgresql/Project/6.1.%20Result.png)

6.2. Выбрать хабы, у которых имеются комментраии в публикациях,
а так же пользователей, оставивших максимальное количество 
комментариев в соответствующих хабах.
```sql
SELECT 
	hubs.name AS hub_name, 
	title, 
	users.name AS user_name, 
	count(comments.publication_id) AS comments_count
FROM hubs_publications
JOIN publications 
	ON publications.id = hubs_publications.publication_id
JOIN hubs 
	ON hubs.id = hub_id
JOIN users 
	ON publications.author_id = users.id
LEFT JOIN comments
	ON publications.id= comments.publication_id
GROUP BY hubs.id, publications.id, users.id
ORDER BY comments_count DESC;
```
Скрипт приведен в файле `6.2. Join.sql`

Результат выполнения запроса:
![Join2](https://github.com/orlfi/GeekBrains/blob/Postgresql.Project/10.Postgresql/Project/6.2.%20Result.png)