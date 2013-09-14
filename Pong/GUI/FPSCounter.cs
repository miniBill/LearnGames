namespace Pong.GUI
{
	class FPSCounter
	{
		int frames;
		int savedFrames;
		double elapsed;

		public int Update(double time)
		{
			elapsed += time;
			if (time >= 1)
			{
				savedFrames = frames;
				frames = 1;
				elapsed = 0;
			}
			frames++;
			return savedFrames;
		}
	}
}
