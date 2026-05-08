using System;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000503 RID: 1283
	public class DitherSnakePass : GenPass
	{
		// Token: 0x060035FC RID: 13820 RVA: 0x0061EE09 File Offset: 0x0061D009
		public DitherSnakePass(string passName)
			: base(passName, 1.0)
		{
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x0061EE1C File Offset: 0x0061D01C
		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			progress.Message = Language.GetTextValue("WorldGeneration.DualDungeonsDitherSnake");
			GenVars.CurrentDungeon = 0;
			GenVars.CurrentDungeonGenVars.dungeonDitherSnake = this.CalculateDungeonDitherSnake(progress, 0.0, 0.02500000037252903);
			this.GenerateDungeonDitherSnake(progress, 0.02500000037252903, 0.5);
			GenVars.CurrentDungeon = 1;
			GenVars.CurrentDungeonGenVars.dungeonDitherSnake = this.CalculateDungeonDitherSnake(progress, 0.5, 0.5249999761581421);
			this.GenerateDungeonDitherSnake(progress, 0.5249999761581421, 1.0);
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x0061EEC0 File Offset: 0x0061D0C0
		private void GenerateDungeonDitherSnake(GenerationProgress progress, double progressMin, double progressMax)
		{
			int num = 0;
			double num2 = (double)GenVars.CurrentDungeonGenVars.dungeonDitherSnake.Count;
			foreach (DungeonControlLine dungeonControlLine in GenVars.CurrentDungeonGenVars.dungeonDitherSnake)
			{
				progress.Set((double)num++ / num2, progressMin, progressMax);
				dungeonControlLine.Paint(GenVars.CurrentDungeonGenVars.outerPotentialDungeonBounds.Hitbox);
			}
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x0061EF48 File Offset: 0x0061D148
		private DitherSnake CalculateDungeonDitherSnake(GenerationProgress progress, double progressMin, double progressMax)
		{
			DitherSnake ditherSnake = new DitherSnake();
			UnifiedRandom genRand = WorldGen.genRand;
			List<DungeonGenerationStyleData> dungeonGenerationStyles = GenVars.CurrentDungeonGenVars.dungeonGenerationStyles;
			DungeonBounds outerPotentialDungeonBounds = GenVars.CurrentDungeonGenVars.outerPotentialDungeonBounds;
			double num = (double)Main.maxTilesX / 4200.0;
			int num2 = (int)(20.0 * num);
			int count = dungeonGenerationStyles.Count;
			double num3 = 1.0 / (double)(num2 - 1);
			double num4 = 1.0 / (double)(count - 1);
			double num5 = (double)outerPotentialDungeonBounds.Height / (double)count;
			double num6 = 1.0 - DungeonControlLine.NormalizedDistanceSafeFromDither;
			double num7 = num5 / 2.0 * (1.0 + num6 / 2.0) - 1.0;
			double num8 = num7 - 0.1 * num * num5;
			double num9 = 0.05;
			double num10 = Utils.Remap(num, 1.0, 2.0, 1.0, 1.5, true);
			double num11 = num7 + num5 / 2.0 * num10;
			double num12 = num7;
			Vector2D vector2D;
			vector2D..ctor((double)outerPotentialDungeonBounds.X + num11, (double)outerPotentialDungeonBounds.Y + num12);
			Vector2D vector2D2;
			vector2D2..ctor((double)outerPotentialDungeonBounds.Width - num11 * 2.0, (double)outerPotentialDungeonBounds.Height - num12 * 2.0);
			double num13 = Math.Min(num7 - num8, vector2D2.X * num3 / 2.0);
			DungeonGenerationStyleData dungeonGenerationStyleData = dungeonGenerationStyles[0];
			double num14 = num7;
			Vector2D vector2D3 = Vector2D.Zero;
			int num15 = num2 * count;
			int num16 = num15 / dungeonGenerationStyles.Count;
			double num17 = num7;
			double num18 = num7;
			for (int i = 0; i < num15; i++)
			{
				progress.Set((double)i / (double)num15, progressMin, progressMax);
				int num19 = i % num2;
				int num20 = i / num2;
				double num21 = num3 * (double)num19;
				double num22 = num4 * (double)num20;
				int num23 = ((GenVars.CurrentDungeonGenVars.dungeonSide == (int)DungeonSide.Left) ? 1 : (-1));
				if (num20 % 2 == 1)
				{
					num23 *= -1;
				}
				if (num23 < 0)
				{
					num21 = 1.0 - num21;
				}
				Vector2D vector2D4 = vector2D + vector2D2 * new Vector2D(num21, num22);
				if (i == 0)
				{
					vector2D3 = vector2D4;
				}
				else if (num20 == 0 && (vector2D4.X - (double)GenVars.CurrentDungeonGenVars.dungeonLocation) * (double)num23 < 0.0)
				{
					vector2D3..ctor((double)GenVars.CurrentDungeonGenVars.dungeonLocation, vector2D4.Y);
				}
				else
				{
					num17 = Utils.Lerp(Math.Max(num8, num17 - num13), Math.Min(num7, num17 + num13), genRand.NextDouble());
					num18 = Utils.Lerp(Math.Max(num8, num18 - num13), Math.Min(num7, num18 + num13), genRand.NextDouble());
					double num24 = (num17 + num18) / 2.0;
					vector2D4.Y += (num17 - num18) / 2.0;
					int num25 = i / num16;
					DungeonGenerationStyleData dungeonGenerationStyleData2 = dungeonGenerationStyles[num25];
					DungeonControlLine dungeonControlLine = new DungeonControlLine(vector2D3, vector2D4, num14, num24, num25, dungeonGenerationStyleData2);
					ditherSnake.Add(dungeonControlLine);
					vector2D3 = vector2D4;
					num14 = num24;
					if (num19 == num2 - 1 && num20 != count - 1)
					{
						Vector2D vector2D5 = vector2D4;
						Vector2D vector2D6 = vector2D4 + vector2D2 * new Vector2D(0.0, num4);
						Vector2D vector2D7 = Vector2D.Lerp(vector2D5, vector2D6, 0.5);
						for (double num26 = num9; num26 < 0.5; num26 += num9)
						{
							vector2D4 = vector2D5.RotatedBy(6.283185307179586 * num26 * (double)num23, vector2D7);
							vector2D4.X = Utils.Lerp(vector2D4.X, vector2D7.X, 1.0 - num10);
							dungeonControlLine = new DungeonControlLine(vector2D3, vector2D4, num14, num24, num25, dungeonGenerationStyleData2)
							{
								CurveLine = true
							};
							ditherSnake.Add(dungeonControlLine);
							vector2D3 = vector2D4;
							num14 = num24;
						}
						i++;
					}
				}
			}
			ditherSnake.SetTangents();
			int num27 = ((GenVars.CurrentDungeonGenVars.dungeonSide == (int)DungeonSide.Left) ? 1 : (-1));
			ditherSnake.First<DungeonControlLine>().StartTangent = Vector2D.UnitX * (double)num27;
			ditherSnake.Last<DungeonControlLine>().EndTangent = Vector2D.UnitX * (double)num27;
			ditherSnake.AdjustTangentsToPreventSelfIntersection();
			return ditherSnake;
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x0061F3CB File Offset: 0x0061D5CB
		// Note: this type is marked as 'beforefieldinit'.
		static DitherSnakePass()
		{
		}

		// Token: 0x04005B0B RID: 23307
		public static readonly double[,] _bayerDither = new double[,]
		{
			{ 0.0, 0.5, 0.125, 0.625 },
			{ 0.75, 0.25, 0.875, 0.375 },
			{ 0.1875, 0.6875, 0.0625, 0.5625 },
			{ 0.9375, 0.4375, 0.8125, 0.3125 }
		};
	}
}
