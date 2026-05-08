using System;

namespace Terraria.Modules
{
	// Token: 0x02000065 RID: 101
	public class TileObjectDrawModule
	{
		// Token: 0x06001462 RID: 5218 RVA: 0x004BB408 File Offset: 0x004B9608
		public TileObjectDrawModule(TileObjectDrawModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.xOffset = 0;
				this.yOffset = 0;
				this.flipHorizontal = false;
				this.flipVertical = false;
				this.stepDown = 0;
				return;
			}
			this.xOffset = copyFrom.xOffset;
			this.yOffset = copyFrom.yOffset;
			this.flipHorizontal = copyFrom.flipHorizontal;
			this.flipVertical = copyFrom.flipVertical;
			this.stepDown = copyFrom.stepDown;
		}

		// Token: 0x04001063 RID: 4195
		public int xOffset;

		// Token: 0x04001064 RID: 4196
		public int yOffset;

		// Token: 0x04001065 RID: 4197
		public bool flipHorizontal;

		// Token: 0x04001066 RID: 4198
		public bool flipVertical;

		// Token: 0x04001067 RID: 4199
		public int stepDown;
	}
}
