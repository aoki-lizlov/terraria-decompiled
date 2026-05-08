using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000412 RID: 1042
	public class TEFoodPlatter : TileEntityType<TEFoodPlatter>, IFixLoadedData
	{
		// Token: 0x06002FCE RID: 12238 RVA: 0x005B53E9 File Offset: 0x005B35E9
		public TEFoodPlatter()
		{
			this.item = new Item();
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x005B53FC File Offset: 0x005B35FC
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEFoodPlatter.ValidTile(x, y);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x005B5408 File Offset: 0x005B3608
		public static int Hook_AfterPlacement(int x, int y, int type = 520, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)TileEntityType<TEFoodPlatter>.EntityTypeID, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntityType<TEFoodPlatter>.Place(x, y);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x005B5450 File Offset: 0x005B3650
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 520 && Main.tile[x, y].frameY == 0;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x005B549E File Offset: 0x005B369E
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.item.type);
			writer.Write(this.item.prefix);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x005B54D8 File Offset: 0x005B36D8
		public override void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x005B5524 File Offset: 0x005B3724
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

		// Token: 0x06002FD5 RID: 12245 RVA: 0x005B557C File Offset: 0x005B377C
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 16, 16, this.item.type, 1, false, (int)this.item.prefix, false);
			}
			this.item = new Item();
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x005B55F8 File Offset: 0x005B37F8
		public static void TryPlacing(int x, int y, int type, int prefix, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 1, y + 1);
			TEFoodPlatter tefoodPlatter;
			if (!TileEntity.TryGetAt<TEFoodPlatter>(x, y, out tefoodPlatter))
			{
				int num = Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 16, 16, 1, 1, false, 0, false);
				Main.item[num].SetDefaults(type);
				Main.item[num].Prefix(prefix);
				Main.item[num].stack = stack;
				NetMessage.SendData(21, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (tefoodPlatter.item.stack > 0)
			{
				tefoodPlatter.DropItem();
			}
			tefoodPlatter.item = new Item();
			tefoodPlatter.item.SetDefaults(type, null);
			tefoodPlatter.item.Prefix(prefix);
			tefoodPlatter.item.stack = stack;
			NetMessage.SendData(86, -1, -1, null, tefoodPlatter.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x005B56E4 File Offset: 0x005B38E4
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			if (TEFoodPlatter.FitsFoodPlatter(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
			{
				player.GamepadEnableGrappleCooldown();
				TEFoodPlatter.PlaceItemInFrame(player, clickX, clickY);
				return;
			}
			TEFoodPlatter tefoodPlatter;
			if (TileEntity.TryGetAt<TEFoodPlatter>(clickX, clickY, out tefoodPlatter) && tefoodPlatter.item.stack > 0)
			{
				player.GamepadEnableGrappleCooldown();
				WorldGen.KillTile(clickX, clickY, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)clickX, (float)clickY, 1f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x005B5774 File Offset: 0x005B3974
		public static bool FitsFoodPlatter(Item i)
		{
			return i.stack > 0 && ItemID.Sets.IsFood[i.type];
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x005B5790 File Offset: 0x005B3990
		public static void PlaceItemInFrame(Player player, int x, int y)
		{
			if (!player.ItemTimeIsZero)
			{
				return;
			}
			TEFoodPlatter tefoodPlatter;
			if (!TileEntity.TryGetAt<TEFoodPlatter>(x, y, out tefoodPlatter))
			{
				return;
			}
			if (tefoodPlatter.item.stack > 0)
			{
				WorldGen.KillTile(x, y, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)Player.tileTargetX, (float)y, 1f, 0, 0, 0);
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(133, -1, -1, null, x, (float)y, (float)player.selectedItem, (float)player.whoAmI, 1, 0, 0);
			}
			else
			{
				TEFoodPlatter.TryPlacing(x, y, player.inventory[player.selectedItem].type, (int)player.inventory[player.selectedItem].prefix, 1);
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
			WorldGen.RangeFrame(x, y, x + 1, y + 1);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x005B58CF File Offset: 0x005B3ACF
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x04005698 RID: 22168
		public Item item;
	}
}
