using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000B0 RID: 176
	public static class Searches
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x004DE0F7 File Offset: 0x004DC2F7
		public static GenSearch Chain(GenSearch search, params GenCondition[] conditions)
		{
			return search.Conditions(conditions);
		}

		// Token: 0x020006DA RID: 1754
		public class Left : GenSearch
		{
			// Token: 0x06003F3E RID: 16190 RVA: 0x00699BF5 File Offset: 0x00697DF5
			public Left(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x06003F3F RID: 16191 RVA: 0x00699C04 File Offset: 0x00697E04
			public override Point Find(Point origin)
			{
				for (int i = 0; i < this._maxDistance; i++)
				{
					if (base.Check(origin.X - i, origin.Y))
					{
						return new Point(origin.X - i, origin.Y);
					}
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040067C9 RID: 26569
			private int _maxDistance;
		}

		// Token: 0x020006DB RID: 1755
		public class Right : GenSearch
		{
			// Token: 0x06003F40 RID: 16192 RVA: 0x00699C51 File Offset: 0x00697E51
			public Right(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x06003F41 RID: 16193 RVA: 0x00699C60 File Offset: 0x00697E60
			public override Point Find(Point origin)
			{
				for (int i = 0; i < this._maxDistance; i++)
				{
					if (base.Check(origin.X + i, origin.Y))
					{
						return new Point(origin.X + i, origin.Y);
					}
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040067CA RID: 26570
			private int _maxDistance;
		}

		// Token: 0x020006DC RID: 1756
		public class Down : GenSearch
		{
			// Token: 0x06003F42 RID: 16194 RVA: 0x00699CAD File Offset: 0x00697EAD
			public Down(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x06003F43 RID: 16195 RVA: 0x00699CBC File Offset: 0x00697EBC
			public override Point Find(Point origin)
			{
				int num = 0;
				while (num < this._maxDistance && origin.Y + num < Main.maxTilesY)
				{
					if (base.Check(origin.X, origin.Y + num))
					{
						return new Point(origin.X, origin.Y + num);
					}
					num++;
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040067CB RID: 26571
			private int _maxDistance;
		}

		// Token: 0x020006DD RID: 1757
		public class Up : GenSearch
		{
			// Token: 0x06003F44 RID: 16196 RVA: 0x00699D18 File Offset: 0x00697F18
			public Up(int maxDistance)
			{
				this._maxDistance = maxDistance;
			}

			// Token: 0x06003F45 RID: 16197 RVA: 0x00699D28 File Offset: 0x00697F28
			public override Point Find(Point origin)
			{
				for (int i = 0; i < this._maxDistance; i++)
				{
					if (base.Check(origin.X, origin.Y - i))
					{
						return new Point(origin.X, origin.Y - i);
					}
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040067CC RID: 26572
			private int _maxDistance;
		}

		// Token: 0x020006DE RID: 1758
		public class Rectangle : GenSearch
		{
			// Token: 0x06003F46 RID: 16198 RVA: 0x00699D75 File Offset: 0x00697F75
			public Rectangle(int width, int height)
			{
				this._width = width;
				this._height = height;
			}

			// Token: 0x06003F47 RID: 16199 RVA: 0x00699D8C File Offset: 0x00697F8C
			public override Point Find(Point origin)
			{
				for (int i = 0; i < this._width; i++)
				{
					for (int j = 0; j < this._height; j++)
					{
						if (base.Check(origin.X + i, origin.Y + j))
						{
							return new Point(origin.X + i, origin.Y + j);
						}
					}
				}
				return GenSearch.NOT_FOUND;
			}

			// Token: 0x040067CD RID: 26573
			private int _width;

			// Token: 0x040067CE RID: 26574
			private int _height;
		}
	}
}
