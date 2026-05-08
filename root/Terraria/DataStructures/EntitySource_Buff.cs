using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000567 RID: 1383
	public class EntitySource_Buff : IEntitySource
	{
		// Token: 0x060037EA RID: 14314 RVA: 0x0063061B File Offset: 0x0062E81B
		public EntitySource_Buff(IEntitySourceTarget entity, int buffId, int buffIndex)
		{
			this.Entity = entity;
			this.BuffId = buffId;
			this.BuffIndex = buffIndex;
		}

		// Token: 0x04005C20 RID: 23584
		public readonly IEntitySourceTarget Entity;

		// Token: 0x04005C21 RID: 23585
		public readonly int BuffId;

		// Token: 0x04005C22 RID: 23586
		public readonly int BuffIndex;
	}
}
