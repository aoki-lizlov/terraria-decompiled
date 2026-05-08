using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020001D5 RID: 469
	public class Camera
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x0051D31B File Offset: 0x0051B51B
		public Vector2 UnscaledPosition
		{
			get
			{
				return Main.screenPosition;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x0051D322 File Offset: 0x0051B522
		public Vector2 UnscaledSize
		{
			get
			{
				return new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0051D335 File Offset: 0x0051B535
		public Vector2 ScaledPosition
		{
			get
			{
				return this.UnscaledPosition + this.GameViewMatrix.Translation;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x0051D34D File Offset: 0x0051B54D
		public Vector2 ScaledSize
		{
			get
			{
				return this.UnscaledSize - this.GameViewMatrix.Translation * 2f;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0051D370 File Offset: 0x0051B570
		public float BiggerScaledAxis
		{
			get
			{
				Vector2 scaledSize = this.ScaledSize;
				if (scaledSize.X <= scaledSize.Y)
				{
					return scaledSize.Y;
				}
				return scaledSize.X;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x0051D3A0 File Offset: 0x0051B5A0
		public float SmallerScaledAxis
		{
			get
			{
				Vector2 scaledSize = this.ScaledSize;
				if (scaledSize.X >= scaledSize.Y)
				{
					return scaledSize.Y;
				}
				return scaledSize.X;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x0051D3CF File Offset: 0x0051B5CF
		public RasterizerState Rasterizer
		{
			get
			{
				return Main.Rasterizer;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x0051D3D6 File Offset: 0x0051B5D6
		public SamplerState Sampler
		{
			get
			{
				return Main.DefaultSamplerState;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x0051D3DD File Offset: 0x0051B5DD
		public SpriteViewMatrix GameViewMatrix
		{
			get
			{
				return Main.GameViewMatrix;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x0051D3E4 File Offset: 0x0051B5E4
		public SpriteBatch SpriteBatch
		{
			get
			{
				return Main.spriteBatch;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x0051D3EB File Offset: 0x0051B5EB
		public Vector2 Center
		{
			get
			{
				return this.UnscaledPosition + this.UnscaledSize * 0.5f;
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0000357B File Offset: 0x0000177B
		public Camera()
		{
		}
	}
}
