using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000BA RID: 186
	public class ShapeData
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x004DE47B File Offset: 0x004DC67B
		public int Count
		{
			get
			{
				return this._points.Count;
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x004DE488 File Offset: 0x004DC688
		public ShapeData()
		{
			this._points = new HashSet<Point16>();
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x004DE49B File Offset: 0x004DC69B
		public ShapeData(ShapeData original)
		{
			this._points = new HashSet<Point16>(original._points);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x004DE4B4 File Offset: 0x004DC6B4
		public void Add(int x, int y)
		{
			Point16 point = new Point16(x, y);
			if (!this._points.Contains(point))
			{
				this._points.Add(point);
			}
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x004DE4E8 File Offset: 0x004DC6E8
		public void AddBounds(int minX, int minY, int maxX, int maxY)
		{
			for (int i = minX; i <= maxX; i++)
			{
				for (int j = minY; j <= maxY; j++)
				{
					this.Add(i, j);
				}
			}
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x004DE518 File Offset: 0x004DC718
		public void Remove(int x, int y)
		{
			Point16 point = new Point16(x, y);
			if (this._points.Contains(point))
			{
				this._points.Remove(point);
			}
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x004DE54C File Offset: 0x004DC74C
		public void RemoveBounds(int minX, int minY, int maxX, int maxY)
		{
			for (int i = minX; i <= maxX; i++)
			{
				for (int j = minY; j <= maxY; j++)
				{
					this.Remove(i, j);
				}
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x004DE57A File Offset: 0x004DC77A
		public HashSet<Point16> GetData()
		{
			return this._points;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x004DE582 File Offset: 0x004DC782
		public void Clear()
		{
			this._points.Clear();
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x004DE58F File Offset: 0x004DC78F
		public bool Contains(int x, int y)
		{
			return this._points.Contains(new Point16(x, y));
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x004DE5A4 File Offset: 0x004DC7A4
		public void Add(ShapeData shapeData, Point localOrigin, Point remoteOrigin)
		{
			foreach (Point16 point in shapeData.GetData())
			{
				this.Add(remoteOrigin.X - localOrigin.X + (int)point.X, remoteOrigin.Y - localOrigin.Y + (int)point.Y);
			}
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x004DE620 File Offset: 0x004DC820
		public void Subtract(ShapeData shapeData, Point localOrigin, Point remoteOrigin)
		{
			foreach (Point16 point in shapeData.GetData())
			{
				this.Remove(remoteOrigin.X - localOrigin.X + (int)point.X, remoteOrigin.Y - localOrigin.Y + (int)point.Y);
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x004DE69C File Offset: 0x004DC89C
		public static Rectangle GetBounds(Point origin, params ShapeData[] shapes)
		{
			int num = (int)shapes[0]._points.First<Point16>().X;
			int num2 = num;
			int num3 = (int)shapes[0]._points.First<Point16>().Y;
			int num4 = num3;
			for (int i = 0; i < shapes.Length; i++)
			{
				foreach (Point16 point in shapes[i]._points)
				{
					num = Math.Max(num, (int)point.X);
					num2 = Math.Min(num2, (int)point.X);
					num3 = Math.Max(num3, (int)point.Y);
					num4 = Math.Min(num4, (int)point.Y);
				}
			}
			return new Rectangle(num2 + origin.X, num4 + origin.Y, num - num2, num3 - num4);
		}

		// Token: 0x04001282 RID: 4738
		private HashSet<Point16> _points;
	}
}
