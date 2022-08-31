DROP TABLE IF EXISTS users_with_confirmed_statuses;

CREATE TEMPORARY TABLE users_with_confirmed_statuses (
user_id INT
);

INSERT INTO users_with_confirmed_statuses
	SELECT DISTINCT
		users.id
		FROM users
			JOIN friendship 
				ON friendship.requested_by_user_id = users.id
					OR friendship.requested_to_user_id = users.id
			JOIN friendship_statuses ON friendship_statuses.id = friendship.status_id
		WHERE friendship_statuses."name" = 'подтверждено';

DROP TABLE IF EXISTS users_without_confirmed_statuses;

CREATE TEMPORARY TABLE users_without_confirmed_statuses (
user_id INT
);

INSERT INTO users_without_confirmed_statuses
	SELECT id
		FROM USERS
		LEFT JOIN users_with_confirmed_statuses 
			on id = user_id
		WHERE user_id IS NULL;
			
BEGIN;
-- Удаляем пользователя из сообществ
 DELETE 
 	FROM communities_users
 	USING users_without_confirmed_statuses
 		WHERE users_without_confirmed_statuses.user_id = communities_users.user_id;

 -- Удаляем записи о дружеских отношениях
 DELETE 
 	FROM friendship 
 	USING users_without_confirmed_statuses
 		WHERE friendship.requested_by_user_id = users_without_confirmed_statuses.user_id
 					OR friendship.requested_to_user_id = users_without_confirmed_statuses.user_id;

 -- Удаляем сообщения пользователей
 DELETE 
 	FROM messages
 		USING users_without_confirmed_statuses
 			WHERE users_without_confirmed_statuses.user_id = messages.from_user_id 
 				OR users_without_confirmed_statuses.user_id = messages.to_user_id;

 -- Очищаем ссылку на фотографии пользователя в таблице users
 UPDATE users 
 	SET main_photo_id = NULL 
 		WHERE users.id IN (SELECT user_id FROM users_without_confirmed_statuses);

-- Удаляем фотографии пользователя
DELETE FROM photo 
	USING users_without_confirmed_statuses
		WHERE users_without_confirmed_statuses.user_id = photo.owner_id;

-- Удаляем видео файлы пользователя
DELETE FROM video 
	USING users_without_confirmed_statuses
		WHERE users_without_confirmed_statuses.user_id = video.owner_id;
		
-- Удаляем пользователя
DELETE FROM users 
	USING users_without_confirmed_statuses
		WHERE users_without_confirmed_statuses.user_id = users.id;
		
ROLLBACK;