-- simple example
CREATE SCHEMA stats
GO 

CREATE TABLE stats.calculation
(
  calculation_id UNIQUEIDENTIFIER not null,
  name nvarchar(256),
  due_date date,    
  CONSTRAINT pk_calculation PRIMARY KEY (calculation_id)
)
GO

CREATE TABLE stats.calculation_details
(
  calculation_details_id UNIQUEIDENTIFIER not null,
  calculation_id UNIQUEIDENTIFIER not null,
  year int not null,
  month int not null,
  value decimal not null,
  CONSTRAINT pk_calculation_details PRIMARY KEY (calculation_details_id),
  CONSTRAINT fk_calculation_details1 FOREIGN KEY (calculation_id) REFERENCES stats.calculation (calculation_id)
)
GO


-- complex example

-- global entities
CREATE TABLE department
(
  department_id int IDENTITY(1,1) not null,  
  name nvarchar(256) not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_department PRIMARY KEY CLUSTERED (department_id ASC)
)
GO

CREATE TABLE eligibility_group
(
  eligibility_group_id int IDENTITY(1,1) not null,  
  name nvarchar(256) not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_eligibility_group PRIMARY KEY CLUSTERED (eligibility_group_id ASC)
)
GO

CREATE TABLE currency
(
  currency_id int IDENTITY(1,1) not null,  
  name nvarchar(256) not null,
  currency_code nvarchar(10) not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_currency PRIMARY KEY CLUSTERED (currency_id ASC)
)
GO

CREATE TABLE country
(
  country_id int IDENTITY(1,1) not null,  
  name nvarchar(256) not null,
  country_code nvarchar(10) not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_country PRIMARY KEY CLUSTERED (country_id ASC)
)
GO

CREATE SCHEMA model
GO

-- target entities
CREATE SEQUENCE model.policy_seq as int START WITH 1 MINVALUE 1;
CREATE TABLE model.policy
(
  policy_id int not null default(next value for model.policy_seq),  
  country_id int not null,
  currency_id int null,
  name nvarchar(256) not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_policy PRIMARY KEY CLUSTERED (policy_id ASC),
  CONSTRAINT fk_policy1 FOREIGN KEY (country_id) REFERENCES country (country_id),
  CONSTRAINT fk_policy2 FOREIGN KEY (currency_id) REFERENCES currency (currency_id),
)
GO

CREATE SEQUENCE model.tax_seq as int START WITH 1 MINVALUE 1;
CREATE TABLE model.tax
(
  tax_id int not null default(NEXT VALUE FOR model.tax_seq),  
  policy_id int not null,
  amount decimal not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_tax PRIMARY KEY CLUSTERED (tax_id ASC),
  CONSTRAINT fk_tax1 FOREIGN KEY (policy_id) REFERENCES model.policy (policy_id)
)
GO

CREATE SEQUENCE model.coverage_seq as int START WITH 1 MINVALUE 1;
CREATE TABLE model.coverage
(
  coverage_id int not null default(NEXT VALUE FOR model.coverage_seq),  
  policy_id int not null,
  comment nvarchar(256),
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_coverage PRIMARY KEY CLUSTERED (coverage_id ASC),
  CONSTRAINT fk_coverage1 FOREIGN KEY (policy_id) REFERENCES model.policy (policy_id)
)
GO

CREATE TABLE model.coverage_department
(
  coverage_id int not null,  
  department_id int not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_coverage_department PRIMARY KEY CLUSTERED (coverage_id ASC, department_id ASC),
  CONSTRAINT fk_coverage_department1 FOREIGN KEY (coverage_id) REFERENCES model.coverage (coverage_id),
  CONSTRAINT fk_coverage_department2 FOREIGN KEY (department_id) REFERENCES department (department_id),
)
GO

CREATE TABLE coverage_eligibility_group
(
  coverage_id int not null,  
  eligibility_group_id int not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_coverage_eligibility_group PRIMARY KEY CLUSTERED (coverage_id ASC, eligibility_group_id ASC),
  CONSTRAINT fk_coverage_eligibility_group1 FOREIGN KEY (coverage_id) REFERENCES model.coverage (coverage_id),
  CONSTRAINT fk_coverage_eligibility_group2 FOREIGN KEY (eligibility_group_id) REFERENCES eligibility_group (eligibility_group_id),
)
GO

CREATE SEQUENCE model.premium_seq as int START WITH 1 MINVALUE 1;
CREATE TABLE model.premium
(
  premium_id int not null default(NEXT VALUE FOR model.premium_seq),  
  premium_type int not null,
  coverage_id int not null,
  amount decimal not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_premium PRIMARY KEY CLUSTERED (premium_id ASC),
  CONSTRAINT fk_premium1 FOREIGN KEY (coverage_id) REFERENCES model.coverage (coverage_id)
)
GO

CREATE SEQUENCE model.covered_seq as int START WITH 1 MINVALUE 1;
CREATE TABLE model.covered
(
  covered_id int not null default(NEXT VALUE FOR model.covered_seq),  
  covered_type int not null,
  coverage_id int not null,
  headcount int not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_covered PRIMARY KEY CLUSTERED (covered_id ASC),
  CONSTRAINT fk_covered1 FOREIGN KEY (coverage_id) REFERENCES model.coverage (coverage_id)
)
GO

CREATE SEQUENCE model.comment_seq as int START WITH 1 MINVALUE 1;
CREATE TABLE model.comment
(
  comment_id int not null default(NEXT VALUE FOR model.comment_seq),  
  comment_type int not null,
  policy_id int null,
  premium_id int null,
  comment_text nvarchar(max) not null,
  author_user_id int not null,
  created datetime2 not null DEFAULT GETDATE(),
  updated datetime2 not null DEFAULT GETDATE(),
  CONSTRAINT pk_comment PRIMARY KEY CLUSTERED (comment_id ASC),
  CONSTRAINT fk_comment1 FOREIGN KEY (policy_id) REFERENCES model.policy (policy_id),
  CONSTRAINT fk_comment2 FOREIGN KEY (premium_id) REFERENCES model.premium (premium_id)
)
GO

