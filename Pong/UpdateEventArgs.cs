using System;
using System.Drawing;

namespace Pong.GUI
{
	public class UpdateEventArgs : EventArgs
	{
		public Graphics Graphics { get; private set; }
		public Rectangle Rectangle { get; private set; }

		public UpdateEventArgs (Graphics graphics, Rectangle rectangle)
		{
			Graphics = graphics;
			Rectangle = rectangle;
		}
	}
}
