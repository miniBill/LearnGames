using Pong.Extensions;

namespace Pong.Objects.Sprites {
	abstract class MovingSprite : Sprite
	{
		public double SpeedX { get; set; }

		public double SpeedY { get; set; }

		protected MovingSprite (World world, double width, double height) : base (world, width, height)
		{
		}

		public virtual void Update (double delta)
		{
			X += delta * SpeedX;
			X = X.Clamp (Width / 2 - 1, 1 - Width / 2);
			Y += delta * SpeedY;
			Y = Y.Clamp (Height / 2 - 1, 1 - Height / 2);
		}

		protected bool Collides (Sprite other)
		{
			bool collideX = Right >= other.Left && Left <= other.Right;
			bool collideY = Bottom <= other.Top && Top >= other.Bottom;
			return collideX && collideY;
		}
	}
}