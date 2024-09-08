# 2D game: BunnyInvasion

 
## Screen Position vs. World Position
- Screen Position: This is a position on the screen, which is based on pixels. For example, the top-left corner of the screen is (0, 0) in screen space, and the bottom-right corner is (Screen.width, Screen.height). This is what Input.mousePosition gives you — the mouse’s current position on the screen.

- World Position: This is the location of something within the game world. Unity uses a Cartesian coordinate system for this. For example, the world position (0, 0, 0) is the center of the game world in a 3D scene, and objects can be placed anywhere relative to that. For 2D games, you usually only care about the x and y coordinates, while z is often set to 0.
