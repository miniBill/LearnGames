using System;
using Labyrinth.GUI;
using OpenTK;

namespace Labyrinth {
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			var device = DisplayDevice.Default;
			using (var game = new Game (device.Width, device.Height)) {
				game.Run ();
			}
		}
	}
}

