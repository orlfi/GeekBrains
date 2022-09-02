-- 1. Публикации
CREATE TABLE IF NOT EXISTS publications (
	id INTEGER PRIMARY KEY,
	title VARCHAR(400) NOT NULL UNIQUE,
	cover_image_id INTEGER,
	body TEXT NOT NULL,
	author_id INTEGER NOT NULL,
	created_at TIMESTAMP NOT NULL
);

-- 2. Хабы (наборов тематических статей)
CREATE TABLE IF NOT EXISTS hubs (
	id INTEGER PRIMARY KEY,
	name VARCHAR(200) NOT NULL UNIQUE
);

-- 3. Связи публикаций с хабами (многие ко многим)
CREATE TABLE IF NOT EXISTS hubs_publications (
	hub_id INTEGER,
	publication_id INTEGER,
	PRIMARY KEY (hub_id, publication_id)
);

-- 4. Комментарии
CREATE TABLE IF NOT EXISTS comments (
	id INTEGER PRIMARY KEY,
	parent_comment_id INTEGER PRIMARY KEY,
	publication_id INTEGER NOT NULL,
	user_id INTEGER NOT NULL,
	status_id INTEGER NOT NULL,
	created_at TIMESTAMP NOT NULL
);

-- 5. Статусы комментариев
CREATE TABLE IF NOT EXISTS comment_statuses (
	id INTEGER PRIMARY KEY,
	name VARCHAR(10)
);

-- 6. Пользователи
CREATE TABLE IF NOT EXISTS users (
	id INTEGER PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
  	email VARCHAR(120) NOT NULL UNIQUE,
  	phone VARCHAR(15) UNIQUE,
  	created_at TIMESTAMP
);

-- 7. Профили пользователей
CREATE TABLE IF NOT EXISTS profiles (
	user_id INTEGER PRIMARY KEY,
	avatar_image_id INT,
	gender VARCHAR(1),
	birthday DATE
);

-- 8. Закладки пользователей
CREATE TABLE IF NOT EXISTS bookmarks (
	user_id INTEGER,
	publication_id INTEGER,
	PRIMARY KEY(user_id, publication_id)
);

-- 9. Изображения (для автарок и обложек статей)
CREATE TABLE IF NOT EXISTS images (
	id INTEGER PRIMARY KEY,
	url VARCHAR(250) NOT NULL UNIQUE,
 	owner_id INT NOT NULL,
 	description VARCHAR(250) NOT NULL,
  	uploaded_at TIMESTAMP NOT NULL,
  	size INT NOT NULL
);

-- 10. Подписка на уведомления
CREATE TABLE IF NOT EXISTS notifications (
	user_id INTEGER,
	notification_type_id INTEGER,
	PRIMARY KEY (user_id, notification_type_id)
);

-- 11. Типы уведомлений
CREATE TABLE IF NOT EXISTS notification_types (
	id INTEGER PRIMARY KEY,
	name VARCHAR(250)
);