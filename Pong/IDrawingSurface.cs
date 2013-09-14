using System;
using Pong.GUI;

namespace Pong.Game
{
	public interface IDrawingSurface
	{
		event EventHandler<UpdateEventArgs> Update;
	}
}
