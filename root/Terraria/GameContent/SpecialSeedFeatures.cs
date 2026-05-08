using System;

namespace Terraria.GameContent
{
	// Token: 0x02000244 RID: 580
	public class SpecialSeedFeatures
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x0053A64D File Offset: 0x0053884D
		public static bool ShouldDropExtraGel
		{
			get
			{
				return Main.tenthAnniversaryWorld && Main.drunkWorld && !Main.remixWorld && !Main.notTheBeesWorld;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x0053A64D File Offset: 0x0053884D
		public static bool ShouldDropExtraWood
		{
			get
			{
				return Main.tenthAnniversaryWorld && Main.drunkWorld && !Main.remixWorld && !Main.notTheBeesWorld;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x0053A66E File Offset: 0x0053886E
		public static bool DungeonEntranceHasATree
		{
			get
			{
				return Main.drunkWorld && !SpecialSeedFeatures.NoDungeonGuardian;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x0053A681 File Offset: 0x00538881
		public static bool DungeonEntranceHasStairs
		{
			get
			{
				return !SpecialSeedFeatures.DungeonEntranceIsUnderground && !WorldGen.SecretSeed.roundLandmasses.Enabled;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x0053A699 File Offset: 0x00538899
		public static bool DungeonEntranceIsBuried
		{
			get
			{
				return WorldGen.SecretSeed.surfaceIsDesert.Enabled && !SpecialSeedFeatures.DungeonEntranceIsUnderground;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x0053A6B1 File Offset: 0x005388B1
		public static bool DungeonEntranceIsUnderground
		{
			get
			{
				return Main.drunkWorld || WorldGen.SecretSeed.noSurface.Enabled;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x0053A6C6 File Offset: 0x005388C6
		public static bool NoDungeonGuardian
		{
			get
			{
				return Main.onlyShimmerOceanWorlds;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0053A6CD File Offset: 0x005388CD
		public static bool BossesKeepSpawning
		{
			get
			{
				return Main.getGoodWorld && Main.dontStarveWorld && !Main.tenthAnniversaryWorld;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x0053A6C6 File Offset: 0x005388C6
		public static bool ShimmerSpawnHalfOfWorld
		{
			get
			{
				return Main.onlyShimmerOceanWorlds;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x0053A6C6 File Offset: 0x005388C6
		public static bool RainbowSandAndBlackSandWalls
		{
			get
			{
				return Main.onlyShimmerOceanWorlds;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x0053A6E7 File Offset: 0x005388E7
		public static bool SpawnOnBeach
		{
			get
			{
				return Main.tenthAnniversaryWorld && !Main.remixWorld && !Main.dontStarveWorld;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x0053A701 File Offset: 0x00538901
		public static bool SpawnOnBeachOnDungeonSide
		{
			get
			{
				return SpecialSeedFeatures.SpawnOnBeach && Main.onlyShimmerOceanWorlds;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x0053A711 File Offset: 0x00538911
		public static bool Mechdusa
		{
			get
			{
				return Main.remixWorld && Main.getGoodWorld;
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0000357B File Offset: 0x0000177B
		public SpecialSeedFeatures()
		{
		}
	}
}
