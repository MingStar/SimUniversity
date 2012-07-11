Feature: Game Play
	* build internet link
	* try start up
	* 

Scenario: 010 Set up the Catan beginnger's game
	Given the dice roll is predefined to 5 
	When I set up the Catan beginner's game
	Then the university information should be the following:
		| University | Score | Campuses               | Links                   | Students   |
		| Red        | 2     | (0, 3, bl); (0, 2, r)  | (0, 3, bl); (0, 2, tr)  | w, b, s, g |
		| Blue       | 2     | (0, 0, tl); (1, 0, r)  | (0, 1, bl); (1, 0, br)  | 2o, b, s   |
		| White      | 2     | (-1, 3, l); (2, 0, tl) | (-1, 3, tl); (2, 0, tl) | o, w, s    |
		| Orange     | 2     | (2, 2, l); (1, 3, tl)  | (2, 2, bl); (1, 3, tl)  | g, s, w    |
	And the current game phase should be 'Play'
	And the current university of the turn should be 'Red'

Scenario: 020 build a internet link
	When the university build an internet link at (0, 3, tl)
	Then a red internet link should be at (0, 3, tl)
	And the university information should be the following:
		| University | Score | Links                              | Students |
		| Red        | 2     | (0, 3, bl); (0, 2, tr); (0, 3, tl) | s, g     |
		| Blue       | 2     | (0, 1, bl); (1, 0, br)             | 2o, b, s |
		| White      | 2     | (-1, 3, tl); (2, 0, tl)            | o, w, s  |
		| Orange     | 2     | (2, 2, bl); (1, 3, tl)             | g, s, w  |
	And the current game phase should be 'Play'
	And the current university of the turn should be 'Red'

Scenario: 030 End turn
	Given the dice roll is predefined to 4 
	When the turn is ended
	Then the university information should be the following:
		| University | Students   |
		| Red        | w, s, g    |
		| Blue       | 2o, b, s   |
		| White      | o, w, s, b |
		| Orange     | g, s, 2w   |
	And the current university of the turn should be 'Blue'

Scenario: 040 End turn
	Given the dice roll is predefined to 6 
	When the turn is ended
	Then the university information should be the following:
		| University | Students     |
		| Red        | w, s, g, o   |
		| Blue       | w, 2o, b, s  |
		| White      | 2o, 2w, s, b |
		| Orange     | g, s, 2w     |
	And the current university of the turn should be 'White'

Scenario: 050 Build a internet link
	When the university build an internet link at (2, 0, t)
	Then a white internet link should be at (2, 0, t)
	And the university information should be the following:
		| University | Score | Links                              | Students    |
		| Red        | 2     | (0, 3, bl); (0, 2, tr); (0, 3, tl) | w, s, g, o  |
		| Blue       | 2     | (0, 1, bl); (1, 0, br)             | w, 2o, b, s |
		| White      | 2     | (-1, 3, tl); (2, 0, tl); (2, 0, t) | 2o, w, s    |
		| Orange     | 2     | (2, 2, bl); (1, 3, tl)             | g, s, 2w    |
	And the current university of the turn should be 'White'

Scenario: 060 End turn
	Given the dice roll is predefined to 8
	When the turn is ended
	Then the university information should be the following:
		| University | Students     |
		| Red        | w, s, g, o   |
		| Blue       | w, 2o, 2b, s |
		| White      | 2o, w, s     |
		| Orange     | 2g, s, 2w    |
	And the current university of the turn should be 'Orange'

Scenario: 070 End turn
	Given the dice roll is predefined to 9
	When the turn is ended
	Then the university information should be the following:
		| University | Students     |
		| Red        | w, 2s, g, o  |
		| Blue       | w, 2o, 2b, s |
		| White      | 2o, w, 2s    |
		| Orange     | 3g, s, 2w    |
	And the current university of the turn should be 'Red'

Scenario: 080 End turn
	Given the dice roll is predefined to 2
	When the turn is ended
	Then the university information should be the following:
		| University | Students        |
		| Red        | w, 2s, g, o     |
		| Blue       | w, 2o, 2b, s, g |
		| White      | 2o, w, 2s       |
		| Orange     | 3g, s, 2w       |
	And the current university of the turn should be 'Blue'

Scenario: 090 Try Start up
	Given the startup will fail
	When the player found a startup company
	Then the university information should be the following:
		| University | Students    | Failed Startups |
		| Red        | w, 2s, g, o | 0               |
		| Blue       | w, o, 2b,   | 1               |
		| White      | 2o, w, 2s   | 0               |
		| Orange     | 3g, s, 2w   | 0               |
	And the current university of the turn should be 'Blue'

Scenario: 100 End turn
	Given the dice roll is predefined to 10
	When the turn is ended
	Then the university information should be the following:
		| University | Students    |
		| Red        | w, 2s, g, o |
		| Blue       | w, o, 2b, s |
		| White      | 2o, w, 2s   |
		| Orange     | 3g, 3s, 2w  |
	And the current university of the turn should be 'White'

Scenario: 110 End turn
	Given the dice roll is predefined to 10
	When the turn is ended
	Then the university information should be the following:
		| University | Students     |
		| Red        | w, 2s, g, o  |
		| Blue       | w, o, 2b, 2s |
		| White      | 2o, w, 2s    |
		| Orange     | 3g, 5s, 2w   |
	And the current university of the turn should be 'Orange'

@ignore
Scenario: 120 Exchange students
	When the university exchanges 3 sheep for 1 brick
	Then the university information should be the following:
		| University | Students      |
		| Red        | w, 2s, g, o   |
		| Blue       | w, o, 2b, 2s  |
		| White      | 2o, w, 2s     |
		| Orange     | 3g, 2s, 2w, b |
	And the current university of the turn should be 'Orange'

@ignore
Scenario: 130 Build an internet link
	When the university build an internet link at (0, 3, t)
	Then an Orange internet link should be at (0, 3, t)
	And the university information should be the following:
		| University | Score | Links                              | Students     |
		| Red        | 2     | (0, 3, bl); (0, 2, tr); (0, 3, tl) | w, s, g, o   |
		| Blue       | 2     | (0, 1, bl); (1, 0, br)             | w, o, 2b, 2s |
		| White      | 2     | (-1, 3, tl); (2, 0, tl); (2, 0, t) | 2o, w, 2s    |
		| Orange     | 2     | (2, 2, bl); (1, 3, tl); (0, 3, t)  | 3g, 2s, w    |
	And the current university of the turn should be 'Orange'