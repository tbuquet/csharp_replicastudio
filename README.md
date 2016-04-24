csharp_replicastudio
===========
Replica Studio was a school project done for the ETNA, Paris in 2012 with a team of 8 students. I was the C# developer of the project.
The objective of this software is to provide an easy way to create your own old school point of click games.
This software provides an editor and a "viewer", running with the XNA framework.

Although the development was put on hold, it offers a varierty of features to create and test a game.

##Requirements##
- Framework .NET 4.0
- VS2010, or a way to use XNA projects on VS2012+
- XNA Game Studio 4.0 > [Link](https://www.microsoft.com/en-us/download/details.aspx?id=23714)
- log4net library
- fmod library (viewer only)

##Editor Features##
- Creation a projet for a large variety of resolutions
- Import of pictures/musics
- Creation of different scenes
- Managing different layers of backgrounds and decors, in which the character will appear in front of them or behind them.
- Adding character, animated items in the scene
- Adding walkable areas in the scene, using vertex calculation
- Adding regions areas in the scene, that will determine how big a character will appear in the scene
- Managing a database of characters (playable or not), items, actions, classes, animations, events and dictonaries.
- Creation of animations given a list of sprites (with the help of PCSpriteCreator)
- Different levels of zoom in the scene
- Script manager for the game, offering a wide variety of pre-made scripts, like classic programming functions but also write a message, random functions, manage inventory, animation management, interface management and so on.

##How to install##
- Install XNA Game Studio 4.0
- If not on VS2010, follow [these instructions](http://stackoverflow.com/questions/10881005/how-to-install-xna-game-studio-on-visual-studio-2012)
- Load and compile the solution
- Make sure that log4net is present if using the editor.
- Make sure that fmodex is present if using the viewer.

##TODO##
- Fix and reactivate ColorPanel (managing hue of different decors/objects)
