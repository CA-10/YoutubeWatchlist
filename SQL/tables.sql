CREATE TABLE IF NOT EXISTS videos (
	NAME TEXT,
	NOTES TEXT,
	LINK TEXT,
	CATEGORY_ID TEXT,
	TIMESTAMP_SECONDS INT,
	IMAGE_BASE64 TEXT
);

CREATE TABLE IF NOT EXISTS categories (
	CATEGORY_ID TEXT,
	CATEGORY_NAME TEXT
);