using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000584 RID: 1412
	public class EntitySource_TileEntity : IEntitySource
	{
		// Token: 0x06003807 RID: 14343 RVA: 0x00630774 File Offset: 0x0062E974
		public EntitySource_TileEntity(TileEntity tileEntity)
		{
			this.TileEntity = tileEntity;
		}

		// Token: 0x04005C39 RID: 23609
		public TileEntity TileEntity;
	}
}
