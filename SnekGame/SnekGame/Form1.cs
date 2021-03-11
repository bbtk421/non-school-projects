using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace SnekGame
{
	public partial class Form1 : Form
	{
		private List<Circle> Snake = new List<Circle>(); // create list of circles for snake
		private Circle food = new Circle();

		public Form1()
		{
			InitializeComponent();
			// settings to default
			new Settings();

			//if (Inputs.KeyPressed(Keys.Enter))
			//{
			//	StartGame();
			//}

				// set game speed and startr timer
			gameTimer.Interval = 1000 / Settings.Speed;
			gameTimer.Tick += UpdateScreen;
			gameTimer.Start();

			// start game message
			//lblStartGame.Visible = true;
			//string startGame = "U R ready to play\nSNEK GAME?\nPlease be pressing Enter.";
			//lblStartGame.Text = startGame;
			// start new game
			StartGame();
		}

		private void StartGame()
		{
			lblGameOver.Visible = false;
			lblStartGame.Visible = false;
			// settings to default
			new Settings();
			// create new player object
			Snake.Clear();
			Circle head = new Circle();
			head.X = 10;
			head.Y = 5;
			Snake.Add(head);

			lblScore.Text = Settings.Score.ToString();
			GenerateFood();

		}
		// place food object randomly
		private void GenerateFood()
		{
			int maxXPos = pbCanvas.Size.Width / Settings.Width;
			int maxYPos = pbCanvas.Size.Height / Settings.Height;

			Random random = new Random();
			food = new Circle();
			food.X = random.Next(0, maxXPos); // x between 0 & max
			food.Y = random.Next(0, maxYPos); // y between 0 & max
		}

		private void UpdateScreen(object sender, EventArgs e)
		{
			// check for game over
			if (Settings.GameOver == true)
			{
				// check if enter is pressed
				if (Inputs.KeyPressed(Keys.Enter))
				{
					StartGame();
				}
			}
			else
			{
				if (Inputs.KeyPressed(Keys.Right) && Settings.direction != Direction.Left) // denies input for opposite direction pressed
					Settings.direction = Direction.Right;
				else if (Inputs.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
					Settings.direction = Direction.Left;
				else if (Inputs.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
					Settings.direction = Direction.Up;
				else if (Inputs.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
					Settings.direction = Direction.Down;

				MovePlayer();
			}

			pbCanvas.Invalidate(); // screen refresh so the snake can move/grow
		}

		private void pbCanvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics canvas = e.Graphics;

			if (!Settings.GameOver)
			{
				// set snake color
				for (int i = 0; i < Snake.Count; i++)
				{
					Brush snakeColor;
					if (i == 0)
						snakeColor = Brushes.OliveDrab; // draw snake head
					else
						snakeColor = Brushes.Olive; // draw body

					// draw snake
					canvas.FillEllipse(snakeColor,
						new Rectangle(Snake[i].X * Settings.Width,
						Snake[i].Y * Settings.Height, Settings.Width,
						Settings.Height));

					// draw food
					canvas.FillEllipse(Brushes.Crimson,
						new Rectangle(food.X * Settings.Width,
						food.Y * Settings.Height, Settings.Width,
						Settings.Height));
				}
			}

			else
			{
				string gameOver = "Game over \nYour final score is: " +
					Settings.Score + "\nPress Enter to try again";
				lblGameOver.Text = gameOver;
				lblGameOver.Visible = true;
			}
		}

		private void MovePlayer()
		{
			for (int i = Snake.Count - 1; i >= 0; i--)
			{
				// move head
				if (i == 0)
				{
					switch (Settings.direction)
					{
						case Direction.Right:
							Snake[i].X++;
							break;
						case Direction.Left:
							Snake[i].X--;
							break;
						case Direction.Up:
							Snake[i].Y--;
							break;
						case Direction.Down:
							Snake[i].Y++;
							break;
					}

					// get max x and y positions
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
				}
				else
				{
					// move body
					Snake[i].X = Snake[i - 1].X;
					Snake[i].Y = Snake[i - 1].Y;
				}
			}
		}

		private void Eat()
		{
			// add circle to body
			Circle food = new Circle();
			food.X = Snake[Snake.Count - 1].X;
			food.Y = Snake[Snake.Count - 1].Y;

			Snake.Add(food);

			// update score
			Settings.Score += Settings.Points;
			lblScore.Text = Settings.Score.ToString();

			GenerateFood();

		}

		private void Die()
		{
			Settings.GameOver = true;
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			Inputs.ChangeState(e.KeyCode, true);
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			Inputs.ChangeState(e.KeyCode, false);
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}

