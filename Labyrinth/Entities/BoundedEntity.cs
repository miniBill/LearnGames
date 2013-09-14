using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Labyrinth.Entities {
	class BoundedEntity : Entity
	{
		public readonly Vector3 Size;

		public float Left{ get { return Position.X - Size.X / 2; } }

		public float Right{ get { return Position.X + Size.X / 2; } }

		public float Bottom{ get { return Position.Y - Size.Y / 2; } }

		public float Top{ get { return Position.Y + Size.Y / 2; } }

		public float Back{ get { return Position.Z - Size.Z / 2; } }

		public float Front{ get { return Position.Z + Size.Z / 2; } }

		public BoundedEntity (Vector3 position, Vector3 size) : base (position)
		{
			Size = size;
		}

		public override void Render (FrameEventArgs e)
		{
			DrawCube (Left, Bottom, Back, Right, Top, Front, Color.White);
		}

		public static void DrawCube (float x1, float y1, float z1, float x2, float y2, float z2, Color color)
		{
			var p1 = new Vector3 (x1, y1, z2);
			var p2 = new Vector3 (x2, y1, z2);
			var p3 = new Vector3 (x2, y2, z2);
			var p4 = new Vector3 (x1, y2, z2);
			var p5 = new Vector3 (x1, y1, z1);
			var p6 = new Vector3 (x2, y1, z1);
			var p7 = new Vector3 (x2, y2, z1);
			var p8 = new Vector3 (x1, y2, z1);

			DrawSquare (p1, p2, p3, p4, color);
			DrawSquare (p2, p6, p7, p3, color);
			DrawSquare (p6, p5, p8, p7, color);
			DrawSquare (p5, p1, p4, p8, color);
			DrawSquare (p4, p3, p7, p8, color);
			DrawSquare (p2, p1, p5, p6, color);
		}

		static void DrawSquare (Vector3 pa, Vector3 pb, Vector3 pc, Vector3 pd, Color color)
		{
			GL.Begin (BeginMode.TriangleFan);
			GL.Color4 (color);
			GL.Vertex3 (pa);
			GL.Vertex3 (pb);
			GL.Vertex3 (pc);
			GL.Vertex3 (pd);
			GL.End ();
		}
	}
}

