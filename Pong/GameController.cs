using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pong
{
	class GameController
	{
		readonly Thread gameThread = new Thread(Run);
		Player player1;
		Player player2;
		DrawingSurface drawingSurface;

		public GameController(DrawingSurface drawingSurface)
		{
			this.drawingSurface = drawingSurface;
		}

		public void Start()
		{
			gameThread.Start();
		}

		void Run()
		{
			Stopwatch timer;
			for (; ; )
			{
				Thread.Sleep(0);
			}
		}
	}
}
