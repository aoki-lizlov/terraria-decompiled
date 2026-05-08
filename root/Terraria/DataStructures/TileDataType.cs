using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000589 RID: 1417
	[Flags]
	public enum TileDataType
	{
		// Token: 0x04005C3F RID: 23615
		Tile = 1,
		// Token: 0x04005C40 RID: 23616
		TilePaint = 2,
		// Token: 0x04005C41 RID: 23617
		Wall = 4,
		// Token: 0x04005C42 RID: 23618
		WallPaint = 8,
		// Token: 0x04005C43 RID: 23619
		Liquid = 16,
		// Token: 0x04005C44 RID: 23620
		Wiring = 32,
		// Token: 0x04005C45 RID: 23621
		Actuator = 64,
		// Token: 0x04005C46 RID: 23622
		Slope = 128,
		// Token: 0x04005C47 RID: 23623
		All = 255
	}
}
