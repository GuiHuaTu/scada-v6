-- project.app definition

-- Drop table

-- DROP TABLE project.app;

CREATE TABLE project.app (
	app_id int4 NOT NULL,
	"name" varchar(100) NOT NULL,
	CONSTRAINT pk_app PRIMARY KEY (app_id)
);
