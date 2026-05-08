using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000417 RID: 1047
	public class TEHatRack : TileEntityType<TEHatRack>, IFixLoadedData
	{
		// Token: 0x06003010 RID: 12304 RVA: 0x005B7880 File Offset: 0x005B5A80
		public TEHatRack()
		{
			this._items = new Item[2];
			for (int i = 0; i < this._items.Length; i++)
			{
				this._items[i] = new Item();
			}
			this._dyes = new Item[2];
			for (int j = 0; j < this._dyes.Length; j++)
			{
				this._dyes[j] = new Item();
			}
			this._dollPlayer = new Player();
			this._dollPlayer.hair = 15;
			this._dollPlayer.skinColor = Color.White;
			this._dollPlayer.skinVariant = 10;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x005B7920 File Offset: 0x005B5B20
		public static int Hook_AfterPlacement(int x, int y, int type = 475, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x - 1, y - 3, 3, 4, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x + -1, (float)(y + -3), (float)TileEntityType<TEHatRack>.EntityTypeID, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntityType<TEHatRack>.Place(x + -1, y + -3);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x005B7978 File Offset: 0x005B5B78
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			BitsByte bitsByte = 0;
			bitsByte[0] = !this._items[0].IsAir;
			bitsByte[1] = !this._items[1].IsAir;
			bitsByte[2] = !this._dyes[0].IsAir;
			bitsByte[3] = !this._dyes[1].IsAir;
			writer.Write(bitsByte);
			for (int i = 0; i < 2; i++)
			{
				Item item = this._items[i];
				if (!item.IsAir)
				{
					writer.Write((short)item.type);
					writer.Write(item.prefix);
					writer.Write((short)item.stack);
				}
			}
			for (int j = 0; j < 2; j++)
			{
				Item item2 = this._dyes[j];
				if (!item2.IsAir)
				{
					writer.Write((short)item2.type);
					writer.Write(item2.prefix);
					writer.Write((short)item2.stack);
				}
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x005B7A84 File Offset: 0x005B5C84
		public override void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
			BitsByte bitsByte = reader.ReadByte();
			for (int i = 0; i < 2; i++)
			{
				this._items[i] = new Item();
				Item item = this._items[i];
				if (bitsByte[i])
				{
					item.netDefaults((int)reader.ReadInt16());
					item.Prefix((int)reader.ReadByte());
					item.stack = (int)reader.ReadInt16();
				}
			}
			for (int j = 0; j < 2; j++)
			{
				this._dyes[j] = new Item();
				Item item2 = this._dyes[j];
				if (bitsByte[j + 2])
				{
					item2.netDefaults((int)reader.ReadInt16());
					item2.Prefix((int)reader.ReadByte());
					item2.stack = (int)reader.ReadInt16();
				}
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x005B7B48 File Offset: 0x005B5D48
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y item: ",
				this._items[0],
				" ",
				this._items[1]
			});
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x005B7BB4 File Offset: 0x005B5DB4
		public static void Framing_CheckTile(int callX, int callY)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num = callX - (int)(tileSafely.frameX / 18 % 3);
			int num2 = callY - (int)(tileSafely.frameY / 18 % 4);
			bool flag = false;
			for (int i = num; i < num + 3; i++)
			{
				for (int j = num2; j < num2 + 4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || tile.type != 475)
					{
						flag = true;
					}
				}
			}
			if (!WorldGen.SolidTileAllowBottomSlope(num, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 1, num2 + 4) || !WorldGen.SolidTileAllowBottomSlope(num + 2, num2 + 4))
			{
				flag = true;
			}
			if (flag)
			{
				TileEntityType<TEHatRack>.Kill(num, num2);
				Item.NewItem(new EntitySource_TileBreak(num, num2), num * 16, num2 * 16, 48, 64, 3977, 1, false, 0, false);
				WorldGen.destroyObject = true;
				for (int k = num; k < num + 3; k++)
				{
					for (int l = num2; l < num2 + 4; l++)
					{
						if (Main.tile[k, l].active() && Main.tile[k, l].type == 475)
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				WorldGen.destroyObject = false;
			}
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x005B7D04 File Offset: 0x005B5F04
		public void Draw(int tileLeftX, int tileTopY)
		{
			Player dollPlayer = this._dollPlayer;
			dollPlayer.direction = -1;
			dollPlayer.Male = true;
			if (Framing.GetTileSafely(tileLeftX, tileTopY).frameX % 216 == 54)
			{
				dollPlayer.direction = 1;
			}
			dollPlayer.isDisplayDollOrInanimate = true;
			dollPlayer.isHatRackDoll = true;
			dollPlayer.armor[0] = this._items[0];
			dollPlayer.dye[0] = this._dyes[0];
			dollPlayer.ResetEffects();
			dollPlayer.ResetVisibleAccessories();
			dollPlayer.invis = true;
			dollPlayer.UpdateDyes();
			dollPlayer.DisplayDollUpdate();
			dollPlayer.PlayerFrame();
			Vector2 vector = new Vector2((float)tileLeftX + 1.5f, (float)(tileTopY + 4)) * 16f;
			dollPlayer.direction *= -1;
			Vector2 vector2 = new Vector2((float)(-(float)dollPlayer.width / 2), (float)(-(float)dollPlayer.height - 6)) + new Vector2((float)(dollPlayer.direction * 14), -2f);
			dollPlayer.position = vector + vector2;
			Main.PlayerRenderer.DrawPlayer(Main.Camera, dollPlayer, dollPlayer.position, 0f, dollPlayer.fullRotationOrigin, 0f, 1f);
			dollPlayer.armor[0] = this._items[1];
			dollPlayer.dye[0] = this._dyes[1];
			dollPlayer.ResetEffects();
			dollPlayer.ResetVisibleAccessories();
			dollPlayer.invis = true;
			dollPlayer.UpdateDyes();
			dollPlayer.DisplayDollUpdate();
			dollPlayer.skipAnimatingValuesInPlayerFrame = true;
			dollPlayer.PlayerFrame();
			dollPlayer.skipAnimatingValuesInPlayerFrame = false;
			dollPlayer.direction *= -1;
			vector2 = new Vector2((float)(-(float)dollPlayer.width / 2), (float)(-(float)dollPlayer.height - 6)) + new Vector2((float)(dollPlayer.direction * 12), 16f);
			dollPlayer.position = vector + vector2;
			Main.PlayerRenderer.DrawPlayer(Main.Camera, dollPlayer, dollPlayer.position, 0f, dollPlayer.fullRotationOrigin, 0f, 1f);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x005B7EF8 File Offset: 0x005B60F8
		public string GetItemGamepadInstructions(int slot = 0)
		{
			Item[] array = this._items;
			int num = slot;
			int num2 = 26;
			if (slot >= 2)
			{
				num -= 2;
				array = this._dyes;
				num2 = 27;
			}
			return ItemSlot.GetGamepadInstructions(array, num2, num);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x005B7F2C File Offset: 0x005B612C
		public override void OnPlayerUpdate(Player player)
		{
			if (!player.InTileEntityInteractionRange(player.tileEntityAnchor.X, player.tileEntityAnchor.Y, 3, 4, TileReachCheckSettings.Simple) || player.chest != -1 || player.talkNPC != -1)
			{
				if (player.chest == -1 && player.talkNPC == -1)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				}
				player.tileEntityAnchor.Clear();
			}
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x005B7FA4 File Offset: 0x005B61A4
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			int num = clickX - (int)(Main.tile[clickX, clickY].frameX % 54 / 18);
			int num2 = clickY - (int)(Main.tile[num, clickY].frameY / 18);
			int num3 = TileEntityType<TEHatRack>.Find(num, num2);
			if (num3 != -1)
			{
				num2++;
				num++;
				TEHatRack.hatTargetSlot = 0;
				TileEntity.BasicOpenCloseInteraction(player, num, num2, num3);
			}
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x005B800C File Offset: 0x005B620C
		public override void OnInventoryDraw(Player player, SpriteBatch spriteBatch)
		{
			if (Main.tile[player.tileEntityAnchor.X, player.tileEntityAnchor.Y].type != 475)
			{
				player.tileEntityAnchor.Clear();
				return;
			}
			this.DrawInner(player, spriteBatch);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x005B8059 File Offset: 0x005B6259
		private void DrawInner(Player player, SpriteBatch spriteBatch)
		{
			Main.inventoryScale = 0.72f;
			this.DrawSlotPairSet(player, spriteBatch, 2, 0, 3.5f, 0.5f, 26);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x005B807C File Offset: 0x005B627C
		private void DrawSlotPairSet(Player player, SpriteBatch spriteBatch, int slotsToShowLine, int slotsArrayOffset, float offsetX, float offsetY, int inventoryContextTarget)
		{
			Item[] array = this._items;
			for (int i = 0; i < slotsToShowLine; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int num = (int)(73f + ((float)i + offsetX) * 56f * Main.inventoryScale);
					int num2 = (int)((float)Main.instance.invBottom + ((float)j + offsetY) * 56f * Main.inventoryScale);
					int num3;
					if (j == 0)
					{
						array = this._items;
						num3 = inventoryContextTarget;
					}
					else
					{
						array = this._dyes;
						num3 = 27;
					}
					if (Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, (float)num, (float)num2, (float)TextureAssets.InventoryBack.Width() * Main.inventoryScale, (float)TextureAssets.InventoryBack.Height() * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
					{
						player.mouseInterface = true;
						ItemSlot.Handle(array, num3, i + slotsArrayOffset, true);
					}
					ItemSlot.Draw(spriteBatch, array, num3, i + slotsArrayOffset, new Vector2((float)num, (float)num2), default(Color));
				}
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x005B818C File Offset: 0x005B638C
		public override ItemSlot.AlternateClickAction? GetShiftClickAction(Item[] inv, int context = 0, int slot = 0)
		{
			Item item = inv[slot];
			if (context == 0 && TEHatRack.CanQuickSwapIntoHatRack(item))
			{
				return new ItemSlot.AlternateClickAction?(ItemSlot.AlternateClickAction.TransferToChest);
			}
			if ((context == 26 || context == 27) && Main.LocalPlayer.ItemSpace(item).CanTakeItemToPersonalInventory)
			{
				return new ItemSlot.AlternateClickAction?(ItemSlot.AlternateClickAction.TransferFromChest);
			}
			return null;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x005B81E8 File Offset: 0x005B63E8
		public override bool PerformShiftClickAction(Item[] inv, int context = 0, int slot = 0)
		{
			Item item = inv[slot];
			if (Main.cursorOverride == 9 && context == 0)
			{
				if (Main.cursorOverride == 9 && !item.IsAir && !item.favorited && context == 0 && TEHatRack.CanQuickSwapIntoHatRack(item))
				{
					return this.TryFitting(inv, slot);
				}
			}
			else if (Main.cursorOverride == 8 && (context == 26 || context == 27))
			{
				inv[slot] = Main.LocalPlayer.GetItem(inv[slot], GetItemSettings.QuickTransferFromSlot);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)this.ID, (float)slot, 0f, 0, 0, 0);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x005B8284 File Offset: 0x005B6484
		public static bool CanQuickSwapIntoHatRack(Item item)
		{
			return item.headSlot > 0;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x005B8290 File Offset: 0x005B6490
		private bool TryFitting(Item[] inv, int slot)
		{
			Item item = inv[slot];
			int num = -1;
			for (int i = 0; i < this._items.Length; i++)
			{
				if (this._items[i].IsAir)
				{
					num = i;
					TEHatRack.hatTargetSlot = i;
					break;
				}
			}
			if (num == -1)
			{
				num = TEHatRack.hatTargetSlot;
			}
			if (item.stack > 1 && !this._items[num].IsAir)
			{
				return true;
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			if (item.stack > 1)
			{
				this._items[num] = item.Clone();
				this._items[num].stack = 1;
				item.stack--;
			}
			else
			{
				Utils.Swap<Item>(ref this._items[num], ref inv[slot]);
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(124, -1, -1, null, Main.myPlayer, (float)this.ID, (float)num, 0f, 0, 0, 0);
			}
			TEHatRack.hatTargetSlot++;
			if (TEHatRack.hatTargetSlot >= this._items.Length)
			{
				TEHatRack.hatTargetSlot = 0;
			}
			return true;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x005B83A0 File Offset: 0x005B65A0
		public void WriteItem(int itemIndex, BinaryWriter writer, bool dye)
		{
			Item item = this._items[itemIndex];
			if (dye)
			{
				item = this._dyes[itemIndex];
			}
			writer.Write((ushort)item.type);
			writer.Write((ushort)item.stack);
			writer.Write(item.prefix);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x005B83E8 File Offset: 0x005B65E8
		public void ReadItem(int itemIndex, BinaryReader reader, bool dye)
		{
			int num = (int)reader.ReadUInt16();
			int num2 = (int)reader.ReadUInt16();
			int num3 = (int)reader.ReadByte();
			Item item = this._items[itemIndex];
			if (dye)
			{
				item = this._dyes[itemIndex];
			}
			item.SetDefaults(num, null);
			item.stack = num2;
			item.Prefix(num3);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x005B8438 File Offset: 0x005B6638
		public override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 475 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 54 == 0;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x005B849C File Offset: 0x005B669C
		public static bool IsBreakable(int clickX, int clickY)
		{
			int num = clickX - (int)(Main.tile[clickX, clickY].frameX % 54 / 18);
			int num2 = clickY - (int)(Main.tile[num, clickY].frameY / 18);
			TEHatRack tehatRack;
			return !TileEntity.TryGetAt<TEHatRack>(num, num2, out tehatRack) || !tehatRack.ContainsItems();
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x005B84F4 File Offset: 0x005B66F4
		public bool ContainsItems()
		{
			for (int i = 0; i < 2; i++)
			{
				if (!this._items[i].IsAir || !this._dyes[i].IsAir)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x005B8530 File Offset: 0x005B6730
		public void FixLoadedData()
		{
			Item[] array = this._items;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FixAgainstExploit();
			}
			array = this._dyes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FixAgainstExploit();
			}
		}

		// Token: 0x040056BF RID: 22207
		private const int MyTileID = 475;

		// Token: 0x040056C0 RID: 22208
		public const int entityTileWidth = 3;

		// Token: 0x040056C1 RID: 22209
		public const int entityTileHeight = 4;

		// Token: 0x040056C2 RID: 22210
		private Player _dollPlayer;

		// Token: 0x040056C3 RID: 22211
		private Item[] _items;

		// Token: 0x040056C4 RID: 22212
		private Item[] _dyes;

		// Token: 0x040056C5 RID: 22213
		private static int hatTargetSlot;
	}
}
