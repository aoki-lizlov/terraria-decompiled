using System;
using System.Collections.Generic;
using Terraria.GameContent.Generation.Dungeon.Rooms;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004DB RID: 1243
	public class DungeonGlobalLateDualDungeonFeatures : GlobalDungeonFeature
	{
		// Token: 0x060034F7 RID: 13559 RVA: 0x0060B4E2 File Offset: 0x006096E2
		public DungeonGlobalLateDualDungeonFeatures(DungeonFeatureSettings settings)
			: base(settings)
		{
			DungeonCrawler.CurrentDungeonData.dungeonFeatures.Add(this);
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x0060D224 File Offset: 0x0060B424
		public override bool GenerateFeature(DungeonData data)
		{
			this.generated = false;
			this.LateDualDungeonFeatures(data);
			this.generated = true;
			return true;
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x0060D23C File Offset: 0x0060B43C
		public void LateDualDungeonFeatures(DungeonData data)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			for (int i = 0; i < data.genVars.dungeonGenerationStyles.Count; i++)
			{
				DungeonGenerationStyleData dungeonGenerationStyleData = data.genVars.dungeonGenerationStyles[i];
				byte style = dungeonGenerationStyleData.Style;
				DungeonBounds dungeonBounds = data.outerProgressionBounds[i];
				if ((style == 8 || style == 7) && dungeonGenerationStyleData.BrickGrassTileType != null)
				{
					ushort value = dungeonGenerationStyleData.BrickGrassTileType.Value;
					for (int j = dungeonBounds.Left; j <= dungeonBounds.Right; j++)
					{
						for (int k = dungeonBounds.Top; k <= dungeonBounds.Bottom; k++)
						{
							Tile tile = Main.tile[j, k];
							if (tile.active() && tile.type == 59 && WorldGen.TileIsExposedToAir(j, k))
							{
								tile.type = value;
							}
						}
					}
				}
			}
			List<BiomeDungeonRoom> list = new List<BiomeDungeonRoom>();
			for (int l = 0; l < data.dungeonRooms.Count; l++)
			{
				DungeonRoom dungeonRoom = data.dungeonRooms[l];
				if (dungeonRoom is BiomeDungeonRoom)
				{
					list.Add((BiomeDungeonRoom)dungeonRoom);
				}
			}
			while (list.Count > 0)
			{
				BiomeDungeonRoom biomeDungeonRoom = list[0];
				this.LateDualDungeonFeatures_CleanOutEntrancesOfLiquids(biomeDungeonRoom);
				if (biomeDungeonRoom.settings.StyleData.Style == 8)
				{
					this.LateDualDungeonFeatures_GrowGrass(biomeDungeonRoom);
				}
				list.Remove(biomeDungeonRoom);
			}
			for (int m = 0; m < data.dungeonRooms.Count; m++)
			{
				data.dungeonRooms[m].GenerateLateDungeonFeaturesInRoom(data);
			}
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x0060D3DC File Offset: 0x0060B5DC
		private void LateDualDungeonFeatures_CleanOutEntrancesOfLiquids(BiomeDungeonRoom room)
		{
			DungeonBounds innerBounds = room.InnerBounds;
			int num = innerBounds.Left - 1;
			int num2 = innerBounds.Right + 1;
			int num3 = innerBounds.Top - 1;
			int num4 = innerBounds.Bottom + 1;
			int num5 = -1;
			for (int i = 0; i < 2; i++)
			{
				int num6 = ((i == 0) ? num : num2);
				for (int j = num3; j <= num4; j++)
				{
					if (!innerBounds.Contains(num6, j))
					{
						if (Main.tile[num6, j].active())
						{
							if (num5 != -1)
							{
								this.LateDualDungeonFeatures_CleanOutEntrancesOfLiquids_ActuallyClearArea(num6, (num5 + j - 1) / 2);
							}
							num5 = -1;
						}
						else if (num5 == -1)
						{
							num5 = j;
						}
					}
				}
			}
			int num7 = -1;
			for (int k = 0; k < 2; k++)
			{
				int num8 = ((k == 0) ? num3 : num4);
				for (int l = num; l <= num2; l++)
				{
					if (!innerBounds.Contains(l, num8))
					{
						if (Main.tile[l, num8].active())
						{
							if (num7 != -1)
							{
								this.LateDualDungeonFeatures_CleanOutEntrancesOfLiquids_ActuallyClearArea((num7 + l - 1) / 2, num8);
							}
							num7 = -1;
						}
						else if (num7 == -1)
						{
							num7 = l;
						}
					}
				}
			}
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x0060D4FC File Offset: 0x0060B6FC
		private void LateDualDungeonFeatures_CleanOutEntrancesOfLiquids_ActuallyClearArea(int x, int y)
		{
			int num = 30;
			for (int i = -num; i <= num; i++)
			{
				for (int j = -num; j <= num; j++)
				{
					Tile tile = Main.tile[x + i, y + j];
					if (tile.liquid > 0)
					{
						tile.liquid = 0;
						tile.liquidType(0);
					}
				}
			}
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x0060D550 File Offset: 0x0060B750
		private void LateDualDungeonFeatures_GrowGrass(DungeonRoom room)
		{
			if (room == null || room.settings.StyleData.BrickGrassTileType == null)
			{
				return;
			}
			this.LateDualDungeonFeatures_GrowGrass(room, room.settings.StyleData.BrickTileType, room.settings.StyleData.BrickGrassTileType.Value);
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x0060D5A4 File Offset: 0x0060B7A4
		private void LateDualDungeonFeatures_GrowGrass(DungeonRoom room, ushort dirtType, ushort grassType)
		{
			if (room == null)
			{
				return;
			}
			DungeonBounds outerBounds = room.OuterBounds;
			for (int i = outerBounds.Left; i <= outerBounds.Right; i++)
			{
				for (int j = outerBounds.Top; j <= outerBounds.Bottom; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && tile.type == dirtType && WorldGen.TileIsExposedToAir(i, j))
					{
						tile.type = grassType;
					}
				}
			}
		}
	}
}
