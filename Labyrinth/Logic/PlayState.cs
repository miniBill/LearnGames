using Labyrinth.GUI;
using OpenTK;
using OpenTK.Input;

namespace Labyrinth.Logic {
	class PlayState : IGameState
	{
		readonly World world = new World ();

		public IGameState Update (FrameEventArgs e, KeyboardDevice keyboard)
		{
			world.Update (e, keyboard);
			return this;
		}

		public void Render (FrameEventArgs e)
		{
			world.Render (e);
		}

		public void Render2D (FrameEventArgs e)
		{
		}
	}
}
