Feature: NameNormalization
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@NormalizeName
Scenario: Normalize names
Scenario: Create a CamelCase name
	Given I have a 'Name' list defined as	
	   | name			|
	   | someNaem123	|
       | matchNaem		|
       | someNaem234	|
       | matchNaem		|
       | matchNaem		|
       | someNaem345	|
       | matchNaem2		|
       | matchNaem		|
	When I normalize 'Name' list to 'Result' list
	Then 'Result' list should be equal to
	   | name			|
	   | someNaem123	|
       | matchNaem		|
       | someNaem234	|
       | matchNaem		|
       | matchNaem		|
       | someNaem345	|
       | matchNaem2		|
       | matchNaem		|
