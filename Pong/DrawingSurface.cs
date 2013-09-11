using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Pong
{
	public partial class DrawingSurface : Control
	{
		readonly object blit_lock = new object();
		Image frontBuffer;

		public DrawingSurface()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			var graphics = pe.Graphics;
			graphics.FillRectangle(Brushes.Black, pe.ClipRectangle);
			Image todraw;
			lock (blit_lock)
				todraw = frontBuffer;
			if (todraw != null)
				if (todraw.Size == pe.ClipRectangle.Size)
					graphics.DrawImageUnscaled(todraw, pe.ClipRectangle.Location);
				else
					graphics.DrawImage(todraw, pe.ClipRectangle);
			Thread.Sleep(0);
			BeginInvoke((Action)Refresh);
		}
	}
}
