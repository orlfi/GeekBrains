CREATE OR REPLACE FUNCTION update_main_photo_id_trigger()
RETURNS TRIGGER AS
$$
DECLARE is_owner BOOLEAN;
BEGIN
	is_owner := EXISTS(SELECT * FROM photo WHERE NEW.main_photo_id = id AND NEW.user_id = owner_id);
	IF is_owner THEN
		RETURN NEW;
	ELSE
		RAISE EXCEPTION 'User ID:% is not the owner of the photo ID:% ', NEW.user_id, NEW.main_photo_id;
	END IF;
END
$$
LANGUAGE PLPGSQL;


CREATE TRIGGER check_main_photo_id_on_update BEFORE UPDATE ON profiles
	FOR EACH ROW
	EXECUTE FUNCTION update_main_photo_id_trigger();

UPDATE profiles SET main_photo_id = 44 WHERE user_id = 1

UPDATE profiles SET main_photo_id = 43 WHERE user_id = 1