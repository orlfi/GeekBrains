UPDATE communities
	SET members = (SELECT ARRAY_AGG(user_id)
		FROM communities_users
		WHERE communities_users.community_id = id) 
	WHERE id = 3;