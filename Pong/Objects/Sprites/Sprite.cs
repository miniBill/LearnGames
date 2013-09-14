using OpenTK.Graphics.OpenGL;

namespace Pong.Objects.Sprites {
	abstract class Sprite
	{
		public World World { get; private set; }

		public double Width { get; private set; }

		public double Height { get; private set; }

		public double X { get; set; }

		public double Y { get; set; }

		public bool Highlight { get; set; }

		public double Left { get { return X - Width / 2; } }

		public double Right { get { return X + Width / 2; } }

		public double Top { get { return Y + Height / 2; } }

		public double Bottom { get { return Y - Height / 2; } }

		protected Sprite (World world, double width, double height)
		{
			World = world;
			Width = width;
			Height = height;
		}

		public void Draw ()
		{
			const float Z = -2.5f;

			GL.Begin (BeginMode.TriangleFan);

			if (Highlight)
				GL.Color4 (1.0f, 0.0f, 0.0f, 0.0f);
			else
				GL.Color4 (1.0f, 1.0f, 1.0f, 0.0f);
			GL.Vertex3 (Left, Bottom, Z);
			GL.Vertex3 (Right, Bottom, Z);
			GL.Vertex3 (Right, Top, Z);
			GL.Vertex3 (Left, Top, Z);

			GL.End ();
		}
	}
}