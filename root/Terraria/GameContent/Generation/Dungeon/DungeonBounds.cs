using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x02000492 RID: 1170
	public class DungeonBounds
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x005F9A4B File Offset: 0x005F7C4B
		public Rectangle Hitbox
		{
			get
			{
				if (this._hitbox != null)
				{
					return this._hitbox.Value;
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x005F9A6B File Offset: 0x005F7C6B
		public int X
		{
			get
			{
				return this._boundsLeft;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x005F9A73 File Offset: 0x005F7C73
		public int Y
		{
			get
			{
				return this._boundsTop;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x005F9A7B File Offset: 0x005F7C7B
		public int Width
		{
			get
			{
				return this._boundsRight - this._boundsLeft;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x005F9A8A File Offset: 0x005F7C8A
		public int Height
		{
			get
			{
				return this._boundsBottom - this._boundsTop;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x005F9A99 File Offset: 0x005F7C99
		public int Size
		{
			get
			{
				if (this.Width <= this.Height)
				{
					return this.Height;
				}
				return this.Width;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x005F9A6B File Offset: 0x005F7C6B
		// (set) Token: 0x0600339D RID: 13213 RVA: 0x005F9AB6 File Offset: 0x005F7CB6
		public int Left
		{
			get
			{
				return this._boundsLeft;
			}
			set
			{
				this._boundsLeft = (int)MathHelper.Clamp((float)value, 10f, (float)(Main.maxTilesX - 10));
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x005F9AD4 File Offset: 0x005F7CD4
		// (set) Token: 0x0600339F RID: 13215 RVA: 0x005F9ADC File Offset: 0x005F7CDC
		public int Right
		{
			get
			{
				return this._boundsRight;
			}
			set
			{
				this._boundsRight = (int)MathHelper.Clamp((float)value, 10f, (float)(Main.maxTilesX - 10));
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x005F9A73 File Offset: 0x005F7C73
		// (set) Token: 0x060033A1 RID: 13217 RVA: 0x005F9AFA File Offset: 0x005F7CFA
		public int Top
		{
			get
			{
				return this._boundsTop;
			}
			set
			{
				this._boundsTop = (int)MathHelper.Clamp((float)value, 10f, (float)(Main.maxTilesY - 10));
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x005F9B18 File Offset: 0x005F7D18
		// (set) Token: 0x060033A3 RID: 13219 RVA: 0x005F9B20 File Offset: 0x005F7D20
		public int Bottom
		{
			get
			{
				return this._boundsBottom;
			}
			set
			{
				this._boundsBottom = (int)MathHelper.Clamp((float)value, 10f, (float)(Main.maxTilesY - 10));
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x005F9B3E File Offset: 0x005F7D3E
		public Point Center
		{
			get
			{
				return new Point((this.Left + this.Right) / 2, (this.Top + this.Bottom) / 2);
			}
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x005F9B63 File Offset: 0x005F7D63
		public Point RandomPointInBounds(UnifiedRandom genRand)
		{
			return new Point(genRand.Next(this.Left, this.Right + 1), genRand.Next(this.Top, this.Bottom + 1));
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x005F9B92 File Offset: 0x005F7D92
		public void Inflate(int amount)
		{
			this.SetBounds(this.Left - amount, this.Top - amount, this.Right + amount, this.Bottom + amount);
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x005F9BBA File Offset: 0x005F7DBA
		public void Shrink(int amount)
		{
			this.SetBounds(this.Left + amount, this.Top + amount, this.Right - amount, this.Bottom - amount);
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x005F9BE2 File Offset: 0x005F7DE2
		public bool ContainsWithFluff(Vector2 point, int fluff)
		{
			if (fluff == 0)
			{
				return this.Contains((int)point.X, (int)point.Y);
			}
			return this.ContainsWithFluff((int)point.X, (int)point.Y, fluff);
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x005F9C11 File Offset: 0x005F7E11
		public bool ContainsWithFluff(Vector2D point, int fluff)
		{
			if (fluff == 0)
			{
				return this.Contains((int)point.X, (int)point.Y);
			}
			return this.ContainsWithFluff((int)point.X, (int)point.Y, fluff);
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x005F9C40 File Offset: 0x005F7E40
		public bool ContainsWithFluff(Point point, int fluff)
		{
			if (fluff == 0)
			{
				return this.Contains(point.X, point.Y);
			}
			return this.ContainsWithFluff(point.X, point.Y, fluff);
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x005F9C6C File Offset: 0x005F7E6C
		public bool ContainsWithFluff(int x, int y, int fluff)
		{
			if (fluff == 0)
			{
				return this.Contains(x, y);
			}
			if (this._hitbox == null)
			{
				return false;
			}
			Rectangle rectangle = new Rectangle(this._hitbox.Value.Left - fluff, this._hitbox.Value.Top - fluff, this._hitbox.Value.Width + fluff * 2, this._hitbox.Value.Height + fluff * 2);
			return rectangle.Contains(x, y);
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x005F9CF6 File Offset: 0x005F7EF6
		public bool Contains(Vector2D point)
		{
			return this.Contains((int)point.X, (int)point.Y);
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x005F9D0C File Offset: 0x005F7F0C
		public bool Contains(Point point)
		{
			return this.Contains(point.X, point.Y);
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x005F9D20 File Offset: 0x005F7F20
		public bool Contains(int x, int y)
		{
			return this._hitbox != null && this._hitbox.Value.Contains(x, y);
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x005F9D51 File Offset: 0x005F7F51
		public bool Intersects(DungeonBounds bounds)
		{
			return bounds.HasHitbox() && this.Intersects(bounds.Hitbox);
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x005F9D6C File Offset: 0x005F7F6C
		public bool Intersects(Rectangle hitbox)
		{
			return this._hitbox != null && this._hitbox.Value.Intersects(hitbox);
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x005F9D9C File Offset: 0x005F7F9C
		public bool IntersectsWithLineThreePointCheck(Point startPoint, Point endPoint)
		{
			return this.IntersectsWithLineThreePointCheck(startPoint.ToVector2D(), endPoint.ToVector2D());
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x005F9DB0 File Offset: 0x005F7FB0
		public bool IntersectsWithLineThreePointCheck(int startPointX, int startPointY, int endPointX, int endPointY)
		{
			return this.IntersectsWithLineThreePointCheck(new Vector2D((double)startPointX, (double)startPointY), new Vector2D((double)endPointX, (double)endPointY));
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x005F9DCC File Offset: 0x005F7FCC
		public bool IntersectsWithLineThreePointCheck(Vector2D startPoint, Vector2D endPoint)
		{
			return this._hitbox != null && (this.Contains(startPoint) || this.Contains(endPoint) || this.Contains((startPoint + endPoint) / 2.0));
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x005F9E1A File Offset: 0x005F801A
		public bool HasHitbox()
		{
			return this._hitbox != null;
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x005F9E27 File Offset: 0x005F8027
		public void SetBoundsLeft(int minX)
		{
			this.Left = minX;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x005F9E30 File Offset: 0x005F8030
		public void SetBoundsRight(int maxX)
		{
			this.Right = maxX;
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x005F9E39 File Offset: 0x005F8039
		public void SetBoundsTop(int minY)
		{
			this.Top = minY;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x005F9E42 File Offset: 0x005F8042
		public void SetBoundsBottom(int maxY)
		{
			this.Bottom = maxY;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x005F9E4B File Offset: 0x005F804B
		public void SetBounds(Rectangle rect)
		{
			this.SetBounds(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x005F9E6F File Offset: 0x005F806F
		public void SetBounds(int minX, int minY, int maxX, int maxY)
		{
			this.Left = minX;
			this.Right = maxX;
			this.Top = minY;
			this.Bottom = maxY;
			this.CalculateHitbox();
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x005F9E98 File Offset: 0x005F8098
		public void UpdateBounds(int x, int y)
		{
			if (x < this._boundsLeft)
			{
				this.Left = x;
			}
			if (x > this._boundsRight)
			{
				this.Right = x;
			}
			if (y < this._boundsTop)
			{
				this.Top = y;
			}
			if (y > this._boundsBottom)
			{
				this.Bottom = y;
			}
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x005F9EE8 File Offset: 0x005F80E8
		public void UpdateBounds(DungeonBounds bounds)
		{
			if (this.Width == 0 || this.Height == 0)
			{
				this.SetBounds(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
				return;
			}
			this.UpdateBounds(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x005F9F44 File Offset: 0x005F8144
		public void UpdateBounds(int minX, int minY, int maxX, int maxY)
		{
			if (minX < this._boundsLeft)
			{
				this.Left = minX;
			}
			if (maxX > this._boundsRight)
			{
				this.Right = maxX;
			}
			if (minY < this._boundsTop)
			{
				this.Top = minY;
			}
			if (maxY > this._boundsBottom)
			{
				this.Bottom = maxY;
			}
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x005F9F94 File Offset: 0x005F8194
		public Rectangle CalculateHitbox()
		{
			if (this.Right <= this.Left)
			{
				this.Right = this.Left + 1;
			}
			if (this.Bottom <= this.Top)
			{
				this.Bottom = this.Top + 1;
			}
			this._hitbox = new Rectangle?(new Rectangle(this.X, this.Y, this.Width, this.Height));
			return this._hitbox.Value;
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x005FA00C File Offset: 0x005F820C
		public void Reset()
		{
			this._hitbox = null;
			this.Left = 0;
			this.Right = 0;
			this.Top = 0;
			this.Bottom = 0;
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x0000357B File Offset: 0x0000177B
		public DungeonBounds()
		{
		}

		// Token: 0x040058F6 RID: 22774
		[JsonProperty]
		private Rectangle? _hitbox;

		// Token: 0x040058F7 RID: 22775
		private int _boundsLeft;

		// Token: 0x040058F8 RID: 22776
		private int _boundsRight;

		// Token: 0x040058F9 RID: 22777
		private int _boundsTop;

		// Token: 0x040058FA RID: 22778
		private int _boundsBottom;
	}
}
