WITH user_photo_and_video_cnt AS
(SELECT 
	users.first_name,  
	users.last_name,
	COUNT(DISTINCT photo.id) AS photo_count,
	COUNT(DISTINCT video.id) AS video_count
	FROM users
	LEFT JOIN photo
		ON photo.owner_id = users.id
	LEFT JOIN video 
		ON video.owner_id = users.id
	GROUP BY users.first_name, users.last_name)
SELECT 
	user_photo_and_video_cnt.first_name,  
	user_photo_and_video_cnt.last_name,
	user_photo_and_video_cnt.photo_count,
	DENSE_RANK() OVER (ORDER BY photo_count DESC) AS photo_rank,
	user_photo_and_video_cnt.video_count,
	DENSE_RANK() OVER (ORDER BY video_count DESC) AS video_rank
FROM user_photo_and_video_cnt
ORDER BY photo_rank DESC, video_rank DESC;
