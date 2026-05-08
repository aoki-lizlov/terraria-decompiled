using System;
using Microsoft.Xna.Framework;

namespace Terraria
{
	// Token: 0x02000024 RID: 36
	public abstract class Entity : IEntitySourceTarget
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000119F4 File Offset: 0x0000FBF4
		public bool AnyWet
		{
			get
			{
				return this.wet || this.lavaWet || this.honeyWet || this.shimmerWet;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00011A16 File Offset: 0x0000FC16
		public virtual Vector2 VisualPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00011A1E File Offset: 0x0000FC1E
		public float AngleTo(Vector2 Destination)
		{
			return (float)Math.Atan2((double)(Destination.Y - this.Center.Y), (double)(Destination.X - this.Center.X));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00011A4C File Offset: 0x0000FC4C
		public float AngleFrom(Vector2 Source)
		{
			return (float)Math.Atan2((double)(this.Center.Y - Source.Y), (double)(this.Center.X - Source.X));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00011A7A File Offset: 0x0000FC7A
		public float Distance(Vector2 Other)
		{
			return Vector2.Distance(this.Center, Other);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00011A88 File Offset: 0x0000FC88
		public float DistanceSQ(Vector2 Other)
		{
			return Vector2.DistanceSquared(this.Center, Other);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00011A96 File Offset: 0x0000FC96
		public Vector2 DirectionTo(Vector2 Destination)
		{
			return Vector2.Normalize(Destination - this.Center);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00011AA9 File Offset: 0x0000FCA9
		public Vector2 DirectionFrom(Vector2 Source)
		{
			return Vector2.Normalize(this.Center - Source);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00011ABC File Offset: 0x0000FCBC
		public bool WithinRange(Vector2 Target, float MaxRange)
		{
			return Vector2.DistanceSquared(this.Center, Target) <= MaxRange * MaxRange;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00011AD2 File Offset: 0x0000FCD2
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00011B0B File Offset: 0x0000FD0B
		public Vector2 Center
		{
			get
			{
				return new Vector2(this.position.X + (float)this.width / 2f, this.position.Y + (float)this.height / 2f);
			}
			set
			{
				this.position = new Vector2(value.X - (float)this.width / 2f, value.Y - (float)this.height / 2f);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00011B40 File Offset: 0x0000FD40
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00011B6B File Offset: 0x0000FD6B
		public Vector2 Left
		{
			get
			{
				return new Vector2(this.position.X, this.position.Y + (float)this.height / 2f);
			}
			set
			{
				this.position = new Vector2(value.X, value.Y - (float)this.height / 2f);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00011B92 File Offset: 0x0000FD92
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00011BC5 File Offset: 0x0000FDC5
		public Vector2 Right
		{
			get
			{
				return new Vector2(this.position.X + (float)this.width, this.position.Y + (float)this.height / 2f);
			}
			set
			{
				this.position = new Vector2(value.X - (float)this.width, value.Y - (float)this.height / 2f);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00011C1F File Offset: 0x0000FE1F
		public Vector2 Top
		{
			get
			{
				return new Vector2(this.position.X + (float)this.width / 2f, this.position.Y);
			}
			set
			{
				this.position = new Vector2(value.X - (float)this.width / 2f, value.Y);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00011A16 File Offset: 0x0000FC16
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00011C46 File Offset: 0x0000FE46
		public Vector2 TopLeft
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00011C4F File Offset: 0x0000FE4F
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00011C74 File Offset: 0x0000FE74
		public Vector2 TopRight
		{
			get
			{
				return new Vector2(this.position.X + (float)this.width, this.position.Y);
			}
			set
			{
				this.position = new Vector2(value.X - (float)this.width, value.Y);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00011C95 File Offset: 0x0000FE95
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		public Vector2 Bottom
		{
			get
			{
				return new Vector2(this.position.X + (float)this.width / 2f, this.position.Y + (float)this.height);
			}
			set
			{
				this.position = new Vector2(value.X - (float)this.width / 2f, value.Y - (float)this.height);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00011CF7 File Offset: 0x0000FEF7
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00011D1C File Offset: 0x0000FF1C
		public Vector2 BottomLeft
		{
			get
			{
				return new Vector2(this.position.X, this.position.Y + (float)this.height);
			}
			set
			{
				this.position = new Vector2(value.X, value.Y - (float)this.height);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00011D3D File Offset: 0x0000FF3D
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00011D6A File Offset: 0x0000FF6A
		public Vector2 BottomRight
		{
			get
			{
				return new Vector2(this.position.X + (float)this.width, this.position.Y + (float)this.height);
			}
			set
			{
				this.position = new Vector2(value.X - (float)this.width, value.Y - (float)this.height);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00011D93 File Offset: 0x0000FF93
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00011DA8 File Offset: 0x0000FFA8
		public Vector2 Size
		{
			get
			{
				return new Vector2((float)this.width, (float)this.height);
			}
			set
			{
				this.width = (int)value.X;
				this.height = (int)value.Y;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00011DEF File Offset: 0x0000FFEF
		public Rectangle Hitbox
		{
			get
			{
				return new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
			}
			set
			{
				this.position = new Vector2((float)value.X, (float)value.Y);
				this.width = value.Width;
				this.height = value.Height;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00011E22 File Offset: 0x00010022
		protected Entity()
		{
		}

		// Token: 0x04000120 RID: 288
		public int whoAmI;

		// Token: 0x04000121 RID: 289
		public Vector2 position;

		// Token: 0x04000122 RID: 290
		public Vector2 velocity;

		// Token: 0x04000123 RID: 291
		public Vector2 oldPosition;

		// Token: 0x04000124 RID: 292
		public Vector2 oldVelocity;

		// Token: 0x04000125 RID: 293
		public int oldDirection;

		// Token: 0x04000126 RID: 294
		public int direction = 1;

		// Token: 0x04000127 RID: 295
		public int width;

		// Token: 0x04000128 RID: 296
		public int height;

		// Token: 0x04000129 RID: 297
		public bool wet;

		// Token: 0x0400012A RID: 298
		public bool shimmerWet;

		// Token: 0x0400012B RID: 299
		public bool honeyWet;

		// Token: 0x0400012C RID: 300
		public byte wetCount;

		// Token: 0x0400012D RID: 301
		public bool lavaWet;
	}
}
