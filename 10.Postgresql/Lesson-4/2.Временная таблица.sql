CREATE TEMPORARY TABLE big_photo (
	id INT,
	owner_id INT,
	size INT,
	url VARCHAR(250)
);

INSERT INTO big_photo
	SELECT 
		id,
		owner_id,
		size,
		url
	FROM photo 
	ORDER BY size DESC LIMIT 10;

SELECT 		
		big_photo.id as photo_id,
		CONCAT(first_name, ' ', last_name),
		photo.url,
		big_photo.url,
		big_photo.size
	FROM big_photo
	JOIN users 
		ON owner_id = users.id
	LEFT OUTER JOIN photo 
		ON main_photo_id = photo.id;