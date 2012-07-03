Feature: Test the set up phase of the game

Scenario: Set up the Catan beginnger's game
	When I set up the Catan beginner's game
	Then the current game phase should be 'Play'
	And the player information should be the following:
		| Player | Score | Campuses               | Links                   |
		| Red    | 2     | (0, 3, bl); (0, 2, r)  | (0, 3, bl); (0, 2, tr)  |
		| Blue   | 2     | (0, 0, tl); (1, 0, r)  | (0, 1, bl); (1, 0, br)  |
		| White  | 2     | (-1, 3, l); (2, 0, tl) | (-1, 3, tl); (2, 0, tl) |
		| Orange | 2     | (2, 2, l); (1, 3, tl)  | (2, 2, bl); (1, 3, tl)  |