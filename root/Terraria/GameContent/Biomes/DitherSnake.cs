using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000502 RID: 1282
	public class DitherSnake : List<DungeonControlLine>
	{
		// Token: 0x060035EF RID: 13807 RVA: 0x0061E8FC File Offset: 0x0061CAFC
		public new void Add(DungeonControlLine line)
		{
			if (base.Count > 0)
			{
				DungeonControlLine dungeonControlLine = this.Last<DungeonControlLine>();
				dungeonControlLine.Next = line;
				line.Prev = dungeonControlLine;
			}
			line.Index = base.Count;
			base.Add(line);
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x0061E93C File Offset: 0x0061CB3C
		public DungeonControlLine GetClosestLineTo(Vector2D pos)
		{
			DungeonControlLine dungeonControlLine = null;
			double num = double.MaxValue;
			foreach (DungeonControlLine dungeonControlLine2 in this)
			{
				double num2 = dungeonControlLine2.Center.Distance(pos);
				if (num2 < num)
				{
					dungeonControlLine = dungeonControlLine2;
					num = num2;
				}
			}
			return dungeonControlLine;
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x0061E9A8 File Offset: 0x0061CBA8
		public DungeonControlLine GetLineContaining(Vector2D pos, DungeonControlLine initialGuess = null, int depth = 0)
		{
			if (initialGuess == null)
			{
				initialGuess = this.GetClosestLineTo(pos);
			}
			if (depth == 3)
			{
				return null;
			}
			if (Vector2D.Dot(pos - initialGuess.Start, initialGuess.StartTangent) < 0.0 && initialGuess.Prev != null)
			{
				return this.GetLineContaining(pos, initialGuess.Prev, depth + 1);
			}
			if (Vector2D.Dot(pos - initialGuess.End, initialGuess.EndTangent) < 0.0 && initialGuess.Next != null)
			{
				return this.GetLineContaining(pos, initialGuess.Next, depth + 1);
			}
			return initialGuess;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x0061EA40 File Offset: 0x0061CC40
		public double GetPositionAlongSnake(Vector2D pos)
		{
			DungeonControlLine lineContaining = this.GetLineContaining(pos, null, 0);
			double num;
			double num2;
			if (!lineContaining.CanPaint((int)pos.X, (int)pos.Y, out num, out num2))
			{
				num2 = 0.5;
			}
			return (double)lineContaining.Index + num2;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x0061EA84 File Offset: 0x0061CC84
		public bool IsCircleInsideBorderWithStyle(DungeonGenerationStyleData style, Vector2D center, int radius)
		{
			DungeonControlLine closestLineTo = this.GetClosestLineTo(center);
			return closestLineTo.Style == style && this.IsCircleInsideBorderWithMatchingStyle(closestLineTo, center, radius);
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x0061EAB0 File Offset: 0x0061CCB0
		public bool IsCircleInsideBorderWithMatchingStyle(DungeonControlLine nearbyLine, Vector2D center, int radius)
		{
			double num = (double)radius * DitherSnake.ExtraBuffer;
			foreach (Vector2D vector2D in DitherSnake.CircleTestPoints)
			{
				Vector2D vector2D2 = center + vector2D * num;
				DungeonControlLine lineContaining = this.GetLineContaining(vector2D2, nearbyLine, 0);
				if (lineContaining == null || lineContaining.Style != nearbyLine.Style)
				{
					return false;
				}
				if (!lineContaining.IsInsideBorder(vector2D2.ToPoint()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x0061EB28 File Offset: 0x0061CD28
		public Vector2D GetRoomPositionInsideBorder(DungeonControlLine line, double normalizedDistanceAlong, double normalizedDistanceFrom, int roomRadius, out SnakeOrientation orientation)
		{
			orientation = SnakeOrientation.Unknown;
			Vector2D vector2D = Vector2D.Lerp(line.Start, line.End, normalizedDistanceAlong);
			Vector2D potentialRoomPosition = line.GetPotentialRoomPosition(normalizedDistanceAlong, 0.0, roomRadius);
			Vector2D potentialRoomPosition2 = line.GetPotentialRoomPosition(normalizedDistanceAlong, 1.0, roomRadius);
			Vector2D vector2D2 = ((potentialRoomPosition.Y < potentialRoomPosition2.Y) ? potentialRoomPosition : potentialRoomPosition2);
			Vector2D vector2D3 = ((potentialRoomPosition.Y > potentialRoomPosition2.Y) ? potentialRoomPosition : potentialRoomPosition2);
			for (int i = 0; i < 4; i++)
			{
				Vector2D potentialRoomPosition3 = line.GetPotentialRoomPosition(normalizedDistanceAlong, normalizedDistanceFrom, roomRadius);
				if (this.IsCircleInsideBorderWithMatchingStyle(line, potentialRoomPosition3, roomRadius))
				{
					double num = potentialRoomPosition3.Distance(vector2D);
					double num2 = potentialRoomPosition3.Distance(vector2D2);
					double num3 = potentialRoomPosition3.Distance(vector2D3);
					if (num < num2 && num < num3)
					{
						orientation = SnakeOrientation.Center;
					}
					else if (num2 < num3)
					{
						orientation = SnakeOrientation.Top;
					}
					else
					{
						orientation = SnakeOrientation.Bottom;
					}
					return potentialRoomPosition3;
				}
				normalizedDistanceFrom *= 0.8;
			}
			orientation = SnakeOrientation.Center;
			return line.Center;
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x0061EC20 File Offset: 0x0061CE20
		public void SetTangents()
		{
			DungeonControlLine dungeonControlLine = base[0];
			dungeonControlLine.StartTangent = dungeonControlLine.NormalizedLineDirection;
			while (dungeonControlLine.Next != null)
			{
				DungeonControlLine next = dungeonControlLine.Next;
				Vector2D vector2D = (dungeonControlLine.NormalizedLineDirection + next.NormalizedLineDirection).SafeNormalize(default(Vector2D));
				next.StartTangent = vector2D;
				dungeonControlLine.EndTangent = -vector2D;
				dungeonControlLine = next;
			}
			dungeonControlLine.EndTangent = -dungeonControlLine.NormalizedLineDirection;
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x0061EC98 File Offset: 0x0061CE98
		public void AdjustTangentsToPreventSelfIntersection()
		{
			for (int i = 0; i < 100; i++)
			{
				bool flag = false;
				using (List<DungeonControlLine>.Enumerator enumerator = base.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.AdjustTangentsToPreventSelfIntersection())
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					break;
				}
			}
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x0061ECFC File Offset: 0x0061CEFC
		public bool IsLineInsideBorder(Vector2D from, Vector2D to, int halfWidth)
		{
			Vector2D vector2D = (to - from).SafeNormalize(Vector2D.UnitX).RotatedBy(1.5707963267948966, default(Vector2D)) * (double)halfWidth;
			return this.IsLineInsideBorder(from + vector2D, to + vector2D) && this.IsLineInsideBorder(from - vector2D, to - vector2D);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x0061ED68 File Offset: 0x0061CF68
		public bool IsLineInsideBorder(Vector2D from, Vector2D to)
		{
			DungeonControlLine line = this.GetClosestLineTo(from);
			return Utils.PlotLine(from.ToPoint(), to.ToPoint(), delegate(int x, int y)
			{
				line = this.GetLineContaining(new Vector2D((double)x, (double)y), line, 0);
				return line != null && line.IsInsideBorder(new Point(x, y));
			}, true);
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x0061EDAD File Offset: 0x0061CFAD
		public DitherSnake()
		{
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x0061EDB8 File Offset: 0x0061CFB8
		// Note: this type is marked as 'beforefieldinit'.
		static DitherSnake()
		{
		}

		// Token: 0x04005B09 RID: 23305
		private static readonly Vector2D[] CircleTestPoints = (from i in Enumerable.Range(0, 12)
			select Vector2D.UnitX.RotatedBy(6.283185307179586 * (double)i / 12.0, default(Vector2D))).ToArray<Vector2D>();

		// Token: 0x04005B0A RID: 23306
		private static readonly double ExtraBuffer = 1.0 / Math.Cos(0.5235987755982988);

		// Token: 0x0200098E RID: 2446
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0
		{
			// Token: 0x0600497A RID: 18810 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x0600497B RID: 18811 RVA: 0x006D2198 File Offset: 0x006D0398
			internal bool <IsLineInsideBorder>b__0(int x, int y)
			{
				this.line = this.<>4__this.GetLineContaining(new Vector2D((double)x, (double)y), this.line, 0);
				return this.line != null && this.line.IsInsideBorder(new Point(x, y));
			}

			// Token: 0x04007652 RID: 30290
			public DungeonControlLine line;

			// Token: 0x04007653 RID: 30291
			public DitherSnake <>4__this;
		}

		// Token: 0x0200098F RID: 2447
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600497C RID: 18812 RVA: 0x006D21E5 File Offset: 0x006D03E5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600497D RID: 18813 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600497E RID: 18814 RVA: 0x006D21F4 File Offset: 0x006D03F4
			internal Vector2D <.cctor>b__14_0(int i)
			{
				return Vector2D.UnitX.RotatedBy(6.283185307179586 * (double)i / 12.0, default(Vector2D));
			}

			// Token: 0x04007654 RID: 30292
			public static readonly DitherSnake.<>c <>9 = new DitherSnake.<>c();
		}
	}
}
