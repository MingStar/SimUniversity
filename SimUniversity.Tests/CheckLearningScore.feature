Feature: Learning Score

Scenario: Calculate tournament score #1
	Given the AI tournament result is as the following:
		| Round Result | Expected Score |
		| 10-2         | 64             |
		| 10-9         | 1              |
		| 3-10         | -49            |
		| 9-10         | -1             |
		| 10-5         | 25             |
		| 10-9         | 1              |
		| 10-9         | 1              |
	When the AI tournament result score is calculated
	Then the total round count should be 7
	And the challenger winning count should be 5
	And the tournament score from rounds should be 27
	And the tournament score from winning should be 225

Scenario: Calculate tournament score #2
	Given the AI tournament result is as the following:
		| Round Result | Expected Score |
		| 10-2         | 64             |
		| 10-9         | 1              |
		| 3-10         | -49            |
		| 9-10         | -1             |
		| 10-5         | 25             |
		| 10-2         | 64             |
		| 10-2         | 64             |
	When the AI tournament result score is calculated
	Then the total round count should be 7
	And the challenger winning count should be 5
	And the tournament score from rounds should be 153
	And the tournament score from winning should be 225

Scenario: Calculate tournament score #3
	Given the AI tournament result is as the following:
		| Round Result | Expected Score |
		| 2-10         | -64            |
		| 9-10         | -1             |
		| 10-3         | 49             |
		| 10-9         | 1              |
		| 5-10         | -25            |
		| 2-10         | -64            |
		| 2-10         | -64            |
	When the AI tournament result score is calculated
	Then the total round count should be 7
	And the challenger winning count should be 2
	And the tournament score from rounds should be -153
	And the tournament score from winning should be -225