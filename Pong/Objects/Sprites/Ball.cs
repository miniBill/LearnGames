using System;
using Pong.Objects;
using Pong.Extensions;

namespace Pong.Objects.Sprites {
	class Ball : MovingSprite
	{
		double speed;

		public Ball (World world)
			: base (world, 1 / 100.0, 3.0 / 2.0 / 100.0)
		{
		}

		public bool Stopped {
			get { return Math.Abs (SpeedX * SpeedX + SpeedY * SpeedY) < World.CollisionTolerance * World.CollisionTolerance; }
		}

		public override void Update (double delta)
		{
			base.Update (delta);
			Player collision = Collides (World.Player)
				? World.Player
				: Collides (World.AI)
					? World.AI
					: null;
			if (collision != null) {
				World.Player.Highlight = World.AI.Highlight = false;
				collision.Highlight = true;
				speed *= 11 / 10.0;
				double diff = Y - collision.Y;
				double projected = diff.Project (-collision.Height / 2, collision.Height / 2, 0, 8).Clamp (0, 7);
				var segment = (int)Math.Floor (projected);
				int xsign = -Math.Sign (SpeedX);
				switch (segment) {
					case 0:
						SpeedY = -2;
						SpeedX = xsign;
						break;
					case 1:
						SpeedY = -1;
						SpeedX = xsign;
						break;
					case 2:
						SpeedY = -1;
						SpeedX = 2 * xsign;
						break;
					case 3:
						SpeedY = -1;
						SpeedX = 4 * xsign;
						break;
					case 4:
						SpeedY = 1;
						SpeedX = 4 * xsign;
						break;
					case 5:
						SpeedY = 1;
						SpeedX = 2 * xsign;
						break;
					case 6:
						SpeedY = 1;
						SpeedX = xsign;
						break;
					case 7:
						SpeedY = 2;
						SpeedX = xsign;
						break;
				}
				RescaleSpeed ();
			}
			if (Top > 1 - World.CollisionTolerance || Bottom < World.CollisionTolerance - 1)
				SpeedY *= -1;
		}

		readonly Random rnd = new Random (0);

		public void Kick ()
		{
			speed = 0.3;
			X = 0;
			Y = 0;
			SpeedX = rnd.Next () % 2 == 0 ? 1 : -1;
			SpeedY = rnd.NextDouble ().Project (0, 1, -1, 1);
			RescaleSpeed ();
		}

		void RescaleSpeed ()
		{
			double length = Math.Sqrt (SpeedX * SpeedX + SpeedY * SpeedY);
			SpeedX *= speed / length;
			SpeedY *= speed / length;
		}
	}
}