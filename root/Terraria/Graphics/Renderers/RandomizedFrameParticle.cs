using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000210 RID: 528
	public class RandomizedFrameParticle : ABasicParticle
	{
		// Token: 0x0600218B RID: 8587 RVA: 0x0052FE10 File Offset: 0x0052E010
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.FadeInNormalizedTime = 0f;
			this.FadeOutNormalizedTime = 1f;
			this.ColorTint = Color.White;
			this.AnimationFramesAmount = 0;
			this.GameFramesPerAnimationFrame = 0;
			this._timeTolive = 0f;
			this._timeSinceSpawn = 0f;
			this._gameFramesCounted = 0;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0052FE6F File Offset: 0x0052E06F
		public void SetTypeInfo(int animationFramesAmount, int gameFramesPerAnimationFrame, float timeToLive)
		{
			this._timeTolive = timeToLive;
			this.GameFramesPerAnimationFrame = gameFramesPerAnimationFrame;
			this.AnimationFramesAmount = animationFramesAmount;
			this.RandomizeFrame();
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0052FE8C File Offset: 0x0052E08C
		private void RandomizeFrame()
		{
			this._frame = this._texture.Frame(1, this.AnimationFramesAmount, 0, Main.rand.Next(this.AnimationFramesAmount), 0, 0);
			this._origin = this._frame.Size() / 2f;
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0052FEE0 File Offset: 0x0052E0E0
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			if (this._timeSinceSpawn >= this._timeTolive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
			int num = this._gameFramesCounted + 1;
			this._gameFramesCounted = num;
			if (num >= this.GameFramesPerAnimationFrame)
			{
				this._gameFramesCounted = 0;
				this.RandomizeFrame();
			}
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0052FF44 File Offset: 0x0052E144
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = this.ColorTint * Utils.GetLerpValue(0f, this.FadeInNormalizedTime, this._timeSinceSpawn / this._timeTolive, true) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this._timeTolive, true);
			spritebatch.Draw(this._texture.Value, settings.AnchorPosition + this.LocalPosition, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, SpriteEffects.None, 0f);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0052FFE4 File Offset: 0x0052E1E4
		public RandomizedFrameParticle()
		{
		}

		// Token: 0x04004BF8 RID: 19448
		public float FadeInNormalizedTime;

		// Token: 0x04004BF9 RID: 19449
		public float FadeOutNormalizedTime = 1f;

		// Token: 0x04004BFA RID: 19450
		public Color ColorTint = Color.White;

		// Token: 0x04004BFB RID: 19451
		public int AnimationFramesAmount;

		// Token: 0x04004BFC RID: 19452
		public int GameFramesPerAnimationFrame;

		// Token: 0x04004BFD RID: 19453
		private float _timeTolive;

		// Token: 0x04004BFE RID: 19454
		private float _timeSinceSpawn;

		// Token: 0x04004BFF RID: 19455
		private int _gameFramesCounted;
	}
}
