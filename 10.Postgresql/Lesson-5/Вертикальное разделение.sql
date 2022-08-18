SELECT id AS user_id, main_photo_id, created_at INTO profiles FROM users;

ALTER TABLE users DROP COLUMN main_photo_id, DROP COLUMN created_at;

ALTER TABLE profiles
	ADD CONSTRAINT profiles_user_id_fk
	FOREIGN KEY (user_id)
	REFERENCES users(id);