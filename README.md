# Maze Target Hunt

###################
## Overview
###################

Maze Target Hunt is a Unity maze chase puzzle-action game played from a bird's-eye view. The player must navigate a maze, use a hot/cold detector to locate a hidden target, and avoid being caught by an AI enemy.

The longer the player takes, the faster the AI becomes.

####################
## Gameplay
####################

### Goal

Find and collect the target before the AI catches the player.

### Win Condition

The player wins by collecting the target.

### Lose Condition

The player loses if the AI enemy catches them.

####################
## Controls
####################

-  For the player to move,  WASD keys or Arrow keys should be used.
- The game uses a fixed bird's-eye view camera that follows the player.

####################
## Core Features
####################

- The gameplay uses Maze navigation 
- AI enemy chaser uses Unity NavMesh
- Over time, AI speed increases 
- Hot/Cold proximity detector
- When the target is near, beeping sound gets faster
- Enemy and Target are spawned at random positions at each round
- Implemented Timer HUD 
- Implemented Pause button  
- Implemented Win and Lose screens 
- Implemented Return to Start Menu option  


###########################
## Rubric Features Implemented
###########################

- Audio:  The following were added: 
          - Music for the the Background  
          - Click sounds for the UI
          - 3D spatial sound
          - Audio for the AI enemy
          - Beeps for Hot/Cold detector

- VFX: For target collection and enemy collision, particle effects and explosions were added.

- UI: Created a full menu system, HUD, pause button, timer display, hot/cold detector, and win/lose screens.

- Animations: Added animation/rotation for the pickup collectible and Enemy AI Animation bounce and stretch.
 
- Shaders and Lighting: Created 5 custom shaders/materials and used real-time lighting in the scene.

####################
## Main Scripts
####################

- **PlayerController.cs**  
  Movement of the player, pickup collection, player collisions, and win/lose condition are handled by this script.

- **EnemyMovement.cs**  
    Movement of the AI enemy using NavMesh is added in this script. It also includes the increase of AI enemy speed over time.

- **HotColdDetector.cs**  
   Hot/cold detector UI, including color changes, text updates, and beep timing are implemented in this script.

- **UIGameTimer.cs**  
   The round timer shown on the HUD is implemented in this script.

- **GameManager.cs**  
   Round start, enemy/target spawns randomized position, pause functionality, round ending, and returning to the menu are handled in this script.

- **CameraController.cs**  
   The camera follows the player from a bird's eye view.

- **Rotator.cs**  
   Pickup collectible rotation is implemented in this script, and this will make it easier to notice.

####################
## How to Play
####################

1. To play, click Start Game.
2. To move through the maze, WASD keys or arrow keys should be used.
3. Use the hot/cold detector:
   - Blue denotes Cold
   - Orange denotes Warm
   - Red color denotes Hot
4. To find the target, use the detector.
5. Before the AI catches you, collect the target.
6. Return to the start menu, if you win or lose.

#######################
## Technical Details
#######################

### Hot/Cold Detector
The game checks how far the player is from the target. Based on the distance, the display changes color and shows Cold, Warm, or Hot.

It also plays a short beep to help guide the player. The beep happens faster as the player gets closer to the target.

### Enemy
The enemy uses Unity NavMesh to move through the maze and chase the player. It starts slow, then gradually gets faster as the round continues, making the game harder over time.

### Random Spawning
The target and enemy spawn at preset points on valid maze paths. This stops them from appearing inside walls or in places the player cannot reach.

The spawn system also checks the player position so the target and enemy do not start too close to the player.

### Timer and Round Flow
The timer starts when the round begins and stops when the player wins or loses. After the round ends, the player can return to the start menu and start a new randomized round.

##################################
## Assets and Resources Used / Modified
##################################

This project uses a combination of custom-created assets, Unity learning resources, and third-party audio/reference materials.

### Unity Learning Material / Code Reference

Some project structure and C# scripting concepts were adapted from Unity learning/tutorial material, including:

- Player movement using Rigidbody physics
- Pickup collection using trigger collisions
- Camera following the player
- Rotating collectible object
- UI text updates
- Basic win/lose condition logic

These concepts were modified for this project by adding:

- Over time, AI enemy speed will increase
- Detection for Hot/Cold proximity
- Beep frequency is Distance-based 
- AI enemy chase behavior using NavMesh
- Enemy and Target spawn points are randomized in each round
- Pause and Return-to-Menu functionality
- Timer HUD implementation
- Customized behavior for Win/Lose conditions

### Custom / Modified Assets

- Custom maze layout
- Expanded maze map
- 5 custom shaders/materials
- Custom UI layout and menu system
- Hot/cold detector UI
- Pickup animation/rotation and Enemy AI Animation bounce/stretch 
- Collision-based VFX for target collection and enemy collision

### Maze Design Reference

-https://www.doyoumaze.com/blog/category/types+of+mazes

### Music and Sound Sources

- https://freesound.org/people/Wax_vibe/sounds/550332/
- https://freesound.org/people/jmguru/sounds/117723/
- https://freesound.org/people/MrFossy/sounds/590042/
- https://freesound.org/people/ATP2-kh/sounds/844243/
- https://freesound.org/people/TRP/sounds/616821/

####################
## Project Status
####################

Completed as a playable Unity class project prototype.

