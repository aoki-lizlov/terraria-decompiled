using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent
{
	// Token: 0x02000238 RID: 568
	public class ExtraSpawnPointManager
	{
		// Token: 0x0600226A RID: 8810 RVA: 0x00537EA0 File Offset: 0x005360A0
		public static bool TryGetExtraSpawnPointForTeam(int team, out Point spawnPoint)
		{
			spawnPoint = Point.Zero;
			if (!Main.teamBasedSpawnsSeed)
			{
				return false;
			}
			if (team < 0 || team >= ExtraSpawnPointManager.extraSpawnPoints.Length)
			{
				return false;
			}
			try
			{
				spawnPoint = ExtraSpawnPointManager.extraSpawnPoints[team];
			}
			catch (IndexOutOfRangeException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00537F00 File Offset: 0x00536100
		public static void GenerateExtraSpawns_Setup()
		{
			if (ExtraSpawnPointManager.settings.skyblock)
			{
				ExtraSpawnPointManager._listOfLandmasses.Clear();
				for (int i = 0; i < GenVars.landmassData.Count; i++)
				{
					LandmassData landmassData = GenVars.landmassData[i];
					if (landmassData.DataType == LandmassDataType.SkyblockIsland || landmassData.Style == 13)
					{
						ExtraSpawnPointManager._listOfLandmasses.Add(landmassData);
					}
				}
				return;
			}
			if (ExtraSpawnPointManager.settings.roundLandmass)
			{
				ExtraSpawnPointManager._listOfLandmasses.Clear();
				for (int j = 0; j < GenVars.landmassData.Count; j++)
				{
					LandmassData landmassData2 = GenVars.landmassData[j];
					if (landmassData2.DataType == LandmassDataType.RoundLandmass && landmassData2.Position.Distance(new Vector2((float)Main.spawnTileX, (float)Main.spawnTileY)) >= 300f)
					{
						ExtraSpawnPointManager._listOfLandmasses.Add(landmassData2);
					}
				}
				return;
			}
			if (ExtraSpawnPointManager.settings.extraLiquid)
			{
				ExtraSpawnPointManager._listOfLandmasses.Clear();
				for (int k = 0; k < GenVars.landmassData.Count; k++)
				{
					LandmassData landmassData3 = GenVars.landmassData[k];
					if (landmassData3.DataType == LandmassDataType.ExtraLiquidBubbleSquare && landmassData3.Position.Distance(new Vector2((float)Main.spawnTileX, (float)Main.spawnTileY)) >= 300f)
					{
						ExtraSpawnPointManager._listOfLandmasses.Add(landmassData3);
					}
				}
			}
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00538048 File Offset: 0x00536248
		public static void ResetExtraSpawns()
		{
			ExtraSpawnPointManager._listOfLandmasses.Clear();
			ExtraSpawnPointManager.extraSpawnPoints = new Point[0];
			ExtraSpawnPointManager.settings = default(ExtraSpawnSettings);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0053806C File Offset: 0x0053626C
		public static void GenerateExtraSpawns()
		{
			ExtraSpawnPointManager.GenerateExtraSpawns_Setup();
			ExtraSpawnType spawnType = ExtraSpawnPointManager.settings.spawnType;
			if (spawnType == ExtraSpawnType.None || spawnType != ExtraSpawnType.TeamBased)
			{
				ExtraSpawnPointManager.extraSpawnPoints = new Point[0];
				return;
			}
			ExtraSpawnPointManager.extraSpawnPoints = new Point[(int)PlayerTeamID.Count];
			ExtraSpawnPointManager.extraSpawnPoints[0] = new Point(Main.spawnTileX, Main.spawnTileY);
			List<Point> list = new List<Point>();
			for (int i = 1; i < (int)PlayerTeamID.Count; i++)
			{
				ExtraSpawnPointManager.GenerateExtraSpawns_TryFindSpawnRandomly(list, ExtraSpawnPointManager.GenerateExtraSpawns_GetFallbackSpawn(i, (int)PlayerTeamID.Count));
			}
			for (int j = 1; j < (int)PlayerTeamID.Count; j++)
			{
				Point point = list[WorldGen.genRand.Next(list.Count)];
				ExtraSpawnPointManager.extraSpawnPoints[j] = point;
				list.Remove(point);
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0053812C File Offset: 0x0053632C
		private static bool GenerateExtraSpawns_TryFindSpawnRandomly(List<Point> spawnPoints, Point fallbackSpawn)
		{
			int num = 500;
			int num2 = 60;
			int num3 = 60;
			bool flag = true;
			LandmassData landmassData = default(LandmassData);
			for (int i = 0; i < num; i++)
			{
				int num4 = 0;
				int num5 = 0;
				int num6 = (int)Main.worldSurface;
				if (flag)
				{
					num4 = WorldGen.genRand.Next(num2, Main.maxTilesX / 2);
				}
				else
				{
					num4 = WorldGen.genRand.Next(Main.maxTilesX / 2, Main.maxTilesX - num2);
				}
				if (!ExtraSpawnPointManager.settings.surface)
				{
					num6 = Main.UnderworldLayer - 50 - num3;
					num5 = WorldGen.genRand.Next((int)Main.worldSurface + 200 + num3, num6);
				}
				if (ExtraSpawnPointManager.settings.remix)
				{
					num6 = GenVars.remixMushroomLayerHigh - num3;
					num5 = GenVars.remixSurfaceLayerLow + 50 + num3;
				}
				if (ExtraSpawnPointManager.settings.skyblock)
				{
					LandmassData landmassData2 = default(LandmassData);
					int j = 500;
					while (j > 0)
					{
						j--;
						if (ExtraSpawnPointManager._listOfLandmasses.Count <= 0)
						{
							break;
						}
						landmassData2 = ExtraSpawnPointManager._listOfLandmasses[WorldGen.genRand.Next(ExtraSpawnPointManager._listOfLandmasses.Count)];
						if ((!ExtraSpawnPointManager.settings.surface || (double)landmassData2.Position.Y <= Main.worldSurface) && (!ExtraSpawnPointManager.settings.remix || (landmassData2.DataType == LandmassDataType.SkyblockIsland && landmassData2.Style == 13)))
						{
							break;
						}
					}
					num4 = (int)MathHelper.Clamp(landmassData2.Top.X, (float)num2, (float)(Main.maxTilesX - num2));
					num5 = (int)MathHelper.Clamp(landmassData2.Top.Y, (float)num3, (float)(Main.maxTilesY - num3));
					num6 = (int)MathHelper.Clamp((float)(num5 + landmassData2.RadiusOrHalfSize), (float)num3, (float)(Main.maxTilesY - num3));
					landmassData = landmassData2;
				}
				else if (ExtraSpawnPointManager.settings.roundLandmass || ExtraSpawnPointManager.settings.extraLiquid)
				{
					LandmassData landmassData3 = default(LandmassData);
					Vector2 vector = Vector2.Zero;
					int k = 500;
					while (k > 0)
					{
						k--;
						if (ExtraSpawnPointManager._listOfLandmasses.Count <= 0)
						{
							break;
						}
						landmassData3 = ExtraSpawnPointManager._listOfLandmasses[WorldGen.genRand.Next(ExtraSpawnPointManager._listOfLandmasses.Count)];
						vector = landmassData3.Position;
						if (ExtraSpawnPointManager.settings.roundLandmass)
						{
							vector = landmassData3.Top;
						}
						if ((!ExtraSpawnPointManager.settings.surface || (double)vector.Y <= Main.worldSurface) && (!ExtraSpawnPointManager.settings.remix || (vector.Y >= (float)GenVars.remixSurfaceLayerLow && vector.Y <= (float)GenVars.remixMushroomLayerHigh)) && (!ExtraSpawnPointManager.settings.roundLandmass || k <= 250 || landmassData3.RadiusOrHalfSize >= 40) && (!ExtraSpawnPointManager.settings.extraLiquid || k <= 250 || landmassData3.RadiusOrHalfSize >= 10))
						{
							break;
						}
					}
					num4 = (int)MathHelper.Clamp(vector.X, (float)num2, (float)(Main.maxTilesX - num2));
					num5 = (int)MathHelper.Clamp(vector.Y, (float)num3, (float)(Main.maxTilesY - num3));
					num6 = Main.maxTilesY - 50;
					landmassData = landmassData3;
				}
				flag = !flag;
				if (ExtraSpawnPointManager.GenerateExtraSpawns_TryFindSpawnAt(spawnPoints, ref num4, ref num5, num6))
				{
					spawnPoints.Add(new Point(num4, num5));
					if (!landmassData.Equals(default(LandmassData)))
					{
						ExtraSpawnPointManager._listOfLandmasses.Remove(landmassData);
					}
					return true;
				}
			}
			spawnPoints.Add(fallbackSpawn);
			return false;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x005384A4 File Offset: 0x005366A4
		private static bool GenerateExtraSpawns_TryFindSpawnAt(List<Point> spawnPoints, ref int spawnX, ref int spawnY, int maxY)
		{
			spawnY = ExtraSpawnPointManager.GenerateExtraSpawns_IterateDownToFloor(spawnX, spawnY, maxY);
			if (ExtraSpawnPointManager.settings.remix && ExtraSpawnPointManager.settings.skyblock)
			{
				spawnY -= 2;
				spawnX -= 10;
				spawnX += WorldGen.genRand.Next(21);
				return true;
			}
			int num = 50;
			if (ExtraSpawnPointManager.settings.skyblock)
			{
				num = 15;
			}
			if (ExtraSpawnPointManager.settings.extraLiquid)
			{
				num = 30;
			}
			bool flag = false;
			int num2 = Math.Max(0, spawnX - num);
			int num3 = num;
			int num4 = Math.Max(0, spawnY - num);
			int num5 = num;
			int[] tilesToAvoidForSpawn_TeamBasedSpawns = WorldGen.GetTilesToAvoidForSpawn_TeamBasedSpawns();
			int num6 = 50;
			int num7 = 100;
			Func<Tile, int, int, bool> func = delegate(Tile tile, int x, int y)
			{
				Point point = new Point(x, y);
				int num8 = 250;
				if (ExtraSpawnPointManager.settings.extraLiquid && tile.type == 379)
				{
					return false;
				}
				if (ExtraSpawnPointManager.settings.skyblock && tile.type != 0)
				{
					return false;
				}
				if (ExtraSpawnPointManager.settings.remix)
				{
					if (WorldGen.GetWorldSize() == 0)
					{
						num8 = 150;
					}
					if (!ExtraSpawnPointManager.settings.skyblock && (y < GenVars.remixSurfaceLayerLow || y > GenVars.remixMushroomLayerHigh))
					{
						return false;
					}
					if (ExtraSpawnPointManager.settings.skyblock && y < Main.UnderworldLayer)
					{
						return false;
					}
				}
				if (ExtraSpawnPointManager.settings.roundLandmass && WorldGen.GetWorldSize() == 0)
				{
					num8 = 150;
				}
				for (int i = 0; i < spawnPoints.Count; i++)
				{
					Point point2 = spawnPoints[i];
					int num9 = Math.Abs(point2.X - point.X);
					int num10 = Math.Abs(point2.Y - point.Y);
					if (num9 < num8 && num10 < num8)
					{
						return false;
					}
				}
				return true;
			};
			Vector2 vector = Utils.CheckForGoodTeleportationSpot(ref flag, num2, num3, num4, num5, new Utils.RandomTeleportationAttemptSettings
			{
				teleporteeSize = new Vector2(20f, 42f),
				teleporteeVelocity = Vector2.Zero,
				teleporteeGravityDirection = 1f,
				avoidLava = true,
				avoidAnyLiquid = true,
				avoidHurtTiles = true,
				avoidWalls = true,
				mostlySolidFloor = true,
				strictRange = true,
				maximumFallDistanceFromOrignalPoint = num7,
				attemptsBeforeGivingUp = 250,
				tilesToAvoid = tilesToAvoidForSpawn_TeamBasedSpawns,
				tilesToAvoidRange = num6,
				specializedConditions = func
			});
			if (flag)
			{
				spawnX = (int)(vector.X / 16f);
				spawnY = (int)(vector.Y / 16f);
				return true;
			}
			return false;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0053861C File Offset: 0x0053681C
		private static Point GenerateExtraSpawns_GetFallbackSpawn(int iteration, int iterationMax)
		{
			float num = ExtraSpawnPointManager.GenerateExtraSpawns_WorldPercentileAvoidWorldSpawnIfNeeded((float)iteration / (float)iterationMax);
			int num2 = (int)((float)Main.maxTilesX * num);
			int num3 = 0;
			double worldSurface = Main.worldSurface;
			if (!ExtraSpawnPointManager.settings.surface)
			{
				num3 = (int)((float)Main.maxTilesY * 0.5f);
				int underworldLayer = Main.UnderworldLayer;
			}
			if (ExtraSpawnPointManager.settings.roundLandmass)
			{
				if (ExtraSpawnPointManager.settings.surface)
				{
					num3 = 0;
					double worldSurface2 = Main.worldSurface;
				}
				else
				{
					num3 = (int)Main.worldSurface + 100;
					int underworldLayer2 = Main.UnderworldLayer;
				}
			}
			if (ExtraSpawnPointManager.settings.remix)
			{
				num3 = (int)MathHelper.Lerp((float)GenVars.remixSurfaceLayerLow, (float)GenVars.remixMushroomLayerHigh, 0.5f);
				int remixMushroomLayerHigh = GenVars.remixMushroomLayerHigh;
			}
			num3 = ExtraSpawnPointManager.GenerateExtraSpawns_IterateDownToFloor(num2, num3, (int)Main.worldSurface);
			return new Point(num2, num3);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x005386D8 File Offset: 0x005368D8
		private static float GenerateExtraSpawns_WorldPercentileAvoidWorldSpawnIfNeeded(float currentPercentile)
		{
			if (!ExtraSpawnPointManager.settings.surface && !ExtraSpawnPointManager.settings.remix)
			{
				return currentPercentile;
			}
			float num = 0.1f;
			if (currentPercentile < 0.5f)
			{
				return Utils.Remap(currentPercentile, 0f, 0.5f, 0f, 0.5f - num, true);
			}
			return Utils.Remap(currentPercentile, 0.5f, 1f, 0.5f + num, 1f, true);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x00538748 File Offset: 0x00536948
		private static int GenerateExtraSpawns_IterateDownToFloor(int spawnX, int spawnY, int maxY)
		{
			if (spawnY > Main.maxTilesY - 5)
			{
				spawnY = Main.maxTilesY - 5;
			}
			else if (spawnY < 5)
			{
				spawnY = 5;
			}
			if (maxY <= spawnY)
			{
				return spawnY;
			}
			bool extraLiquid = ExtraSpawnPointManager.settings.extraLiquid;
			int num = spawnY;
			while (num < maxY && num < Main.maxTilesY)
			{
				Tile tile = Main.tile[spawnX, num];
				if (tile.active() && (extraLiquid || tile.liquid <= 0) && (tile.type < 0 || Main.tileSolid[(int)tile.type]) && (!ExtraSpawnPointManager.settings.remix || (tile.type != 195 && tile.type != 474)))
				{
					return num;
				}
				num++;
			}
			return spawnY;
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x005387F8 File Offset: 0x005369F8
		public static void PrepareExtraSpawns()
		{
			ExtraSpawnPointManager.GenerateExtraSpawns_Setup();
			for (int i = 1; i < ExtraSpawnPointManager.extraSpawnPoints.Length; i++)
			{
				Point point = ExtraSpawnPointManager.extraSpawnPoints[i];
				if ((double)point.Y >= Main.worldSurface && point.Y < Main.UnderworldLayer)
				{
					WorldGen.PlaceTorchesAroundSpawn(point.X, point.Y);
				}
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00538854 File Offset: 0x00536A54
		public static void Clear()
		{
			ExtraSpawnPointManager.extraSpawnPoints = new Point[0];
			ExtraSpawnPointManager.settings = default(ExtraSpawnSettings);
			ExtraSpawnPointManager._listOfLandmasses.Clear();
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00538878 File Offset: 0x00536A78
		public static void Read(BinaryReader reader, bool networking = false)
		{
			byte b = reader.ReadByte();
			ExtraSpawnPointManager.extraSpawnPoints = new Point[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				int num = (int)reader.ReadInt16();
				int num2 = (int)reader.ReadInt16();
				ExtraSpawnPointManager.extraSpawnPoints[i] = new Point(num, num2);
			}
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x005388C4 File Offset: 0x00536AC4
		public static void Write(BinaryWriter writer, bool networking = false)
		{
			writer.Write((byte)ExtraSpawnPointManager.extraSpawnPoints.Length);
			for (int i = 0; i < ExtraSpawnPointManager.extraSpawnPoints.Length; i++)
			{
				writer.Write((short)ExtraSpawnPointManager.extraSpawnPoints[i].X);
				writer.Write((short)ExtraSpawnPointManager.extraSpawnPoints[i].Y);
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0000357B File Offset: 0x0000177B
		public ExtraSpawnPointManager()
		{
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0053891F File Offset: 0x00536B1F
		// Note: this type is marked as 'beforefieldinit'.
		static ExtraSpawnPointManager()
		{
		}

		// Token: 0x04004CEE RID: 19694
		public static Point[] extraSpawnPoints = new Point[0];

		// Token: 0x04004CEF RID: 19695
		public static ExtraSpawnSettings settings = default(ExtraSpawnSettings);

		// Token: 0x04004CF0 RID: 19696
		private static List<LandmassData> _listOfLandmasses = new List<LandmassData>();

		// Token: 0x020007C2 RID: 1986
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x0600420A RID: 16906 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x0600420B RID: 16907 RVA: 0x006BD88C File Offset: 0x006BBA8C
			internal bool <GenerateExtraSpawns_TryFindSpawnAt>b__0(Tile tile, int x, int y)
			{
				Point point = new Point(x, y);
				int num = 250;
				if (ExtraSpawnPointManager.settings.extraLiquid && tile.type == 379)
				{
					return false;
				}
				if (ExtraSpawnPointManager.settings.skyblock && tile.type != 0)
				{
					return false;
				}
				if (ExtraSpawnPointManager.settings.remix)
				{
					if (WorldGen.GetWorldSize() == 0)
					{
						num = 150;
					}
					if (!ExtraSpawnPointManager.settings.skyblock && (y < GenVars.remixSurfaceLayerLow || y > GenVars.remixMushroomLayerHigh))
					{
						return false;
					}
					if (ExtraSpawnPointManager.settings.skyblock && y < Main.UnderworldLayer)
					{
						return false;
					}
				}
				if (ExtraSpawnPointManager.settings.roundLandmass && WorldGen.GetWorldSize() == 0)
				{
					num = 150;
				}
				for (int i = 0; i < this.spawnPoints.Count; i++)
				{
					Point point2 = this.spawnPoints[i];
					int num2 = Math.Abs(point2.X - point.X);
					int num3 = Math.Abs(point2.Y - point.Y);
					if (num2 < num && num3 < num)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x040070DE RID: 28894
			public List<Point> spawnPoints;
		}
	}
}
