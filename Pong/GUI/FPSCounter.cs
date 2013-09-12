namespace Pong
{
	struct FPSCounter
	{
		private int frames;
		private int savedFrames;
		private long savedSecond;

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
