
DROP TABLE IF EXISTS users_with_bad_avatars;

CREATE TEMPORARY TABLE users_with_bad_avatars (
    user_id INT
);

INSERT INTO users_with_bad_avatars
		SELECT profiles.user_id
    		FROM images 
    			JOIN profiles 
					ON avatar_image_id = images.id 
			WHERE user_id != owner_id;

UPDATE profiles 
    SET avatar_image_id = NULL
     WHERE user_id IN (SELECT user_id FROM users_with_bad_avatars);

UPDATE profiles SET avatar_image_id = 2
    WHERE user_id = 16;

UPDATE profiles SET avatar_image_id = 9
    WHERE user_id = 53;

UPDATE profiles SET avatar_image_id = 18
    WHERE user_id = 4;    

UPDATE profiles SET avatar_image_id = 37
    WHERE user_id = 84;        

UPDATE profiles SET avatar_image_id = 65
    WHERE user_id = 66;            