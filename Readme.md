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

### TODO:
* Use dice cards instead of dice rolls for AI learning (to minimise the dice roll variance)
* Have another AI to search all possible moves within the turn (particular useful in late game)
* Write tests for the game play
* Enable deep copy of game state (so AI cannot break the actual game)
* Persist game state

### Tidy up:
* Remove game state hashing