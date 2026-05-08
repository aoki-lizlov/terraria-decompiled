using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation.Dungeon.Features;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004A6 RID: 1190
	public class BiomeRuggedDungeonRoom : BiomeDungeonRoom
	{
		// Token: 0x0600342D RID: 13357 RVA: 0x006012E3 File Offset: 0x005FF4E3
		public BiomeRuggedDungeonRoom(DungeonRoomSettings settings)
			: base(settings)
		{
			this._innerShapeData = new ShapeData();
			this._outerShapeData = new ShapeData();
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x00601304 File Offset: 0x005FF504
		public override void CalculateRoom(DungeonData data)
		{
			this.calculated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.BiomeRoom(data, x, y, false);
			this.calculated = true;
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x0060134C File Offset: 0x005FF54C
		public override bool GenerateRoom(DungeonData data)
		{
			this.generated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.BiomeRoom(data, x, y, true);
			this.generated = true;
			return true;
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x00601394 File Offset: 0x005FF594
		public override bool CanGenerateFeatureAt(DungeonData data, IDungeonFeature feature, int x, int y)
		{
			return !(feature is DungeonGlobalBanners) && !(feature is DungeonGlobalPaintings) && base.CanGenerateFeatureAt(data, feature, x, y);
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x006013B4 File Offset: 0x005FF5B4
		public override void GenerateEarlyDungeonFeaturesInRoom(DungeonData data)
		{
			new UnifiedRandom(this.settings.RandomSeed);
			BiomeDungeonRoomSettings biomeDungeonRoomSettings = (BiomeDungeonRoomSettings)this.settings;
			bool flag = this.settings.StyleData.Style == 0;
			ushort num = (flag ? data.genVars.brickTileType : this.settings.StyleData.BrickTileType);
			if (!flag)
			{
				ushort brickWallType = this.settings.StyleData.BrickWallType;
			}
			else
			{
				ushort brickWallType2 = data.genVars.brickWallType;
			}
			DungeonUtils.GenerateSpeleothemsInArea(data, biomeDungeonRoomSettings.StyleData, this.InnerBounds, this.InnerBounds.Width / 6, num, biomeDungeonRoomSettings.OverridePaintTile, -1, -1);
			DungeonUtils.GenerateFloatingRocksInArea(data, biomeDungeonRoomSettings.StyleData, this.InnerBounds, num, true, biomeDungeonRoomSettings.OverridePaintTile);
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x00601478 File Offset: 0x005FF678
		public void BiomeRoom(DungeonData data, int i, int j, bool generating)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(this.settings.RandomSeed);
			BiomeDungeonRoomSettings biomeDungeonRoomSettings = (BiomeDungeonRoomSettings)this.settings;
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			Vector2 position = new Vector2((float)i, (float)j);
			int num = BiomeDungeonRoom.GetBiomeRoomInnerSize(biomeDungeonRoomSettings.StyleData);
			int num2 = 8;
			int num3 = BiomeDungeonRoom.GetBiomeRoomOuterSize(biomeDungeonRoomSettings.StyleData);
			if (this.calculated)
			{
				position = this.Position;
				num = this.RoomInnerSize;
				num3 = this.RoomOuterSize;
				num2 = this.WallDepth;
			}
			int num4 = 20;
			int num5 = Math.Max(num4 + num2, Math.Min(Main.maxTilesX - num4 - num2, (int)position.X - num));
			int num6 = Math.Max(num4 + num2, Math.Min(Main.maxTilesX - num4 - num2, (int)position.X + num));
			int num7 = Math.Max(num4 + num2, Math.Min(Main.maxTilesY - num4 - num2, (int)position.Y - num));
			int num8 = Math.Max(num4 + num2, Math.Min(Main.maxTilesY - num4 - num2, (int)position.Y + num));
			int num9 = Math.Max(num4, Math.Min(Main.maxTilesX - num4, (int)position.X - num3));
			int num10 = Math.Max(num4, Math.Min(Main.maxTilesX - num4, (int)position.X + num3));
			int num11 = Math.Max(num4, Math.Min(Main.maxTilesY - num4, (int)position.Y - num3));
			int num12 = Math.Max(num4, Math.Min(Main.maxTilesY - num4, (int)position.Y + num3));
			this.InnerBounds.SetBounds(num5, num7, num6, num8);
			this.OuterBounds.SetBounds(num9, num11, num10, num12);
			data.dungeonBounds.UpdateBounds(num9, num11, num10, num12);
			int num13 = num8 - num7;
			Point center = this.InnerBounds.Center;
			int num14 = Math.Max(8, num2);
			for (int k = num9; k < num10; k++)
			{
				int num15 = k;
				float num16 = Utils.MultiLerp(Math.Max(0f, Math.Min(1f, (float)(k - num9) / Math.Max(1f, (float)(num10 - num9)))), new float[]
				{
					1f, 0.95f, 0.85f, 0.25f, 0.1f, 0.05f, 0f, 0.05f, 0.1f, 0.25f,
					0.85f, 0.95f, 1f
				});
				float num17 = (float)Math.Max(1, num13 / 16) + (float)Math.Max(1, num13 / 4) * num16;
				float num18 = (float)num7 + num17 + (float)unifiedRandom.Next(3);
				float num19 = num18 - (float)num14;
				float num20 = (float)num8 - num17 - 1f - (float)unifiedRandom.Next(3);
				float num21 = num20 + (float)num14;
				for (int l = num11; l < num12; l++)
				{
					bool flag = false;
					bool flag2 = false;
					int num22 = l;
					Tile tile = Main.tile[num15, num22];
					Main.tile[num15, num22 - 1];
					Main.tile[num15, num22 + 1];
					Main.tile[num15, num22 + 2];
					if (generating && (tile.type == 484 || tile.type == 485))
					{
						tile.active(false);
					}
					if ((float)l >= num19 && (float)l <= num21)
					{
						if (k < num9 + num2 - 1 || k >= num10 - num2 + 1 || ((float)l >= num19 && (float)l <= num18) || ((float)l >= num20 && (float)l <= num21))
						{
							if (!generating)
							{
								this._outerShapeData.Add(num15 - (int)position.X, num22 - (int)position.Y);
							}
							else
							{
								DungeonUtils.ChangeTileType(tile, brickTileType, false, biomeDungeonRoomSettings.OverridePaintTile);
								if (tile.liquid > 0)
								{
									if (flag2)
									{
										tile.liquid = 0;
									}
									tile.liquidType(0);
								}
								if (flag)
								{
									DungeonUtils.ChangeWallType(tile, 0, false, biomeDungeonRoomSettings.OverridePaintWall);
								}
								else
								{
									DungeonUtils.ChangeWallType(tile, brickWallType, false, biomeDungeonRoomSettings.OverridePaintWall);
								}
							}
						}
						else if (this.InnerBounds.Contains(num15, num22))
						{
							if (!generating)
							{
								this._outerShapeData.Add(num15 - (int)position.X, num22 - (int)position.Y);
								this._innerShapeData.Add(num15 - (int)position.X, num22 - (int)position.Y);
							}
							else
							{
								if (tile.liquid > 0)
								{
									if (flag2)
									{
										tile.liquid = 0;
									}
									tile.liquidType(0);
								}
								if (flag)
								{
									DungeonUtils.ChangeWallType(tile, 0, false, biomeDungeonRoomSettings.OverridePaintWall);
								}
								else
								{
									DungeonUtils.ChangeWallType(tile, brickWallType, false, biomeDungeonRoomSettings.OverridePaintWall);
								}
								tile.active(false);
								tile.ClearBlockPaintAndCoating();
							}
						}
						else if (!generating)
						{
							this._outerShapeData.Add(num15 - (int)position.X, num22 - (int)position.Y);
						}
						else
						{
							bool flag3 = k == num9 || k == num10 - 1 || l == num11 || l == num12 - 1 || k == num5 - 1 || k == num6 || l == num7 - 1 || l == num8;
							if (tile.liquid > 0)
							{
								tile.liquid = 0;
							}
							DungeonUtils.ChangeTileType(tile, brickTileType, true, biomeDungeonRoomSettings.OverridePaintTile);
							if (!flag3)
							{
								DungeonUtils.ChangeWallType(tile, brickWallType, false, biomeDungeonRoomSettings.OverridePaintWall);
							}
						}
					}
				}
			}
			base.BiomeRoom_AddHallwaySpace(position, num5, num6, num7, num8, 0, brickWallType, this.settings.OverridePaintWall, generating);
			base.BiomeRoom_FinishRoom(unifiedRandom, num9, num10, num11, num12, false);
			this.RoomInnerSize = num;
			this.RoomOuterSize = num3;
			this.WallDepth = num2;
			this.Position = position;
			this.InnerBounds.CalculateHitbox();
			this.OuterBounds.CalculateHitbox();
		}

		// Token: 0x040059DF RID: 23007
		public Vector2 Position;

		// Token: 0x040059E0 RID: 23008
		public int RoomInnerSize;

		// Token: 0x040059E1 RID: 23009
		public int RoomOuterSize;

		// Token: 0x040059E2 RID: 23010
		public int WallDepth;
	}
}
