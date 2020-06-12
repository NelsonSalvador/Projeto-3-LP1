# Projeto 3 Linguagens de Progrmação I 2019/2020 - Roguelike

## Authors

Nelson salvador n21904295 | Pedro Coutinho n21905323 | Miguel Martinho n21901530

Nelson Salvador:
- Highscore system
- Input system
- Map and object sync trough dictionary
- PowerUps
- PowerUps Spawns
- debugging

Pedro Coutinho:
- Enemy Spawn
- Enemy movement
- Action messages
- Map generation
- debugging

Miguel Martinho:
- Report
- Player movement
- Collision system
- Gameloop
- UI
- Main menu
- debugging

## Git repository

https://github.com/NelsonSalvador/Projeto-3-LP1

## Program Architecture

Program starts with input given by player on row and column size. Then creates
the menu and takes input for the multiple menu options. If the player chooses to
start a new game, a new game instance will be created. This game instance will
create a map and a player instance and have it as a property. With each new
level a new map will be generated, along with the new positions of the new
objects trough the Generate method from the class map. The player instance
will remain the same trough out the levels the only thing that changes is his
starting position which is randomly generated with the new map.

The method StartGame is responsible for the main gameloop and for essentially
running the game. The player makes gives his 2 directional inputs which
are fed to the GetInput method, which separates the directional inputs and
tells the CheckMove method where the player wishes to move. CheckMove method
verifies if the player can move to said direction by comparing the tile given by
GetInput to the information on said tile in the dictionary. If it has an object
the player will either not be able to move there if there is an enemy or a wall
or be able to pick up a PowerUp. 
After the 2 moves have been made, the enemies will move. A *for each* function
will run trough the enemy list to make sure each enemy can move close to the
player, if the move that is closest to the player is against a wall, they will
move in a random direction. The EnemySelect will compare every move an enemy can
make a choose the one with the smallest distance to the player. if the enemy
sees he has 2 possible moves with the same distance he will make the first one
he checked. When the enemy hits a wall the method EnemyRandomMove will ensure
the enemy moves into a random available direction by making various compares
with the tiles closest to him.

In terms of the Map class, it has a *dictionary* that stores the type of any
object in-game, and an instance of a class if that object has various types
or if its important to know their coordinates. 

### UML Diagram

![](UML.png)

## References
Help with the dictionary
https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/