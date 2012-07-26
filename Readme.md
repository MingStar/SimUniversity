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

### Console UI
![Console UI](https://github.com/MingStar/SimUniversity/blob/master/images/ConsoleUI.png)

### Game Concept Translation Table

<table>
	<tr>
		<th>SimUniversity</th><th>Original Catan</th>
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
		<td>Successful Startup</td><td>Development Card - Victory Point</td>
	</tr>
	<tr>
		<td>Failed Startup</td><td>Development Card - Knight</td>
	</tr>
	<tr>
		<td>Longest Internet Link</td><td>Longest Road</td>
	</tr>
	<tr>
		<td>Most Failed Startup</td><td>Largest Army</td>
	</tr>
</table>

### New Features Coming
* .NET Game Engine
	* support multiple games
	* support multiple clients
	* with a WCF/web service interface (to support game clients written in other languages)
	* simple user management

* Game Engine admin/viewer (Front end)

* .NET Game Client (Console/Web UI)
	* join a game
	* play a multi-player game
	* Display the game

### TODO:
* Test whether the solution can be compiled using Mono
* Added round results after the AI tournament in learning
* Write tests for the game play
* Have another AI to search all possible moves within the turn (particular useful in late game)
* Enable deep copy of game state (so AI won't be possible to break the actual game)
* Persist game state

### Tidy up:
* Remove game state hashing

