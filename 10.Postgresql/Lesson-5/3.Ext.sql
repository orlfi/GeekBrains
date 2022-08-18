 SELECT * FROM pg_available_extensions where name = 'sslinfo';
 SELECT * FROM pg_extension WHERE extname = 'sslinfo';
 CREATE EXTENSION "sslinfo";