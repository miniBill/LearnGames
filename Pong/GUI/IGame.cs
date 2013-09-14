using System;
using OpenTK;
using OpenTK.Input;

namespace Pong.GUI {
	public interface IGame
	{
		KeyboardDevice Keyboard { get; }

		void Exit ();

		event EventHandler<FrameEventArgs> Update;
		event EventHandler<FrameEventArgs> Render;
	}
}
