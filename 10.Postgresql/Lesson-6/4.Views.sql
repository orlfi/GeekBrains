CREATE VIEW big_videos AS
	SELECT * FROM video
		WHERE SIZE > 500
		ORDER BY size;

SELECT * FROM big_videos;

UPDATE big_videos SET size = 499 WHERE id = 58


CREATE VIEW users_video_summary AS 
	SELECT  owner_id, COUNT(*) as count, SUM(size) as size
		FROM video
		GROUP by owner_id;

SELECT * FROM users_video_summary
	where owner_id = 87

UPDATE users_video_summary SET size = 10
	where owner_id = 87