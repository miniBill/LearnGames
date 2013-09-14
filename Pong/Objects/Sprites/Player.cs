using Pong.Objects;
using Pong.Extensions;

namespace Pong.Objects.Sprites {
	class Player : MovingSprite
	{
		public bool AI { get; private set; }

		public Player (bool ai, World world)
			: base (world, 1 / 100.0, ai ? 1 / 10.0 : 1 / 8.0)
		{
			X = ai ? 1 - Width / 2 : Width / 2 - 1;
			AI = ai;
		}

		public const double TOP_SPEED = 0.3;

		public override void Update (double delta)
		{
			base.Update (delta);
			if (!AI)
				return;
			if (World.Ball.X > -0.5) {
				double projected = World.Ball.Y.Project (Bottom + Width / 3, Top - Width / 3 , -TOP_SPEED, TOP_SPEED);
				SpeedY = projected.Clamp (-TOP_SPEED, TOP_SPEED);
			} else {
				if (Top > 1 - World.CollisionTolerance || Bottom < World.CollisionTolerance - 1)
					SpeedY *= -1;
			}
		}
	}
}
