-- Представление выводит помесячно количество комментариев по каждому хабу
DROP VIEW IF EXISTS hubs_comments_count_by_month;

CREATE VIEW hubs_comments_count_by_month AS
    SELECT date_trunc('month',created_at) AS year_month,
        hubs.name AS hub_name,
        count(*) as comments_count
    FROM comments
    JOIN hubs_publications
        ON hubs_publications.publication_id = comments.publication_id
    JOIN hubs
        ON hubs_publications.hub_id = hubs.id
    GROUP BY year_month, hubs.name
    ORDER BY year_month, hubs.name;

SELECT 
	to_char(year_month,'month yyyy') AS month_name, 
	hub_name, 
	comments_count 
FROM hubs_comments_count_by_month;