using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.IO;
using ReLogic.OS;
using SDL3;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x0200004E RID: 78
	public static class Program
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x003561A7 File Offset: 0x003543A7
		public static float LoadedPercentage
		{
			get
			{
				if (Program.ThingsToLoad == 0)
				{
					return 1f;
				}
				return (float)Program.ThingsLoaded / (float)Program.ThingsToLoad;
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x003561C3 File Offset: 0x003543C3
		public static void StartForceLoad()
		{
			if (!Main.SkipAssemblyLoad)
			{
				new Thread(new ParameterizedThreadStart(Program.ForceLoadThread))
				{
					IsBackground = true
				}.Start();
				return;
			}
			Program.LoadedEverything = true;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x003561F0 File Offset: 0x003543F0
		public static void ForceLoadThread(object threadContext)
		{
			Program.ForceLoadAssembly(Assembly.GetExecutingAssembly(), true);
			Program.LoadedEverything = true;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00356204 File Offset: 0x00354404
		private static void ForceJITOnAssembly(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				foreach (MethodInfo methodInfo in Program.IsMono ? type.GetMethods() : type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (!methodInfo.IsAbstract && !methodInfo.ContainsGenericParameters && methodInfo.GetMethodBody() != null)
					{
						if (Program.IsMono)
						{
							Program.JitForcedMethodCache = methodInfo.MethodHandle.GetFunctionPointer();
						}
						else
						{
							RuntimeHelpers.PrepareMethod(methodInfo.MethodHandle);
						}
					}
				}
				Program.ThingsLoaded++;
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x003562B4 File Offset: 0x003544B4
		private static void ForceStaticInitializers(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				if (!type.IsGenericType)
				{
					RuntimeHelpers.RunClassConstructor(type.TypeHandle);
				}
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x003562ED File Offset: 0x003544ED
		private static void ForceLoadAssembly(Assembly assembly, bool initializeStaticMembers)
		{
			Program.ThingsToLoad = assembly.GetTypes().Length;
			Program.ForceJITOnAssembly(assembly);
			if (initializeStaticMembers)
			{
				Program.ForceStaticInitializers(assembly);
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0035630C File Offset: 0x0035450C
		private static void ForceLoadAssembly(string name, bool initializeStaticMembers)
		{
			Assembly assembly = null;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				if (assemblies[i].GetName().Name.Equals(name))
				{
					assembly = assemblies[i];
					break;
				}
			}
			if (assembly == null)
			{
				assembly = Assembly.Load(name);
			}
			Program.ForceLoadAssembly(assembly, initializeStaticMembers);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00356368 File Offset: 0x00354568
		private static void SetupLogging()
		{
			if (Program.LaunchParameters.ContainsKey("-logfile"))
			{
				string text = Program.LaunchParameters["-logfile"];
				if (text == null || text.Trim() == "")
				{
					text = Path.Combine(Program.SavePath, "Logs", string.Format("Log_{0:yyyyMMddHHmmssfff}.log", DateTime.Now));
				}
				else
				{
					text = Path.Combine(text, string.Format("Log_{0:yyyyMMddHHmmssfff}.log", DateTime.Now));
				}
				ConsoleOutputMirror.ToFile(text);
			}
			CrashWatcher.Inititialize();
			CrashWatcher.DumpOnException = Program.LaunchParameters.ContainsKey("-minidump");
			CrashWatcher.LogAllExceptions = Program.LaunchParameters.ContainsKey("-logerrors");
			if (Program.LaunchParameters.ContainsKey("-fulldump"))
			{
				CrashDump.Options options = CrashDump.Options.WithFullMemory;
				Console.WriteLine("Full Dump logs enabled.");
				CrashWatcher.EnableCrashDumps(options);
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00356440 File Offset: 0x00354640
		private static void InitializeConsoleOutput()
		{
			if (Debugger.IsAttached)
			{
				return;
			}
			try
			{
				Console.OutputEncoding = Encoding.UTF8;
				if (Platform.IsWindows)
				{
					Console.InputEncoding = Encoding.Unicode;
				}
				else
				{
					Console.InputEncoding = Encoding.UTF8;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00356494 File Offset: 0x00354694
		public static void LaunchGame(string[] args, bool monoArgs = false)
		{
			Thread.CurrentThread.Name = "Main Thread";
			if (monoArgs)
			{
				args = Utils.ConvertMonoArgsToDotNet(args);
			}
			Program.LogFNANativeLibVersions();
			Program.LaunchParameters = Utils.ParseArguements(args);
			Program.SavePath = (Program.LaunchParameters.ContainsKey("-savedirectory") ? Program.LaunchParameters["-savedirectory"] : Platform.Get<IPathService>().GetStoragePath("Terraria"));
			ThreadPool.SetMinThreads(8, 8);
			Program.InitializeConsoleOutput();
			Program.SetupLogging();
			Platform.Get<IWindowService>().SetQuickEditEnabled(false);
			Program.RunGame();
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00356524 File Offset: 0x00354724
		public static void RunGame()
		{
			LanguageManager.Instance.SetLanguage(GameCulture.DefaultCulture);
			if (Platform.IsOSX)
			{
				Main.OnEngineLoad += delegate
				{
					Main.instance.IsMouseVisible = false;
				};
			}
			else if (Platform.IsWindows)
			{
				Main.OnEngineLoad += delegate
				{
					IMouseNotifier mouseNotifier = Platform.Get<IMouseNotifier>();
					if (mouseNotifier != null)
					{
						mouseNotifier.AddMouseHandler(delegate(bool connected)
						{
							if (connected)
							{
								Main.instance.IsMouseVisible = true;
								Main.instance.ReHideCursor = true;
							}
						});
					}
				};
			}
			using (Main main = new Main())
			{
				try
				{
					Lang.InitializeLegacyLocalization();
					SocialAPI.Initialize(null);
					LaunchInitializer.LoadParameters(main);
					Main.OnEnginePreload += Program.StartForceLoad;
					if (Main.dedServ)
					{
						main.DedServ();
					}
					main.Run();
				}
				catch (Exception ex)
				{
					Program.DisplayException(ex);
				}
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0035660C File Offset: 0x0035480C
		private static void LogFNANativeLibVersions()
		{
			Console.WriteLine(string.Concat(new object[]
			{
				"SDL v",
				SDL.SDL_GetVersion(),
				".",
				SDL.SDL_GetRevision()
			}) ?? "");
			uint num = FNA3D.FNA3D_LinkedVersion();
			Console.WriteLine(string.Concat(new object[]
			{
				"FNA3D v",
				num / 10000U,
				".",
				num / 100U % 100U,
				".",
				num % 100U
			}) ?? "");
			uint num2 = FAudio.FAudioLinkedVersion();
			Console.WriteLine(string.Concat(new object[]
			{
				"FAudio v",
				num2 / 10000U,
				".",
				num2 / 100U % 100U,
				".",
				num2 % 100U
			}) ?? "");
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0035671C File Offset: 0x0035491C
		private static void DisplayException(Exception e)
		{
			try
			{
				string text = e.ToString();
				if (WorldGen.isGeneratingOrLoadingWorld)
				{
					try
					{
						text = string.Format("Creating world - Seed: {0} Width: {1}, Height: {2}, Evil: {3}, IsExpert: {4}\n{5}", new object[]
						{
							Main.ActiveWorldFileData.SeedText,
							Main.maxTilesX,
							Main.maxTilesY,
							WorldGen.WorldGenParam_Evil,
							Main.expertMode,
							text
						});
					}
					catch
					{
					}
				}
				using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
				{
					streamWriter.WriteLine(DateTime.Now);
					streamWriter.WriteLine(text);
					streamWriter.WriteLine("");
				}
				if (Main.dedServ)
				{
					Console.WriteLine(Language.GetTextValue("Error.ServerCrash"), DateTime.Now, text);
				}
				MessageBox.Show(text, "Terraria: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch
			{
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0035682C File Offset: 0x00354A2C
		// Note: this type is marked as 'beforefieldinit'.
		static Program()
		{
		}

		// Token: 0x040009D6 RID: 2518
		public static bool IsXna = false;

		// Token: 0x040009D7 RID: 2519
		public static bool IsFna = true;

		// Token: 0x040009D8 RID: 2520
		public static bool IsMono = Type.GetType("Mono.Runtime") != null;

		// Token: 0x040009D9 RID: 2521
		public static Dictionary<string, string> LaunchParameters = new Dictionary<string, string>();

		// Token: 0x040009DA RID: 2522
		public static string SavePath;

		// Token: 0x040009DB RID: 2523
		public const string TerrariaSaveFolderPath = "Terraria";

		// Token: 0x040009DC RID: 2524
		private static int ThingsToLoad;

		// Token: 0x040009DD RID: 2525
		private static int ThingsLoaded;

		// Token: 0x040009DE RID: 2526
		public static bool LoadedEverything;

		// Token: 0x040009DF RID: 2527
		public static IntPtr JitForcedMethodCache;

		// Token: 0x02000629 RID: 1577
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003C5A RID: 15450 RVA: 0x0066DED0 File Offset: 0x0066C0D0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003C5B RID: 15451 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003C5C RID: 15452 RVA: 0x0066DEDC File Offset: 0x0066C0DC
			internal void <RunGame>b__21_0()
			{
				Main.instance.IsMouseVisible = false;
			}

			// Token: 0x06003C5D RID: 15453 RVA: 0x0066DEEC File Offset: 0x0066C0EC
			internal void <RunGame>b__21_1()
			{
				IMouseNotifier mouseNotifier = Platform.Get<IMouseNotifier>();
				if (mouseNotifier != null)
				{
					mouseNotifier.AddMouseHandler(delegate(bool connected)
					{
						if (connected)
						{
							Main.instance.IsMouseVisible = true;
							Main.instance.ReHideCursor = true;
						}
					});
				}
			}

			// Token: 0x06003C5E RID: 15454 RVA: 0x0066DF27 File Offset: 0x0066C127
			internal void <RunGame>b__21_2(bool connected)
			{
				if (connected)
				{
					Main.instance.IsMouseVisible = true;
					Main.instance.ReHideCursor = true;
				}
			}

			// Token: 0x040064E6 RID: 25830
			public static readonly Program.<>c <>9 = new Program.<>c();

			// Token: 0x040064E7 RID: 25831
			public static Action <>9__21_0;

			// Token: 0x040064E8 RID: 25832
			public static Action<bool> <>9__21_2;

			// Token: 0x040064E9 RID: 25833
			public static Action <>9__21_1;
		}
	}
}
