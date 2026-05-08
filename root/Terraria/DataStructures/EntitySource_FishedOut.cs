using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200057C RID: 1404
	public class EntitySource_FishedOut : IEntitySource
	{
		// Token: 0x060037FF RID: 14335 RVA: 0x0063070F File Offset: 0x0062E90F
		public EntitySource_FishedOut(IEntitySourceTarget entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04005C32 RID: 23602
		public readonly IEntitySourceTarget Entity;
	}
}
