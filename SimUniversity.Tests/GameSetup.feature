Feature: Test the set up phase of the game

Scenario: 010 Set up the Catan beginnger's game
	When the dice roll is predefined to 5 
	And I set up the Catan beginner's game
	Then the university information should be the following:
		| University | Score | Campuses               | Links                   | Students   |
		| Red        | 2     | (0, 3, bl); (0, 2, r)  | (0, 3, bl); (0, 2, tr)  | w, b, s, g |
		| Blue       | 2     | (0, 0, tl); (1, 0, r)  | (0, 1, bl); (1, 0, br)  | 2o, b, s   |
		| White      | 2     | (-1, 3, l); (2, 0, tl) | (-1, 3, tl); (2, 0, tl) | o, w, s    |
		| Orange     | 2     | (2, 2, l); (1, 3, tl)  | (2, 2, bl); (1, 3, tl)  | g, s, w    |
	And the current game phase should be 'Play'
	And the current university of the turn should be 'Red'

@ignore
Scenario: 020 End turn
	When the turn is ended with a dice roll predefined to 4
	Then the university information should be the following:
		| University | Students |
		| Red        | b, w     |
		| Blue       | o        |
		| White      | b        |
		| Orange     | w        |
	And the current university of the turn should be 'Blue'

@ignore
Scenario: 030 End turn
	When the turn is ended with a dice roll predefined to 6
	Then the university information should be the following:
		| University | Students |
		| Red        | b, w, o  |
		| Blue       | o, b     |
		| White      | b, o, w  |
		| Orange     | w        |
	And the current university of the turn should be 'White'

	
@ignore
Scenario: 030 build a internet link
	When the university build an internet link at (0, 4, tl)
	Then a red internet link should be at (0, 4, tl)