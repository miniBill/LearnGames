using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Pong.Game.Sprites;

namespace Pong.Game
{
	internal class GameController
	{
		readonly World world = new World();
		private readonly DrawingSurface drawingSurface;
		private Stopwatch timer;

		public GameController(DrawingSurface drawingSurface)
		{
			this.drawingSurface = drawingSurface;
			drawingSurface.Update += drawingSurface_Update;
			introFont = new Font(FontFamily.GenericMonospace, 30.0f, FontStyle.Bold);
			pointsFont = new Font(FontFamily.GenericMonospace, 20.0f, FontStyle.Bold);
		}

		public void Start()
		{
			timer = new Stopwatch();
			timer.Start();
		}

		private bool started;
		private long saved;
		FPSCounter counter = new FPSCounter();

		private readonly StringFormat introFormat = new StringFormat
		{
			Alignment = StringAlignment.Center,
			LineAlignment = StringAlignment.Center
		};

		private readonly StringFormat centerFormat = new StringFormat
		{
			Alignment = StringAlignment.Center,
			LineAlignment = StringAlignment.Near
		};

		private readonly StringFormat rightFormat = new StringFormat
		{
			Alignment = StringAlignment.Far,
			LineAlignment = StringAlignment.Near
		};

		private ulong playerPoints;
		private ulong aiPoints;
		private Font introFont;
		private Font pointsFont;
		private void drawingSurface_Update(object sender, PaintEventArgs e)
		{
			if (timer == null)
				return;
			long ticks = timer.ElapsedTicks;
			long idelta = ticks - saved;
			double delta = idelta / 10000000.0;
			saved = ticks;
			if (idelta == 0)
				return;

			world.Update(delta);
			var graphics = drawingSurface.GetGraphics();
			if (!started)
			{
				graphics.DrawString("Press ENTER to begin!", introFont, Brushes.White, e.ClipRectangle.GetCenter(),
					introFormat);
			}
			else
			{
				if (world.Ball.Stopped)
					world.Ball.Kick();
				Size size = e.ClipRectangle.Size;
				int fps = counter.Update(ticks);
				graphics.FillRectangle(Brushes.White, size.Width * 99.0f / 200.0f, 0, size.Width / 100.0f, size.Height);
				graphics.DrawString(String.Format("{0} FPS", fps), pointsFont, Brushes.Blue,
					size.Width / 2.0f, 0, centerFormat);
				graphics.DrawString(String.Format("{0}", playerPoints), pointsFont, Brushes.White,
					0, 0);
				graphics.DrawString(String.Format("{0}", aiPoints), pointsFont, Brushes.White,
					size.Width, 0, rightFormat);
				world.DrawTo(graphics, size);
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

		public void KeyDown(Keys keyCode)
		{
			switch (keyCode)
			{
				case Keys.Enter:
					started = true;
					break;
				case Keys.Up:
				case Keys.W:
					world.Player.SpeedY = -Player.TopSpeed;
					break;
				case Keys.Down:
				case Keys.S:
					world.Player.SpeedY = Player.TopSpeed;
					break;
				case Keys.Q:
				case Keys.Escape:
					timer.Stop();
					drawingSurface.Stop();
					Application.Exit();
					break;
			}
		}

		public void KeyUp(Keys keyCode)
		{
			switch (keyCode)
			{
				case Keys.Up:
				case Keys.W:
				case Keys.Down:
				case Keys.S:
					world.Player.SpeedY = 0;
					break;
			}
		}
	}
}
