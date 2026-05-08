using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x020001F3 RID: 499
	public abstract class Overlay : GameEffect
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x00523858 File Offset: 0x00521A58
		public RenderLayers Layer
		{
			get
			{
				return this._layer;
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00523860 File Offset: 0x00521A60
		public Overlay(EffectPriority priority, RenderLayers layer)
		{
			this._priority = priority;
			this._layer = layer;
		}

		// Token: 0x060020C3 RID: 8387
		public abstract void Draw(SpriteBatch spriteBatch);

		// Token: 0x060020C4 RID: 8388
		public abstract void Update(GameTime gameTime);

		// Token: 0x04004B2E RID: 19246
		public OverlayMode Mode = OverlayMode.Inactive;

		// Token: 0x04004B2F RID: 19247
		private RenderLayers _layer = RenderLayers.All;
	}
}
