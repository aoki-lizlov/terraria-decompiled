using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x02000489 RID: 1161
	public class ShapeBranch : GenShape
	{
		// Token: 0x06003365 RID: 13157 RVA: 0x005F67E5 File Offset: 0x005F49E5
		public ShapeBranch()
		{
			this._offset = new Point(10, -5);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x005F67FC File Offset: 0x005F49FC
		public ShapeBranch(Point offset)
		{
			this._offset = offset;
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x005F680B File Offset: 0x005F4A0B
		public ShapeBranch(double angle, double distance)
		{
			this._offset = new Point((int)(Math.Cos(angle) * distance), (int)(Math.Sin(angle) * distance));
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x005F6830 File Offset: 0x005F4A30
		private bool PerformSegment(Point origin, GenAction action, Point start, Point end, int size)
		{
			size = Math.Max(1, size);
			Utils.TileActionAttempt <>9__0;
			for (int i = -(size >> 1); i < size - (size >> 1); i++)
			{
				for (int j = -(size >> 1); j < size - (size >> 1); j++)
				{
					Point point = new Point(start.X + i, start.Y + j);
					Utils.TileActionAttempt tileActionAttempt;
					if ((tileActionAttempt = <>9__0) == null)
					{
						tileActionAttempt = (<>9__0 = (int tileX, int tileY) => this.UnitApply(action, origin, tileX, tileY, new object[0]) || !this._quitOnFail);
					}
					if (!Utils.PlotLine(point, end, tileActionAttempt, false))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x005F68D0 File Offset: 0x005F4AD0
		public override bool Perform(Point origin, GenAction action)
		{
			Vector2D vector2D;
			vector2D..ctor((double)this._offset.X, (double)this._offset.Y);
			double num = vector2D.Length();
			int num2 = (int)(num / 6.0);
			if (this._endPoints != null)
			{
				this._endPoints.Add(new Point(origin.X + this._offset.X, origin.Y + this._offset.Y));
			}
			if (!this.PerformSegment(origin, action, origin, new Point(origin.X + this._offset.X, origin.Y + this._offset.Y), num2))
			{
				return false;
			}
			int num3 = (int)(num / 8.0);
			for (int i = 0; i < num3; i++)
			{
				double num4 = ((double)i + 1.0) / ((double)num3 + 1.0);
				Point point = new Point((int)(num4 * (double)this._offset.X), (int)(num4 * (double)this._offset.Y));
				Vector2D vector2D2;
				vector2D2..ctor((double)(this._offset.X - point.X), (double)(this._offset.Y - point.Y));
				vector2D2 = vector2D2.RotatedBy((GenBase._random.NextDouble() * 0.5 + 1.0) * (double)((GenBase._random.Next(2) == 0) ? (-1) : 1), default(Vector2D)) * 0.75;
				Point point2 = new Point((int)vector2D2.X + point.X, (int)vector2D2.Y + point.Y);
				if (this._endPoints != null)
				{
					this._endPoints.Add(new Point(point2.X + origin.X, point2.Y + origin.Y));
				}
				if (!this.PerformSegment(origin, action, new Point(point.X + origin.X, point.Y + origin.Y), new Point(point2.X + origin.X, point2.Y + origin.Y), num2 - 1))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x005F6B18 File Offset: 0x005F4D18
		public ShapeBranch OutputEndpoints(List<Point> endpoints)
		{
			this._endPoints = endpoints;
			return this;
		}

		// Token: 0x040058D6 RID: 22742
		private Point _offset;

		// Token: 0x040058D7 RID: 22743
		private List<Point> _endPoints;

		// Token: 0x0200097A RID: 2426
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x0600493C RID: 18748 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600493D RID: 18749 RVA: 0x006D0CF2 File Offset: 0x006CEEF2
			internal bool <PerformSegment>b__0(int tileX, int tileY)
			{
				return this.<>4__this.UnitApply(this.action, this.origin, tileX, tileY, new object[0]) || !this.<>4__this._quitOnFail;
			}

			// Token: 0x0400761A RID: 30234
			public ShapeBranch <>4__this;

			// Token: 0x0400761B RID: 30235
			public GenAction action;

			// Token: 0x0400761C RID: 30236
			public Point origin;

			// Token: 0x0400761D RID: 30237
			public Utils.TileActionAttempt <>9__0;
		}
	}
}
