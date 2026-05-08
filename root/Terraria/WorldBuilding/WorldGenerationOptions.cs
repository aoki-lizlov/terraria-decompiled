using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000098 RID: 152
	public class WorldGenerationOptions
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x004DD4B6 File Offset: 0x004DB6B6
		public static IEnumerable<AWorldGenerationOption> Options
		{
			get
			{
				return WorldGenerationOptions._options;
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x004DD4BD File Offset: 0x004DB6BD
		public static T Get<T>() where T : AWorldGenerationOption
		{
			return WorldGenerationOptions.OptionStorage<T>.Instance;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x004DD4C4 File Offset: 0x004DB6C4
		static WorldGenerationOptions()
		{
			WorldGenerationOptions.Register<WorldSeedOption_Normal>();
			WorldGenerationOptions.Register<WorldSeedOption_NotTheBees>();
			WorldGenerationOptions.Register<WorldSeedOption_Drunk>();
			WorldGenerationOptions.Register<WorldSeedOption_Anniversary>();
			WorldGenerationOptions.Register<WorldSeedOption_DontStarve>();
			WorldGenerationOptions.Register<WorldSeedOption_ForTheWorthy>();
			WorldGenerationOptions.Register<WorldSeedOption_NoTraps>();
			WorldGenerationOptions.Register<WorldSeedOption_Remix>();
			WorldGenerationOptions.Register<WorldSeedOption_Everything>();
			WorldGenerationOptions.Register<WorldSeedOption_Skyblock>();
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x004DD504 File Offset: 0x004DB704
		public static void Register<T>() where T : AWorldGenerationOption, new()
		{
			if (WorldGenerationOptions.OptionStorage<T>.Instance != null)
			{
				throw new ArgumentException(typeof(T) + " has already been registered");
			}
			T t = new T();
			WorldGenerationOptions.OptionStorage<T>.Instance = t;
			WorldGenerationOptions._options.Add(t);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x004DD553 File Offset: 0x004DB753
		public static void Reset()
		{
			WorldGenerationOptions.Get<WorldSeedOption_Normal>().Enabled = true;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x004DD560 File Offset: 0x004DB760
		public static void SelectOption(AWorldGenerationOption option)
		{
			WorldGenerationOptions.Reset();
			if (option != null)
			{
				option.Enabled = true;
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x004DD574 File Offset: 0x004DB774
		public static AWorldGenerationOption GetOptionFromSeedText(string processedSeed)
		{
			int num = WorldFileData.TranslateSeed(processedSeed);
			string text = Regex.Replace(processedSeed.ToLower(), "[^a-z0-9]+", "");
			foreach (AWorldGenerationOption aworldGenerationOption in WorldGenerationOptions.Options)
			{
				foreach (int num2 in aworldGenerationOption.SpecialSeedValues)
				{
					if (num == num2)
					{
						return aworldGenerationOption;
					}
				}
				foreach (string text2 in aworldGenerationOption.SpecialSeedNames)
				{
					if (text == text2)
					{
						return aworldGenerationOption;
					}
				}
			}
			return null;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x004DD638 File Offset: 0x004DB838
		public static void TryEnablingFlagFrom(string line)
		{
			int length = "seed_".Length;
			if (line.Length < length)
			{
				return;
			}
			if (!line.ToLower().StartsWith("seed_"))
			{
				return;
			}
			string[] array = line.Substring(length).Split(new char[] { '=' });
			if (array.Length != 2)
			{
				return;
			}
			int num;
			if (!int.TryParse(array[1].Trim(), out num))
			{
				return;
			}
			bool flag = Utils.Clamp<int>(num, 0, 1) == 1;
			string namePiece = array[0].Trim().ToLower();
			AWorldGenerationOption aworldGenerationOption = WorldGenerationOptions._options.FirstOrDefault((AWorldGenerationOption x) => x.ServerConfigName != null && x.ServerConfigName == namePiece);
			if (aworldGenerationOption == null)
			{
				return;
			}
			aworldGenerationOption.AutoGenEnabled = flag;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0000357B File Offset: 0x0000177B
		public WorldGenerationOptions()
		{
		}

		// Token: 0x040011CC RID: 4556
		private static List<AWorldGenerationOption> _options = new List<AWorldGenerationOption>();

		// Token: 0x040011CD RID: 4557
		private const string _powerPermissionsLineHeader = "seed_";

		// Token: 0x0200068C RID: 1676
		private class OptionStorage<T> where T : AWorldGenerationOption
		{
			// Token: 0x06003E95 RID: 16021 RVA: 0x0000357B File Offset: 0x0000177B
			public OptionStorage()
			{
			}

			// Token: 0x04006763 RID: 26467
			public static T Instance;
		}

		// Token: 0x0200068D RID: 1677
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x06003E96 RID: 16022 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06003E97 RID: 16023 RVA: 0x0069814D File Offset: 0x0069634D
			internal bool <TryEnablingFlagFrom>b__0(AWorldGenerationOption x)
			{
				return x.ServerConfigName != null && x.ServerConfigName == this.namePiece;
			}

			// Token: 0x04006764 RID: 26468
			public string namePiece;
		}
	}
}
