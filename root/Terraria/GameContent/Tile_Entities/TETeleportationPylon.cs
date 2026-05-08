using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Tile_Entities
{
	// Token: 0x02000413 RID: 1043
	public class TETeleportationPylon : TileEntityType<TETeleportationPylon>
	{
		// Token: 0x06002FDB RID: 12251 RVA: 0x005B58DC File Offset: 0x005B3ADC
		public TETeleportationPylon()
		{
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x005B58E4 File Offset: 0x005B3AE4
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			TeleportPylonType teleportPylonType;
			if (!this.TryGetPylonTypeFromTileCoords(x, y, out teleportPylonType))
			{
				TETeleportationPylon.RejectPlacementFromNet(x, y);
				return;
			}
			if (Main.PylonSystem.HasPylonOfType(teleportPylonType))
			{
				TETeleportationPylon.RejectPlacementFromNet(x, y);
				return;
			}
			base.NetPlaceEntityAttempt(x, y);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x005B5922 File Offset: 0x005B3B22
		public bool TryGetPylonType(out TeleportPylonType pylonType)
		{
			return this.TryGetPylonTypeFromTileCoords((int)this.Position.X, (int)this.Position.Y, out pylonType);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x005B5944 File Offset: 0x005B3B44
		private static void RejectPlacementFromNet(int x, int y)
		{
			WorldGen.KillTile(x, y, false, false, false);
			if (Main.netMode == 2)
			{
				NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x005B597A File Offset: 0x005B3B7A
		public override void OnPlaced()
		{
			Main.PylonSystem.RequestImmediateUpdate();
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x005B597A File Offset: 0x005B3B7A
		public override void OnRemoved()
		{
			Main.PylonSystem.RequestImmediateUpdate();
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x005B5988 File Offset: 0x005B3B88
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Position.X,
				"x  ",
				this.Position.Y,
				"y"
			});
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x005B59D8 File Offset: 0x005B3BD8
		public static void Framing_CheckTile(int callX, int callY)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			Tile tileSafely = Framing.GetTileSafely(callX, callY);
			int num = callX - (int)(tileSafely.frameX / 18 % 3);
			int num2 = callY - (int)(tileSafely.frameY / 18 % 4);
			int pylonStyleFromTile = TETeleportationPylon.GetPylonStyleFromTile(tileSafely);
			bool flag = false;
			for (int i = num; i < num + 3; i++)
			{
				for (int j = num2; j < num2 + 4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile == null)
					{
						return;
					}
					if (!tile.active() || tile.type != 597)
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
				TileEntityType<TETeleportationPylon>.Kill(num, num2);
				int pylonItemTypeFromTileStyle = TETeleportationPylon.GetPylonItemTypeFromTileStyle(pylonStyleFromTile);
				Item.NewItem(new EntitySource_TileBreak(num, num2), num * 16, num2 * 16, 48, 64, pylonItemTypeFromTileStyle, 1, false, 0, false);
				WorldGen.destroyObject = true;
				for (int k = num; k < num + 3; k++)
				{
					for (int l = num2; l < num2 + 4; l++)
					{
						if (Main.tile[k, l].active() && Main.tile[k, l].type == 597)
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				WorldGen.destroyObject = false;
			}
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x005B5B3A File Offset: 0x005B3D3A
		public static int GetPylonStyleFromTile(Tile tile)
		{
			return (int)(tile.frameX / 54);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x005B5B48 File Offset: 0x005B3D48
		public static int GetPylonItemTypeFromTileStyle(int style)
		{
			switch (style)
			{
			case 1:
				return 4875;
			case 2:
				return 4916;
			case 3:
				return 4917;
			case 4:
				return 4918;
			case 5:
				return 4919;
			case 6:
				return 4920;
			case 7:
				return 4921;
			case 8:
				return 4951;
			case 9:
				return 5652;
			case 10:
				return 5653;
			default:
				return 4876;
			}
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x005B5BC8 File Offset: 0x005B3DC8
		public override bool IsTileValidForEntity(int x, int y)
		{
			return Main.tile[x, y].active() && Main.tile[x, y].type == 597 && Main.tile[x, y].frameY == 0 && Main.tile[x, y].frameX % 54 == 0;
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x005B5C2C File Offset: 0x005B3E2C
		public static int PlacementPreviewHook_AfterPlacement(int x, int y, int type = 597, int style = 0, int direction = 1, int alternate = 0)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(Main.myPlayer, x - 1, y - 3, 3, 4, TileChangeType.None);
				NetMessage.SendData(87, -1, -1, null, x + -1, (float)(y + -3), (float)TileEntityType<TETeleportationPylon>.EntityTypeID, 0f, 0, 0, 0);
				return -1;
			}
			return TileEntityType<TETeleportationPylon>.Place(x + -1, y + -3);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x005B5C84 File Offset: 0x005B3E84
		public static int PlacementPreviewHook_CheckIfCanPlace(int x, int y, int type = 597, int style = 0, int direction = 1, int alternate = 0)
		{
			TeleportPylonType pylonTypeFromPylonTileStyle = TETeleportationPylon.GetPylonTypeFromPylonTileStyle(style);
			if (Main.PylonSystem.HasPylonOfType(pylonTypeFromPylonTileStyle))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x005B5CA8 File Offset: 0x005B3EA8
		private bool TryGetPylonTypeFromTileCoords(int x, int y, out TeleportPylonType pylonType)
		{
			pylonType = TeleportPylonType.SurfacePurity;
			Tile tile = Main.tile[x, y];
			if (tile == null || !tile.active() || tile.type != 597)
			{
				return false;
			}
			int num = (int)(tile.frameX / 54);
			pylonType = TETeleportationPylon.GetPylonTypeFromPylonTileStyle(num);
			return true;
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x005B5CF2 File Offset: 0x005B3EF2
		private static TeleportPylonType GetPylonTypeFromPylonTileStyle(int pylonStyle)
		{
			return (TeleportPylonType)pylonStyle;
		}

		// Token: 0x04005699 RID: 22169
		private const int MyTileID = 597;

		// Token: 0x0400569A RID: 22170
		public const int entityTileWidth = 3;

		// Token: 0x0400569B RID: 22171
		public const int entityTileHeight = 4;
	}
}
