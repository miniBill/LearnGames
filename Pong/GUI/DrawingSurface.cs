using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Pong
{
	public partial class DrawingSurface : Control
	{
		Bitmap buffer;
		readonly object bufferLock = new object();
		Graphics imageGraphics;
		Graphics controlGraphics;
		readonly Timer timer;

		public new event EventHandler<PaintEventArgs> Update;

		public DrawingSurface()
		{
			InitializeComponent();
			HandleCreated += DrawingSurface_HandleCreated;
			timer = new Timer(1000.0 / 50.0);
			timer.Elapsed += delegate
			{
				if (controlGraphics == null) return;
				CleanBuffer(Size);
				if (Update != null)
					lock (bufferLock)
						Update(this, new PaintEventArgs(imageGraphics, ClientRectangle));
				try
				{
					Invoke((Action)Refresh);
				}
				catch
				{
					timer.Stop();
				}
			};
			timer.Start();
		}

		void DrawingSurface_HandleCreated(object sender, EventArgs e)
		{
			if (controlGraphics == null)
				controlGraphics = CreateGraphics();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			lock (bufferLock)
				if (buffer != null)
					pe.Graphics.DrawImageUnscaled(buffer, pe.ClipRectangle.Location);
		}

		private void CleanBuffer(Size size)
		{
			lock (bufferLock)
			{
				if (buffer == null || buffer.Size != size)
				{
					if (buffer != null)
						buffer.Dispose();
					buffer = new Bitmap(size.Width, size.Height);
					if (imageGraphics != null)
						imageGraphics.Dispose();
					imageGraphics = Graphics.FromImage(buffer);
				}
				imageGraphics.FillRectangle(Brushes.Black, 0, 0, size.Width, size.Height);
			}
		}

		public Graphics GetGraphics()
		{
			return imageGraphics;
		}

		public void Stop()
		{
			timer.Dispose();
		}
	}
}
