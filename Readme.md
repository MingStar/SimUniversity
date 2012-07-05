# SimUniversity

-- Variance of Settlers of Catan, with a University/Campus theme.

Idea from assignment in UNSW CSE COMP1711 computing course in 2002, lecturer Richard Buckland.

### Difference from the original game:
* No player trading (yet)
* No cards, instead players can found startup companies with a 20% success rate
* No initial resources from the setup phase

### Difference from the original COMP1711 SimUniversity game
* Resource are Wood, Brick, Ore, Grain and Sheep (as student degrees too hard to remember)

### TODO:
* Extract the game controlling logic out from Game.ApplyMove
* Enable mocking for the game controlling logic
* Write tests for the game play

### Tidy up:
* Remove game state hashing
* Enable deep copy of game state