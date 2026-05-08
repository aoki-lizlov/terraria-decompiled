using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200044D RID: 1101
	public class PartySky : CustomSky
	{
		// Token: 0x06003201 RID: 12801 RVA: 0x005E55D4 File Offset: 0x005E37D4
		public override void OnLoad()
		{
			this._textures = new Asset<Texture2D>[3];
			for (int i = 0; i < this._textures.Length; i++)
			{
				this._textures[i] = TextureAssets.Extra[69 + i];
			}
			this.GenerateBalloons(false);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x005E561C File Offset: 0x005E381C
		private void GenerateBalloons(bool onlyMissing)
		{
			if (!onlyMissing)
			{
				this._balloons = new PartySky.Balloon[Main.maxTilesY / 4];
			}
			for (int i = 0; i < this._balloons.Length; i++)
			{
				if (!onlyMissing || !this._balloons[i].Active)
				{
					int num = (int)((double)Main.screenPosition.Y * 0.7 - (double)Main.screenHeight);
					int num2 = (int)((double)num - Main.worldSurface * 16.0);
					this._balloons[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)this._random.Next(num2, num));
					this.ResetBalloon(i);
					this._balloons[i].Active = true;
				}
			}
			this._balloonsDrawing = this._balloons.Length;
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x005E5704 File Offset: 0x005E3904
		public void ResetBalloon(int i)
		{
			this._balloons[i].Depth = (float)i / (float)this._balloons.Length * 1.75f + 1.6f;
			this._balloons[i].Speed = -1.5f - 2.5f * (float)this._random.NextDouble();
			this._balloons[i].Texture = this._textures[this._random.Next(2)].Value;
			this._balloons[i].Variant = this._random.Next(3);
			if (this._random.Next(30) == 0)
			{
				this._balloons[i].Texture = this._textures[2].Value;
			}
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x005E57D8 File Offset: 0x005E39D8
		public override void Update(GameTime gameTime)
		{
			if (!PartySky.MultipleSkyWorkaroundFix && Main.dayRate == 0)
			{
				return;
			}
			PartySky.MultipleSkyWorkaroundFix = false;
			if (FocusHelper.PauseSkies)
			{
				return;
			}
			for (int i = 0; i < this._balloons.Length; i++)
			{
				if (this._balloons[i].Active)
				{
					PartySky.Balloon[] balloons = this._balloons;
					int num = i;
					int frame = balloons[num].Frame;
					balloons[num].Frame = frame + 1;
					PartySky.Balloon[] balloons2 = this._balloons;
					int num2 = i;
					balloons2[num2].Position.Y = balloons2[num2].Position.Y + this._balloons[i].Speed;
					PartySky.Balloon[] balloons3 = this._balloons;
					int num3 = i;
					balloons3[num3].Position.X = balloons3[num3].Position.X + Main.windSpeedCurrent * (3f - this._balloons[i].Speed);
					if (this._balloons[i].Position.Y < 300f)
					{
						if (!this._leaving)
						{
							this.ResetBalloon(i);
							this._balloons[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)Main.worldSurface * 16f + 1600f);
							if (this._random.Next(30) == 0)
							{
								this._balloons[i].Texture = this._textures[2].Value;
							}
						}
						else
						{
							this._balloons[i].Active = false;
							this._balloonsDrawing--;
						}
					}
				}
			}
			if (this._balloonsDrawing == 0)
			{
				this._active = false;
			}
			this._active = true;
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x005E5980 File Offset: 0x005E3B80
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.gameMenu && this._active)
			{
				this._active = false;
				this._leaving = false;
				for (int i = 0; i < this._balloons.Length; i++)
				{
					this._balloons[i].Active = false;
				}
			}
			if ((double)Main.screenPosition.Y > Main.worldSurface * 16.0 || Main.gameMenu)
			{
				return;
			}
			if (this.Opacity <= 0f)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			for (int j = 0; j < this._balloons.Length; j++)
			{
				float depth = this._balloons[j].Depth;
				if (num == -1 && depth < maxDepth)
				{
					num = j;
				}
				if (depth <= minDepth)
				{
					break;
				}
				num2 = j;
			}
			if (num == -1)
			{
				return;
			}
			Vector2 vector = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, Main.screenWidth + 1000, Main.screenHeight + 1000);
			for (int k = num; k < num2; k++)
			{
				if (this._balloons[k].Active)
				{
					Color color = new Color(Main.ColorOfTheSkies.ToVector4() * 0.9f + new Vector4(0.1f)) * 0.8f;
					float num3 = 1f;
					if (this._balloons[k].Depth > 3f)
					{
						num3 = 0.6f;
					}
					else if ((double)this._balloons[k].Depth > 2.5)
					{
						num3 = 0.7f;
					}
					else if (this._balloons[k].Depth > 2f)
					{
						num3 = 0.8f;
					}
					else if ((double)this._balloons[k].Depth > 1.5)
					{
						num3 = 0.9f;
					}
					num3 *= 0.9f;
					color = new Color((int)((float)color.R * num3), (int)((float)color.G * num3), (int)((float)color.B * num3), (int)((float)color.A * num3));
					Vector2 vector2 = new Vector2(1f / this._balloons[k].Depth, 0.9f / this._balloons[k].Depth);
					Vector2 vector3 = this._balloons[k].Position;
					vector3 = (vector3 - vector) * vector2 + vector - Main.screenPosition;
					vector3.X = (vector3.X + 500f) % 4000f;
					if (vector3.X < 0f)
					{
						vector3.X += 4000f;
					}
					vector3.X -= 500f;
					if (rectangle.Contains((int)vector3.X, (int)vector3.Y))
					{
						spriteBatch.Draw(this._balloons[k].Texture, vector3, new Rectangle?(this._balloons[k].GetSourceRectangle()), color * this.Opacity, 0f, Vector2.Zero, vector2.X * 2f, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x005E5CF6 File Offset: 0x005E3EF6
		public override void Activate(Vector2 position, params object[] args)
		{
			if (this._active)
			{
				this._leaving = false;
				this.GenerateBalloons(true);
				return;
			}
			this.GenerateBalloons(false);
			this._active = true;
			this._leaving = false;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x005E5D24 File Offset: 0x005E3F24
		public override void Deactivate(params object[] args)
		{
			this._leaving = true;
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x005E5D2D File Offset: 0x005E3F2D
		public override bool IsActive()
		{
			return this._active;
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x005E5D35 File Offset: 0x005E3F35
		public override void Reset()
		{
			this._active = false;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x005E5D3E File Offset: 0x005E3F3E
		public PartySky()
		{
		}

		// Token: 0x040057DE RID: 22494
		public static bool MultipleSkyWorkaroundFix;

		// Token: 0x040057DF RID: 22495
		private bool _active;

		// Token: 0x040057E0 RID: 22496
		private bool _leaving;

		// Token: 0x040057E1 RID: 22497
		private Asset<Texture2D>[] _textures;

		// Token: 0x040057E2 RID: 22498
		private PartySky.Balloon[] _balloons;

		// Token: 0x040057E3 RID: 22499
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040057E4 RID: 22500
		private int _balloonsDrawing;

		// Token: 0x02000962 RID: 2402
		private struct Balloon
		{
			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x060048C9 RID: 18633 RVA: 0x006D0085 File Offset: 0x006CE285
			// (set) Token: 0x060048CA RID: 18634 RVA: 0x006D008D File Offset: 0x006CE28D
			public Texture2D Texture
			{
				get
				{
					return this._texture;
				}
				set
				{
					this._texture = value;
					this.FrameWidth = value.Width / 3;
					this.FrameHeight = value.Height / 3;
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x060048CB RID: 18635 RVA: 0x006D00B2 File Offset: 0x006CE2B2
			// (set) Token: 0x060048CC RID: 18636 RVA: 0x006D00BA File Offset: 0x006CE2BA
			public int Frame
			{
				get
				{
					return this._frameCounter;
				}
				set
				{
					this._frameCounter = value % 42;
				}
			}

			// Token: 0x060048CD RID: 18637 RVA: 0x006D00C6 File Offset: 0x006CE2C6
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(this.FrameWidth * this.Variant, this._frameCounter / 14 * this.FrameHeight, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x040075A5 RID: 30117
			private const int MAX_FRAMES_X = 3;

			// Token: 0x040075A6 RID: 30118
			private const int MAX_FRAMES_Y = 3;

			// Token: 0x040075A7 RID: 30119
			private const int FRAME_RATE = 14;

			// Token: 0x040075A8 RID: 30120
			public int Variant;

			// Token: 0x040075A9 RID: 30121
			private Texture2D _texture;

			// Token: 0x040075AA RID: 30122
			public Vector2 Position;

			// Token: 0x040075AB RID: 30123
			public float Depth;

			// Token: 0x040075AC RID: 30124
			public int FrameHeight;

			// Token: 0x040075AD RID: 30125
			public int FrameWidth;

			// Token: 0x040075AE RID: 30126
			public float Speed;

			// Token: 0x040075AF RID: 30127
			public bool Active;

			// Token: 0x040075B0 RID: 30128
			private int _frameCounter;
		}
	}
}
