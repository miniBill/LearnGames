using Labyrinth.Entities;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Labyrinth.Logic {
	class Axes : Entity
	{
		public Axes () : base (Vector3.Zero)
		{
		}

		public override void Render (FrameEventArgs e)
		{
			GL.Begin (BeginMode.Lines);
			GL.Color4 (1.0f, 0.0f, 0.0f, 0.0f);
			GL.Vertex3 (-Vector3.UnitX);
			GL.Vertex3 (2 * Vector3.UnitX);
			GL.End ();

			GL.Begin (BeginMode.Lines);
			GL.Color4 (0.0f, 1.0f, 0.0f, 0.0f);
			GL.Vertex3 (-Vector3.UnitY);
			GL.Vertex3 (2 * Vector3.UnitY);
			GL.End ();

			GL.Begin (BeginMode.Lines);
			GL.Color4 (0.0f, 0.0f, 1.0f, 0.0f);
			GL.Vertex3 (-Vector3.UnitZ);
			GL.Vertex3 (2 * Vector3.UnitZ);
			GL.End ();
		}
	}
}
