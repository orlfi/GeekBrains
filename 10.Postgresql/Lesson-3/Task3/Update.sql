UPDATE photo set metadata = json_build_object(
	'id', id,
	'url',url,
	'size',size
);