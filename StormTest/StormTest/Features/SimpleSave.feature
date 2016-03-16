@inTransaction
Feature: SimpleSave
	In order save data to db
	I want to call simple DAL method

Background: 
	Given I have companies
	 | Id | Name        |
	 | 1  | SomeCompany |
	 And I have departments in companies
	 | Id | CompanyId | Name          |
	 | 1  | 1         | Supplementary |
	 | 2  | 1         | Business      |	  
	 And I have empleyees in departments
	 | Id | DepartmentId | Name    |
	 | 1  | 1            | Peter   |
	 | 2  | 2            | Alex    |
	 | 3  | 1            | Richard |
	 And I have payments for employees
	 | Id | EmployeeId | EffectiveDate | Amount  |
	 | 1  | 1          | 01/01/2015    | 1234.56 |
	 | 2  | 1          | 01/02/2015    | 1111.11 |
	 | 3  | 2          | 01/02/2015    | 2345.67 |
	 | 4  | 1          | 01/03/2015    | 1212.12 |
	 When I save companies


Scenario: Save and then load		
	When I load company 'SomeCompany' as 'A'
	Then Company 'A' should be equal to first of source companies

Scenario: Load, change and save
	When I load company 'SomeCompany' as 'A'
	 And I serialize company 'A' and deserialize result to company 'B'
	 And I remove 'Business' department from company 'B'
	 And I find department 'Supplementary' in company 'B'
	 And I add employee 'A' 'Hank' to department 
	 And I add payment '01/01/2015' '222' to employee 'A'	
	 And I find employee 'B' 'Peter' in department
	 And I add payment '01/04/2015' '333' to employee 'B'
	 And I remove payment by date '01/01/2015' from employee 'B'
	 And I remove employee 'Richard' from department
	 And I update company 'A' in the database
	 And I load company 'SomeCompany' as 'C'
	Then Company 'B' should be equal to company 'C'