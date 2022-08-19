create role serovf login;
alter role serovf with password '123456'
create role belovr login;
alter role belovr with password '123456';
create role analysts;
create role qa;
grant analysts TO serovf;
grant qa TO belovr;
GRANT SELECT ON ALL TABLES IN SCHEMA public TO analysts;
GRANT ALL ON ALL TABLES IN SCHEMA public TO qa;
GRANT ALL ON ALL SEQUENCES IN SCHEMA public TO qa;