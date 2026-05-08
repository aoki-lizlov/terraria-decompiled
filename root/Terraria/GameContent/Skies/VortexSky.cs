using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000450 RID: 1104
	public class VortexSky : CustomSky
	{
		// Token: 0x0600321E RID: 12830 RVA: 0x005E6098 File Offset: 0x005E4298
		public override void OnLoad()
		{
			this._planetTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Planet", 1);
			this._bgTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Background", 1);
			this._boltTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Bolt", 1);
			this._flashTexture = Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Flash", 1);
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x005E6100 File Offset: 0x005E4300
		public override void Update(GameTime gameTime)
		{
			if (this._isActive)
			{
				this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
			}
			else
			{
				this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
			}
			if (this._ticksUntilNextBolt <= 0)
			{
				this._ticksUntilNextBolt = this._random.Next(1, 5);
				int num = 0;
				while (this._bolts[num].IsAlive && num != this._bolts.Length - 1)
				{
					num++;
				}
				this._bolts[num].IsAlive = true;
				this._bolts[num].Position.X = this._random.NextFloat() * ((float)Main.maxTilesX * 16f + 4000f) - 2000f;
				this._bolts[num].Position.Y = this._random.NextFloat() * 500f;
				this._bolts[num].Depth = this._random.NextFloat() * 8f + 2f;
				this._bolts[num].Life = 30;
			}
			this._ticksUntilNextBolt--;
			for (int i = 0; i < this._bolts.Length; i++)
			{
				if (this._bolts[i].IsAlive)
				{
					VortexSky.Bolt[] bolts = this._bolts;
					int num2 = i;
					bolts[num2].Life = bolts[num2].Life - 1;
					if (this._bolts[i].Life <= 0)
					{
						this._bolts[i].IsAlive = false;
					}
				}
			}
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x005E62B4 File Offset: 0x005E44B4
		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x005E62D8 File Offset: 0x005E44D8
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
				spriteBatch.Draw(this._bgTexture.Value, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f) * this._fadeOpacity);
				Vector2 vector = new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
				Vector2 vector2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
				spriteBatch.Draw(this._planetTexture.Value, vector + new Vector2(-200f, -200f) + vector2, null, Color.White * 0.9f * this._fadeOpacity, 0f, new Vector2((float)(this._planetTexture.Width() >> 1), (float)(this._planetTexture.Height() >> 1)), 1f, SpriteEffects.None, 0f);
			}
			float num = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			Vector2 vector3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, Main.screenWidth + 1000, Main.screenHeight + 1000);
			for (int i = 0; i < this._bolts.Length; i++)
			{
				if (this._bolts[i].IsAlive && this._bolts[i].Depth > minDepth && this._bolts[i].Depth < maxDepth)
				{
					Vector2 vector4 = new Vector2(1f / this._bolts[i].Depth, 0.9f / this._bolts[i].Depth);
					Vector2 vector5 = (this._bolts[i].Position - vector3) * vector4 + vector3 - Main.screenPosition;
					if (rectangle.Contains((int)vector5.X, (int)vector5.Y))
					{
						Texture2D texture2D = this._boltTexture.Value;
						int life = this._bolts[i].Life;
						if (life > 26 && life % 2 == 0)
						{
							texture2D = this._flashTexture.Value;
						}
						float num2 = (float)life / 30f;
						spriteBatch.Draw(texture2D, vector5, null, Color.White * num * num2 * this._fadeOpacity, 0f, Vector2.Zero, vector4.X * 5f, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x005E6667 File Offset: 0x005E4867
		public override float GetCloudAlpha()
		{
			return (1f - this._fadeOpacity) * 0.3f + 0.7f;
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x005E6684 File Offset: 0x005E4884
		public override void Activate(Vector2 position, params object[] args)
		{
			this._fadeOpacity = 0.002f;
			this._isActive = true;
			this._bolts = new VortexSky.Bolt[500];
			for (int i = 0; i < this._bolts.Length; i++)
			{
				this._bolts[i].IsAlive = false;
			}
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x005E66D8 File Offset: 0x005E48D8
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x005E66D8 File Offset: 0x005E48D8
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x005E66E1 File Offset: 0x005E48E1
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x005E66FA File Offset: 0x005E48FA
		public VortexSky()
		{
		}

		// Token: 0x040057ED RID: 22509
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040057EE RID: 22510
		private Asset<Texture2D> _planetTexture;

		// Token: 0x040057EF RID: 22511
		private Asset<Texture2D> _bgTexture;

		// Token: 0x040057F0 RID: 22512
		private Asset<Texture2D> _boltTexture;

		// Token: 0x040057F1 RID: 22513
		private Asset<Texture2D> _flashTexture;

		// Token: 0x040057F2 RID: 22514
		private bool _isActive;

		// Token: 0x040057F3 RID: 22515
		private int _ticksUntilNextBolt;

		// Token: 0x040057F4 RID: 22516
		private float _fadeOpacity;

		// Token: 0x040057F5 RID: 22517
		private VortexSky.Bolt[] _bolts;

		// Token: 0x02000963 RID: 2403
		private struct Bolt
		{
			// Token: 0x040075B1 RID: 30129
			public Vector2 Position;

			// Token: 0x040075B2 RID: 30130
			public float Depth;

			// Token: 0x040075B3 RID: 30131
			public int Life;

			// Token: 0x040075B4 RID: 30132
			public bool IsAlive;
		}
	}
}
