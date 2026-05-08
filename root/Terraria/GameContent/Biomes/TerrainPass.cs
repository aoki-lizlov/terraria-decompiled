using System;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000507 RID: 1287
	public class TerrainPass : GenPass
	{
		// Token: 0x0600361F RID: 13855 RVA: 0x006214E8 File Offset: 0x0061F6E8
		public TerrainPass()
			: base(GenPassNameID.Terrain, 449.3721923828125)
		{
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x00621500 File Offset: 0x0061F700
		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			int num = configuration.Get<int>("FlatBeachPadding");
			progress.Message = Lang.gen[0].Value;
			TerrainPass.TerrainFeatureType terrainFeatureType = TerrainPass.TerrainFeatureType.Plateau;
			double num2 = (double)Main.maxTilesY * 0.3;
			num2 *= (double)GenBase._random.Next(90, 110) * 0.005;
			double num3 = num2 + (double)Main.maxTilesY * 0.2;
			num3 *= (double)GenBase._random.Next(90, 110) * 0.01;
			if (WorldGen.remixWorldGen)
			{
				num3 = (double)Main.maxTilesY * 0.5;
				if (Main.maxTilesX > 2500)
				{
					num3 = (double)Main.maxTilesY * 0.6;
				}
				num3 *= (double)GenBase._random.Next(95, 106) * 0.01;
			}
			double num4 = num2;
			double num5 = num2;
			double num6 = num3;
			double num7 = num3;
			if (WorldGen.SecretSeed.noSurface.Enabled)
			{
				num2 = 25.0;
				num3 = (double)Main.maxTilesY * 0.4;
				num3 *= (double)GenBase._random.Next(90, 110) * 0.01;
			}
			double num8 = (double)Main.maxTilesY * 0.23;
			TerrainPass.SurfaceHistory surfaceHistory = new TerrainPass.SurfaceHistory(500);
			int num9 = GenVars.leftBeachEnd + num;
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				progress.Set((double)i / (double)Main.maxTilesX);
				num4 = Math.Min(num2, num4);
				num5 = Math.Max(num2, num5);
				num6 = Math.Min(num3, num6);
				num7 = Math.Max(num3, num7);
				if (num9 <= 0)
				{
					terrainFeatureType = (TerrainPass.TerrainFeatureType)GenBase._random.Next(0, 5);
					num9 = GenBase._random.Next(5, 40);
					if (terrainFeatureType == TerrainPass.TerrainFeatureType.Plateau)
					{
						num9 *= (int)((double)GenBase._random.Next(5, 30) * 0.2);
					}
				}
				num9--;
				if ((double)i > (double)Main.maxTilesX * 0.45 && (double)i < (double)Main.maxTilesX * 0.55 && (terrainFeatureType == TerrainPass.TerrainFeatureType.Mountain || terrainFeatureType == TerrainPass.TerrainFeatureType.Valley))
				{
					terrainFeatureType = (TerrainPass.TerrainFeatureType)GenBase._random.Next(3);
				}
				if ((double)i > (double)Main.maxTilesX * 0.48 && (double)i < (double)Main.maxTilesX * 0.52)
				{
					terrainFeatureType = TerrainPass.TerrainFeatureType.Plateau;
				}
				if (!WorldGen.SecretSeed.noSurface.Enabled)
				{
					num2 += TerrainPass.GenerateWorldSurfaceOffset(terrainFeatureType);
				}
				double num10 = 0.17;
				double num11 = 0.26;
				if (WorldGen.SecretSeed.surfaceIsInSpace.Enabled)
				{
					num11 = 0.2199999988079071;
				}
				else if (WorldGen.drunkWorldGen)
				{
					num10 = 0.15;
					num11 = 0.28;
				}
				if (WorldGen.GetWorldSize() == 0)
				{
					num10 += 0.02;
				}
				if (!WorldGen.SecretSeed.noSurface.Enabled)
				{
					if (i < GenVars.leftBeachEnd + num || i > GenVars.rightBeachStart - num)
					{
						num2 = Utils.Clamp<double>(num2, (double)Main.maxTilesY * num10, num8);
					}
					else if (num2 < (double)Main.maxTilesY * num10)
					{
						num2 = (double)Main.maxTilesY * num10;
						num9 = 0;
					}
					else if (num2 > (double)Main.maxTilesY * num11)
					{
						num2 = (double)Main.maxTilesY * num11;
						num9 = 0;
					}
				}
				while (GenBase._random.Next(0, 3) == 0)
				{
					num3 += (double)GenBase._random.Next(-2, 3);
				}
				if (WorldGen.SecretSeed.noSurface.Enabled)
				{
					if (num3 < num2 + (double)Main.maxTilesY * 0.35)
					{
						num3 += 1.0;
					}
					if (num3 > num2 + (double)Main.maxTilesY * 0.45)
					{
						num3 -= 1.0;
					}
				}
				else if (WorldGen.remixWorldGen)
				{
					if (Main.maxTilesX > 2500)
					{
						if (num3 > (double)Main.maxTilesY * 0.7)
						{
							num3 -= 1.0;
						}
					}
					else if (num3 > (double)Main.maxTilesY * 0.6)
					{
						num3 -= 1.0;
					}
				}
				else
				{
					if (num3 < num2 + (double)Main.maxTilesY * 0.06)
					{
						num3 += 1.0;
					}
					if (num3 > num2 + (double)Main.maxTilesY * 0.35)
					{
						num3 -= 1.0;
					}
				}
				surfaceHistory.Record(num2);
				if (WorldGen.SecretSeed.surfaceIsInSpace.Enabled && !WorldGen.SecretSeed.noSurface.Enabled)
				{
					TerrainPass.FillColumn(i, num2 - (double)Main.maxTilesY * 0.08, num3);
				}
				else
				{
					TerrainPass.FillColumn(i, num2, num3);
					if (i == GenVars.rightBeachStart - num)
					{
						if (num2 > num8)
						{
							TerrainPass.RetargetSurfaceHistory(surfaceHistory, i, num8);
						}
						terrainFeatureType = TerrainPass.TerrainFeatureType.Plateau;
						num9 = Main.maxTilesX - i;
					}
				}
			}
			Main.worldSurface = (double)((int)(num5 + 25.0));
			if (WorldGen.SecretSeed.noSurface.Enabled)
			{
				Main.worldSurface = 25.0;
			}
			Main.rockLayer = num7;
			double num12 = (double)((int)((Main.rockLayer - Main.worldSurface) / 6.0) * 6);
			Main.rockLayer = (double)((int)(Main.worldSurface + num12));
			int num13 = (int)(Main.rockLayer + (double)Main.maxTilesY) / 2 + GenBase._random.Next(-100, 20);
			int num14 = num13 + GenBase._random.Next(50, 80);
			if (WorldGen.remixWorldGen)
			{
				num14 = (int)(Main.worldSurface * 4.0 + num3) / 5;
			}
			int num15 = 20;
			if (num6 < num5 + (double)num15)
			{
				double num16 = (num6 + num5) / 2.0;
				double num17 = Math.Abs(num6 - num5);
				if (num17 < (double)num15)
				{
					num17 = (double)num15;
				}
				num6 = num16 + num17 / 2.0;
				num5 = num16 - num17 / 2.0;
			}
			GenVars.rockLayer = num3;
			GenVars.rockLayerHigh = num7;
			GenVars.rockLayerLow = num6;
			GenVars.worldSurface = num2;
			GenVars.worldSurfaceHigh = num5;
			GenVars.worldSurfaceLow = num4;
			GenVars.waterLine = num13;
			GenVars.lavaLine = num14;
			GenVars.remixMushroomLayerLow = Main.maxTilesY - 350;
			GenVars.remixMushroomLayerHigh = Main.UnderworldLayer;
			GenVars.remixSurfaceLayerLow = (int)GenVars.rockLayerLow;
			GenVars.remixSurfaceLayerHigh = GenVars.remixMushroomLayerLow;
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x00621B30 File Offset: 0x0061FD30
		private static void FillColumn(int x, double worldSurface, double rockLayer)
		{
			int num = 0;
			while ((double)num < worldSurface)
			{
				Main.tile[x, num].active(false);
				Main.tile[x, num].frameX = -1;
				Main.tile[x, num].frameY = -1;
				num++;
			}
			for (int i = (int)worldSurface; i < Main.maxTilesY; i++)
			{
				if ((double)i < rockLayer)
				{
					Main.tile[x, i].active(true);
					Main.tile[x, i].type = 0;
					Main.tile[x, i].frameX = -1;
					Main.tile[x, i].frameY = -1;
				}
				else
				{
					Main.tile[x, i].active(true);
					Main.tile[x, i].type = 1;
					Main.tile[x, i].frameX = -1;
					Main.tile[x, i].frameY = -1;
				}
			}
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x00621C30 File Offset: 0x0061FE30
		private static void RetargetColumn(int x, double worldSurface)
		{
			int num = 0;
			while ((double)num < worldSurface)
			{
				Main.tile[x, num].active(false);
				Main.tile[x, num].frameX = -1;
				Main.tile[x, num].frameY = -1;
				num++;
			}
			for (int i = (int)worldSurface; i < Main.maxTilesY; i++)
			{
				if (Main.tile[x, i].type != 1 || !Main.tile[x, i].active())
				{
					Main.tile[x, i].active(true);
					Main.tile[x, i].type = 0;
					Main.tile[x, i].frameX = -1;
					Main.tile[x, i].frameY = -1;
				}
			}
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x00621D00 File Offset: 0x0061FF00
		private static double GenerateWorldSurfaceOffset(TerrainPass.TerrainFeatureType featureType)
		{
			double num = 0.0;
			if ((WorldGen.drunkWorldGen || WorldGen.getGoodWorldGen || WorldGen.remixWorldGen) && WorldGen.genRand.Next(2) == 0)
			{
				switch (featureType)
				{
				case TerrainPass.TerrainFeatureType.Plateau:
					while (GenBase._random.Next(0, 6) == 0)
					{
						num += (double)GenBase._random.Next(-1, 2);
					}
					break;
				case TerrainPass.TerrainFeatureType.Hill:
					while (GenBase._random.Next(0, 3) == 0)
					{
						num -= 1.0;
					}
					while (GenBase._random.Next(0, 10) == 0)
					{
						num += 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Dale:
					while (GenBase._random.Next(0, 3) == 0)
					{
						num += 1.0;
					}
					while (GenBase._random.Next(0, 10) == 0)
					{
						num -= 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Mountain:
					while (GenBase._random.Next(0, 3) != 0)
					{
						num -= 1.0;
					}
					while (GenBase._random.Next(0, 6) == 0)
					{
						num += 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Valley:
					while (GenBase._random.Next(0, 3) != 0)
					{
						num += 1.0;
					}
					while (GenBase._random.Next(0, 5) == 0)
					{
						num -= 1.0;
					}
					break;
				}
			}
			else
			{
				switch (featureType)
				{
				case TerrainPass.TerrainFeatureType.Plateau:
					while (GenBase._random.Next(0, 7) == 0)
					{
						num += (double)GenBase._random.Next(-1, 2);
					}
					break;
				case TerrainPass.TerrainFeatureType.Hill:
					while (GenBase._random.Next(0, 4) == 0)
					{
						num -= 1.0;
					}
					while (GenBase._random.Next(0, 10) == 0)
					{
						num += 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Dale:
					while (GenBase._random.Next(0, 4) == 0)
					{
						num += 1.0;
					}
					while (GenBase._random.Next(0, 10) == 0)
					{
						num -= 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Mountain:
					while (GenBase._random.Next(0, 2) == 0)
					{
						num -= 1.0;
					}
					while (GenBase._random.Next(0, 6) == 0)
					{
						num += 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Valley:
					while (GenBase._random.Next(0, 2) == 0)
					{
						num += 1.0;
					}
					while (GenBase._random.Next(0, 5) == 0)
					{
						num -= 1.0;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x00621F98 File Offset: 0x00620198
		private static void RetargetSurfaceHistory(TerrainPass.SurfaceHistory history, int targetX, double targetHeight)
		{
			int num = 0;
			while (num < history.Length / 2 && history[history.Length - 1] > targetHeight)
			{
				for (int i = 0; i < history.Length - num * 2; i++)
				{
					double num2 = history[history.Length - i - 1];
					num2 -= 1.0;
					history[history.Length - i - 1] = num2;
					if (num2 <= targetHeight)
					{
						break;
					}
				}
				num++;
			}
			for (int j = 0; j < history.Length; j++)
			{
				double num3 = history[history.Length - j - 1];
				TerrainPass.RetargetColumn(targetX - j, num3);
			}
		}

		// Token: 0x02000994 RID: 2452
		private enum TerrainFeatureType
		{
			// Token: 0x04007665 RID: 30309
			Plateau,
			// Token: 0x04007666 RID: 30310
			Hill,
			// Token: 0x04007667 RID: 30311
			Dale,
			// Token: 0x04007668 RID: 30312
			Mountain,
			// Token: 0x04007669 RID: 30313
			Valley
		}

		// Token: 0x02000995 RID: 2453
		private class SurfaceHistory
		{
			// Token: 0x17000591 RID: 1425
			public double this[int index]
			{
				get
				{
					return this._heights[(index + this._index) % this._heights.Length];
				}
				set
				{
					this._heights[(index + this._index) % this._heights.Length] = value;
				}
			}

			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x06004985 RID: 18821 RVA: 0x006D22ED File Offset: 0x006D04ED
			public int Length
			{
				get
				{
					return this._heights.Length;
				}
			}

			// Token: 0x06004986 RID: 18822 RVA: 0x006D22F7 File Offset: 0x006D04F7
			public SurfaceHistory(int size)
			{
				this._heights = new double[size];
			}

			// Token: 0x06004987 RID: 18823 RVA: 0x006D230B File Offset: 0x006D050B
			public void Record(double height)
			{
				this._heights[this._index] = height;
				this._index = (this._index + 1) % this._heights.Length;
			}

			// Token: 0x0400766A RID: 30314
			private readonly double[] _heights;

			// Token: 0x0400766B RID: 30315
			private int _index;
		}
	}
}
