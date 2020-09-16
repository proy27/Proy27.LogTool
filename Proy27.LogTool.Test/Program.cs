using System;
using System.Diagnostics;
using System.Threading;

namespace Proy27.LogTool.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");
			var sw = Stopwatch.StartNew();
			int i = 0;
			while (true)
			{
				i++;
				Thread.Sleep(1000);
				Log.Info(sw.Avg(i*3));
			}
		}
	}
}
