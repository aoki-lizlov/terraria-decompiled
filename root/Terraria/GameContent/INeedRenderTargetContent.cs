using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x02000250 RID: 592
	public interface INeedRenderTargetContent
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600232C RID: 9004
		bool IsReady { get; }

		// Token: 0x0600232D RID: 9005
		void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch);

		// Token: 0x0600232E RID: 9006
		void Reset();
	}
}
