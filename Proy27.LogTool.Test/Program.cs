using System;
using System.Diagnostics;
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

			var a = new MyClass {aaa = "aaa",bbb = "bbb"};
			Log.Info(a);// Convert to Json


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
		public class MyClass
		{
			public string aaa { get; set; }
			public string bbb { get; set; }
		}
	}
}
