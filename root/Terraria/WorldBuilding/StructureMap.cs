using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.ID;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000BC RID: 188
	public class StructureMap
	{
		// Token: 0x060017A0 RID: 6048 RVA: 0x004DE948 File Offset: 0x004DCB48
		public bool CanPlace(Rectangle area, int padding = 0)
		{
			return this.CanPlace(area, TileID.Sets.GeneralPlacementTiles, padding);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x004DE958 File Offset: 0x004DCB58
		public bool CanPlace(Rectangle area, bool[] validTiles, int padding = 0)
		{
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				if (area.X < 0 || area.Y < 0 || area.X + area.Width > Main.maxTilesX - 1 || area.Y + area.Height > Main.maxTilesY - 1)
				{
					flag2 = false;
				}
				else
				{
					Rectangle rectangle = new Rectangle(area.X - padding, area.Y - padding, area.Width + padding * 2, area.Height + padding * 2);
					for (int i = 0; i < this._protectedStructures.Count; i++)
					{
						if (rectangle.Intersects(this._protectedStructures[i]))
						{
							return false;
						}
					}
					for (int j = rectangle.X; j < rectangle.X + rectangle.Width; j++)
					{
						for (int k = rectangle.Y; k < rectangle.Y + rectangle.Height; k++)
						{
							if (Main.tile[j, k].active())
							{
								ushort type = Main.tile[j, k].type;
								if (!validTiles[(int)type])
								{
									return false;
								}
							}
						}
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x004DEAC0 File Offset: 0x004DCCC0
		public Rectangle GetBoundingBox()
		{
			object @lock = this._lock;
			Rectangle rectangle;
			lock (@lock)
			{
				if (this._structures.Count == 0)
				{
					rectangle = Rectangle.Empty;
				}
				else
				{
					Point point = new Point(this._structures.Min((Rectangle rect) => rect.Left), this._structures.Min((Rectangle rect) => rect.Top));
					Point point2 = new Point(this._structures.Max((Rectangle rect) => rect.Right), this._structures.Max((Rectangle rect) => rect.Bottom));
					rectangle = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
				}
			}
			return rectangle;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x004DEC04 File Offset: 0x004DCE04
		public void AddStructure(Rectangle area, int padding = 0)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				area.Inflate(padding, padding);
				this._structures.Add(area);
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x004DEC54 File Offset: 0x004DCE54
		public void AddProtectedStructure(Rectangle area, int padding = 0)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				area.Inflate(padding, padding);
				this._structures.Add(area);
				this._protectedStructures.Add(area);
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x004DECB0 File Offset: 0x004DCEB0
		public void Reset()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._protectedStructures.Clear();
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x004DECF8 File Offset: 0x004DCEF8
		public StructureMap()
		{
		}

		// Token: 0x04001289 RID: 4745
		[JsonProperty]
		private readonly List<Rectangle> _structures = new List<Rectangle>(2048);

		// Token: 0x0400128A RID: 4746
		[JsonProperty]
		private readonly List<Rectangle> _protectedStructures = new List<Rectangle>(2048);

		// Token: 0x0400128B RID: 4747
		private readonly object _lock = new object();

		// Token: 0x020006E5 RID: 1765
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003F59 RID: 16217 RVA: 0x0069A393 File Offset: 0x00698593
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003F5A RID: 16218 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003F5B RID: 16219 RVA: 0x0069A39F File Offset: 0x0069859F
			internal int <GetBoundingBox>b__5_0(Rectangle rect)
			{
				return rect.Left;
			}

			// Token: 0x06003F5C RID: 16220 RVA: 0x0069A3A8 File Offset: 0x006985A8
			internal int <GetBoundingBox>b__5_1(Rectangle rect)
			{
				return rect.Top;
			}

			// Token: 0x06003F5D RID: 16221 RVA: 0x0069A3B1 File Offset: 0x006985B1
			internal int <GetBoundingBox>b__5_2(Rectangle rect)
			{
				return rect.Right;
			}

			// Token: 0x06003F5E RID: 16222 RVA: 0x0069A3BA File Offset: 0x006985BA
			internal int <GetBoundingBox>b__5_3(Rectangle rect)
			{
				return rect.Bottom;
			}

			// Token: 0x040067DB RID: 26587
			public static readonly StructureMap.<>c <>9 = new StructureMap.<>c();

			// Token: 0x040067DC RID: 26588
			public static Func<Rectangle, int> <>9__5_0;

			// Token: 0x040067DD RID: 26589
			public static Func<Rectangle, int> <>9__5_1;

			// Token: 0x040067DE RID: 26590
			public static Func<Rectangle, int> <>9__5_2;

			// Token: 0x040067DF RID: 26591
			public static Func<Rectangle, int> <>9__5_3;
		}
	}
}
