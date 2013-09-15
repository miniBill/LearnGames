using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;

namespace Labyrinth.Entities {
	class Player : BoundedEntity
	{
		public Player (Vector3 position = default(Vector3)) : base (position, new Vector3 (0.25f, 0.5f, 0.25f))
		{
		}

		public override void Update (FrameEventArgs e, KeyboardDevice keyboard)
		{
			base.Update (e, keyboard);
			if (keyboard [Key.Up] || keyboard [Key.W]) {
				Speed.Z = -1;
			} else if (keyboard [Key.Down] || keyboard [Key.S]) {
				Speed.Z = 1;
			} else {
				Speed.Z = 0;
			}
			if (keyboard [Key.Right] || keyboard [Key.D]) {
				Speed.X = 1;
			} else if (keyboard [Key.Left] || keyboard [Key.A]) {
				Speed.X = -1;
			} else {
				Speed.X = 0;
			}
			Speed.Y = 0;
			var length = Speed.Length;
			if (Math.Abs (length) > 0.0001) {
				float scale = 2f / length;
				Speed *= scale;
			}
		}

		public override void Render (FrameEventArgs e)
		{
			DrawCube (Left, Bottom, Back, Right, Top, Front, Color.Pink);
		}
	}
}

