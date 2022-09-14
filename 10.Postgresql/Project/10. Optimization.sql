-- Для проведения оптимизации использую запрос '6.1. Join.sql'
-- Результат построения плана выполнения запроса представлен в файле '10. Explain analyze before optimization.png'
-- Как можно увидить, львиную долю времени выполнения (1.140 мс) занимает перебор по таблице comments:
-- '->  Seq Scan on comments  (cost=0.00..74.00 rows=1665 width=4) (actual time=0.009..0.708 rows=1663 loops=1)'
-- т.е. из 1.140 ms общего времени выполнения 0.708 занимает Scan on comments
-- Таким образом можно сделать вывод, что узкое место в последовательном сканировании таблицы 'comments' по внешнему ключу comments.publication_id.
-- Для оптимизации запроса построю дополнительный индекс в таблице comments по внешнему ключу publication_id:

CREATE INDEX IF NOT EXISTS comments_publication_id_fk ON comments(publication_id);

-- После построения индекса время выполнения запроса уменьшилось до 0.352 мс
-- поиск по индексу в таблице comments уменьшился до 0.037 мс
-- '->  Index Scan using comments_publication_id_fk on comments  (cost=0.28..2.66 rows=17 width=4) (actual time=0.014..0.037 rows=19 loops=4)'.
-- Результат оптимизации запроса приведен в файле '10. Explain analyze after optimization.png' 