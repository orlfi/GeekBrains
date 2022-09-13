-- Выбор пользователей с количеством публикаций больше среднего 
-- с использованием коррелируещего подзапроса и некоррелируещего подзапроса
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
ORDER BY publications_count DESC
