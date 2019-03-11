# Personal Information
COMP476 Assignment #3: Advanced Game Development, Concordia University    
Presented by: Thomas Backs    
ID: 27554524    

## Introduction
Multiplayer game inspired from the classic Pac-Man Battle Royale, but in my version, it will be with Pokemon instead. Each player controls a single pokemon (*Bulbasaur, Charmander, Squirtle, or Pikachu*), the players are trying to catch all PokeBall around the map, there is some Gengar's that are trying to catch these pokemon running around, try to not get caught by it! There is 4 Masterballs around the map as well, upon capturing a masterball, the Gengar will turn white and will try to run away from the players.    

## Control and UI    
`TO DO `    

## R1) Level Environment    
the environment is 2.5D style, with 3D assets. The path is one tile thick, the walls are represented by 1x1 cube. There is two warp tunnel at the each end of the map along the x-axis, it will warp the player to the other side. The same applies to our Gengar. The pac-dots on our path is represented as pokeball from Pokemon universe. There is 4 masterballs on our map at each corner, upon catching the masterball, it will turn Gengar into a different shade of color and allows our pokemon to kill them and send them back to prison. Gengar movement is accomplished by using `NavMesh` component attached to it.    

## Sources
Bulbasaur, Charmander, Gengar, Pikachu, Squirtle, and Pokeball/Masterball https://www.models-resource.com/3ds/pokemonxy/

