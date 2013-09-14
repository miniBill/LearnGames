namespace Pong.GUI
{
	struct FPSCounter
	{
		int frames;
		int savedFrames;
		long savedSecond;

		public int Update(long ticks)
		{
			if (ticks - savedSecond >= 1000*10000)
			{
				savedFrames = frames;
				frames = 1;
				savedSecond = ticks;
			}
			frames++;
			return savedFrames;
		}
	}
}
