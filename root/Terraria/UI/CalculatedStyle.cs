using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x020000E7 RID: 231
	public struct CalculatedStyle
	{
		// Token: 0x060018E6 RID: 6374 RVA: 0x004E5F6D File Offset: 0x004E416D
		public CalculatedStyle(float x, float y, float width, float height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x004E5F8C File Offset: 0x004E418C
		public Rectangle ToRectangle()
		{
			return new Rectangle((int)this.X, (int)this.Y, (int)this.Width, (int)this.Height);
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x004E5FAF File Offset: 0x004E41AF
		public Vector2 Position()
		{
			return new Vector2(this.X, this.Y);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x004E5FC2 File Offset: 0x004E41C2
		public Vector2 Center()
		{
			return new Vector2(this.X + this.Width * 0.5f, this.Y + this.Height * 0.5f);
		}

		// Token: 0x04001313 RID: 4883
		public float X;

		// Token: 0x04001314 RID: 4884
		public float Y;

		// Token: 0x04001315 RID: 4885
		public float Width;

		// Token: 0x04001316 RID: 4886
		public float Height;
	}
}
