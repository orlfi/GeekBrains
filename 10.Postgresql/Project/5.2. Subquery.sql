-- Для всех пользователей вывести заголовок последнего сообщения, 
-- опубликованное более 6 месяцев назад и отсортировать по дате публикации 
-- (полбзователи с отстутсвутющими публикациями вывести в конце)
SELECT
    users.name,
    six_month_old_last_publications.created_at,
    title AS publication_title
FROM users
LEFT JOIN  
(
    SELECT last_publications.author_id, publications.created_at, title  FROM 
    (
        SELECT
            author_id,
            MAX(id) AS publication_id
        FROM publications
        WHERE created_at < NOW()  - interval '6 month'
        GROUP BY author_id
    ) AS last_publications
    JOIN publications 
        ON id = publication_id
) AS six_month_old_last_publications
    ON six_month_old_last_publications.author_id = users.id
ORDER BY six_month_old_last_publications.created_at ASC NULLS LAST;

