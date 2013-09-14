using System.Drawing;
using Pong.Objects;
using Pong.Extensions;

namespace Pong.Objects.Sprites
{
	class Player : MovingSprite
	{
		readonly World world;
		public bool AI { get; private set; }

		public Player(bool ai, World world)
			: base(1 / 100.0, ai ? 1 / 10.0 : 1 / 8.0)
		{
			this.world = world;
			X = ai ? 1 - Width / 2 : Width / 2;
			AI = ai;
		}

		public const double TOP_SPEED = 3;

		public override void Update(double delta)
		{
			base.Update(delta);
			if (!AI) return;
			if (world.Ball.X > .25)
			{
				double projected = world.Ball.Y.Project(Top + Width / 3, Bottom - Width / 3, -TOP_SPEED, TOP_SPEED);
				SpeedY = projected.Clamp(-TOP_SPEED, TOP_SPEED);
			}
			else
			{
				if (Top < world.CollisionTolerance || Bottom > 1 - world.CollisionTolerance)
					SpeedY *= -1;
			}
		}

		public override void DrawTo(Graphics graphics, Size rectangle)
		{
			graphics.FillRectangle(Brushes.White, GetRectangle(rectangle));
		}
	}
}
