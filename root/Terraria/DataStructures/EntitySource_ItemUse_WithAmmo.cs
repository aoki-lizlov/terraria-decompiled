using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200056A RID: 1386
	public class EntitySource_ItemUse_WithAmmo : EntitySource_ItemUse
	{
		// Token: 0x060037ED RID: 14317 RVA: 0x00630664 File Offset: 0x0062E864
		public EntitySource_ItemUse_WithAmmo(IEntitySourceTarget entity, Item item, int ammoItemIdUsed)
			: base(entity, item)
		{
			this.AmmoItemIdUsed = ammoItemIdUsed;
		}

		// Token: 0x04005C27 RID: 23591
		public readonly int AmmoItemIdUsed;
	}
}
