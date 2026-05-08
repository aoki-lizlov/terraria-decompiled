using System;
using System.Collections.Generic;
using Terraria.GameContent.Generation.Dungeon.Features;
using Terraria.GameContent.Generation.Dungeon.Rooms;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x0200049D RID: 1181
	public class DungeonGenerationStyleData
	{
		// Token: 0x060033CE RID: 13262 RVA: 0x000379E9 File Offset: 0x00035BE9
		public virtual bool CanGenerateFeatureAt(DungeonData data, DungeonRoom room, IDungeonFeature feature, int x, int y)
		{
			return true;
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x005FA4D3 File Offset: 0x005F86D3
		public virtual void GetBookshelfMinMaxSizes(int defaultMin, int defaultMax, out int min, out int max)
		{
			min = defaultMin;
			max = defaultMax;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x005FA4DC File Offset: 0x005F86DC
		public bool TileIsInStyle(int tileType, bool includeCracked = true)
		{
			return (this.BrickGrassTileType != null && tileType == (int)this.BrickGrassTileType.Value) || (includeCracked && tileType == (int)this.BrickCrackedTileType) || tileType == (int)this.BrickTileType;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x005FA512 File Offset: 0x005F8712
		public bool WallIsInStyle(int wallType, bool includeWindows = false)
		{
			return (includeWindows && (wallType == (int)this.WindowGlassWallType || wallType == (int)this.WindowEdgeWallType || wallType == (int)this.WindowClosedGlassWallType)) || wallType == (int)this.BrickWallType;
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x005FA540 File Offset: 0x005F8740
		public int GetPlatformStyle(UnifiedRandom genRand)
		{
			int num = ((this.PlatformItemTypes == null || this.PlatformItemTypes.Length == 0) ? (-1) : this.PlatformItemTypes[genRand.Next(this.PlatformItemTypes.Length)]);
			if (num >= 0)
			{
				return (int)ItemID.Sets.DerivedPlacementDetails[num].tileStyle;
			}
			return -1;
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x005FA590 File Offset: 0x005F8790
		public int GetWindowPlatformStyle(UnifiedRandom genRand)
		{
			int num = ((this.WindowPlatformItemTypes == null || this.WindowPlatformItemTypes.Length == 0) ? (-1) : this.WindowPlatformItemTypes[genRand.Next(this.WindowPlatformItemTypes.Length)]);
			if (num >= 0)
			{
				return (int)ItemID.Sets.DerivedPlacementDetails[num].tileStyle;
			}
			return -1;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x005FA5DD File Offset: 0x005F87DD
		public DungeonGenerationStyleData()
		{
		}

		// Token: 0x0400597A RID: 22906
		public byte Style;

		// Token: 0x0400597B RID: 22907
		public int UnbreakableWallProgressionTier = -1;

		// Token: 0x0400597C RID: 22908
		public ushort BrickTileType;

		// Token: 0x0400597D RID: 22909
		public ushort? BrickGrassTileType;

		// Token: 0x0400597E RID: 22910
		public ushort BrickCrackedTileType;

		// Token: 0x0400597F RID: 22911
		public ushort BrickWallType;

		// Token: 0x04005980 RID: 22912
		public ushort WindowGlassWallType;

		// Token: 0x04005981 RID: 22913
		public ushort WindowClosedGlassWallType;

		// Token: 0x04005982 RID: 22914
		public ushort WindowEdgeWallType;

		// Token: 0x04005983 RID: 22915
		public int[] WindowPlatformItemTypes;

		// Token: 0x04005984 RID: 22916
		public ushort PitTrapTileType;

		// Token: 0x04005985 RID: 22917
		public int LiquidType = -1;

		// Token: 0x04005986 RID: 22918
		public int LockedBiomeChestType;

		// Token: 0x04005987 RID: 22919
		public int LockedBiomeChestStyle;

		// Token: 0x04005988 RID: 22920
		public int BiomeChestItemType;

		// Token: 0x04005989 RID: 22921
		public int BiomeChestLootItemType;

		// Token: 0x0400598A RID: 22922
		public int[] ChestItemTypes;

		// Token: 0x0400598B RID: 22923
		public int[] DoorItemTypes;

		// Token: 0x0400598C RID: 22924
		public int[] PlatformItemTypes;

		// Token: 0x0400598D RID: 22925
		public int[] ChandelierItemTypes;

		// Token: 0x0400598E RID: 22926
		public int[] LanternItemTypes;

		// Token: 0x0400598F RID: 22927
		public int[] TableItemTypes;

		// Token: 0x04005990 RID: 22928
		public int[] WorkbenchItemTypes;

		// Token: 0x04005991 RID: 22929
		public int[] CandleItemTypes;

		// Token: 0x04005992 RID: 22930
		public int[] VaseOrStatueItemTypes;

		// Token: 0x04005993 RID: 22931
		public int[] BookcaseItemTypes;

		// Token: 0x04005994 RID: 22932
		public int[] ChairItemTypes;

		// Token: 0x04005995 RID: 22933
		public int[] BedItemTypes;

		// Token: 0x04005996 RID: 22934
		public int[] PianoItemTypes;

		// Token: 0x04005997 RID: 22935
		public int[] DresserItemTypes;

		// Token: 0x04005998 RID: 22936
		public int[] SofaItemTypes;

		// Token: 0x04005999 RID: 22937
		public int[] BathtubItemTypes;

		// Token: 0x0400599A RID: 22938
		public int[] LampItemTypes;

		// Token: 0x0400599B RID: 22939
		public int[] CandelabraItemTypes;

		// Token: 0x0400599C RID: 22940
		public int[] ClockItemTypes;

		// Token: 0x0400599D RID: 22941
		public int[] BannerItemTypes;

		// Token: 0x0400599E RID: 22942
		public bool EdgeDither;

		// Token: 0x0400599F RID: 22943
		public DungeonRoomType BiomeRoomType;

		// Token: 0x040059A0 RID: 22944
		public List<DungeonGenerationStyleData> SubStyles;
	}
}
