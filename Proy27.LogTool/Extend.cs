using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Proy27.LogTool
{
	public static class Extend
	{
		public static string To4(this double a)
		{
			return a.ToString("0.0000");
		}
		public static string To1(this double a)
		{
			return a.ToString("0.0");
		}
		public static string To4(this Stopwatch a)
		{
			return a.Elapsed.TotalSeconds.To4();
		}
		public static double TS(this Stopwatch a)
		{
			return a.Elapsed.TotalSeconds;
		}
		public static string Avg(this Stopwatch a, long count)
		{
			return (count / a.Elapsed.TotalSeconds).To4();
		}
	}
}
