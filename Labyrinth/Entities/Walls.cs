using System;
using OpenTK;
using System.Drawing;

namespace Labyrinth.Entities {
	class Walls : BoundedEntity
	{
		public Walls (Vector3 position = default(Vector3)) : base (position, new Vector3 (WIDTH, 1, HEIGHT))
		{
			GenerateTerrain ();
		}

		public override void Render (FrameEventArgs e)
		{
			for (int y = 0; y < HEIGHT; y++)
				for (int x = 0; x < WIDTH; x++)
					RenderCell (X (x), Y (y), terrain [x, y]);
		}

		static int X (int x)
		{
			return x - WIDTH / 2;
		}

		static int Y (int y)
		{
			return y - HEIGHT / 2;
		}

		void RenderCell (int x, int y, uint cell)
		{
			if ((cell & 1) != 0)
				BoundedEntity.DrawCube (x, Bottom, y, x + 1, Top, y + 0.1f, Color.Green);
			if ((cell & 2) != 0)
				BoundedEntity.DrawCube (x + 0.9f, Bottom, y, x + 1, Top, y + 1, Color.Green);
			if ((cell & 4) != 0)
				BoundedEntity.DrawCube (x, Bottom, y + 0.9f, x + 1, Top, y + 1, Color.Green);
			if ((cell & 8) != 0)
				BoundedEntity.DrawCube (x, Bottom, y, x + 0.1f, Top, y + 1, Color.Green);
		}

		const int WIDTH = 40;
		const int HEIGHT = 40;
		const int SIZE = WIDTH * HEIGHT;
		readonly uint[,] terrain = new uint[WIDTH, HEIGHT];
		readonly Random r = new Random (0);

		uint R16 ()
		{
			return (uint)(r.Next () % 16);
		}

		void GenerateTerrain ()
		{
			for (int y = 0; y < HEIGHT; y++)
				for (int x = 0; x < WIDTH; x++)
					for (int h = 0; h < 8; h++)
						terrain [x, y] |= (R16 () & R16 () & R16 () & R16 ());
		}
	}
}
