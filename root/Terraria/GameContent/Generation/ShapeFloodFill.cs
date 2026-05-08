using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x0200048A RID: 1162
	public class ShapeFloodFill : GenShape
	{
		// Token: 0x0600336B RID: 13163 RVA: 0x005F6B22 File Offset: 0x005F4D22
		public ShapeFloodFill(int maximumActions = 100)
		{
			this._maximumActions = maximumActions;
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x005F6B34 File Offset: 0x005F4D34
		public override bool Perform(Point origin, GenAction action)
		{
			Queue<Point> queue = new Queue<Point>();
			HashSet<Point16> hashSet = new HashSet<Point16>();
			queue.Enqueue(origin);
			int num = this._maximumActions;
			while (queue.Count > 0)
			{
				if (num <= 0)
				{
					break;
				}
				Point point = queue.Dequeue();
				if (!hashSet.Contains(new Point16(point.X, point.Y)) && base.UnitApply(action, origin, point.X, point.Y, new object[0]))
				{
					hashSet.Add(new Point16(point));
					num--;
					if (point.X + 1 < Main.maxTilesX - 1)
					{
						queue.Enqueue(new Point(point.X + 1, point.Y));
					}
					if (point.X - 1 >= 1)
					{
						queue.Enqueue(new Point(point.X - 1, point.Y));
					}
					if (point.Y + 1 < Main.maxTilesY - 1)
					{
						queue.Enqueue(new Point(point.X, point.Y + 1));
					}
					if (point.Y - 1 >= 1)
					{
						queue.Enqueue(new Point(point.X, point.Y - 1));
					}
				}
			}
			while (queue.Count > 0)
			{
				Point point2 = queue.Dequeue();
				if (!hashSet.Contains(new Point16(point2.X, point2.Y)))
				{
					queue.Enqueue(point2);
					break;
				}
			}
			return queue.Count == 0;
		}

		// Token: 0x040058D8 RID: 22744
		private int _maximumActions;
	}
}
