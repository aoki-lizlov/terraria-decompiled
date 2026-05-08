using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000415 RID: 1045
	public class TEDisplayDoll : TileEntityType<TEDisplayDoll>, IFixLoadedData
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x005B5CF6 File Offset: 0x005B3EF6
		public Item[] Equipment
		{
			get
			{
				return this._equip;
			}
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x005B5D00 File Offset: 0x005B3F00
		static TEDisplayDoll()
		{
			TEDisplayDoll.SupportedUseStylePoses.Clear();
			TEDisplayDoll.RegisterUsePose(1, DisplayDollPoseID.Use1, 1f, null);
			TEDisplayDoll.RegisterUsePose(1, DisplayDollPoseID.Use2, 0.8f, null);
			TEDisplayDoll.RegisterUsePose(1, DisplayDollPoseID.Use3, 0.6f, null);
			TEDisplayDoll.RegisterUsePose(1, DisplayDollPoseID.Use4, 0.4143f, null);
			TEDisplayDoll.RegisterUsePose(1, DisplayDollPoseID.Use5, 0.2f, null);
			TEDisplayDoll.RegisterUsePose(7, DisplayDollPoseID.Use1, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(3, DisplayDollPoseID.Use1, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(4, DisplayDollPoseID.Use1, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(5, DisplayDollPoseID.Use1, 0.5f, new float?(-1.5707964f));
			TEDisplayDoll.RegisterUsePose(5, DisplayDollPoseID.Use2, 0.5f, new float?(-0.7853982f));
			TEDisplayDoll.RegisterUsePose(5, DisplayDollPoseID.Use3, 0.5f, new float?(0f));
			TEDisplayDoll.RegisterUsePose(5, DisplayDollPoseID.Use4, 0.5f, new float?(0.7853981f));
			TEDisplayDoll.RegisterUsePose(5, DisplayDollPoseID.Use5, 0.5f, new float?(1.5707964f));
			TEDisplayDoll.RegisterUsePose(6, DisplayDollPoseID.Use1, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(2, DisplayDollPoseID.Use1, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(8, DisplayDollPoseID.Use1, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(9, DisplayDollPoseID.Use1, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(11, DisplayDollPoseID.Use1, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(12, DisplayDollPoseID.Use1, 0.75f, null);
			TEDisplayDoll.RegisterUsePose(12, DisplayDollPoseID.Use2, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(12, DisplayDollPoseID.Use3, 0.25f, null);
			TEDisplayDoll.RegisterUsePose(13, DisplayDollPoseID.Use1, 0.5f, new float?(-1.5707964f));
			TEDisplayDoll.RegisterUsePose(13, DisplayDollPoseID.Use2, 0.5f, new float?(-0.7853982f));
			TEDisplayDoll.RegisterUsePose(13, DisplayDollPoseID.Use3, 0.5f, new float?(0f));
			TEDisplayDoll.RegisterUsePose(13, DisplayDollPoseID.Use4, 0.5f, new float?(0.7853981f));
			TEDisplayDoll.RegisterUsePose(13, DisplayDollPoseID.Use5, 0.5f, new float?(1.5707964f));
			TEDisplayDoll.RegisterUsePose(14, DisplayDollPoseID.Use1, 0.5f, null);
			TEDisplayDoll.RegisterUsePose(15, DisplayDollPoseID.Use1, 0.5f, null);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x005B5F98 File Offset: 0x005B4198
		private static void RegisterUsePose(int useStyle, DisplayDollPoseID pose, float usePercent, float? useAim = null)
		{
			List<TEDisplayDoll.DisplayDollPose> list;
			if (!TEDisplayDoll.SupportedUseStylePoses.TryGetValue(useStyle, out list))
			{
				list = new List<TEDisplayDoll.DisplayDollPose>();
				TEDisplayDoll.SupportedUseStylePoses[useStyle] = list;
			}
			list.Add(new TEDisplayDoll.DisplayDollPose
			{
				Pose = pose,
				ItemAnimationPercent = usePercent,
				ItemAimRadians = useAim
			});
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x005B5FF0 File Offset: 0x005B41F0
		public TEDisplayDoll()
		{
			this._equip = new Item[9];
			for (int i = 0; i < this._equip.Length; i++)
			{
				this._equip[i] = new Item();
			}
			this._dyes = new Item[9];
			for (int j = 0; j < this._dyes.Length; j++)
			{
				this._dyes[j] = new Item();
			}
			this._misc = new Item[1];
			for (int k = 0; k < this._misc.Length; k++)
			{
				this._misc[k] = new Item();
			}
			this._dollPlayer = new Player();
			this._dollPlayer.hair = 15;
			this._dollPlayer.skinColor = Color.White;
			this._dollPlayer.skinVariant = 10;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x005B60C0 File Offset: 0x005B42C0
		public static int Hook_AfterPlacement(int x, int y, int type = 470, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x, y - 2, 2, 3, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x, (float)(y - 2), (float)TileEntityType<TEDisplayDoll>.EntityTypeID, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntityType<TEDisplayDoll>.Place(x, y - 2);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x005B6110 File Offset: 0x005B4310
		private bool IsValidPose(int testedPose)
		{
			bool flag = false;
			if (testedPose <= 3)
			{
				flag = true;
			}
			Item item = this._misc[0];
			List<TEDisplayDoll.DisplayDollPose> list;
			if (!flag && item != null && !item.IsAir && TEDisplayDoll.SupportedUseStylePoses.TryGetValue(item.useStyle, out list))
			{
				foreach (TEDisplayDoll.DisplayDollPose displayDollPose in list)
				{
					if ((DisplayDollPoseID)this._pose == displayDollPose.Pose)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x005B61A4 File Offset: 0x005B43A4
		public override void WriteExtraData(BinaryWriter writer, bool networkSend)
		{
			BitsByte bitsByte = 0;
			bitsByte[0] = !this._equip[0].IsAir;
			bitsByte[1] = !this._equip[1].IsAir;
			bitsByte[2] = !this._equip[2].IsAir;
			bitsByte[3] = !this._equip[3].IsAir;
			bitsByte[4] = !this._equip[4].IsAir;
			bitsByte[5] = !this._equip[5].IsAir;
			bitsByte[6] = !this._equip[6].IsAir;
			bitsByte[7] = !this._equip[7].IsAir;
			BitsByte bitsByte2 = 0;
			bitsByte2[0] = !this._dyes[0].IsAir;
			bitsByte2[1] = !this._dyes[1].IsAir;
			bitsByte2[2] = !this._dyes[2].IsAir;
			bitsByte2[3] = !this._dyes[3].IsAir;
			bitsByte2[4] = !this._dyes[4].IsAir;
			bitsByte2[5] = !this._dyes[5].IsAir;
			bitsByte2[6] = !this._dyes[6].IsAir;
			bitsByte2[7] = !this._dyes[7].IsAir;
			BitsByte bitsByte3 = 0;
			bitsByte3[0] = !this._misc[0].IsAir;
			bitsByte3[1] = !this._equip[8].IsAir;
			bitsByte3[2] = !this._dyes[8].IsAir;
			writer.Write(bitsByte);
			writer.Write(bitsByte2);
			writer.Write(this._pose);
			writer.Write(bitsByte3);
			foreach (Item item in this._equip)
			{
				if (!item.IsAir)
				{
					writer.Write((short)item.type);
					writer.Write(item.prefix);
					writer.Write((short)item.stack);
				}
			}
			foreach (Item item2 in this._dyes)
			{
				if (!item2.IsAir)
				{
					writer.Write((short)item2.type);
					writer.Write(item2.prefix);
					writer.Write((short)item2.stack);
				}
			}
			foreach (Item item3 in this._misc)
			{
				if (!item3.IsAir)
				{
					writer.Write((short)item3.type);
					writer.Write(item3.prefix);
					writer.Write((short)item3.stack);
				}
			}
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x005B64B4 File Offset: 0x005B46B4
		public override void ReadExtraData(BinaryReader reader, int gameVersion, bool networkSend)
		{
			BitsByte bitsByte = reader.ReadByte();
			BitsByte bitsByte2 = reader.ReadByte();
			if (gameVersion >= 307)
			{
				this._pose = reader.ReadByte();
			}
			BitsByte bitsByte3 = 0;
			if (gameVersion >= 308)
			{
				bitsByte3 = reader.ReadByte();
			}
			bool flag = false;
			if (gameVersion == 311)
			{
				flag = bitsByte3[1];
				bitsByte3[1] = false;
			}
			int num = (int)bitsByte | (bitsByte3[1] ? 256 : 0);
			for (int i = 0; i < this._equip.Length; i++)
			{
				this._equip[i] = new Item();
				Item item = this._equip[i];
				if ((num & (1 << i)) != 0)
				{
					item.netDefaults((int)reader.ReadInt16());
					item.Prefix((int)reader.ReadByte());
					item.stack = (int)reader.ReadInt16();
				}
			}
			long num2 = (long)((int)bitsByte2 | (bitsByte3[2] ? 256 : 0));
			for (int j = 0; j < this._dyes.Length; j++)
			{
				this._dyes[j] = new Item();
				Item item2 = this._dyes[j];
				if ((num2 & (1L << (j & 31))) != 0L)
				{
					item2.netDefaults((int)reader.ReadInt16());
					item2.Prefix((int)reader.ReadByte());
					item2.stack = (int)reader.ReadInt16();
				}
			}
			for (int k = 0; k < this._misc.Length; k++)
			{
				this._misc[k] = new Item();
				Item item3 = this._misc[k];
				if (bitsByte3[k])
				{
					item3.netDefaults((int)reader.ReadInt16());
					item3.Prefix((int)reader.ReadByte());
					item3.stack = (int)reader.ReadInt16();
				}
			}
			if (flag)
			{
				Item item4 = this._equip[8];
				item4.netDefaults((int)reader.ReadInt16());
				item4.Prefix((int)reader.ReadByte());
				item4.stack = (int)reader.ReadInt16();
			}
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x005B66B4 File Offset: 0x005B48B4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y item: ",
				this._equip[0],
				" ",
				this._equip[1],
				" ",
				this._equip[2]
			});
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x005B6734 File Offset: 0x005B4934
		public static void Framing_CheckTile(int callX, int callY)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num = callX - (int)(tileSafely.frameX / 18 % 2);
			int num2 = callY - (int)(tileSafely.frameY / 18 % 3);
			bool flag = false;
			for (int i = num; i < num + 2; i++)
			{
				for (int j = num2; j < num2 + 3; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active() || tile.type != 470)
					{
						flag = true;
					}
				}
			}
			if (!WorldGen.SolidTileAllowBottomSlope(num, num2 + 3) || !WorldGen.SolidTileAllowBottomSlope(num + 1, num2 + 3))
			{
				flag = true;
			}
			if (flag)
			{
				TileEntityType<TEDisplayDoll>.Kill(num, num2);
				if (Main.tile[callX, callY].frameX / 72 != 1)
				{
					Item.NewItem(new EntitySource_TileBreak(num, num2), num * 16, num2 * 16, 32, 48, 498, 1, false, 0, false);
				}
				else
				{
					Item.NewItem(new EntitySource_TileBreak(num, num2), num * 16, num2 * 16, 32, 48, 1989, 1, false, 0, false);
				}
				WorldGen.destroyObject = true;
				for (int k = num; k < num + 2; k++)
				{
					for (int l = num2; l < num2 + 3; l++)
					{
						if (Main.tile[k, l].active() && Main.tile[k, l].type == 470)
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				WorldGen.destroyObject = false;
			}
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x005B68B0 File Offset: 0x005B4AB0
		public void Draw(int tileLeftX, int tileTopY)
		{
			Player dollPlayer = this._dollPlayer;
			for (int i = 0; i < 8; i++)
			{
				dollPlayer.armor[i] = this._equip[i];
				dollPlayer.dye[i] = this._dyes[i];
			}
			Item item = this._misc[0];
			dollPlayer.inventory[0] = item;
			dollPlayer.direction = -1;
			dollPlayer.Male = true;
			Tile tileSafely = Framing.GetTileSafely(tileLeftX, tileTopY);
			if (tileSafely.frameX % 72 == 36)
			{
				dollPlayer.direction = 1;
			}
			if (tileSafely.frameX / 72 == 1)
			{
				dollPlayer.Male = false;
			}
			dollPlayer.isDisplayDollOrInanimate = true;
			dollPlayer.ResetEffects();
			dollPlayer.ResetVisibleAccessories();
			dollPlayer.UpdateDyes();
			dollPlayer.DisplayDollUpdate();
			dollPlayer.UpdateSocialShadow();
			dollPlayer.bodyFrameCounter = 0.0;
			dollPlayer.headFrameCounter = 0.0;
			dollPlayer.legFrameCounter = 0.0;
			dollPlayer.wingFrameCounter = 0;
			dollPlayer.sitting.isSitting = false;
			dollPlayer.itemAnimationMax = 0;
			dollPlayer.itemAnimation = 0;
			Item item2 = this._equip[8];
			int num = -1;
			if (!item2.IsAir)
			{
				num = item2.mountType;
			}
			if (dollPlayer.mount.Type != num)
			{
				if (num == -1)
				{
					dollPlayer.mount.Dismount(dollPlayer, false);
				}
				else
				{
					dollPlayer.mount.SetMount(num, dollPlayer);
				}
			}
			dollPlayer.miscDyes[3] = this._dyes[8];
			dollPlayer.miscDyes[2] = this._dyes[8];
			int num2 = 0;
			DisplayDollPoseID displayDollPoseID = (DisplayDollPoseID)this._pose;
			if (!this.IsValidPose((int)this._pose))
			{
				displayDollPoseID = DisplayDollPoseID.Standing;
			}
			if (num != -1)
			{
				dollPlayer.mount.ApplyDummyFrameCounters();
				if (displayDollPoseID == DisplayDollPoseID.Sitting || displayDollPoseID == DisplayDollPoseID.Jumping)
				{
					displayDollPoseID = DisplayDollPoseID.Standing;
				}
			}
			switch (displayDollPoseID)
			{
			case DisplayDollPoseID.Standing:
				dollPlayer.velocity = Vector2.Zero;
				break;
			case DisplayDollPoseID.Sitting:
				dollPlayer.velocity = Vector2.Zero;
				dollPlayer.sitting.isSitting = true;
				num2 = 14;
				break;
			case DisplayDollPoseID.Jumping:
				dollPlayer.velocity = Vector2.UnitY;
				break;
			case DisplayDollPoseID.Walking:
				dollPlayer.velocity = Vector2.UnitX * (float)dollPlayer.direction;
				dollPlayer.legFrame.Y = dollPlayer.legFrame.Height * 9;
				dollPlayer.bodyFrame.Y = dollPlayer.legFrame.Y;
				break;
			default:
			{
				dollPlayer.velocity = Vector2.Zero;
				List<TEDisplayDoll.DisplayDollPose> list;
				if (TEDisplayDoll.SupportedUseStylePoses.TryGetValue(item.useStyle, out list))
				{
					foreach (TEDisplayDoll.DisplayDollPose displayDollPose in list)
					{
						if ((DisplayDollPoseID)this._pose == displayDollPose.Pose)
						{
							dollPlayer.itemAnimationMax = 1000;
							dollPlayer.itemAnimation = (int)(1000f * displayDollPose.ItemAnimationPercent);
							dollPlayer.itemRotation = 0f;
							float? num3 = displayDollPose.ItemAimRadians;
							if (num3 == null)
							{
								break;
							}
							Player player = dollPlayer;
							num3 = displayDollPose.ItemAimRadians;
							player.itemRotation = num3.Value;
							if (dollPlayer.direction == -1)
							{
								dollPlayer.itemRotation *= -1f;
								break;
							}
							break;
						}
					}
				}
				break;
			}
			}
			dollPlayer.PlayerFrame();
			Vector2 vector = new Vector2((float)(tileLeftX + 1), (float)(tileTopY + 3)) * 16f + new Vector2((float)(-(float)dollPlayer.width / 2), (float)(-(float)dollPlayer.height - 6 + num2));
			dollPlayer.position = vector;
			dollPlayer.lastVisualizedSelectedItem = item;
			dollPlayer.ItemCheck_EmitHeldItemLight(item);
			dollPlayer.AnimatePlayerAndGetItemFrame(0f, item);
			TEDisplayDoll._playerRenderer.OverrideHeldProjectile = null;
			if (item != null && !item.IsAir && item.shoot > 0)
			{
				Projectile projectileDummy = TEDisplayDoll._projectileDummy;
				projectileDummy.SetDefaults(item.shoot);
				projectileDummy.isAPreviewDisplayDoll = true;
				bool flag = false;
				List<TEDisplayDoll.DisplayDollPose> list2;
				if (TEDisplayDoll.SupportedUseStylePoses.TryGetValue(item.useStyle, out list2))
				{
					foreach (TEDisplayDoll.DisplayDollPose displayDollPose2 in list2)
					{
						if ((DisplayDollPoseID)this._pose == displayDollPose2.Pose)
						{
							projectileDummy.AI_DisplayDoll(dollPlayer, displayDollPose2, out flag);
							break;
						}
					}
				}
				if (flag)
				{
					TEDisplayDoll._playerRenderer.OverrideHeldProjectile = projectileDummy;
					int drawLayer = projectileDummy.drawLayer;
					if (drawLayer <= 3)
					{
						Main.instance.DrawProjDirect(projectileDummy, null);
					}
				}
			}
			dollPlayer.isFullbright = tileSafely.fullbrightBlock();
			dollPlayer.skinDyePacked = PlayerDrawHelper.PackShader((int)tileSafely.color(), PlayerDrawHelper.ShaderConfiguration.TilePaintID);
			TEDisplayDoll._playerRenderer.PrepareDrawForFrame(dollPlayer);
			TEDisplayDoll._playerRenderer.DrawPlayer(Main.Camera, dollPlayer, dollPlayer.position, 0f, dollPlayer.fullRotationOrigin, 0f, 1f);
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x005B6D80 File Offset: 0x005B4F80
		public override void OnPlayerUpdate(Player player)
		{
			if (!player.InTileEntityInteractionRange(player.tileEntityAnchor.X, player.tileEntityAnchor.Y, 2, 3, TileReachCheckSettings.Simple) || player.chest != -1 || player.talkNPC != -1)
			{
				if (player.chest == -1 && player.talkNPC == -1)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				}
				player.tileEntityAnchor.Clear();
			}
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x005B6DF8 File Offset: 0x005B4FF8
		public static void OnPlayerInteraction(Player player, int clickX, int clickY)
		{
			int num = clickX;
			if (Main.tile[num, clickY].frameX % 36 != 0)
			{
				num--;
			}
			int num2 = clickY - (int)(Main.tile[num, clickY].frameY / 18);
			int num3 = TileEntityType<TEDisplayDoll>.Find(num, num2);
			if (num3 != -1)
			{
				num2++;
				TileEntity.BasicOpenCloseInteraction(player, num, num2, num3);
			}
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x005B6E54 File Offset: 0x005B5054
		public override void OnInventoryDraw(Player player, SpriteBatch spriteBatch)
		{
			if (Main.tile[player.tileEntityAnchor.X, player.tileEntityAnchor.Y].type != 470)
			{
				player.tileEntityAnchor.Clear();
				return;
			}
			this.DrawUI(player, spriteBatch);
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x005B6EA4 File Offset: 0x005B50A4
		public string GetItemGamepadInstructions(int slot = 0)
		{
			Item[] array = this._equip;
			int num = slot;
			int num2;
			if (slot >= 18)
			{
				array = this._misc;
				num = 0;
				num2 = 38;
			}
			else if (slot >= 9)
			{
				num -= 9;
				array = this._dyes;
				num2 = 25;
			}
			else if (slot == 8)
			{
				num2 = 39;
			}
			else if (slot >= 3)
			{
				num2 = 24;
			}
			else
			{
				num2 = 23;
			}
			return ItemSlot.GetGamepadInstructions(array, num2, num);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x005B6F00 File Offset: 0x005B5100
		private void DrawUI(Player player, SpriteBatch spriteBatch)
		{
			Main.inventoryScale = 0.755f;
			this.DrawSlotMisc(player, spriteBatch, 1, 0, 0f, 0.5f, 38);
			this.DrawSlotPairSet(player, spriteBatch, 3, 0, 1f, 0.5f, 23);
			this.DrawSlotPairSet(player, spriteBatch, 5, 3, 4f, 0.5f, 24);
			this.DrawSlotPairSet(player, spriteBatch, 1, 8, 9f, 0.5f, 39);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x005B6F70 File Offset: 0x005B5170
		private void DrawSlotMisc(Player player, SpriteBatch spriteBatch, int slotsToShowLine, int slotsArrayOffset, float offsetX, float offsetY, int inventoryContextTarget)
		{
			Item[] array = this._misc;
			int num = inventoryContextTarget;
			for (int i = 0; i < slotsToShowLine; i++)
			{
				for (int j = 0; j < 1; j++)
				{
					int num2 = (int)(22f + ((float)i + offsetX) * 56f * Main.inventoryScale);
					int num3 = (int)((float)Main.instance.invBottom + ((float)j + offsetY) * 56f * Main.inventoryScale);
					if (j == 0)
					{
						array = this._misc;
						num = inventoryContextTarget;
					}
					if (Utils.FloatIntersect((float)Main.mouseX, (float)Main.mouseY, 0f, 0f, (float)num2, (float)num3, (float)TextureAssets.InventoryBack.Width() * Main.inventoryScale, (float)TextureAssets.InventoryBack.Height() * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
					{
						player.mouseInterface = true;
						ItemSlot.Handle(array, num, i + slotsArrayOffset, true);
					}
					ItemSlot.Draw(spriteBatch, array, num, i + slotsArrayOffset, new Vector2((float)num2, (float)num3), default(Color));
				}
			}
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x005B7074 File Offset: 0x005B5274
		private void DrawSlotPairSet(Player player, SpriteBatch spriteBatch, int slotsToShowLine, int slotsArrayOffset, float offsetX, float offsetY, int inventoryContextTarget)
		{
			Item[] array = this._equip;
			for (int i = 0; i < slotsToShowLine; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int num = (int)(22f + ((float)i + offsetX) * 56f * Main.inventoryScale);
					int num2 = (int)((float)Main.instance.invBottom + ((float)j + offsetY) * 56f * Main.inventoryScale);
					int num3;
					if (j == 0)
					{
						array = this._equip;
						num3 = inventoryContextTarget;
					}
					else
					{
						array = this._dyes;
						num3 = 25;
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

		// Token: 0x06002FFC RID: 12284 RVA: 0x005B7184 File Offset: 0x005B5384
		public override ItemSlot.AlternateClickAction? GetShiftClickAction(Item[] inv, int context = 0, int slot = 0)
		{
			Item item = inv[slot];
			if (context == 0 && TEDisplayDoll.CanQuickSwapIntoDisplayDoll(item))
			{
				return new ItemSlot.AlternateClickAction?(ItemSlot.AlternateClickAction.TransferToChest);
			}
			if ((context == 23 || context == 24 || context == 39 || context == 25 || context == 38) && Main.LocalPlayer.ItemSpace(item).CanTakeItemToPersonalInventory)
			{
				return new ItemSlot.AlternateClickAction?(ItemSlot.AlternateClickAction.TransferFromChest);
			}
			return null;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x005B71F0 File Offset: 0x005B53F0
		public override bool PerformShiftClickAction(Item[] inv, int context = 0, int slot = 0)
		{
			Item item = inv[slot];
			if (Main.cursorOverride == 9 && context == 0)
			{
				if (!item.IsAir && !item.favorited && TEDisplayDoll.CanQuickSwapIntoDisplayDoll(item))
				{
					return this.TryFitting(inv, slot);
				}
			}
			else if (Main.cursorOverride == 8 && (context == 23 || context == 24 || context == 39 || context == 25 || context == 38))
			{
				inv[slot] = Main.LocalPlayer.GetItem(item, GetItemSettings.QuickTransferFromSlot);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)this.ID, (float)slot, (float)((context == 38) ? 3 : ((context == 25) ? 1 : 0)), 0, 0, 0);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x005B729D File Offset: 0x005B549D
		public static bool CanQuickSwapIntoDisplayDoll(Item item)
		{
			return item.headSlot > 0 || item.bodySlot > 0 || item.legSlot > 0 || item.accessory || item.mountType >= 0 || TEDisplayDoll.AcceptedInWeaponSlot(item);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x005B72D3 File Offset: 0x005B54D3
		public static bool AcceptedInWeaponSlot(Item item)
		{
			return (item.useStyle != 0 && item.mountType == -1) || item.holdStyle != 0;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x005B72F4 File Offset: 0x005B54F4
		private bool TryFitting(Item[] inv, int slot)
		{
			Item item = inv[slot];
			Item[] array = this._equip;
			int num = -1;
			if (item.headSlot > 0)
			{
				num = 0;
			}
			else if (item.bodySlot > 0)
			{
				num = 1;
			}
			else if (item.legSlot > 0)
			{
				num = 2;
			}
			else if (item.accessory)
			{
				num = this.GetAccessoryTargetSlot(item);
			}
			else if (item.mountType >= 0)
			{
				num = 8;
			}
			else if (TEDisplayDoll.AcceptedInWeaponSlot(item))
			{
				array = this._misc;
				num = 0;
			}
			if (num == -1)
			{
				return false;
			}
			if (item.stack > 1 && !array[num].IsAir)
			{
				return true;
			}
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
			if (item.stack > 1)
			{
				item.favorited = false;
				array[num] = item.Clone();
				array[num].stack = 1;
				item.stack--;
			}
			else
			{
				inv[slot].favorited = false;
				Utils.Swap<Item>(ref array[num], ref inv[slot]);
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)this.ID, (float)num, (float)((array == this._misc) ? 3 : 0), 0, 0, 0);
			}
			return true;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x005B7414 File Offset: 0x005B5614
		private int GetAccessoryTargetSlot(Item item)
		{
			int num;
			if (ItemSlot.HasIncompatibleAccessory(item, new ArraySegment<Item>(this._equip, 3, 5), out num))
			{
				return num;
			}
			for (int i = 3; i < 6; i++)
			{
				if (this._equip[i].IsAir)
				{
					return i;
				}
			}
			return 3;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x005B7458 File Offset: 0x005B5658
		public void WriteItem(int itemIndex, BinaryWriter writer, Item[] collection)
		{
			Item item = collection[itemIndex];
			writer.Write((ushort)item.type);
			writer.Write((ushort)item.stack);
			writer.Write(item.prefix);
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x005B7490 File Offset: 0x005B5690
		public void ReadItem(int itemIndex, BinaryReader reader, Item[] collection)
		{
			int num = (int)reader.ReadUInt16();
			int num2 = (int)reader.ReadUInt16();
			int num3 = (int)reader.ReadByte();
			if (itemIndex >= collection.Length)
			{
				return;
			}
			Item item = collection[itemIndex];
			item.SetDefaults(num, null);
			item.stack = num2;
			item.Prefix(num3);
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x005B74D4 File Offset: 0x005B56D4
		public void WriteData(int itemIndex, int command, BinaryWriter writer)
		{
			bool flag = command == 1;
			bool flag2 = command == 2;
			bool flag3 = command == 3;
			if (flag2)
			{
				writer.Write(this._pose);
				return;
			}
			Item[] array = this._equip;
			if (flag)
			{
				array = this._dyes;
			}
			if (flag3)
			{
				array = this._misc;
			}
			this.WriteItem(itemIndex, writer, array);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x005B7524 File Offset: 0x005B5724
		public void ReadData(int itemIndex, int command, BinaryReader reader)
		{
			bool flag = command == 1;
			bool flag2 = command == 2;
			bool flag3 = command == 3;
			if (flag2)
			{
				this.ReadPose(reader);
				return;
			}
			Item[] array = this._equip;
			if (flag)
			{
				array = this._dyes;
			}
			if (flag3)
			{
				array = this._misc;
			}
			this.ReadItem(itemIndex, reader, array);
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x005B756D File Offset: 0x005B576D
		public static void WriteDummySync(int itemIndex, int command, BinaryWriter writer)
		{
			if (command == 2)
			{
				writer.Write(0);
				return;
			}
			writer.Write(0);
			writer.Write(0);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x005B758B File Offset: 0x005B578B
		public static void ReadDummySync(int itemIndex, int command, BinaryReader reader)
		{
			if (command == 2)
			{
				reader.ReadByte();
				return;
			}
			reader.ReadInt32();
			reader.ReadByte();
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x005B75A9 File Offset: 0x005B57A9
		public void ReadPose(BinaryReader reader)
		{
			this._pose = reader.ReadByte();
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x005B75B8 File Offset: 0x005B57B8
		public override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 470 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 36 == 0;
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x005B761C File Offset: 0x005B581C
		public void SetInventoryFromMannequin(int headFrame, int shirtFrame, int legFrame)
		{
			headFrame /= 100;
			shirtFrame /= 100;
			legFrame /= 100;
			if (headFrame >= 0 && headFrame < Item.headType.Length)
			{
				this._equip[0].SetDefaults(Item.headType[headFrame], null);
			}
			if (shirtFrame >= 0 && shirtFrame < Item.bodyType.Length)
			{
				this._equip[1].SetDefaults(Item.bodyType[shirtFrame], null);
			}
			if (legFrame >= 0 && legFrame < Item.legType.Length)
			{
				this._equip[2].SetDefaults(Item.legType[legFrame], null);
			}
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x005B76A4 File Offset: 0x005B58A4
		public static bool IsBreakable(int clickX, int clickY)
		{
			int num = clickX;
			if (Main.tile[num, clickY].frameX % 36 != 0)
			{
				num--;
			}
			int num2 = clickY - (int)(Main.tile[num, clickY].frameY / 18);
			TEDisplayDoll tedisplayDoll;
			return !TileEntity.TryGetAt<TEDisplayDoll>(num, num2, out tedisplayDoll) || !tedisplayDoll.ContainsItems();
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x005B76FC File Offset: 0x005B58FC
		public static bool TryChangePose(int clickX, int clickY)
		{
			int num = clickX;
			if (Main.tile[num, clickY].frameX % 36 != 0)
			{
				num--;
			}
			int num2 = clickY - (int)(Main.tile[num, clickY].frameY / 18);
			TEDisplayDoll tedisplayDoll;
			if (TileEntity.TryGetAt<TEDisplayDoll>(num, num2, out tedisplayDoll))
			{
				tedisplayDoll.ChangePose();
				if (Main.netMode == 1)
				{
					NetMessage.SendData(121, -1, -1, null, Main.myPlayer, (float)tedisplayDoll.ID, (float)tedisplayDoll._pose, 2f, 0, 0, 0);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x005B777F File Offset: 0x005B597F
		public void ChangePose()
		{
			this._pose += 1;
			if (!this.IsValidPose((int)this._pose))
			{
				this._pose = 0;
			}
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x005B77A8 File Offset: 0x005B59A8
		public bool ContainsItems()
		{
			Item[] array = this._equip;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsAir)
				{
					return true;
				}
			}
			array = this._dyes;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsAir)
				{
					return true;
				}
			}
			array = this._misc;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsAir)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x005B781C File Offset: 0x005B5A1C
		public void FixLoadedData()
		{
			Item[] array = this._equip;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FixAgainstExploit();
			}
			array = this._dyes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FixAgainstExploit();
			}
			array = this._misc;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FixAgainstExploit();
			}
		}

		// Token: 0x040056A6 RID: 22182
		private const int MyTileID = 470;

		// Token: 0x040056A7 RID: 22183
		public const int entityTileWidth = 2;

		// Token: 0x040056A8 RID: 22184
		public const int entityTileHeight = 3;

		// Token: 0x040056A9 RID: 22185
		private Player _dollPlayer;

		// Token: 0x040056AA RID: 22186
		private Item[] _equip;

		// Token: 0x040056AB RID: 22187
		private Item[] _dyes;

		// Token: 0x040056AC RID: 22188
		private Item[] _misc;

		// Token: 0x040056AD RID: 22189
		private byte _pose;

		// Token: 0x040056AE RID: 22190
		public static Dictionary<int, List<TEDisplayDoll.DisplayDollPose>> SupportedUseStylePoses = new Dictionary<int, List<TEDisplayDoll.DisplayDollPose>>();

		// Token: 0x040056AF RID: 22191
		private static Projectile _projectileDummy = new Projectile();

		// Token: 0x040056B0 RID: 22192
		private static LegacyPlayerRenderer _playerRenderer = new LegacyPlayerRenderer();

		// Token: 0x02000939 RID: 2361
		public struct DisplayDollPose
		{
			// Token: 0x04007526 RID: 29990
			public DisplayDollPoseID Pose;

			// Token: 0x04007527 RID: 29991
			public float ItemAnimationPercent;

			// Token: 0x04007528 RID: 29992
			public float? ItemAimRadians;
		}
	}
}
