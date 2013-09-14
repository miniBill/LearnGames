using OpenTK;
using OpenTK.Input;

namespace Labyrinth.Logic {
	interface IGameState
	{
		IGameState Update (FrameEventArgs e, KeyboardDevice keyboard);

		void Render (FrameEventArgs e);

		void Render2D (FrameEventArgs e);
	}
}

