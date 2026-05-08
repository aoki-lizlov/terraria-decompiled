using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x020004A0 RID: 1184
	public class DungeonShapes
	{
		// Token: 0x06003414 RID: 13332 RVA: 0x0000357B File Offset: 0x0000177B
		public DungeonShapes()
		{
		}

		// Token: 0x02000985 RID: 2437
		public class CircleRoom : GenShape
		{
			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x0600494E RID: 18766 RVA: 0x006D0DFF File Offset: 0x006CEFFF
			public int VerticalRadius
			{
				get
				{
					return this._verticalRadius;
				}
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x0600494F RID: 18767 RVA: 0x006D0E07 File Offset: 0x006CF007
			public int HorizontalRadius
			{
				get
				{
					return this._horizontalRadius;
				}
			}

			// Token: 0x06004950 RID: 18768 RVA: 0x006D0E0F File Offset: 0x006CF00F
			public CircleRoom(int radius)
			{
				this._verticalRadius = radius;
				this._horizontalRadius = radius;
			}

			// Token: 0x06004951 RID: 18769 RVA: 0x006D0E25 File Offset: 0x006CF025
			public CircleRoom(int horizontalRadius, int verticalRadius)
			{
				this._horizontalRadius = horizontalRadius;
				this._verticalRadius = verticalRadius;
			}

			// Token: 0x06004952 RID: 18770 RVA: 0x006D0E3B File Offset: 0x006CF03B
			public void SetRadius(int radius)
			{
				this._verticalRadius = radius;
				this._horizontalRadius = radius;
			}

			// Token: 0x06004953 RID: 18771 RVA: 0x006D0E4C File Offset: 0x006CF04C
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

			// Token: 0x04007632 RID: 30258
			private int _verticalRadius;

			// Token: 0x04007633 RID: 30259
			private int _horizontalRadius;
		}

		// Token: 0x02000986 RID: 2438
		public class MoundRoom : GenShape
		{
			// Token: 0x06004954 RID: 18772 RVA: 0x006D0F00 File Offset: 0x006CF100
			public MoundRoom(int halfWidth, int height)
			{
				this._halfWidth = halfWidth;
				this._height = height;
			}

			// Token: 0x06004955 RID: 18773 RVA: 0x006D0F18 File Offset: 0x006CF118
			public override bool Perform(Point origin, GenAction action)
			{
				int height = this._height;
				float num = (float)this._halfWidth;
				int num2 = this._height / 2;
				for (int i = -this._halfWidth; i <= this._halfWidth; i++)
				{
					int num3 = Math.Min(this._height, (int)(-((float)(this._height + 1) / (num * num)) * ((float)i + num) * ((float)i - num)));
					for (int j = 0; j < num3; j++)
					{
						if (!base.UnitApply(action, origin, i + origin.X, origin.Y - j + num2, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x04007634 RID: 30260
			private int _halfWidth;

			// Token: 0x04007635 RID: 30261
			private int _height;
		}

		// Token: 0x02000987 RID: 2439
		public class HourglassRoom : GenShape
		{
			// Token: 0x06004956 RID: 18774 RVA: 0x006D0FB7 File Offset: 0x006CF1B7
			public HourglassRoom(int width, int height, float percentileAddon)
			{
				this._width = width;
				this._height = height;
				this._percentileAddon = percentileAddon;
			}

			// Token: 0x06004957 RID: 18775 RVA: 0x006D0FD4 File Offset: 0x006CF1D4
			public override bool Perform(Point origin, GenAction action)
			{
				int num = this._height / 2;
				for (int i = -num; i <= num; i++)
				{
					int num2 = origin.Y + i;
					float num3 = ((float)i + (float)num) / (float)this._height;
					float num4 = Math.Max(0f, Math.Min(1f, Utils.MultiLerp(Utils.WrappedLerp(0f, 1f, num3), new float[] { 1f, 1f, 0.75f, 0.65f, 0.45f, 0.4f, 0.35f, 0.35f }) + this._percentileAddon));
					int num5 = (int)((float)this._width * num4) / 2;
					for (int j = -num5; j <= num5; j++)
					{
						int num6 = origin.X + j;
						if (!base.UnitApply(action, origin, num6, num2, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x04007636 RID: 30262
			private int _width;

			// Token: 0x04007637 RID: 30263
			private int _height;

			// Token: 0x04007638 RID: 30264
			private float _percentileAddon;
		}

		// Token: 0x02000988 RID: 2440
		public class QuadCircleRoom : GenShape
		{
			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x06004958 RID: 18776 RVA: 0x006D10A2 File Offset: 0x006CF2A2
			public int Radius
			{
				get
				{
					return this._radius;
				}
			}

			// Token: 0x06004959 RID: 18777 RVA: 0x006D10AA File Offset: 0x006CF2AA
			public QuadCircleRoom(int radius, int distanceBetweenSpheres)
			{
				this._radius = radius;
				this._distanceBetweenSpheres = distanceBetweenSpheres;
			}

			// Token: 0x0600495A RID: 18778 RVA: 0x006D10C0 File Offset: 0x006CF2C0
			public void SetRadius(int radius)
			{
				this._radius = radius;
			}

			// Token: 0x0600495B RID: 18779 RVA: 0x006D10CC File Offset: 0x006CF2CC
			public override bool Perform(Point origin, GenAction action)
			{
				int num = (this._radius + 1) * (this._radius + 1);
				int num2 = 3;
				for (int i = 0; i < 5; i++)
				{
					Point point;
					switch (i)
					{
					default:
						point = new Vector2((float)origin.X, (float)(origin.Y - this._distanceBetweenSpheres + num2)).ToPoint();
						break;
					case 1:
						point = new Vector2((float)origin.X, (float)(origin.Y + this._distanceBetweenSpheres - num2)).ToPoint();
						break;
					case 2:
						point = new Vector2((float)(origin.X - this._distanceBetweenSpheres + num2), (float)origin.Y).ToPoint();
						break;
					case 3:
						point = new Vector2((float)(origin.X + this._distanceBetweenSpheres - num2), (float)origin.Y).ToPoint();
						break;
					case 4:
						point = origin;
						break;
					}
					for (int j = point.Y - this._radius; j <= point.Y + this._radius; j++)
					{
						double num3 = (double)this._radius / (double)this._radius * (double)(j - point.Y);
						int num4 = Math.Min(this._radius, (int)Math.Sqrt((double)num - num3 * num3));
						for (int k = point.X - num4; k <= point.X + num4; k++)
						{
							if (!base.UnitApply(action, origin, k, j, new object[0]) && this._quitOnFail)
							{
								return false;
							}
						}
					}
				}
				return true;
			}

			// Token: 0x04007639 RID: 30265
			private int _radius;

			// Token: 0x0400763A RID: 30266
			private int _distanceBetweenSpheres;
		}
	}
}
