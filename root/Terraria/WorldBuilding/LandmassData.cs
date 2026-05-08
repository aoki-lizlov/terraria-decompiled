using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000B4 RID: 180
	public struct LandmassData
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x004DE127 File Offset: 0x004DC327
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x004DE145 File Offset: 0x004DC345
		public Vector2 Top
		{
			get
			{
				return this.Position - new Vector2(0f, (float)this.RadiusOrHalfSize);
			}
			set
			{
				this.Position = value + new Vector2(0f, (float)this.RadiusOrHalfSize);
			}
		}

		// Token: 0x040011F1 RID: 4593
		public LandmassDataType DataType;

		// Token: 0x040011F2 RID: 4594
		public Vector2 Position;

		// Token: 0x040011F3 RID: 4595
		public int RadiusOrHalfSize;

		// Token: 0x040011F4 RID: 4596
		public int Style;
	}
}
