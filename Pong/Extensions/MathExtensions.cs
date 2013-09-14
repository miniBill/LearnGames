using System;

namespace Pong.Extensions
{
	static class MathExtensions
	{
		public static double Clamp(this double value, double min, double max)
		{
			return Math.Max(min, Math.Min(value, max));
		}

		public static double Project(this double input, double fromlow, double fromhigh, double tolow, double tohigh){
			return (input - fromlow) * (tohigh - tolow) / (fromhigh - fromlow) + tolow;
		}
	}
}
