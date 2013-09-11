using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pong
{
	public partial class MainForm : Form
	{
		readonly GameController controller;

		public MainForm()
		{
			InitializeComponent();
			controller = new GameController(drawingSurface1);
			controller.Start();
		}
	}
}
