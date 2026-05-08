using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020005AC RID: 1452
	public struct Point16
	{
		// Token: 0x06003968 RID: 14696 RVA: 0x00652112 File Offset: 0x00650312
		public Point16(Point point)
		{
			this.X = (short)point.X;
			this.Y = (short)point.Y;
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x0065212E File Offset: 0x0065032E
		public Point16(int X, int Y)
		{
			this.X = (short)X;
			this.Y = (short)Y;
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x00652140 File Offset: 0x00650340
		public Point16(short X, short Y)
		{
			this.X = X;
			this.Y = Y;
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x00652150 File Offset: 0x00650350
		public static Point16 Max(int firstX, int firstY, int secondX, int secondY)
		{
			return new Point16((firstX > secondX) ? firstX : secondX, (firstY > secondY) ? firstY : secondY);
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x00652167 File Offset: 0x00650367
		public Point16 Max(int compareX, int compareY)
		{
			return new Point16(((int)this.X > compareX) ? ((int)this.X) : compareX, ((int)this.Y > compareY) ? ((int)this.Y) : compareY);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x00652192 File Offset: 0x00650392
		public Point16 Max(Point16 compareTo)
		{
			return new Point16((this.X > compareTo.X) ? this.X : compareTo.X, (this.Y > compareTo.Y) ? this.Y : compareTo.Y);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x006521D1 File Offset: 0x006503D1
		public static bool operator ==(Point16 first, Point16 second)
		{
			return first.X == second.X && first.Y == second.Y;
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x006521F1 File Offset: 0x006503F1
		public static bool operator !=(Point16 first, Point16 second)
		{
			return first.X != second.X || first.Y != second.Y;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x00652214 File Offset: 0x00650414
		public override bool Equals(object obj)
		{
			Point16 point = (Point16)obj;
			return this.X == point.X && this.Y == point.Y;
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x00652247 File Offset: 0x00650447
		public override int GetHashCode()
		{
			return ((int)this.X << 16) | (int)((ushort)this.Y);
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x0065225A File Offset: 0x0065045A
		public override string ToString()
		{
			return string.Format("{{{0}, {1}}}", this.X, this.Y);
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x0065227C File Offset: 0x0065047C
		public static implicit operator Point(Point16 p)
		{
			return new Point((int)p.X, (int)p.Y);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x0065228F File Offset: 0x0065048F
		// Note: this type is marked as 'beforefieldinit'.
		static Point16()
		{
		}

		// Token: 0x04005D9C RID: 23964
		public short X;

		// Token: 0x04005D9D RID: 23965
		public short Y;

		// Token: 0x04005D9E RID: 23966
		public static Point16 Zero = new Point16(0, 0);

		// Token: 0x04005D9F RID: 23967
		public static Point16 NegativeOne = new Point16(-1, -1);
	}
}
