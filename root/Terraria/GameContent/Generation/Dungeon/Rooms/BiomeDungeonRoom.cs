using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon.Features;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004A4 RID: 1188
	public abstract class BiomeDungeonRoom : DungeonRoom
	{
		// Token: 0x0600341A RID: 13338 RVA: 0x006007A7 File Offset: 0x005FE9A7
		public BiomeDungeonRoom(DungeonRoomSettings settings)
			: base(settings)
		{
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x006007B0 File Offset: 0x005FE9B0
		public override bool CanGenerateFeatureAt(DungeonData data, IDungeonFeature feature, int x, int y)
		{
			return !(feature is DungeonDropTrap) && !(feature is DungeonPitTrap) && !(feature is DungeonGlobalSpikes) && !(feature is DungeonGlobalTraps) && !(feature is DungeonGlobalBookshelves) && base.CanGenerateFeatureAt(data, feature, x, y);
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x00009E46 File Offset: 0x00008046
		public override void GeneratePreHallwaysDungeonFeaturesInRoom(DungeonData data)
		{
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x00009E46 File Offset: 0x00008046
		public override void GenerateEarlyDungeonFeaturesInRoom(DungeonData data)
		{
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x006007E7 File Offset: 0x005FE9E7
		public override void CalculatePlatformsAndDoors(DungeonData data)
		{
			DungeonUtils.CalculatePlatformsAndDoorsOnEdgesOfRoom(data, this.InnerBounds, this.settings.ForceStyleForDoorsAndPlatforms ? this.settings.StyleData : null, new int?(0), new int?(0));
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x0060081C File Offset: 0x005FEA1C
		public override ConnectionPointQuality GetHallwayConnectionPoint(Vector2D otherRoomPos, out Vector2D connectionPoint)
		{
			connectionPoint = base.Center;
			int num = (((otherRoomPos - connectionPoint).SafeNormalize(Vector2D.UnitX).X < 0.0) ? (-1) : 1);
			while (base.IsInsideRoom(connectionPoint.ToPoint()))
			{
				connectionPoint.X += (double)num;
			}
			return ConnectionPointQuality.Good;
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x00600888 File Offset: 0x005FEA88
		public override ProtectionType GetProtectionTypeFromPoint(int x, int y)
		{
			if (this.settings.StyleData.Style == 0 || this.settings.StyleData.Style == 10)
			{
				return ProtectionType.None;
			}
			if (this._innerShapeData != null && this._outerShapeData != null)
			{
				if (!this._outerShapeData.Contains(x - base.Center.X, y - base.Center.Y))
				{
					return ProtectionType.None;
				}
				if (!this._innerShapeData.Contains(x - base.Center.X, y - base.Center.Y))
				{
					return ProtectionType.Walls;
				}
				return ProtectionType.TilesAndWalls;
			}
			else
			{
				if (!this.OuterBounds.Contains(x, y))
				{
					return ProtectionType.None;
				}
				if (!this.InnerBounds.Contains(x, y))
				{
					return ProtectionType.Walls;
				}
				return ProtectionType.TilesAndWalls;
			}
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x00600944 File Offset: 0x005FEB44
		public override bool IsInsideRoom(int x, int y)
		{
			return base.IsInsideRoom(x, y) && (this._innerShapeData == null || this._innerShapeData.Contains(x - base.Center.X, y - base.Center.Y));
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x00600984 File Offset: 0x005FEB84
		public static int GetBiomeRoomInnerSize(DungeonGenerationStyleData styleData)
		{
			float num = (float)Main.maxTilesX / 4200f;
			if (styleData.Style == 10)
			{
				return (int)(50f * num);
			}
			return (int)(32f * num);
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x006009B9 File Offset: 0x005FEBB9
		public static int GetBiomeRoomOuterSize(DungeonGenerationStyleData styleData)
		{
			return BiomeDungeonRoom.GetBiomeRoomInnerSize(styleData) + 8;
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x006009C4 File Offset: 0x005FEBC4
		public void BiomeRoom_AddHallwaySpace(Vector2 position, int edgeLeft, int edgeRight, int edgeTop, int edgeBottom, int shapeOffset, ushort wallType, int wallPaint, bool generating)
		{
			int num = (edgeTop + edgeBottom) / 2 - 3;
			int num2 = (edgeTop + edgeBottom) / 2 + 3;
			for (int i = edgeLeft; i < edgeRight; i++)
			{
				int num3 = i;
				for (int j = num; j < num2; j++)
				{
					int num4 = j;
					Tile tile = Main.tile[num3, num4];
					if (!generating && this._innerShapeData != null)
					{
						this._innerShapeData.Add(num3 - (int)position.X + shapeOffset, num4 - (int)position.Y + shapeOffset);
						this._outerShapeData.Add(num3 - (int)position.X + shapeOffset, num4 - (int)position.Y + shapeOffset);
					}
					else
					{
						tile.active(false);
						if (tile.wall != wallType)
						{
							DungeonUtils.ChangeWallType(tile, wallType, false, wallPaint);
						}
					}
				}
			}
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x00600A98 File Offset: 0x005FEC98
		public void BiomeRoom_FinishRoom(UnifiedRandom genRand, int outerRoomLeft, int outerRoomRight, int outerRoomTop, int outerRoomBottom, bool treatAsSurface = false)
		{
			List<int> list = new List<int>();
			for (int i = outerRoomLeft; i < outerRoomRight; i++)
			{
				list.Clear();
				int num = i;
				for (int j = outerRoomTop; j < outerRoomBottom; j++)
				{
					int num2 = j;
					Tile tile = Main.tile[num, num2];
					if (list.Contains(num2))
					{
						list.Remove(num2);
					}
					else if (this.InnerBounds.Contains(num, num2))
					{
						switch (this.settings.StyleData.Style)
						{
						default:
							if (tile.liquid > 0)
							{
								tile.liquidType(0);
							}
							break;
						case 2:
							if (treatAsSurface)
							{
								this.BiomeRoom_SnowySurface(genRand, ref list, num, num2, tile);
							}
							if (tile.liquid > 0)
							{
								tile.liquidType(0);
							}
							break;
						case 3:
							if (treatAsSurface)
							{
								this.BiomeRoom_SandySurface(genRand, ref list, num, num2, tile);
							}
							if (tile.liquid > 0)
							{
								tile.liquidType(1);
							}
							break;
						case 4:
							if (treatAsSurface)
							{
								this.BiomeRoom_GrassySurface(genRand, ref list, num, num2, tile, 25, 23);
							}
							if (tile.liquid > 0)
							{
								tile.liquidType(0);
							}
							break;
						case 5:
							if (treatAsSurface)
							{
								this.BiomeRoom_GrassySurface(genRand, ref list, num, num2, tile, 203, 199);
							}
							if (tile.liquid > 0)
							{
								tile.liquidType(0);
							}
							break;
						case 6:
							if (treatAsSurface)
							{
								this.BiomeRoom_GrassySurface(genRand, ref list, num, num2, tile, 117, 109);
							}
							if (tile.liquid > 0)
							{
								tile.liquidType(0);
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x00600C4C File Offset: 0x005FEE4C
		public void BiomeRoom_GrassySurface(UnifiedRandom genRand, ref List<int> heightsToSkip, int tileX, int tileY, Tile tile, ushort tileTypeToReplace, ushort grassTileType)
		{
			if (!Main.tile[tileX, tileY - 1].active() && tile.active() && tile.type == tileTypeToReplace)
			{
				if (WorldGen.TileIsExposedToAir(tileX, tileY))
				{
					DungeonUtils.ChangeTileType(tile, grassTileType, false, -1);
				}
				else
				{
					DungeonUtils.ChangeTileType(tile, 0, false, -1);
				}
				for (int i = 0; i < 5 + genRand.Next(4); i++)
				{
					int num = tileY + i;
					Tile tile2 = Main.tile[tileX, num];
					if (tile2.active() && tile2.type == tileTypeToReplace)
					{
						heightsToSkip.Add(num);
						tile2.liquid = 0;
						if (WorldGen.TileIsExposedToAir(tileX, num))
						{
							DungeonUtils.ChangeTileType(tile2, grassTileType, false, -1);
						}
						else
						{
							DungeonUtils.ChangeTileType(tile2, 0, false, -1);
						}
					}
				}
			}
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x00600D14 File Offset: 0x005FEF14
		public void BiomeRoom_SandySurface(UnifiedRandom genRand, ref List<int> heightsToSkip, int tileX, int tileY, Tile tile)
		{
			if (!Main.tile[tileX, tileY - 1].active() && tile.active() && tile.type == 396)
			{
				if (WorldGen.BlockBelowMakesSandConvertIntoHardenedSand(tileX, tileY))
				{
					DungeonUtils.ChangeTileType(tile, 397, false, -1);
				}
				else
				{
					DungeonUtils.ChangeTileType(tile, 53, false, -1);
				}
				for (int i = 0; i < 5 + genRand.Next(4); i++)
				{
					int num = tileY + i;
					Tile tile2 = Main.tile[tileX, num];
					if (tile2.active() && tile2.type == 396)
					{
						heightsToSkip.Add(num);
						tile2.liquid = 0;
						if (WorldGen.BlockBelowMakesSandConvertIntoHardenedSand(tileX, num))
						{
							DungeonUtils.ChangeTileType(tile2, 397, false, -1);
						}
						else
						{
							DungeonUtils.ChangeTileType(tile2, 53, false, -1);
						}
					}
				}
			}
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x00600DE8 File Offset: 0x005FEFE8
		public void BiomeRoom_SnowySurface(UnifiedRandom genRand, ref List<int> heightsToSkip, int tileX, int tileY, Tile tile)
		{
			if (!Main.tile[tileX, tileY - 1].active() && tile.active() && tile.type == 161)
			{
				DungeonUtils.ChangeTileType(tile, 147, false, -1);
				for (int i = 0; i < 5 + genRand.Next(4); i++)
				{
					int num = tileY + i;
					Tile tile2 = Main.tile[tileX, num];
					if (tile2.active() && tile2.type == 161)
					{
						heightsToSkip.Add(num);
						tile2.liquid = 0;
						DungeonUtils.ChangeTileType(tile2, 147, false, -1);
					}
				}
			}
		}

		// Token: 0x040059D6 RID: 22998
		protected const int BIOMEROOM_INNER_SIZE_BASE = 32;

		// Token: 0x040059D7 RID: 22999
		protected const int BIOMEROOM_INNER_SIZE_BASE_TEMPLE = 50;

		// Token: 0x040059D8 RID: 23000
		protected const int BIOMEROOM_WALL_DEPTH = 8;

		// Token: 0x040059D9 RID: 23001
		protected ShapeData _innerShapeData;

		// Token: 0x040059DA RID: 23002
		protected ShapeData _outerShapeData;
	}
}
