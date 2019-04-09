# Wanaheim
One of the nine realms needs you! Fight monsters, buy items (to keep economy flowing) and grow strong as a hero of this story!
Wanaheim is a browser game with typical RPG mechanic. You will be growing your character by fighting at the arena and buying new items.

## Table of contents
* [Status](#status)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)

## Status
The project is still in progress. To find out what features are completed go to [Features](#features) section.


## Technologies
* ASP.NET Core - version 2.1
* Entity Framework Core - version 2.1.8
* Angular - version 7.2
* NgRx - version 7.2


## Setup
To make application working, go to ClientApp through command prompt and type "npm install" to restore all packages.


## Features
* Item Shop - basic structure and all CRUD operations are implemented. The only thing that is not ready yet is NgRx state of this component. Item shop provides the list of items to buy for a normal user and tools to manage items for a user with admin's right.  
* Authentication - application is using JWT token. Backend is generating tokens and sending them to angular application. Then, a token is stored as a state and in local storage. 

To-do list:
* Arena 
* Player panel
* Different views basing on player's role
