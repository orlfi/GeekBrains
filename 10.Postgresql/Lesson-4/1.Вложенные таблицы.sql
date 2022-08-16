SELECT 
	id,
	(SELECT 
		    CONCAT(first_name, ' ', last_name)
	    FROM users 
	    WHERE owner_id = id
    ) AS user,
	(SELECT 
		(SELECT 
                url 
		    FROM photo 
		    WHERE main_photo_id = id
         )
	    FROM users 
	    WHERE owner_id = id
    ) AS main_photo_url,
	url, 
	size 
FROM photo 
ORDER BY size DESC 
LIMIT 10;