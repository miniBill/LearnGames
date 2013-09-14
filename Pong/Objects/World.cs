using System.Drawing;
using Pong.Objects.Sprites;

namespace Pong.Objects
{
	class World
	{
		public Player Player { get; private set; }
		public Player AI { get; private set; }
		public Ball Ball { get; private set; }

		public double CollisionTolerance
		{
			get { return 0.0001; }
		}

		public World()
		{
			Ball = new Ball(this);
			Player = new Player(false, this);
			AI = new Player(true, this);
			Player.Y = AI.Y = Ball.X = Ball.Y = 0.5;
		}

		public void Update(double delta)
		{
			Player.Update(delta);
			AI.Update(delta);
			Ball.Update(delta);
		}

		public void DrawTo(Graphics graphics, Size size)
		{
			Player.DrawTo(graphics, size);
			AI.DrawTo(graphics, size);
			Ball.DrawTo(graphics, size);
		}
	}
}