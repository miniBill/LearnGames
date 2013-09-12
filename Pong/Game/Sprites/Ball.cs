using System;
using System.Drawing;

namespace Pong.Game.Sprites
{
	class Ball : MovingSprite
	{
		private readonly World world;

		public Ball(World world)
			: base(1 / 100.0, 1 / 100.0)
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
			if (Collides(world.Player) || Collides(world.AI))
				SpeedX *= -1;
			if (Math.Abs(Top) < world.CollisionTolerance || Math.Abs(Bottom - 1) < world.CollisionTolerance)
				SpeedY *= -1;
		}

		readonly Random rnd = new Random(0);

		public void Kick()
		{
			SpeedX = rnd.NextDouble().Project(0, 1, -3, 3);
			SpeedY = rnd.NextDouble().Project(0, 1, -2, 2);
			while (Math.Abs(SpeedY) < 1.5)
				SpeedY *= 2;
			while (Math.Abs(SpeedY) < 1)
				SpeedY *= 2;
		}

		public override void DrawTo(Graphics graphics, Size rectangle)
		{
			graphics.FillRectangle(Brushes.White, GetRectangle(rectangle));
		}
	}
}