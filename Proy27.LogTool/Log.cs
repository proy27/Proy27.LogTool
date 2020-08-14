using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Proy27.LogTool
{
	public static class Log
	{
		public static bool IsDebug { get; set; }
		public static bool IsConsole { get; set; }
		private static Object _lock = new object();
		private static void _Log(string e, string type)
		{
			var now = DateTime.Now;
			var ss = now.ToString("s") + $"  {type}  " + e;
			if (IsConsole)
				Console.WriteLine(ss);
			var path = LogPath + now.ToString("yyyyMMdd") + ".log";
			Task.Run(() => LogWriteFileByAppend(path, ss));
		}
		private static void _Log(object e, string type)
		{
			_Log(JsonConvert.SerializeObject(e), type);
		}

		public static void Info(object e)
		{
			_Log(e, "[INF]");
		}
		public static void Error(object e)
		{
			_Log(e, "[ERR]");
		}
		public static void Debug(object e)
		{
			if (IsDebug)
				_Log(e, "[DBG]");
		}

		static void LogWriteFileByAppend(string path, string e)
		{
			lock (_lock)
			{
				File.AppendAllText(path, e + "\r\n");
			}
		}


		static Log()
		{
			IsDebug = true;
			IsConsole = true;
			Directory.CreateDirectory(LogPath);
		}
		public static void SetLogPath(string path)
		{
			LogPath = path;
		}
		private static string LogPath = AppDomain.CurrentDomain.BaseDirectory + @"\Log\";
	}
}
