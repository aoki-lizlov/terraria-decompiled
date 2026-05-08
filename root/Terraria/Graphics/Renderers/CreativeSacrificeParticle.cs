using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200020B RID: 523
	public class CreativeSacrificeParticle : IParticle
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x0052F51B File Offset: 0x0052D71B
		// (set) Token: 0x0600216E RID: 8558 RVA: 0x0052F523 File Offset: 0x0052D723
		public bool ShouldBeRemovedFromRenderer
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldBeRemovedFromRenderer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ShouldBeRemovedFromRenderer>k__BackingField = value;
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0052F52C File Offset: 0x0052D72C
		public CreativeSacrificeParticle(Asset<Texture2D> textureAsset, Rectangle? frame, Vector2 initialVelocity, Vector2 initialLocalPosition)
		{
			this._texture = textureAsset;
			this._frame = ((frame != null) ? frame.Value : this._texture.Frame(1, 1, 0, 0, 0, 0));
			this._origin = this._frame.Size() / 2f;
			this.Velocity = initialVelocity;
			this.LocalPosition = initialLocalPosition;
			this.StopWhenBelowXScale = 0f;
			this.ShouldBeRemovedFromRenderer = false;
			this._scale = 0.6f;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0052F5B8 File Offset: 0x0052D7B8
		public void Update(ref ParticleRendererSettings settings)
		{
			this.Velocity += this.AccelerationPerFrame;
			this.LocalPosition += this.Velocity;
			this._scale += this.ScaleOffsetPerFrame;
			if (this._scale <= this.StopWhenBelowXScale)
			{
				this.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0052F61C File Offset: 0x0052D81C
		public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = Color.Lerp(Color.White, new Color(255, 255, 255, 0), Utils.GetLerpValue(0.1f, 0.5f, this._scale, false));
			spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, 0f, this._origin, this._scale, SpriteEffects.None, 0f);
		}

		// Token: 0x04004BD1 RID: 19409
		[CompilerGenerated]
		private bool <ShouldBeRemovedFromRenderer>k__BackingField;

		// Token: 0x04004BD2 RID: 19410
		public Vector2 AccelerationPerFrame;

		// Token: 0x04004BD3 RID: 19411
		public Vector2 Velocity;

		// Token: 0x04004BD4 RID: 19412
		public Vector2 LocalPosition;

		// Token: 0x04004BD5 RID: 19413
		public float ScaleOffsetPerFrame;

		// Token: 0x04004BD6 RID: 19414
		public float StopWhenBelowXScale;

		// Token: 0x04004BD7 RID: 19415
		private Asset<Texture2D> _texture;

		// Token: 0x04004BD8 RID: 19416
		private Rectangle _frame;

		// Token: 0x04004BD9 RID: 19417
		private Vector2 _origin;

		// Token: 0x04004BDA RID: 19418
		private float _scale;
	}
}
