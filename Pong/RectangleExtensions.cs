using System.Drawing;

namespace Pong
{
	static class RectangleExtensions
	{
		public static Point GetCenter(this Rectangle rectangle)
		{
			return new Point(rectangle.Left + rectangle.Width/2, rectangle.Top + rectangle.Height/2);
		}
	}
}
