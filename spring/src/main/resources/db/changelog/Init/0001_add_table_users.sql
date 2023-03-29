--liquibase formatted sql
--changeset stinner:init

-- Used for authentication in spring
create table users_
(
    id                  uuid primary key,
    email               varchar(255)    not null,
    password            varchar(72)     not null,
    account_locked      boolean         not null default true,
    credentials_expired boolean         not null default false,
    enabled             boolean         not null default false,
    authorities         authority array not null default Array []::authority[],
    created_at          timestamptz     not null,
    firstname           varchar(255)    null,
    lastname            varchar(255)    null
);