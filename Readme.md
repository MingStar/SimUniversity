# SimUniversity

*Variance of the board game Settlers of Catan, with a University/Campus theme.*

Idea from the project in COMP1711 UNSW CSE computing course in 2002, lecturer Richard Buckland.

### Difference from the original Settlers of Catan game:
* No player trading (yet)
* No development cards, instead players can found startup companies with a 20% success rate

### Difference from the original COMP1711 SimUniversity game
* Resource are Wood, Brick, Ore, Grain and Sheep (as student degrees too hard to remember)
* Acquire students from the 2nd campus in the setup phase (same as Catan)

### TODO:
* Move the game moves info to the contract
* Seperate interfaces for game update and interfaces for AI queries
* Write tests for the game play
* Fix AI player selecting bad location in set up phase
* Enable deep copy of game state (so AI cannot break the actual game)

### Tidy up:
* Remove game state hashing
