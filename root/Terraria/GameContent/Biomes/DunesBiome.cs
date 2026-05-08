using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.GameContent.Biomes.Desert;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000508 RID: 1288
	public class DunesBiome : MicroBiome
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x0062203D File Offset: 0x0062023D
		public int MaximumWidth
		{
			get
			{
				return this._singleDunesWidth.ScaledMaximum * 2;
			}
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x0062204C File Offset: 0x0062024C
		public override bool Place(Point origin, StructureMap structures, GenerationProgress progress)
		{
			int num = (int)((double)GenBase._random.Next(60, 100) * this._heightScale);
			int num2 = (int)((double)GenBase._random.Next(60, 100) * this._heightScale);
			int random = this._singleDunesWidth.GetRandom(GenBase._random);
			int random2 = this._singleDunesWidth.GetRandom(GenBase._random);
			DunesBiome.DunesDescription dunesDescription = DunesBiome.DunesDescription.CreateFromPlacement(new Point(origin.X - random / 2 + 30, origin.Y), random, num);
			DunesBiome.DunesDescription dunesDescription2 = DunesBiome.DunesDescription.CreateFromPlacement(new Point(origin.X + random2 / 2 - 30, origin.Y), random2, num2);
			this.PlaceSingle(dunesDescription, structures);
			this.PlaceSingle(dunesDescription2, structures);
			return true;
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x00622100 File Offset: 0x00620300
		private void PlaceSingle(DunesBiome.DunesDescription description, StructureMap structures)
		{
			int num = GenBase._random.Next(3) + 8;
			for (int i = 0; i < num - 1; i++)
			{
				int num2 = (int)(2.0 / (double)num * (double)description.Area.Width);
				int num3 = (int)((double)i / (double)num * (double)description.Area.Width + (double)description.Area.Left) + num2 * 2 / 5;
				num3 += GenBase._random.Next(-5, 6);
				double num4 = (double)i / (double)(num - 2);
				double num5 = 1.0 - Math.Abs(num4 - 0.5) * 2.0;
				DunesBiome.PlaceHill(num3 - num2 / 2, num3 + num2 / 2, (num5 * 0.3 + 0.2) * this._heightScale, description);
			}
			int num6 = GenBase._random.Next(2) + 1;
			for (int j = 0; j < num6; j++)
			{
				int num7 = description.Area.Width / 2;
				int num8 = description.Area.Center.X;
				num8 += GenBase._random.Next(-10, 11);
				DunesBiome.PlaceHill(num8 - num7 / 2, num8 + num7 / 2, 0.8 * this._heightScale, description);
			}
			structures.AddStructure(description.Area, 20);
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x00622270 File Offset: 0x00620470
		private static void PlaceHill(int startX, int endX, double scale, DunesBiome.DunesDescription description)
		{
			Point point = new Point(startX, (int)description.Surface[startX]);
			Point point2 = new Point(endX, (int)description.Surface[endX]);
			Point point3 = new Point((point.X + point2.X) / 2, (point.Y + point2.Y) / 2 - (int)(35.0 * scale));
			int num = (point2.X - point3.X) / 4;
			int num2 = (point2.X - point3.X) / 16;
			if (description.WindDirection == DunesBiome.WindDirection.Left)
			{
				point3.X -= WorldGen.genRand.Next(num2, num + 1);
			}
			else
			{
				point3.X += WorldGen.genRand.Next(num2, num + 1);
			}
			Point point4 = new Point(0, (int)(scale * 12.0));
			Point point5 = new Point(point4.X / -2, point4.Y / -2);
			DunesBiome.PlaceCurvedLine(point, point3, (description.WindDirection != DunesBiome.WindDirection.Left) ? point5 : point4, description);
			DunesBiome.PlaceCurvedLine(point3, point2, (description.WindDirection == DunesBiome.WindDirection.Left) ? point5 : point4, description);
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x00622394 File Offset: 0x00620594
		private static void PlaceCurvedLine(Point startPoint, Point endPoint, Point anchorOffset, DunesBiome.DunesDescription description)
		{
			Point point = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
			point.X += anchorOffset.X;
			point.Y += anchorOffset.Y;
			Vector2D vector2D = startPoint.ToVector2D();
			Vector2D vector2D2 = endPoint.ToVector2D();
			Vector2D vector2D3 = point.ToVector2D();
			double num = 0.5 / (vector2D2.X - vector2D.X);
			Point point2 = new Point(-1, -1);
			for (double num2 = 0.0; num2 <= 1.0; num2 += num)
			{
				Vector2D vector2D4 = Vector2D.Lerp(vector2D, vector2D3, num2);
				Vector2D vector2D5 = Vector2D.Lerp(vector2D3, vector2D2, num2);
				Point point3 = Vector2D.Lerp(vector2D4, vector2D5, num2).ToPoint();
				if (!(point3 == point2))
				{
					point2 = point3;
					int num3 = description.Area.Width / 2 - Math.Abs(point3.X - description.Area.Center.X);
					int num4 = (int)description.Surface[point3.X] + (int)(Math.Sqrt((double)num3) * 3.0);
					for (int i = point3.Y - 10; i < point3.Y; i++)
					{
						if (GenBase._tiles[point3.X, i].active() && GenBase._tiles[point3.X, i].type != 53)
						{
							GenBase._tiles[point3.X, i].ClearEverything();
						}
					}
					for (int j = point3.Y; j < num4; j++)
					{
						GenBase._tiles[point3.X, j].ResetToType(53);
					}
				}
			}
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x00622573 File Offset: 0x00620773
		public DunesBiome()
		{
		}

		// Token: 0x04005B14 RID: 23316
		[JsonProperty("SingleDunesWidth")]
		private WorldGenRange _singleDunesWidth = WorldGenRange.Empty;

		// Token: 0x04005B15 RID: 23317
		[JsonProperty("HeightScale")]
		private double _heightScale = 1.0;

		// Token: 0x02000996 RID: 2454
		private class DunesDescription
		{
			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x06004988 RID: 18824 RVA: 0x006D2332 File Offset: 0x006D0532
			// (set) Token: 0x06004989 RID: 18825 RVA: 0x006D233A File Offset: 0x006D053A
			public bool IsValid
			{
				[CompilerGenerated]
				get
				{
					return this.<IsValid>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IsValid>k__BackingField = value;
				}
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x0600498A RID: 18826 RVA: 0x006D2343 File Offset: 0x006D0543
			// (set) Token: 0x0600498B RID: 18827 RVA: 0x006D234B File Offset: 0x006D054B
			public SurfaceMap Surface
			{
				[CompilerGenerated]
				get
				{
					return this.<Surface>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Surface>k__BackingField = value;
				}
			}

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x0600498C RID: 18828 RVA: 0x006D2354 File Offset: 0x006D0554
			// (set) Token: 0x0600498D RID: 18829 RVA: 0x006D235C File Offset: 0x006D055C
			public Rectangle Area
			{
				[CompilerGenerated]
				get
				{
					return this.<Area>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Area>k__BackingField = value;
				}
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x0600498E RID: 18830 RVA: 0x006D2365 File Offset: 0x006D0565
			// (set) Token: 0x0600498F RID: 18831 RVA: 0x006D236D File Offset: 0x006D056D
			public DunesBiome.WindDirection WindDirection
			{
				[CompilerGenerated]
				get
				{
					return this.<WindDirection>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<WindDirection>k__BackingField = value;
				}
			}

			// Token: 0x06004990 RID: 18832 RVA: 0x0000357B File Offset: 0x0000177B
			private DunesDescription()
			{
			}

			// Token: 0x06004991 RID: 18833 RVA: 0x006D2378 File Offset: 0x006D0578
			public static DunesBiome.DunesDescription CreateFromPlacement(Point origin, int width, int height)
			{
				Rectangle rectangle = new Rectangle(origin.X - width / 2, origin.Y - height / 2, width, height);
				return new DunesBiome.DunesDescription
				{
					Area = rectangle,
					IsValid = true,
					Surface = SurfaceMap.FromArea(rectangle.Left - 20, rectangle.Width + 40),
					WindDirection = ((WorldGen.genRand.Next(2) == 0) ? DunesBiome.WindDirection.Left : DunesBiome.WindDirection.Right)
				};
			}

			// Token: 0x0400766C RID: 30316
			[CompilerGenerated]
			private bool <IsValid>k__BackingField;

			// Token: 0x0400766D RID: 30317
			[CompilerGenerated]
			private SurfaceMap <Surface>k__BackingField;

			// Token: 0x0400766E RID: 30318
			[CompilerGenerated]
			private Rectangle <Area>k__BackingField;

			// Token: 0x0400766F RID: 30319
			[CompilerGenerated]
			private DunesBiome.WindDirection <WindDirection>k__BackingField;
		}

		// Token: 0x02000997 RID: 2455
		private enum WindDirection
		{
			// Token: 0x04007671 RID: 30321
			Left,
			// Token: 0x04007672 RID: 30322
			Right
		}
	}
}
