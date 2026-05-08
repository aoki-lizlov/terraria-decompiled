using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000E0 RID: 224
	[Serializable]
	public struct Viewport
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00034D1F File Offset: 0x00032F1F
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x00034D2C File Offset: 0x00032F2C
		public int Height
		{
			get
			{
				return this.viewport.h;
			}
			set
			{
				this.viewport.h = value;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00034D3A File Offset: 0x00032F3A
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x00034D47 File Offset: 0x00032F47
		public float MaxDepth
		{
			get
			{
				return this.viewport.maxDepth;
			}
			set
			{
				this.viewport.maxDepth = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00034D55 File Offset: 0x00032F55
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00034D62 File Offset: 0x00032F62
		public float MinDepth
		{
			get
			{
				return this.viewport.minDepth;
			}
			set
			{
				this.viewport.minDepth = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00034D70 File Offset: 0x00032F70
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x00034D7D File Offset: 0x00032F7D
		public int Width
		{
			get
			{
				return this.viewport.w;
			}
			set
			{
				this.viewport.w = value;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00034D8B File Offset: 0x00032F8B
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x00034D98 File Offset: 0x00032F98
		public int Y
		{
			get
			{
				return this.viewport.y;
			}
			set
			{
				this.viewport.y = value;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x00034DA6 File Offset: 0x00032FA6
		// (set) Token: 0x060015AF RID: 5551 RVA: 0x00034DB3 File Offset: 0x00032FB3
		public int X
		{
			get
			{
				return this.viewport.x;
			}
			set
			{
				this.viewport.x = value;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00034DC1 File Offset: 0x00032FC1
		public float AspectRatio
		{
			get
			{
				if (this.viewport.h != 0 && this.viewport.w != 0)
				{
					return (float)this.viewport.w / (float)this.viewport.h;
				}
				return 0f;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00034DFC File Offset: 0x00032FFC
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x00034E30 File Offset: 0x00033030
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(this.viewport.x, this.viewport.y, this.viewport.w, this.viewport.h);
			}
			set
			{
				this.viewport.x = value.X;
				this.viewport.y = value.Y;
				this.viewport.w = value.Width;
				this.viewport.h = value.Height;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00034E81 File Offset: 0x00033081
		public Rectangle TitleSafeArea
		{
			get
			{
				return this.Bounds;
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00034E8C File Offset: 0x0003308C
		public Viewport(int x, int y, int width, int height)
		{
			this.viewport.x = x;
			this.viewport.y = y;
			this.viewport.w = width;
			this.viewport.h = height;
			this.viewport.minDepth = 0f;
			this.viewport.maxDepth = 1f;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00034EEC File Offset: 0x000330EC
		public Viewport(Rectangle bounds)
		{
			this.viewport.x = bounds.X;
			this.viewport.y = bounds.Y;
			this.viewport.w = bounds.Width;
			this.viewport.h = bounds.Height;
			this.viewport.minDepth = 0f;
			this.viewport.maxDepth = 1f;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00034F60 File Offset: 0x00033160
		public Vector3 Project(Vector3 source, Matrix projection, Matrix view, Matrix world)
		{
			Matrix matrix = Matrix.Multiply(Matrix.Multiply(world, view), projection);
			Vector3 vector = Vector3.Transform(source, matrix);
			float num = source.X * matrix.M14 + source.Y * matrix.M24 + source.Z * matrix.M34 + matrix.M44;
			if (!MathHelper.WithinEpsilon(num, 1f))
			{
				vector.X /= num;
				vector.Y /= num;
				vector.Z /= num;
			}
			vector.X = (vector.X + 1f) * 0.5f * (float)this.Width + (float)this.X;
			vector.Y = (-vector.Y + 1f) * 0.5f * (float)this.Height + (float)this.Y;
			vector.Z = vector.Z * (this.MaxDepth - this.MinDepth) + this.MinDepth;
			return vector;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00035068 File Offset: 0x00033268
		public Vector3 Unproject(Vector3 source, Matrix projection, Matrix view, Matrix world)
		{
			Matrix matrix = Matrix.Invert(Matrix.Multiply(Matrix.Multiply(world, view), projection));
			source.X = (source.X - (float)this.X) / (float)this.Width * 2f - 1f;
			source.Y = -((source.Y - (float)this.Y) / (float)this.Height * 2f - 1f);
			source.Z = (source.Z - this.MinDepth) / (this.MaxDepth - this.MinDepth);
			Vector3 vector = Vector3.Transform(source, matrix);
			float num = source.X * matrix.M14 + source.Y * matrix.M24 + source.Z * matrix.M34 + matrix.M44;
			if (!MathHelper.WithinEpsilon(num, 1f))
			{
				vector.X /= num;
				vector.Y /= num;
				vector.Z /= num;
			}
			return vector;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00035174 File Offset: 0x00033374
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X:",
				this.viewport.x.ToString(),
				" Y:",
				this.viewport.y.ToString(),
				" Width:",
				this.viewport.w.ToString(),
				" Height:",
				this.viewport.h.ToString(),
				" MinDepth:",
				this.viewport.minDepth.ToString(),
				" MaxDepth:",
				this.viewport.maxDepth.ToString(),
				"}"
			});
		}

		// Token: 0x04000A87 RID: 2695
		internal FNA3D.FNA3D_Viewport viewport;
	}
}
