using OpenTK;
using Labyrinth.Entities;
using OpenTK.Input;
using System.Collections.Generic;

namespace Labyrinth.Logic {
	class World
	{
		readonly List<Entity> entities = new List<Entity> ();

		public World ()
		{
			entities.Add (new Walls ());
			entities.Add (new Player ());
			entities.Add (new Axes ());
		}

		public void Update (FrameEventArgs e, KeyboardDevice keyboard)
		{
			foreach (var entity in entities)
				entity.Update (e, keyboard);
		}

		public void Render (FrameEventArgs e)
		{
			foreach (var entity in entities)
				entity.Render (e);
		}
	}
}
