using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000455 RID: 1109
	public class NebulaSky : CustomSky
	{
		// Token: 0x0600324F RID: 12879 RVA: 0x005E8318 File Offset: 0x005E6518
		public override void OnLoad()
		{
			this._planetTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Planet", 1);
			this._bgTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Background", 1);
			this._beamTexture = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Beam", 1);
			this._rockTextures = new Asset<Texture2D>[3];
			for (int i = 0; i < this._rockTextures.Length; i++)
			{
				this._rockTextures[i] = Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Rock_" + i, 1);
			}
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x005E83AC File Offset: 0x005E65AC
		public override void Update(GameTime gameTime)
		{
			if (this._isActive)
			{
				this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
				return;
			}
			this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x005E83FA File Offset: 0x005E65FA
		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x005E8420 File Offset: 0x005E6620
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
				spriteBatch.Draw(this._bgTexture.Value, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * this._fadeOpacity));
				Vector2 vector = new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
				Vector2 vector2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
				spriteBatch.Draw(this._planetTexture.Value, vector + new Vector2(-200f, -200f) + vector2, null, Color.White * 0.9f * this._fadeOpacity, 0f, new Vector2((float)(this._planetTexture.Width() >> 1), (float)(this._planetTexture.Height() >> 1)), 1f, SpriteEffects.None, 0f);
			}
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < this._pillars.Length; i++)
			{
				float depth = this._pillars[i].Depth;
				if (num == -1 && depth < maxDepth)
				{
					num = i;
				}
				if (depth <= minDepth)
				{
					break;
				}
				num2 = i;
			}
			if (num == -1)
			{
				return;
			}
			Vector2 vector3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, Main.screenWidth + 1000, Main.screenHeight + 1000);
			float num3 = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			for (int j = num; j < num2; j++)
			{
				Vector2 vector4 = new Vector2(1f / this._pillars[j].Depth, 0.9f / this._pillars[j].Depth);
				Vector2 vector5 = this._pillars[j].Position;
				vector5 = (vector5 - vector3) * vector4 + vector3 - Main.screenPosition;
				if (rectangle.Contains((int)vector5.X, (int)vector5.Y))
				{
					float num4 = vector4.X * 450f;
					spriteBatch.Draw(this._beamTexture.Value, vector5, null, Color.White * 0.2f * num3 * this._fadeOpacity, 0f, Vector2.Zero, new Vector2(num4 / 70f, num4 / 45f), SpriteEffects.None, 0f);
					int num5 = 0;
					for (float num6 = 0f; num6 <= 1f; num6 += 0.03f)
					{
						float num7 = 1f - (num6 + Main.GlobalTimeWrappedHourly * 0.02f + (float)Math.Sin((double)j)) % 1f;
						spriteBatch.Draw(this._rockTextures[num5].Value, vector5 + new Vector2((float)Math.Sin((double)(num6 * 1582f)) * (num4 * 0.5f) + num4 * 0.5f, num7 * 2000f), null, Color.White * num7 * num3 * this._fadeOpacity, num7 * 20f, new Vector2((float)(this._rockTextures[num5].Width() >> 1), (float)(this._rockTextures[num5].Height() >> 1)), 0.9f, SpriteEffects.None, 0f);
						num5 = (num5 + 1) % this._rockTextures.Length;
					}
				}
			}
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x005E888F File Offset: 0x005E6A8F
		public override float GetCloudAlpha()
		{
			return (1f - this._fadeOpacity) * 0.3f + 0.7f;
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x005E88AC File Offset: 0x005E6AAC
		public override void Activate(Vector2 position, params object[] args)
		{
			this._fadeOpacity = 0.002f;
			this._isActive = true;
			this._pillars = new NebulaSky.LightPillar[40];
			for (int i = 0; i < this._pillars.Length; i++)
			{
				this._pillars[i].Position.X = (float)i / (float)this._pillars.Length * ((float)Main.maxTilesX * 16f + 20000f) + this._random.NextFloat() * 40f - 20f - 20000f;
				this._pillars[i].Position.Y = this._random.NextFloat() * 200f - 2000f;
				this._pillars[i].Depth = this._random.NextFloat() * 8f + 7f;
			}
			Array.Sort<NebulaSky.LightPillar>(this._pillars, new Comparison<NebulaSky.LightPillar>(this.SortMethod));
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x005E89B0 File Offset: 0x005E6BB0
		private int SortMethod(NebulaSky.LightPillar pillar1, NebulaSky.LightPillar pillar2)
		{
			return pillar2.Depth.CompareTo(pillar1.Depth);
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x005E89C4 File Offset: 0x005E6BC4
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x005E89C4 File Offset: 0x005E6BC4
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x005E89CD File Offset: 0x005E6BCD
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x005E89E6 File Offset: 0x005E6BE6
		public NebulaSky()
		{
		}

		// Token: 0x04005810 RID: 22544
		private NebulaSky.LightPillar[] _pillars;

		// Token: 0x04005811 RID: 22545
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04005812 RID: 22546
		private Asset<Texture2D> _planetTexture;

		// Token: 0x04005813 RID: 22547
		private Asset<Texture2D> _bgTexture;

		// Token: 0x04005814 RID: 22548
		private Asset<Texture2D> _beamTexture;

		// Token: 0x04005815 RID: 22549
		private Asset<Texture2D>[] _rockTextures;

		// Token: 0x04005816 RID: 22550
		private bool _isActive;

		// Token: 0x04005817 RID: 22551
		private float _fadeOpacity;

		// Token: 0x0200096C RID: 2412
		private struct LightPillar
		{
			// Token: 0x040075E0 RID: 30176
			public Vector2 Position;

			// Token: 0x040075E1 RID: 30177
			public float Depth;
		}
	}
}
