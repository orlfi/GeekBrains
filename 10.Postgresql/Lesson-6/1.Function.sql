DROP FUNCTION IF EXISTS get_most_active_sender_to_user;
CREATE FUNCTION get_most_active_sender_to_user(user_id INTEGER)
RETURNS INTEGER AS
$$
	SELECT from_user_id
		FROM messages
		WHERE to_user_id = user_id
		GROUP BY from_user_id
		ORDER BY  COUNT(from_user_id) DESC
		LIMIT 1
$$
LANGUAGE SQL;

SELECT get_most_active_sender_to_user(77);