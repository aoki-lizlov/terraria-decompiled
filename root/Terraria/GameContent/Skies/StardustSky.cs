using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000453 RID: 1107
	public class StardustSky : CustomSky
	{
		// Token: 0x0600323C RID: 12860 RVA: 0x005E7694 File Offset: 0x005E5894
		public override void OnLoad()
		{
			this._planetTexture = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Planet", 1);
			this._bgTexture = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Background", 1);
			this._starTextures = new Asset<Texture2D>[2];
			for (int i = 0; i < this._starTextures.Length; i++)
			{
				this._starTextures[i] = Main.Assets.Request<Texture2D>("Images/Misc/StarDustSky/Star " + i, 1);
			}
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x005E7710 File Offset: 0x005E5910
		public override void Update(GameTime gameTime)
		{
			if (this._isActive)
			{
				this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
				return;
			}
			this._fadeOpacity = Math.Max(0f, this._fadeOpacity - 0.01f);
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x005E775E File Offset: 0x005E595E
		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x005E7784 File Offset: 0x005E5984
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
			for (int i = 0; i < this._stars.Length; i++)
			{
				float depth = this._stars[i].Depth;
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
			float num3 = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			Vector2 vector3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, Main.screenWidth + 1000, Main.screenHeight + 1000);
			for (int j = num; j < num2; j++)
			{
				Vector2 vector4 = new Vector2(1f / this._stars[j].Depth, 1.1f / this._stars[j].Depth);
				Vector2 vector5 = (this._stars[j].Position - vector3) * vector4 + vector3 - Main.screenPosition;
				if (rectangle.Contains((int)vector5.X, (int)vector5.Y))
				{
					float num4 = (float)Math.Sin((double)(this._stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly + this._stars[j].SinOffset)) * this._stars[j].AlphaAmplitude + this._stars[j].AlphaAmplitude;
					float num5 = (float)Math.Sin((double)(this._stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly * 5f + this._stars[j].SinOffset)) * 0.1f - 0.1f;
					num4 = MathHelper.Clamp(num4, 0f, 1f);
					Texture2D value = this._starTextures[this._stars[j].TextureIndex].Value;
					spriteBatch.Draw(value, vector5, null, Color.White * num3 * num4 * 0.8f * (1f - num5) * this._fadeOpacity, 0f, new Vector2((float)(value.Width >> 1), (float)(value.Height >> 1)), (vector4.X * 0.5f + 0.5f) * (num4 * 0.3f + 0.7f), SpriteEffects.None, 0f);
				}
			}
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x005E7BD8 File Offset: 0x005E5DD8
		public override float GetCloudAlpha()
		{
			return (1f - this._fadeOpacity) * 0.3f + 0.7f;
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x005E7BF4 File Offset: 0x005E5DF4
		public override void Activate(Vector2 position, params object[] args)
		{
			this._fadeOpacity = 0.002f;
			this._isActive = true;
			int num = 200;
			int num2 = 10;
			this._stars = new StardustSky.Star[num * num2];
			int num3 = 0;
			for (int i = 0; i < num; i++)
			{
				float num4 = (float)i / (float)num;
				for (int j = 0; j < num2; j++)
				{
					float num5 = (float)j / (float)num2;
					this._stars[num3].Position.X = num4 * (float)Main.maxTilesX * 16f;
					this._stars[num3].Position.Y = num5 * ((float)Main.worldSurface * 16f + 2000f) - 1000f;
					this._stars[num3].Depth = this._random.NextFloat() * 8f + 1.5f;
					this._stars[num3].TextureIndex = this._random.Next(this._starTextures.Length);
					this._stars[num3].SinOffset = this._random.NextFloat() * 6.28f;
					this._stars[num3].AlphaAmplitude = this._random.NextFloat() * 5f;
					this._stars[num3].AlphaFrequency = this._random.NextFloat() + 1f;
					num3++;
				}
			}
			Array.Sort<StardustSky.Star>(this._stars, new Comparison<StardustSky.Star>(this.SortMethod));
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x005E7D86 File Offset: 0x005E5F86
		private int SortMethod(StardustSky.Star meteor1, StardustSky.Star meteor2)
		{
			return meteor2.Depth.CompareTo(meteor1.Depth);
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x005E7D9A File Offset: 0x005E5F9A
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x005E7D9A File Offset: 0x005E5F9A
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x005E7DA3 File Offset: 0x005E5FA3
		public override bool IsActive()
		{
			return this._isActive || this._fadeOpacity > 0.001f;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x005E7DBC File Offset: 0x005E5FBC
		public StardustSky()
		{
		}

		// Token: 0x04005803 RID: 22531
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04005804 RID: 22532
		private Asset<Texture2D> _planetTexture;

		// Token: 0x04005805 RID: 22533
		private Asset<Texture2D> _bgTexture;

		// Token: 0x04005806 RID: 22534
		private Asset<Texture2D>[] _starTextures;

		// Token: 0x04005807 RID: 22535
		private bool _isActive;

		// Token: 0x04005808 RID: 22536
		private StardustSky.Star[] _stars;

		// Token: 0x04005809 RID: 22537
		private float _fadeOpacity;

		// Token: 0x02000966 RID: 2406
		private struct Star
		{
			// Token: 0x040075C4 RID: 30148
			public Vector2 Position;

			// Token: 0x040075C5 RID: 30149
			public float Depth;

			// Token: 0x040075C6 RID: 30150
			public int TextureIndex;

			// Token: 0x040075C7 RID: 30151
			public float SinOffset;

			// Token: 0x040075C8 RID: 30152
			public float AlphaFrequency;

			// Token: 0x040075C9 RID: 30153
			public float AlphaAmplitude;
		}
	}
}
