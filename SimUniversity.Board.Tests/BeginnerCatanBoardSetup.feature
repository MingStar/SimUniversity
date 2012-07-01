Feature: Setting up boards

Scenario: Set up the beginner board for Catan (as from the game rules)
	When I set up the beginner board for Catan
	Then there should be 19 hexagons on the board
	And there should be 54 vectices on the board	
	And there should be 72 edges on the board
	And the details of hexagons should be the following:
		| X  | Y | Number Marker | Student | Adj. # of hexes | Adj. # of vertices | Adj. # of edges |
		| 0  | 0 | 5             | Ore     | 3               | 6                  | 6               |
		| 1  | 0 | 2             | Grain   | 4               | 6                  | 6               |
		| 2  | 0 | 6             | Wood    | 3               | 6                  | 6               |
		| 2  | 1 | 3             | Ore     | 4               | 6                  | 6               |
		| 1  | 1 | 9             | Sheep   | 6               | 6                  | 6               |
		| 0  | 1 | 10            | Sheep   | 6               | 6                  | 6               |
		| -1 | 1 | 8             | Brick   | 4               | 6                  | 6               |
		| -2 | 2 | 0             | None    | 3               | 6                  | 6               |
		| -1 | 2 | 3             | Wood    | 6               | 6                  | 6               |
		| 0  | 2 | 11            | Grain   | 6               | 6                  | 6               |
		| 1  | 2 | 4             | Wood    | 6               | 6                  | 6               |
		| 2  | 2 | 8             | Grain   | 3               | 6                  | 6               |
		| 1  | 3 | 10            | Sheep   | 4               | 6                  | 6               |
		| 0  | 3 | 5             | Brick   | 6               | 6                  | 6               |
		| -1 | 3 | 6             | Ore     | 6               | 6                  | 6               |
		| -2 | 3 | 4             | Brick   | 4               | 6                  | 6               |
		| -2 | 4 | 11            | Wood    | 3               | 6                  | 6               |
		| -1 | 4 | 12            | Sheep   | 4               | 6                  | 6               |
		| 0  | 4 | 9             | Grain   | 3               | 6                  | 6               |

Scenario: Set up the basic board for Catan
	When I set up the basic board for Catan
	Then there should be 19 hexagons on the board
	And there should be 54 vectices on the board	
	And there should be 72 edges on the board
	And the resource count should be the following:
		| Resource | Count |
		| Grain    | 4     |
		| Wood     | 4     |
		| Brick    | 3     |
		| Ore      | 3     |
		| Sheep    | 4     |
		| None     | 1     |