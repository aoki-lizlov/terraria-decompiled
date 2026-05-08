using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200056B RID: 1387
	public class EntitySource_Mount : IEntitySource
	{
		// Token: 0x060037EE RID: 14318 RVA: 0x00630675 File Offset: 0x0062E875
		public EntitySource_Mount(IEntitySourceTarget entity, int mountId)
		{
			this.Entity = entity;
			this.MountId = mountId;
		}

		// Token: 0x04005C28 RID: 23592
		public readonly IEntitySourceTarget Entity;

		// Token: 0x04005C29 RID: 23593
		public readonly int MountId;
	}
}
