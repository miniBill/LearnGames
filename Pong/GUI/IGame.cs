using System;
using OpenTK;

namespace Pong.GUI
{
	public interface IGame
	{
		event EventHandler<FrameEventArgs> Update;
		event EventHandler<FrameEventArgs> Render;
	}
}
