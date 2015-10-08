Feature: CamelCaseNameCreation
	In order to assign names to a model	
	I want to convert most known form of names to CamelCase/PascalCase

@CamelCase
Scenario Outline: Create a CamelCase name
	Given I have a 'Name' defined as '<name>'
	When I convert 'Name' to CamelCase 'Result'
	Then 'Result' should be equal to '<result>'

Examples:
| name				| result			|
| somename			| Somename			|
| some_name			| SomeName			|
| SOME_NAME			| SomeName			|
| SOMENAME			| Somename			|
| SomeName			| SomeName			|
| SomeName_OfSomeone| SomeNameOfSomeone	|
| someName			| SomeName			|