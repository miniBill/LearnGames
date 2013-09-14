using System.Drawing;
using Pong.Extensions;

namespace Pong.Objects.Sprites
{
	abstract class MovingSprite
	{
		public double SpeedX { get; protected set; }
		public double SpeedY { get; set; }
		public double Width { get; private set; }
		public double Height { get; private set; }
		public double X { get; set; }
		public double Y { get; set; }
		public double Left { get { return X - Width / 2; } }
		public double Right { get { return X + Width / 2; } }
		public double Top { get { return Y - Height / 2; } }
		public double Bottom { get { return Y + Height / 2; } }

		protected MovingSprite(double width, double height)
		{
			Width = width;
			Height = height;
		}

		public virtual void Update(double delta)
		{
			X += delta * SpeedX;
			X = X.Clamp(Width / 2, 1 - Width / 2);
			Y += delta * SpeedY;
			Y = Y.Clamp(Height / 2, 1 - Height / 2);
		}

		public abstract void DrawTo(Graphics graphics, Size rectangle);

		protected static RectangleF GetRectangle(double px, double py, double pwidth, double pheight, Size rectangle)
		{
			var x = (float)px.Project(0, 1, 0, rectangle.Width);
			var y = (float)py.Project(0, 1, 0, rectangle.Height);
			var width = (float)pwidth.Project(0, 1, 0, rectangle.Width);
			var height = (float)pheight.Project(0, 1, 0, rectangle.Height);
			return new RectangleF(x - width / 2, y - height / 2, width, height);
		}

		protected bool Collides(MovingSprite other)
		{
			bool collideX = Right >= other.Left && Left <= other.Right;
			bool collideY = Bottom >= other.Top && Top <= other.Bottom;
			return collideX && collideY;
		}

		protected RectangleF GetRectangle(Size rectangle)
		{
			return GetRectangle(X, Y, Width, Height, rectangle);
		}
	}
}