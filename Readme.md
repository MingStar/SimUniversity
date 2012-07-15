# SimUniversity

*Variance of the board game Settlers of Catan, with a University/Campus theme.*

Idea from the project in COMP1711 UNSW CSE computing course in 2002, lecturer Richard Buckland.

### Difference from the original Settlers of Catan game:
* No player trading (yet)
* No robber
* No development cards, instead players can found startup companies with a 20% success rate
* No limitations on the number of campuses (settlements) and super campuses (cities)

### Difference from the original COMP1711 SimUniversity game
* Resource are Wood, Brick, Ore, Grain and Sheep (as student degrees are too hard to remember)
* Acquire students from the 2nd campus in the setup phase (same as Catan)
* When a startup company failed, no card stealing from other players (yet)


### Translation Table

<table>
	<tr>
		<th>Concepts in SimUniversity</th><th>Concepts in Catan</th>
	</tr>
	<tr>
		<td>Hexagon (Suburb)</td><td>Hex Terrian</td>
	</tr>
	<tr>
		<td>Edge</td><td>Path</td>
	</tr>
	<tr>
		<td>Vertex</td><td>Intersection</td>
	</tr>
	<tr>
		<td>Trading Site</td><td>Trading Post</td>
	</tr>
	<tr>
		<td>Internet Link</td><td>Road</td>
	</tr>
	<tr>
		<td>Traditional Campus</td><td>Settlement</td>
	</tr>
	<tr>
		<td>Super Campus</td><td>City</td>
	</tr>
	<tr>
		<td>Student/Degree</td><td>Resource</td>
	</tr>
	<tr>
		<td>Startup Company</td><td>Development Card^</td>
	</tr>
</table>
^Startup Company is roughly equivalent to Development Card.


### TODO:
* Need to be able to be select only a subset of parameters to learn => SimplexScores
* Make the simplex learning method to take into account of winning ratio (e.g. a 5-0 win should be MUCH better than a 3-2 win), the winning difference in one round is less important.
* Ask Resharper not to modify some files
* Write tests for the game play
* Have another AI to search all possible moves within the turn (particular useful in late game)
* Enable deep copy of game state (so AI won't be possible to break the actual game)
* Persist game state

### Tidy up:
* Remove game state hashing
