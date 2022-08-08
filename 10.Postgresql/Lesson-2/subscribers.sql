-- Таблица подписчики пользователей
CREATE TABLE subscribers_users (
  user_id INT NOT NULL,
  subscriber_id INT NOT NULL,
  subscribed_at TIMESTAMP,
  PRIMARY KEY (user_id, subscriber_id)
);

-- Таблица подписчики сообществ
CREATE TABLE communities_subscribers (
  community_id INT NOT NULL,
  subscriber_id INT NOT NULL,
  subscribed_at TIMESTAMP,
  PRIMARY KEY (community_id, subscriber_id)
);
