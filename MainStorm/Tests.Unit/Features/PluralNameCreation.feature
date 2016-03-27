Feature: PluralNameCreation
	In order to assign names to Collection type of Navigation Properties	
	I want to convert a singular name to a plural name

@PluralName
Scenario Outline: Create a plural name
	Given I have a 'Name' defined as '<name>'
	When I convert 'Name' to plural 'Result'
	Then 'Result' should be equal to '<result>'

Examples:
| name				| result			|
| ballista			| ballistae			|
| class				| classes			|
| box				| boxes				|
| byte				| bytes				|
| bolt				| bolts				|
| fish				| fishes			|
| guy				| guys				|
| ply				| plies				|
