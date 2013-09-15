using System;
using OpenTK;
using System.Drawing;

namespace Labyrinth.Entities {
	class Walls : BoundedEntity
	{
		public Walls (Vector3 position = default(Vector3)) : base (position, new Vector3 (WIDTH, 2, HEIGHT))
		{
			GenerateTerrain ();
		}

		public override void Render (FrameEventArgs e)
		{
			for (int y = 0; y <= HEIGHT; y++)
				for (int x = 0; x <= WIDTH; x++) {
					int dx = x - WIDTH / 2;
					int dy = y - HEIGHT / 2;
					if (UpWall (x, y))
						BoundedEntity.DrawCube (dx - 0.1f, Bottom, dy - 0.1f, dx + 1.1f, Top, dy + 0.1f, Color.Lime);
					if (LeftWall (x, y))
						BoundedEntity.DrawCube (dx + 0.9f, Bottom, dy - 0.1f, dx + 1.1f, Top, dy + 1.1f, Color.Lime);
				}
		}

		bool UpWall (int x, int y)
		{
			return And (x, y, 1) || And (x, y - 1, 4);
		}

		bool LeftWall (int x, int y)
		{
			return And (x, y, 8) || And (x - 1, y, 2);
		}

		bool And (int x, int y, int i)
		{
			return x >= 0 && y >= 0 && x < WIDTH && y < HEIGHT && (terrain [x, y] & i) != 0;
		}

		const int WIDTH = 20;
		const int HEIGHT = 20;
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
