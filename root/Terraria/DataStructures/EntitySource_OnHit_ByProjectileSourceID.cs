using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000580 RID: 1408
	public class EntitySource_OnHit_ByProjectileSourceID : AEntitySource_OnHit
	{
		// Token: 0x06003803 RID: 14339 RVA: 0x00630752 File Offset: 0x0062E952
		public EntitySource_OnHit_ByProjectileSourceID(IEntitySourceTarget entityStriking, IEntitySourceTarget entityStruck, int projectileSourceId)
			: base(entityStriking, entityStruck)
		{
			this.SourceId = projectileSourceId;
		}

		// Token: 0x04005C37 RID: 23607
		public readonly int SourceId;
	}
}
