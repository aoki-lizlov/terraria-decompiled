using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Terraria
{
	// Token: 0x0200001B RID: 27
	internal static class LinuxLaunch
	{
		// Token: 0x060000DA RID: 218 RVA: 0x0000C78C File Offset: 0x0000A98C
		private static void Main(string[] args)
		{
			AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs sargs)
			{
				string resourceName = new AssemblyName(sargs.Name).Name + ".dll";
				string text = Array.Find<string>(typeof(Program).Assembly.GetManifestResourceNames(), (string element) => element.EndsWith(resourceName));
				if (text == null)
				{
					return null;
				}
				Assembly assembly;
				using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text))
				{
					byte[] array = new byte[manifestResourceStream.Length];
					manifestResourceStream.Read(array, 0, array.Length);
					assembly = Assembly.Load(array);
				}
				return assembly;
			};
			Environment.SetEnvironmentVariable("FNA_WORKAROUND_WINDOW_RESIZABLE", "1");
			Program.LaunchGame(args, true);
		}

		// Token: 0x020005F0 RID: 1520
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x06003B5B RID: 15195 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x06003B5C RID: 15196 RVA: 0x0065ACFD File Offset: 0x00658EFD
			internal bool <Main>b__1(string element)
			{
				return element.EndsWith(this.resourceName);
			}

			// Token: 0x04006373 RID: 25459
			public string resourceName;
		}

		// Token: 0x020005F1 RID: 1521
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003B5D RID: 15197 RVA: 0x0065AD0B File Offset: 0x00658F0B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003B5E RID: 15198 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003B5F RID: 15199 RVA: 0x0065AD18 File Offset: 0x00658F18
			internal Assembly <Main>b__0_0(object sender, ResolveEventArgs sargs)
			{
				LinuxLaunch.<>c__DisplayClass0_0 CS$<>8__locals1 = new LinuxLaunch.<>c__DisplayClass0_0();
				CS$<>8__locals1.resourceName = new AssemblyName(sargs.Name).Name + ".dll";
				string text = Array.Find<string>(typeof(Program).Assembly.GetManifestResourceNames(), (string element) => element.EndsWith(CS$<>8__locals1.resourceName));
				if (text == null)
				{
					return null;
				}
				Assembly assembly;
				using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text))
				{
					byte[] array = new byte[manifestResourceStream.Length];
					manifestResourceStream.Read(array, 0, array.Length);
					assembly = Assembly.Load(array);
				}
				return assembly;
			}

			// Token: 0x04006374 RID: 25460
			public static readonly LinuxLaunch.<>c <>9 = new LinuxLaunch.<>c();

			// Token: 0x04006375 RID: 25461
			public static ResolveEventHandler <>9__0_0;
		}
	}
}
