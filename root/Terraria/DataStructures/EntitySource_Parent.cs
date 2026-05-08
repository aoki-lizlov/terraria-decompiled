using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000566 RID: 1382
	public class EntitySource_Parent : IEntitySource
	{
		// Token: 0x060037E9 RID: 14313 RVA: 0x0063060C File Offset: 0x0062E80C
		public EntitySource_Parent(IEntitySourceTarget entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04005C1F RID: 23583
		public readonly IEntitySourceTarget Entity;
	}
}
