ALTER TABLE bookmarks
    ADD CONSTRAINT bookmarks_user_id_fk
    FOREIGN KEY (user_id)
    REFERENCES users(id);

ALTER TABLE bookmarks
    ADD CONSTRAINT bookmarks_publication_id_fk
    FOREIGN KEY (publication_id)
    REFERENCES publications(id);

ALTER TABLE comments
    ADD CONSTRAINT comments_user_id_fk
    FOREIGN KEY (user_id)
    REFERENCES users(id);

ALTER TABLE comments
    ADD CONSTRAINT comments_publication_id_fk
    FOREIGN KEY (publication_id)
    REFERENCES publications(id);

ALTER TABLE comments
    ADD CONSTRAINT comments_status_id_fk
    FOREIGN KEY (status_id)
    REFERENCES comment_statuses(id);    

ALTER TABLE hubs_publications
    ADD CONSTRAINT hubs_publications_hub_id_fk
    FOREIGN KEY (hub_id)
    REFERENCES hubs(id);

ALTER TABLE hubs_publications
    ADD CONSTRAINT hubs_publications_publication_id_fk
    FOREIGN KEY (publication_id)
    REFERENCES publications(id);

ALTER TABLE images
    ADD CONSTRAINT images_owner_id_fk
    FOREIGN KEY (owner_id)
    REFERENCES users(id);

ALTER TABLE notifications
    ADD CONSTRAINT notifications_user_id_fk
    FOREIGN KEY (user_id)
    REFERENCES users(id);

ALTER TABLE notifications
    ADD CONSTRAINT notifications_notification_type_id_fk
    FOREIGN KEY (notification_type_id)
    REFERENCES notification_types(id);

ALTER TABLE profiles
    ADD CONSTRAINT profiles_avatar_image_id_fk
    FOREIGN KEY (avatar_image_id)
    REFERENCES images(id);

ALTER TABLE publications
    ADD CONSTRAINT notifications_author_id_fk
    FOREIGN KEY (author_id)
    REFERENCES users(id);

ALTER TABLE publications
    ADD CONSTRAINT notifications_cover_image_id_fk
    FOREIGN KEY (cover_image_id)
    REFERENCES images(id);