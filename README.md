# Personal Information
COMP476 Assignment #3: Advanced Game Development, Concordia University    
Presented by: Thomas Backs    
ID: 27554524    

## Introduction
Multiplayer game inspired from the classic Pac-Man Battle Royale, but in my version, it will be with Pokemon instead. Each player controls a single pokemon (*Bulbasaur, Charmander, Squirtle, or Pikachu*), the players are trying to catch all PokeBall around the map, there is some Gengar's that are trying to catch these pokemon running around, try to not get caught by it! There is 4 Masterballs around the map as well, upon capturing a masterball, the Gengar will turn white and will try to run away from the players.    

## Control and UI    
`TO DO `    

## R1) Level Environment    
the environment is 2.5D style, with 3D assets. The path is one tile thick, the walls are represented by 1x1 cube. There is two warp tunnel at the each end of the map along the z-axis, it will warp the player to the other side. The same applies to our Gengar. The pac-dots on our path is represented as pokeball from Pokemon universe. There is 4 masterballs on our map at each corner, upon catching the masterball, it will turn Gengar into a different shade of color and allows our pokemon to kill them and send them back to prison.    

## R2) Basic Networked Multiplayer Pacman Game    
The network component that are being used in the project are Photo Engine, downloaded from Unity Assets store. It provides a lot of feature and host migration support as well, which is one of the main reason that I picked it. The Ghost used in the game are Gengar pokemons, its movement is accomplished by using `NavMesh` component provided by Unity 3D. The player move its pokemon by using the standard WASD key control. Player has spawning location at each corner of the map.    

## R3) Advanced Networked Multiplayer Pacman Game
The Master Balls (power dots) are located at each corner of the map.

## Sources
Bulbasaur, Charmander, Gengar, Pikachu, Squirtle, and Pokeball/Masterball https://www.models-resource.com/3ds/pokemonxy/
Pokemon fonts: https://www.dafont.com/pokemon.font    
Major source of Photon Unity Networking Assets explanation: Info Gamer - https://www.youtube.com/channel/UCyoayn_uVt2I55ZCUuBVRcQ    
