using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000452 RID: 1106
	public class SlimeSky : CustomSky
	{
		// Token: 0x06003233 RID: 12851 RVA: 0x005E6E88 File Offset: 0x005E5088
		public override void OnLoad()
		{
			this._textures = new Asset<Texture2D>[4];
			for (int i = 0; i < 4; i++)
			{
				this._textures[i] = Main.Assets.Request<Texture2D>("Images/Misc/Sky_Slime_" + (i + 1), 1);
			}
			this.GenerateSlimes();
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x005E6ED8 File Offset: 0x005E50D8
		private void GenerateSlimes()
		{
			this._slimes = new SlimeSky.Slime[Main.maxTilesY / 6];
			for (int i = 0; i < this._slimes.Length; i++)
			{
				int num = (int)((double)Main.screenPosition.Y * 0.7 - (double)Main.screenHeight);
				int num2 = (int)((double)num - Main.worldSurface * 16.0);
				this._slimes[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)this._random.Next(num2, num));
				this._slimes[i].Speed = 5f + 3f * (float)this._random.NextDouble();
				this._slimes[i].Depth = (float)i / (float)this._slimes.Length * 1.75f + 1.6f;
				this._slimes[i].Texture = this._textures[this._random.Next(2)].Value;
				if (this._random.Next(60) == 0)
				{
					this._slimes[i].Texture = this._textures[3].Value;
					this._slimes[i].Speed = 6f + 3f * (float)this._random.NextDouble();
					SlimeSky.Slime[] slimes = this._slimes;
					int num3 = i;
					slimes[num3].Depth = slimes[num3].Depth + 0.5f;
				}
				else if (this._random.Next(30) == 0)
				{
					this._slimes[i].Texture = this._textures[2].Value;
					this._slimes[i].Speed = 6f + 2f * (float)this._random.NextDouble();
				}
				this._slimes[i].Active = true;
			}
			this._slimesRemaining = this._slimes.Length;
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x005E70E0 File Offset: 0x005E52E0
		public override void Update(GameTime gameTime)
		{
			if (FocusHelper.PauseSkies)
			{
				return;
			}
			for (int i = 0; i < this._slimes.Length; i++)
			{
				if (this._slimes[i].Active)
				{
					SlimeSky.Slime[] slimes = this._slimes;
					int num = i;
					int frame = slimes[num].Frame;
					slimes[num].Frame = frame + 1;
					SlimeSky.Slime[] slimes2 = this._slimes;
					int num2 = i;
					slimes2[num2].Position.Y = slimes2[num2].Position.Y + this._slimes[i].Speed;
					if ((double)this._slimes[i].Position.Y > Main.worldSurface * 16.0)
					{
						if (!this._isLeaving)
						{
							this._slimes[i].Depth = (float)i / (float)this._slimes.Length * 1.75f + 1.6f;
							this._slimes[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), -100f);
							this._slimes[i].Texture = this._textures[this._random.Next(2)].Value;
							this._slimes[i].Speed = 5f + 3f * (float)this._random.NextDouble();
							if (this._random.Next(60) == 0)
							{
								this._slimes[i].Texture = this._textures[3].Value;
								this._slimes[i].Speed = 6f + 3f * (float)this._random.NextDouble();
								SlimeSky.Slime[] slimes3 = this._slimes;
								int num3 = i;
								slimes3[num3].Depth = slimes3[num3].Depth + 0.5f;
							}
							else if (this._random.Next(30) == 0)
							{
								this._slimes[i].Texture = this._textures[2].Value;
								this._slimes[i].Speed = 6f + 2f * (float)this._random.NextDouble();
							}
						}
						else
						{
							this._slimes[i].Active = false;
							this._slimesRemaining--;
						}
					}
				}
			}
			if (this._slimesRemaining == 0)
			{
				this._isActive = false;
			}
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x005E7344 File Offset: 0x005E5544
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.screenPosition.Y > 10000f || Main.gameMenu)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < this._slimes.Length; i++)
			{
				float depth = this._slimes[i].Depth;
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
			Vector2 vector = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, Main.screenWidth + 1000, Main.screenHeight + 1000);
			for (int j = num; j < num2; j++)
			{
				if (this._slimes[j].Active)
				{
					Color color = new Color(Main.ColorOfTheSkies.ToVector4() * 0.9f + new Vector4(0.1f)) * 0.8f;
					float num3 = 1f;
					if (this._slimes[j].Depth > 3f)
					{
						num3 = 0.6f;
					}
					else if ((double)this._slimes[j].Depth > 2.5)
					{
						num3 = 0.7f;
					}
					else if (this._slimes[j].Depth > 2f)
					{
						num3 = 0.8f;
					}
					else if ((double)this._slimes[j].Depth > 1.5)
					{
						num3 = 0.9f;
					}
					num3 *= 0.8f;
					color = new Color((int)((float)color.R * num3), (int)((float)color.G * num3), (int)((float)color.B * num3), (int)((float)color.A * num3));
					Vector2 vector2 = new Vector2(1f / this._slimes[j].Depth, 0.9f / this._slimes[j].Depth);
					Vector2 vector3 = this._slimes[j].Position;
					vector3 = (vector3 - vector) * vector2 + vector - Main.screenPosition;
					vector3.X = (vector3.X + 500f) % 4000f;
					if (vector3.X < 0f)
					{
						vector3.X += 4000f;
					}
					vector3.X -= 500f;
					if (rectangle.Contains((int)vector3.X, (int)vector3.Y))
					{
						spriteBatch.Draw(this._slimes[j].Texture, vector3, new Rectangle?(this._slimes[j].GetSourceRectangle()), color, 0f, Vector2.Zero, vector2.X * 2f, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x005E764F File Offset: 0x005E584F
		public override void Activate(Vector2 position, params object[] args)
		{
			this.GenerateSlimes();
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x005E7665 File Offset: 0x005E5865
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x005E766E File Offset: 0x005E586E
		public override void Reset()
		{
			this._isActive = false;
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x005E7677 File Offset: 0x005E5877
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x005E767F File Offset: 0x005E587F
		public SlimeSky()
		{
		}

		// Token: 0x040057FD RID: 22525
		private Asset<Texture2D>[] _textures;

		// Token: 0x040057FE RID: 22526
		private SlimeSky.Slime[] _slimes;

		// Token: 0x040057FF RID: 22527
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04005800 RID: 22528
		private int _slimesRemaining;

		// Token: 0x04005801 RID: 22529
		private bool _isActive;

		// Token: 0x04005802 RID: 22530
		private bool _isLeaving;

		// Token: 0x02000965 RID: 2405
		private struct Slime
		{
			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x060048CE RID: 18638 RVA: 0x006D00F6 File Offset: 0x006CE2F6
			// (set) Token: 0x060048CF RID: 18639 RVA: 0x006D00FE File Offset: 0x006CE2FE
			public Texture2D Texture
			{
				get
				{
					return this._texture;
				}
				set
				{
					this._texture = value;
					this.FrameWidth = value.Width;
					this.FrameHeight = value.Height / 4;
				}
			}

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x060048D0 RID: 18640 RVA: 0x006D0121 File Offset: 0x006CE321
			// (set) Token: 0x060048D1 RID: 18641 RVA: 0x006D0129 File Offset: 0x006CE329
			public int Frame
			{
				get
				{
					return this._frame;
				}
				set
				{
					this._frame = value % 24;
				}
			}

			// Token: 0x060048D2 RID: 18642 RVA: 0x006D0135 File Offset: 0x006CE335
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(0, this._frame / 6 * this.FrameHeight, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x040075BA RID: 30138
			private const int MAX_FRAMES = 4;

			// Token: 0x040075BB RID: 30139
			private const int FRAME_RATE = 6;

			// Token: 0x040075BC RID: 30140
			private Texture2D _texture;

			// Token: 0x040075BD RID: 30141
			public Vector2 Position;

			// Token: 0x040075BE RID: 30142
			public float Depth;

			// Token: 0x040075BF RID: 30143
			public int FrameHeight;

			// Token: 0x040075C0 RID: 30144
			public int FrameWidth;

			// Token: 0x040075C1 RID: 30145
			public float Speed;

			// Token: 0x040075C2 RID: 30146
			public bool Active;

			// Token: 0x040075C3 RID: 30147
			private int _frame;
		}
	}
}
