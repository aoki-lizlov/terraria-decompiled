using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000569 RID: 1385
	public class EntitySource_ItemOpen : IEntitySource
	{
		// Token: 0x060037EC RID: 14316 RVA: 0x0063064E File Offset: 0x0062E84E
		public EntitySource_ItemOpen(IEntitySourceTarget entity, int itemType)
		{
			this.Entity = entity;
			this.ItemType = itemType;
		}

		// Token: 0x04005C25 RID: 23589
		public readonly IEntitySourceTarget Entity;

		// Token: 0x04005C26 RID: 23590
		public readonly int ItemType;
	}
}
