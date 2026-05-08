using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000030 RID: 48
	[TypeConverter(typeof(PointConverter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Point : IEquatable<Point>
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00018BF6 File Offset: 0x00016DF6
		public static Point Zero
		{
			get
			{
				return Point.zeroPoint;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00018BFD File Offset: 0x00016DFD
		internal string DebugDisplayString
		{
			get
			{
				return this.X.ToString() + " " + this.Y.ToString();
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00018C1F File Offset: 0x00016E1F
		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00018C2F File Offset: 0x00016E2F
		public bool Equals(Point other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00018C4F File Offset: 0x00016E4F
		public override bool Equals(object obj)
		{
			return obj is Point && this.Equals((Point)obj);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00018C67 File Offset: 0x00016E67
		public override int GetHashCode()
		{
			return this.X ^ this.Y;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00018C78 File Offset: 0x00016E78
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X:",
				this.X.ToString(),
				" Y:",
				this.Y.ToString(),
				"}"
			});
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00018CC4 File Offset: 0x00016EC4
		public static Point operator +(Point value1, Point value2)
		{
			return new Point(value1.X + value2.X, value1.Y + value2.Y);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00018CE5 File Offset: 0x00016EE5
		public static Point operator -(Point value1, Point value2)
		{
			return new Point(value1.X - value2.X, value1.Y - value2.Y);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00018D06 File Offset: 0x00016F06
		public static Point operator *(Point value1, Point value2)
		{
			return new Point(value1.X * value2.X, value1.Y * value2.Y);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00018D27 File Offset: 0x00016F27
		public static Point operator /(Point value1, Point value2)
		{
			return new Point(value1.X / value2.X, value1.Y / value2.Y);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00018D48 File Offset: 0x00016F48
		public static bool operator ==(Point a, Point b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00018D52 File Offset: 0x00016F52
		public static bool operator !=(Point a, Point b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00009E6B File Offset: 0x0000806B
		// Note: this type is marked as 'beforefieldinit'.
		static Point()
		{
		}

		// Token: 0x040005AC RID: 1452
		public int X;

		// Token: 0x040005AD RID: 1453
		public int Y;

		// Token: 0x040005AE RID: 1454
		private static Point zeroPoint;
	}
}
