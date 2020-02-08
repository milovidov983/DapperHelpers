DROP DATABASE IF EXISTS example_db;

DROP TABLE IF EXISTS "Users";
DROP TABLE IF EXISTS "UsersJsonb";

CREATE DATABASE example_db;

CREATE TABLE "Users"
(
	"Id" serial not null,
	"Name" text,
	"Email" text not null,
	"RegisteredAt" timestamp not null
);

CREATE TABLE "UsersJsonb"
(
	"Id" serial not null,
	"Data" jsonb
);