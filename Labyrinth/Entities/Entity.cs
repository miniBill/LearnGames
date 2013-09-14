using System;
using OpenTK;
using OpenTK.Input;

namespace Labyrinth.Entities {
	abstract class Entity
	{
		public Vector3 Position;
		public Vector3 Speed;

		protected Entity (Vector3 position)
		{
			Position = position;
		}

		public virtual void Update (FrameEventArgs e, KeyboardDevice keyboard)
		{
			Position += (float)e.Time * Speed;
		}

		public virtual void Render (FrameEventArgs e)
		{
		}
	}
}

