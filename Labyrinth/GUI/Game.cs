using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Labyrinth.Logic;

namespace Labyrinth.GUI {
	class Game : GameWindow
	{
		IGameState currentState;

		/// <summary>Creates a 800x600 window with the specified title.</summary>
		public Game (int width, int height)
			: base (width, height, GraphicsMode.Default, "Labyrinth", GameWindowFlags.Fullscreen)
		{
			VSync = VSyncMode.On;

			Letter.LoadFont ();

			currentState = new WelcomeState ();
		}

		/// <summary>Load resources here.</summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);

			GL.ClearColor (0, 0, 0, 0.0f);
			GL.Enable (EnableCap.DepthTest);
		}

		/// <summary>
		/// Called when your window is resized. Set your viewport here. It is also
		/// a good place to set up your projection matrix (which probably changes
		/// along when the aspect ratio of your window).
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnResize (EventArgs e)
		{
			base.OnResize (e);

			GL.Viewport (ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
		}

		/// <summary>
		/// Called when it is time to setup the next frame. Add you game logic here.
		/// </summary>
		/// <param name="e">Contains timing information for framerate independent logic.</param>
		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			base.OnUpdateFrame (e);

			if (Keyboard [Key.Escape] || Keyboard [Key.Q])
				Exit ();

			currentState = currentState.Update (e, Keyboard);
		}

		readonly FPSCounter counter = new FPSCounter ();

		/// <summary>
		/// Called when it is time to render the next frame. Add your rendering code here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);

			GL.Clear (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView ((float)Math.PI / 4, Width / (float)Height, 1.0f, 99.0f);
			GL.MatrixMode (MatrixMode.Projection);
			GL.LoadIdentity ();
			GL.LoadMatrix (ref projection);

			var modelview = GetModelView ();
			GL.MatrixMode (MatrixMode.Modelview);
			GL.LoadMatrix (ref modelview);

			currentState.Render (e);

			GL.MatrixMode (MatrixMode.Projection);
			GL.LoadIdentity ();
			float prop = Width / (float)Height;
			GL.Ortho (-prop, prop, -1, 1, -1, 1);

			GL.MatrixMode (MatrixMode.Modelview);
			GL.LoadIdentity ();

			GL.Disable (EnableCap.CullFace);
			GL.Clear (ClearBufferMask.DepthBufferBit);

			currentState.Render2D (e);

			int fps = counter.Update (e.Time);
			Letter.RenderString (fps.ToString (), 0, 0.95f, HAlign.Center, VAlign.Top, 0.3f);

			SwapBuffers ();
		}

		Matrix4 GetModelView ()
		{
			Vector3 pos;
			Vector3 up;
			if (currentState is WelcomeState) {
				pos = new Vector3 (0, 0, 50);
				up = Vector3.UnitY;
			} else {
				pos = new Vector3 (0, 30, 0);
				up = -Vector3.UnitZ;
			}
			Matrix4 modelview = Matrix4.LookAt (pos, Vector3.Zero, up);
			return modelview;
		}
	}
}

