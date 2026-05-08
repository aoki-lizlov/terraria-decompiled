using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000034 RID: 52
	[TypeConverter(typeof(RectangleConverter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Rectangle : IEquatable<Rectangle>
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0001A540 File Offset: 0x00018740
		public int Left
		{
			get
			{
				return this.X;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0001A548 File Offset: 0x00018748
		public int Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0001A557 File Offset: 0x00018757
		public int Top
		{
			get
			{
				return this.Y;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0001A55F File Offset: 0x0001875F
		public int Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0001A56E File Offset: 0x0001876E
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x0001A581 File Offset: 0x00018781
		public Point Location
		{
			get
			{
				return new Point(this.X, this.Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0001A59B File Offset: 0x0001879B
		public Point Center
		{
			get
			{
				return new Point(this.X + this.Width / 2, this.Y + this.Height / 2);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0001A5C0 File Offset: 0x000187C0
		public bool IsEmpty
		{
			get
			{
				return this.Width == 0 && this.Height == 0 && this.X == 0 && this.Y == 0;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0001A5E5 File Offset: 0x000187E5
		public static Rectangle Empty
		{
			get
			{
				return Rectangle.emptyRectangle;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0001A5EC File Offset: 0x000187EC
		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(new string[]
				{
					this.X.ToString(),
					" ",
					this.Y.ToString(),
					" ",
					this.Width.ToString(),
					" ",
					this.Height.ToString()
				});
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0001A654 File Offset: 0x00018854
		public Rectangle(int x, int y, int width, int height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0001A673 File Offset: 0x00018873
		public bool Contains(int x, int y)
		{
			return this.X <= x && x < this.X + this.Width && this.Y <= y && y < this.Y + this.Height;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0001A6AC File Offset: 0x000188AC
		public bool Contains(Point value)
		{
			return this.X <= value.X && value.X < this.X + this.Width && this.Y <= value.Y && value.Y < this.Y + this.Height;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0001A704 File Offset: 0x00018904
		public bool Contains(Rectangle value)
		{
			return this.X <= value.X && value.X + value.Width <= this.X + this.Width && this.Y <= value.Y && value.Y + value.Height <= this.Y + this.Height;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0001A76C File Offset: 0x0001896C
		public void Contains(ref Point value, out bool result)
		{
			result = this.X <= value.X && value.X < this.X + this.Width && this.Y <= value.Y && value.Y < this.Y + this.Height;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0001A7C4 File Offset: 0x000189C4
		public void Contains(ref Rectangle value, out bool result)
		{
			result = this.X <= value.X && value.X + value.Width <= this.X + this.Width && this.Y <= value.Y && value.Y + value.Height <= this.Y + this.Height;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0001A82D File Offset: 0x00018A2D
		public void Offset(Point offset)
		{
			this.X += offset.X;
			this.Y += offset.Y;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0001A855 File Offset: 0x00018A55
		public void Offset(int offsetX, int offsetY)
		{
			this.X += offsetX;
			this.Y += offsetY;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0001A873 File Offset: 0x00018A73
		public void Inflate(int horizontalValue, int verticalValue)
		{
			this.X -= horizontalValue;
			this.Y -= verticalValue;
			this.Width += horizontalValue * 2;
			this.Height += verticalValue * 2;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0001A8B1 File Offset: 0x00018AB1
		public bool Equals(Rectangle other)
		{
			return this == other;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0001A8BF File Offset: 0x00018ABF
		public override bool Equals(object obj)
		{
			return obj is Rectangle && this == (Rectangle)obj;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0001A8DC File Offset: 0x00018ADC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X:",
				this.X.ToString(),
				" Y:",
				this.Y.ToString(),
				" Width:",
				this.Width.ToString(),
				" Height:",
				this.Height.ToString(),
				"}"
			});
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0001A955 File Offset: 0x00018B55
		public override int GetHashCode()
		{
			return this.X ^ this.Y ^ this.Width ^ this.Height;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0001A972 File Offset: 0x00018B72
		public bool Intersects(Rectangle value)
		{
			return value.Left < this.Right && this.Left < value.Right && value.Top < this.Bottom && this.Top < value.Bottom;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0001A9B2 File Offset: 0x00018BB2
		public void Intersects(ref Rectangle value, out bool result)
		{
			result = value.Left < this.Right && this.Left < value.Right && value.Top < this.Bottom && this.Top < value.Bottom;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0001A9F1 File Offset: 0x00018BF1
		public static bool operator ==(Rectangle a, Rectangle b)
		{
			return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0001AA2D File Offset: 0x00018C2D
		public static bool operator !=(Rectangle a, Rectangle b)
		{
			return !(a == b);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0001AA3C File Offset: 0x00018C3C
		public static Rectangle Intersect(Rectangle value1, Rectangle value2)
		{
			Rectangle rectangle;
			Rectangle.Intersect(ref value1, ref value2, out rectangle);
			return rectangle;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0001AA58 File Offset: 0x00018C58
		public static void Intersect(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
		{
			if (value1.Intersects(value2))
			{
				int num = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
				int num2 = Math.Max(value1.X, value2.X);
				int num3 = Math.Max(value1.Y, value2.Y);
				int num4 = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);
				result = new Rectangle(num2, num3, num - num2, num4 - num3);
				return;
			}
			result = new Rectangle(0, 0, 0, 0);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0001AAFC File Offset: 0x00018CFC
		public static Rectangle Union(Rectangle value1, Rectangle value2)
		{
			int num = Math.Min(value1.X, value2.X);
			int num2 = Math.Min(value1.Y, value2.Y);
			return new Rectangle(num, num2, Math.Max(value1.Right, value2.Right) - num, Math.Max(value1.Bottom, value2.Bottom) - num2);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0001AB60 File Offset: 0x00018D60
		public static void Union(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
		{
			result.X = Math.Min(value1.X, value2.X);
			result.Y = Math.Min(value1.Y, value2.Y);
			result.Width = Math.Max(value1.Right, value2.Right) - result.X;
			result.Height = Math.Max(value1.Bottom, value2.Bottom) - result.Y;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00009E6B File Offset: 0x0000806B
		// Note: this type is marked as 'beforefieldinit'.
		static Rectangle()
		{
		}

		// Token: 0x040005B7 RID: 1463
		public int X;

		// Token: 0x040005B8 RID: 1464
		public int Y;

		// Token: 0x040005B9 RID: 1465
		public int Width;

		// Token: 0x040005BA RID: 1466
		public int Height;

		// Token: 0x040005BB RID: 1467
		private static Rectangle emptyRectangle;
	}
}
