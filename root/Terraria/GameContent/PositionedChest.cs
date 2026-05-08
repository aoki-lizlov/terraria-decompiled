using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x0200023B RID: 571
	public struct PositionedChest
	{
		// Token: 0x06002286 RID: 8838 RVA: 0x00539149 File Offset: 0x00537349
		public PositionedChest(Chest chest, Vector2 position)
		{
			this.chest = chest;
			this.position = position;
		}

		// Token: 0x04004D00 RID: 19712
		public Chest chest;

		// Token: 0x04004D01 RID: 19713
		public Vector2 position;
	}
}
