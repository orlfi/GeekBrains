UPDATE users
	SET contacts = ROW(phone, email);

UPDATE users
	SET contacts.email = 'test@somemail.ru' WHERE id = 21;