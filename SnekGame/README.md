# Snek Game

This is a simple Snake clone in C# with a WFA for a UI. The snake moves via redrawing the segemnt(s) at the next position based on the direction.
It has collision detection for the sides of the playing field, the food and yourself.

## Game Logic: Collision Handling

```c#
					int maxXPos = pbCanvas.Size.Width / Settings.Width;
					int maxYPos = pbCanvas.Size.Height / Settings.Height;

					// detect collisions with game borders
					if (Snake[i].X < 0 || Snake[i].Y < 0
						|| Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos)
					{
						Die();
					}

					// detect collisions with body
					for (int j = 1; j < Snake.Count; j++)
					{
						if (Snake[i].X == Snake[j].X &&
							Snake[i].Y == Snake[j].Y)
						{
							Die();
						}
					}

					// detect collision with food
					if (Snake[0].X == food.X && Snake[0].Y == food.Y)
					{
						Eat();
					}
```
## Features to add
* Title screen of some kind
* Start button attached to StartGame(); or press enter to start
* Score logging to text file
* High scores display pulled from log file
* Simple sound effects for movement, eating and dying
