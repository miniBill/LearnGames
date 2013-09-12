using System;
using System.Drawing;

namespace Pong.Game.Sprites
{
	class Ball : MovingSprite
	{
		private readonly World world;
		private double speed = 2.5;

		public Ball(World world)
			: base(1 / 100.0, 3.0 / 2.0 / 100.0)
		{
			this.world = world;
		}

		public bool Stopped
		{
			get { return Math.Abs(SpeedX * SpeedX + SpeedY * SpeedY) < 0.001; }
		}

		public override void Update(double delta)
		{
			base.Update(delta);
			Player collision = Collides(world.Player)
				? world.Player
				: Collides(world.AI)
					? world.AI
					: null;
			if (collision != null)
			{
				speed *= 11 / 10.0;
				double diff = Y - collision.Y;
				double projected = diff.Project(-collision.Height / 2, collision.Height / 2, 0, 8).Clamp(0, 7);
				var segment = (int)Math.Floor(projected);
				int xsign = Math.Sign(SpeedX);
				switch (segment)
				{
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
				SpeedX *= -1;
				RescaleSpeed();
			}
			if (Math.Abs(Top) < world.CollisionTolerance || Math.Abs(Bottom - 1) < world.CollisionTolerance)
				SpeedY *= -1;
		}

		readonly Random rnd = new Random(0);

		public void Kick()
		{
			speed = 2.5;
			SpeedX = rnd.Next() % 2 == 0 ? 1 : -1;
			SpeedY = RandomSpeedY();
			RescaleSpeed();
		}

		private void RescaleSpeed()
		{
			double length = Math.Sqrt(SpeedX * SpeedX + SpeedY * SpeedY);
			SpeedX *= speed / length * 2 / 3;
			SpeedY *= speed / length;
		}

		private double RandomSpeedY()
		{
			return rnd.NextDouble().Project(0, 1, -1, 1);
		}

		public override void DrawTo(Graphics graphics, Size rectangle)
		{
			graphics.FillRectangle(Brushes.White, GetRectangle(rectangle));
		}
	}
}