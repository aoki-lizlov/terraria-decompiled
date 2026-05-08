using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x02000493 RID: 1171
	public struct DungeonDoorData
	{
		// Token: 0x040058FB RID: 22779
		public Point Position;

		// Token: 0x040058FC RID: 22780
		public ushort? OverrideBrickTileType;

		// Token: 0x040058FD RID: 22781
		public ushort? OverrideBrickWallType;

		// Token: 0x040058FE RID: 22782
		public int? OverrideStyle;

		// Token: 0x040058FF RID: 22783
		public int Direction;

		// Token: 0x04005900 RID: 22784
		public bool InAHallway;

		// Token: 0x04005901 RID: 22785
		public int? OverrideWidthFluff;

		// Token: 0x04005902 RID: 22786
		public bool SkipOtherDoorsCheck;

		// Token: 0x04005903 RID: 22787
		public bool SkipSpaceCheck;

		// Token: 0x04005904 RID: 22788
		public bool AlwaysClearArea;
	}
}
