using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000551 RID: 1361
	public struct ItemSyncPersistentStats
	{
		// Token: 0x0600378B RID: 14219 RVA: 0x0062F6B2 File Offset: 0x0062D8B2
		public void CopyFrom(WorldItem item)
		{
			this.type = item.type;
			this.color = item.color;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x0062F6CC File Offset: 0x0062D8CC
		public void PasteInto(WorldItem item)
		{
			if (this.type != item.type)
			{
				return;
			}
			item.color = this.color;
		}

		// Token: 0x04005BBC RID: 23484
		private Color color;

		// Token: 0x04005BBD RID: 23485
		private int type;
	}
}
