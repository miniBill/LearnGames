using OpenTK;
using OpenTK.Input;
using Labyrinth.GUI;

namespace Labyrinth.Logic {
	class WelcomeState : IGameState
	{
		public IGameState Update (FrameEventArgs e, KeyboardDevice keyboard)
		{
			if (keyboard [Key.Enter])
				return new PlayState ();

			return this;
		}

		public void Render (FrameEventArgs e)
		{
		}

		public void Render2D (FrameEventArgs e)
		{
			Letter.RenderString ("Hello");
			Letter.RenderString ("Press ENTER to continue", Letter.GetWidth ("Hello") / 2.0f, -Letter.Height, HAlign.Right, VAlign.Top, 0.15f);
		}
	}
}
