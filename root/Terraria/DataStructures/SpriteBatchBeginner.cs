using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x0200055D RID: 1373
	public struct SpriteBatchBeginner
	{
		// Token: 0x060037C6 RID: 14278 RVA: 0x0062FF41 File Offset: 0x0062E141
		public SpriteBatchBeginner(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
		{
			this.sortMode = sortMode;
			this.blendState = blendState;
			this.samplerState = samplerState;
			this.depthStencilState = depthStencilState;
			this.rasterizerState = rasterizerState;
			this.effect = effect;
			this.transformMatrix = transformMatrix;
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x0062FF78 File Offset: 0x0062E178
		public void Begin(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin(this.sortMode, this.blendState, this.samplerState, this.depthStencilState, this.rasterizerState, this.effect, this.transformMatrix);
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x0062FFAA File Offset: 0x0062E1AA
		public void Begin(SpriteBatch spriteBatch, SpriteSortMode sortMode)
		{
			spriteBatch.Begin(sortMode, this.blendState, this.samplerState, this.depthStencilState, this.rasterizerState, this.effect, this.transformMatrix);
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x0062FFD7 File Offset: 0x0062E1D7
		public void Begin(TileBatch tileBatch)
		{
			tileBatch.Begin(this.rasterizerState, this.transformMatrix);
		}

		// Token: 0x04005BDB RID: 23515
		private SpriteSortMode sortMode;

		// Token: 0x04005BDC RID: 23516
		private BlendState blendState;

		// Token: 0x04005BDD RID: 23517
		private SamplerState samplerState;

		// Token: 0x04005BDE RID: 23518
		private DepthStencilState depthStencilState;

		// Token: 0x04005BDF RID: 23519
		private RasterizerState rasterizerState;

		// Token: 0x04005BE0 RID: 23520
		private Effect effect;

		// Token: 0x04005BE1 RID: 23521
		public Matrix transformMatrix;
	}
}
