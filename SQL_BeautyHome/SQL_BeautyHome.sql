use master
go
if exists (select name from sysdatabases where name = N'SQL_BeautyHome')
drop database SQL_BeautyHome
go
CREATE DATABASE SQL_BeautyHome
go
USE SQL_BeautyHome

Create table [user]
(
	user_id Bigint NOT NULL IDENTITY(1,1),
	user_name Varchar(50) NOT NULL,
	password Varchar(256) NOT NULL,
	full_name Nvarchar(50) NULL,
	address Nvarchar(200) NULL,
	email Varchar(50) NULL,
	phone Varchar(11) NULL,
	role int NOT NULL,
Primary Key (user_id)
) 
go

Create table [furniture]
(
	furniture_id Bigint NOT NULL IDENTITY(1,1),
	name Nvarchar(50) NULL,
Primary Key (furniture_id)
) 
go

Create table [type_product]
(
	type_product_id Bigint NOT NULL IDENTITY(1,1),
	name Nvarchar(50) NULL,
	furniture_id Bigint NULL,
Primary Key ([type_product_id])
) 
go

Create table [order]
(
	order_id Bigint NOT NULL IDENTITY(1,1),
	product_id Bigint NULL,
	user_id Bigint NULL,
	full_name Nvarchar(50) NULL,
	address Nvarchar(200) NULL,
	mail Varchar(50) NULL,
	product_name Nvarchar(50) NULL,
	price Float NULL,
	date_order Datetime NULL,
	datereceived Datetime NULL,
	status Integer NULL,
Primary Key (order_id)
) 
go

Create table [image_product]
(
	product_id Bigint NOT NULL,
	url_Image1 Varchar(200) NULL,
	url_Image2 Varchar(200) NULL,
	url_Image3 Varchar(200) NULL,
Primary Key (product_id)
) 
go

Create table [product]
(
	product_id Bigint NOT NULL IDENTITY(1,1),
	type_product_id Bigint NULL,
	name Nvarchar(50) NULL,
	descriptionDetails Nvarchar(200) NULL,
	description Nvarchar(500) NULL,
	evaluate Char(10) NULL,
	amount Float NULL,
	price Float NULL,
	color Nvarchar(20) NULL,
Primary Key (product_id)
) 
go

Create table [comment_product]
(
	comment_puduct_id Bigint NOT NULL IDENTITY(1,1),
	product_id Bigint NULL,
	user_id Bigint NULL,
	txt_comment Nvarchar(200) NULL,
Primary Key (comment_puduct_id)
) 
go

ALTER TABLE type_product
ADD FOREIGN KEY (furniture_id) REFERENCES furniture(furniture_id);
ALTER TABLE product
ADD FOREIGN KEY (type_product_id) REFERENCES type_product(type_product_id);
ALTER TABLE image_product
ADD FOREIGN KEY (product_id) REFERENCES product(product_id);
ALTER TABLE comment_product
ADD FOREIGN KEY (product_id) REFERENCES product(product_id);
ALTER TABLE [order]
ADD FOREIGN KEY (product_id) REFERENCES product(product_id);
ALTER TABLE [order]
ADD FOREIGN KEY (user_id) REFERENCES [user](user_id);