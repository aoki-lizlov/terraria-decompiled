using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200020F RID: 527
	public class FlameParticle : ABasicParticle
	{
		// Token: 0x06002185 RID: 8581 RVA: 0x0052FC10 File Offset: 0x0052DE10
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.FadeOutNormalizedTime = 1f;
			this._timeTolive = 0f;
			this._timeSinceSpawn = 0f;
			this._indexOfPlayerWhoSpawnedThis = 0;
			this._packedShaderIndex = 0;
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0052FC47 File Offset: 0x0052DE47
		public override void SetBasicInfo(Asset<Texture2D> textureAsset, Rectangle? frame, Vector2 initialVelocity, Vector2 initialLocalPosition)
		{
			base.SetBasicInfo(textureAsset, frame, initialVelocity, initialLocalPosition);
			this._origin = new Vector2((float)(this._frame.Width / 2), (float)(this._frame.Height - 2));
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0052FC7B File Offset: 0x0052DE7B
		public void SetTypeInfo(float timeToLive, int indexOfPlayerWhoSpawnedIt, int packedShaderIndex)
		{
			this._timeTolive = timeToLive;
			this._indexOfPlayerWhoSpawnedThis = indexOfPlayerWhoSpawnedIt;
			this._packedShaderIndex = packedShaderIndex;
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0052FC92 File Offset: 0x0052DE92
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			if (this._timeSinceSpawn >= this._timeTolive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0052FCC4 File Offset: 0x0052DEC4
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = new Color(120, 120, 120, 60) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this._timeTolive, true);
			Vector2 vector = settings.AnchorPosition + this.LocalPosition;
			ulong num = Main.TileFrameSeed ^ (((ulong)this.LocalPosition.X << 32) | (ulong)((uint)this.LocalPosition.Y));
			Player player = Main.player[this._indexOfPlayerWhoSpawnedThis];
			for (int i = 0; i < 4; i++)
			{
				Vector2 vector2 = new Vector2((float)Utils.RandomInt(ref num, -2, 3), (float)Utils.RandomInt(ref num, -2, 3));
				DrawData drawData = new DrawData(this._texture.Value, vector + vector2 * this.Scale, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, SpriteEffects.None, 0f)
				{
					shader = this._packedShaderIndex
				};
				PlayerDrawHelper.SetShaderForData(player, 0, ref drawData);
				drawData.Draw(spritebatch);
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0052FDFD File Offset: 0x0052DFFD
		public FlameParticle()
		{
		}

		// Token: 0x04004BF3 RID: 19443
		public float FadeOutNormalizedTime = 1f;

		// Token: 0x04004BF4 RID: 19444
		private float _timeTolive;

		// Token: 0x04004BF5 RID: 19445
		private float _timeSinceSpawn;

		// Token: 0x04004BF6 RID: 19446
		private int _indexOfPlayerWhoSpawnedThis;

		// Token: 0x04004BF7 RID: 19447
		private int _packedShaderIndex;
	}
}
