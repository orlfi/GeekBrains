SELECT 		
		big_photo.id as photo_id,
		CONCAT(first_name, ' ', last_name),
		photo.url,
		big_photo.url,
		big_photo.size
	FROM photo as big_photo
	JOIN users 
		ON owner_id = users.id
	LEFT OUTER JOIN photo 
		ON main_photo_id = photo.id
	ORDER BY big_photo.size DESC LIMIT 10;