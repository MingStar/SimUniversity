# SimUniversity

Variance of Settlers of Catan, with a university theme.

Idea from assignment in UNSW CSE COMP1711 computing course in 2002, lecturer Richard Buckland.

## Difference from the original game:
* No player trading (yet)
* No cards, has the chance for startup company instead

## Difference from the original COMP1711 SimUniversity game
* Resource are Wood, Brick, Ore, Grain and Sheep (too hard to remember the student degrees)

## TODO:
* Extract the game controlling logic out from the GameController and Game.ApplyMove
* Enable mocking for the game controlling logic
* Write tests for the game play

## Tidy up:
* Remove game state hashing
* Enable deep copy of game state