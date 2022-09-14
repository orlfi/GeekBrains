-- Выводит количество комментариев по id  пользователя
DROP FUNCTION IF EXISTS comments_total_by_user_id;

CREATE FUNCTION comments_total_by_user_id(requested_user_id INTEGER) 
RETURNS BIGINT AS
$$
SELECT
    COUNT(*)
FROM users
LEFT JOIN comments
    ON users.id = comments.user_id
WHERE users.id = requested_user_id;
$$
LANGUAGE SQL;

SELECT comments_total_by_user_id(3);