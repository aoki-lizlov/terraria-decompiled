using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000419 RID: 1049
	public class TEWeaponsRack : TileEntityType<TEWeaponsRack>, IFixLoadedData
	{
		// Token: 0x06003027 RID: 12327 RVA: 0x005B8577 File Offset: 0x005B6777
		public TEWeaponsRack()
		{
			this.item = new Item();
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x005B858A File Offset: 0x005B678A
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TEWeaponsRack.NetPlaceEntity(x, y);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x005B8594 File Offset: 0x005B6794
		public static void NetPlaceEntity(int x, int y)
		{
			int num = TileEntityType<TEWeaponsRack>.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, num, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x005B85C0 File Offset: 0x005B67C0
		public override bool IsTileValidForEntity(int x, int y)
		{
			return TEWeaponsRack.ValidTile(x, y);
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x005B85CC File Offset: 0x005B67CC
		public static bool ValidTile(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 471 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 54 == 0;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x005B8630 File Offset: 0x005B6830
		public static int Hook_AfterPlacement(int x, int y, int type = 471, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y, 3, 3, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)y, (float)TileEntityType<TEWeaponsRack>.EntityTypeID, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntityType<TEWeaponsRack>.Place(x, y);
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x005B8678 File Offset: 0x005B6878
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			writer.Write((short)this.item.type);
			writer.Write(this.item.prefix);
			writer.Write((short)this.item.stack);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x005B86B0 File Offset: 0x005B68B0
		public override void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
			this.item = new Item();
			this.item.netDefaults((int)reader.ReadInt16());
			this.item.Prefix((int)reader.ReadByte());
			this.item.stack = (int)reader.ReadInt16();
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x005B86FC File Offset: 0x005B68FC
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

		// Token: 0x06003030 RID: 12336 RVA: 0x005B8754 File Offset: 0x005B6954
		public static void Framing_CheckTile(int callX, int callY)
		{
			int num = 3;
			int num2 = 3;
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num3 = callX - (int)(tileSafely.frameX / 18) % num;
			int num4 = callY - (int)(tileSafely.frameY / 18) % num2;
			bool flag = false;
			for (int i = num3; i < num3 + num; i++)
			{
				for (int j = num4; j < num4 + num2; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || tile.type != 471 || tile.wall == 0)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				TEWeaponsRack teweaponsRack;
				if (TileEntity.TryGetAt<TEWeaponsRack>(num3, num4, out teweaponsRack) && teweaponsRack.item.stack > 0)
				{
					teweaponsRack.DropItem();
					if (Main.netMode != 2)
					{
						Main.LocalPlayer.InterruptItemUsageIfOverTile(471);
					}
				}
				WorldGen.destroyObject = true;
				for (int k = num3; k < num3 + num; k++)
				{
					for (int l = num4; l < num4 + num2; l++)
					{
						if (Main.tile[k, l].active() && Main.tile[k, l].type == 471)
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				Item.NewItem(new EntitySource_TileBreak(num3, num4), num3 * 16, num4 * 16, 48, 48, 2699, 1, false, 0, false);
				TileEntityType<TEWeaponsRack>.Kill(num3, num4);
				WorldGen.destroyObject = false;
			}
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x005B88C8 File Offset: 0x005B6AC8
		public void DropItem()
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(new EntitySource_TileBreak((int)this.Position.X, (int)this.Position.Y), (int)(this.Position.X * 16), (int)(this.Position.Y * 16), 32, 32, this.item.type, 1, false, (int)this.item.prefix, false);
			}
			this.item = new Item();
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x005B8944 File Offset: 0x005B6B44
		public static void TryPlacing(int x, int y, int type, int prefix, int stack)
		{
			WorldGen.RangeFrame(x, y, x + 3, y + 3);
			TEWeaponsRack teweaponsRack;
			if (!TileEntity.TryGetAt<TEWeaponsRack>(x, y, out teweaponsRack))
			{
				int num = Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 32, 32, 1, 1, false, 0, false);
				Main.item[num].SetDefaults(type);
				Main.item[num].Prefix(prefix);
				Main.item[num].stack = stack;
				NetMessage.SendData(21, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (teweaponsRack.item.stack > 0)
			{
				teweaponsRack.DropItem();
			}
			teweaponsRack.item = new Item();
			teweaponsRack.item.SetDefaults(type, null);
			teweaponsRack.item.Prefix(prefix);
			teweaponsRack.item.stack = stack;
			NetMessage.SendData(86, -1, -1, null, teweaponsRack.ID, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x005B8A30 File Offset: 0x005B6C30
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			if (TEWeaponsRack.FitsWeaponFrame(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
			{
				player.GamepadEnableGrappleCooldown();
				TEWeaponsRack.PlaceItemInFrame(player, clickX, clickY);
				return;
			}
			int num = clickX - (int)(Main.tile[clickX, clickY].frameX % 54 / 18);
			int num2 = clickY - (int)(Main.tile[num, clickY].frameY % 54 / 18);
			TEWeaponsRack teweaponsRack;
			if (TileEntity.TryGetAt<TEWeaponsRack>(num, num2, out teweaponsRack) && teweaponsRack.item.stack > 0)
			{
				player.GamepadEnableGrappleCooldown();
				WorldGen.KillTile(num, num2, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)num, (float)num2, 1f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x005B8AF4 File Offset: 0x005B6CF4
		public static bool FitsWeaponFrame(Item i)
		{
			return (!i.IsAir && (i.fishingPole > 0 || ItemID.Sets.CanBePlacedOnWeaponRacks[i.type])) || (i.damage > 0 && i.useStyle != 0 && i.stack > 0);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x005B8B34 File Offset: 0x005B6D34
		private static void PlaceItemInFrame(Player player, int x, int y)
		{
			if (!player.ItemTimeIsZero)
			{
				return;
			}
			x -= (int)(Main.tile[x, y].frameX % 54 / 18);
			y -= (int)(Main.tile[x, y].frameY % 54 / 18);
			TEWeaponsRack teweaponsRack;
			if (!TileEntity.TryGetAt<TEWeaponsRack>(x, y, out teweaponsRack))
			{
				return;
			}
			if (teweaponsRack.item.stack > 0)
			{
				WorldGen.KillTile(x, y, true, false, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)Player.tileTargetX, (float)y, 1f, 0, 0, 0);
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(123, -1, -1, null, x, (float)y, (float)player.selectedItem, (float)player.whoAmI, 1, 0, 0);
			}
			else
			{
				TEWeaponsRack.TryPlacing(x, y, player.inventory[player.selectedItem].type, (int)player.inventory[player.selectedItem].prefix, 1);
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
			WorldGen.RangeFrame(x, y, x + 3, y + 3);
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x005B8CAE File Offset: 0x005B6EAE
		public void FixLoadedData()
		{
			this.item.FixAgainstExploit();
		}

		// Token: 0x040056C9 RID: 22217
		public Item item;

		// Token: 0x040056CA RID: 22218
		private const int MyTileID = 471;
	}
}
