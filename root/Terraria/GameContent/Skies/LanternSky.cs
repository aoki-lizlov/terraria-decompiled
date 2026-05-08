using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200044B RID: 1099
	public class LanternSky : CustomSky
	{
		// Token: 0x060031EE RID: 12782 RVA: 0x005E4AE1 File Offset: 0x005E2CE1
		public override void OnLoad()
		{
			this._texture = TextureAssets.Extra[134];
			this.GenerateLanterns(false);
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x005E4AFC File Offset: 0x005E2CFC
		private void GenerateLanterns(bool onlyMissing)
		{
			if (!onlyMissing)
			{
				this._lanterns = new LanternSky.Lantern[Main.maxTilesY / 4];
			}
			for (int i = 0; i < this._lanterns.Length; i++)
			{
				if (!onlyMissing || !this._lanterns[i].Active)
				{
					int num = (int)((double)Main.screenPosition.Y * 0.7 - (double)Main.screenHeight);
					int num2 = (int)((double)num - Main.worldSurface * 16.0);
					this._lanterns[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)this._random.Next(num2, num));
					this.ResetLantern(i);
					this._lanterns[i].Active = true;
				}
			}
			this._lanternsDrawing = this._lanterns.Length;
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x005E4BE4 File Offset: 0x005E2DE4
		public void ResetLantern(int i)
		{
			this._lanterns[i].Depth = (1f - (float)i / (float)this._lanterns.Length) * 4.4f + 1.6f;
			this._lanterns[i].Speed = -1.5f - 2.5f * (float)this._random.NextDouble();
			this._lanterns[i].Texture = this._texture.Value;
			this._lanterns[i].Variant = this._random.Next(3);
			this._lanterns[i].TimeUntilFloat = (int)((float)(2000 + this._random.Next(1200)) * 2f);
			this._lanterns[i].TimeUntilFloatMax = this._lanterns[i].TimeUntilFloat;
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x005E4CD4 File Offset: 0x005E2ED4
		public override void Update(GameTime gameTime)
		{
			if (FocusHelper.PauseSkies)
			{
				return;
			}
			this._opacity = Utils.Clamp<float>(this._opacity + (float)LanternNight.LanternsUp.ToDirectionInt() * 0.01f, 0f, 1f);
			for (int i = 0; i < this._lanterns.Length; i++)
			{
				if (this._lanterns[i].Active)
				{
					float num = Main.windSpeedCurrent;
					if (num == 0f)
					{
						num = 0.1f;
					}
					float num2 = (float)Math.Sin((double)(this._lanterns[i].Position.X / 120f)) * 0.5f;
					LanternSky.Lantern[] lanterns = this._lanterns;
					int num3 = i;
					lanterns[num3].Position.Y = lanterns[num3].Position.Y + num2 * 0.5f;
					LanternSky.Lantern[] lanterns2 = this._lanterns;
					int num4 = i;
					lanterns2[num4].Position.Y = lanterns2[num4].Position.Y + this._lanterns[i].FloatAdjustedSpeed * 0.5f;
					LanternSky.Lantern[] lanterns3 = this._lanterns;
					int num5 = i;
					lanterns3[num5].Position.X = lanterns3[num5].Position.X + (0.1f + num) * (3f - this._lanterns[i].Speed) * 0.5f * ((float)i / (float)this._lanterns.Length + 1.5f) / 2.5f;
					this._lanterns[i].Rotation = num2 * (float)((num < 0f) ? (-1) : 1) * 0.5f;
					this._lanterns[i].TimeUntilFloat = Math.Max(0, this._lanterns[i].TimeUntilFloat - 1);
					if (this._lanterns[i].Position.Y < 300f)
					{
						if (!this._leaving)
						{
							this.ResetLantern(i);
							this._lanterns[i].Position = new Vector2((float)(this._random.Next(0, Main.maxTilesX) * 16), (float)Main.worldSurface * 16f + 1600f);
						}
						else
						{
							this._lanterns[i].Active = false;
							this._lanternsDrawing--;
						}
					}
				}
			}
			this._active = true;
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x005E4F10 File Offset: 0x005E3110
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.gameMenu && this._active)
			{
				this._active = false;
				this._leaving = false;
				for (int i = 0; i < this._lanterns.Length; i++)
				{
					this._lanterns[i].Active = false;
				}
			}
			if ((double)Main.screenPosition.Y > Main.worldSurface * 16.0 || Main.gameMenu)
			{
				return;
			}
			if (this._opacity <= 0f)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			for (int j = 0; j < this._lanterns.Length; j++)
			{
				float depth = this._lanterns[j].Depth;
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
				if (this._lanterns[k].Active)
				{
					Color color = new Color(250, 120, 60, 120);
					float num3 = 1f;
					if (this._lanterns[k].Depth > 5f)
					{
						num3 = 0.3f;
					}
					else if ((double)this._lanterns[k].Depth > 4.5)
					{
						num3 = 0.4f;
					}
					else if (this._lanterns[k].Depth > 4f)
					{
						num3 = 0.5f;
					}
					else if ((double)this._lanterns[k].Depth > 3.5)
					{
						num3 = 0.6f;
					}
					else if (this._lanterns[k].Depth > 3f)
					{
						num3 = 0.7f;
					}
					else if ((double)this._lanterns[k].Depth > 2.5)
					{
						num3 = 0.8f;
					}
					else if (this._lanterns[k].Depth > 2f)
					{
						num3 = 0.9f;
					}
					color = new Color((int)((float)color.R * num3), (int)((float)color.G * num3), (int)((float)color.B * num3), (int)((float)color.A * num3));
					Vector2 vector2 = new Vector2(1f / this._lanterns[k].Depth, 0.9f / this._lanterns[k].Depth);
					vector2 *= 1.2f;
					Vector2 vector3 = this._lanterns[k].Position;
					vector3 = (vector3 - vector) * vector2 + vector - Main.screenPosition;
					vector3.X = (vector3.X + 500f) % 4000f;
					if (vector3.X < 0f)
					{
						vector3.X += 4000f;
					}
					vector3.X -= 500f;
					if (rectangle.Contains((int)vector3.X, (int)vector3.Y))
					{
						this.DrawLantern(spriteBatch, this._lanterns[k], color, vector2, vector3, num3);
					}
				}
			}
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x005E52A0 File Offset: 0x005E34A0
		private void DrawLantern(SpriteBatch spriteBatch, LanternSky.Lantern lantern, Color opacity, Vector2 depthScale, Vector2 position, float alpha)
		{
			float y = (Main.GlobalTimeWrappedHourly % 6f / 6f * 6.2831855f).ToRotationVector2().Y;
			float num = y * 0.2f + 0.8f;
			Color color = new Color(255, 255, 255, 0) * this._opacity * alpha * num * 0.4f;
			for (float num2 = 0f; num2 < 1f; num2 += 0.33333334f)
			{
				Vector2 vector = new Vector2(0f, 2f).RotatedBy((double)(6.2831855f * num2 + lantern.Rotation), default(Vector2)) * y;
				spriteBatch.Draw(lantern.Texture, position + vector, new Rectangle?(lantern.GetSourceRectangle()), color, lantern.Rotation, lantern.GetSourceRectangle().Size() / 2f, depthScale.X * 2f, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(lantern.Texture, position, new Rectangle?(lantern.GetSourceRectangle()), opacity * this._opacity, lantern.Rotation, lantern.GetSourceRectangle().Size() / 2f, depthScale.X * 2f, SpriteEffects.None, 0f);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x005E5410 File Offset: 0x005E3610
		public override void Activate(Vector2 position, params object[] args)
		{
			if (this._active)
			{
				this._leaving = false;
				this.GenerateLanterns(true);
				return;
			}
			this.GenerateLanterns(false);
			this._active = true;
			this._leaving = false;
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x005E543E File Offset: 0x005E363E
		public override void Deactivate(params object[] args)
		{
			this._leaving = true;
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x005E5447 File Offset: 0x005E3647
		public override bool IsActive()
		{
			return this._active;
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x005E544F File Offset: 0x005E364F
		public override void Reset()
		{
			this._active = false;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x005E5458 File Offset: 0x005E3658
		public LanternSky()
		{
		}

		// Token: 0x040057D2 RID: 22482
		private bool _active;

		// Token: 0x040057D3 RID: 22483
		private bool _leaving;

		// Token: 0x040057D4 RID: 22484
		private float _opacity;

		// Token: 0x040057D5 RID: 22485
		private Asset<Texture2D> _texture;

		// Token: 0x040057D6 RID: 22486
		private LanternSky.Lantern[] _lanterns;

		// Token: 0x040057D7 RID: 22487
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040057D8 RID: 22488
		private int _lanternsDrawing;

		// Token: 0x040057D9 RID: 22489
		private const float slowDown = 0.5f;

		// Token: 0x02000961 RID: 2401
		private struct Lantern
		{
			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x060048C5 RID: 18629 RVA: 0x006D0021 File Offset: 0x006CE221
			// (set) Token: 0x060048C6 RID: 18630 RVA: 0x006D0029 File Offset: 0x006CE229
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
					this.FrameHeight = value.Height;
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x060048C7 RID: 18631 RVA: 0x006D004C File Offset: 0x006CE24C
			public float FloatAdjustedSpeed
			{
				get
				{
					return this.Speed * ((float)this.TimeUntilFloat / (float)this.TimeUntilFloatMax);
				}
			}

			// Token: 0x060048C8 RID: 18632 RVA: 0x006D0064 File Offset: 0x006CE264
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(this.FrameWidth * this.Variant, 0, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x04007599 RID: 30105
			private const int MAX_FRAMES_X = 3;

			// Token: 0x0400759A RID: 30106
			public int Variant;

			// Token: 0x0400759B RID: 30107
			public int TimeUntilFloat;

			// Token: 0x0400759C RID: 30108
			public int TimeUntilFloatMax;

			// Token: 0x0400759D RID: 30109
			private Texture2D _texture;

			// Token: 0x0400759E RID: 30110
			public Vector2 Position;

			// Token: 0x0400759F RID: 30111
			public float Depth;

			// Token: 0x040075A0 RID: 30112
			public float Rotation;

			// Token: 0x040075A1 RID: 30113
			public int FrameHeight;

			// Token: 0x040075A2 RID: 30114
			public int FrameWidth;

			// Token: 0x040075A3 RID: 30115
			public float Speed;

			// Token: 0x040075A4 RID: 30116
			public bool Active;
		}
	}
}
