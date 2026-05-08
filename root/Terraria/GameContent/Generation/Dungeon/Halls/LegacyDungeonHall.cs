using System;
using System.Collections.Generic;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon.Rooms;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Halls
{
	// Token: 0x020004C9 RID: 1225
	public class LegacyDungeonHall : DungeonHall
	{
		// Token: 0x060034C7 RID: 13511 RVA: 0x00606EB1 File Offset: 0x006050B1
		public LegacyDungeonHall(DungeonHallSettings settings)
			: base(settings)
		{
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x0060856C File Offset: 0x0060676C
		public override void CalculatePlatformsAndDoors(DungeonData data)
		{
			if (!base.Processed)
			{
				return;
			}
			DungeonUtils.CalculatePlatformAndDoorsOnHallway(data, this.StartPosition, this.StartDirection.Y, this.settings.ForceStyleForDoorsAndPlatforms ? this.settings.StyleData : null, 0.1);
			DungeonUtils.CalculatePlatformAndDoorsOnHallway(data, this.EndPosition, this.EndDirection.Y, this.settings.ForceStyleForDoorsAndPlatforms ? this.settings.StyleData : null, 0.1);
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x006085F8 File Offset: 0x006067F8
		public override void CalculateHall(DungeonData data, Vector2D startPoint, Vector2D endPoint)
		{
			this.calculated = false;
			this.OverrideStartPosition = startPoint;
			this.OverrideEndPosition = endPoint;
			this.LegacyHall(data, 0, 0, false);
			this.calculated = true;
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x00608620 File Offset: 0x00606820
		public override void GenerateHall(DungeonData data)
		{
			this.generated = false;
			this.LegacyHall(data, 0, 0, true);
			this.generated = true;
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x0060863A File Offset: 0x0060683A
		public bool GenerateHall(DungeonData data, int x, int y)
		{
			this.generated = false;
			this.LegacyHall(data, x, y, true);
			this.generated = true;
			return true;
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x00608658 File Offset: 0x00606858
		public virtual void LegacyHall(DungeonData dungeonData, int i, int j, bool generating = false)
		{
			LegacyDungeonHallSettings legacyDungeonHallSettings = (LegacyDungeonHallSettings)this.settings;
			UnifiedRandom unifiedRandom = new UnifiedRandom(legacyDungeonHallSettings.RandomSeed);
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickCrackedTileType = this.settings.StyleData.BrickCrackedTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			Vector2D vector2D;
			vector2D..ctor((double)i, (double)j);
			Vector2D vector2D2 = vector2D;
			Vector2D vector2D3 = Vector2D.Zero;
			int num = (int)(4.0 * dungeonData.hallStrengthScalar) + unifiedRandom.Next(2);
			Vector2D zero = Vector2D.Zero;
			Vector2D zero2 = Vector2D.Zero;
			double hallStepScalar = dungeonData.hallStepScalar;
			int k = (int)(35.0 * hallStepScalar) + unifiedRandom.Next(45);
			bool flag = false;
			if (legacyDungeonHallSettings.CrackedBrickChance > 0.0)
			{
				flag = unifiedRandom.NextDouble() <= legacyDungeonHallSettings.CrackedBrickChance;
			}
			if (legacyDungeonHallSettings.ForceHorizontal)
			{
				k += (int)(20.0 * hallStepScalar);
				dungeonData.lastDungeonHall = Vector2D.Zero;
			}
			else
			{
				if (unifiedRandom.Next(5) == 0)
				{
					num *= 2;
					k /= 2;
				}
				if (WorldGen.SecretSeed.errorWorld.Enabled && unifiedRandom.Next(2) == 0)
				{
					num *= 2;
				}
				if (WorldGen.SecretSeed.errorWorld.Enabled && unifiedRandom.Next(2) == 0)
				{
					k *= 2;
				}
			}
			Vector2D vector2D4 = dungeonData.lastDungeonHall;
			if (this.calculated)
			{
				vector2D2 = (vector2D = this.StartPosition);
				vector2D3 = (this.EndPosition - this.StartPosition).SafeNormalize(Vector2D.UnitX);
				num = this.Strength;
				k = this.Steps;
				vector2D4 = this.LastHall;
			}
			int num2 = k;
			int num3 = num;
			double num4 = dungeonData.hallInteriorToExteriorRatio;
			if ((float)legacyDungeonHallSettings.OverrideStrength > 0f)
			{
				num3 = (num = legacyDungeonHallSettings.OverrideStrength);
			}
			if (legacyDungeonHallSettings.OverrideSteps > 0)
			{
				num2 = (k = legacyDungeonHallSettings.OverrideSteps);
			}
			if (legacyDungeonHallSettings.OverrideInteriorToExteriorRatio > 0.0)
			{
				num4 = legacyDungeonHallSettings.OverrideInteriorToExteriorRatio;
			}
			bool flag2 = false;
			int num5 = Main.UnderworldLayer - (int)(100.0 * ((dungeonData.HallSizeScalar > dungeonData.RoomSizeScalar) ? dungeonData.HallSizeScalar : dungeonData.RoomSizeScalar));
			bool flag3 = false;
			if (this.OverrideStartPosition != default(Vector2D) && this.OverrideEndPosition != default(Vector2D))
			{
				flag3 = true;
				Vector2D overrideStartPosition = this.OverrideStartPosition;
				Vector2D vector2D5 = this.OverrideEndPosition - overrideStartPosition;
				Vector2D vector2D6 = vector2D5.SafeNormalize(Vector2D.UnitX);
				num2 = (k = (int)Math.Ceiling(vector2D5.Length() / vector2D6.Length()));
				vector2D = overrideStartPosition;
				vector2D2 = vector2D;
				zero.X = vector2D6.X;
				zero.Y = vector2D6.Y;
				zero2.X = -vector2D6.X;
				zero2.Y = -vector2D6.Y;
				vector2D3 = vector2D6;
			}
			else
			{
				bool flag4 = false;
				bool flag5 = true;
				while (!flag4)
				{
					bool flag6 = false;
					int num10;
					if (flag5 && !legacyDungeonHallSettings.ForceHorizontal)
					{
						bool flag7 = true;
						bool flag8 = true;
						bool flag9 = true;
						bool flag10 = true;
						bool flag11 = false;
						int num6 = k;
						bool flag12 = false;
						for (int l = j; l > j - num6; l--)
						{
							if (!WorldGen.InWorld(i, l, 50))
							{
								flag7 = false;
								break;
							}
							if (DungeonUtils.IsConsideredDungeonWall((int)Main.tile[i, l].wall, false))
							{
								if (flag12)
								{
									flag7 = false;
									break;
								}
							}
							else
							{
								flag12 = true;
							}
						}
						flag12 = false;
						for (int m = j; m < j + num6; m++)
						{
							if (!WorldGen.InWorld(i, m, 50))
							{
								flag8 = false;
								break;
							}
							if (m >= num5)
							{
								flag11 = true;
								flag8 = false;
								break;
							}
							if (DungeonUtils.IsConsideredDungeonWall((int)Main.tile[i, m].wall, false))
							{
								if (flag12)
								{
									flag8 = false;
									break;
								}
							}
							else
							{
								flag12 = true;
							}
						}
						flag12 = false;
						for (int n = i; n > i - num6; n--)
						{
							if (!WorldGen.InWorld(n, j, 50))
							{
								flag9 = false;
								break;
							}
							if (DungeonUtils.IsConsideredDungeonWall((int)Main.tile[n, j].wall, false))
							{
								if (flag12)
								{
									flag9 = false;
									break;
								}
							}
							else
							{
								flag12 = true;
							}
						}
						flag12 = false;
						for (int num7 = i; num7 < i + num6; num7++)
						{
							if (!WorldGen.InWorld(num7, j, 50))
							{
								flag10 = false;
								break;
							}
							if (DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num7, j].wall, false))
							{
								if (flag12)
								{
									flag10 = false;
									break;
								}
							}
							else
							{
								flag12 = true;
							}
						}
						if (flag9 || flag10 || flag7 || flag8)
						{
							int num8 = 100;
							int num9;
							do
							{
								num8--;
								if (num8 <= 0)
								{
									goto Block_39;
								}
								num9 = unifiedRandom.Next(4);
								if (num9 == 1 && flag11)
								{
									num9 = ((unifiedRandom.Next(2) == 0) ? 2 : 3);
								}
							}
							while ((num9 != 0 || !flag7) && (num9 != 1 || !flag8) && (num9 != 2 || !flag9) && (num9 != 3 || !flag10));
							IL_0529:
							if (num9 == 0)
							{
								num10 = -1;
								goto IL_058A;
							}
							if (num9 == 1)
							{
								num10 = 1;
								goto IL_058A;
							}
							flag6 = true;
							if (num9 == 2)
							{
								num10 = -1;
								goto IL_058A;
							}
							num10 = 1;
							goto IL_058A;
							Block_39:
							num9 = 0;
							goto IL_0529;
						}
						if (unifiedRandom.Next(2) == 0)
						{
							num10 = -1;
						}
						else
						{
							num10 = 1;
						}
						if (unifiedRandom.Next(2) == 0)
						{
							flag6 = true;
						}
						if (num10 == 1 && !flag6 && flag11)
						{
							num10 = ((unifiedRandom.Next(2) == 0) ? 1 : (-1));
							flag6 = true;
						}
					}
					else
					{
						if (unifiedRandom.Next(2) == 0)
						{
							num10 = -1;
						}
						else
						{
							num10 = 1;
						}
						if (unifiedRandom.Next(2) == 0)
						{
							flag6 = true;
						}
						if (num10 == 1 && j + k >= num5)
						{
							num10 = ((unifiedRandom.Next(2) == 0) ? (-1) : 1);
							flag6 = true;
						}
					}
					IL_058A:
					flag5 = false;
					if (legacyDungeonHallSettings.ForceHorizontal)
					{
						flag6 = true;
					}
					if (flag6)
					{
						zero.Y = 0.0;
						zero.X = (double)num10;
						zero2.Y = 0.0;
						zero2.X = (double)(-(double)num10);
						vector2D3.Y = 0.0;
						vector2D3.X = (double)num10;
						if (unifiedRandom.Next(3) == 0)
						{
							if (unifiedRandom.Next(2) == 0)
							{
								vector2D3.Y = -0.20000000298023224 * dungeonData.hallSlantVariantScalar;
							}
							else
							{
								vector2D3.Y = 0.20000000298023224 * dungeonData.hallSlantVariantScalar;
							}
						}
					}
					else
					{
						num++;
						vector2D3.Y = (double)num10;
						vector2D3.X = 0.0;
						zero.X = 0.0;
						zero.Y = (double)num10;
						zero2.X = 0.0;
						zero2.Y = (double)(-(double)num10);
						if (legacyDungeonHallSettings.ZigzagChance > 0.0 && unifiedRandom.NextDouble() <= legacyDungeonHallSettings.ZigzagChance)
						{
							flag2 = true;
							if (unifiedRandom.Next(2) == 0)
							{
								vector2D3.X = (double)unifiedRandom.Next(10, 20) * 0.1 * dungeonData.hallSlantVariantScalar;
							}
							else
							{
								vector2D3.X = (double)(-(double)unifiedRandom.Next(10, 20)) * 0.1 * dungeonData.hallSlantVariantScalar;
							}
						}
						else if (unifiedRandom.Next(2) == 0)
						{
							if (unifiedRandom.Next(2) == 0)
							{
								vector2D3.X = (double)unifiedRandom.Next(20, 40) * 0.01 * dungeonData.hallSlantVariantScalar;
							}
							else
							{
								vector2D3.X = (double)(-(double)unifiedRandom.Next(20, 40)) * 0.01 * dungeonData.hallSlantVariantScalar;
							}
						}
						else
						{
							k /= 2;
						}
					}
					if (dungeonData.lastDungeonHall != zero2)
					{
						flag4 = true;
					}
				}
			}
			int num11 = 0;
			float num12 = (float)Main.maxTilesX * 0.25f;
			float num13 = (float)Main.maxTilesX * 0.75f;
			if (WorldGen.SecretSeed.errorWorld.Enabled)
			{
				num12 = (float)Main.maxTilesX * 0.4f;
				num13 = (float)Main.maxTilesX * 0.6f;
			}
			bool flag13 = vector2D.Y < Main.rockLayer + 100.0;
			if (WorldGen.remixWorldGen)
			{
				flag13 = vector2D.Y < Main.worldSurface + 100.0;
			}
			bool flag14 = vector2D.X < (double)(Main.maxTilesX / 2) && vector2D.X > (double)num12;
			bool flag15 = vector2D.X > (double)(Main.maxTilesX / 2) && vector2D.X < (double)num13;
			if (!flag3 && !legacyDungeonHallSettings.ForceHorizontal)
			{
				if (vector2D.X > (double)(Main.maxTilesX - 200))
				{
					int num10 = -1;
					zero.X = (double)num10;
					zero.Y = 0.0;
					vector2D3.X = (double)num10;
					vector2D3.Y = 0.0;
					if (unifiedRandom.Next(3) == 0)
					{
						if (unifiedRandom.Next(2) == 0)
						{
							vector2D3.Y = -0.20000000298023224 * dungeonData.hallSlantVariantScalar;
						}
						else
						{
							vector2D3.Y = 0.20000000298023224 * dungeonData.hallSlantVariantScalar;
						}
					}
				}
				else if (vector2D.X < 200.0)
				{
					int num10 = 1;
					zero.X = (double)num10;
					zero.Y = 0.0;
					vector2D3.X = (double)num10;
					vector2D3.Y = 0.0;
					if (unifiedRandom.Next(3) == 0)
					{
						if (unifiedRandom.Next(2) == 0)
						{
							vector2D3.Y = -0.20000000298023224 * dungeonData.hallSlantVariantScalar;
						}
						else
						{
							vector2D3.Y = 0.20000000298023224 * dungeonData.hallSlantVariantScalar;
						}
					}
				}
				else if (vector2D.Y >= (double)num5)
				{
					int num10 = -1;
					num++;
					zero.X = 0.0;
					zero.Y = (double)num10;
					vector2D3.X = 0.0;
					vector2D3.Y = (double)num10;
					if (unifiedRandom.Next(2) == 0)
					{
						if (unifiedRandom.Next(2) == 0)
						{
							vector2D3.X = (double)((float)unifiedRandom.Next(20, 50) * 0.01f) * dungeonData.hallSlantVariantScalar;
						}
						else
						{
							vector2D3.X = (double)((float)(-(float)unifiedRandom.Next(20, 50)) * 0.01f) * dungeonData.hallSlantVariantScalar;
						}
					}
				}
				else if (vector2D.Y < 200.0)
				{
					int num10 = 1;
					num++;
					zero.X = 0.0;
					zero.Y = (double)num10;
					vector2D3.X = 0.0;
					vector2D3.Y = (double)num10;
					if (unifiedRandom.Next(2) == 0)
					{
						if (unifiedRandom.Next(2) == 0)
						{
							vector2D3.X = (double)((float)unifiedRandom.Next(20, 50) * 0.01f) * dungeonData.hallSlantVariantScalar;
						}
						else
						{
							vector2D3.X = (double)((float)(-(float)unifiedRandom.Next(20, 50)) * 0.01f) * dungeonData.hallSlantVariantScalar;
						}
					}
				}
				else if (!flag3)
				{
					if (flag13)
					{
						int num10 = 1;
						num++;
						zero.X = 0.0;
						zero.Y = (double)num10;
						vector2D3.X = 0.0;
						vector2D3.Y = (double)num10;
						if (legacyDungeonHallSettings.ZigzagChance > 0.0 && unifiedRandom.NextDouble() <= legacyDungeonHallSettings.ZigzagChance)
						{
							flag2 = true;
							if (unifiedRandom.Next(2) == 0)
							{
								vector2D3.X = (double)unifiedRandom.Next(10, 20) * 0.1 * dungeonData.hallSlantVariantScalar;
							}
							else
							{
								vector2D3.X = (double)(-(double)unifiedRandom.Next(10, 20)) * 0.1 * dungeonData.hallSlantVariantScalar;
							}
						}
						else if (unifiedRandom.Next(2) == 0)
						{
							if (unifiedRandom.Next(2) == 0)
							{
								vector2D3.X = (double)unifiedRandom.Next(20, 50) * 0.01 * dungeonData.hallSlantVariantScalar;
							}
							else
							{
								vector2D3.X = (double)unifiedRandom.Next(20, 50) * 0.01 * dungeonData.hallSlantVariantScalar;
							}
						}
					}
					else if (flag14)
					{
						int num10 = -1;
						zero.Y = 0.0;
						zero.X = (double)num10;
						vector2D3.Y = 0.0;
						vector2D3.X = (double)num10;
						if (unifiedRandom.Next(3) == 0)
						{
							if (unifiedRandom.Next(2) == 0)
							{
								vector2D3.Y = -0.20000000298023224 * dungeonData.hallSlantVariantScalar;
							}
							else
							{
								vector2D3.Y = 0.20000000298023224 * dungeonData.hallSlantVariantScalar;
							}
						}
					}
					else if (flag15)
					{
						int num10 = 1;
						zero.Y = 0.0;
						zero.X = (double)num10;
						vector2D3.Y = 0.0;
						vector2D3.X = (double)num10;
						if (unifiedRandom.Next(3) == 0)
						{
							if (unifiedRandom.Next(2) == 0)
							{
								vector2D3.Y = -0.20000000298023224 * dungeonData.hallSlantVariantScalar;
							}
							else
							{
								vector2D3.Y = 0.20000000298023224 * dungeonData.hallSlantVariantScalar;
							}
						}
					}
				}
			}
			Vector2D vector2D7 = zero;
			dungeonData.lastDungeonHall = zero;
			if (!this.calculated && !flag3 && Math.Abs(vector2D3.X) > Math.Abs(vector2D3.Y) && unifiedRandom.Next(3) != 0)
			{
				num = (int)((float)num3 * ((float)unifiedRandom.Next(110, 150) * 0.01f));
			}
			if (!base.Processed)
			{
				this.Bounds.SetBounds((int)vector2D.X, (int)vector2D.Y, (int)vector2D.X, (int)vector2D.Y);
			}
			Vector2D vector2D8 = vector2D;
			Vector2D vector2D9 = vector2D + vector2D3 * (double)k;
			DungeonRoomSearchSettings dungeonRoomSearchSettings = new DungeonRoomSearchSettings
			{
				Fluff = k / 2 + num
			};
			List<DungeonRoom> allRoomsInSpots = DungeonUtils.GetAllRoomsInSpots(dungeonData.dungeonRooms, vector2D8, vector2D9, dungeonRoomSearchSettings);
			while (k > 0)
			{
				num11++;
				if (flag3)
				{
					if (!WorldGen.InWorld((int)(vector2D.X + zero.X), (int)(vector2D.Y + zero.Y), 10))
					{
						k = 0;
					}
				}
				else if (zero.X > 0.0 && vector2D.X > (double)(Main.maxTilesX - 100))
				{
					k = 0;
				}
				else if (zero.X < 0.0 && vector2D.X < 100.0)
				{
					k = 0;
				}
				else if (zero.Y > 0.0 && vector2D.Y >= (double)num5)
				{
					k = 0;
				}
				else if (zero.Y < 0.0 && vector2D.Y < 100.0)
				{
					k = 0;
				}
				else if (WorldGen.remixWorldGen && zero.Y < 0.0 && vector2D.Y < (Main.rockLayer + Main.worldSurface) / 2.0)
				{
					k = 0;
				}
				else if (!WorldGen.remixWorldGen && zero.Y < 0.0 && vector2D.Y < Main.rockLayer + 50.0)
				{
					k = 0;
				}
				k--;
				int num14 = Math.Max(0, Math.Min(Main.maxTilesX - 1, (int)(vector2D.X - (double)num - 4.0 - (double)unifiedRandom.Next(6))));
				int num15 = Math.Max(0, Math.Min(Main.maxTilesX - 1, (int)(vector2D.X + (double)num + 4.0 + (double)unifiedRandom.Next(6))));
				int num16 = Math.Max(0, Math.Min(Main.maxTilesY - 1, (int)(vector2D.Y - (double)num - 4.0 - (double)unifiedRandom.Next(6))));
				int num17 = Math.Max(0, Math.Min(Main.maxTilesY - 1, (int)(vector2D.Y + (double)num + 4.0 + (double)unifiedRandom.Next(6))));
				if (!base.Processed)
				{
					dungeonData.dungeonBounds.UpdateBounds(num14, num16, num15, num17);
					this.Bounds.UpdateBounds(num14, num16, num15, num17);
				}
				if (generating && !this.settings.CarveOnly)
				{
					for (int num18 = num14; num18 < num15; num18++)
					{
						for (int num19 = num16; num19 < num17; num19++)
						{
							bool flag16 = true;
							ProtectionType highestProtectionTypeFromPoint = DungeonUtils.GetHighestProtectionTypeFromPoint(num18, num19, allRoomsInSpots);
							if (highestProtectionTypeFromPoint != ProtectionType.TilesAndWalls)
							{
								if (highestProtectionTypeFromPoint == ProtectionType.Tiles)
								{
									flag16 = false;
								}
								Tile tile = Main.tile[num18, num19];
								tile.liquid = 0;
								if (flag16 && num19 <= Main.UnderworldLayer + 7 && this.CanPlaceTileAt(dungeonData, tile, (int)brickTileType, (int)brickCrackedTileType))
								{
									DungeonUtils.ChangeTileType(tile, brickTileType, true, this.settings.OverridePaintTile);
								}
							}
						}
					}
					for (int num20 = num14 + 1; num20 < num15 - 1; num20++)
					{
						for (int num21 = num16 + 1; num21 < num17 - 1; num21++)
						{
							if (num21 < Main.UnderworldLayer + 7)
							{
								bool flag17 = true;
								ProtectionType highestProtectionTypeFromPoint2 = DungeonUtils.GetHighestProtectionTypeFromPoint(num20, num21, allRoomsInSpots);
								if (highestProtectionTypeFromPoint2 != ProtectionType.TilesAndWalls)
								{
									if (highestProtectionTypeFromPoint2 == ProtectionType.Walls && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num20, num21].wall, false))
									{
										flag17 = false;
									}
									if (flag17)
									{
										DungeonUtils.ChangeWallType(Main.tile[num20, num21], brickWallType, false, this.settings.OverridePaintWall);
									}
								}
							}
						}
					}
				}
				if (generating)
				{
					int num22 = 0;
					if (vector2D3.Y == 0.0 && unifiedRandom.Next(num + 1) == 0)
					{
						num22 = unifiedRandom.Next(1, 3);
					}
					else if (vector2D3.X == 0.0 && unifiedRandom.Next(num - 1) == 0)
					{
						num22 = unifiedRandom.Next(1, 3);
					}
					else if (unifiedRandom.Next(num * 3) == 0)
					{
						num22 = unifiedRandom.Next(1, 3);
					}
					num14 = Math.Max(0, Math.Min(Main.maxTilesX - 1, (int)(vector2D.X - (double)num * num4 - (double)num22)));
					num15 = Math.Max(0, Math.Min(Main.maxTilesX - 1, (int)(vector2D.X + (double)num * num4 + (double)num22)));
					num16 = Math.Max(0, Math.Min(Main.maxTilesY - 1, (int)(vector2D.Y - (double)num * num4 - (double)num22)));
					num17 = Math.Max(0, Math.Min(Main.maxTilesY - 1, (int)(vector2D.Y + (double)num * num4 + (double)num22)));
					for (int num23 = num14; num23 < num15; num23++)
					{
						for (int num24 = num16; num24 < num17; num24++)
						{
							bool flag18 = true;
							bool flag19 = true;
							ProtectionType highestProtectionTypeFromPoint3 = DungeonUtils.GetHighestProtectionTypeFromPoint(num23, num24, allRoomsInSpots);
							if (highestProtectionTypeFromPoint3 != ProtectionType.TilesAndWalls)
							{
								if (highestProtectionTypeFromPoint3 == ProtectionType.Tiles)
								{
									flag18 = false;
								}
								if (highestProtectionTypeFromPoint3 == ProtectionType.Walls && DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num23, num24].wall, false))
								{
									flag19 = false;
								}
								if (this.CanRemoveTileAt(dungeonData, Main.tile[num23, num24], (int)brickCrackedTileType))
								{
									if (flag)
									{
										if ((Main.tile[num23, num24].active() || !DungeonUtils.IsConsideredDungeonWall((int)Main.tile[num23, num24].wall, false)) && num24 < Main.UnderworldLayer)
										{
											if (this.settings.CarveOnly)
											{
												Main.tile[num23, num24].ClearTile();
											}
											else
											{
												Main.tile[num23, num24].ClearTile();
												if (flag18)
												{
													DungeonUtils.ChangeTileType(Main.tile[num23, num24], brickCrackedTileType, false, this.settings.OverridePaintTile);
												}
											}
										}
									}
									else
									{
										Main.tile[num23, num24].ClearTile();
									}
									if (flag19 && num24 < Main.UnderworldLayer && !this.settings.CarveOnly)
									{
										DungeonUtils.ChangeWallType(Main.tile[num23, num24], brickWallType, false, this.settings.OverridePaintWall);
									}
								}
							}
						}
					}
				}
				vector2D += vector2D3;
				if (!flag3 && flag2 && num11 > unifiedRandom.Next(10, 20))
				{
					num11 = 0;
					vector2D3.X *= -1.0;
				}
			}
			dungeonData.genVars.generatingDungeonPositionX = (int)vector2D.X;
			dungeonData.genVars.generatingDungeonPositionY = (int)vector2D.Y;
			this.StartPosition = vector2D2;
			this.EndPosition = vector2D;
			this.StartDirection = vector2D7;
			this.EndDirection = zero;
			this.Strength = num3;
			this.Steps = num2;
			this.LastHall = vector2D4;
			this.CrackedBrick = flag;
			if (!base.Processed)
			{
				this.Bounds.CalculateHitbox();
			}
		}

		// Token: 0x04005A6A RID: 23146
		public Vector2D LastHall;

		// Token: 0x04005A6B RID: 23147
		public int Strength;

		// Token: 0x04005A6C RID: 23148
		public int Steps;

		// Token: 0x04005A6D RID: 23149
		protected Vector2D OverrideStartPosition;

		// Token: 0x04005A6E RID: 23150
		protected Vector2D OverrideEndPosition;
	}
}
