using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200057D RID: 1405
	public class EntitySource_Gift : IEntitySource
	{
		// Token: 0x06003800 RID: 14336 RVA: 0x0063071E File Offset: 0x0062E91E
		public EntitySource_Gift(Entity entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04005C33 RID: 23603
		public readonly IEntitySourceTarget Entity;
	}
}
