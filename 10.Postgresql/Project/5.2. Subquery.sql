-- Выбрать хабы, у которых имеются комментарии в публикациях,
-- а также пользователей, оставивших максимальное количество комментрариев в хабе
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
ORDER BY hubs.name
