using System;
using Terraria.DataStructures;
using Terraria.Enums;

namespace Terraria.Modules
{
	// Token: 0x02000062 RID: 98
	public class TileObjectBaseModule
	{
		// Token: 0x0600145F RID: 5215 RVA: 0x004BB2DC File Offset: 0x004B94DC
		public TileObjectBaseModule(TileObjectBaseModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.width = 1;
				this.height = 1;
				this.origin = Point16.Zero;
				this.direction = TileObjectDirection.None;
				this.randomRange = 0;
				this.flattenAnchors = false;
				this.specificRandomStyles = null;
				return;
			}
			this.width = copyFrom.width;
			this.height = copyFrom.height;
			this.origin = copyFrom.origin;
			this.direction = copyFrom.direction;
			this.randomRange = copyFrom.randomRange;
			this.flattenAnchors = copyFrom.flattenAnchors;
			this.specificRandomStyles = null;
			if (copyFrom.specificRandomStyles != null)
			{
				this.specificRandomStyles = new int[copyFrom.specificRandomStyles.Length];
				copyFrom.specificRandomStyles.CopyTo(this.specificRandomStyles, 0);
			}
		}

		// Token: 0x04001058 RID: 4184
		public int width;

		// Token: 0x04001059 RID: 4185
		public int height;

		// Token: 0x0400105A RID: 4186
		public Point16 origin;

		// Token: 0x0400105B RID: 4187
		public TileObjectDirection direction;

		// Token: 0x0400105C RID: 4188
		public int randomRange;

		// Token: 0x0400105D RID: 4189
		public bool flattenAnchors;

		// Token: 0x0400105E RID: 4190
		public int[] specificRandomStyles;
	}
}
