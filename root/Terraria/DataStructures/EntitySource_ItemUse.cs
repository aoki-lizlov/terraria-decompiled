using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000568 RID: 1384
	public class EntitySource_ItemUse : IEntitySource
	{
		// Token: 0x060037EB RID: 14315 RVA: 0x00630638 File Offset: 0x0062E838
		public EntitySource_ItemUse(IEntitySourceTarget entity, Item item)
		{
			this.Entity = entity;
			this.Item = item;
		}

		// Token: 0x04005C23 RID: 23587
		public readonly IEntitySourceTarget Entity;

		// Token: 0x04005C24 RID: 23588
		public readonly Item Item;
	}
}
