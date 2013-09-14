using System;
using System.Drawing;
using Pong.GUI;
using System.Diagnostics;
using OpenTK.Input;
using Pong.Objects;
using Pong.Objects.Sprites;
using Pong.Extensions;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Pong.Logic {
	public class GameController
	{
		readonly World world = new World ();

		public GameController (IGame game)
		{
			game.Update += Game_Update;
			game.Render += Game_Render;
		}

		bool started;
		bool paused;
		readonly FPSCounter counter = new FPSCounter();

		ulong playerPoints;
		ulong aiPoints;

		void Game_Update (object sender, FrameEventArgs e)
		{
			double delta = e.Time;

			if (!started)
				return;
			if (!paused)
				world.Update (delta);
			if (world.Ball.Stopped)
				world.Ball.Kick ();
			if (world.Ball.Left < world.CollisionTolerance) {
				aiPoints++;
				world.Ball.X = world.Ball.Y = 0.5;
				world.Ball.Kick ();
			} else if (world.Ball.Right > 1 - world.CollisionTolerance) {
				playerPoints++;
				world.Ball.X = world.Ball.Y = 0.5;
				world.Ball.Kick ();
			}
		}

		void Game_Render (object sender, FrameEventArgs e)
		{
			double delta = e.Time;

			if (!started) {
				;//graphics.DrawString ("Press ENTER to begin!", introFont, Brushes.White, rectangle.GetCenter (), introFormat);
			} else {
				if (!paused)
					world.Update (delta);
				if (world.Ball.Stopped)
					world.Ball.Kick ();
				int fps = counter.Update (delta);
				graphics.FillRectangle (Brushes.White, size.Width * 99.0f / 200.0f, 0, size.Width / 100.0f, size.Height);
				graphics.DrawString (String.Format ("{0} FPS", fps), pointsFont, Brushes.Blue,
					size.Width / 2.0f, 0, centerFormat);
				graphics.DrawString (String.Format ("{0}", playerPoints), pointsFont, Brushes.White,
					0, 0);
				graphics.DrawString (String.Format ("{0}", aiPoints), pointsFont, Brushes.White,
					size.Width, 0, rightFormat);
				world.DrawTo (graphics, size);
				if (paused) {
					graphics.DrawString ("P A U S E D", introFont, Brushes.Gray, rectangle.GetCenter (),
						introFormat);
				}

				if (world.Ball.Left < world.CollisionTolerance) {
					aiPoints++;
					world.Ball.X = world.Ball.Y = 0.5;
					world.Ball.Kick ();
				} else if (world.Ball.Right > 1 - world.CollisionTolerance) {
					playerPoints++;
					world.Ball.X = world.Ball.Y = 0.5;
					world.Ball.Kick ();
				}
			}
		}

		public void KeyDown (Key keyCode)
		{
			switch (keyCode) {
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

		public void KeyUp (Key keyCode)
		{
			switch (keyCode) {
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
