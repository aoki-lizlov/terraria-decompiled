using System;
using Terraria.DataStructures;
using Terraria.GameContent.LeashedEntities;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000410 RID: 1040
	public class TEKiteAnchor : TELeashedEntityAnchorWithItem
	{
		// Token: 0x06002FBA RID: 12218 RVA: 0x005B4FF2 File Offset: 0x005B31F2
		public TEKiteAnchor()
		{
			this.type = TEKiteAnchor._myEntityID;
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x005B5005 File Offset: 0x005B3205
		public override void RegisterTileEntityID(int assignedID)
		{
			this.type = (TEKiteAnchor._myEntityID = (byte)assignedID);
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x005B5018 File Offset: 0x005B3218
		public override bool IsTileValidForEntity(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile.active() && tile.type == 723;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x005B5049 File Offset: 0x005B3249
		public override TileEntity GenerateInstance()
		{
			return new TEKiteAnchor();
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x005B5050 File Offset: 0x005B3250
		public static void Kill(int x, int y)
		{
			TileEntity.Kill(x, y, (int)TEKiteAnchor._myEntityID);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x005B505E File Offset: 0x005B325E
		public static int Hook_AfterPlacement(int x, int y, int type, int style, int direction, int alternate)
		{
			return TELeashedEntityAnchorWithItem.PlaceFromPlayerPlacementHook(x, y, (int)TEKiteAnchor._myEntityID);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x005B506C File Offset: 0x005B326C
		public override bool FitsItem(int itemType)
		{
			return ItemID.Sets.IsAKite[itemType];
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x005B5075 File Offset: 0x005B3275
		public override LeashedEntity CreateLeashedEntity()
		{
			if (this.itemType <= 0)
			{
				return null;
			}
			LeashedKite leashedKite = (LeashedKite)LeashedKite.Prototype.NewInstance();
			leashedKite.SetDefaults(ContentSamples.ItemsByType[this.itemType].shoot);
			return leashedKite;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x005B50AC File Offset: 0x005B32AC
		public static void DebugPlace(int x, int y, int itemType)
		{
			int num = TileEntity.Place(x, y, (int)TEKiteAnchor._myEntityID);
			((TEKiteAnchor)TileEntity.ByID[num]).InsertItem(itemType);
			NetMessage.SendData(156, -1, -1, null, x, (float)y, (float)itemType, 0f, 0, 0, 0);
		}

		// Token: 0x04005695 RID: 22165
		private static byte _myEntityID;
	}
}
