WITH community_users_video AS
(SELECT 
		communities.id AS communities_id,
		communities.name AS communities_name,
		communities_users.user_id AS communities_user_id,
		video.size
	FROM video
 	JOIN communities_users
 		ON video.owner_id = communities_users.user_id
	RIGHT JOIN communities 
 		ON communities.id = communities_users.community_id
)
SELECT DISTINCT
	community_users_video.communities_id AS communities_id_2version,
	community_users_video.communities_name AS communities_name_2version,
	AVG(community_users_video.size) OVER (PARTITION BY community_users_video.communities_id) AS average_video_size_2version,
	FIRST_VALUE(users.first_name || ' ' || users.last_Name) 
		OVER (PARTITION BY community_users_video.communities_id ORDER BY community_users_video.size DESC NULLS LAST) AS user_with_max_video_2version
FROM community_users_video 
LEFT JOIN users
	ON community_users_video.communities_user_id = users.id
ORDER BY communities_id_2version;
