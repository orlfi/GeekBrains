CREATE OR REPLACE PROCEDURE verify_user_main_photo_id()
LANGUAGE PLPGSQL AS
$$
DECLARE profile_row RECORD;
BEGIN
	FOR profile_row IN
		SELECT user_id
		FROM profiles
			JOIN photo 
				ON profiles.main_photo_id = photo.id
		WHERE user_id != owner_id
		LOOP
			UPDATE profiles SET main_photo_id = null WHERE user_id = profile_row.user_id;
		END LOOP;
	COMMIT;
END;
$$;