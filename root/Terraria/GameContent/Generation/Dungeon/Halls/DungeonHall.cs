using System;
using System.Collections.Generic;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon.Rooms;

namespace Terraria.GameContent.Generation.Dungeon.Halls
{
	// Token: 0x020004C5 RID: 1221
	public abstract class DungeonHall
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060034B8 RID: 13496 RVA: 0x00607771 File Offset: 0x00605971
		public bool Processed
		{
			get
			{
				return this.calculated || this.generated;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x00607783 File Offset: 0x00605983
		public Vector2D CenterPosition
		{
			get
			{
				return (this.StartPosition + this.EndPosition) / 2.0;
			}
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x006077A4 File Offset: 0x006059A4
		public DungeonHall(DungeonHallSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x060034BB RID: 13499
		public abstract void CalculateHall(DungeonData data, Vector2D startPoint, Vector2D endPoint);

		// Token: 0x060034BC RID: 13500
		public abstract void CalculatePlatformsAndDoors(DungeonData data);

		// Token: 0x060034BD RID: 13501
		public abstract void GenerateHall(DungeonData data);

		// Token: 0x060034BE RID: 13502 RVA: 0x001FC6F1 File Offset: 0x001FA8F1
		public virtual int GetFurnitureCount(int defaultCount)
		{
			return defaultCount;
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x006077C0 File Offset: 0x006059C0
		public void GenerateDungeonSquareHall(DungeonData data, List<DungeonRoom> roomsInArea, Vector2D currentPoint, ushort tileType, ushort tileCrackedType, ushort wallType, int innerBoundsSize, int outerBoundsSize, bool placeOverProtectedBricks = false, bool crackedBricks = false, bool clearPaintFirst = false)
		{
			int num = innerBoundsSize + outerBoundsSize;
			for (int i = -num; i <= num; i++)
			{
				int num2 = (int)currentPoint.X + i;
				for (int j = -num; j <= num; j++)
				{
					int num3 = (int)currentPoint.Y + j;
					bool flag = true;
					bool flag2 = true;
					ProtectionType highestProtectionTypeFromPoint = DungeonUtils.GetHighestProtectionTypeFromPoint(num2, num3, roomsInArea);
					if (highestProtectionTypeFromPoint != ProtectionType.TilesAndWalls)
					{
						if (highestProtectionTypeFromPoint == ProtectionType.Tiles)
						{
							flag = false;
						}
						if (highestProtectionTypeFromPoint == ProtectionType.Walls && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num2, num3].wall, false))
						{
							flag2 = false;
						}
						Tile tile = Main.tile[num2, num3];
						if (Math.Abs(i) <= innerBoundsSize && Math.Abs(j) <= innerBoundsSize)
						{
							if (!this.CanRemoveTileAt(data, tile, (int)tileCrackedType))
							{
								goto IL_023B;
							}
							if (crackedBricks)
							{
								if ((tile.active() || !DungeonUtils.IsConsideredDungeonWall((int)tile.wall, false)) && num3 < Main.UnderworldLayer)
								{
									if (this.settings.CarveOnly)
									{
										tile.ClearTile();
									}
									else
									{
										if (flag)
										{
											tile.ClearTile();
										}
										if (flag2)
										{
											tile.wall = 0;
										}
										if (flag2)
										{
											if (clearPaintFirst)
											{
												WorldGen.paintWall(num2, num3, 0, false, false);
											}
											DungeonUtils.ChangeWallType(tile, wallType, false, this.settings.OverridePaintWall);
										}
										if (flag)
										{
											if (clearPaintFirst)
											{
												WorldGen.paintTile(num2, num3, 0, false, false);
											}
											DungeonUtils.ChangeTileType(tile, tileCrackedType, false, this.settings.OverridePaintTile);
										}
									}
								}
							}
							else
							{
								tile.ClearTile();
								if (!this.settings.CarveOnly && flag2)
								{
									if (clearPaintFirst)
									{
										WorldGen.paintWall(num2, num3, 0, false, false);
									}
									DungeonUtils.ChangeWallType(tile, wallType, false, this.settings.OverridePaintWall);
								}
							}
						}
						else if (this.CanPlaceTileAt(data, tile, (int)tileType, (int)tileCrackedType))
						{
							if (flag)
							{
								tile.ClearTile();
							}
							if (flag2)
							{
								tile.wall = 0;
							}
							if (flag)
							{
								if (clearPaintFirst)
								{
									WorldGen.paintTile(num2, num3, 0, false, false);
								}
								DungeonUtils.ChangeTileType(tile, tileType, false, this.settings.OverridePaintTile);
							}
							if (flag2 && i > -num && i < num && j > -num && j < num)
							{
								if (clearPaintFirst)
								{
									WorldGen.paintWall(num2, num3, 0, false, false);
								}
								DungeonUtils.ChangeWallType(tile, wallType, false, this.settings.OverridePaintWall);
							}
						}
						tile.liquid = 0;
					}
					IL_023B:;
				}
			}
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x00607A20 File Offset: 0x00605C20
		public virtual bool CanPlaceTileAt(DungeonData data, Tile tile, int tileType, int tileCrackedType)
		{
			return !this.settings.CarveOnly && (!DungeonUtils.IsConsideredDungeonWall((int)tile.wall, false) || (tile.active() && !DungeonUtils.IsHigherOrEqualTieredDungeonTile(data, (int)tile.type, tileType) && (int)tile.type != tileCrackedType));
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x00607A74 File Offset: 0x00605C74
		public virtual bool CanRemoveTileAt(DungeonData data, Tile tile, int tileCrackedType)
		{
			return !tile.active() || data.Type != DungeonType.DualDungeon || (int)tile.type != tileCrackedType;
		}

		// Token: 0x04005A5F RID: 23135
		public DungeonHallSettings settings;

		// Token: 0x04005A60 RID: 23136
		public bool calculated;

		// Token: 0x04005A61 RID: 23137
		public bool generated;

		// Token: 0x04005A62 RID: 23138
		public DungeonBounds Bounds = new DungeonBounds();

		// Token: 0x04005A63 RID: 23139
		public Vector2D StartPosition;

		// Token: 0x04005A64 RID: 23140
		public Vector2D EndPosition;

		// Token: 0x04005A65 RID: 23141
		public Vector2D StartDirection;

		// Token: 0x04005A66 RID: 23142
		public Vector2D EndDirection;

		// Token: 0x04005A67 RID: 23143
		public bool CrackedBrick;
	}
}
