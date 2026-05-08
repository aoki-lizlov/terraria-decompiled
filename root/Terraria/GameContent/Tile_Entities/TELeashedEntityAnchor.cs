using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200040E RID: 1038
	public abstract class TELeashedEntityAnchor : TileEntity
	{
		// Token: 0x06002FAA RID: 12202 RVA: 0x005B4DDC File Offset: 0x005B2FDC
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			int num = TileEntity.Place(x, y, (int)this.type);
			NetMessage.SendData(86, -1, -1, null, num, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x005B4E0E File Offset: 0x005B300E
		public override void OnRemoved()
		{
			this.DespawnLeashedEntity();
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x005B4E18 File Offset: 0x005B3018
		protected static int PlaceFromPlayerPlacementHook(int x, int y, int type)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)type, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntity.Place(x, y, type);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x005B4E5B File Offset: 0x005B305B
		public override void OnWorldLoaded()
		{
			this.RespawnLeashedEntity();
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x005B4E63 File Offset: 0x005B3063
		protected void DespawnLeashedEntity()
		{
			if (this.leashedEntity != null)
			{
				this.leashedEntity.active = false;
			}
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x005B4E79 File Offset: 0x005B3079
		protected void RespawnLeashedEntity()
		{
			this.DespawnLeashedEntity();
			this.leashedEntity = this.CreateLeashedEntity();
			LeashedEntity.AddNewEntity(this.leashedEntity, this.Position);
		}

		// Token: 0x06002FB0 RID: 12208
		public abstract LeashedEntity CreateLeashedEntity();

		// Token: 0x06002FB1 RID: 12209 RVA: 0x005B4E9E File Offset: 0x005B309E
		protected TELeashedEntityAnchor()
		{
		}

		// Token: 0x04005693 RID: 22163
		private LeashedEntity leashedEntity;
	}
}
