using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon.Rooms;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Halls
{
	// Token: 0x020004C7 RID: 1223
	public class LegacyEntranceDungeonHall : LegacyDungeonHall
	{
		// Token: 0x060034C3 RID: 13507 RVA: 0x00607A9D File Offset: 0x00605C9D
		public LegacyEntranceDungeonHall(DungeonHallSettings settings)
			: base(settings)
		{
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x00009E46 File Offset: 0x00008046
		public override void CalculatePlatformsAndDoors(DungeonData data)
		{
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x00607AA8 File Offset: 0x00605CA8
		public override void LegacyHall(DungeonData dungeonData, int i, int j, bool generating = false)
		{
			LegacyEntranceDungeonHallSettings legacyEntranceDungeonHallSettings = (LegacyEntranceDungeonHallSettings)this.settings;
			UnifiedRandom unifiedRandom = new UnifiedRandom(legacyEntranceDungeonHallSettings.RandomSeed);
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickCrackedTileType = this.settings.StyleData.BrickCrackedTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			Vector2D vector2D = Vector2D.Zero;
			Vector2D vector2D2 = Vector2D.Zero;
			int num = Main.maxTilesX / 2;
			int num2 = unifiedRandom.Next(5, 9);
			vector2D.X = (double)i;
			vector2D.Y = (double)j;
			Vector2D vector2D3 = vector2D;
			int k = unifiedRandom.Next(10, 30);
			int num3 = ((i > dungeonData.genVars.generatingDungeonTopX) ? (-1) : 1);
			if (i > Main.maxTilesX - 400)
			{
				num3 = -1;
			}
			else if (i < 400)
			{
				num3 = 1;
			}
			vector2D2.Y = -1.0;
			vector2D2.X = (double)num3;
			if (unifiedRandom.Next(3) != 0)
			{
				vector2D2.X *= (double)(1f + (float)unifiedRandom.Next(0, 200) * 0.01f);
			}
			else if (unifiedRandom.Next(3) == 0)
			{
				vector2D2.X *= (double)((float)unifiedRandom.Next(50, 76) * 0.01f);
			}
			else if (unifiedRandom.Next(6) == 0)
			{
				vector2D2.Y *= 2.0;
			}
			if (dungeonData.useSkewedDungeonEntranceHalls)
			{
				if (dungeonData.genVars.generatingDungeonPositionX < num && vector2D2.X < 0.0 && vector2D2.X < -0.5)
				{
					vector2D2.X = 0.5;
				}
				if (dungeonData.genVars.generatingDungeonPositionX > num && vector2D2.X > 0.0 && vector2D2.X > 0.5)
				{
					vector2D2.X = -0.5;
				}
			}
			else
			{
				if (dungeonData.genVars.generatingDungeonPositionX < num && vector2D2.X < -0.5)
				{
					vector2D2.X = -0.5;
				}
				if (dungeonData.genVars.generatingDungeonPositionX > num && vector2D2.X > 0.5)
				{
					vector2D2.X = 0.5;
				}
			}
			if (WorldGen.drunkWorldGen || WorldGen.SecretSeed.noSurface.Enabled)
			{
				num3 *= -1;
				vector2D2.X *= -1.0;
			}
			if (this.calculated)
			{
				vector2D3 = (vector2D = this.StartPosition);
				vector2D2 = (this.EndPosition - this.StartPosition).SafeNormalize(Vector2D.UnitX);
				num3 = this.Direction;
				num2 = this.Strength;
				k = this.Steps;
			}
			int num4 = num2;
			int num5 = k;
			double num6 = dungeonData.hallInteriorToExteriorRatio;
			if ((float)legacyEntranceDungeonHallSettings.OverrideStrength > 0f)
			{
				num4 = (num2 = legacyEntranceDungeonHallSettings.OverrideStrength);
			}
			if (legacyEntranceDungeonHallSettings.OverrideSteps > 0)
			{
				num5 = (k = legacyEntranceDungeonHallSettings.OverrideSteps);
			}
			if (legacyEntranceDungeonHallSettings.OverrideInteriorToExteriorRatio > 0.0)
			{
				num6 = legacyEntranceDungeonHallSettings.OverrideInteriorToExteriorRatio;
			}
			bool flag = false;
			if (this.OverrideStartPosition != default(Vector2D) && this.OverrideEndPosition != default(Vector2D))
			{
				flag = true;
				Vector2D overrideStartPosition = this.OverrideStartPosition;
				Vector2D vector2D4 = this.OverrideEndPosition - overrideStartPosition;
				Vector2D vector2D5 = vector2D4.SafeNormalize(Vector2D.UnitX);
				num5 = (k = (int)Math.Ceiling(vector2D4.Length() / vector2D5.Length()));
				vector2D3 = (vector2D = overrideStartPosition);
				vector2D2 = vector2D5;
				num3 = ((vector2D5.X > 0.0) ? 1 : (-1));
			}
			this.Bounds.SetBounds((int)vector2D.X, (int)vector2D.Y, (int)vector2D.X, (int)vector2D.Y);
			Vector2D vector2D6 = vector2D;
			Vector2D vector2D7 = vector2D + vector2D2 * (double)k;
			DungeonRoomSearchSettings dungeonRoomSearchSettings = new DungeonRoomSearchSettings
			{
				Fluff = k / 2 + num2
			};
			List<DungeonRoom> allRoomsInSpots = DungeonUtils.GetAllRoomsInSpots(dungeonData.dungeonRooms, vector2D6, vector2D7, dungeonRoomSearchSettings);
			Vector2D vector2D8 = vector2D2;
			int num7 = 30;
			int num8 = 10;
			int num9 = 0;
			while (k > 0)
			{
				k--;
				if (!WorldGen.InWorld((int)vector2D.X, (int)vector2D.Y, num7 + 5))
				{
					break;
				}
				int num10 = Math.Max(num7, Math.Min(Main.maxTilesX - num7 - 1, (int)(vector2D.X - (double)num2 - 4.0 - (double)unifiedRandom.Next(6))));
				int num11 = Math.Max(num7, Math.Min(Main.maxTilesX - num7 - 1, (int)(vector2D.X + (double)num2 + 4.0 + (double)unifiedRandom.Next(6))));
				int num12 = Math.Max(num7, Math.Min(Main.maxTilesY - num7 - 1, (int)(vector2D.Y - (double)num2 - 4.0)));
				int num13 = Math.Max(num7, Math.Min(Main.maxTilesY - num7 - 1, (int)(vector2D.Y + (double)num2 + 4.0 + (double)unifiedRandom.Next(6))));
				if (!base.Processed)
				{
					dungeonData.dungeonBounds.UpdateBounds(num10, num12, num11, num13);
					this.Bounds.UpdateBounds(num10, num12, num11, num13);
				}
				int num14 = 1;
				if (vector2D.X > (double)num)
				{
					num14 = -1;
				}
				int num15 = (int)(vector2D.X + dungeonData.dungeonEntranceStrengthX * 0.6 * (double)num14 + dungeonData.dungeonEntranceStrengthX2 * (double)num14);
				int num16 = (int)(dungeonData.dungeonEntranceStrengthY2 * 0.5);
				if (!legacyEntranceDungeonHallSettings.UsePrecalculatedEntrance && vector2D.Y < Main.worldSurface - 5.0 && ((Main.tile[num15, (int)(vector2D.Y - (double)num2 - 6.0 + (double)num16)].wall == 0 && Main.tile[num15, (int)(vector2D.Y - (double)num2 - 7.0 + (double)num16)].wall == 0 && Main.tile[num15, (int)(vector2D.Y - (double)num2 - 8.0 + (double)num16)].wall == 0) || WorldGen.SecretSeed.surfaceIsDesert.Enabled))
				{
					dungeonData.createdDungeonEntranceOnSurface = true;
					if (generating)
					{
						WorldGen.TileRunner(num15, (int)(vector2D.Y - (double)num2 - 6.0 + (double)num16), (double)unifiedRandom.Next(25, 35), unifiedRandom.Next(10, 20), -1, false, 0.0, -1.0, false, true, -1);
					}
				}
				if (generating && !this.settings.CarveOnly)
				{
					for (int l = num10; l < num11; l++)
					{
						for (int m = num12; m < num13; m++)
						{
							bool flag2 = true;
							ProtectionType highestProtectionTypeFromPoint = DungeonUtils.GetHighestProtectionTypeFromPoint(l, m, allRoomsInSpots);
							if (highestProtectionTypeFromPoint != ProtectionType.TilesAndWalls)
							{
								if (highestProtectionTypeFromPoint == ProtectionType.Tiles)
								{
									flag2 = false;
								}
								Tile tile = Main.tile[l, m];
								tile.liquid = 0;
								if (flag2 && this.CanPlaceTileAt(dungeonData, tile, (int)brickTileType, (int)brickCrackedTileType))
								{
									WorldGen.paintTile(l, m, 0, false, false);
									DungeonUtils.ChangeTileType(Main.tile[l, m], brickTileType, true, legacyEntranceDungeonHallSettings.OverridePaintTile);
								}
							}
						}
					}
					for (int n = num10 + 1; n < num11 - 1; n++)
					{
						for (int num17 = num12 + 1; num17 < num13 - 1; num17++)
						{
							bool flag3 = true;
							ProtectionType highestProtectionTypeFromPoint2 = DungeonUtils.GetHighestProtectionTypeFromPoint(n, num17, allRoomsInSpots);
							if (highestProtectionTypeFromPoint2 != ProtectionType.TilesAndWalls)
							{
								if (highestProtectionTypeFromPoint2 == ProtectionType.Walls && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[n, num17].wall, false))
								{
									flag3 = false;
								}
								if (flag3)
								{
									WorldGen.paintWall(n, num17, 0, false, false);
									DungeonUtils.ChangeWallType(Main.tile[n, num17], brickWallType, false, legacyEntranceDungeonHallSettings.OverridePaintWall);
								}
							}
						}
					}
				}
				int num18 = 0;
				if (unifiedRandom.Next(num2) == 0)
				{
					num18 = unifiedRandom.Next(1, 3);
				}
				num10 = Math.Max(num7, Math.Min(Main.maxTilesX - num7 - 1, (int)(vector2D.X - (double)num2 * num6 - (double)num18)));
				num11 = Math.Max(num7, Math.Min(Main.maxTilesX - num7 - 1, (int)(vector2D.X + (double)num2 * num6 + (double)num18)));
				num12 = Math.Max(num7, Math.Min(Main.maxTilesY - num7 - 1, (int)(vector2D.Y - (double)num2 * num6 - (double)num18)));
				num13 = Math.Max(num7, Math.Min(Main.maxTilesY - num7 - 1, (int)(vector2D.Y + (double)num2 * num6 + (double)num18)));
				if (generating)
				{
					if (flag)
					{
						num9--;
						if (num9 <= 0)
						{
							num9 = num8;
							DungeonPlatformData dungeonPlatformData = new DungeonPlatformData
							{
								Position = new Point((int)vector2D.X, (int)vector2D.Y),
								PlacePotsChance = 0.25,
								InAHallway = true
							};
							dungeonData.dungeonPlatformData.Add(dungeonPlatformData);
						}
					}
					for (int num19 = num10; num19 < num11; num19++)
					{
						for (int num20 = num12; num20 < num13; num20++)
						{
							bool flag4 = true;
							ProtectionType highestProtectionTypeFromPoint3 = DungeonUtils.GetHighestProtectionTypeFromPoint(num19, num20, allRoomsInSpots);
							if (highestProtectionTypeFromPoint3 != ProtectionType.TilesAndWalls)
							{
								if (highestProtectionTypeFromPoint3 == ProtectionType.Walls && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num19, num20].wall, false))
								{
									flag4 = false;
								}
								Main.tile[num19, num20].ClearTile();
								if (flag4 && !this.settings.CarveOnly)
								{
									DungeonUtils.ChangeWallType(Main.tile[num19, num20], brickWallType, false, legacyEntranceDungeonHallSettings.OverridePaintWall);
								}
							}
						}
					}
				}
				if (!legacyEntranceDungeonHallSettings.UsePrecalculatedEntrance && dungeonData.createdDungeonEntranceOnSurface)
				{
					k = 0;
				}
				vector2D += vector2D2;
				if (!flag && vector2D.Y < Main.worldSurface)
				{
					vector2D2.Y *= 0.9800000190734863;
				}
			}
			dungeonData.genVars.generatingDungeonPositionX = (int)vector2D.X;
			dungeonData.genVars.generatingDungeonPositionY = (int)vector2D.Y;
			this.StartPosition = vector2D3;
			this.EndPosition = vector2D;
			this.StartDirection = new Vector2D(vector2D8.X, vector2D8.Y);
			this.EndDirection = new Vector2D(vector2D2.X, vector2D2.Y);
			this.Strength = num4;
			this.Steps = num5;
			this.Direction = num3;
			if (!base.Processed)
			{
				this.Bounds.CalculateHitbox();
			}
		}

		// Token: 0x04005A69 RID: 23145
		public int Direction;
	}
}
