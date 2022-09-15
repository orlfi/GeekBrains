CREATE OR REPLACE PROCEDURE verify_user_avatar_image_id()
LANGUAGE PLPGSQL AS
$$
DECLARE profile_row RECORD;
BEGIN
	FOR profile_row IN
		SELECT profiles.user_id
    		FROM images 
    			JOIN profiles 
					ON avatar_image_id = images.id 
			WHERE user_id != owner_id
		LOOP
			UPDATE profiles SET avatar_image_id = null WHERE user_id = profile_row.user_id;
		END LOOP;
	COMMIT;
END;
$$;