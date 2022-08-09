--
-- PostgreSQL database dump
--

-- Dumped from database version 12.11 (Ubuntu 12.11-0ubuntu0.20.04.1)
-- Dumped by pg_dump version 12.11 (Ubuntu 12.11-0ubuntu0.20.04.1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: communities; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.communities (
    id integer NOT NULL,
    name character varying(120),
    creator_id integer NOT NULL,
    created_at timestamp without time zone
);


ALTER TABLE public.communities OWNER TO gb;

--
-- Name: communities_id_seq; Type: SEQUENCE; Schema: public; Owner: gb
--

CREATE SEQUENCE public.communities_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.communities_id_seq OWNER TO gb;

--
-- Name: communities_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: gb
--

ALTER SEQUENCE public.communities_id_seq OWNED BY public.communities.id;


--
-- Name: communities_subscribers; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.communities_subscribers (
    community_id integer NOT NULL,
    subscriber_id integer NOT NULL,
    subscribed_at timestamp without time zone
);


ALTER TABLE public.communities_subscribers OWNER TO gb;

--
-- Name: communities_users; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.communities_users (
    community_id integer NOT NULL,
    user_id integer NOT NULL,
    created_at timestamp without time zone
);


ALTER TABLE public.communities_users OWNER TO gb;

--
-- Name: friendship; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.friendship (
    id integer NOT NULL,
    requested_by_user_id integer NOT NULL,
    requested_to_user_id integer NOT NULL,
    status_id integer NOT NULL,
    requested_at timestamp without time zone,
    confirmed_at timestamp without time zone
);


ALTER TABLE public.friendship OWNER TO gb;

--
-- Name: friendship_id_seq; Type: SEQUENCE; Schema: public; Owner: gb
--

CREATE SEQUENCE public.friendship_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.friendship_id_seq OWNER TO gb;

--
-- Name: friendship_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: gb
--

ALTER SEQUENCE public.friendship_id_seq OWNED BY public.friendship.id;


--
-- Name: friendship_statuses; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.friendship_statuses (
    id integer NOT NULL,
    name character varying(30)
);


ALTER TABLE public.friendship_statuses OWNER TO gb;

--
-- Name: friendship_statuses_id_seq; Type: SEQUENCE; Schema: public; Owner: gb
--

CREATE SEQUENCE public.friendship_statuses_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.friendship_statuses_id_seq OWNER TO gb;

--
-- Name: friendship_statuses_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: gb
--

ALTER SEQUENCE public.friendship_statuses_id_seq OWNED BY public.friendship_statuses.id;


--
-- Name: messages; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.messages (
    id integer NOT NULL,
    from_user_id integer NOT NULL,
    to_user_id integer NOT NULL,
    body text,
    is_important boolean,
    is_delivered boolean,
    created_at timestamp without time zone
);


ALTER TABLE public.messages OWNER TO gb;

--
-- Name: messages_id_seq; Type: SEQUENCE; Schema: public; Owner: gb
--

CREATE SEQUENCE public.messages_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.messages_id_seq OWNER TO gb;

--
-- Name: messages_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: gb
--

ALTER SEQUENCE public.messages_id_seq OWNED BY public.messages.id;


--
-- Name: photo; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.photo (
    id integer NOT NULL,
    url character varying(250) NOT NULL,
    owner_id integer NOT NULL,
    description character varying(250) NOT NULL,
    uploaded_at timestamp without time zone NOT NULL,
    size integer NOT NULL
);


ALTER TABLE public.photo OWNER TO gb;

--
-- Name: photo_id_seq; Type: SEQUENCE; Schema: public; Owner: gb
--

CREATE SEQUENCE public.photo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.photo_id_seq OWNER TO gb;

--
-- Name: photo_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: gb
--

ALTER SEQUENCE public.photo_id_seq OWNED BY public.photo.id;


--
-- Name: subscribers_users; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.subscribers_users (
    user_id integer NOT NULL,
    subscriber_id integer NOT NULL,
    subscribed_at timestamp without time zone
);


ALTER TABLE public.subscribers_users OWNER TO gb;

--
-- Name: users; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.users (
    id integer NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    email character varying(120) NOT NULL,
    phone character varying(15),
    main_photo_id integer,
    created_at timestamp without time zone
);


ALTER TABLE public.users OWNER TO gb;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: gb
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO gb;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: gb
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- Name: video; Type: TABLE; Schema: public; Owner: gb
--

CREATE TABLE public.video (
    id integer NOT NULL,
    url character varying(250) NOT NULL,
    owner_id integer NOT NULL,
    description character varying(250) NOT NULL,
    uploaded_at timestamp without time zone NOT NULL,
    size integer NOT NULL
);


ALTER TABLE public.video OWNER TO gb;

--
-- Name: video_id_seq; Type: SEQUENCE; Schema: public; Owner: gb
--

CREATE SEQUENCE public.video_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.video_id_seq OWNER TO gb;

--
-- Name: video_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: gb
--

ALTER SEQUENCE public.video_id_seq OWNED BY public.video.id;


--
-- Name: communities id; Type: DEFAULT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.communities ALTER COLUMN id SET DEFAULT nextval('public.communities_id_seq'::regclass);


--
-- Name: friendship id; Type: DEFAULT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.friendship ALTER COLUMN id SET DEFAULT nextval('public.friendship_id_seq'::regclass);


--
-- Name: friendship_statuses id; Type: DEFAULT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.friendship_statuses ALTER COLUMN id SET DEFAULT nextval('public.friendship_statuses_id_seq'::regclass);


--
-- Name: messages id; Type: DEFAULT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.messages ALTER COLUMN id SET DEFAULT nextval('public.messages_id_seq'::regclass);


--
-- Name: photo id; Type: DEFAULT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.photo ALTER COLUMN id SET DEFAULT nextval('public.photo_id_seq'::regclass);


--
-- Name: users id; Type: DEFAULT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- Name: video id; Type: DEFAULT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.video ALTER COLUMN id SET DEFAULT nextval('public.video_id_seq'::regclass);


--
-- Data for Name: communities; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.communities (id, name, creator_id, created_at) FROM stdin;
1	libero. Proin mi. Aliquam	49	2023-07-04 09:50:20
2	auctor velit. Aliquam	57	2023-06-04 13:21:22
3	mi felis,	31	2023-06-30 07:28:31
4	mattis velit	67	2021-10-14 10:14:28
5	Duis	18	2023-02-28 21:54:23
6	id, ante. Nunc mauris	40	2021-11-15 21:34:05
7	amet diam eu dolor	38	2022-10-05 20:48:55
8	fermentum	49	2023-03-04 18:33:29
9	in	24	2022-02-28 15:52:52
10	Aliquam rutrum lorem	28	2022-05-28 22:08:30
11	sociis natoque penatibus et	1	2021-09-28 11:33:08
12	tincidunt dui augue	61	2022-11-23 18:12:07
13	eget laoreet	10	2022-06-26 18:13:27
14	erat neque	21	2021-10-04 09:41:47
15	magna. Phasellus dolor elit,	63	2022-07-29 10:38:08
16	erat. Sed nunc est, mollis	32	2022-02-16 21:13:14
17	pretium aliquet,	54	2023-05-01 04:14:17
18	justo faucibus lectus, a	90	2023-04-23 13:16:18
19	elementum, dui quis accumsan	29	2022-06-12 01:19:34
20	massa. Integer vitae	14	2022-08-23 18:11:39
21	Cras dictum ultricies	24	2022-11-24 23:03:47
22	eu sem. Pellentesque	53	2022-05-26 12:34:27
23	mollis vitae, posuere	57	2022-03-18 11:52:57
24	ornare, elit	6	2021-11-22 23:12:34
25	turpis	26	2022-08-13 18:15:17
26	amet, faucibus ut,	12	2022-08-16 23:38:23
27	enim. Etiam gravida molestie arcu.	95	2022-04-11 14:27:37
28	viverra. Maecenas iaculis aliquet	8	2022-02-27 05:52:23
29	scelerisque sed, sapien. Nunc	66	2023-04-26 12:07:15
30	lectus. Cum	46	2022-12-02 08:57:48
31	at lacus. Quisque	23	2022-10-05 11:07:00
32	eleifend egestas.	56	2022-07-25 09:33:26
33	Aenean	46	2023-03-01 13:54:15
34	convallis,	92	2022-12-18 11:32:11
35	Fusce aliquam, enim	94	2021-10-21 23:35:55
36	nec, leo. Morbi neque	75	2023-01-06 07:53:50
37	lacinia	22	2023-02-09 21:42:38
38	Duis at lacus.	8	2023-01-13 02:59:49
39	mus.	23	2022-03-11 09:25:21
40	vitae risus. Duis	71	2021-12-02 14:01:50
41	tristique ac,	14	2021-11-10 01:54:26
42	sit amet, consectetuer adipiscing elit.	91	2021-10-12 14:43:28
43	amet luctus vulputate, nisi	28	2022-09-16 12:46:47
44	Fusce mollis.	73	2022-02-09 23:39:09
45	dis	19	2023-05-13 12:40:22
46	libero est, congue	28	2022-12-16 12:09:09
47	et libero.	11	2021-09-10 13:42:16
48	Curae Phasellus ornare.	28	2022-03-03 22:55:52
49	lacus. Aliquam rutrum	59	2022-02-08 07:53:21
50	at pretium aliquet, metus	32	2023-03-15 17:09:15
51	nec, diam. Duis	95	2023-01-27 15:58:24
52	molestie sodales. Mauris blandit	24	2022-12-09 13:30:49
53	eget nisi dictum	11	2021-08-17 01:36:52
54	facilisi. Sed	76	2022-11-04 22:21:45
55	id sapien. Cras dolor	22	2022-07-18 07:50:44
56	enim. Etiam imperdiet	19	2022-07-19 03:13:00
57	et magnis	43	2022-02-19 14:39:14
58	imperdiet ullamcorper. Duis at	83	2021-08-04 11:04:35
59	mattis velit justo nec ante.	29	2022-01-14 09:14:19
60	dictum eleifend, nunc risus	78	2023-07-27 01:57:42
61	ligula.	96	2021-10-04 12:01:21
62	interdum. Sed	99	2022-08-02 15:52:17
63	lacinia mattis. Integer	28	2022-02-23 14:29:26
64	sollicitudin commodo ipsum.	71	2021-11-29 05:06:36
65	Vestibulum ante ipsum primis	68	2023-07-25 13:28:46
66	sit amet, consectetuer	31	2022-09-21 18:21:21
67	at lacus. Quisque er	93	2022-10-23 22:20:14
68	augue ut lacus. Nulla	55	2022-08-13 06:35:59
69	id nunc interdum	16	2023-06-09 01:45:32
70	feugiat metus sit amet	19	2021-09-15 02:08:56
71	pretium et, rutrum non, hendrerit	95	2023-04-21 18:17:56
72	mauris id sapien. Cras	79	2023-06-07 11:45:09
73	sem eget massa. Suspendisse	71	2022-04-14 08:55:37
74	condimentum. Donec	54	2021-10-29 09:39:42
75	fringilla	31	2022-06-07 12:16:05
76	nibh lacinia	78	2023-04-02 13:10:01
77	magna sed dui. Fusce	77	2023-04-24 02:16:09
78	tristique pharetra. Quisque	86	2023-07-25 15:24:02
79	ligula. Donec luctus aliquet	82	2023-08-05 11:16:03
80	elit, pharetra ut, pharetra sed,	46	2023-03-05 16:32:03
81	aptent taciti sociosqu ad	73	2021-10-14 05:59:34
82	semper. Nam tempor diam	19	2022-06-23 06:16:23
83	ornare egestas ligula.	9	2023-03-09 07:16:50
84	ligula. Aliquam erat volutpat.	46	2023-02-02 06:23:33
85	Proin sed turpis nec	69	2022-07-12 07:23:15
86	metus	27	2021-10-21 23:25:27
87	lectus quis massa. Mauris	75	2023-05-23 19:01:59
88	nunc interdum feugiat. Sed	90	2021-11-17 04:03:51
89	et, rutrum eu, ultrices	95	2022-10-30 00:31:24
90	velit. Aliquam nisl.	57	2023-06-11 08:02:57
91	convallis est,	61	2022-08-08 20:20:36
92	malesuada vel, convallis	71	2021-11-24 12:01:02
93	euismod et, commodo	78	2022-07-11 19:35:53
94	conubia nostra, per	91	2022-11-22 11:02:23
95	vulputate, lacus. Cras interdum.	98	2022-11-12 02:32:58
96	Donec elementum, lorem	47	2023-05-18 18:56:08
97	at, velit. Cras	43	2022-05-28 15:51:24
98	felis, adipiscing fringilla,	5	2022-12-13 15:15:56
99	ac turpis egestas. Aliquam	35	2022-09-08 08:32:17
100	quam a	68	2021-08-05 00:17:42
\.


--
-- Data for Name: communities_subscribers; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.communities_subscribers (community_id, subscriber_id, subscribed_at) FROM stdin;
6	1	2023-07-11 14:25:08
94	8	2021-10-22 10:16:09
95	44	2022-12-15 23:15:27
34	79	2021-12-17 02:28:07
41	74	2023-05-19 08:09:25
14	82	2022-01-06 03:32:57
10	99	2022-01-18 08:25:33
77	5	2021-08-13 04:30:45
50	83	2023-04-11 12:14:52
95	81	2023-01-31 21:52:48
72	59	2022-09-04 04:27:29
27	53	2023-05-24 15:29:11
85	56	2023-01-26 23:53:59
92	75	2021-11-17 08:24:59
22	22	2021-10-18 18:08:42
66	78	2022-06-13 21:04:20
44	43	2022-03-26 17:29:03
90	24	2022-02-23 08:57:20
48	20	2022-12-12 07:45:10
73	56	2022-12-20 22:31:22
28	86	2022-05-01 23:26:48
26	38	2022-07-19 05:38:59
12	73	2022-12-20 08:54:15
37	16	2022-06-16 12:56:08
73	48	2023-02-01 20:11:28
75	61	2023-02-05 10:47:06
94	34	2023-02-23 00:19:13
53	73	2022-08-02 04:05:42
73	62	2022-01-02 02:07:40
59	65	2023-08-02 15:32:07
12	10	2022-04-05 18:59:34
84	17	2021-09-17 12:26:45
8	38	2023-05-10 23:20:35
18	47	2023-04-17 13:27:19
45	41	2021-08-05 06:28:47
20	12	2022-05-27 01:24:58
74	32	2022-04-28 01:25:38
62	63	2022-03-08 12:45:32
26	6	2022-05-07 12:56:19
28	15	2022-02-07 00:29:26
74	28	2023-01-15 02:57:18
46	21	2022-04-10 16:28:05
66	93	2022-06-24 16:25:39
1	37	2023-01-15 09:12:48
41	95	2023-05-30 01:47:57
43	54	2022-08-06 17:49:26
50	14	2022-01-30 03:21:27
12	43	2022-09-28 12:34:12
67	80	2023-02-09 20:55:16
46	81	2023-04-13 06:18:39
33	45	2022-04-13 04:38:30
92	20	2023-07-13 11:09:26
51	7	2023-06-09 19:46:45
89	57	2022-03-24 09:37:42
13	47	2021-11-19 05:00:46
45	32	2021-11-27 16:52:48
94	57	2023-06-24 15:04:25
10	31	2023-01-25 01:02:39
12	48	2022-04-08 03:37:03
85	80	2021-11-16 11:53:01
56	77	2021-08-29 16:22:38
74	88	2021-08-16 15:09:30
23	6	2022-07-30 12:47:51
16	46	2021-12-26 09:29:00
52	30	2022-12-23 09:02:37
43	28	2022-03-09 23:49:20
42	84	2022-10-31 00:48:23
52	61	2021-09-17 02:54:16
54	66	2022-08-18 05:32:44
68	71	2023-04-16 13:37:13
11	25	2022-12-02 20:49:19
89	25	2022-12-30 10:59:39
99	56	2022-02-24 01:41:20
31	81	2022-10-19 20:32:19
54	77	2022-03-01 04:09:19
29	78	2021-08-05 06:10:16
27	82	2022-05-05 14:23:44
31	65	2022-12-27 23:43:51
43	95	2022-11-05 05:41:50
33	77	2022-02-03 20:56:01
65	95	2021-10-08 14:04:27
98	82	2022-06-10 06:42:40
93	10	2022-05-17 22:53:11
72	64	2022-05-07 13:04:02
81	70	2021-08-05 13:26:38
53	7	2022-04-10 11:16:02
76	12	2021-11-20 22:33:43
93	44	2022-11-21 03:55:25
74	29	2022-08-19 06:38:31
34	72	2021-10-31 19:44:02
96	57	2022-11-16 18:02:26
98	70	2023-07-16 15:29:14
54	63	2023-01-18 06:02:46
24	48	2022-06-27 15:00:40
21	37	2021-11-11 05:43:55
37	86	2023-07-12 16:43:28
18	30	2021-10-03 14:09:48
78	40	2022-04-05 12:15:30
16	29	2022-06-30 17:32:27
6	19	2023-07-20 20:43:08
\.


--
-- Data for Name: communities_users; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.communities_users (community_id, user_id, created_at) FROM stdin;
6	1	2023-07-11 14:25:08
94	8	2021-10-22 10:16:09
95	44	2022-12-15 23:15:27
34	79	2021-12-17 02:28:07
41	74	2023-05-19 08:09:25
14	82	2022-01-06 03:32:57
10	99	2022-01-18 08:25:33
77	5	2021-08-13 04:30:45
50	83	2023-04-11 12:14:52
95	81	2023-01-31 21:52:48
72	59	2022-09-04 04:27:29
27	53	2023-05-24 15:29:11
85	56	2023-01-26 23:53:59
92	75	2021-11-17 08:24:59
22	22	2021-10-18 18:08:42
66	78	2022-06-13 21:04:20
44	43	2022-03-26 17:29:03
90	24	2022-02-23 08:57:20
48	20	2022-12-12 07:45:10
73	56	2022-12-20 22:31:22
28	86	2022-05-01 23:26:48
26	38	2022-07-19 05:38:59
12	73	2022-12-20 08:54:15
37	16	2022-06-16 12:56:08
73	48	2023-02-01 20:11:28
75	61	2023-02-05 10:47:06
94	34	2023-02-23 00:19:13
53	73	2022-08-02 04:05:42
73	62	2022-01-02 02:07:40
59	65	2023-08-02 15:32:07
12	10	2022-04-05 18:59:34
84	17	2021-09-17 12:26:45
8	38	2023-05-10 23:20:35
18	47	2023-04-17 13:27:19
45	41	2021-08-05 06:28:47
20	12	2022-05-27 01:24:58
74	32	2022-04-28 01:25:38
62	63	2022-03-08 12:45:32
26	6	2022-05-07 12:56:19
28	15	2022-02-07 00:29:26
74	28	2023-01-15 02:57:18
46	21	2022-04-10 16:28:05
66	93	2022-06-24 16:25:39
1	37	2023-01-15 09:12:48
41	95	2023-05-30 01:47:57
43	54	2022-08-06 17:49:26
50	14	2022-01-30 03:21:27
12	43	2022-09-28 12:34:12
67	80	2023-02-09 20:55:16
46	81	2023-04-13 06:18:39
33	45	2022-04-13 04:38:30
92	20	2023-07-13 11:09:26
51	7	2023-06-09 19:46:45
89	57	2022-03-24 09:37:42
13	47	2021-11-19 05:00:46
45	32	2021-11-27 16:52:48
94	57	2023-06-24 15:04:25
10	31	2023-01-25 01:02:39
12	48	2022-04-08 03:37:03
85	80	2021-11-16 11:53:01
56	77	2021-08-29 16:22:38
74	88	2021-08-16 15:09:30
23	6	2022-07-30 12:47:51
16	46	2021-12-26 09:29:00
52	30	2022-12-23 09:02:37
43	28	2022-03-09 23:49:20
42	84	2022-10-31 00:48:23
52	61	2021-09-17 02:54:16
54	66	2022-08-18 05:32:44
68	71	2023-04-16 13:37:13
11	25	2022-12-02 20:49:19
89	25	2022-12-30 10:59:39
99	56	2022-02-24 01:41:20
31	81	2022-10-19 20:32:19
54	77	2022-03-01 04:09:19
29	78	2021-08-05 06:10:16
27	82	2022-05-05 14:23:44
31	65	2022-12-27 23:43:51
43	95	2022-11-05 05:41:50
33	77	2022-02-03 20:56:01
65	95	2021-10-08 14:04:27
98	82	2022-06-10 06:42:40
93	10	2022-05-17 22:53:11
72	64	2022-05-07 13:04:02
81	70	2021-08-05 13:26:38
53	7	2022-04-10 11:16:02
76	12	2021-11-20 22:33:43
93	44	2022-11-21 03:55:25
74	29	2022-08-19 06:38:31
34	72	2021-10-31 19:44:02
96	57	2022-11-16 18:02:26
98	70	2023-07-16 15:29:14
54	63	2023-01-18 06:02:46
24	48	2022-06-27 15:00:40
21	37	2021-11-11 05:43:55
37	86	2023-07-12 16:43:28
18	30	2021-10-03 14:09:48
78	40	2022-04-05 12:15:30
16	29	2022-06-30 17:32:27
6	19	2023-07-20 20:43:08
\.


--
-- Data for Name: friendship; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.friendship (id, requested_by_user_id, requested_to_user_id, status_id, requested_at, confirmed_at) FROM stdin;
1	7	76	1	2021-08-02 05:09:59	2021-10-10 01:11:24
2	31	76	2	2021-08-04 07:19:27	2022-01-09 09:55:11
3	21	50	3	2021-08-04 03:45:02	2021-08-15 08:37:09
4	28	30	3	2021-08-02 22:30:08	2022-04-21 10:23:58
5	99	78	3	2021-08-02 12:57:06	2022-08-23 07:35:13
6	88	3	2	2021-08-02 04:30:39	2022-07-18 12:32:29
7	47	86	3	2021-08-03 05:17:34	2021-10-12 23:55:57
8	53	95	1	2021-08-02 17:29:07	2021-10-12 10:45:48
9	42	99	2	2021-08-03 18:57:27	2021-10-11 15:12:53
10	69	17	2	2021-08-01 14:17:09	2023-06-13 09:12:48
11	6	44	2	2021-08-03 20:31:57	2022-11-18 07:22:20
12	64	55	3	2021-08-04 08:09:46	2023-01-06 01:47:15
13	32	49	1	2021-08-03 20:38:01	2023-06-08 11:34:11
14	27	91	2	2021-08-02 00:28:48	2021-10-26 08:38:46
15	28	38	2	2021-08-01 11:57:28	2023-01-07 15:03:19
16	58	12	2	2021-08-03 02:04:21	2021-12-06 20:56:20
17	92	19	2	2021-08-02 20:36:54	2022-01-23 10:14:01
18	51	94	2	2021-08-02 06:55:12	2022-07-08 09:56:38
19	88	77	2	2021-08-03 23:56:45	2022-12-07 09:42:03
20	39	33	1	2021-08-01 17:49:46	2023-05-20 20:07:25
21	50	82	1	2021-08-02 05:11:55	2023-02-26 09:24:29
22	35	73	2	2021-08-03 08:41:01	2021-08-24 11:58:11
23	75	46	3	2021-08-01 13:02:32	2023-04-27 08:53:08
24	9	25	2	2021-08-02 12:07:48	2021-09-13 03:02:39
25	88	46	1	2021-08-01 22:21:39	2021-10-17 20:50:15
26	61	36	3	2021-08-03 02:35:40	2022-01-20 12:30:17
27	87	75	1	2021-08-03 17:04:09	2021-11-27 21:57:27
28	29	81	2	2021-08-03 01:15:17	2023-04-09 21:44:39
29	25	50	1	2021-08-04 02:14:18	2021-08-27 23:37:03
30	5	20	3	2021-08-03 05:51:33	2021-10-25 08:35:30
31	87	3	1	2021-08-02 03:13:11	2023-07-18 00:33:20
32	93	35	3	2021-08-01 10:10:44	2023-07-10 03:25:04
33	90	41	1	2021-08-03 11:41:28	2022-01-21 11:27:58
34	4	95	3	2021-08-03 11:01:42	2021-10-09 14:10:52
35	8	84	2	2021-08-02 03:50:38	2022-01-23 22:07:58
36	19	30	3	2021-08-02 12:30:52	2023-07-03 19:29:10
37	86	35	2	2021-08-02 21:35:27	2023-01-13 07:18:25
38	4	5	2	2021-08-04 07:33:30	2022-07-15 13:21:49
39	59	78	3	2021-08-03 04:36:20	2022-08-11 06:57:37
40	29	76	2	2021-08-02 09:59:18	2023-05-04 00:18:21
41	31	61	3	2021-08-04 02:13:54	2022-08-05 02:20:48
42	37	81	2	2021-08-02 12:34:56	2023-07-08 04:27:00
43	65	72	2	2021-08-01 19:20:42	2023-06-26 15:43:56
44	11	73	3	2021-08-02 14:31:32	2023-01-22 01:31:54
45	25	22	3	2021-08-03 19:12:41	2022-11-29 11:42:32
46	30	35	2	2021-08-03 21:31:08	2022-08-18 05:47:33
47	99	63	2	2021-08-02 21:12:44	2022-08-20 18:11:11
48	1	43	2	2021-08-03 14:43:26	2022-02-28 20:44:59
49	93	88	1	2021-08-03 01:02:14	2023-05-22 07:37:32
50	98	26	3	2021-08-01 20:47:33	2022-03-18 15:18:53
51	37	72	3	2021-08-03 02:55:44	2023-03-12 10:32:02
52	64	41	1	2021-08-03 12:45:06	2022-05-09 18:16:00
53	45	60	2	2021-08-01 19:02:34	2022-02-17 02:53:40
54	15	11	2	2021-08-04 01:03:53	2022-03-19 08:35:15
55	10	39	2	2021-08-02 15:01:22	2023-06-11 02:11:11
56	33	32	3	2021-08-03 17:52:05	2022-11-22 10:22:09
57	36	16	1	2021-08-01 19:51:38	2023-02-23 05:26:44
58	23	76	3	2021-08-02 17:21:57	2022-10-01 11:36:27
59	24	87	2	2021-08-02 17:24:23	2022-11-22 21:11:23
60	2	50	2	2021-08-01 12:42:27	2022-03-21 11:34:44
61	92	26	3	2021-08-03 23:10:45	2023-06-21 20:50:00
62	63	95	3	2021-08-03 17:18:17	2022-08-27 05:36:49
63	99	99	1	2021-08-04 03:27:33	2022-01-27 21:50:37
64	50	29	3	2021-08-01 20:26:48	2022-04-07 06:07:12
65	73	21	3	2021-08-03 09:52:04	2022-11-19 11:52:01
66	15	69	3	2021-08-03 00:31:48	2021-12-11 08:44:15
67	86	77	2	2021-08-02 20:58:02	2022-08-06 01:32:04
68	15	82	2	2021-08-02 13:07:57	2022-11-19 09:06:30
69	92	64	3	2021-08-03 07:28:44	2023-02-12 14:24:38
70	64	5	3	2021-08-02 12:34:44	2023-03-09 04:01:19
71	13	86	2	2021-08-03 07:04:31	2021-12-08 23:34:51
72	54	25	3	2021-08-01 23:26:28	2021-11-09 05:10:20
73	51	17	1	2021-08-02 23:46:51	2022-01-28 09:08:06
74	49	68	1	2021-08-03 14:47:22	2022-10-11 21:49:58
75	24	76	2	2021-08-01 16:33:20	2023-03-12 09:46:15
76	32	54	3	2021-08-02 04:12:35	2021-12-17 20:32:20
77	89	19	3	2021-08-02 16:48:32	2022-05-23 23:23:25
78	32	36	3	2021-08-01 11:15:23	2022-08-25 16:36:14
79	46	64	2	2021-08-04 00:57:56	2023-06-17 22:42:44
80	94	72	2	2021-08-03 12:56:21	2023-01-27 09:22:04
81	18	35	1	2021-08-01 14:58:15	2022-06-01 04:45:16
82	13	28	3	2021-08-02 09:22:16	2022-04-17 23:02:07
83	13	76	3	2021-08-02 04:42:10	2022-12-16 20:10:40
84	40	32	2	2021-08-01 18:28:24	2023-03-05 05:45:53
85	3	27	1	2021-08-02 06:34:42	2023-01-16 20:08:02
86	44	96	2	2021-08-03 15:13:55	2023-07-06 23:04:18
87	11	50	3	2021-08-03 12:59:06	2023-06-18 07:42:33
88	57	89	2	2021-08-03 03:23:32	2023-05-14 08:25:06
89	20	70	2	2021-08-02 23:16:44	2022-03-27 12:32:53
90	18	14	2	2021-08-02 09:31:41	2022-10-19 22:42:40
91	70	99	2	2021-08-02 12:49:49	2021-12-06 19:58:18
92	48	38	2	2021-08-04 03:55:40	2023-01-28 18:03:33
93	46	91	3	2021-08-04 00:59:43	2022-04-14 15:59:30
94	1	14	3	2021-08-03 02:09:43	2022-01-19 05:42:01
95	25	57	2	2021-08-01 22:29:08	2022-11-10 18:08:41
96	21	42	3	2021-08-03 08:09:27	2021-12-17 20:15:25
97	17	85	2	2021-08-03 19:25:10	2022-08-28 03:16:44
98	23	77	2	2021-08-03 07:00:39	2022-10-14 13:57:59
99	79	85	3	2021-08-03 15:07:35	2022-06-10 03:16:23
100	92	53	2	2021-08-02 01:43:24	2022-03-19 10:07:37
\.


--
-- Data for Name: friendship_statuses; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.friendship_statuses (id, name) FROM stdin;
1	запрошено
2	подтверждено
3	отклонено
\.


--
-- Data for Name: messages; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.messages (id, from_user_id, to_user_id, body, is_important, is_delivered, created_at) FROM stdin;
1	1	69	molestie tortor nibh sit amet orci. Ut sagittis lobortis mauris. Suspendisse aliquet molestie tellus. Aenean egestas hendrerit neque.	f	f	2021-08-31 23:18:32
2	78	87	augue eu tellus. Phasellus	f	f	2022-06-04 17:27:11
3	14	98	vehicula aliquet libero. Integer in magna. Phasellus dolor elit, pellentesque a, facilisis	f	f	2022-11-14 19:09:49
4	93	40	Duis mi enim, condimentum eget, volutpat ornare, facilisis eget, ipsum. Donec sollicitudin adipiscing ligula. Aenean gravida nunc	f	t	2022-08-26 06:39:59
5	52	93	in, cursus et, eros. Proin ultrices. Duis volutpat nunc sit amet	t	f	2023-02-01 20:07:33
6	95	11	cursus a, enim. Suspendisse aliquet, sem ut	f	f	2023-01-01 19:14:17
7	97	56	varius ultrices, mauris ipsum porta elit, a feugiat tellus lorem eu metus. In lorem. Donec elementum, lorem	f	t	2023-03-26 19:00:09
8	52	73	lorem ipsum sodales purus, in molestie tortor nibh	t	f	2023-07-24 10:41:52
9	19	83	justo faucibus lectus, a sollicitudin orci sem eget massa. Suspendisse eleifend. Cras sed leo. Cras vehicula aliquet libero. Integer in	t	f	2022-07-26 08:16:08
10	99	37	placerat, augue. Sed molestie. Sed id risus quis diam luctus lobortis. Class aptent taciti sociosqu	t	f	2022-01-08 23:20:52
11	52	49	ad litora torquent per conubia nostra, per	f	t	2022-08-02 21:12:35
12	41	68	Proin vel nisl. Quisque fringilla euismod enim. Etiam gravida molestie arcu.	f	f	2023-05-09 18:21:39
13	32	35	mauris erat eget	t	t	2022-02-06 11:21:56
14	52	23	rutrum urna, nec	f	f	2022-06-01 19:20:55
15	77	15	magna. Sed eu eros. Nam consequat dolor vitae dolor. Donec fringilla. Donec feugiat metus sit	t	f	2023-01-06 12:25:05
16	13	66	diam at pretium aliquet, metus urna convallis	f	f	2022-12-15 03:10:03
17	23	70	Vivamus molestie dapibus ligula. Aliquam erat volutpat. Nulla dignissim. Maecenas ornare egestas ligula. Nullam	f	f	2022-09-11 22:39:19
18	48	80	cursus non, egestas a, dui. Cras pellentesque. Sed dictum. Proin eget odio. Aliquam vulputate ullamcorper magna.	f	t	2022-09-02 03:54:20
19	17	24	pharetra, felis eget varius ultrices, mauris ipsum porta elit, a	t	t	2021-12-12 03:30:14
20	91	88	odio tristique pharetra. Quisque ac libero nec ligula consectetuer rhoncus. Nullam velit dui, semper et, lacinia vitae,	f	t	2023-07-10 14:34:24
21	84	35	mi	t	f	2021-10-01 13:28:12
22	59	3	scelerisque, lorem ipsum sodales purus, in molestie tortor nibh sit amet orci. Ut sagittis lobortis mauris. Suspendisse aliquet	f	t	2021-11-16 17:43:43
23	46	4	quis accumsan convallis, ante lectus convallis est, vitae sodales nisi magna sed dui.	f	t	2021-08-11 07:31:43
24	66	30	sed, sapien. Nunc	t	f	2022-11-23 03:18:17
25	93	63	feugiat tellus lorem eu metus. In lorem.	f	t	2023-01-30 18:44:09
26	26	4	Duis at lacus. Quisque purus sapien, gravida non, sollicitudin a, malesuada id,	f	t	2021-12-08 20:51:45
27	61	43	Mauris non dui nec urna suscipit nonummy. Fusce fermentum fermentum arcu. Vestibulum ante ipsum	t	t	2023-02-23 05:41:46
28	51	29	lobortis, nisi nibh lacinia orci, consectetuer euismod est arcu ac orci. Ut semper pretium	t	f	2021-12-23 05:07:29
29	14	70	placerat, augue.	f	f	2022-06-23 18:23:34
30	67	96	Duis mi enim, condimentum	f	f	2023-07-12 23:45:31
31	71	17	eu, ultrices sit amet, risus. Donec nibh enim, gravida sit	f	f	2022-05-08 02:16:12
32	41	8	nunc nulla vulputate dui,	f	t	2022-03-28 05:30:37
33	38	64	lorem, luctus ut, pellentesque eget, dictum placerat, augue. Sed molestie.	f	f	2022-11-24 10:24:54
34	75	32	sed orci lobortis augue scelerisque mollis. Phasellus libero mauris, aliquam eu, accumsan sed, facilisis vitae,	t	f	2022-12-14 12:42:32
35	58	15	nibh enim, gravida sit amet, dapibus id, blandit at, nisi.	t	t	2021-08-14 07:52:17
36	36	71	nec, mollis vitae, posuere at, velit. Cras lorem lorem, luctus ut, pellentesque eget, dictum placerat, augue.	f	f	2023-08-03 23:08:09
37	6	15	molestie orci tincidunt adipiscing. Mauris molestie pharetra nibh. Aliquam ornare, libero at auctor ullamcorper, nisl	t	t	2023-04-11 16:24:08
38	49	46	id, ante. Nunc mauris sapien, cursus in, hendrerit consectetuer, cursus et, magna. Praesent interdum ligula eu	f	f	2023-01-02 08:33:14
39	48	97	massa non ante bibendum ullamcorper. Duis cursus, diam at pretium aliquet,	f	t	2023-06-16 02:11:50
40	77	75	rutrum lorem ac risus. Morbi metus. Vivamus euismod urna. Nullam lobortis quam a felis ullamcorper viverra. Maecenas iaculis aliquet	f	f	2022-09-24 15:10:26
41	46	73	Suspendisse dui. Fusce diam	f	f	2023-04-10 02:15:18
42	94	30	eu eros. Nam consequat dolor vitae dolor. Donec fringilla. Donec feugiat metus	f	t	2022-08-02 10:47:21
43	63	9	massa. Suspendisse eleifend. Cras sed leo. Cras vehicula aliquet libero. Integer	f	f	2022-06-23 04:09:08
44	77	97	amet, dapibus id, blandit at, nisi. Cum sociis natoque penatibus	f	f	2021-10-29 22:21:25
45	93	60	torquent per conubia nostra, per inceptos hymenaeos. Mauris ut quam vel sapien imperdiet ornare. In faucibus. Morbi vehicula. Pellentesque tincidunt	t	t	2021-12-28 15:21:13
46	57	82	et, rutrum non, hendrerit id, ante. Nunc mauris sapien, cursus in, hendrerit consectetuer,	f	f	2022-12-30 06:46:22
47	72	77	nec metus facilisis lorem	t	t	2022-04-03 15:05:34
48	39	94	et magnis dis parturient montes, nascetur ridiculus mus. Proin vel arcu eu odio tristique pharetra.	t	t	2022-03-26 09:25:27
49	44	66	tempus, lorem fringilla ornare	f	f	2022-09-29 06:12:25
50	16	18	Donec egestas. Aliquam nec enim. Nunc ut erat. Sed nunc	f	t	2022-08-02 16:46:05
51	23	49	erat nonummy ultricies ornare, elit	t	t	2023-04-09 01:16:10
52	10	52	ipsum non arcu. Vivamus sit amet risus. Donec egestas. Aliquam nec enim. Nunc	t	f	2023-08-06 14:05:37
53	21	71	magna a tortor.	t	t	2022-09-30 10:54:33
54	6	67	ullamcorper magna. Sed eu eros. Nam consequat dolor vitae dolor. Donec fringilla. Donec feugiat metus	t	f	2023-01-31 17:27:11
55	51	27	orci luctus et ultrices posuere cubilia Curae Donec tincidunt. Donec vitae erat vel pede blandit congue. In scelerisque scelerisque	t	t	2023-04-22 10:10:46
56	15	22	ante bibendum ullamcorper. Duis cursus, diam at pretium aliquet, metus urna convallis erat, eget	t	f	2022-09-15 02:12:30
57	20	15	aliquet lobortis, nisi nibh lacinia orci, consectetuer euismod est arcu ac orci. Ut semper pretium neque. Morbi quis	t	f	2023-01-03 12:10:44
58	61	38	hymenaeos. Mauris ut quam	t	t	2023-06-22 14:41:38
59	24	87	fringilla purus mauris a nunc. In at pede. Cras vulputate velit eu sem. Pellentesque ut	f	t	2022-10-03 19:10:05
60	26	41	scelerisque scelerisque dui. Suspendisse ac metus vitae velit egestas lacinia. Sed congue, elit sed consequat	t	f	2021-10-04 13:38:07
61	13	75	ut aliquam iaculis,	f	f	2022-07-03 08:39:38
62	57	63	Etiam ligula tortor, dictum eu, placerat eget, venenatis a, magna. Lorem ipsum	f	t	2021-09-30 01:27:40
63	98	32	Praesent eu dui. Cum sociis natoque penatibus et magnis dis parturient	f	f	2022-12-04 21:37:52
64	57	79	elementum, lorem ut aliquam iaculis, lacus pede sagittis augue, eu	f	f	2022-04-18 14:46:10
65	9	79	Duis ac arcu. Nunc mauris.	f	t	2022-08-04 15:27:18
66	81	95	sed tortor. Integer aliquam adipiscing lacus. Ut nec urna et arcu imperdiet ullamcorper.	t	f	2022-05-01 02:38:38
67	27	33	fringilla est. Mauris eu turpis. Nulla aliquet. Proin velit. Sed malesuada augue ut lacus. Nulla tincidunt, neque vitae	f	t	2023-02-04 15:10:20
68	38	98	Duis ac arcu. Nunc mauris. Morbi non sapien	f	f	2022-11-13 00:41:38
69	54	77	magna. Praesent interdum ligula eu enim. Etiam imperdiet dictum	t	t	2021-08-12 12:16:59
70	30	15	nibh.	t	t	2022-10-18 17:53:17
71	2	84	nulla. Integer urna. Vivamus molestie dapibus ligula. Aliquam erat volutpat. Nulla dignissim. Maecenas ornare egestas ligula. Nullam	f	f	2022-06-05 00:30:06
72	76	28	vel est tempor bibendum. Donec felis orci, adipiscing non, luctus sit amet, faucibus	f	t	2023-06-03 22:28:29
73	80	55	in, dolor. Fusce feugiat. Lorem ipsum dolor sit amet,	f	f	2022-11-01 21:35:23
74	76	17	Nunc lectus pede, ultrices a,	f	f	2022-10-04 21:43:32
75	59	96	Fusce feugiat. Lorem ipsum dolor sit amet,	f	f	2023-01-15 23:46:39
76	87	15	mauris, aliquam eu, accumsan sed, facilisis vitae,	t	f	2021-10-14 09:08:37
77	29	35	eleifend egestas. Sed pharetra, felis eget varius ultrices, mauris ipsum porta elit, a feugiat tellus lorem	t	t	2022-09-29 22:35:08
78	76	27	nibh. Quisque nonummy ipsum non arcu. Vivamus sit amet risus. Donec egestas.	t	t	2022-10-07 09:09:00
79	85	68	ornare, elit elit fermentum risus, at fringilla	t	f	2023-01-20 19:59:45
80	27	30	egestas, urna justo faucibus lectus, a sollicitudin orci sem eget massa. Suspendisse	t	f	2023-05-11 07:49:50
81	63	89	arcu. Morbi sit amet massa. Quisque	t	f	2022-11-15 16:46:07
82	45	84	vitae, sodales at, velit. Pellentesque ultricies dignissim lacus. Aliquam rutrum	t	t	2022-02-05 12:22:26
83	30	100	sem. Nulla interdum. Curabitur dictum. Phasellus in felis. Nulla tempor augue ac ipsum. Phasellus vitae mauris sit	t	f	2022-10-31 22:27:57
84	10	56	cubilia Curae Phasellus ornare. Fusce mollis. Duis sit amet diam eu dolor egestas rhoncus. Proin nisl sem, consequat	t	f	2022-05-31 14:46:38
85	90	81	magnis dis	t	f	2022-11-19 00:57:30
86	16	58	dui. Fusce diam	t	t	2022-02-02 22:39:26
87	19	86	sagittis lobortis mauris. Suspendisse aliquet molestie tellus. Aenean egestas	f	t	2021-12-19 01:02:58
88	35	50	Sed nunc est, mollis non, cursus non, egestas a, dui. Cras pellentesque. Sed dictum.	f	f	2022-08-07 04:34:32
89	29	32	Proin vel	t	f	2021-08-29 07:17:08
90	69	77	fringilla mi lacinia mattis. Integer eu lacus. Quisque imperdiet, erat nonummy ultricies ornare, elit elit fermentum risus, at fringilla	t	t	2021-11-15 12:51:05
91	69	77	et ultrices posuere	t	t	2022-01-26 20:52:48
92	73	59	posuere cubilia Curae Donec tincidunt. Donec vitae erat vel pede blandit congue. In scelerisque	f	f	2022-12-16 13:52:54
93	99	60	Aliquam ultrices iaculis odio. Nam interdum enim non nisi. Aenean eget metus. In nec orci. Donec nibh. Quisque nonummy ipsum	t	f	2023-01-30 20:54:35
94	29	70	nec, mollis vitae, posuere at, velit. Cras lorem lorem, luctus ut, pellentesque eget, dictum	t	t	2023-02-05 13:12:49
95	63	31	libero et tristique pellentesque, tellus sem mollis dui, in sodales elit erat vitae risus. Duis a mi fringilla mi	f	f	2023-06-28 18:13:57
96	81	62	a, magna. Lorem ipsum dolor sit amet, consectetuer adipiscing	f	f	2022-05-08 04:18:28
97	96	14	ultrices iaculis odio. Nam interdum enim non nisi. Aenean eget metus. In nec orci. Donec nibh. Quisque	f	t	2021-11-29 03:01:07
98	33	4	sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec	t	f	2023-07-31 22:40:46
99	27	57	id, blandit	t	t	2021-09-26 06:07:44
100	65	3	semper cursus. Integer mollis.	f	t	2022-06-30 10:28:41
\.


--
-- Data for Name: photo; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.photo (id, url, owner_id, description, uploaded_at, size) FROM stdin;
1	https://myimages.com/lyqjaiybuhcwydj	71	sed orci lobortis augue scelerisque mollis. Phasellus libero mauris, aliquam	2022-08-07 04:35:40	161
2	https://myimages.com/nuldwffmfvzlvod	16	lectus sit amet luctus vulputate,	2022-08-08 04:41:15	482
3	http://myimages.com/mvfatifttmxgqya	92	convallis in, cursus et, eros. Proin ultrices.	2022-08-04 16:16:22	712
4	https://myimages.com/copfnnyfzzdnxgp	84	elementum at, egestas a, scelerisque sed, sapien. Nunc pulvinar arcu	2022-08-04 14:34:05	628
5	https://myimages.com/pxqbvkqosvnhpfs	34	scelerisque scelerisque dui. Suspendisse ac metus	2022-08-07 00:54:14	643
6	http://myimages.com/ikjzxefydnnjtix	66	porta elit, a feugiat tellus lorem eu metus. In	2022-08-07 20:17:55	887
7	http://myimages.com/xmjwnjzqrzkvxix	7	ac, eleifend	2022-08-06 02:58:58	942
8	http://myimages.com/htldfszrqkvixjq	71	ac, feugiat non, lobortis	2022-08-05 21:43:39	338
9	http://myimages.com/dkecfyuhmzfctna	53	ipsum ac mi eleifend	2022-08-02 01:05:10	227
10	https://myimages.com/viudyjujjglhzse	34	egestas. Aliquam nec enim. Nunc ut erat. Sed nunc est,	2022-08-06 20:53:52	764
11	https://myimages.com/jtgtxggvscyqnxh	2	accumsan convallis, ante lectus convallis est, vitae	2022-08-08 04:32:11	990
12	http://myimages.com/pfkllvgqamfzkht	32	Sed neque. Sed eget lacus. Mauris non	2022-08-02 21:52:48	602
13	https://myimages.com/oznyhvgestouuuw	7	aliquet libero. Integer in	2022-08-01 22:17:06	363
14	http://myimages.com/acegrpwpvcxmeum	36	sed pede.	2022-08-01 11:37:48	498
15	http://myimages.com/evxrnssynxtzslf	19	lectus. Cum sociis natoque penatibus et magnis dis	2022-08-07 13:46:04	143
16	https://myimages.com/aghlyjeqzunurit	92	lectus sit amet luctus vulputate,	2022-08-06 15:45:26	781
17	http://myimages.com/rprlqlyvqtemipk	88	eu arcu. Morbi	2022-08-02 13:54:06	176
18	http://myimages.com/uctxyompfbstnbw	4	enim diam vel arcu. Curabitur ut odio	2022-08-02 20:45:30	915
19	http://myimages.com/awebbzbhwcablis	5	elit, dictum eu, eleifend nec, malesuada	2022-08-03 05:36:13	193
20	https://myimages.com/cinnxjuxzexeyqy	41	non, cursus non,	2022-08-01 19:46:31	475
21	https://myimages.com/qnmsssfvahswdqg	60	fermentum fermentum arcu. Vestibulum ante ipsum primis	2022-08-01 22:31:49	525
22	http://myimages.com/zedmqtflikkjbli	46	Integer id magna et ipsum cursus vestibulum. Mauris magna.	2022-08-03 10:44:25	404
23	https://myimages.com/yfpdlbttcqjwxlh	40	a, facilisis non, bibendum sed, est. Nunc laoreet lectus	2022-08-03 10:01:42	579
24	http://myimages.com/yffvoddcdffqvph	6	nec luctus felis purus ac tellus. Suspendisse sed dolor.	2022-08-08 00:35:35	974
25	https://myimages.com/rhhvhhinhyhgskp	32	luctus. Curabitur egestas nunc sed libero. Proin sed turpis nec	2022-08-03 00:16:47	744
26	http://myimages.com/dqwljenygdcnupc	48	vel, vulputate eu, odio. Phasellus	2022-08-05 19:14:40	134
27	https://myimages.com/yxfuojxhjmbiign	5	Aliquam	2022-08-05 01:38:46	959
28	https://myimages.com/bpksppxhzvneozk	69	eget laoreet posuere, enim nisl elementum purus,	2022-08-05 04:51:43	155
29	http://myimages.com/mxhezopgaxsdlbs	14	pede, malesuada	2022-08-08 08:45:08	477
30	https://myimages.com/lcrqjilgejhembu	93	tempus mauris erat eget ipsum. Suspendisse sagittis. Nullam vitae	2022-08-07 05:13:13	968
31	https://myimages.com/ldtlupkvdrleeko	56	magna. Duis dignissim	2022-08-05 01:43:26	342
32	http://myimages.com/riimjfqmendnzzl	92	neque. In ornare sagittis felis. Donec	2022-08-01 20:13:24	886
33	http://myimages.com/znyluksypteayiz	29	augue ac ipsum. Phasellus vitae mauris sit amet lorem	2022-08-05 13:58:41	735
34	https://myimages.com/jtxxfmdtbkxfeat	49	quam quis diam. Pellentesque habitant morbi tristique	2022-08-05 04:20:18	971
35	https://myimages.com/kmtodvowilvokxm	94	mi. Aliquam	2022-08-02 01:10:18	958
36	https://myimages.com/swuveessswsoksh	56	neque. Morbi quis urna. Nunc quis arcu vel	2022-08-03 05:07:55	624
37	http://myimages.com/svpnjcornwciziq	84	ac facilisis facilisis, magna tellus faucibus	2022-08-06 02:33:00	385
38	http://myimages.com/vvxevikowbebwbs	48	sed libero. Proin sed turpis nec mauris blandit	2022-08-05 06:53:09	486
39	http://myimages.com/evomsnkqjfhlksi	56	non dui nec urna suscipit nonummy. Fusce	2022-08-05 20:22:18	941
40	http://myimages.com/wshrjycmmbtknta	5	ipsum porta elit, a	2022-08-02 12:10:06	589
41	https://myimages.com/cewufwhibhmnbnl	20	vulputate, lacus. Cras interdum. Nunc sollicitudin commodo ipsum. Suspendisse	2022-08-07 04:01:15	351
42	https://myimages.com/ahkcsaeqzwsuvku	98	a, malesuada id, erat. Etiam vestibulum massa rutrum	2022-08-02 10:15:19	106
43	http://myimages.com/zupqvupkpepgymi	1	dolor.	2022-08-03 02:37:13	192
44	https://myimages.com/ehdxwfwjabxtiml	85	Donec	2022-08-05 07:56:35	409
45	http://myimages.com/xzolcnssmgovnnv	38	lacus vestibulum lorem, sit	2022-08-02 14:38:39	896
46	https://myimages.com/znexrmfdsbzepie	61	Proin vel arcu eu odio	2022-08-04 04:44:52	267
47	http://myimages.com/yockucpjfsdylba	41	risus quis diam luctus lobortis.	2022-08-02 02:56:41	604
48	https://myimages.com/ouwjacqufhlecyc	85	Morbi sit amet massa. Quisque porttitor eros nec	2022-08-07 02:03:57	359
49	https://myimages.com/wtcmqblgviwdnnu	69	auctor	2022-08-04 19:54:03	646
50	https://myimages.com/zimdrxcfrvgsafn	71	Quisque ornare tortor at risus. Nunc ac sem	2022-08-04 15:00:04	521
51	https://myimages.com/hhqrkzwtajtdzfp	84	magna. Duis dignissim tempor arcu. Vestibulum ut	2022-08-04 09:54:46	801
52	http://myimages.com/itldwfgrrfosmth	52	vulputate mauris sagittis placerat. Cras dictum ultricies	2022-08-02 03:23:48	592
53	http://myimages.com/krsyvtonkcbvlyw	87	enim nisl elementum	2022-08-05 06:29:27	393
54	http://myimages.com/fmmglpjvefcxvhv	38	lorem, eget mollis lectus pede et	2022-08-02 02:15:22	450
55	http://myimages.com/puvwebioixeurvh	19	at pretium aliquet, metus	2022-08-02 15:43:43	219
56	https://myimages.com/vdozvwdrwwxsbfy	9	Nulla aliquet. Proin	2022-08-01 21:51:53	407
57	http://myimages.com/akqzkoirmoilqwe	58	vulputate ullamcorper magna. Sed	2022-08-06 16:50:47	343
58	https://myimages.com/krzihgmzxbggxza	63	Donec fringilla.	2022-08-03 22:47:58	420
59	http://myimages.com/pxqbvkqosvnhpfs	86	metus. Aliquam erat volutpat. Nulla	2022-08-06 15:28:55	372
60	http://myimages.com/qzatmnyjwcjswgy	70	adipiscing ligula. Aenean	2022-08-04 08:54:27	698
61	https://myimages.com/jwfematztbvspuz	64	bibendum. Donec felis	2022-08-05 03:44:31	822
62	https://myimages.com/lhjzaeuvqbuulyp	23	laoreet ipsum. Curabitur consequat, lectus sit	2022-08-05 02:39:58	572
63	http://myimages.com/gwnzgstuwzmusid	2	Nam ac nulla. In tincidunt congue turpis.	2022-08-01 17:27:24	499
64	http://myimages.com/edjoexffdeunfun	4	aliquam eu, accumsan sed, facilisis vitae, orci. Phasellus dapibus	2022-08-07 13:45:19	780
65	http://myimages.com/kspplocabnfmgfg	66	a mi fringilla mi lacinia mattis. Integer eu lacus.	2022-08-03 12:05:32	732
66	https://myimages.com/gxkshnecxeoznzh	30	quis arcu vel quam dignissim	2022-08-04 09:13:37	438
67	https://myimages.com/ayipzaarnsfilsx	49	massa. Quisque porttitor eros nec tellus.	2022-08-07 06:31:50	100
68	https://myimages.com/rfbtwuxslasdjtz	65	lectus ante dictum mi, ac mattis velit justo	2022-08-06 17:32:15	111
69	http://myimages.com/lopclijcwtgianp	36	a, auctor	2022-08-01 20:10:19	505
70	https://myimages.com/fsftonjcfqnixil	25	ut odio vel est tempor bibendum. Donec felis	2022-08-07 19:19:07	218
71	https://myimages.com/crzpbdgusmmrwrh	3	tincidunt, nunc ac mattis ornare, lectus	2022-08-05 15:38:29	670
72	http://myimages.com/iokrwrkueufyrab	46	ante lectus convallis est, vitae	2022-08-02 21:55:10	683
73	http://myimages.com/qvrvhnpupjtsott	23	vulputate, nisi sem semper	2022-08-04 01:50:01	841
74	http://myimages.com/xnfgiyyixiufgpr	65	tristique senectus et netus	2022-08-01 17:50:09	693
75	http://myimages.com/dlzmqgzgjnyryjp	46	quis, pede.	2022-08-06 03:11:43	403
76	http://myimages.com/rjmwlbxxucwxayw	39	consequat enim diam vel arcu. Curabitur ut odio	2022-08-04 06:23:07	933
77	https://myimages.com/isulqycmfydcbfs	26	quis arcu vel quam dignissim	2022-08-06 12:21:08	980
78	https://myimages.com/rhgrmytjouryyrl	55	in, tempus	2022-08-06 01:34:15	968
79	http://myimages.com/eovihtvvbpfnlhp	22	faucibus. Morbi vehicula. Pellentesque tincidunt tempus risus. Donec egestas.	2022-08-01 10:54:00	816
80	https://myimages.com/volcxtmyyizpjhj	33	lorem, eget mollis lectus	2022-08-06 07:54:59	761
81	https://myimages.com/fxkptpygjkkbkll	20	felis, adipiscing fringilla, porttitor vulputate,	2022-08-04 02:54:24	137
82	http://myimages.com/lyoumfkxgjsduhp	90	In scelerisque scelerisque dui. Suspendisse ac metus vitae	2022-08-06 11:50:29	651
83	http://myimages.com/redlesnxvxapekl	99	lacus, varius et, euismod et, commodo at, libero. Morbi	2022-08-05 16:56:01	782
84	https://myimages.com/jvavowfpocphltr	48	tortor. Integer aliquam adipiscing lacus.	2022-08-07 19:39:00	132
85	https://myimages.com/pxgkzqlevbxncck	62	sed tortor. Integer	2022-08-02 11:05:12	259
86	https://myimages.com/kcvcaiojvovpoum	57	libero. Proin mi. Aliquam gravida mauris ut	2022-08-01 11:32:08	863
87	https://myimages.com/cmkppztahrrafrk	96	ut erat. Sed	2022-08-05 22:17:44	717
88	http://myimages.com/nqwapnmhnmvvlsf	79	eu turpis. Nulla aliquet.	2022-08-06 07:04:16	379
89	http://myimages.com/cnulqjiizwixeab	57	leo. Cras vehicula aliquet libero. Integer in	2022-08-05 15:51:03	821
90	https://myimages.com/hhhpldgfrwljptk	35	tristique aliquet. Phasellus fermentum	2022-08-04 08:26:05	442
91	https://myimages.com/jysreibteuesjmw	61	risus odio, auctor vitae,	2022-08-06 15:39:21	507
92	https://myimages.com/uzuzgpugumrjoze	28	Nunc laoreet lectus quis massa. Mauris vestibulum, neque sed	2022-08-08 06:59:35	534
93	http://myimages.com/vrjtofbxrdoglxk	5	ligula. Aliquam erat volutpat. Nulla dignissim.	2022-08-04 05:52:16	279
94	https://myimages.com/mhuorarmaqswonm	69	aliquam iaculis,	2022-08-04 00:22:14	908
95	https://myimages.com/rhhvhhinhyhgskj	87	erat volutpat.	2022-08-05 22:11:09	409
96	http://myimages.com/tfeyfoaoayasjef	77	enim. Curabitur massa. Vestibulum accumsan neque et nunc. Quisque	2022-08-02 18:09:16	710
97	http://myimages.com/klqrvzxwlfcgiqr	43	a, aliquet	2022-08-02 08:17:54	940
98	https://myimages.com/xcrstizykwjmmwe	88	convallis est,	2022-08-01 12:47:17	635
99	http://myimages.com/jikyenkeouglgsv	94	dolor. Fusce mi lorem, vehicula et, rutrum eu, ultrices	2022-08-08 10:17:02	625
100	http://myimages.com/qykfrcmexblsrux	28	nec, imperdiet nec, leo. Morbi neque tellus, imperdiet	2022-08-07 21:26:01	678
\.


--
-- Data for Name: subscribers_users; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.subscribers_users (user_id, subscriber_id, subscribed_at) FROM stdin;
6	1	2023-07-11 14:25:08
94	8	2021-10-22 10:16:09
95	44	2022-12-15 23:15:27
34	79	2021-12-17 02:28:07
41	74	2023-05-19 08:09:25
14	82	2022-01-06 03:32:57
10	99	2022-01-18 08:25:33
77	5	2021-08-13 04:30:45
50	83	2023-04-11 12:14:52
95	81	2023-01-31 21:52:48
72	59	2022-09-04 04:27:29
27	53	2023-05-24 15:29:11
85	56	2023-01-26 23:53:59
92	75	2021-11-17 08:24:59
22	22	2021-10-18 18:08:42
66	78	2022-06-13 21:04:20
44	43	2022-03-26 17:29:03
90	24	2022-02-23 08:57:20
48	20	2022-12-12 07:45:10
73	56	2022-12-20 22:31:22
28	86	2022-05-01 23:26:48
26	38	2022-07-19 05:38:59
12	73	2022-12-20 08:54:15
37	16	2022-06-16 12:56:08
73	48	2023-02-01 20:11:28
75	61	2023-02-05 10:47:06
94	34	2023-02-23 00:19:13
53	73	2022-08-02 04:05:42
73	62	2022-01-02 02:07:40
59	65	2023-08-02 15:32:07
12	10	2022-04-05 18:59:34
84	17	2021-09-17 12:26:45
8	38	2023-05-10 23:20:35
18	47	2023-04-17 13:27:19
45	41	2021-08-05 06:28:47
20	12	2022-05-27 01:24:58
74	32	2022-04-28 01:25:38
62	63	2022-03-08 12:45:32
26	6	2022-05-07 12:56:19
28	15	2022-02-07 00:29:26
74	28	2023-01-15 02:57:18
46	21	2022-04-10 16:28:05
66	93	2022-06-24 16:25:39
1	37	2023-01-15 09:12:48
41	95	2023-05-30 01:47:57
43	54	2022-08-06 17:49:26
50	14	2022-01-30 03:21:27
12	43	2022-09-28 12:34:12
67	80	2023-02-09 20:55:16
46	81	2023-04-13 06:18:39
33	45	2022-04-13 04:38:30
92	20	2023-07-13 11:09:26
51	7	2023-06-09 19:46:45
89	57	2022-03-24 09:37:42
13	47	2021-11-19 05:00:46
45	32	2021-11-27 16:52:48
94	57	2023-06-24 15:04:25
10	31	2023-01-25 01:02:39
12	48	2022-04-08 03:37:03
85	80	2021-11-16 11:53:01
56	77	2021-08-29 16:22:38
74	88	2021-08-16 15:09:30
23	6	2022-07-30 12:47:51
16	46	2021-12-26 09:29:00
52	30	2022-12-23 09:02:37
43	28	2022-03-09 23:49:20
42	84	2022-10-31 00:48:23
52	61	2021-09-17 02:54:16
54	66	2022-08-18 05:32:44
68	71	2023-04-16 13:37:13
11	25	2022-12-02 20:49:19
89	25	2022-12-30 10:59:39
99	56	2022-02-24 01:41:20
31	81	2022-10-19 20:32:19
54	77	2022-03-01 04:09:19
29	78	2021-08-05 06:10:16
27	82	2022-05-05 14:23:44
31	65	2022-12-27 23:43:51
43	95	2022-11-05 05:41:50
33	77	2022-02-03 20:56:01
65	95	2021-10-08 14:04:27
98	82	2022-06-10 06:42:40
93	10	2022-05-17 22:53:11
72	64	2022-05-07 13:04:02
81	70	2021-08-05 13:26:38
53	7	2022-04-10 11:16:02
76	12	2021-11-20 22:33:43
93	44	2022-11-21 03:55:25
74	29	2022-08-19 06:38:31
34	72	2021-10-31 19:44:02
96	57	2022-11-16 18:02:26
98	70	2023-07-16 15:29:14
54	63	2023-01-18 06:02:46
24	48	2022-06-27 15:00:40
21	37	2021-11-11 05:43:55
37	86	2023-07-12 16:43:28
18	30	2021-10-03 14:09:48
78	40	2022-04-05 12:15:30
16	29	2022-06-30 17:32:27
6	19	2023-07-20 20:43:08
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.users (id, first_name, last_name, email, phone, main_photo_id, created_at) FROM stdin;
1	Lillian	Salas	ipsum.primis.in@aol.edu	0918 032 5813	27	\N
2	Edward	Dillard	pellentesque.ultricies@outlook.couk	0800 979 3487	76	\N
3	Hiram	Ware	orci.lacus@hotmail.com	0800 255442	18	\N
4	Aiko	William	maecenas@icloud.net	056 8973 4473	75	\N
5	Ronan	Mcknight	dui.fusce.diam@protonmail.org	07215 353467	37	\N
6	Tyrone	Vaughan	ipsum.dolor@icloud.ca	0800 317331	5	\N
7	Cynthia	Middleton	semper.nam@outlook.edu	(01377) 688148	93	\N
8	Jennifer	Salinas	luctus.et.ultrices@protonmail.ca	(0115) 077 4396	74	\N
9	Nicholas	Colon	ultrices@icloud.net	(016977) 8167	89	\N
10	Maite	Vinson	dolor.tempus@outlook.com	0800 793 8687	88	\N
11	Elvis	William	a.sollicitudin.orci@google.net	056 6615 7750	35	\N
12	Hiram	Larson	risus.a.ultricies@aol.edu	(016977) 2137	72	\N
13	Sydney	Fowler	erat.nonummy@google.edu	055 4455 5976	94	\N
14	Warren	Kerr	sapien.cras.dolor@yahoo.ca	0800 395526	7	\N
15	Baxter	Monroe	molestie.in@icloud.edu	07624 659670	5	\N
16	Kathleen	Walters	adipiscing.elit.curabitur@protonmail.couk	(0110) 368 5677	27	\N
17	Isabella	Stark	non@hotmail.ca	070 8112 8011	91	\N
18	Paloma	Juarez	ut.semper@protonmail.com	(0121) 151 1455	27	\N
19	Cora	Charles	lacus@hotmail.org	0800 148 3447	47	\N
20	Jason	Brock	cursus@outlook.couk	(016977) 1171	88	\N
21	Dolan	Mcfarland	varius.orci@icloud.org	070 4426 5028	86	\N
22	Grant	Brooks	pellentesque@google.edu	070 2571 1025	44	\N
23	Beatrice	Salinas	sed@google.couk	0800 1111	53	\N
24	Halla	Hooper	quisque@outlook.edu	055 9677 3012	43	\N
25	Hilel	Fry	commodo@outlook.edu	(01688) 274531	51	\N
26	Christian	Solis	quisque.porttitor@outlook.net	076 4886 7643	74	\N
27	Bruno	Dillard	eros.nam@google.org	0845 46 44	19	\N
28	Aimee	Kane	eu.neque@protonmail.edu	0500 856276	28	\N
29	Bethany	Todd	libero.at@google.org	0845 46 41	54	\N
30	Tyrone	Osborne	curabitur@protonmail.com	(0112) 276 5162	76	\N
31	Lani	Rowland	cras.vulputate@protonmail.ca	070 2268 5677	2	\N
32	Jacob	Steele	sit.amet@protonmail.com	0800 111134	77	\N
33	Evangeline	Dyer	eu.lacus.quisque@icloud.ca	0960 805 1263	83	\N
34	Cameron	Brock	nam.interdum@google.couk	070 7938 3064	91	\N
35	Elaine	Randolph	arcu.sed@yahoo.ca	0814 517 4306	94	\N
36	Carolyn	Wolfe	curae@aol.couk	(01718) 418327	21	\N
37	Lilah	Pratt	enim.commodo.hendrerit@protonmail.couk	(016977) 8267	21	\N
38	Timon	Levine	elementum@aol.edu	055 4206 8667	77	\N
39	Heidi	Hartman	eu.odio.tristique@aol.couk	0302 827 9237	34	\N
40	Cally	Frederick	risus.in@aol.net	07624 225885	8	\N
41	Timothy	Rojas	orci.lobortis@hotmail.edu	0832 202 4987	27	\N
42	Roth	Hunt	condimentum.donec.at@hotmail.ca	0845 46 42	26	\N
43	Kermit	Lindsay	mattis.integer@hotmail.couk	0500 348575	70	\N
44	Solomon	Hickman	laoreet.lectus.quis@icloud.com	(0141) 524 5643	15	\N
45	Daria	Santana	nisl@google.org	(01723) 85726	94	\N
46	Nicholas	Hodges	semper.pretium.neque@icloud.ca	(016977) 2166	24	\N
47	Basia	Booth	auctor@hotmail.com	0884 839 6780	47	\N
48	Lilah	Sweeney	imperdiet@hotmail.org	0800 111123	71	\N
49	Allegra	Howe	nunc.ut@google.com	070 8864 5423	17	\N
50	Jasmine	Boone	porttitor@outlook.org	0500 165447	29	\N
51	Stephanie	Haley	et.pede.nunc@yahoo.edu	076 7226 8156	99	\N
52	Lysandra	Douglas	velit@icloud.couk	0923 428 5322	83	\N
53	Rooney	Montgomery	augue.id@hotmail.ca	07012 547765	30	\N
54	Caleb	Clark	nec.tellus@protonmail.ca	(01053) 653161	31	\N
55	Dolan	Bennett	blandit.viverra@aol.edu	0500 532758	20	\N
56	Sade	Watson	mauris@aol.couk	0915 201 4317	21	\N
57	Wylie	Dawson	proin.vel@google.ca	0500 665181	58	\N
58	Briar	Frye	aliquam@icloud.net	(026) 7425 1502	78	\N
59	Raymond	Richard	egestas.hendrerit@hotmail.couk	0953 137 4416	39	\N
60	Brynn	Hill	ac.feugiat.non@aol.edu	0925 701 9984	67	\N
61	Patrick	Knox	ultricies.ornare@google.org	(028) 7717 8834	5	\N
62	Lance	Mcintyre	massa.non.ante@google.couk	07528 575811	51	\N
63	Alexander	Reeves	lacinia.vitae@icloud.org	07722 184804	78	\N
64	Vance	Mathis	orci.quis@outlook.edu	055 5330 2636	53	\N
65	Lance	Wise	tortor@hotmail.ca	(0114) 144 4962	81	\N
66	Owen	Levy	velit.pellentesque@google.org	07624 824708	33	\N
67	Lani	Walls	arcu.vel.quam@icloud.edu	07624 875352	75	\N
68	Seth	Oneal	orci.tincidunt@icloud.org	0800 111176	51	\N
69	Ava	Gray	leo.cras@google.com	0800 640351	44	\N
70	Scarlett	Ellison	neque@icloud.com	076 2675 1862	44	\N
71	Uma	Rivers	pharetra@icloud.couk	076 3206 8226	91	\N
72	Britanney	Duncan	consequat@yahoo.org	(012172) 87994	24	\N
73	Leo	Woodward	donec.nibh@aol.couk	0800 501113	79	\N
74	Althea	Dickson	sit@hotmail.couk	076 7289 8296	25	\N
75	Lynn	Salinas	non.ante@aol.ca	(011611) 57770	33	\N
76	Zane	Holloway	tellus@outlook.couk	0800 368316	83	\N
77	Irene	Key	mattis.velit.justo@aol.com	0365 322 2168	35	\N
78	Hiram	Dickson	magna.malesuada@google.couk	(0131) 052 0456	95	\N
79	Nyssa	Robbins	quisque.varius@protonmail.net	0800 216 8754	65	\N
80	Evan	Skinner	iaculis.lacus@aol.couk	0500 332866	53	\N
81	Stephen	Bauer	aenean.euismod@yahoo.ca	0800 344829	15	\N
82	Winifred	Cobb	ut.molestie.in@protonmail.ca	0845 46 43	72	\N
83	Burke	Todd	convallis@google.couk	056 4308 5767	98	\N
84	Amela	Browning	sit.amet@hotmail.com	076 6355 5369	6	\N
85	Yoshio	Mccarthy	ac@icloud.com	0500 293071	37	\N
86	Jennifer	Griffin	ipsum.suspendisse@icloud.net	076 7747 8654	35	\N
87	Thane	Campos	cursus.et.eros@icloud.net	(0141) 314 8765	53	\N
88	Maris	Mcpherson	sed.id@yahoo.com	070 7521 3289	34	\N
89	Declan	Figueroa	mauris.sapien@google.ca	0824 504 4137	7	\N
90	Clinton	Sandoval	facilisis@aol.couk	(0115) 285 7311	74	\N
91	Calvin	Combs	enim@aol.couk	0800 584 8428	13	\N
92	Jana	Riddle	augue.ut@protonmail.com	076 5492 1522	14	\N
93	Ian	Dorsey	fusce@hotmail.org	(024) 7719 7646	83	\N
94	Dominic	Stevens	aliquam.nisl@hotmail.edu	(016977) 7029	48	\N
95	Wylie	Dillon	neque.in@aol.edu	0358 097 7135	86	\N
96	Tamekah	Crosby	eu@hotmail.net	0845 46 43 45	69	\N
97	Carson	Logan	nunc.mauris@icloud.ca	076 5865 2626	9	\N
98	Fitzgerald	Blevins	nullam.lobortis@hotmail.com	(0121) 703 1832	83	\N
99	Noelle	Frederick	laoreet@hotmail.edu	056 6870 7469	84	\N
100	Cyrus	Butler	suscipit.est@protonmail.couk	076 7512 5213	24	\N
\.


--
-- Data for Name: video; Type: TABLE DATA; Schema: public; Owner: gb
--

COPY public.video (id, url, owner_id, description, uploaded_at, size) FROM stdin;
1	http://myvideos.com/sixeyixmtnlnzdp	71	convallis convallis dolor. Quisque tincidunt pede ac urna.	2022-08-07 00:40:48	214
2	http://myvideos.com/jjlkkjvxdngscog	35	urna et arcu imperdiet ullamcorper. Duis	2022-08-04 07:49:19	147
3	https://myvideos.com/ygyrcgqyumoldsz	87	mollis non, cursus non, egestas a, dui. Cras	2022-08-03 07:10:59	700
4	https://myvideos.com/qsafmmrlnmoefjg	31	lectus sit	2022-08-02 01:11:41	600
5	https://myvideos.com/bupxfbsipsrnogd	80	Etiam ligula	2022-08-06 01:18:45	768
6	https://myvideos.com/qncuvfvudoyysvn	61	Aenean	2022-08-03 06:23:28	121
7	http://myvideos.com/suvixkwhpbruubc	14	facilisis. Suspendisse commodo tincidunt nibh. Phasellus nulla. Integer vulputate, risus	2022-08-07 12:04:27	655
8	https://myvideos.com/iyrvfkgogjgruda	86	metus. Aliquam erat volutpat. Nulla facilisis. Suspendisse commodo	2022-08-05 09:28:13	264
9	https://myvideos.com/lzlshnvcvdidskd	84	volutpat. Nulla dignissim.	2022-08-06 13:10:52	724
10	https://myvideos.com/ysaolnidfyyjvcr	43	eu, odio. Phasellus at augue id ante dictum	2022-08-03 12:50:35	707
11	https://myvideos.com/lbotxtqtyjpqncf	76	magnis dis parturient	2022-08-08 06:22:14	642
12	https://myvideos.com/wfpyauuhvnngaql	19	convallis ligula. Donec luctus aliquet odio. Etiam ligula tortor, dictum	2022-08-07 10:57:54	736
13	http://myvideos.com/hrknexmumjwktio	23	tellus faucibus leo, in lobortis tellus	2022-08-06 14:17:31	220
14	http://myvideos.com/vcucqeffajcsydu	6	et, rutrum eu, ultrices sit	2022-08-05 00:21:41	936
15	http://myvideos.com/ohfidiosdcqpdaw	81	eu augue porttitor interdum.	2022-08-02 12:21:31	158
16	https://myvideos.com/xhuzqpplcnufsjb	87	ut dolor dapibus gravida. Aliquam tincidunt, nunc ac mattis ornare,	2022-08-02 10:20:32	566
17	http://myvideos.com/piiqvcianlublui	46	Aliquam ultrices iaculis odio. Nam interdum	2022-08-07 22:01:41	294
18	http://myvideos.com/sqcohohaeqwkgxn	68	sapien. Nunc pulvinar arcu et pede. Nunc sed orci	2022-08-07 00:46:19	334
19	https://myvideos.com/wvvbghyetsvitzh	62	eu nulla	2022-08-07 07:09:29	787
20	https://myvideos.com/atqxrvpxklkltga	88	tortor	2022-08-02 18:33:26	250
21	http://myvideos.com/ybupyqvmgudkomc	74	Donec tincidunt. Donec vitae erat vel pede blandit congue.	2022-08-06 03:13:28	217
22	https://myvideos.com/ruggpirlsylrifs	53	Aenean	2022-08-05 07:54:58	680
23	https://myvideos.com/eppvvtdyrrxdlrc	21	cursus. Integer mollis. Integer	2022-08-01 16:39:22	443
24	https://myvideos.com/ndvpvvjdcexjzha	75	Suspendisse sed dolor.	2022-08-08 02:08:09	293
25	https://myvideos.com/nsozuiuzxmrjtbw	46	at, egestas	2022-08-01 11:47:08	416
26	https://myvideos.com/fabainlvqpxakta	26	arcu. Sed et libero. Proin	2022-08-05 19:33:45	256
27	https://myvideos.com/kiktehhzoutmgle	47	arcu eu odio tristique	2022-08-05 19:50:59	408
28	http://myvideos.com/gpsvbreodjbcvqm	14	mattis semper, dui lectus rutrum urna, nec luctus felis	2022-08-05 03:31:30	730
29	http://myvideos.com/uljdsjngeajaprx	47	nec ante.	2022-08-06 13:08:59	133
30	http://myvideos.com/jpvgqejwnlbqbos	74	lorem, luctus ut, pellentesque eget, dictum placerat, augue.	2022-08-07 01:15:32	755
31	https://myvideos.com/erfpvxjfwfkitrz	22	sem, vitae aliquam	2022-08-02 19:38:45	293
32	https://myvideos.com/notqkbnwnxbxais	89	risus. Morbi	2022-08-04 00:32:38	262
33	http://myvideos.com/imtlipgqjmjiknx	23	vestibulum lorem,	2022-08-03 22:50:22	225
34	http://myvideos.com/cxbyppqeocfppcu	25	eros non enim commodo hendrerit. Donec porttitor tellus	2022-08-06 08:57:29	923
35	http://myvideos.com/vjierryafxsqhjz	23	ac ipsum. Phasellus vitae mauris sit	2022-08-07 07:37:56	560
36	http://myvideos.com/oalmqdctilmzlik	98	placerat velit. Quisque	2022-08-03 20:43:31	556
37	http://myvideos.com/dzryrcvbnbyfvyw	25	molestie arcu. Sed eu nibh vulputate	2022-08-05 23:20:09	303
38	http://myvideos.com/fcdrdhalhhnkeqm	98	a mi	2022-08-03 13:38:05	583
39	http://myvideos.com/dczwzvnnhzdjtse	59	Duis elementum, dui quis accumsan convallis, ante lectus convallis	2022-08-03 05:18:14	529
40	http://myvideos.com/xegjjwqytsjmcxw	97	tellus justo sit amet	2022-08-03 16:01:04	728
41	https://myvideos.com/brfmlapbgcvxccq	10	laoreet ipsum.	2022-08-01 10:56:16	470
42	http://myvideos.com/issjlbzpkvcrqrp	48	sit amet	2022-08-04 16:21:22	202
43	http://myvideos.com/saxhesvcbsfrnzj	49	ante. Nunc mauris sapien, cursus in, hendrerit consectetuer, cursus	2022-08-08 01:08:22	718
44	https://myvideos.com/bdrkiswirpqzyjt	39	metus eu erat semper rutrum. Fusce	2022-08-02 02:43:46	247
45	https://myvideos.com/igkqqipgmnonubd	56	odio.	2022-08-03 22:14:35	489
46	https://myvideos.com/yjrxcdwyhkgamwn	6	Nulla tempor	2022-08-06 20:52:31	617
47	https://myvideos.com/fzypeexrqkzyaks	93	nec quam. Curabitur vel lectus.	2022-08-03 18:45:56	189
48	https://myvideos.com/sihblebynfifmgn	90	diam. Sed diam lorem, auctor quis, tristique ac, eleifend	2022-08-04 05:23:10	496
49	http://myvideos.com/izzaddydqykjcbm	26	pede. Cras vulputate velit eu	2022-08-07 06:41:22	941
50	https://myvideos.com/iimadcsxxzuzkgf	89	enim. Mauris quis turpis	2022-08-07 03:38:10	763
51	http://myvideos.com/xpmrckebsnrlfoc	8	aliquet nec, imperdiet nec, leo. Morbi	2022-08-08 08:31:25	370
52	https://myvideos.com/dnorrsnpbupunsd	81	mauris. Suspendisse aliquet molestie tellus. Aenean	2022-08-05 06:48:01	426
53	https://myvideos.com/vnskxuluwelteaz	65	magna. Sed eu eros.	2022-08-04 15:28:32	221
54	https://myvideos.com/wipahwwkjsfshsg	58	diam at pretium aliquet,	2022-08-02 00:36:31	727
55	http://myvideos.com/qtzknzhfszfjqrr	99	est, vitae	2022-08-08 02:21:43	948
56	https://myvideos.com/wnfxxshxhzweddh	69	tincidunt vehicula	2022-08-07 20:33:04	619
57	http://myvideos.com/rqboaqwutbefujf	35	dolor sit amet, consectetuer adipiscing elit. Aliquam auctor, velit	2022-08-06 08:45:36	212
58	https://myvideos.com/eqlnjrieweijwxy	11	Vestibulum ante ipsum primis	2022-08-04 20:09:06	517
59	https://myvideos.com/crdvqtrmbgvqpzy	60	Suspendisse eleifend. Cras sed	2022-08-07 17:09:21	928
60	https://myvideos.com/ylfjodthfpaytbm	22	purus mauris a nunc. In at	2022-08-03 19:58:50	923
61	https://myvideos.com/kunbkdwhjcgtvrh	89	vulputate dui,	2022-08-03 17:11:05	338
62	http://myvideos.com/unsqacpyqyaptct	50	ligula elit,	2022-08-01 18:25:05	808
63	http://myvideos.com/euzpvhdoqccyufn	14	lorem	2022-08-02 01:12:20	316
64	http://myvideos.com/bosggbxglkjrnkp	63	Fusce dolor quam, elementum at, egestas a, scelerisque sed, sapien.	2022-08-03 13:32:35	693
65	http://myvideos.com/jvmlqmvcesjolur	81	amet	2022-08-06 13:05:51	234
66	https://myvideos.com/kneffaynbhipaer	90	magna. Lorem ipsum dolor	2022-08-04 00:26:06	703
67	https://myvideos.com/ixcyobbabyuruxg	3	luctus ut, pellentesque eget, dictum placerat, augue. Sed molestie.	2022-08-08 09:29:17	303
68	http://myvideos.com/xbnoiitspcmpktn	42	Quisque purus sapien, gravida non, sollicitudin	2022-08-07 22:15:20	927
69	http://myvideos.com/zfsjlhjogfvhslo	56	Mauris magna.	2022-08-04 18:13:34	543
70	https://myvideos.com/hmvzzpjnjyjwnlp	81	amet massa. Quisque porttitor eros nec tellus. Nunc lectus	2022-08-08 05:39:32	602
71	http://myvideos.com/wfxoawnbqmidmwo	28	torquent per conubia nostra, per inceptos hymenaeos. Mauris	2022-08-04 05:25:26	902
72	http://myvideos.com/iqjvuqdwpajwnjg	11	ornare placerat, orci lacus vestibulum lorem, sit amet	2022-08-06 20:23:26	822
73	https://myvideos.com/umoizopsidornwf	81	semper et, lacinia vitae, sodales	2022-08-03 18:45:19	173
74	https://myvideos.com/lelnqlvtvvhyisg	44	porttitor	2022-08-03 00:06:31	124
75	http://myvideos.com/bjqakunqbpdhrnv	73	vitae risus. Duis a mi fringilla mi	2022-08-06 10:33:20	372
76	http://myvideos.com/ipuugsposgxqhwc	77	ligula. Aenean gravida nunc sed pede. Cum sociis	2022-08-07 15:59:29	298
77	https://myvideos.com/wzvgcbctioqtwma	51	mauris. Morbi	2022-08-07 10:47:15	980
78	https://myvideos.com/vrecowkfjyydwzw	45	id, libero. Donec consectetuer mauris id sapien.	2022-08-02 08:30:52	388
79	https://myvideos.com/qtltglncnhejrce	19	habitant morbi tristique senectus et netus et malesuada fames	2022-08-05 03:43:15	678
80	http://myvideos.com/xzhmsqaccnhdsus	27	odio vel est tempor bibendum. Donec felis orci, adipiscing non,	2022-08-03 21:42:31	517
81	https://myvideos.com/pyrpjqvrmzwgqwo	70	purus gravida sagittis. Duis gravida. Praesent eu	2022-08-06 07:20:09	573
82	http://myvideos.com/qawxiiyyxoporkm	45	Nunc laoreet lectus quis massa.	2022-08-04 06:24:50	475
83	https://myvideos.com/qkelwxbbdgwpacs	25	volutpat. Nulla dignissim. Maecenas	2022-08-02 02:00:45	347
84	https://myvideos.com/mqliqfkusjsqvcr	41	laoreet, libero et tristique pellentesque,	2022-08-03 20:15:31	545
85	http://myvideos.com/ungdjhlzltdoumq	35	lectus justo eu arcu. Morbi sit amet massa.	2022-08-04 12:17:38	203
86	http://myvideos.com/xpbegqknldrkcrr	93	amet metus. Aliquam erat volutpat. Nulla facilisis. Suspendisse commodo	2022-08-06 18:49:28	989
87	http://myvideos.com/pzftynzggrfpukg	39	urna. Ut tincidunt vehicula risus. Nulla eget metus	2022-08-04 21:40:14	666
88	http://myvideos.com/viodptmsbwhripx	38	diam nunc, ullamcorper eu, euismod ac, fermentum	2022-08-05 10:46:39	221
89	http://myvideos.com/kbhfsczfnixeuop	95	vehicula aliquet libero. Integer in	2022-08-06 07:47:30	696
90	http://myvideos.com/vtdaifsouoljulo	54	amet diam eu	2022-08-02 01:07:50	146
91	https://myvideos.com/aydyuxercxdvwbo	76	pede ac urna. Ut tincidunt vehicula risus. Nulla eget	2022-08-05 04:06:41	467
92	https://myvideos.com/ewncdpjatbewvcs	26	eu lacus. Quisque	2022-08-07 17:21:58	212
93	http://myvideos.com/nnkadfvritwanzs	5	a purus. Duis	2022-08-01 10:37:52	865
94	https://myvideos.com/yyaxentgwknerxv	65	at, velit. Cras lorem lorem, luctus ut, pellentesque	2022-08-05 19:03:50	598
95	https://myvideos.com/odjkbuzyxpfbaur	58	luctus ut, pellentesque eget, dictum placerat, augue. Sed	2022-08-04 19:58:31	194
96	http://myvideos.com/iyitavrwchcvvmh	64	egestas. Sed pharetra, felis eget	2022-08-03 00:51:04	378
97	http://myvideos.com/iaovokkbiuqfcmz	21	lacus. Mauris	2022-08-07 04:56:54	905
98	https://myvideos.com/bqsbxtctgdhdeao	99	ligula. Aenean euismod mauris eu elit. Nulla facilisi. Sed	2022-08-01 12:30:39	751
99	https://myvideos.com/clwfieraiyjnbes	43	hendrerit consectetuer,	2022-08-04 13:07:04	731
100	https://myvideos.com/sufmycipalbsslp	29	et,	2022-08-02 13:32:49	818
\.


--
-- Name: communities_id_seq; Type: SEQUENCE SET; Schema: public; Owner: gb
--

SELECT pg_catalog.setval('public.communities_id_seq', 100, true);


--
-- Name: friendship_id_seq; Type: SEQUENCE SET; Schema: public; Owner: gb
--

SELECT pg_catalog.setval('public.friendship_id_seq', 100, true);


--
-- Name: friendship_statuses_id_seq; Type: SEQUENCE SET; Schema: public; Owner: gb
--

SELECT pg_catalog.setval('public.friendship_statuses_id_seq', 3, true);


--
-- Name: messages_id_seq; Type: SEQUENCE SET; Schema: public; Owner: gb
--

SELECT pg_catalog.setval('public.messages_id_seq', 100, true);


--
-- Name: photo_id_seq; Type: SEQUENCE SET; Schema: public; Owner: gb
--

SELECT pg_catalog.setval('public.photo_id_seq', 100, true);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: gb
--

SELECT pg_catalog.setval('public.users_id_seq', 100, true);


--
-- Name: video_id_seq; Type: SEQUENCE SET; Schema: public; Owner: gb
--

SELECT pg_catalog.setval('public.video_id_seq', 100, true);


--
-- Name: communities communities_name_key; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.communities
    ADD CONSTRAINT communities_name_key UNIQUE (name);


--
-- Name: communities communities_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.communities
    ADD CONSTRAINT communities_pkey PRIMARY KEY (id);


--
-- Name: communities_subscribers communities_subscribers_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.communities_subscribers
    ADD CONSTRAINT communities_subscribers_pkey PRIMARY KEY (community_id, subscriber_id);


--
-- Name: communities_users communities_users_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.communities_users
    ADD CONSTRAINT communities_users_pkey PRIMARY KEY (community_id, user_id);


--
-- Name: friendship friendship_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.friendship
    ADD CONSTRAINT friendship_pkey PRIMARY KEY (id);


--
-- Name: friendship_statuses friendship_statuses_name_key; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.friendship_statuses
    ADD CONSTRAINT friendship_statuses_name_key UNIQUE (name);


--
-- Name: friendship_statuses friendship_statuses_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.friendship_statuses
    ADD CONSTRAINT friendship_statuses_pkey PRIMARY KEY (id);


--
-- Name: messages messages_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.messages
    ADD CONSTRAINT messages_pkey PRIMARY KEY (id);


--
-- Name: photo photo_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.photo
    ADD CONSTRAINT photo_pkey PRIMARY KEY (id);


--
-- Name: photo photo_url_key; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.photo
    ADD CONSTRAINT photo_url_key UNIQUE (url);


--
-- Name: subscribers_users subscribers_users_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.subscribers_users
    ADD CONSTRAINT subscribers_users_pkey PRIMARY KEY (user_id, subscriber_id);


--
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- Name: users users_phone_key; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_phone_key UNIQUE (phone);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: video video_pkey; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.video
    ADD CONSTRAINT video_pkey PRIMARY KEY (id);


--
-- Name: video video_url_key; Type: CONSTRAINT; Schema: public; Owner: gb
--

ALTER TABLE ONLY public.video
    ADD CONSTRAINT video_url_key UNIQUE (url);


--
-- PostgreSQL database dump complete
--

