using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000207 RID: 519
	public class LittleFlyingCritterParticle : IPooledParticle, IParticle, IParticleRepel
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x0052D3F7 File Offset: 0x0052B5F7
		// (set) Token: 0x0600213D RID: 8509 RVA: 0x0052D3FF File Offset: 0x0052B5FF
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

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0052D408 File Offset: 0x0052B608
		// (set) Token: 0x0600213F RID: 8511 RVA: 0x0052D410 File Offset: 0x0052B610
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

		// Token: 0x06002140 RID: 8512 RVA: 0x0000357B File Offset: 0x0000177B
		public LittleFlyingCritterParticle()
		{
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0052D41C File Offset: 0x0052B61C
		public void Prepare(LittleFlyingCritterParticle.FlyType type, Vector2 position, int duration, Color overrideColor = default(Color), int repelLifetimeDecay = 0)
		{
			this._type = type;
			this._variantRow = Main.rand.Next(8);
			this._variantColumn = ((Main.rand.Next(5) == 0) ? 1 : 0);
			this._spawnPosition = position;
			this._localPosition = position + Main.rand.NextVector2Circular(4f, 8f);
			this._neverGoBelowThis = position.Y + 8f;
			this.RandomizeVelocity();
			this._lifeTimeCounted = 0;
			this._lifeTimeTotal = 300 + Main.rand.Next(6) * 60;
			this._overrideColor = overrideColor;
			this._repelLifetimeDecay = repelLifetimeDecay;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0052D4C8 File Offset: 0x0052B6C8
		private void RandomizeVelocity()
		{
			this._velocity = Main.rand.NextVector2Circular(1f, 1f);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0052D4E4 File Offset: 0x0052B6E4
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0052D4ED File Offset: 0x0052B6ED
		public virtual void FetchFromPool()
		{
			this.IsRestingInPool = false;
			this.ShouldBeRemovedFromRenderer = false;
			this._addedVelocity = Vector2.Zero;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0052D508 File Offset: 0x0052B708
		public void Update(ref ParticleRendererSettings settings)
		{
			int num = this._lifeTimeCounted + 1;
			this._lifeTimeCounted = num;
			if (num >= this._lifeTimeTotal)
			{
				this.ShouldBeRemovedFromRenderer = true;
			}
			float num2 = 0.02f;
			int num3 = 30;
			if (this._type == LittleFlyingCritterParticle.FlyType.ButterFly)
			{
				num2 = 0.01f;
				num3 = 600;
			}
			this._velocity += new Vector2((float)Math.Sign(this._spawnPosition.X - this._localPosition.X) * num2, (float)Math.Sign(this._spawnPosition.Y - this._localPosition.Y) * num2);
			if (this._lifeTimeCounted % num3 == 0 && Main.rand.Next(2) == 0)
			{
				this.RandomizeVelocity();
				if (Main.rand.Next(2) == 0)
				{
					this._velocity /= 2f;
				}
			}
			this._addedVelocity *= 0.98f;
			if (this._addedVelocity.Length() < 0.01f)
			{
				this._addedVelocity = new Vector2(0f, 0f);
			}
			this._localPosition += this._velocity + this._addedVelocity;
			if (this._localPosition.Y > this._neverGoBelowThis)
			{
				this._localPosition.Y = this._neverGoBelowThis;
				if (this._velocity.Y > 0f)
				{
					this._velocity.Y = this._velocity.Y * -1f;
				}
				if (this._addedVelocity.Y > 0f)
				{
					this._addedVelocity.Y = this._addedVelocity.Y * -1f;
				}
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0052D6B8 File Offset: 0x0052B8B8
		public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Vector2 vector = settings.AnchorPosition + this._localPosition;
			if (vector.X < -10f || vector.X > (float)(Main.screenWidth + 10) || vector.Y < -10f || vector.Y > (float)(Main.screenHeight + 10))
			{
				this.ShouldBeRemovedFromRenderer = true;
				return;
			}
			LittleFlyingCritterParticle.FlyType type = this._type;
			if (type == LittleFlyingCritterParticle.FlyType.RegularFly)
			{
				this.Draw_Fly(ref settings, spritebatch);
				return;
			}
			if (type != LittleFlyingCritterParticle.FlyType.ButterFly)
			{
				return;
			}
			this.Draw_ButterFly(ref settings, spritebatch);
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0052D73C File Offset: 0x0052B93C
		private void Draw_ButterFly(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Vector2 vector = this._velocity + this._addedVelocity;
			Texture2D value = TextureAssets.Extra[281].Value;
			int num = this._lifeTimeCounted % 10 / 5;
			int variantRow = this._variantRow;
			bool flag = this._variantColumn == 1;
			Rectangle rectangle = new Rectangle(flag ? 10 : 0, (variantRow * 2 + num) * 10, flag ? 14 : 8, 8);
			Vector2 vector2 = rectangle.Size() / 2f;
			float num2 = Utils.Remap((float)this._lifeTimeCounted, 0f, 90f, 0f, 1f, true) * Utils.Remap((float)this._lifeTimeCounted, (float)(this._lifeTimeTotal - 90), (float)this._lifeTimeTotal, 1f, 0f, true);
			Color color = Lighting.GetColor(this._localPosition.ToTileCoordinates());
			this._overrideColor = Color.White;
			Vector4 vector3 = this._overrideColor.ToVector4() * color.ToVector4();
			Color color2 = new Color(vector3);
			float num3 = 0.75f;
			spritebatch.Draw(value, settings.AnchorPosition + this._localPosition, new Rectangle?(rectangle), color2 * num2, 0f, vector2, num3, (vector.X < 0f) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0052D898 File Offset: 0x0052BA98
		private void Draw_Fly(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Vector2 vector = this._velocity + this._addedVelocity;
			Texture2D value = TextureAssets.Extra[262].Value;
			int num = this._lifeTimeCounted % 6 / 3;
			Rectangle rectangle = value.Frame(1, 6, 0, num, 0, 0);
			Vector2 vector2 = new Vector2((float)((vector.X > 0f) ? 3 : 1), 3f);
			float num2 = Utils.Remap((float)this._lifeTimeCounted, 0f, 90f, 0f, 1f, true) * Utils.Remap((float)this._lifeTimeCounted, (float)(this._lifeTimeTotal - 90), (float)this._lifeTimeTotal, 1f, 0f, true);
			Color color = Lighting.GetColor(this._localPosition.ToTileCoordinates());
			if (this._overrideColor == default(Color))
			{
				spritebatch.Draw(value, settings.AnchorPosition + this._localPosition, new Rectangle?(rectangle), color * num2, 0f, vector2, 1f, (vector.X > 0f) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				return;
			}
			Vector4 vector3 = this._overrideColor.ToVector4() * color.ToVector4();
			Color color2 = new Color(vector3);
			rectangle.Offset(0, 12);
			spritebatch.Draw(value, settings.AnchorPosition + this._localPosition, new Rectangle?(rectangle), color2 * num2, 0f, vector2, 1f, (vector.X > 0f) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			rectangle.Offset(0, 12);
			spritebatch.Draw(value, settings.AnchorPosition + this._localPosition, new Rectangle?(rectangle), color * num2, 0f, vector2, 1f, (vector.X > 0f) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0052DA88 File Offset: 0x0052BC88
		public void BeRepelled(ref ParticleRepelDetails details)
		{
			float num = Utils.Remap(this._localPosition.Distance(details.SourcePosition) - details.Radius, 0f, 100f, 1f, 0f, true);
			if (num <= 0f)
			{
				return;
			}
			Vector2 vector = this._localPosition.DirectionFrom(details.SourcePosition).SafeNormalize(-Vector2.UnitY).RotatedByRandom(0.5235987901687622);
			this._addedVelocity = vector * 3.5f * num;
			this._lifeTimeCounted += this._repelLifetimeDecay;
		}

		// Token: 0x04004B9B RID: 19355
		private int _lifeTimeCounted;

		// Token: 0x04004B9C RID: 19356
		private int _lifeTimeTotal;

		// Token: 0x04004B9D RID: 19357
		[CompilerGenerated]
		private bool <IsRestingInPool>k__BackingField;

		// Token: 0x04004B9E RID: 19358
		[CompilerGenerated]
		private bool <ShouldBeRemovedFromRenderer>k__BackingField;

		// Token: 0x04004B9F RID: 19359
		private Vector2 _spawnPosition;

		// Token: 0x04004BA0 RID: 19360
		private Vector2 _localPosition;

		// Token: 0x04004BA1 RID: 19361
		private Vector2 _velocity;

		// Token: 0x04004BA2 RID: 19362
		private float _neverGoBelowThis;

		// Token: 0x04004BA3 RID: 19363
		private Vector2 _addedVelocity;

		// Token: 0x04004BA4 RID: 19364
		private int _repelLifetimeDecay;

		// Token: 0x04004BA5 RID: 19365
		private Color _overrideColor;

		// Token: 0x04004BA6 RID: 19366
		private LittleFlyingCritterParticle.FlyType _type;

		// Token: 0x04004BA7 RID: 19367
		private int _variantRow;

		// Token: 0x04004BA8 RID: 19368
		private int _variantColumn;

		// Token: 0x020007AB RID: 1963
		public enum FlyType
		{
			// Token: 0x040070A7 RID: 28839
			RegularFly,
			// Token: 0x040070A8 RID: 28840
			ButterFly
		}
	}
}
