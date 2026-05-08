using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000564 RID: 1380
	public struct PortableStoolUsage
	{
		// Token: 0x060037E7 RID: 14311 RVA: 0x006305C9 File Offset: 0x0062E7C9
		public void Reset()
		{
			this.HasAStool = false;
			this.IsInUse = false;
			this.HeightBoost = 0;
			this.VisualYOffset = 0;
			this.MapYOffset = 0;
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x006305EE File Offset: 0x0062E7EE
		public void SetStats(int heightBoost, int visualYOffset, int mapYOffset)
		{
			this.HasAStool = true;
			this.HeightBoost = heightBoost;
			this.VisualYOffset = visualYOffset;
			this.MapYOffset = mapYOffset;
		}

		// Token: 0x04005C1A RID: 23578
		public bool HasAStool;

		// Token: 0x04005C1B RID: 23579
		public bool IsInUse;

		// Token: 0x04005C1C RID: 23580
		public int HeightBoost;

		// Token: 0x04005C1D RID: 23581
		public int VisualYOffset;

		// Token: 0x04005C1E RID: 23582
		public int MapYOffset;
	}
}
