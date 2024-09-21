# 2D game: BunnyInvasion

 
## Screen Position vs. World Position
- Screen Position: This is a position on the screen, which is based on pixels. For example, the top-left corner of the screen is (0, 0) in screen space, and the bottom-right corner is (Screen.width, Screen.height). This is what Input.mousePosition gives you — the mouse’s current position on the screen.

- World Position: This is the location of something within the game world. Unity uses a Cartesian coordinate system for this. For example, the world position (0, 0, 0) is the center of the game world in a 3D scene, and objects can be placed anywhere relative to that. For 2D games, you usually only care about the x and y coordinates, while z is often set to 0.

------------------------------------------------------------------
## Additional:
### SOLID standard:
- S - Single Responsibility Principle:
A class should have only one reason to change, meaning it should only have one job or responsibility. This ensures that each class has a clear purpose, making the code easier to maintain and modify.
- O - Open/Closed Principle:
Software entities (like classes, modules, functions) should be open for extension, but closed for modification. This means that the behavior of a class can be extended without modifying its source code, typically achieved through abstraction (like interfaces or inheritance).
- L - Liskov Substitution Principle:
Objects of a subclass should be able to replace objects of the superclass without affecting the correctness of the program. Essentially, subclasses should behave in a way that doesn't break the contract established by the parent class.
- I - Interface Segregation Principle:
Clients should not be forced to depend on interfaces they don't use. Instead of having large, monolithic interfaces, it's better to have smaller, more specific interfaces that are tailored to the needs of different clients.
- D - Dependency Inversion Principle:
High-level modules should not depend on low-level modules. Both should depend on abstractions (e.g., interfaces). This means that dependencies between classes should be on interfaces or abstract classes, not concrete implementations.