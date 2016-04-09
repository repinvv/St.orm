CREATE TABLE country
(
  country_id int IDENTITY(1,1) not null,  
  name nvarchar(256) not null,
  country_code nvarchar(10) not null,
  CONSTRAINT pk_country PRIMARY KEY CLUSTERED (country_id ASC)
)
GO

CREATE SCHEMA models
GO 

CREATE TABLE models.company
(
  company_id int IDENTITY(1,1) not null,
  name nvarchar(256), 
  country_id int null,
  CONSTRAINT pk_company PRIMARY KEY (company_id ASC),
  CONSTRAINT fk_company1 FOREIGN KEY (country_id) REFERENCES country (country_id),
)
GO

CREATE TABLE models.department
(
  department_id int IDENTITY(1,1) not null,  
  company_id int not null,
  country_id int null,
  name nvarchar(256) not null,
  CONSTRAINT pk_department PRIMARY KEY (department_id ASC),
  CONSTRAINT fk_department1 FOREIGN KEY (company_id) REFERENCES models.company (company_id),
  CONSTRAINT fk_department2 FOREIGN KEY (country_id) REFERENCES country (country_id),
)
GO

CREATE TABLE models.asset
(
  asset_id int IDENTITY(1,1) not null,
  department_id int not null,
  name nvarchar(256) not null,
  country_id int null,
  CONSTRAINT pk_asset PRIMARY KEY (asset_id ASC),
  CONSTRAINT fk_asset1 FOREIGN KEY (department_id) REFERENCES models.department (department_id),
  CONSTRAINT fk_asset2 FOREIGN KEY (country_id) REFERENCES country (country_id),
)
GO

CREATE TABLE models.employee
(
  employee_id int IDENTITY(1,1) not null,
  department_id int not null,
  name nvarchar(256) not null,
  CONSTRAINT pk_employee PRIMARY KEY (employee_id ASC),
  CONSTRAINT fk_employee1 FOREIGN KEY (department_id) REFERENCES models.department (department_id)
)
GO

CREATE TABLE models.payment
(
  payment_id int IDENTITY(1,1) not null,
  employee_id int not null,
  effective_date datetime2 not null,
  amount decimal(18,2) not null,
  CONSTRAINT pk_payment PRIMARY KEY (payment_id ASC),
  CONSTRAINT fk_payment1 FOREIGN KEY (employee_id) REFERENCES models.employee (employee_id)
)
GO

CREATE TABLE models.payment_country
(
	payment_id int not null,
	country_id int not null,
	CONSTRAINT pk_payment_country PRIMARY KEY (payment_id ASC, country_id ASC),
	CONSTRAINT fk_payment_country1 FOREIGN KEY (payment_id) REFERENCES models.payment (payment_id),
	CONSTRAINT fk_payment_country2 FOREIGN KEY (country_id) REFERENCES country (country_id),
)
GO



