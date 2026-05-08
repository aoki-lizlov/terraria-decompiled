using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent
{
	// Token: 0x02000243 RID: 579
	public static class SecretSeedsTracker
	{
		// Token: 0x060022C7 RID: 8903 RVA: 0x0053A4C6 File Offset: 0x005386C6
		public static void SetstringsFromConfig(ICollection<string> seedStrings)
		{
			SecretSeedsTracker._seedsForConfig.AddRange(seedStrings);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0053A4D4 File Offset: 0x005386D4
		public static void PrepareInterface()
		{
			if (!SecretSeedsTracker._processedConfig)
			{
				SecretSeedsTracker._processedConfig = true;
				SecretSeedsTracker._seedsForInterface.Clear();
				using (List<string>.Enumerator enumerator = SecretSeedsTracker._seedsForConfig.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						WorldGen.SecretSeed secretSeed;
						if (SecretSeedsTracker.SeedHasSecret(enumerator.Current, out secretSeed))
						{
							SecretSeedsTracker._seedsForInterface.Add(secretSeed);
						}
					}
				}
				SecretSeedsTracker._seedsForInterface = SecretSeedsTracker._seedsForInterface.Distinct<WorldGen.SecretSeed>().ToList<WorldGen.SecretSeed>();
				SecretSeedsTracker._seedsForConfig.Clear();
				SecretSeedsTracker._seedsForConfig.AddRange(SecretSeedsTracker._seedsForInterface.Select((WorldGen.SecretSeed x) => x.TextThatWasUsedToUnlock));
			}
			SecretSeedsTracker._seedsForConfig.Sort();
			SecretSeedsTracker._seedsForInterface.Sort((WorldGen.SecretSeed a, WorldGen.SecretSeed b) => a.TextThatWasUsedToUnlock.CompareTo(b.TextThatWasUsedToUnlock));
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0053A5D0 File Offset: 0x005387D0
		private static bool SeedHasSecret(string seedString, out WorldGen.SecretSeed seed)
		{
			return WorldGen.SecretSeed.CheckInputForSecretSeed(seedString, out seed);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0053A5DC File Offset: 0x005387DC
		public static void AddSeedToTrack(string seedString)
		{
			WorldGen.SecretSeed secretSeed;
			if (!SecretSeedsTracker.SeedHasSecret(seedString, out secretSeed))
			{
				return;
			}
			if (SecretSeedsTracker._seedsForInterface.Contains(secretSeed))
			{
				return;
			}
			SecretSeedsTracker._seedsForConfig.Add(secretSeed.TextThatWasUsedToUnlock);
			SecretSeedsTracker._seedsForInterface.Add(secretSeed);
			Main.SaveSettings();
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0053A623 File Offset: 0x00538823
		public static List<string> GetStringsToSave()
		{
			return SecretSeedsTracker._seedsForConfig;
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x0053A62A File Offset: 0x0053882A
		public static List<WorldGen.SecretSeed> SeedsForInterface
		{
			get
			{
				return SecretSeedsTracker._seedsForInterface;
			}
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0053A631 File Offset: 0x00538831
		// Note: this type is marked as 'beforefieldinit'.
		static SecretSeedsTracker()
		{
		}

		// Token: 0x04004D19 RID: 19737
		private static List<string> _seedsForConfig = new List<string>();

		// Token: 0x04004D1A RID: 19738
		private static List<WorldGen.SecretSeed> _seedsForInterface = new List<WorldGen.SecretSeed>();

		// Token: 0x04004D1B RID: 19739
		private static bool _processedConfig = false;

		// Token: 0x020007CE RID: 1998
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600422E RID: 16942 RVA: 0x006BE0AC File Offset: 0x006BC2AC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600422F RID: 16943 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004230 RID: 16944 RVA: 0x006BE0B8 File Offset: 0x006BC2B8
			internal string <PrepareInterface>b__4_1(WorldGen.SecretSeed x)
			{
				return x.TextThatWasUsedToUnlock;
			}

			// Token: 0x06004231 RID: 16945 RVA: 0x006BE0C0 File Offset: 0x006BC2C0
			internal int <PrepareInterface>b__4_0(WorldGen.SecretSeed a, WorldGen.SecretSeed b)
			{
				return a.TextThatWasUsedToUnlock.CompareTo(b.TextThatWasUsedToUnlock);
			}

			// Token: 0x04007102 RID: 28930
			public static readonly SecretSeedsTracker.<>c <>9 = new SecretSeedsTracker.<>c();

			// Token: 0x04007103 RID: 28931
			public static Func<WorldGen.SecretSeed, string> <>9__4_1;

			// Token: 0x04007104 RID: 28932
			public static Comparison<WorldGen.SecretSeed> <>9__4_0;
		}
	}
}
