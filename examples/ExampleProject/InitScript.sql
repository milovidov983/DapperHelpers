DROP DATABASE IF EXISTS example_db;

DROP TABLE IF EXISTS "Users";

CREATE DATABASE example_db;

CREATE TABLE "Users"
(
	"Id" serial not null,
	"Name" text,
	"Email" text not null,
	"RegisteredAt" timestamp not null
);


ALTER TABLE "Users"
	add constraint """Users""_pk"
		primary key("Id");
