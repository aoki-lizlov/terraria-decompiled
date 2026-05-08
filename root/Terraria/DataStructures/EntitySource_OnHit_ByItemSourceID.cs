using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000581 RID: 1409
	public class EntitySource_OnHit_ByItemSourceID : AEntitySource_OnHit
	{
		// Token: 0x06003804 RID: 14340 RVA: 0x00630763 File Offset: 0x0062E963
		public EntitySource_OnHit_ByItemSourceID(IEntitySourceTarget entityStriking, IEntitySourceTarget entityStruck, int itemSourceId)
			: base(entityStriking, entityStruck)
		{
			this.SourceId = itemSourceId;
		}

		// Token: 0x04005C38 RID: 23608
		public readonly int SourceId;
	}
}
