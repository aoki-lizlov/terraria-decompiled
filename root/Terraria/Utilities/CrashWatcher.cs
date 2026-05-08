using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Terraria.Utilities
{
	// Token: 0x020000CE RID: 206
	public static class CrashWatcher
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x004E1211 File Offset: 0x004DF411
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x004E1218 File Offset: 0x004DF418
		public static bool LogAllExceptions
		{
			[CompilerGenerated]
			get
			{
				return CrashWatcher.<LogAllExceptions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				CrashWatcher.<LogAllExceptions>k__BackingField = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x004E1220 File Offset: 0x004DF420
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x004E1227 File Offset: 0x004DF427
		public static bool DumpOnException
		{
			[CompilerGenerated]
			get
			{
				return CrashWatcher.<DumpOnException>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				CrashWatcher.<DumpOnException>k__BackingField = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x004E122F File Offset: 0x004DF42F
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x004E1236 File Offset: 0x004DF436
		public static bool DumpOnCrash
		{
			[CompilerGenerated]
			get
			{
				return CrashWatcher.<DumpOnCrash>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				CrashWatcher.<DumpOnCrash>k__BackingField = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x004E123E File Offset: 0x004DF43E
		// (set) Token: 0x06001817 RID: 6167 RVA: 0x004E1245 File Offset: 0x004DF445
		public static CrashDump.Options CrashDumpOptions
		{
			[CompilerGenerated]
			get
			{
				return CrashWatcher.<CrashDumpOptions>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				CrashWatcher.<CrashDumpOptions>k__BackingField = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x004E124D File Offset: 0x004DF44D
		private static string DumpPath
		{
			get
			{
				return Path.Combine(Main.SavePath, "Dumps");
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x004E1260 File Offset: 0x004DF460
		public static void Inititialize()
		{
			Console.WriteLine("Error Logging Enabled.");
			AppDomain.CurrentDomain.FirstChanceException += delegate(object sender, FirstChanceExceptionEventArgs exceptionArgs)
			{
				if (Main.IsFullScreenThatWouldBeStuckOnCrashMessage())
				{
					return;
				}
				if (CrashWatcher.LogAllExceptions && !false)
				{
					string text = CrashWatcher.PrintException(exceptionArgs.Exception);
					Console.Write("================\r\n" + string.Format("{0}: First-Chance Exception\r\nThread: {1} [{2}]\r\nCulture: {3}\r\nException: {4}\r\n", new object[]
					{
						DateTime.Now,
						Thread.CurrentThread.ManagedThreadId,
						Thread.CurrentThread.Name,
						Thread.CurrentThread.CurrentCulture.Name,
						text
					}) + "================\r\n\r\n");
				}
			};
			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs exceptionArgs)
			{
				if (Main.IsFullScreenThatWouldBeStuckOnCrashMessage())
				{
					return;
				}
				string text2 = CrashWatcher.PrintException((Exception)exceptionArgs.ExceptionObject);
				Console.Write("================\r\n" + string.Format("{0}: Unhandled Exception\r\nThread: {1} [{2}]\r\nCulture: {3}\r\nException: {4}\r\n", new object[]
				{
					DateTime.Now,
					Thread.CurrentThread.ManagedThreadId,
					Thread.CurrentThread.Name,
					Thread.CurrentThread.CurrentCulture.Name,
					text2
				}) + "================\r\n");
				if (CrashWatcher.DumpOnCrash)
				{
					CrashDump.WriteException(CrashWatcher.CrashDumpOptions, CrashWatcher.DumpPath);
				}
			};
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x004E12CC File Offset: 0x004DF4CC
		private static string PrintException(Exception ex)
		{
			string text = ex.ToString();
			try
			{
				int num = (int)typeof(Exception).GetProperty("HResult", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetGetMethod(true).Invoke(ex, null);
				if (num != 0)
				{
					text = text + "\nHResult: " + num;
				}
			}
			catch
			{
			}
			if (ex is ReflectionTypeLoadException)
			{
				foreach (Exception ex2 in ((ReflectionTypeLoadException)ex).LoaderExceptions)
				{
					text = text + "\n+--> " + CrashWatcher.PrintException(ex2);
				}
			}
			return text;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x004E1370 File Offset: 0x004DF570
		public static void EnableCrashDumps(CrashDump.Options options)
		{
			CrashWatcher.DumpOnCrash = true;
			CrashWatcher.CrashDumpOptions = options;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x004E137E File Offset: 0x004DF57E
		public static void DisableCrashDumps()
		{
			CrashWatcher.DumpOnCrash = false;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00009E46 File Offset: 0x00008046
		[Conditional("DEBUG")]
		private static void HookDebugExceptionDialog()
		{
		}

		// Token: 0x040012C0 RID: 4800
		[CompilerGenerated]
		private static bool <LogAllExceptions>k__BackingField;

		// Token: 0x040012C1 RID: 4801
		[CompilerGenerated]
		private static bool <DumpOnException>k__BackingField;

		// Token: 0x040012C2 RID: 4802
		[CompilerGenerated]
		private static bool <DumpOnCrash>k__BackingField;

		// Token: 0x040012C3 RID: 4803
		[CompilerGenerated]
		private static CrashDump.Options <CrashDumpOptions>k__BackingField;

		// Token: 0x020006F0 RID: 1776
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003FA4 RID: 16292 RVA: 0x0069B0F9 File Offset: 0x006992F9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003FA5 RID: 16293 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003FA6 RID: 16294 RVA: 0x0069B108 File Offset: 0x00699308
			internal void <Inititialize>b__18_0(object sender, FirstChanceExceptionEventArgs exceptionArgs)
			{
				if (Main.IsFullScreenThatWouldBeStuckOnCrashMessage())
				{
					return;
				}
				if (CrashWatcher.LogAllExceptions && !false)
				{
					string text = CrashWatcher.PrintException(exceptionArgs.Exception);
					Console.Write("================\r\n" + string.Format("{0}: First-Chance Exception\r\nThread: {1} [{2}]\r\nCulture: {3}\r\nException: {4}\r\n", new object[]
					{
						DateTime.Now,
						Thread.CurrentThread.ManagedThreadId,
						Thread.CurrentThread.Name,
						Thread.CurrentThread.CurrentCulture.Name,
						text
					}) + "================\r\n\r\n");
				}
			}

			// Token: 0x06003FA7 RID: 16295 RVA: 0x0069B1A0 File Offset: 0x006993A0
			internal void <Inititialize>b__18_1(object sender, UnhandledExceptionEventArgs exceptionArgs)
			{
				if (Main.IsFullScreenThatWouldBeStuckOnCrashMessage())
				{
					return;
				}
				string text = CrashWatcher.PrintException((Exception)exceptionArgs.ExceptionObject);
				Console.Write("================\r\n" + string.Format("{0}: Unhandled Exception\r\nThread: {1} [{2}]\r\nCulture: {3}\r\nException: {4}\r\n", new object[]
				{
					DateTime.Now,
					Thread.CurrentThread.ManagedThreadId,
					Thread.CurrentThread.Name,
					Thread.CurrentThread.CurrentCulture.Name,
					text
				}) + "================\r\n");
				if (CrashWatcher.DumpOnCrash)
				{
					CrashDump.WriteException(CrashWatcher.CrashDumpOptions, CrashWatcher.DumpPath);
				}
			}

			// Token: 0x04006808 RID: 26632
			public static readonly CrashWatcher.<>c <>9 = new CrashWatcher.<>c();

			// Token: 0x04006809 RID: 26633
			public static EventHandler<FirstChanceExceptionEventArgs> <>9__18_0;

			// Token: 0x0400680A RID: 26634
			public static UnhandledExceptionEventHandler <>9__18_1;
		}
	}
}
