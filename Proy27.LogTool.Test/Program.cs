using System;
using System.Diagnostics;
using System.Dynamic;
using System.Threading;

namespace Proy27.LogTool.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Log.Info("I am info");
			Log.Debug("I am Debug");
			Log.Error("I am Error");
			dynamic a = new ExpandoObject();
			a.aaa = "aaa";
			a.bbb = "bbb";
			Log.Info(a);// Convert to Json

			var sw = Stopwatch.StartNew(); 
			for (int j = 0; j < 100; j++)
			{
				if (j % 2 == 0)
				{
					//Log.Info("i%5==0");
				}
				Thread.Sleep(1000);
				Log.Info("i%5==0");
				//Log.NextLine();
				Log.RepeatLine(sw.Avg(j),0);
				Log.RepeatLine(sw.Avg(j*2),1);
			}
			Log.NextLine();
			Log.NextLine();
		}
	}
}
