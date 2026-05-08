using System;

namespace Terraria.Modules
{
	// Token: 0x02000068 RID: 104
	public class TileObjectStyleModule
	{
		// Token: 0x06001465 RID: 5221 RVA: 0x004BB584 File Offset: 0x004B9784
		public TileObjectStyleModule(TileObjectStyleModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.style = 0;
				this.horizontal = false;
				this.styleWrapLimit = 0;
				this.styleWrapLimitVisualOverride = null;
				this.styleLineSkipVisualoverride = null;
				this.styleMultiplier = 1;
				this.styleLineSkip = 1;
				return;
			}
			this.style = copyFrom.style;
			this.horizontal = copyFrom.horizontal;
			this.styleWrapLimit = copyFrom.styleWrapLimit;
			this.styleMultiplier = copyFrom.styleMultiplier;
			this.styleLineSkip = copyFrom.styleLineSkip;
			this.styleWrapLimitVisualOverride = copyFrom.styleWrapLimitVisualOverride;
			this.styleLineSkipVisualoverride = copyFrom.styleLineSkipVisualoverride;
		}

		// Token: 0x0400106E RID: 4206
		public int style;

		// Token: 0x0400106F RID: 4207
		public bool horizontal;

		// Token: 0x04001070 RID: 4208
		public int styleWrapLimit;

		// Token: 0x04001071 RID: 4209
		public int styleMultiplier;

		// Token: 0x04001072 RID: 4210
		public int styleLineSkip;

		// Token: 0x04001073 RID: 4211
		public int? styleWrapLimitVisualOverride;

		// Token: 0x04001074 RID: 4212
		public int? styleLineSkipVisualoverride;
	}
}
