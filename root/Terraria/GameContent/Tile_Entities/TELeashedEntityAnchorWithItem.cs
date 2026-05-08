using System;
using System.IO;
using Terraria.DataStructures;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200040F RID: 1039
	public abstract class TELeashedEntityAnchorWithItem : TELeashedEntityAnchor
	{
		// Token: 0x06002FB2 RID: 12210 RVA: 0x005B4EA6 File Offset: 0x005B30A6
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.itemType);
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x005B4EB5 File Offset: 0x005B30B5
		public override void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
			this.itemType = (int)reader.ReadInt16();
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x005B4EC4 File Offset: 0x005B30C4
		public void DropItemForTileBreak()
		{
			if (this.itemType <= 0)
			{
				return;
			}
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 16, 16, this.itemType, 1, false, 0, false);
			}
			this.itemType = 0;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x005B4F35 File Offset: 0x005B3135
		public void InsertItem(int itemType)
		{
			this.itemType = itemType;
			base.RespawnLeashedEntity();
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x005B4F44 File Offset: 0x005B3144
		public override void OnWorldLoaded()
		{
			if (!this.FitsItem(this.itemType))
			{
				this.itemType = 0;
			}
			base.OnWorldLoaded();
		}

		// Token: 0x06002FB7 RID: 12215
		public abstract bool FitsItem(int itemType);

		// Token: 0x06002FB8 RID: 12216 RVA: 0x005B4F64 File Offset: 0x005B3164
		protected new static int PlaceFromPlayerPlacementHook(int x, int y, int type)
		{
			int num = TELeashedEntityAnchor.PlaceFromPlayerPlacementHook(x, y, type);
			Item heldItem = Main.LocalPlayer.HeldItem;
			int type2 = heldItem.type;
			if (!heldItem.consumable)
			{
				Item item = heldItem;
				int num2 = item.stack - 1;
				item.stack = num2;
				if (num2 <= 0)
				{
					heldItem.TurnToAir(false);
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(156, -1, -1, null, x, (float)y, (float)type2, 0f, 0, 0, 0);
			}
			else
			{
				((TELeashedEntityAnchorWithItem)TileEntity.ByID[num]).InsertItem(type2);
			}
			return num;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x005B4FEA File Offset: 0x005B31EA
		protected TELeashedEntityAnchorWithItem()
		{
		}

		// Token: 0x04005694 RID: 22164
		protected int itemType;
	}
}
