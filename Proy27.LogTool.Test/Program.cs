using System;
using System.Diagnostics;
using System.Threading;

namespace Proy27.LogTool.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			var sw = Stopwatch.StartNew();
			Thread.Sleep(888);
			Console.WriteLine(sw.Avg(3));
		}
	}
}
