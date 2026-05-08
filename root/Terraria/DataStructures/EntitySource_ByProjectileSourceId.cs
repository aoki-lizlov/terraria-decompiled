using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000573 RID: 1395
	public class EntitySource_ByProjectileSourceId : IEntitySource
	{
		// Token: 0x060037F6 RID: 14326 RVA: 0x006306CC File Offset: 0x0062E8CC
		public EntitySource_ByProjectileSourceId(int projectileSourceId)
		{
			this.SourceId = projectileSourceId;
		}

		// Token: 0x04005C2D RID: 23597
		public readonly int SourceId;
	}
}
