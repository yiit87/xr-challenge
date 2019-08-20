Implemented Systems:

Basic requirements:
- WASD movement system (Xbox controller should work too)
- A simple pickup system for multiple pickable objects (Star, Health, Speed)
- Displaying total score that is collected in a level
- There are more than 5 object to pick up in each level
- There are walls in the game to hold the player in the area
- If player pickups 5 star in total they can finish the level and continue to the next one
- If player didnt pick up 5 star but tries to finish the level a warning will be show to the player
-GIT: Ive used the fork to copy the repository and pushing to that copy ever since. While im sending a zip file with git local folder in it, you should be able to merge my repository with yours 

As extra asset I only used graphical textures, audio files for level musics and sound effects and also the player character and its animations were ready. All i did was connecting the correct animation in correct time. 

Extras:

- Level map creation system is created by a data driven method. In the folder LEVEL GENERATE you can see txt files. Each txt file contains different numbers. Each number represent a different object in game. As an example 1 is a wall. The whole map in game created with this system. 
- Things that is not added to the map with this system : Enemies, Stars, Player
- There is a full game loop. There are currently 3 level exist. (i was going to add 5 but didnt have time to edit the maps). In each level player has to gather 5 stars in order to complete the level. There are also enemies floating around. If they see you they will shoot you with their laser system. As soon as you got hit you die. 
- There are also traps on the map. if you walk into it, you wont die but it will hurt you and reduce your health
- Ive added a health bar to the game so you can see what is your condition. If its full red you are dead..
- UI also shows the total collected star number. And a warning added if you try to leave the level without collecting all 5 stars
- Each level has a background music. Buttons have sound effects when pressed and stars has sound effects when picked.

Other Extras:
- Camera follows the player. If you get closer to a wall below you (bottom of the screen) camera will move up.
- Enemy model has some particle effects on them that i created and added.
- Enemy system using FSM. I wanted to keep it simple so ive implemented only patrol and attack. 
- Character has jumping/walking animations. This will be different when you pick up a speed pickable.
- I added 2 extra pickable objects.. Both inherits from Pickup class. 1 for health which increases your health. Other one is for speed which make you faster for a while
- There is a pause menu exist. If you press ESC it become active. 


Things to add/Improve:
-There are tons of things that can bee added. One of them is a better AI system for enemy. Definetly some sound effects for shooting and for hoevering. There can be different style of enemies too. 
-More level details. With current system its actually not that difficult to add different models to each map like rocks, trees or other elemenets to make map more interesting.
-There can be a save load system for scores maybe for higher score.. 
-An options menu definetley needed but due to limited time i havent implemented
-Also a scene where you can select different levels would be beneficial from users perspective.
