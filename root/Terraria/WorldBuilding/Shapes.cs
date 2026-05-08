using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000B8 RID: 184
	public static class Shapes
	{
		// Token: 0x020006DF RID: 1759
		public class Circle : GenShape
		{
			// Token: 0x06003F48 RID: 16200 RVA: 0x00699DEE File Offset: 0x00697FEE
			public Circle(int radius)
			{
				this._verticalRadius = radius;
				this._horizontalRadius = radius;
			}

			// Token: 0x06003F49 RID: 16201 RVA: 0x00699E04 File Offset: 0x00698004
			public Circle(int horizontalRadius, int verticalRadius)
			{
				this._horizontalRadius = horizontalRadius;
				this._verticalRadius = verticalRadius;
			}

			// Token: 0x06003F4A RID: 16202 RVA: 0x00699E1A File Offset: 0x0069801A
			public void SetRadius(int radius)
			{
				this._verticalRadius = radius;
				this._horizontalRadius = radius;
			}

			// Token: 0x06003F4B RID: 16203 RVA: 0x00699E2C File Offset: 0x0069802C
			public override bool Perform(Point origin, GenAction action)
			{
				int num = (this._horizontalRadius + 1) * (this._horizontalRadius + 1);
				for (int i = origin.Y - this._verticalRadius; i <= origin.Y + this._verticalRadius; i++)
				{
					double num2 = (double)this._horizontalRadius / (double)this._verticalRadius * (double)(i - origin.Y);
					int num3 = Math.Min(this._horizontalRadius, (int)Math.Sqrt((double)num - num2 * num2));
					for (int j = origin.X - num3; j <= origin.X + num3; j++)
					{
						if (!base.UnitApply(action, origin, j, i, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040067CF RID: 26575
			private int _verticalRadius;

			// Token: 0x040067D0 RID: 26576
			private int _horizontalRadius;
		}

		// Token: 0x020006E0 RID: 1760
		public class HalfCircle : GenShape
		{
			// Token: 0x06003F4C RID: 16204 RVA: 0x00699EE0 File Offset: 0x006980E0
			public HalfCircle(int radius, bool bottomHalf = false)
			{
				this._radius = radius;
				this._bottomHalf = bottomHalf;
			}

			// Token: 0x06003F4D RID: 16205 RVA: 0x00699EF8 File Offset: 0x006980F8
			public override bool Perform(Point origin, GenAction action)
			{
				int num = (this._radius + 1) * (this._radius + 1);
				int num2 = origin.Y - this._radius;
				int num3 = origin.Y;
				int num4 = 0;
				if (this._bottomHalf)
				{
					num2 = origin.Y;
					num3 = origin.Y + this._radius;
					num4 = -this._radius;
				}
				for (int i = num2; i <= num3; i++)
				{
					int num5 = Math.Min(this._radius, (int)Math.Sqrt((double)(num - (i - origin.Y) * (i - origin.Y))));
					int num6 = i + num4;
					for (int j = origin.X - num5; j <= origin.X + num5; j++)
					{
						if (!base.UnitApply(action, origin, j, num6, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040067D1 RID: 26577
			private int _radius;

			// Token: 0x040067D2 RID: 26578
			private bool _bottomHalf;
		}

		// Token: 0x020006E1 RID: 1761
		public class Slime : GenShape
		{
			// Token: 0x06003F4E RID: 16206 RVA: 0x00699FD1 File Offset: 0x006981D1
			public Slime(int radius)
			{
				this._radius = radius;
				this._xScale = 1.0;
				this._yScale = 1.0;
			}

			// Token: 0x06003F4F RID: 16207 RVA: 0x00699FFE File Offset: 0x006981FE
			public Slime(int radius, double xScale, double yScale)
			{
				this._radius = radius;
				this._xScale = xScale;
				this._yScale = yScale;
			}

			// Token: 0x06003F50 RID: 16208 RVA: 0x0069A01C File Offset: 0x0069821C
			public override bool Perform(Point origin, GenAction action)
			{
				double num = (double)this._radius;
				int num2 = (this._radius + 1) * (this._radius + 1);
				for (int i = origin.Y - (int)(num * this._yScale); i <= origin.Y; i++)
				{
					double num3 = (double)(i - origin.Y) / this._yScale;
					int num4 = (int)Math.Min((double)this._radius * this._xScale, this._xScale * Math.Sqrt((double)num2 - num3 * num3));
					for (int j = origin.X - num4; j <= origin.X + num4; j++)
					{
						if (!base.UnitApply(action, origin, j, i, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				for (int k = origin.Y + 1; k <= origin.Y + (int)(num * this._yScale * 0.5) - 1; k++)
				{
					double num5 = (double)(k - origin.Y) * (2.0 / this._yScale);
					int num6 = (int)Math.Min((double)this._radius * this._xScale, this._xScale * Math.Sqrt((double)num2 - num5 * num5));
					for (int l = origin.X - num6; l <= origin.X + num6; l++)
					{
						if (!base.UnitApply(action, origin, l, k, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040067D3 RID: 26579
			private int _radius;

			// Token: 0x040067D4 RID: 26580
			private double _xScale;

			// Token: 0x040067D5 RID: 26581
			private double _yScale;
		}

		// Token: 0x020006E2 RID: 1762
		public class Rectangle : GenShape
		{
			// Token: 0x06003F51 RID: 16209 RVA: 0x0069A19A File Offset: 0x0069839A
			public Rectangle(Microsoft.Xna.Framework.Rectangle area)
			{
				this._area = area;
			}

			// Token: 0x06003F52 RID: 16210 RVA: 0x0069A1A9 File Offset: 0x006983A9
			public Rectangle(int width, int height)
			{
				this._area = new Microsoft.Xna.Framework.Rectangle(0, 0, width, height);
			}

			// Token: 0x06003F53 RID: 16211 RVA: 0x0069A1C0 File Offset: 0x006983C0
			public void SetArea(Microsoft.Xna.Framework.Rectangle area)
			{
				this._area = area;
			}

			// Token: 0x06003F54 RID: 16212 RVA: 0x0069A1CC File Offset: 0x006983CC
			public override bool Perform(Point origin, GenAction action)
			{
				for (int i = origin.X + this._area.Left; i < origin.X + this._area.Right; i++)
				{
					for (int j = origin.Y + this._area.Top; j < origin.Y + this._area.Bottom; j++)
					{
						if (!base.UnitApply(action, origin, i, j, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040067D6 RID: 26582
			private Microsoft.Xna.Framework.Rectangle _area;
		}

		// Token: 0x020006E3 RID: 1763
		public class Tail : GenShape
		{
			// Token: 0x06003F55 RID: 16213 RVA: 0x0069A252 File Offset: 0x00698452
			public Tail(double width, Vector2D endOffset)
			{
				this._width = width * 16.0;
				this._endOffset = endOffset * 16.0;
			}

			// Token: 0x06003F56 RID: 16214 RVA: 0x0069A280 File Offset: 0x00698480
			public override bool Perform(Point origin, GenAction action)
			{
				Vector2D vector2D = new Vector2D((double)(origin.X << 4), (double)(origin.Y << 4));
				return Utils.PlotTileTale(vector2D, vector2D + this._endOffset, this._width, (int x, int y) => this.UnitApply(action, origin, x, y, new object[0]) || !this._quitOnFail);
			}

			// Token: 0x040067D7 RID: 26583
			private double _width;

			// Token: 0x040067D8 RID: 26584
			private Vector2D _endOffset;

			// Token: 0x02000A48 RID: 2632
			[CompilerGenerated]
			private sealed class <>c__DisplayClass3_0
			{
				// Token: 0x06004AB4 RID: 19124 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass3_0()
				{
				}

				// Token: 0x06004AB5 RID: 19125 RVA: 0x006D4A64 File Offset: 0x006D2C64
				internal bool <Perform>b__0(int x, int y)
				{
					return this.<>4__this.UnitApply(this.action, this.origin, x, y, new object[0]) || !this.<>4__this._quitOnFail;
				}

				// Token: 0x04007715 RID: 30485
				public Shapes.Tail <>4__this;

				// Token: 0x04007716 RID: 30486
				public GenAction action;

				// Token: 0x04007717 RID: 30487
				public Point origin;
			}
		}

		// Token: 0x020006E4 RID: 1764
		public class Mound : GenShape
		{
			// Token: 0x06003F57 RID: 16215 RVA: 0x0069A2EC File Offset: 0x006984EC
			public Mound(int halfWidth, int height)
			{
				this._halfWidth = halfWidth;
				this._height = height;
			}

			// Token: 0x06003F58 RID: 16216 RVA: 0x0069A304 File Offset: 0x00698504
			public override bool Perform(Point origin, GenAction action)
			{
				int height = this._height;
				double num = (double)this._halfWidth;
				for (int i = -this._halfWidth; i <= this._halfWidth; i++)
				{
					int num2 = Math.Min(this._height, (int)(-((double)(this._height + 1) / (num * num)) * ((double)i + num) * ((double)i - num)));
					for (int j = 0; j < num2; j++)
					{
						if (!base.UnitApply(action, origin, i + origin.X, origin.Y - j, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x040067D9 RID: 26585
			private int _halfWidth;

			// Token: 0x040067DA RID: 26586
			private int _height;
		}
	}
}
