using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation.Dungeon.Features;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004A7 RID: 1191
	public class BiomeStructuredDungeonRoom : BiomeDungeonRoom
	{
		// Token: 0x06003433 RID: 13363 RVA: 0x006012E3 File Offset: 0x005FF4E3
		public BiomeStructuredDungeonRoom(DungeonRoomSettings settings)
			: base(settings)
		{
			this._innerShapeData = new ShapeData();
			this._outerShapeData = new ShapeData();
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x00601A50 File Offset: 0x005FFC50
		public override void CalculateRoom(DungeonData data)
		{
			this.calculated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.BiomeRoom(data, x, y, false);
			this.calculated = true;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x00601A98 File Offset: 0x005FFC98
		public override bool GenerateRoom(DungeonData data)
		{
			this.generated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.BiomeRoom(data, x, y, true);
			this.generated = true;
			return true;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x00601AE0 File Offset: 0x005FFCE0
		public override void GenerateEarlyDungeonFeaturesInRoom(DungeonData data)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(this.settings.RandomSeed);
			BiomeDungeonRoomSettings biomeDungeonRoomSettings = (BiomeDungeonRoomSettings)this.settings;
			byte style = this.settings.StyleData.Style;
			int x = this.InnerBounds.Center.X;
			int y = this.InnerBounds.Center.Y;
			float width = (float)this.InnerBounds.Width;
			int height = this.InnerBounds.Height;
			int bottom = this.InnerBounds.Bottom;
			int num = (int)(width * 0.25f);
			int num2 = unifiedRandom.Next(2);
			if (num2 == 1 && !biomeDungeonRoomSettings.StyleData.CanGenerateFeatureAt(data, this, null, x, y))
			{
				num2 = 0;
			}
			if (num2 == 0 || num2 != 1)
			{
				DungeonWindowBasicSettings dungeonWindowBasicSettings = new DungeonWindowBasicSettings
				{
					Style = this.settings.StyleData,
					Width = 9,
					Height = height / 5,
					Closed = (unifiedRandom.Next(3) != 0)
				};
				new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, x, y);
				dungeonWindowBasicSettings.Width = 7;
				dungeonWindowBasicSettings.Height = height / 5 - 4;
				new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, x - num, y + 3);
				new DungeonWindowBasic(dungeonWindowBasicSettings).GenerateFeature(data, x + num, y + 3);
				return;
			}
			int num3 = 4;
			DungeonPillarSettings dungeonPillarSettings = new DungeonPillarSettings();
			dungeonPillarSettings.Style = this.settings.StyleData;
			dungeonPillarSettings.OverridePaintTile = this.settings.OverridePaintTile;
			dungeonPillarSettings.OverridePaintWall = this.settings.OverridePaintWall;
			dungeonPillarSettings.PillarType = PillarType.BlockActuatedSolidTop;
			dungeonPillarSettings.Width = 10;
			dungeonPillarSettings.Height = bottom - y + 5;
			dungeonPillarSettings.CrowningOnTop = true;
			dungeonPillarSettings.CrowningOnBottom = false;
			dungeonPillarSettings.CrowningStopsAtPillar = true;
			dungeonPillarSettings.AlwaysPlaceEntirePillar = false;
			new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, x + 1, bottom + num3);
			dungeonPillarSettings.Width = 7;
			dungeonPillarSettings.Height = bottom - y;
			new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, x - num + 1, bottom + num3);
			new DungeonPillar(dungeonPillarSettings).GenerateFeature(data, x + num, bottom + num3);
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x00601CF0 File Offset: 0x005FFEF0
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
			this.InnerBounds.SetBounds(num5, num7, num6 - 1, num8 - 1);
			this.OuterBounds.SetBounds(num9, num11, num10 - 1, num12 - 1);
			data.dungeonBounds.UpdateBounds(num9, num11, num10 - 1, num12 - 1);
			int num13 = num10 - num9;
			int num14 = num12 - num11;
			Point center = this.OuterBounds.Center;
			Point center2 = this.OuterBounds.Center;
			int num15 = unifiedRandom.Next(4);
			int num16 = 1;
			for (int k = num9; k < num10; k++)
			{
				int num17 = k;
				float num18 = Math.Max(0f, Math.Min(1f, (float)(k - num9) / Math.Max(1f, (float)(num10 - 1 - num9))));
				float num19 = this.BiomeRoom_GetYPercent(unifiedRandom, num15, num18);
				float num20 = (float)Math.Max(1, num14 / 16) + (float)Math.Max(1, num14 / 4) * num19;
				float num21 = (float)num7 + num20;
				float num22 = num21 - (float)num2;
				float num23 = (float)num8 - num20 - 1f;
				float num24 = num23 + (float)num2;
				for (int l = num11; l < num12; l++)
				{
					int num25 = l;
					float num26 = Math.Max(0f, Math.Min(1f, (float)(l - num11) / Math.Max(1f, (float)(num12 - 1 - num11))));
					float num27 = this.BiomeRoom_GetXPercent(unifiedRandom, num15, num26);
					float num28 = (float)Math.Max(1, num13 / 4) * num27;
					float num29 = (float)num5 + num28;
					float num30 = num29 - (float)num2;
					float num31 = (float)num6 - num28 - 1f;
					float num32 = num31 + (float)num2;
					Tile tile = Main.tile[num17, num25];
					Main.tile[num17, num25 - 1];
					Main.tile[num17, num25 + 1];
					Main.tile[num17, num25 + 2];
					if (generating && (tile.type == 484 || tile.type == 485))
					{
						tile.active(false);
					}
					if ((float)l >= num22 && (float)l <= num24 && (float)k >= num30 && (float)k <= num32)
					{
						if (k <= num9 + num2 - 1 || k >= num10 - num2 + 1 || ((float)l >= num22 && (float)l <= num21) || ((float)l >= num23 && (float)l <= num24) || ((float)k >= num30 && (float)k <= num29) || ((float)k >= num31 && (float)k <= num32))
						{
							if (!generating)
							{
								this._outerShapeData.Add(num17 - (int)position.X + num16, num25 - (int)position.Y + num16);
							}
							else
							{
								DungeonUtils.ChangeTileType(tile, brickTileType, false, biomeDungeonRoomSettings.OverridePaintTile);
								if (tile.liquid > 0)
								{
									tile.liquid = 0;
									tile.liquidType(0);
								}
								DungeonUtils.ChangeWallType(tile, brickWallType, false, biomeDungeonRoomSettings.OverridePaintWall);
							}
						}
						else if (this.InnerBounds.Contains(num17, num25))
						{
							if (!generating)
							{
								this._innerShapeData.Add(num17 - (int)position.X + num16, num25 - (int)position.Y + num16);
								this._outerShapeData.Add(num17 - (int)position.X + num16, num25 - (int)position.Y + num16);
							}
							else
							{
								if (tile.liquid > 0)
								{
									tile.liquid = 0;
									tile.liquidType(0);
								}
								DungeonUtils.ChangeWallType(tile, brickWallType, false, biomeDungeonRoomSettings.OverridePaintWall);
								tile.active(false);
								tile.ClearBlockPaintAndCoating();
							}
						}
						else if (!generating)
						{
							this._outerShapeData.Add(num17 - (int)position.X + num16, num25 - (int)position.Y + num16);
						}
						else
						{
							bool flag = k == num9 || k == num10 - 1 || l == num11 || l == num12 - 1 || k == num5 - 1 || k == num6 || l == num7 - 1 || l == num8;
							if (tile.liquid > 0)
							{
								tile.liquid = 0;
								tile.liquidType(0);
							}
							DungeonUtils.ChangeTileType(tile, brickTileType, true, biomeDungeonRoomSettings.OverridePaintTile);
							if (!flag)
							{
								DungeonUtils.ChangeWallType(tile, brickWallType, false, biomeDungeonRoomSettings.OverridePaintWall);
							}
						}
					}
				}
			}
			base.BiomeRoom_AddHallwaySpace(position, num5, num6, num7, num8, num16, brickWallType, this.settings.OverridePaintWall, generating);
			base.BiomeRoom_FinishRoom(unifiedRandom, num9, num10, num11, num12, false);
			this.RoomInnerSize = num;
			this.RoomOuterSize = num3;
			this.WallDepth = num2;
			this.Position = position;
			this.InnerBounds.CalculateHitbox();
			this.OuterBounds.CalculateHitbox();
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x00602350 File Offset: 0x00600550
		public float BiomeRoom_GetXPercent(UnifiedRandom genRand, int variant, float percentile)
		{
			switch (variant)
			{
			default:
				return Utils.MultiLerp(Utils.WrappedLerp(0f, 1f, percentile), new float[] { 0.1f, 0.5f, 0.1f });
			case 1:
			{
				float num = Utils.WrappedLerp(0f, 1f, percentile);
				float[] array = new float[4];
				array[1] = 0.9f;
				array[2] = 0.25f;
				return Utils.MultiLerp(num, array);
			}
			case 2:
				return 0f;
			case 3:
				return 0f;
			}
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x006023D4 File Offset: 0x006005D4
		public float BiomeRoom_GetYPercent(UnifiedRandom genRand, int variant, float percentile)
		{
			switch (variant)
			{
			default:
				return Utils.MultiLerp(Utils.WrappedLerp(0f, 1f, percentile), new float[] { 1f, 0f, 0.5f, 0.5f, 0f, 0f, 0.5f });
			case 1:
				return 0f;
			case 2:
				return Utils.MultiLerp(Utils.WrappedLerp(0f, 1f, percentile), new float[] { 1f, 1f, 0.5f, 0f, 0.5f, 0.25f, 0.1f });
			case 3:
				return Utils.MultiLerp(Utils.WrappedLerp(0f, 1f, percentile), new float[]
				{
					0f, 0.25f, 0.6f, 0.25f, 0f, 0f, 0.3f, 0.4f, 0.3f, 0f,
					0f
				});
			}
		}

		// Token: 0x040059E3 RID: 23011
		public const int VARIANT_DOUBLEDIAMOND = 0;

		// Token: 0x040059E4 RID: 23012
		public const int VARIANT_ROUNDED = 1;

		// Token: 0x040059E5 RID: 23013
		public const int VARIANT_CANDY = 2;

		// Token: 0x040059E6 RID: 23014
		public const int VARIANT_WIGGLED = 3;

		// Token: 0x040059E7 RID: 23015
		public const int MAX_VARIANTS = 4;

		// Token: 0x040059E8 RID: 23016
		public Vector2 Position;

		// Token: 0x040059E9 RID: 23017
		public int RoomInnerSize;

		// Token: 0x040059EA RID: 23018
		public int RoomOuterSize;

		// Token: 0x040059EB RID: 23019
		public int WallDepth;
	}
}
