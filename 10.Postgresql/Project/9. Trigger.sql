CREATE OR REPLACE FUNCTION verify_publication_creation_datetime_trigger()
RETURNS TRIGGER AS
$$
DECLARE today TIMESTAMP;
BEGIN
today := NOW();
IF NEW.created_at > today THEN
	RAISE EXCEPTION 'created_at:% must be greater than the current date:% ', NEW.created_at, today;
END IF;
RETURN NEW;
END
$$
LANGUAGE PLPGSQL;

CREATE TRIGGER verify_publication_creation_datetime_on_update BEFORE UPDATE ON publications
	FOR EACH ROW
	EXECUTE FUNCTION verify_publication_creation_datetime_trigger();

UPDATE publications SET created_at = '2099-01-01' WHERE id = 1

UPDATE publications SET created_at = '2021-01-01 08:00' WHERE id = 1