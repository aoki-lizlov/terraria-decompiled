using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200040D RID: 1037
	public class TEDeadCellsDisplayJar : TileEntityType<TEDeadCellsDisplayJar>, IFixLoadedData
	{
		// Token: 0x06002F9D RID: 12189 RVA: 0x005B4871 File Offset: 0x005B2A71
		public TEDeadCellsDisplayJar()
		{
			this.item = new Item();
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x005B4884 File Offset: 0x005B2A84
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEDeadCellsDisplayJar.ValidTile(x, y);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x005B4890 File Offset: 0x005B2A90
		public static int Hook_AfterPlacement(int x, int y, int type = 698, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, 2, 2, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)TileEntityType<TEDeadCellsDisplayJar>.EntityTypeID, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntityType<TEDeadCellsDisplayJar>.Place(x, y);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x005B48D8 File Offset: 0x005B2AD8
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 698 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 18 == 0;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x005B493C File Offset: 0x005B2B3C
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.item.type);
			writer.Write(this.item.prefix);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x005B4974 File Offset: 0x005B2B74
		public override void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x005B49C0 File Offset: 0x005B2BC0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y item: ",
				this.item
			});
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x005B4A18 File Offset: 0x005B2C18
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 32, 32, this.item.type, 1, false, (int)this.item.prefix, false);
			}
			this.item = new Item();
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x005B4A94 File Offset: 0x005B2C94
		public static void TryPlacing(int x, int y, int type, int prefix, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 1, y + 2);
			TEDeadCellsDisplayJar tedeadCellsDisplayJar;
			if (!TileEntity.TryGetAt<TEDeadCellsDisplayJar>(x, y, out tedeadCellsDisplayJar))
			{
				int num = Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 32, 32, 1, 1, false, 0, false);
				Main.item[num].SetDefaults(type);
				Main.item[num].Prefix(prefix);
				Main.item[num].stack = stack;
				NetMessage.SendData(21, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (tedeadCellsDisplayJar.item.stack > 0)
			{
				tedeadCellsDisplayJar.DropItem();
			}
			tedeadCellsDisplayJar.item = new Item();
			tedeadCellsDisplayJar.item.SetDefaults(type, null);
			tedeadCellsDisplayJar.item.Prefix(prefix);
			tedeadCellsDisplayJar.item.stack = stack;
			NetMessage.SendData(86, -1, -1, null, tedeadCellsDisplayJar.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x005B4B80 File Offset: 0x005B2D80
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			if (TEDeadCellsDisplayJar.FitsJar(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
			{
				player.GamepadEnableGrappleCooldown();
				TEDeadCellsDisplayJar.PlaceItemInJar(player, clickX, clickY);
				return;
			}
			int num = clickX;
			int num2 = clickY;
			if (Main.tile[num, num2].frameX % 18 != 0)
			{
				num--;
			}
			if (Main.tile[num, num2].frameY % 36 != 0)
			{
				num2--;
			}
			TEDeadCellsDisplayJar tedeadCellsDisplayJar;
			if (TileEntity.TryGetAt<TEDeadCellsDisplayJar>(num, num2, out tedeadCellsDisplayJar) && tedeadCellsDisplayJar.item.stack > 0)
			{
				player.GamepadEnableGrappleCooldown();
				WorldGen.KillTile(clickX, clickY, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)num, (float)num2, 1f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x005B4C44 File Offset: 0x005B2E44
		public static bool FitsJar(Item i)
		{
			return i.stack > 0;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x005B4C50 File Offset: 0x005B2E50
		public static void PlaceItemInJar(Player player, int x, int y)
		{
			if (!player.ItemTimeIsZero)
			{
				return;
			}
			if (Main.tile[x, y].frameX % 18 != 0)
			{
				x--;
			}
			if (Main.tile[x, y].frameY % 36 != 0)
			{
				y--;
			}
			TEDeadCellsDisplayJar tedeadCellsDisplayJar;
			if (!TileEntity.TryGetAt<TEDeadCellsDisplayJar>(x, y, out tedeadCellsDisplayJar))
			{
				return;
			}
			if (tedeadCellsDisplayJar.item.stack > 0)
			{
				WorldGen.KillTile(x, y, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)Player.tileTargetX, (float)y, 1f, 0, 0, 0);
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(149, -1, -1, null, x, (float)y, (float)player.selectedItem, (float)player.whoAmI, 1, 0, 0);
			}
			else
			{
				TEDeadCellsDisplayJar.TryPlacing(x, y, player.inventory[player.selectedItem].type, (int)player.inventory[player.selectedItem].prefix, 1);
			}
			player.inventory[player.selectedItem].stack--;
			if (player.inventory[player.selectedItem].stack <= 0)
			{
				player.inventory[player.selectedItem].SetDefaults(0, null);
				Main.mouseItem.SetDefaults(0, null);
			}
			if (player.selectedItem == 58)
			{
				Main.mouseItem = player.inventory[player.selectedItem].Clone();
			}
			player.releaseUseItem = false;
			player.mouseInterface = true;
			player.PlayDroppedItemAnimation(20);
			WorldGen.RangeFrame(x, y, x + 1, y + 2);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x005B4DCD File Offset: 0x005B2FCD
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x04005692 RID: 22162
		public Item item;
	}
}
