using System.Drawing;
using System.Windows.Forms;
using Pong.Game;

namespace Pong.GUI
{
	public partial class MainForm : Form
	{
		readonly GameController controller;

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
				return cp;
			}
		}


		public MainForm()
		{
			InitializeComponent();
			Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
			Width = workingArea.Height;
			Height = Width * 2 / 3;
			Top = workingArea.Height / 6;
			Left = (workingArea.Width - Width) / 2;
			controller = new GameController(drawingSurface1);
			controller.Start();
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			controller.KeyDown(e.KeyCode);
		}

		private void MainForm_KeyUp(object sender, KeyEventArgs e)
		{
			controller.KeyUp(e.KeyCode);
		}

		private void drawingSurface1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			e.IsInputKey = true;
		}
	}
}
