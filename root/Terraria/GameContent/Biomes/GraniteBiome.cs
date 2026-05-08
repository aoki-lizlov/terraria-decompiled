using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000509 RID: 1289
	public class GraniteBiome : MicroBiome
	{
		// Token: 0x0600362B RID: 13867 RVA: 0x00622595 File Offset: 0x00620795
		public static bool CanPlace(Point origin, StructureMap structures)
		{
			return !WorldGen.BiomeTileCheck(origin.X, origin.Y) && !GenBase._tiles[origin.X, origin.Y].active();
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x006225CC File Offset: 0x006207CC
		public override bool Place(Point origin, StructureMap structures, GenerationProgress progress)
		{
			if (GenBase._tiles[origin.X, origin.Y].active())
			{
				return false;
			}
			origin.X -= this._sourceMagmaMap.GetLength(0) / 2;
			origin.Y -= this._sourceMagmaMap.GetLength(1) / 2;
			this.BuildMagmaMap(origin);
			Rectangle rectangle;
			this.SimulatePressure(out rectangle);
			this.PlaceGranite(origin, rectangle);
			this.CleanupTiles(origin, rectangle);
			this.PlaceDecorations(origin, rectangle);
			structures.AddStructure(rectangle.Modified(origin.X, origin.Y, 0, 0), 8);
			return true;
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x00622670 File Offset: 0x00620870
		private void BuildMagmaMap(Point tileOrigin)
		{
			this._sourceMagmaMap = new GraniteBiome.Magma[200, 200];
			this._targetMagmaMap = new GraniteBiome.Magma[200, 200];
			for (int i = 0; i < this._sourceMagmaMap.GetLength(0); i++)
			{
				for (int j = 0; j < this._sourceMagmaMap.GetLength(1); j++)
				{
					int num = i + tileOrigin.X;
					int num2 = j + tileOrigin.Y;
					this._sourceMagmaMap[i, j] = GraniteBiome.Magma.CreateEmpty((double)(WorldGen.SolidTile(num, num2, false) ? 4 : 1));
					this._targetMagmaMap[i, j] = this._sourceMagmaMap[i, j];
				}
			}
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x00622724 File Offset: 0x00620924
		private void SimulatePressure(out Rectangle effectedMapArea)
		{
			int length = this._sourceMagmaMap.GetLength(0);
			int length2 = this._sourceMagmaMap.GetLength(1);
			int num = length / 2;
			int num2 = length2 / 2;
			int num3 = num;
			int num4 = num3;
			int num5 = num2;
			int num6 = num5;
			for (int i = 0; i < 300; i++)
			{
				for (int j = num3; j <= num4; j++)
				{
					for (int k = num5; k <= num6; k++)
					{
						GraniteBiome.Magma magma = this._sourceMagmaMap[j, k];
						if (magma.IsActive)
						{
							double num7 = 0.0;
							Vector2D vector2D = Vector2D.Zero;
							for (int l = -1; l <= 1; l++)
							{
								for (int m = -1; m <= 1; m++)
								{
									if (l != 0 || m != 0)
									{
										Vector2D vector2D2 = GraniteBiome._normalisedVectors[(l + 1) * 3 + (m + 1)];
										GraniteBiome.Magma magma2 = this._sourceMagmaMap[j + l, k + m];
										if (magma.Pressure > 0.01 && !magma2.IsActive)
										{
											if (l == -1)
											{
												num3 = Utils.Clamp<int>(j + l, 1, num3);
											}
											else
											{
												num4 = Utils.Clamp<int>(j + l, num4, length - 2);
											}
											if (m == -1)
											{
												num5 = Utils.Clamp<int>(k + m, 1, num5);
											}
											else
											{
												num6 = Utils.Clamp<int>(k + m, num6, length2 - 2);
											}
											this._targetMagmaMap[j + l, k + m] = magma2.ToFlow();
										}
										double pressure = magma2.Pressure;
										num7 += pressure;
										vector2D += pressure * vector2D2;
									}
								}
							}
							num7 /= 8.0;
							if (num7 > magma.Resistance)
							{
								double num8 = vector2D.Length() / 8.0;
								double num9 = Math.Max(num7 - num8 - magma.Pressure, 0.0) + num8 + magma.Pressure * 0.875 - magma.Resistance;
								num9 = Math.Max(0.0, num9);
								this._targetMagmaMap[j, k] = GraniteBiome.Magma.CreateFlow(num9, Math.Max(0.0, magma.Resistance - num9 * 0.02));
							}
						}
					}
				}
				if (i < 2)
				{
					this._targetMagmaMap[num, num2] = GraniteBiome.Magma.CreateFlow(25.0, 0.0);
				}
				Utils.Swap<GraniteBiome.Magma[,]>(ref this._sourceMagmaMap, ref this._targetMagmaMap);
			}
			effectedMapArea = new Rectangle(num3, num5, num4 - num3 + 1, num6 - num5 + 1);
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x006229F0 File Offset: 0x00620BF0
		private bool ShouldUseLava(Point tileOrigin)
		{
			int length = this._sourceMagmaMap.GetLength(0);
			int length2 = this._sourceMagmaMap.GetLength(1);
			int num = length / 2;
			int num2 = length2 / 2;
			if (tileOrigin.Y + num2 <= GenVars.lavaLine - 30)
			{
				return false;
			}
			for (int i = -50; i < 50; i++)
			{
				for (int j = -50; j < 50; j++)
				{
					if (WorldGen.InWorld(tileOrigin.X + num + i, tileOrigin.Y + num2 + j, 10) && GenBase._tiles[tileOrigin.X + num + i, tileOrigin.Y + num2 + j].active())
					{
						ushort type = GenBase._tiles[tileOrigin.X + num + i, tileOrigin.Y + num2 + j].type;
						if (type == 147 || type - 161 <= 2 || type == 200)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x00622AE8 File Offset: 0x00620CE8
		private void PlaceGranite(Point tileOrigin, Rectangle magmaMapArea)
		{
			bool flag = this.ShouldUseLava(tileOrigin);
			ushort num = 368;
			ushort num2 = 180;
			if (WorldGen.drunkWorldGen)
			{
				num = 367;
				num2 = 178;
			}
			for (int i = magmaMapArea.Left; i < magmaMapArea.Right; i++)
			{
				for (int j = magmaMapArea.Top; j < magmaMapArea.Bottom; j++)
				{
					if (WorldGen.InWorld(tileOrigin.X + i, tileOrigin.Y + j, 10))
					{
						GraniteBiome.Magma magma = this._sourceMagmaMap[i, j];
						if (magma.IsActive)
						{
							Tile tile = GenBase._tiles[tileOrigin.X + i, tileOrigin.Y + j];
							double num3 = Math.Sin((double)(tileOrigin.Y + j) * 0.4) * 0.7 + 1.2;
							double num4 = 0.2 + 0.5 / Math.Sqrt(Math.Max(0.0, magma.Pressure - magma.Resistance));
							if (Math.Max(1.0 - Math.Max(0.0, num3 * num4), magma.Pressure / 15.0) > 0.35 + (WorldGen.SolidTile(tileOrigin.X + i, tileOrigin.Y + j, false) ? 0.0 : 0.5))
							{
								if (TileID.Sets.Ore[(int)tile.type])
								{
									tile.ResetToType(tile.type);
								}
								else
								{
									tile.ResetToType(num);
								}
								tile.wall = num2;
							}
							else if (magma.Resistance < 0.01)
							{
								WorldUtils.ClearTile(tileOrigin.X + i, tileOrigin.Y + j, false);
								tile.wall = num2;
							}
							if (tile.liquid > 0 && flag)
							{
								tile.liquidType(1);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x00622D00 File Offset: 0x00620F00
		private void CleanupTiles(Point tileOrigin, Rectangle magmaMapArea)
		{
			ushort num = 180;
			if (WorldGen.drunkWorldGen)
			{
				num = 178;
			}
			List<Point16> list = new List<Point16>();
			for (int i = magmaMapArea.Left; i < magmaMapArea.Right; i++)
			{
				for (int j = magmaMapArea.Top; j < magmaMapArea.Bottom; j++)
				{
					if (this._sourceMagmaMap[i, j].IsActive)
					{
						int num2 = 0;
						int num3 = i + tileOrigin.X;
						int num4 = j + tileOrigin.Y;
						if (WorldGen.InWorld(num3, num4, 10) && WorldGen.SolidTile(num3, num4, false))
						{
							for (int k = -1; k <= 1; k++)
							{
								for (int l = -1; l <= 1; l++)
								{
									if (WorldGen.SolidTile(num3 + k, num4 + l, false))
									{
										num2++;
									}
								}
							}
							if (num2 < 3)
							{
								list.Add(new Point16(num3, num4));
							}
						}
					}
				}
			}
			foreach (Point16 point in list)
			{
				int x = (int)point.X;
				int y = (int)point.Y;
				WorldUtils.ClearTile(x, y, true);
				GenBase._tiles[x, y].wall = num;
			}
			list.Clear();
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x00622E60 File Offset: 0x00621060
		private void PlaceDecorations(Point tileOrigin, Rectangle magmaMapArea)
		{
			FastRandom fastRandom = new FastRandom(Main.ActiveWorldFileData.Seed).WithModifier(65440UL);
			for (int i = magmaMapArea.Left; i < magmaMapArea.Right; i++)
			{
				for (int j = magmaMapArea.Top; j < magmaMapArea.Bottom; j++)
				{
					GraniteBiome.Magma magma = this._sourceMagmaMap[i, j];
					int num = i + tileOrigin.X;
					int num2 = j + tileOrigin.Y;
					if (WorldGen.InWorld(num, num2, 10) && magma.IsActive)
					{
						WorldUtils.TileFrame(num, num2, false);
						WorldGen.SquareWallFrame(num, num2, true);
						FastRandom fastRandom2 = fastRandom.WithModifier(num, num2);
						if (fastRandom2.Next(8) == 0 && GenBase._tiles[num, num2].active())
						{
							if (!GenBase._tiles[num, num2 + 1].active())
							{
								WorldGen.PlaceUncheckedStalactite(num, num2 + 1, fastRandom2.Next(2) == 0, fastRandom2.Next(3), false);
							}
							if (!GenBase._tiles[num, num2 - 1].active())
							{
								WorldGen.PlaceUncheckedStalactite(num, num2 - 1, fastRandom2.Next(2) == 0, fastRandom2.Next(3), false);
							}
						}
						if (fastRandom2.Next(2) == 0)
						{
							Tile.SmoothSlope(num, num2, true, false);
						}
					}
				}
			}
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x00622FCA File Offset: 0x006211CA
		public GraniteBiome()
		{
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x00622FFC File Offset: 0x006211FC
		// Note: this type is marked as 'beforefieldinit'.
		static GraniteBiome()
		{
		}

		// Token: 0x04005B16 RID: 23318
		private const int MAX_MAGMA_ITERATIONS = 300;

		// Token: 0x04005B17 RID: 23319
		private GraniteBiome.Magma[,] _sourceMagmaMap = new GraniteBiome.Magma[200, 200];

		// Token: 0x04005B18 RID: 23320
		private GraniteBiome.Magma[,] _targetMagmaMap = new GraniteBiome.Magma[200, 200];

		// Token: 0x04005B19 RID: 23321
		private static Vector2D[] _normalisedVectors = new Vector2D[]
		{
			Vector2D.Normalize(new Vector2D(-1.0, -1.0)),
			Vector2D.Normalize(new Vector2D(-1.0, 0.0)),
			Vector2D.Normalize(new Vector2D(-1.0, 1.0)),
			Vector2D.Normalize(new Vector2D(0.0, -1.0)),
			new Vector2D(0.0, 0.0),
			Vector2D.Normalize(new Vector2D(0.0, 1.0)),
			Vector2D.Normalize(new Vector2D(1.0, -1.0)),
			Vector2D.Normalize(new Vector2D(1.0, 0.0)),
			Vector2D.Normalize(new Vector2D(1.0, 1.0))
		};

		// Token: 0x02000998 RID: 2456
		private struct Magma
		{
			// Token: 0x06004992 RID: 18834 RVA: 0x006D23EA File Offset: 0x006D05EA
			private Magma(double pressure, double resistance, bool active)
			{
				this.Pressure = pressure;
				this.Resistance = resistance;
				this.IsActive = active;
			}

			// Token: 0x06004993 RID: 18835 RVA: 0x006D2401 File Offset: 0x006D0601
			public GraniteBiome.Magma ToFlow()
			{
				return new GraniteBiome.Magma(this.Pressure, this.Resistance, true);
			}

			// Token: 0x06004994 RID: 18836 RVA: 0x006D2415 File Offset: 0x006D0615
			public static GraniteBiome.Magma CreateFlow(double pressure, double resistance = 0.0)
			{
				return new GraniteBiome.Magma(pressure, resistance, true);
			}

			// Token: 0x06004995 RID: 18837 RVA: 0x006D241F File Offset: 0x006D061F
			public static GraniteBiome.Magma CreateEmpty(double resistance = 0.0)
			{
				return new GraniteBiome.Magma(0.0, resistance, false);
			}

			// Token: 0x04007673 RID: 30323
			public readonly double Pressure;

			// Token: 0x04007674 RID: 30324
			public readonly double Resistance;

			// Token: 0x04007675 RID: 30325
			public readonly bool IsActive;
		}
	}
}
