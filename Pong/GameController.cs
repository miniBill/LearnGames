using System;
using System.Drawing;
using Pong.GUI;
using System.Diagnostics;
using Pong.Game;
using OpenTK.Input;
using Pong.Sprites;

namespace Pong
{
	public class GameController
	{
		readonly World world = new World();
		Stopwatch timer;

		public GameController(IDrawingSurface drawingSurface)
		{
			drawingSurface.Update += DrawingSurface_Update;
			introFont = new Font(FontFamily.GenericMonospace, 30.0f, FontStyle.Bold);
			pointsFont = new Font(FontFamily.GenericMonospace, 20.0f, FontStyle.Bold);
		}

		public void Start()
		{
			timer = new Stopwatch();
			timer.Start();
		}

		bool started;
		bool paused;
		long saved;
		readonly FPSCounter counter;

		readonly StringFormat introFormat = new StringFormat
		{
			Alignment = StringAlignment.Center,
			LineAlignment = StringAlignment.Center
		};

		readonly StringFormat centerFormat = new StringFormat
		{
			Alignment = StringAlignment.Center,
			LineAlignment = StringAlignment.Near
		};

		readonly StringFormat rightFormat = new StringFormat
		{
			Alignment = StringAlignment.Far,
			LineAlignment = StringAlignment.Near
		};

		ulong playerPoints;
		ulong aiPoints;
		readonly Font introFont;
		readonly Font pointsFont;
		void DrawingSurface_Update(object sender, UpdateEventArgs e)
		{
			if (timer == null)
				return;
			long ticks = timer.ElapsedTicks;
			long idelta = ticks - saved;
			double delta = idelta / 10000000.0;
			saved = ticks;
			if (idelta == 0)
				return;

			var graphics = e.Graphics;
			var rectangle = e.Rectangle;
			if (!started)
			{
				graphics.DrawString("Press ENTER to begin!", introFont, Brushes.White, rectangle.GetCenter(),
					introFormat);
			}
			else
			{
				if (!paused)
					world.Update(delta);
				if (world.Ball.Stopped)
					world.Ball.Kick();
				Size size = rectangle.Size;
				int fps = counter.Update(ticks);
				graphics.FillRectangle(Brushes.White, size.Width * 99.0f / 200.0f, 0, size.Width / 100.0f, size.Height);
				graphics.DrawString(String.Format("{0} FPS", fps), pointsFont, Brushes.Blue,
					size.Width / 2.0f, 0, centerFormat);
				graphics.DrawString(String.Format("{0}", playerPoints), pointsFont, Brushes.White,
					0, 0);
				graphics.DrawString(String.Format("{0}", aiPoints), pointsFont, Brushes.White,
					size.Width, 0, rightFormat);
				world.DrawTo(graphics, size);
				if (paused)
				{
					graphics.DrawString("P A U S E D", introFont, Brushes.Gray, rectangle.GetCenter(),
					introFormat);
				}

				if (world.Ball.Left < world.CollisionTolerance)
				{
					aiPoints++;
					world.Ball.X = world.Ball.Y = 0.5;
					world.Ball.Kick();
				}
				else if (world.Ball.Right > 1 - world.CollisionTolerance)
				{
					playerPoints++;
					world.Ball.X = world.Ball.Y = 0.5;
					world.Ball.Kick();
				}
			}
		}

		public void KeyDown(Key keyCode)
		{
			switch (keyCode)
			{
				case Key.Enter:
					started = true;
					paused = false;
					break;
				case Key.Up:
				case Key.W:
					world.Player.SpeedY = -Player.TOP_SPEED * 1.5;
					paused = false;
					break;
				case Key.Down:
				case Key.S:
					world.Player.SpeedY = Player.TOP_SPEED * 1.5;
					paused = false;
					break;
				case Key.Q:
				case Key.Escape:
					timer.Stop ();
					Environment.Exit (0);
					break;
				case Key.Space:
				case Key.P:
					paused = !paused;
					break;
			}
		}

		public void KeyUp(Key keyCode)
		{
			switch (keyCode)
			{
				case Key.Up:
				case Key.W:
				case Key.Down:
				case Key.S:
					world.Player.SpeedY = 0;
					break;
			}
		}
	}
}
