using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000097 RID: 151
	internal class SpriteEffect : Effect
	{
		// Token: 0x060012A5 RID: 4773 RVA: 0x0002B5D0 File Offset: 0x000297D0
		public SpriteEffect(GraphicsDevice device)
			: base(device, Resources.SpriteEffect)
		{
			this.CacheEffectParameters();
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0002B5E4 File Offset: 0x000297E4
		protected SpriteEffect(SpriteEffect cloneSource)
			: base(cloneSource)
		{
			this.CacheEffectParameters();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0002B5F3 File Offset: 0x000297F3
		public override Effect Clone()
		{
			return new SpriteEffect(this);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0002B5FB File Offset: 0x000297FB
		private void CacheEffectParameters()
		{
			this.matrixParam = base.Parameters["MatrixTransform"];
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0002B614 File Offset: 0x00029814
		protected internal override void OnApply()
		{
			Viewport viewport = base.GraphicsDevice.Viewport;
			Matrix matrix = Matrix.CreateOrthographicOffCenter(0f, (float)viewport.Width, (float)viewport.Height, 0f, 0f, 1f);
			Matrix matrix2 = Matrix.CreateTranslation(-0.5f, -0.5f, 0f);
			this.matrixParam.SetValue(matrix2 * matrix);
		}

		// Token: 0x040008AE RID: 2222
		private EffectParameter matrixParam;
	}
}
