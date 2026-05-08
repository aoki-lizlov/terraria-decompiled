using System;

namespace Terraria.DataStructures
{
	// Token: 0x020005A1 RID: 1441
	public struct PlayerInteractionAnchor
	{
		// Token: 0x060038FE RID: 14590 RVA: 0x00651116 File Offset: 0x0064F316
		public PlayerInteractionAnchor(int entityID, int x = -1, int y = -1)
		{
			this.interactEntityID = entityID;
			this.X = x;
			this.Y = y;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x0065112D File Offset: 0x0064F32D
		public bool InUse
		{
			get
			{
				return this.interactEntityID != -1;
			}
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x0065113B File Offset: 0x0064F33B
		public void Clear()
		{
			this.interactEntityID = -1;
			this.X = -1;
			this.Y = -1;
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x00651116 File Offset: 0x0064F316
		public void Set(int entityID, int x, int y)
		{
			this.interactEntityID = entityID;
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x00651152 File Offset: 0x0064F352
		public bool IsInValidUseTileEntity()
		{
			return this.GetTileEntity() != null;
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x00651160 File Offset: 0x0064F360
		public TileEntity GetTileEntity()
		{
			TileEntity tileEntity = null;
			if (this.InUse)
			{
				TileEntity.TryGet<TileEntity>(this.interactEntityID, out tileEntity);
			}
			return tileEntity;
		}

		// Token: 0x04005D50 RID: 23888
		public int interactEntityID;

		// Token: 0x04005D51 RID: 23889
		public int X;

		// Token: 0x04005D52 RID: 23890
		public int Y;
	}
}
