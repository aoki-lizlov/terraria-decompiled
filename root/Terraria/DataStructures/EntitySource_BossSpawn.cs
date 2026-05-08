using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200057A RID: 1402
	public class EntitySource_BossSpawn : IEntitySource
	{
		// Token: 0x060037FD RID: 14333 RVA: 0x006306F1 File Offset: 0x0062E8F1
		public EntitySource_BossSpawn(IEntitySourceTarget entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04005C30 RID: 23600
		public readonly IEntitySourceTarget Entity;
	}
}
