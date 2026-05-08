using System;
using Terraria.GameContent.Generation.Dungeon;
using Terraria.ID;

namespace Terraria
{
	// Token: 0x02000041 RID: 65
	public struct NPCSpawningFlagsForDualDungeons
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x00272B98 File Offset: 0x00270D98
		public bool CanScan(Tile tile)
		{
			ushort type = tile.type;
			return tile.active() && Main.tileSolid[(int)type] && !Main.tileSolidTop[(int)type] && !TileID.Sets.Boulders[(int)type] && type != 48 && type != 137 && type != 232;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00272BEC File Offset: 0x00270DEC
		public bool ScanZonesFor(bool scanOnly, int spawnTileX, int spawnTileY, int spawnTileType, int spawnWallType, bool npcSpawnPointIsInDualDungeon)
		{
			if (!npcSpawnPointIsInDualDungeon)
			{
				return false;
			}
			bool flag = false;
			if (spawnTileType > 191)
			{
				if (spawnTileType <= 385)
				{
					if (spawnTileType <= 234)
					{
						switch (spawnTileType)
						{
						case 195:
						case 199:
						case 201:
						case 203:
						case 204:
							break;
						case 196:
						case 197:
						case 198:
						case 202:
						case 205:
							goto IL_0373;
						case 200:
							if (!scanOnly)
							{
								this.ZoneSnow = (this.ZoneCrimson = true);
							}
							flag = true;
							goto IL_0373;
						case 206:
							goto IL_028E;
						default:
							switch (spawnTileType)
							{
							case 224:
								goto IL_028E;
							case 225:
								goto IL_029F;
							case 226:
								if (!scanOnly)
								{
									this.ZoneLihzhardTemple = true;
								}
								flag = true;
								goto IL_0373;
							default:
								if (spawnTileType != 234)
								{
									goto IL_0373;
								}
								break;
							}
							break;
						}
					}
					else if (spawnTileType <= 352)
					{
						if (spawnTileType != 347 && spawnTileType != 352)
						{
							goto IL_0373;
						}
					}
					else
					{
						if (spawnTileType - 383 <= 1)
						{
							goto IL_029F;
						}
						if (spawnTileType != 385)
						{
							goto IL_0373;
						}
						goto IL_027D;
					}
				}
				else if (spawnTileType <= 483)
				{
					switch (spawnTileType)
					{
					case 398:
					case 400:
						goto IL_02F8;
					case 399:
					case 401:
						break;
					case 402:
					case 403:
						goto IL_027D;
					default:
						if (spawnTileType == 474)
						{
							goto IL_02F8;
						}
						if (spawnTileType - 481 > 2)
						{
							goto IL_0373;
						}
						goto IL_0342;
					}
				}
				else if (spawnTileType <= 528)
				{
					if (spawnTileType == 492)
					{
						goto IL_027D;
					}
					if (spawnTileType != 528)
					{
						goto IL_0373;
					}
					goto IL_0367;
				}
				else
				{
					if (spawnTileType == 661)
					{
						goto IL_02F8;
					}
					if (spawnTileType != 662)
					{
						goto IL_0373;
					}
				}
				if (!scanOnly)
				{
					this.ZoneCrimson = true;
				}
				flag = true;
				goto IL_0373;
			}
			if (spawnTileType <= 62)
			{
				if (spawnTileType <= 32)
				{
					if (spawnTileType != 1)
					{
						if (spawnTileType - 22 > 3 && spawnTileType != 32)
						{
							goto IL_0373;
						}
						goto IL_02F8;
					}
				}
				else
				{
					switch (spawnTileType)
					{
					case 38:
						break;
					case 39:
					case 40:
					case 42:
						goto IL_0373;
					case 41:
					case 43:
					case 44:
						goto IL_0342;
					default:
						if (spawnTileType == 59)
						{
							goto IL_02B0;
						}
						if (spawnTileType - 60 > 2)
						{
							goto IL_0373;
						}
						goto IL_029F;
					}
				}
				if (spawnWallType > 0 && DungeonGenerationStyles.Cavern.WallIsInStyle(spawnWallType, false))
				{
					flag = true;
					goto IL_0373;
				}
				goto IL_0373;
			}
			else if (spawnTileType <= 120)
			{
				if (spawnTileType - 70 <= 2)
				{
					goto IL_0367;
				}
				if (spawnTileType == 74)
				{
					goto IL_029F;
				}
				switch (spawnTileType)
				{
				case 109:
				case 110:
				case 113:
				case 116:
				case 117:
				case 118:
					goto IL_027D;
				case 111:
				case 114:
				case 115:
				case 119:
					goto IL_0373;
				case 112:
					goto IL_02F8;
				case 120:
					break;
				default:
					goto IL_0373;
				}
			}
			else if (spawnTileType <= 148)
			{
				if (spawnTileType == 140)
				{
					goto IL_02F8;
				}
				if (spawnTileType - 147 > 1)
				{
					goto IL_0373;
				}
				goto IL_028E;
			}
			else
			{
				switch (spawnTileType)
				{
				case 161:
				case 162:
					goto IL_028E;
				case 163:
					if (!scanOnly)
					{
						this.ZoneSnow = (this.ZoneCorrupt = true);
					}
					flag = true;
					goto IL_0373;
				case 164:
					goto IL_027D;
				default:
					if (spawnTileType != 191)
					{
						goto IL_0373;
					}
					flag = true;
					goto IL_0373;
				}
			}
			IL_02B0:
			if (spawnWallType > 0 && WallID.Sets.DualDungeonsJungleBiomeWalls[spawnWallType])
			{
				if (!scanOnly)
				{
					this.ZoneJungle = true;
				}
				flag = true;
				goto IL_0373;
			}
			goto IL_0373;
			IL_027D:
			if (!scanOnly)
			{
				this.ZoneHallow = true;
			}
			flag = true;
			goto IL_0373;
			IL_028E:
			if (!scanOnly)
			{
				this.ZoneSnow = true;
			}
			flag = true;
			goto IL_0373;
			IL_029F:
			if (!scanOnly)
			{
				this.ZoneJungle = true;
			}
			flag = true;
			goto IL_0373;
			IL_02F8:
			if (!scanOnly)
			{
				this.ZoneCorrupt = true;
			}
			flag = true;
			goto IL_0373;
			IL_0342:
			if (!scanOnly && (double)spawnTileY > Main.rockLayer)
			{
				this.ZoneDungeon = true;
			}
			flag = true;
			goto IL_0373;
			IL_0367:
			if (!scanOnly)
			{
				this.ZoneGlowshroom = true;
			}
			flag = true;
			IL_0373:
			if (spawnTileType == 123 && spawnWallType > 0)
			{
				if (spawnWallType > 0 && DungeonGenerationStyles.Cavern.WallIsInStyle(spawnWallType, false))
				{
					flag = true;
				}
				else if (WallID.Sets.DualDungeonsJungleBiomeWalls[spawnWallType])
				{
					if (!scanOnly)
					{
						this.ZoneJungle = true;
					}
					flag = true;
				}
				else if (spawnWallType == (int)DungeonGenerationStyles.Temple.BrickWallType)
				{
					if (!scanOnly)
					{
						this.ZoneLihzhardTemple = true;
					}
					flag = true;
				}
			}
			if (spawnTileType <= 112)
			{
				if (spawnTileType != 53 && spawnTileType != 112)
				{
					return flag;
				}
			}
			else if (spawnTileType != 116 && spawnTileType != 234 && spawnTileType - 396 > 7)
			{
				return flag;
			}
			if (WallID.Sets.Conversion.HardenedSand[spawnWallType] || WallID.Sets.Conversion.Sandstone[spawnWallType] || spawnWallType == 223)
			{
				if (!scanOnly)
				{
					this.ZoneUndergroundDesert = true;
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x04000440 RID: 1088
		public bool ZoneDungeon;

		// Token: 0x04000441 RID: 1089
		public bool ZoneSnow;

		// Token: 0x04000442 RID: 1090
		public bool ZoneGlowshroom;

		// Token: 0x04000443 RID: 1091
		public bool ZoneCorrupt;

		// Token: 0x04000444 RID: 1092
		public bool ZoneCrimson;

		// Token: 0x04000445 RID: 1093
		public bool ZoneJungle;

		// Token: 0x04000446 RID: 1094
		public bool ZoneHallow;

		// Token: 0x04000447 RID: 1095
		public bool ZoneLihzhardTemple;

		// Token: 0x04000448 RID: 1096
		public bool ZoneUndergroundDesert;
	}
}
