using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon.Features;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004B5 RID: 1205
	public abstract class DungeonRoom
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x00603E5D File Offset: 0x0060205D
		public bool Processed
		{
			get
			{
				return this.calculated || this.generated;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x00603E6F File Offset: 0x0060206F
		public Point Center
		{
			get
			{
				return this.InnerBounds.Center;
			}
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x00603E7C File Offset: 0x0060207C
		public DungeonRoom(DungeonRoomSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x00603EA4 File Offset: 0x006020A4
		public virtual bool CanGenerateFeatureAt(DungeonData data, IDungeonFeature feature, int x, int y)
		{
			return (!(feature is DungeonWindow) || data.Type == DungeonType.DualDungeon) && (!(feature is DungeonPitTrap) || ((DungeonPitTrapSettings)((DungeonPitTrap)feature).settings).ConnectedRoom == this) && this.settings.StyleData.CanGenerateFeatureAt(data, this, feature, x, y);
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x00603EFC File Offset: 0x006020FC
		public virtual void GeneratePreHallwaysDungeonFeaturesInRoom(DungeonData data)
		{
			if ((this.settings.StyleData.Style == 4 || this.settings.StyleData.Style == 5) && this.InnerBounds.Width > 10 && this.InnerBounds.Height > 10)
			{
				DungeonUtils.GenerateSpeleothemsInArea(data, this.settings.StyleData, this.InnerBounds.Left, this.InnerBounds.Top, this.InnerBounds.Width, this.InnerBounds.Height, Math.Max(3, this.InnerBounds.Width / 3), this.settings.StyleData.BrickTileType, this.settings.OverridePaintTile, -1, -1);
			}
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x00603FC0 File Offset: 0x006021C0
		public virtual void GenerateEarlyDungeonFeaturesInRoom(DungeonData data)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(this.settings.RandomSeed);
			if (data.Type != DungeonType.DualDungeon)
			{
				return;
			}
			if (unifiedRandom.Next(3) == 0)
			{
				DungeonWindowBasicSettings dungeonWindowBasicSettings = new DungeonWindowBasicSettings
				{
					Style = this.settings.StyleData,
					Closed = ((double)this.InnerBounds.Bottom > Main.worldSurface)
				};
				int width = this.InnerBounds.Width;
				int height = this.InnerBounds.Height;
				bool flag = true;
				int num = unifiedRandom.Next(3);
				if (num >= 1 && num <= 2 && (width <= 36 || height <= 15))
				{
					num = 0;
				}
				if (num == 0 && (width <= 14 || height <= 10))
				{
					flag = false;
				}
				if (flag)
				{
					Point point = this.InnerBounds.Center;
					if (num == 0 || num - 1 > 1)
					{
						int num2 = Math.Max(3, this.InnerBounds.Width / 3);
						if (num2 % 2 == 0)
						{
							num2++;
						}
						dungeonWindowBasicSettings.Width = Math.Max(3, num2);
						dungeonWindowBasicSettings.Height = Math.Max(5, this.InnerBounds.Height / 3);
						DungeonWindow dungeonWindow = new DungeonWindowBasic(dungeonWindowBasicSettings);
						point = this.GetRoomCenterForDungeonFeature(data, dungeonWindow);
						if (this.CanGenerateFeatureAt(data, dungeonWindow, point.X, point.Y))
						{
							dungeonWindow.GenerateFeature(data, point.X, point.Y);
						}
					}
					else
					{
						int num2 = Math.Min(7, Math.Max(3, this.InnerBounds.Width / 5));
						if (num2 % 2 == 0)
						{
							num2++;
						}
						dungeonWindowBasicSettings.Width = Math.Max(3, num2);
						dungeonWindowBasicSettings.Height = Math.Max(5, this.InnerBounds.Height / 3);
						DungeonWindow dungeonWindow = new DungeonWindowBasic(dungeonWindowBasicSettings);
						point = this.GetRoomCenterForDungeonFeature(data, dungeonWindow);
						if (this.CanGenerateFeatureAt(data, dungeonWindow, point.X, point.Y))
						{
							dungeonWindow.GenerateFeature(data, point.X, point.Y);
						}
						dungeonWindowBasicSettings.Height -= 2;
						dungeonWindow = new DungeonWindowBasic(dungeonWindowBasicSettings);
						if (this.CanGenerateFeatureAt(data, dungeonWindow, point.X - num2 - 2, point.Y))
						{
							dungeonWindow.GenerateFeature(data, point.X - num2 - 2, point.Y);
						}
						dungeonWindow = new DungeonWindowBasic(dungeonWindowBasicSettings);
						if (this.CanGenerateFeatureAt(data, dungeonWindow, point.X + num2 + 2, point.Y))
						{
							dungeonWindow.GenerateFeature(data, point.X + num2 + 2, point.Y);
						}
					}
				}
			}
			int liquidType = this.settings.StyleData.LiquidType;
			if (liquidType >= 0)
			{
				this.FloodRoom((byte)liquidType);
			}
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void GenerateLateDungeonFeaturesInRoom(DungeonData data)
		{
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x0060425F File Offset: 0x0060245F
		public virtual Point GetRoomCenterForDungeonFeature(DungeonData data, DungeonFeature feature)
		{
			return this.Center;
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x0060425F File Offset: 0x0060245F
		public virtual Point GetRoomCenterForHallway(Vector2D otherRoomPos)
		{
			return this.Center;
		}

		// Token: 0x06003471 RID: 13425
		public abstract void CalculateRoom(DungeonData data);

		// Token: 0x06003472 RID: 13426 RVA: 0x00604267 File Offset: 0x00602467
		public virtual void CalculatePlatformsAndDoors(DungeonData data)
		{
			DungeonUtils.CalculatePlatformsAndDoorsOnEdgesOfRoom(data, this.InnerBounds, this.settings.ForceStyleForDoorsAndPlatforms ? this.settings.StyleData : null, new int?(3), new int?(3));
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x0060429C File Offset: 0x0060249C
		public virtual ConnectionPointQuality GetHallwayConnectionPoint(Vector2D otherRoomPos, out Vector2D connectionPoint)
		{
			if (this.settings.HallwayConnectionPointOverride != null)
			{
				ConnectionPointQuality connectionPointQuality = this.settings.HallwayConnectionPointOverride(this, otherRoomPos, out connectionPoint);
				if (this.settings.HallwayPointAdjuster != null)
				{
					Vector2D vector2D = (otherRoomPos - connectionPoint).SafeNormalize(Vector2D.UnitX);
					connectionPoint -= vector2D * (double)this.settings.HallwayPointAdjuster.Value;
				}
				return connectionPointQuality;
			}
			connectionPoint = this.GetRoomCenterForHallway(otherRoomPos);
			Vector2D vector2D2 = (otherRoomPos - connectionPoint).SafeNormalize(Vector2D.UnitX);
			if (-0.5 < vector2D2.Y && vector2D2.Y < 0.7 && WorldGen.genRand.Next(2) == 0)
			{
				while (this.IsInsideRoom(connectionPoint.ToPoint()))
				{
					connectionPoint.Y += 1.0;
				}
				connectionPoint.Y -= 3.0;
			}
			else if (-0.7 < vector2D2.Y && vector2D2.Y < 0.5 && WorldGen.genRand.Next(3) == 0)
			{
				while (this.IsInsideRoom(connectionPoint.ToPoint()))
				{
					connectionPoint.Y -= 1.0;
				}
				connectionPoint.Y += 3.0;
			}
			else
			{
				connectionPoint += WorldGen.genRand.NextVector2DCircularEdge(4.0, 4.0);
			}
			vector2D2 = (otherRoomPos - connectionPoint).SafeNormalize(Vector2D.UnitX);
			while (this.IsInsideRoom(connectionPoint.ToPoint()))
			{
				connectionPoint += vector2D2;
			}
			if (this.settings.HallwayPointAdjuster != null)
			{
				connectionPoint -= vector2D2 * (double)this.settings.HallwayPointAdjuster.Value;
			}
			return ConnectionPointQuality.Good;
		}

		// Token: 0x06003474 RID: 13428
		public abstract bool GenerateRoom(DungeonData data);

		// Token: 0x06003475 RID: 13429 RVA: 0x006044C5 File Offset: 0x006026C5
		public virtual bool TryGenerateChestInRoom(DungeonData data, DungeonGlobalBasicChests feature)
		{
			return DungeonUtils.GenerateDungeonRegularChest(data, feature, this.settings.StyleData, this.InnerBounds);
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x006044DF File Offset: 0x006026DF
		public virtual bool DualDungeons_TryGenerateBiomeChestInRoom(DungeonData data, DungeonGlobalBiomeChests feature)
		{
			return DungeonUtils.GenerateDungeonBiomeChest(data, feature, this.settings.StyleData, this.InnerBounds, true);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x006044FA File Offset: 0x006026FA
		public virtual ProtectionType GetProtectionTypeFromPoint(int x, int y)
		{
			if (!this.OuterBounds.Contains(x, y))
			{
				return ProtectionType.None;
			}
			return ProtectionType.Walls;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x0060450E File Offset: 0x0060270E
		public bool IsInsideRoom(Point point)
		{
			return this.IsInsideRoom(point.X, point.Y);
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x00604522 File Offset: 0x00602722
		public virtual bool IsInsideRoom(int x, int y)
		{
			return this.InnerBounds.Contains(x, y);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x00604531 File Offset: 0x00602731
		public virtual int GetFloodedRoomTileCount()
		{
			return this.InnerBounds.Width * this.InnerBounds.Height;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x0060454C File Offset: 0x0060274C
		public virtual void FloodRoom(byte liquidType)
		{
			for (int i = this.InnerBounds.Left; i <= this.InnerBounds.Right; i++)
			{
				for (int j = this.InnerBounds.Center.Y; j <= this.InnerBounds.Bottom; j++)
				{
					Tile tile = Main.tile[i, j];
					if (!tile.active())
					{
						tile.liquid = byte.MaxValue;
						tile.liquidType((int)liquidType);
					}
				}
			}
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x001FC6F1 File Offset: 0x001FA8F1
		public virtual int GetFurnitureCount(int defaultCount)
		{
			return defaultCount;
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x006045C8 File Offset: 0x006027C8
		public void GenerateDungeonSquareRoom(DungeonData data, DungeonBounds innerBounds, DungeonBounds outerBounds, Vector2D currentPoint, ushort tileType, ushort wallType, int innerBoundsSize, int totalBoundsSize, bool genTiles = true, bool genWalls = true)
		{
			for (int i = -totalBoundsSize; i <= totalBoundsSize; i++)
			{
				int num = (int)currentPoint.X + i;
				for (int j = -totalBoundsSize; j <= totalBoundsSize; j++)
				{
					int num2 = (int)currentPoint.Y + j;
					Tile tile = Main.tile[num, num2];
					if (Math.Abs(i) <= innerBoundsSize && Math.Abs(j) <= innerBoundsSize)
					{
						innerBounds.UpdateBounds(num, num2);
						if (genWalls)
						{
							DungeonUtils.ChangeWallType(tile, wallType, true, this.settings.OverridePaintWall);
						}
					}
					else if (!DungeonUtils.IsHigherOrEqualTieredDungeonWall(data, (int)tile.wall, (int)wallType))
					{
						outerBounds.UpdateBounds(num, num2);
						if (genTiles)
						{
							DungeonUtils.ChangeTileType(tile, tileType, true, this.settings.OverridePaintTile);
						}
						if (genWalls && i > -totalBoundsSize && i < totalBoundsSize && j > -totalBoundsSize && j < totalBoundsSize)
						{
							DungeonUtils.ChangeWallType(tile, wallType, false, this.settings.OverridePaintWall);
						}
					}
				}
			}
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x006046BC File Offset: 0x006028BC
		public void GenerateDungeonSquareRoom(DungeonData data, Vector2D currentPoint, ushort tileType, ushort tileCrackedType, ushort wallType, int innerBoundsSize, int outerBoundsSize, bool crackedBricks = false)
		{
			int num = innerBoundsSize + outerBoundsSize;
			for (int i = -num; i <= num; i++)
			{
				int num2 = (int)currentPoint.X + i;
				for (int j = -num; j <= num; j++)
				{
					int num3 = (int)currentPoint.Y + j;
					Tile tile = Main.tile[num2, num3];
					if (Math.Abs(i) <= innerBoundsSize && Math.Abs(j) <= innerBoundsSize)
					{
						if (crackedBricks)
						{
							if ((tile.active() || !DungeonUtils.IsConsideredDungeonWall((int)tile.wall, false)) && num3 < Main.UnderworldLayer)
							{
								tile.ClearTile();
								tile.wall = 0;
								DungeonUtils.ChangeWallType(tile, wallType, false, this.settings.OverridePaintWall);
								DungeonUtils.ChangeTileType(tile, tileCrackedType, false, this.settings.OverridePaintTile);
							}
						}
						else
						{
							tile.ClearTile();
							DungeonUtils.ChangeWallType(tile, wallType, false, this.settings.OverridePaintWall);
						}
					}
					else if ((tile.active() && !DungeonUtils.IsHigherOrEqualTieredDungeonTile(data, (int)tile.type, (int)tileType)) || !DungeonUtils.IsConsideredDungeonWall((int)tile.wall, false))
					{
						tile.ClearTile();
						tile.wall = 0;
						DungeonUtils.ChangeTileType(tile, tileType, false, this.settings.OverridePaintTile);
						if (i > -num && i < num && j > -num && j < num)
						{
							DungeonUtils.ChangeWallType(tile, wallType, false, this.settings.OverridePaintWall);
						}
					}
					tile.liquid = 0;
				}
			}
		}

		// Token: 0x04005A31 RID: 23089
		public DungeonRoomSettings settings;

		// Token: 0x04005A32 RID: 23090
		public bool calculated;

		// Token: 0x04005A33 RID: 23091
		public bool generated;

		// Token: 0x04005A34 RID: 23092
		public DungeonBounds InnerBounds = new DungeonBounds();

		// Token: 0x04005A35 RID: 23093
		public DungeonBounds OuterBounds = new DungeonBounds();
	}
}
