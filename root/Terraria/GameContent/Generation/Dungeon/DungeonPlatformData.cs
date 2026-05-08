using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x02000494 RID: 1172
	public struct DungeonPlatformData
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x005FA038 File Offset: 0x005F8238
		public bool IsAShelf
		{
			get
			{
				return this.PlaceBooksChance > 0.0 || this.PlacePotsChance > 0.0 || this.PlaceWaterCandlesChance > 0.0 || this.PlacePotionBottlesChance > 0.0;
			}
		}

		// Token: 0x04005905 RID: 22789
		public Point Position;

		// Token: 0x04005906 RID: 22790
		public int? OverrideStyle;

		// Token: 0x04005907 RID: 22791
		public int OverrideMaxLengthAllowed;

		// Token: 0x04005908 RID: 22792
		public int? OverrideHeightFluff;

		// Token: 0x04005909 RID: 22793
		public bool InAHallway;

		// Token: 0x0400590A RID: 22794
		public bool ForcePlacement;

		// Token: 0x0400590B RID: 22795
		public bool SkipOtherPlatformsCheck;

		// Token: 0x0400590C RID: 22796
		public bool SkipSpaceCheck;

		// Token: 0x0400590D RID: 22797
		public double PlaceBooksChance;

		// Token: 0x0400590E RID: 22798
		public bool NoWaterbolt;

		// Token: 0x0400590F RID: 22799
		public double PlacePotsChance;

		// Token: 0x04005910 RID: 22800
		public double PlaceWaterCandlesChance;

		// Token: 0x04005911 RID: 22801
		public double PlacePotionBottlesChance;

		// Token: 0x04005912 RID: 22802
		public Func<DungeonData, int, int, bool> canPlaceHereCallback;
	}
}
