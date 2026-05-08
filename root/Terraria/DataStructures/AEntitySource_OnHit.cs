using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200057F RID: 1407
	public abstract class AEntitySource_OnHit : IEntitySource
	{
		// Token: 0x06003802 RID: 14338 RVA: 0x0063073C File Offset: 0x0062E93C
		public AEntitySource_OnHit(IEntitySourceTarget entityStriking, IEntitySourceTarget entityStruck)
		{
			this.EntityStriking = entityStriking;
			this.EntityStruck = entityStruck;
		}

		// Token: 0x04005C35 RID: 23605
		public readonly IEntitySourceTarget EntityStriking;

		// Token: 0x04005C36 RID: 23606
		public readonly IEntitySourceTarget EntityStruck;
	}
}
