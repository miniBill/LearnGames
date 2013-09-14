using Pong.GUI;
using OpenTK.Input;
using Pong.Objects;
using Pong.Objects.Sprites;
using OpenTK;

namespace Pong.Logic {
	class GameController
	{
		readonly World world = new World ();
		readonly IGame game;

		public GameController (IGame game)
		{
			this.game = game;
			game.Update += Game_Update;
			game.Render += Game_Render;
			game.Keyboard.KeyDown += KeyDown;
			game.Keyboard.KeyUp += KeyUp;
		}

		bool started;
		bool paused;
		readonly FPSCounter counter = new FPSCounter ();
		ulong playerPoints;
		ulong aiPoints;

		void Game_Update (object sender, FrameEventArgs e)
		{
			double delta = e.Time;

			if (!started)
				return;
			if (!paused)
				world.Update (delta);
			Ball ball = world.Ball;
			if (ball.Stopped)
				ball.Kick ();
			if (ball.Left < world.CollisionTolerance - 1) {
				aiPoints++;
				ball.Kick ();
			} else if (ball.Right > 1 - world.CollisionTolerance) {
				playerPoints++;
				ball.Kick ();
			}
		}

		static void RenderFPS (int fps)
		{
			RenderString (fps.ToString (), 0.0f, 0.9f, HAlign.Center, VAlign.Top);
		}

		void RenderScore ()
		{
			RenderString (playerPoints.ToString (), -0.9f, 0.9f, HAlign.Left, VAlign.Top);
			RenderString (aiPoints.ToString (), 0.9f, 0.9f, HAlign.Right, VAlign.Top);
		}

		static void RenderPaused ()
		{
			RenderString ("P A U S E D", 0.0f, 0.0f, HAlign.Center, VAlign.Center);
		}

		static void RenderStart ()
		{
			RenderString ("Press ENTER to begin!", 0.0f, 0.0f, HAlign.Center, VAlign.Center);
		}

		static void RenderString (string value, float x, float y, HAlign halign, VAlign valign)
		{
			switch (halign) {
				case HAlign.Center:
					x -= value.Length / 2.0f * Letter.Width;
					break;
				case HAlign.Right:
					x -= value.Length * Letter.Width;
					break;
			}
			switch (valign) {
				case VAlign.Center:
					y += Letter.Height / 2.0f;
					break;
				case VAlign.Bottom:
					y += Letter.Height;
					break;
			}
			for (int i = 0; i < value.Length; i++) {
				char c = value [i];
				RenderLetter (c, x, y);
				x += Letter.Width + Letter.Spacing;
			}
		}

		static void RenderLetter (char c, float x, float y)
		{
			Letter.GetLetter (c).Render (x, y);
		}

		void Game_Render (object sender, FrameEventArgs e)
		{
			double delta = e.Time;

			if (!started) {
				RenderStart ();
			} else {
				if (!paused)
					world.Update (delta);
				int fps = counter.Update (delta);
				RenderFPS (fps);
				RenderScore ();
				world.Draw ();
				if (paused)
					RenderPaused ();
			}
		}

		public void KeyDown (object sender, KeyboardKeyEventArgs e)
		{
			switch (e.Key) {
				case Key.Enter:
					started = true;
					paused = false;
					break;
				case Key.Up:
				case Key.W:
					world.Player.SpeedY = Player.TOP_SPEED * 1.5;
					paused = false;
					break;
				case Key.Down:
				case Key.S:
					world.Player.SpeedY = -Player.TOP_SPEED * 1.5;
					paused = false;
					break;
				case Key.Q:
				case Key.Escape:
					game.Exit ();
					break;
				case Key.Space:
				case Key.P:
					paused = !paused;
					break;
			}
		}

		public void KeyUp (object sender, KeyboardKeyEventArgs e)
		{
			switch (e.Key) {
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
