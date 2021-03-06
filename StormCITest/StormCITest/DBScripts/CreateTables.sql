﻿create table entity_with_id
(
    id int IDENTITY(1,1) not null,
    a_bigint bigint,
    a_int int not null,
    a_numeric numeric(6,2),
    a_bit bit,
    a_smallint smallint,
    a_decimal decimal(8,3),
    a_smallmoney smallmoney,
    a_tinyint tinyint,
    a_money money,
    constraint entity_with_id_pk primary key (id)
);
GO

create table entity_with_guid
(
    id uniqueidentifier not null,
	a_float float,
	a_real real not null,
	a_date date,
	a_time time,
	a_offset datetimeoffset,
	a_datetime datetime,
	a_datetime2 datetime2,
	a_smalldatetime smalldatetime,
    constraint entity_with_guid_pk primary key (id)
);
GO

create schema some_schema;
GO

create sequence some_schema.entity_seq as int START WITH 1 MINVALUE 1;
create table some_schema.entity_with_sequence
(
    id int not null default NEXT VALUE FOR some_schema.entity_seq,
	a_char char(1),
	a_varchar varchar(2000) not null,
	a_text text,
	a_nchar nchar(10),
	a_nvarchar nvarchar(max),
	a_ntext ntext,
	a_xml xml,
	a_binary binary(1000),
	a_varbinary varbinary(max),
	a_image image, -- deprecated, same as varbinary(max)
    constraint entity_with_sequence_pk primary key (id)
);
GO

create table entity_with_multikey
(
    id_1 int not null,
	id_2 nvarchar(256) not null,
	content nvarchar(max),
    constraint entity_with_multikey_pk primary key (id_1, id_2)
);
GO

create table entity_without_key
(
    val int not null,
	content nvarchar(max),
    a_float float,
	a_real real not null,
	a_date date,
	a_bigint bigint,
    a_int int not null,
    a_numeric numeric(6,2),
);
GO

create sequence smallentity_seq as int START WITH 1 MINVALUE 1;
create table smallentity_with_sequence
(
    id int not null default NEXT VALUE FOR smallentity_seq,
	a_char char(1),
	a_varchar varchar(2000) not null,
	a_text text,
    constraint smallentity_with_sequence_pk primary key (id)
);