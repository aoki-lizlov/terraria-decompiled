using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x0200041B RID: 1051
	public class TEItemFrame : TileEntityType<TEItemFrame>, IFixLoadedData
	{
		// Token: 0x0600304C RID: 12364 RVA: 0x005B95D4 File Offset: 0x005B77D4
		public TEItemFrame()
		{
			this.item = new Item();
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x005B95E7 File Offset: 0x005B77E7
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEItemFrame.ValidTile(x, y);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x005B95F0 File Offset: 0x005B77F0
		public static int Hook_AfterPlacement(int x, int y, int type = 395, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, 2, 2, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)TileEntityType<TEItemFrame>.EntityTypeID, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntityType<TEItemFrame>.Place(x, y);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x005B9638 File Offset: 0x005B7838
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 395 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 36 == 0;
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x005B969C File Offset: 0x005B789C
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.item.type);
			writer.Write(this.item.prefix);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x005B96D4 File Offset: 0x005B78D4
		public override void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x005B9720 File Offset: 0x005B7920
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

		// Token: 0x06003053 RID: 12371 RVA: 0x005B9778 File Offset: 0x005B7978
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 32, 32, this.item.type, 1, false, (int)this.item.prefix, false);
			}
			this.item = new Item();
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x005B97F4 File Offset: 0x005B79F4
		public static void TryPlacing(int x, int y, int type, int prefix, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 2, y + 2);
			TEItemFrame teitemFrame;
			if (!TileEntity.TryGetAt<TEItemFrame>(x, y, out teitemFrame))
			{
				int num = Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 32, 32, 1, 1, false, 0, false);
				Main.item[num].SetDefaults(type);
				Main.item[num].Prefix(prefix);
				Main.item[num].stack = stack;
				NetMessage.SendData(21, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (teitemFrame.item.stack > 0)
			{
				teitemFrame.DropItem();
			}
			teitemFrame.item = new Item();
			teitemFrame.item.SetDefaults(type, null);
			teitemFrame.item.Prefix(prefix);
			teitemFrame.item.stack = stack;
			NetMessage.SendData(86, -1, -1, null, teitemFrame.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x005B98E0 File Offset: 0x005B7AE0
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			if (TEItemFrame.FitsItemFrame(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
			{
				player.GamepadEnableGrappleCooldown();
				TEItemFrame.PlaceItemInFrame(player, clickX, clickY);
				return;
			}
			int num = clickX;
			int num2 = clickY;
			if (Main.tile[num, num2].frameX % 36 != 0)
			{
				num--;
			}
			if (Main.tile[num, num2].frameY % 36 != 0)
			{
				num2--;
			}
			TEItemFrame teitemFrame;
			if (TileEntity.TryGetAt<TEItemFrame>(num, num2, out teitemFrame) && teitemFrame.item.stack > 0)
			{
				player.GamepadEnableGrappleCooldown();
				WorldGen.KillTile(clickX, clickY, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)num, (float)num2, 1f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x005B4C44 File Offset: 0x005B2E44
		public static bool FitsItemFrame(Item i)
		{
			return i.stack > 0;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x005B99A4 File Offset: 0x005B7BA4
		public static void PlaceItemInFrame(Player player, int x, int y)
		{
			if (!player.ItemTimeIsZero)
			{
				return;
			}
			if (Main.tile[x, y].frameX % 36 != 0)
			{
				x--;
			}
			if (Main.tile[x, y].frameY % 36 != 0)
			{
				y--;
			}
			TEItemFrame teitemFrame;
			if (!TileEntity.TryGetAt<TEItemFrame>(x, y, out teitemFrame))
			{
				return;
			}
			if (teitemFrame.item.stack > 0)
			{
				WorldGen.KillTile(x, y, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)Player.tileTargetX, (float)y, 1f, 0, 0, 0);
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(89, -1, -1, null, x, (float)y, (float)player.selectedItem, (float)player.whoAmI, 1, 0, 0);
			}
			else
			{
				TEItemFrame.TryPlacing(x, y, player.inventory[player.selectedItem].type, (int)player.inventory[player.selectedItem].prefix, 1);
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
			WorldGen.RangeFrame(x, y, x + 2, y + 2);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x005B9B1E File Offset: 0x005B7D1E
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x040056D3 RID: 22227
		public Item item;
	}
}
