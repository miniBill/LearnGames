﻿using System.Drawing;

namespace Pong.Game.Sprites
{
	class Player : MovingSprite
	{
		private readonly World world;
		public bool AI { get; private set; }

		public Player(bool ai, World world)
			: base(1 / 100.0, 1 / 10.0)
		{
			this.world = world;
			X = ai ? 1 - Width / 2 : Width / 2;
			AI = ai;
		}

		public const double TopSpeed = 3;

		public override void Update(double delta)
		{
			base.Update(delta);
			if (AI)
			{
				double projected = world.Ball.Y.Project(Top + Width / 3, Bottom - Width / 3, -TopSpeed, TopSpeed);
				SpeedY = projected.Clamp(-TopSpeed, TopSpeed);
			}
		}

		public override void DrawTo(Graphics graphics, Size rectangle)
		{
			graphics.FillRectangle(Brushes.White, GetRectangle(rectangle));
		}
	}
}