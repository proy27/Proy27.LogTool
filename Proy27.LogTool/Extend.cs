using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

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

	public static class NonBlockingConsole
	{
		private static BlockingCollection<qq> Queue = new BlockingCollection<qq>();
		static NonBlockingConsole()
		{
			var thread = new Thread(
				() =>
				{
					while (true)
					{
						var t = Queue.Take();
						if (t.Type == ConsoleType.WriteLine)
						{
							Console.WriteLine(t.Line);
						}
						else if (t.Type == ConsoleType.RepeatLine)
						{
							//Console.CursorLeft = 0;
							//Console.CursorLeft = 0;
							Console.Write(t.Line);
							Console.Write(new String(' ', 10));
							Console.CursorLeft = 0;
						}
						else if (t.Type == ConsoleType.NextLine)
						{
							Console.CursorTop += 1;
						}
					}
				});
			thread.IsBackground = true;
			thread.Start();
		}
		public static void WriteLine(string value)
		{
			Queue.Add(new qq(value));
		}
		public static void RepeatLine(string value)
		{
			Queue.Add(qq.RepeatLine(value));
		}
		public static void NextLine()
		{
			Queue.Add(new qq("",ConsoleType.NextLine));
		}

		/*OLD
		private static BlockingCollection<string> m_Queue = new BlockingCollection<string>();
		static NonBlockingConsole()
		{
			var thread = new Thread(
				() =>
				{
					while (true) Console.WriteLine(m_Queue.Take());
				});
			thread.IsBackground = true;
			thread.Start();
		}
		public static void WriteLine(string value)
		{
			m_Queue.Add(value);
		}
		//*/
	}

	public class qq
	{
		public ConsoleType Type { get; set; }
		public string Line { get; set; }

		public qq()
		{
		}
		public qq(string e)
		{
			Line = e;
			Type = ConsoleType.WriteLine;
		}
		public qq(string e, ConsoleType c)
		{
			Line = e;
			Type = c;
		}
		public static qq RepeatLine(string e)
		{
			return new qq(e, ConsoleType.RepeatLine);
		}
	}

	public enum ConsoleType
	{
		WriteLine, RepeatLine,NextLine
	}
}
