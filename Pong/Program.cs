using System;
using System.Windows.Forms;

namespace Pong {
	public static class Program
	{
		[STAThread]
		public static void Main ()
		{
			Application.EnableVisualStyles ();
			Application.Run (new MainForm ());
		}
	}
}
