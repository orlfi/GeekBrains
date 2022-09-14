-- Выбрать публикации из раздела 'Разработка игр'.
-- Вывести заголовок публикации, имя автора и количество комментариев к публикациям. 
-- Отсортировать результат по количеству комментариев по убыванию

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