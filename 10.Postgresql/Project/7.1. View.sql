-- Представление, выводящее хабы без публикаций
DROP VIEW hubs_without_publications;

CREATE VIEW hubs_without_publications AS
SELECT 
	hubs.name AS hub_name
FROM  hubs_publications
JOIN publications
	ON publications.id = hubs_publications.publication_id
RIGHT JOIN hubs 
	ON hubs.id = hubs_publications.hub_id
WHERE publications.title IS NULL
ORDER BY hubs.id;

SELECT * FROM hubs_without_publications;