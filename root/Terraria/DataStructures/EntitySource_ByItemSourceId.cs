using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000574 RID: 1396
	public class EntitySource_ByItemSourceId : IEntitySource
	{
		// Token: 0x060037F7 RID: 14327 RVA: 0x006306DB File Offset: 0x0062E8DB
		public EntitySource_ByItemSourceId(IEntitySourceTarget entity, int itemSourceId)
		{
			this.Entity = entity;
			this.SourceId = itemSourceId;
		}

		// Token: 0x04005C2E RID: 23598
		public readonly IEntitySourceTarget Entity;

		// Token: 0x04005C2F RID: 23599
		public readonly int SourceId;
	}
}
