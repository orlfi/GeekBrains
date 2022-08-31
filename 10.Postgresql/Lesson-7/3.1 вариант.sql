SELECT DISTINCT
	communities.id AS communities_id_1version,
	communities.name AS communities_name_1version,
	AVG(video.size) OVER (PARTITION BY communities.id) AS average_video_size_1version,
	FIRST_VALUE(users.first_name || ' ' || users.last_Name) 
		OVER (PARTITION BY communities.id ORDER BY video.size DESC NULLS LAST) AS user_with_max_video_1version
FROM communities
	LEFT JOIN communities_users
		ON communities_users.community_id = communities.id
	LEFT JOIN video
		ON video.owner_id = communities_users.user_id
	LEFT JOIN users
		ON video.owner_id = users.id
ORDER BY communities_id_1version;