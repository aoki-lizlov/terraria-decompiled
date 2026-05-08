using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200057B RID: 1403
	public class EntitySource_DropAsItem : IEntitySource
	{
		// Token: 0x060037FE RID: 14334 RVA: 0x00630700 File Offset: 0x0062E900
		public EntitySource_DropAsItem(IEntitySourceTarget entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04005C31 RID: 23601
		public readonly IEntitySourceTarget Entity;
	}
}
