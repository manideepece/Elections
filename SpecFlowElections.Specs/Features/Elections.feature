Feature: Elections
	Simple calculator for adding two numbers

@mytag
Scenario: Get All Voters
	When I need all voters list
	Then the result should retrieve everything

Scenario Outline: Get a particular Voter
	Given a voter Id (<Id>)
	Then the system should output the Voter

Examples: 
	| Id |
	| 1  |


Scenario Outline: Add a Voter
	Given details of Voter (<firstName>, <lastName>, <age>)
	When I add a voter
	When I need all voters list
	Then the result should retrieve fresh list

Examples: 
	| firstName | lastName | age |
	|    TestFirstName       |   TestLastName       | 32    |


Scenario Outline: Delete a Voter
	Given a voter Id (<Id>)
	When I delete a voter
	When I need all voters list
	Then the result should retrieve list after deletion

Examples: 
	| Id |
	| 17  |