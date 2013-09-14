using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using OpenTK.Graphics.OpenGL;

namespace Labyrinth.GUI {
	class Letter
	{
		public static float Width { get { return PIXEL_WIDTH * width; } }

		public static float Height { get { return PIXEL_HEIGHT * width; } }

		public const float SCALE = 0.05f;
		public static readonly float Spacing = SCALE;
		public const float PIXEL_WIDTH = SCALE;
		public const float PIXEL_HEIGHT = SCALE;
		static int width;
		static int height;
		static readonly Dictionary<char,Letter> letters = new Dictionary<char,Letter> ();
		static readonly char[] charmap = {
			'A',			'B',			'C',			'D',			'E',			'F',
			'G',			'H',			'I',			'J',			'K',			'L',
			'M',			'N',			'O',			'P',			'Q',			'R',
			'S',			'T',			'U',			'V',			'W',			'X',
			'Y',			'Z',			'0',			'1',			'2',			'3',
			'4',			'5',			'6',			'7',			'8',			'9',
			'!',			' '
		};

		public static float GetWidth (string value, float percentage = 1.0f)
		{
			return (value.Length * (Width + Spacing) - Spacing) * percentage;
		}

		public static void RenderString (string value, float x = 0.0f, float y = 0.0f, HAlign halign = HAlign.Center, VAlign valign = VAlign.Center, float percentage = 1.0f)
		{
			float fullWidth = GetWidth (value, percentage);
			switch (halign) {
				case HAlign.Center:
					x -= fullWidth / 2.0f;
					break;
				case HAlign.Right:
					x -= fullWidth;
					break;
			}
			var fullHeight = Height * percentage;
			switch (valign) {
				case VAlign.Center:
					y += fullHeight / 2.0f;
					break;
				case VAlign.Bottom:
					y += fullHeight;
					break;
			}
			for (int i = 0; i < value.Length; i++) {
				Letter.GetLetter (value [i]).Render (x, y, percentage);
				x += (Width + Spacing) * percentage;
			}
		}

		public static void LoadFont ()
		{
			var assembly = Assembly.GetExecutingAssembly ();
			var resourceName = "Labyrinth.Font.png";

			using (Stream stream = assembly.GetManifestResourceStream (resourceName))
			using (var font = (Bitmap)Image.FromStream (stream)) {
				var raw = new Color[font.Width, font.Height];
				for (int y = 0; y < font.Height; y++)
					for (int x = 0; x < font.Width; x++)
						raw [x, y] = font.GetPixel (x, y);
				for (int x = 0; x < font.Width; x++) {
					if (font.GetPixel (x, 0).ToArgb () == Color.Black.ToArgb ()) {
						width = x;
						break;
					}
				}
				if (width == 0)
					throw new ArgumentOutOfRangeException ();
				for (int y = 0; y < font.Height; y++)
					if (font.GetPixel (0, y).ToArgb () == Color.Black.ToArgb ()) {
						height = y;
						break;
					}
				if (height == 0)
					throw new ArgumentOutOfRangeException ();

				int index = 0;
				for (int y = 0; y < font.Height; y += height + 1) {
					if (index >= charmap.Length)
						break;
					for (int x = 0; x < font.Width; x += width + 1) {
						if (index >= charmap.Length)
							break;
						letters.Add (charmap [index++], CreateLetter (font, x, y));
					}
				}
			}
		}

		static Letter CreateLetter (Bitmap font, int x, int y)
		{
			var points = new bool[width * height];
			int i = 0;
			for (int yy = 0; yy < height; yy++)
				for (int xx = 0; xx < width; xx++) {
					var pixel = font.GetPixel (x + xx, y + yy);
					var argb = pixel.ToArgb ();
					points [i++] = argb == Color.Blue.ToArgb ();
					if (argb != Color.White.ToArgb () && argb != Color.Blue.ToArgb ())
						throw new ArgumentException ();
				}
			return new Letter (points);
		}

		public static Letter GetLetter (char c)
		{
			if (c >= 'a' && c <= 'z')
				c = (char)(c + 'A' - 'a');
			return letters [c];
		}

		readonly bool[] points;

		public Letter (bool[] points)
		{
			this.points = points;
		}

		public void Render (float x, float y, float percentage = 1.0f)
		{
			for (int yi = 0; yi < height; yi++)
				for (int xi = 0; xi < width; xi++)
					if (points [xi + yi * width])
						DrawRectangle (x + percentage * xi * PIXEL_WIDTH, y - percentage * yi * PIXEL_HEIGHT, percentage);
		}

		static void DrawRectangle (float x, float y, float percentage = 1.0f)
		{
			GL.Begin (BeginMode.TriangleFan);
			GL.Color4 (1.0f, 1.0f, 1.0f, 0.0f);
			GL.Vertex2 (x, y - percentage * PIXEL_HEIGHT);
			GL.Vertex2 (x + percentage * PIXEL_WIDTH, y - percentage * PIXEL_HEIGHT);
			GL.Vertex2 (x + percentage * PIXEL_WIDTH, y);
			GL.Vertex2 (x, y);
			GL.End ();
		}
	}
}
