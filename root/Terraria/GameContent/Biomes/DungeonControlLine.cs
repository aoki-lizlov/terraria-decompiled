using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000501 RID: 1281
	public class DungeonControlLine
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060035E2 RID: 13794 RVA: 0x0061E0F6 File Offset: 0x0061C2F6
		public Vector2D Center
		{
			get
			{
				return (this.End + this.Start) / 2.0;
			}
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x0000357B File Offset: 0x0000177B
		[JsonConstructor]
		private DungeonControlLine()
		{
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x0061E118 File Offset: 0x0061C318
		public DungeonControlLine(Vector2D start, Vector2D end, double startRadius, double endRadius, int progressionStage, DungeonGenerationStyleData style)
		{
			this.Start = start;
			this.End = end;
			this.StartRadius = startRadius;
			this.EndRadius = endRadius;
			this.ProgressionStage = progressionStage;
			this.Style = style;
			Vector2D vector2D = this.End - this.Start;
			this.LineLength = vector2D.Length();
			this.NormalizedLineDirection = vector2D.SafeNormalize(Vector2D.UnitX);
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x0061E188 File Offset: 0x0061C388
		private void CacheNormals()
		{
			this.StartNormal = new Vector2D(this.StartTangent.Y, -this.StartTangent.X);
			this.EndNormal = new Vector2D(-this.EndTangent.Y, this.EndTangent.X);
			this.CrossTangent = Vector2D.Cross(this.StartTangent, this.EndTangent);
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x0061E1F0 File Offset: 0x0061C3F0
		public bool CanPaint(int x, int y, out double distance, out double normalizedLineProgress)
		{
			distance = 0.0;
			normalizedLineProgress = 0.0;
			Vector2D vector2D;
			vector2D..ctor((double)x, (double)y);
			Vector2D vector2D2 = vector2D - this.Start;
			double num = Vector2D.Dot(vector2D2, this.StartTangent);
			if (num < 0.0)
			{
				if (this.Prev != null)
				{
					return false;
				}
				normalizedLineProgress = 0.0;
				distance = vector2D2.Length();
				return true;
			}
			else
			{
				Vector2D vector2D3 = vector2D - this.End;
				double num2 = Vector2D.Dot(vector2D3, this.EndTangent);
				if (num2 >= 0.0)
				{
					double num3 = Vector2D.Dot(vector2D2, this.StartNormal);
					double num4 = Vector2D.Dot(vector2D3, this.EndNormal);
					double num5 = (num + num2) / 2.0;
					num *= num;
					num2 *= num2;
					double num6 = num / (num + num2);
					double num7 = num3 * (1.0 - num6) + num4 * num6 - num5 * this.CrossTangent * num6 * (1.0 - num6);
					distance = Math.Abs(num7);
					normalizedLineProgress = num6;
					return true;
				}
				if (this.Next != null)
				{
					return false;
				}
				normalizedLineProgress = 1.0;
				distance = vector2D3.Length();
				return true;
			}
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x0061E32C File Offset: 0x0061C52C
		public void Paint(int x, int y)
		{
			double num;
			double num2;
			if (!this.CanPaint(x, y, out num, out num2))
			{
				return;
			}
			double num3 = Utils.Lerp(this.StartRadius, this.EndRadius, num2);
			double num4 = num / num3;
			if (num4 > 1.0)
			{
				return;
			}
			DungeonGenerationStyleData styleWithDitheredTransition = this.GetStyleWithDitheredTransition(x, y, num2);
			if (DungeonControlLine.SkipPaintForEdge(x, y, styleWithDitheredTransition, num4))
			{
				return;
			}
			Tile tile = Main.tile[x, y];
			tile.ClearEverything();
			tile.active(true);
			tile.type = styleWithDitheredTransition.BrickTileType;
			tile.wall = styleWithDitheredTransition.BrickWallType;
			if (styleWithDitheredTransition.UnbreakableWallProgressionTier > DualDungeonUnbreakableWallTiers.EarlyGame)
			{
				int num5 = (int)(num3 * DungeonControlLine.NormalizedDistanceSafeFromDither);
				double num6 = num - (double)num5;
				if (num6 >= -4.0 && num6 <= 0.0)
				{
					int num7 = styleWithDitheredTransition.UnbreakableWallProgressionTier;
					if (num6 <= -2.0)
					{
						num7 += 16;
					}
					tile.wall = 350;
					tile.wallColor((byte)num7);
				}
			}
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x0061E42C File Offset: 0x0061C62C
		public DungeonGenerationStyleData GetStyleWithDitheredTransition(int x, int y, double normalizedLineProgress)
		{
			if (normalizedLineProgress < 0.25)
			{
				if (this.Prev != null && this.Prev.Style != this.Style && Utils.Remap(normalizedLineProgress, 0.0, 0.25, 0.5, 1.0, true) <= DitherSnakePass._bayerDither[x % 4, y % 4])
				{
					return this.Prev.Style;
				}
			}
			else if (normalizedLineProgress > 0.75 && this.Next != null && this.Next.Style != this.Style && Utils.Remap(normalizedLineProgress, 0.75, 1.0, 0.0, 0.5, true) >= DitherSnakePass._bayerDither[x % 4, y % 4])
			{
				return this.Next.Style;
			}
			return this.Style;
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x0061E528 File Offset: 0x0061C728
		public static bool SkipPaintForEdge(int x, int y, DungeonGenerationStyleData style, double normalizedDistanceForPoint)
		{
			if (normalizedDistanceForPoint <= DungeonControlLine.NormalizedDistanceSafeFromDither)
			{
				return false;
			}
			double num = 1.0 - (normalizedDistanceForPoint - DungeonControlLine.NormalizedDistanceSafeFromDither) / (1.0 - DungeonControlLine.NormalizedDistanceSafeFromDither);
			if (!style.EdgeDither)
			{
				return num < 0.25;
			}
			if (!WorldGen.InWorld(x, y, 5))
			{
				return false;
			}
			Tile tile = Main.tile[x, y];
			if (tile != null && !tile.active())
			{
				return true;
			}
			if (num <= DitherSnakePass._bayerDither[x % 4, y % 4])
			{
				return true;
			}
			double num2 = Utils.Lerp(0.0, 0.949999988079071, 1.0 - num);
			return (double)WorldGen.genRand.NextFloat() <= num2 || (num <= 0.09375 && WorldGen.genRand.Next(3) != 0) || (num <= 0.125 && WorldGen.genRand.Next(2) == 0) || (num <= 0.15625 && WorldGen.genRand.Next(4) == 0);
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x0061E638 File Offset: 0x0061C838
		public void Paint(Rectangle dungeonBounds)
		{
			this.CacheNormals();
			double num = Utils.Max<double>(new double[] { this.StartRadius, this.EndRadius });
			Point point = this.Start.ToPoint();
			Point point2 = this.End.ToPoint();
			Rectangle rectangle = Rectangle.Union(new Rectangle(point.X, point.Y, 1, 1), new Rectangle(point2.X, point2.Y, 1, 1));
			rectangle.Inflate((int)num, (int)num);
			Rectangle rectangle2 = Rectangle.Intersect(rectangle, dungeonBounds);
			for (int i = rectangle2.Left; i <= rectangle2.Right; i++)
			{
				for (int j = rectangle2.Top; j <= rectangle2.Bottom; j++)
				{
					this.Paint(i, j);
				}
			}
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x0061E704 File Offset: 0x0061C904
		public bool IsSelfIntersecting()
		{
			this.CacheNormals();
			double num = Vector2D.Cross(this.StartNormal, this.EndNormal);
			Vector2D vector2D = this.End - this.Start;
			double num2 = Vector2D.Cross(vector2D, this.EndNormal) / num;
			double num3 = Vector2D.Cross(vector2D, this.StartNormal) / num;
			return Math.Abs(num2) < this.StartRadius || Math.Abs(num3) < this.EndRadius;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x0061E778 File Offset: 0x0061C978
		public bool AdjustTangentsToPreventSelfIntersection()
		{
			if (!this.IsSelfIntersecting())
			{
				return false;
			}
			Vector2D vector2D = (this.StartTangent - this.EndTangent / 2.0).SafeNormalize(default(Vector2D));
			Vector2D vector2D2 = (this.EndTangent - this.StartTangent / 2.0).SafeNormalize(default(Vector2D));
			if (this.Prev != null)
			{
				this.StartTangent = vector2D;
				this.Prev.EndTangent = -this.StartTangent;
			}
			if (this.Next != null)
			{
				this.EndTangent = vector2D2;
				this.Next.StartTangent = -this.EndTangent;
			}
			return true;
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x0061E838 File Offset: 0x0061CA38
		public bool IsInsideBorder(Point point)
		{
			double num;
			double num2;
			return this.CanPaint(point.X, point.Y, out num, out num2) && num < Utils.Lerp(this.StartRadius, this.EndRadius, num2) * DungeonControlLine.NormalizedDistanceSafeFromDither - 4.0;
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x0061E884 File Offset: 0x0061CA84
		public Vector2D GetPotentialRoomPosition(double normalizedDistanceAlong, double normalizedOffset, int roomRadius)
		{
			Vector2D vector2D = Vector2D.Lerp(this.StartNormal, this.EndNormal, normalizedDistanceAlong).SafeNormalize(default(Vector2D));
			double num = Utils.Lerp(this.StartRadius, this.EndRadius, normalizedDistanceAlong) * DungeonControlLine.NormalizedDistanceSafeFromDither - 4.0 - (double)roomRadius;
			return Vector2D.Lerp(this.Start, this.End, normalizedDistanceAlong) + vector2D * num * normalizedOffset;
		}

		// Token: 0x04005AF5 RID: 23285
		public int Index;

		// Token: 0x04005AF6 RID: 23286
		public DungeonControlLine Next;

		// Token: 0x04005AF7 RID: 23287
		public DungeonControlLine Prev;

		// Token: 0x04005AF8 RID: 23288
		public Vector2D Start;

		// Token: 0x04005AF9 RID: 23289
		public Vector2D End;

		// Token: 0x04005AFA RID: 23290
		public Vector2D StartTangent;

		// Token: 0x04005AFB RID: 23291
		public Vector2D EndTangent;

		// Token: 0x04005AFC RID: 23292
		public Vector2D StartNormal;

		// Token: 0x04005AFD RID: 23293
		public Vector2D EndNormal;

		// Token: 0x04005AFE RID: 23294
		public double CrossTangent;

		// Token: 0x04005AFF RID: 23295
		public double StartRadius;

		// Token: 0x04005B00 RID: 23296
		public double EndRadius;

		// Token: 0x04005B01 RID: 23297
		public static double NormalizedDistanceSafeFromDither;

		// Token: 0x04005B02 RID: 23298
		private const double StyleTransitionDitherWidth = 0.5;

		// Token: 0x04005B03 RID: 23299
		private const int BorderWidth = 4;

		// Token: 0x04005B04 RID: 23300
		public Vector2D NormalizedLineDirection;

		// Token: 0x04005B05 RID: 23301
		public double LineLength;

		// Token: 0x04005B06 RID: 23302
		public DungeonGenerationStyleData Style;

		// Token: 0x04005B07 RID: 23303
		public int ProgressionStage;

		// Token: 0x04005B08 RID: 23304
		public bool CurveLine;
	}
}
