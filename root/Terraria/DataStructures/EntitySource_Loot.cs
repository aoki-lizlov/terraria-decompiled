using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200057E RID: 1406
	public class EntitySource_Loot : IEntitySource
	{
		// Token: 0x06003801 RID: 14337 RVA: 0x0063072D File Offset: 0x0062E92D
		public EntitySource_Loot(IEntitySourceTarget entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04005C34 RID: 23604
		public readonly IEntitySourceTarget Entity;
	}
}
