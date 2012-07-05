Feature: Test the set up phase of the game

Scenario: 010 Set up the Catan beginnger's game
	When I set up the Catan beginner's game
	Then the university information should be the following:
		| University | Score | Campuses               | Links                   | Students |
		| Red        | 2     | (0, 3, bl); (0, 2, r)  | (0, 3, bl); (0, 2, tr)  |          |
		| Blue       | 2     | (0, 0, tl); (1, 0, r)  | (0, 1, bl); (1, 0, br)  |          |
		| White      | 2     | (-1, 3, l); (2, 0, tl) | (-1, 3, tl); (2, 0, tl) |          |
		| Orange     | 2     | (2, 2, l); (1, 3, tl)  | (2, 2, bl); (1, 3, tl)  |          |
	And the current game phase should be 'Play'
	And the current university of the turn should be 'Red'

@ignore
Scenario: 020 Enrolment
	When the enrolment happens with dice roll 5
	Then the university information should be the following:
		| University | Students |
		| Red        | b        |
		| Blue       | o        |
		| White      |          |
		| Orange     |          |
	And the current university of the turn should be 'Red'

@ignore
Scenario: 030 build a link
	When the university build an internet link at (