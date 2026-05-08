using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Modules
{
	// Token: 0x02000069 RID: 105
	public class TileObjectCoordinatesModule
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x004BB62C File Offset: 0x004B982C
		public TileObjectCoordinatesModule(TileObjectCoordinatesModule copyFrom = null, int[] drawHeight = null, Rectangle[,] drawFrameOffs = null)
		{
			if (copyFrom == null)
			{
				this.width = 0;
				this.padding = 0;
				this.paddingFix = Point16.Zero;
				this.styleWidth = 0;
				this.drawStyleOffset = 0;
				this.styleHeight = 0;
				this.calculated = false;
				this.heights = drawHeight;
				this.drawFrameOffsets = drawFrameOffs;
				return;
			}
			this.width = copyFrom.width;
			this.padding = copyFrom.padding;
			this.paddingFix = copyFrom.paddingFix;
			this.drawStyleOffset = copyFrom.drawStyleOffset;
			this.styleWidth = copyFrom.styleWidth;
			this.styleHeight = copyFrom.styleHeight;
			this.calculated = copyFrom.calculated;
			if (drawHeight == null)
			{
				if (copyFrom.heights == null)
				{
					this.heights = null;
				}
				else
				{
					this.heights = new int[copyFrom.heights.Length];
					Array.Copy(copyFrom.heights, this.heights, this.heights.Length);
				}
			}
			else
			{
				this.heights = drawHeight;
			}
			if (drawFrameOffs != null)
			{
				this.drawFrameOffsets = drawFrameOffs;
				return;
			}
			if (copyFrom.drawFrameOffsets == null)
			{
				this.drawFrameOffsets = null;
				return;
			}
			this.drawFrameOffsets = new Rectangle[copyFrom.drawFrameOffsets.GetLength(0), copyFrom.drawFrameOffsets.GetLength(1)];
			Array.Copy(copyFrom.drawFrameOffsets, this.drawFrameOffsets, this.drawFrameOffsets.Length);
		}

		// Token: 0x04001075 RID: 4213
		public int width;

		// Token: 0x04001076 RID: 4214
		public int[] heights;

		// Token: 0x04001077 RID: 4215
		public int padding;

		// Token: 0x04001078 RID: 4216
		public Point16 paddingFix;

		// Token: 0x04001079 RID: 4217
		public int styleWidth;

		// Token: 0x0400107A RID: 4218
		public int styleHeight;

		// Token: 0x0400107B RID: 4219
		public bool calculated;

		// Token: 0x0400107C RID: 4220
		public int drawStyleOffset;

		// Token: 0x0400107D RID: 4221
		public Rectangle[,] drawFrameOffsets;
	}
}
