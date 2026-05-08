using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200020C RID: 524
	public abstract class ABasicParticle : IPooledParticle, IParticle
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x0052F6A4 File Offset: 0x0052D8A4
		// (set) Token: 0x06002173 RID: 8563 RVA: 0x0052F6AC File Offset: 0x0052D8AC
		public bool ShouldBeRemovedFromRenderer
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldBeRemovedFromRenderer>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ShouldBeRemovedFromRenderer>k__BackingField = value;
			}
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0052F6B8 File Offset: 0x0052D8B8
		public ABasicParticle()
		{
			this._texture = null;
			this._frame = Rectangle.Empty;
			this._origin = Vector2.Zero;
			this.Velocity = Vector2.Zero;
			this.LocalPosition = Vector2.Zero;
			this.ShouldBeRemovedFromRenderer = false;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0052F708 File Offset: 0x0052D908
		public virtual void SetBasicInfo(Asset<Texture2D> textureAsset, Rectangle? frame, Vector2 initialVelocity, Vector2 initialLocalPosition)
		{
			this._texture = textureAsset;
			this._frame = ((frame != null) ? frame.Value : this._texture.Frame(1, 1, 0, 0, 0, 0));
			this._origin = this._frame.Size() / 2f;
			this.Velocity = initialVelocity;
			this.LocalPosition = initialLocalPosition;
			this.ShouldBeRemovedFromRenderer = false;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0052F778 File Offset: 0x0052D978
		public virtual void Update(ref ParticleRendererSettings settings)
		{
			this.Velocity += this.AccelerationPerFrame;
			this.LocalPosition += this.Velocity;
			this.RotationVelocity += this.RotationAcceleration;
			this.Rotation += this.RotationVelocity;
			this.ScaleVelocity += this.ScaleAcceleration;
			this.Scale += this.ScaleVelocity;
		}

		// Token: 0x06002177 RID: 8567
		public abstract void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch);

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x0052F807 File Offset: 0x0052DA07
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x0052F80F File Offset: 0x0052DA0F
		public bool IsRestingInPool
		{
			[CompilerGenerated]
			get
			{
				return this.<IsRestingInPool>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsRestingInPool>k__BackingField = value;
			}
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0052F818 File Offset: 0x0052DA18
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0052F824 File Offset: 0x0052DA24
		public virtual void FetchFromPool()
		{
			this.IsRestingInPool = false;
			this.ShouldBeRemovedFromRenderer = false;
			this.AccelerationPerFrame = Vector2.Zero;
			this.Velocity = Vector2.Zero;
			this.LocalPosition = Vector2.Zero;
			this._texture = null;
			this._frame = Rectangle.Empty;
			this._origin = Vector2.Zero;
			this.Rotation = 0f;
			this.RotationVelocity = 0f;
			this.RotationAcceleration = 0f;
			this.Scale = Vector2.Zero;
			this.ScaleVelocity = Vector2.Zero;
			this.ScaleAcceleration = Vector2.Zero;
		}

		// Token: 0x04004BDB RID: 19419
		[CompilerGenerated]
		private bool <ShouldBeRemovedFromRenderer>k__BackingField;

		// Token: 0x04004BDC RID: 19420
		public Vector2 AccelerationPerFrame;

		// Token: 0x04004BDD RID: 19421
		public Vector2 Velocity;

		// Token: 0x04004BDE RID: 19422
		public Vector2 LocalPosition;

		// Token: 0x04004BDF RID: 19423
		protected Asset<Texture2D> _texture;

		// Token: 0x04004BE0 RID: 19424
		protected Rectangle _frame;

		// Token: 0x04004BE1 RID: 19425
		protected Vector2 _origin;

		// Token: 0x04004BE2 RID: 19426
		public float Rotation;

		// Token: 0x04004BE3 RID: 19427
		public float RotationVelocity;

		// Token: 0x04004BE4 RID: 19428
		public float RotationAcceleration;

		// Token: 0x04004BE5 RID: 19429
		public Vector2 Scale;

		// Token: 0x04004BE6 RID: 19430
		public Vector2 ScaleVelocity;

		// Token: 0x04004BE7 RID: 19431
		public Vector2 ScaleAcceleration;

		// Token: 0x04004BE8 RID: 19432
		[CompilerGenerated]
		private bool <IsRestingInPool>k__BackingField;
	}
}
