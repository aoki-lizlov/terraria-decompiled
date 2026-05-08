using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x020001E9 RID: 489
	public abstract class CustomSky : GameEffect
	{
		// Token: 0x06002084 RID: 8324
		public abstract void Update(GameTime gameTime);

		// Token: 0x06002085 RID: 8325
		public abstract void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth);

		// Token: 0x06002086 RID: 8326
		public abstract bool IsActive();

		// Token: 0x06002087 RID: 8327
		public abstract void Reset();

		// Token: 0x06002088 RID: 8328 RVA: 0x001FC6F1 File Offset: 0x001FA8F1
		public virtual Color OnTileColor(Color inColor)
		{
			return inColor;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00043F2B File Offset: 0x0004212B
		public virtual float GetCloudAlpha()
		{
			return 1f;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000379E9 File Offset: 0x00035BE9
		public override bool IsVisible()
		{
			return true;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00522D4A File Offset: 0x00520F4A
		protected CustomSky()
		{
		}
	}
}
