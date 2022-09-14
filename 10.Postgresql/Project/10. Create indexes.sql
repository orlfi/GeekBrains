-- создаем дополнительные индексы

CREATE INDEX IF NOT EXISTS hubs_name_idx ON hubs(name);
CREATE INDEX IF NOT EXISTS hubs_publications_hub_id_fk ON hubs_publications(hub_id);
CREATE INDEX IF NOT EXISTS comments_publication_id_fk ON comments(publication_id);
CREATE INDEX IF NOT EXISTS comments_created_at_idx ON comments(created_at);