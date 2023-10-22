/*
<Internal_Documentation>

Name: Chaewan Woo
Student number: 101354291
Date last Modified: Specific dates are written on git-repo


<GameManager.cs>
Description: A Finite State Machine (FSM) model helps manage and control the 
interactions and behaviors of various game classes and their functions to create the game.

<CameraManager.cs>
Description: The camera manager enables players to zoom in, zoom out, and drag to control the camera's perspective.

<EnemyManager.cs>
Description: A Finite State Machine (FSM) model helps manage the and control the enemy spawn.
<Enemy.cs>
Description: Also using a small FSM model, and this script is for Enemy AI.
<EnemyData.cs>
Description: stores EnemyData.

<FieldManager.cs>
Spawns all pick up objects, and manage spawning pickup objects
<PickUp.cs>
Allows game object to be clicked for collecting resources, updating the game's scores, 
and sending choppedTree object if a connection to another "nextTree" object exists.

<SoundManager.cs>
Sound Manager that handles background music (BGM) and sound effects (SFX), 
allowing you to play and stop audio clips based on their assigned names. 

<UnitManager.cs>
acts as a manager for a list of "Guardian" units and provides methods to add, remove, and clear 
these units from the list, as well as placeholders for playing and ending actions.

<BuildManager.cs>
this script manages the process of placing guardian towers in the game world. 
It interacts with UI elements, the camera, and other game objects to allow players to place towers 
within specified areas while considering available resources and collision detection.
<BuildArea.cs>
monitors for collisions with 2D colliders, indicate whether 
there is an active collision within its defined area to build guardian tower or not.

<CentralTower.cs>
This script manages game-related data and updates UI elements for waves, score, 
health, gold, and resources. It handles damage and displays game-over information.

<GuardianTower.cs>
This script is a type of tower in the game. It only has functionality for taking damages and being destroy if damage reachs 0.
<Tower.cs>
manages guardian purchases for the tower. It handles showing and hiding the purchase menu, 
and upon purchase, it instantiates guardian units at random spawn points, deducting the cost from the player's gold.

<Guardian.cs>
Also using a small FSM model, and this script is for Guaridan AI. (very similar to Enemy script)

<UIMainMenu.cs>
UI for main menu scene

<UIPlayScene.cs>
UI for play scene

















 
 
 
*/