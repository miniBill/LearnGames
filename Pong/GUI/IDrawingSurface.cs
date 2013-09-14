using System;
using Pong.GUI;

namespace Pong.GUI
{
	public interface IDrawingSurface
	{
		event EventHandler<UpdateEventArgs> Update;
	}
}
