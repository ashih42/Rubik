# Rubik
Play and solve a 3x3x3 [Rubik's cube](https://en.wikipedia.org/wiki/Rubik%27s_Cube) with [Thistlethwaite's algorithm](https://en.wikipedia.org/wiki/Morwen_Thistlethwaite#Thistlethwaite's_algorithm) in Unity C#. (42 Silicon Valley)

![superflip](https://github.com/ashih42/Rubik/blob/master/Screenshots/superflip.gif)

Made in Unity version 2018.3.0f2  
Recommended screen resolution: 1280 x 768

## Releases

* [Windows Build](https://github.com/ashih42/Rubik/releases/download/v00/Rubik-Windows-v00.zip)

## Controls

You can toggle `Face Labels` to see the notation follows the [Western color scheme](https://ruwix.com/the-rubiks-cube/japanese-western-color-schemes/):
* `L` Orange face
* `R` Red face
* `D` Yellow face
* `U` White face
* `B` Blue face
* `F` Green face

### Camera Controls
* `Arrow Keys` Rotate entire cube.

### Cube Operations
* `L`, `R`, `D`, `U`, `B`, `F` Rotate 90° clockwise.
* `Control` + `L`, `R`, `D`, `U`, `B`, `F` Rotate 180° clockwise.
* `Shift` + `L`, `R`, `D`, `U`, `B`, `F` Rotate 90° counter-clockwise.
* `Backspace` Undo last move.

### Initializing Cube State
* Enter sequence of moves in the text field, e.g. `F R2 U' D2 B L'` and then click `Reset` to initialize the cube.

## Notes on Thistlethwaite's Algorithm

* Apply bi-directional breadth first search on 4 subproblems:
  * Find path G0 -> G1
  * Find path G1 -> G2
  * Find path G2 -> G3
  * Find path G3 -> G4
* Implementation adapted from Stefan Pochmann's submission for [Shortest Program Contest](https://tomas.rokicki.com/cubecontest/)
