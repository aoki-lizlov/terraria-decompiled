using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000454 RID: 1108
	public class MartianSky : CustomSky
	{
		// Token: 0x06003247 RID: 12871 RVA: 0x005E7DD0 File Offset: 0x005E5FD0
		public override void Update(GameTime gameTime)
		{
			if (FocusHelper.PauseSkies)
			{
				return;
			}
			int num = this._activeUfos;
			for (int i = 0; i < this._ufos.Length; i++)
			{
				MartianSky.Ufo ufo = this._ufos[i];
				if (ufo.IsActive)
				{
					int frame = ufo.Frame;
					ufo.Frame = frame + 1;
					if (!ufo.Update())
					{
						if (!this._leaving)
						{
							ufo.AssignNewBehavior();
						}
						else
						{
							ufo.IsActive = false;
							num--;
						}
					}
				}
				this._ufos[i] = ufo;
			}
			if (!this._leaving && num != this._maxUfos)
			{
				this._ufos[num].IsActive = true;
				this._ufos[num++].AssignNewBehavior();
			}
			this._active = !this._leaving || num != 0;
			this._activeUfos = num;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x005E7EAC File Offset: 0x005E60AC
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.screenPosition.Y > 10000f)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < this._ufos.Length; i++)
			{
				float depth = this._ufos[i].Depth;
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
			Color color = new Color(Main.ColorOfTheSkies.ToVector4() * 0.9f + new Vector4(0.1f));
			Vector2 vector = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
			Rectangle rectangle = new Rectangle(-1000, -1000, Main.screenWidth + 1000, Main.screenHeight + 1000);
			for (int j = num; j < num2; j++)
			{
				Vector2 vector2 = new Vector2(1f / this._ufos[j].Depth, 0.9f / this._ufos[j].Depth);
				Vector2 vector3 = this._ufos[j].Position;
				vector3 = (vector3 - vector) * vector2 + vector - Main.screenPosition;
				if (this._ufos[j].IsActive && rectangle.Contains((int)vector3.X, (int)vector3.Y))
				{
					spriteBatch.Draw(this._ufos[j].Texture, vector3, new Rectangle?(this._ufos[j].GetSourceRectangle()), color * this._ufos[j].Opacity, this._ufos[j].Rotation, Vector2.Zero, vector2.X * 5f * this._ufos[j].Scale, SpriteEffects.None, 0f);
					if (this._ufos[j].GlowTexture != null)
					{
						spriteBatch.Draw(this._ufos[j].GlowTexture, vector3, new Rectangle?(this._ufos[j].GetSourceRectangle()), Color.White * this._ufos[j].Opacity, this._ufos[j].Rotation, Vector2.Zero, vector2.X * 5f * this._ufos[j].Scale, SpriteEffects.None, 0f);
					}
				}
			}
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x005E8160 File Offset: 0x005E6360
		private void GenerateUfos()
		{
			float num = (float)Main.maxTilesX / 4200f;
			this._maxUfos = (int)(256f * num);
			this._ufos = new MartianSky.Ufo[this._maxUfos];
			int num2 = this._maxUfos >> 4;
			for (int i = 0; i < num2; i++)
			{
				float num3 = (float)i / (float)num2;
				this._ufos[i] = new MartianSky.Ufo(TextureAssets.Extra[5].Value, (float)Main.rand.NextDouble() * 4f + 6.6f);
				this._ufos[i].GlowTexture = TextureAssets.GlowMask[90].Value;
			}
			for (int j = num2; j < this._ufos.Length; j++)
			{
				float num4 = (float)(j - num2) / (float)(this._ufos.Length - num2);
				this._ufos[j] = new MartianSky.Ufo(TextureAssets.Extra[6].Value, (float)Main.rand.NextDouble() * 5f + 1.6f);
				this._ufos[j].Scale = 0.5f;
				this._ufos[j].GlowTexture = TextureAssets.GlowMask[91].Value;
			}
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x005E8298 File Offset: 0x005E6498
		public override void Activate(Vector2 position, params object[] args)
		{
			this._activeUfos = 0;
			this.GenerateUfos();
			Array.Sort<MartianSky.Ufo>(this._ufos, (MartianSky.Ufo ufo1, MartianSky.Ufo ufo2) => ufo2.Depth.CompareTo(ufo1.Depth));
			this._active = true;
			this._leaving = false;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x005E82EA File Offset: 0x005E64EA
		public override void Deactivate(params object[] args)
		{
			this._leaving = true;
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x005E82F3 File Offset: 0x005E64F3
		public override bool IsActive()
		{
			return this._active;
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x005E82FB File Offset: 0x005E64FB
		public override void Reset()
		{
			this._active = false;
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x005E8304 File Offset: 0x005E6504
		public MartianSky()
		{
		}

		// Token: 0x0400580A RID: 22538
		private MartianSky.Ufo[] _ufos;

		// Token: 0x0400580B RID: 22539
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x0400580C RID: 22540
		private int _maxUfos;

		// Token: 0x0400580D RID: 22541
		private bool _active;

		// Token: 0x0400580E RID: 22542
		private bool _leaving;

		// Token: 0x0400580F RID: 22543
		private int _activeUfos;

		// Token: 0x02000967 RID: 2407
		private abstract class IUfoController
		{
			// Token: 0x060048D3 RID: 18643
			public abstract void InitializeUfo(ref MartianSky.Ufo ufo);

			// Token: 0x060048D4 RID: 18644
			public abstract bool Update(ref MartianSky.Ufo ufo);

			// Token: 0x060048D5 RID: 18645 RVA: 0x0000357B File Offset: 0x0000177B
			protected IUfoController()
			{
			}
		}

		// Token: 0x02000968 RID: 2408
		private class ZipBehavior : MartianSky.IUfoController
		{
			// Token: 0x060048D6 RID: 18646 RVA: 0x006D0158 File Offset: 0x006CE358
			public override void InitializeUfo(ref MartianSky.Ufo ufo)
			{
				ufo.Position.X = (float)(MartianSky.Ufo.Random.NextDouble() * (double)(Main.maxTilesX << 4));
				ufo.Position.Y = (float)(MartianSky.Ufo.Random.NextDouble() * 5000.0);
				ufo.Opacity = 0f;
				float num = (float)MartianSky.Ufo.Random.NextDouble() * 5f + 10f;
				double num2 = MartianSky.Ufo.Random.NextDouble() * 0.6000000238418579 - 0.30000001192092896;
				ufo.Rotation = (float)num2;
				if (MartianSky.Ufo.Random.Next(2) == 0)
				{
					num2 += 3.1415927410125732;
				}
				this._speed = new Vector2((float)Math.Cos(num2) * num, (float)Math.Sin(num2) * num);
				this._ticks = 0;
				this._maxTicks = MartianSky.Ufo.Random.Next(400, 500);
			}

			// Token: 0x060048D7 RID: 18647 RVA: 0x006D0248 File Offset: 0x006CE448
			public override bool Update(ref MartianSky.Ufo ufo)
			{
				if (this._ticks < 10)
				{
					ufo.Opacity += 0.1f;
				}
				else if (this._ticks > this._maxTicks - 10)
				{
					ufo.Opacity -= 0.1f;
				}
				ufo.Position += this._speed;
				if (this._ticks == this._maxTicks)
				{
					return false;
				}
				this._ticks++;
				return true;
			}

			// Token: 0x060048D8 RID: 18648 RVA: 0x006D02CB File Offset: 0x006CE4CB
			public ZipBehavior()
			{
			}

			// Token: 0x040075CA RID: 30154
			private Vector2 _speed;

			// Token: 0x040075CB RID: 30155
			private int _ticks;

			// Token: 0x040075CC RID: 30156
			private int _maxTicks;
		}

		// Token: 0x02000969 RID: 2409
		private class HoverBehavior : MartianSky.IUfoController
		{
			// Token: 0x060048D9 RID: 18649 RVA: 0x006D02D4 File Offset: 0x006CE4D4
			public override void InitializeUfo(ref MartianSky.Ufo ufo)
			{
				ufo.Position.X = (float)(MartianSky.Ufo.Random.NextDouble() * (double)(Main.maxTilesX << 4));
				ufo.Position.Y = (float)(MartianSky.Ufo.Random.NextDouble() * 5000.0);
				ufo.Opacity = 0f;
				ufo.Rotation = 0f;
				this._ticks = 0;
				this._maxTicks = MartianSky.Ufo.Random.Next(120, 240);
			}

			// Token: 0x060048DA RID: 18650 RVA: 0x006D0354 File Offset: 0x006CE554
			public override bool Update(ref MartianSky.Ufo ufo)
			{
				if (this._ticks < 10)
				{
					ufo.Opacity += 0.1f;
				}
				else if (this._ticks > this._maxTicks - 10)
				{
					ufo.Opacity -= 0.1f;
				}
				if (this._ticks == this._maxTicks)
				{
					return false;
				}
				this._ticks++;
				return true;
			}

			// Token: 0x060048DB RID: 18651 RVA: 0x006D02CB File Offset: 0x006CE4CB
			public HoverBehavior()
			{
			}

			// Token: 0x040075CD RID: 30157
			private int _ticks;

			// Token: 0x040075CE RID: 30158
			private int _maxTicks;
		}

		// Token: 0x0200096A RID: 2410
		private struct Ufo
		{
			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x060048DC RID: 18652 RVA: 0x006D03BB File Offset: 0x006CE5BB
			// (set) Token: 0x060048DD RID: 18653 RVA: 0x006D03C3 File Offset: 0x006CE5C3
			public int Frame
			{
				get
				{
					return this._frame;
				}
				set
				{
					this._frame = value % 12;
				}
			}

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x060048DE RID: 18654 RVA: 0x006D03CF File Offset: 0x006CE5CF
			// (set) Token: 0x060048DF RID: 18655 RVA: 0x006D03D7 File Offset: 0x006CE5D7
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
					this.FrameHeight = value.Height / 3;
				}
			}

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x060048E0 RID: 18656 RVA: 0x006D03FA File Offset: 0x006CE5FA
			// (set) Token: 0x060048E1 RID: 18657 RVA: 0x006D0402 File Offset: 0x006CE602
			public MartianSky.IUfoController Controller
			{
				get
				{
					return this._controller;
				}
				set
				{
					this._controller = value;
					value.InitializeUfo(ref this);
				}
			}

			// Token: 0x060048E2 RID: 18658 RVA: 0x006D0414 File Offset: 0x006CE614
			public Ufo(Texture2D texture, float depth = 1f)
			{
				this._frame = 0;
				this.Position = Vector2.Zero;
				this._texture = texture;
				this.Depth = depth;
				this.Scale = 1f;
				this.FrameWidth = texture.Width;
				this.FrameHeight = texture.Height / 3;
				this.GlowTexture = null;
				this.Opacity = 0f;
				this.Rotation = 0f;
				this.IsActive = false;
				this._controller = null;
			}

			// Token: 0x060048E3 RID: 18659 RVA: 0x006D0491 File Offset: 0x006CE691
			public Rectangle GetSourceRectangle()
			{
				return new Rectangle(0, this._frame / 4 * this.FrameHeight, this.FrameWidth, this.FrameHeight);
			}

			// Token: 0x060048E4 RID: 18660 RVA: 0x006D04B4 File Offset: 0x006CE6B4
			public bool Update()
			{
				return this.Controller.Update(ref this);
			}

			// Token: 0x060048E5 RID: 18661 RVA: 0x006D04C4 File Offset: 0x006CE6C4
			public void AssignNewBehavior()
			{
				int num = MartianSky.Ufo.Random.Next(2);
				if (num == 0)
				{
					this.Controller = new MartianSky.ZipBehavior();
					return;
				}
				if (num != 1)
				{
					return;
				}
				this.Controller = new MartianSky.HoverBehavior();
			}

			// Token: 0x060048E6 RID: 18662 RVA: 0x006D04FC File Offset: 0x006CE6FC
			// Note: this type is marked as 'beforefieldinit'.
			static Ufo()
			{
			}

			// Token: 0x040075CF RID: 30159
			private const int MAX_FRAMES = 3;

			// Token: 0x040075D0 RID: 30160
			private const int FRAME_RATE = 4;

			// Token: 0x040075D1 RID: 30161
			public static UnifiedRandom Random = new UnifiedRandom();

			// Token: 0x040075D2 RID: 30162
			private int _frame;

			// Token: 0x040075D3 RID: 30163
			private Texture2D _texture;

			// Token: 0x040075D4 RID: 30164
			private MartianSky.IUfoController _controller;

			// Token: 0x040075D5 RID: 30165
			public Texture2D GlowTexture;

			// Token: 0x040075D6 RID: 30166
			public Vector2 Position;

			// Token: 0x040075D7 RID: 30167
			public int FrameHeight;

			// Token: 0x040075D8 RID: 30168
			public int FrameWidth;

			// Token: 0x040075D9 RID: 30169
			public float Depth;

			// Token: 0x040075DA RID: 30170
			public float Scale;

			// Token: 0x040075DB RID: 30171
			public float Opacity;

			// Token: 0x040075DC RID: 30172
			public bool IsActive;

			// Token: 0x040075DD RID: 30173
			public float Rotation;
		}

		// Token: 0x0200096B RID: 2411
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060048E7 RID: 18663 RVA: 0x006D0508 File Offset: 0x006CE708
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060048E8 RID: 18664 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060048E9 RID: 18665 RVA: 0x006D0514 File Offset: 0x006CE714
			internal int <Activate>b__13_0(MartianSky.Ufo ufo1, MartianSky.Ufo ufo2)
			{
				return ufo2.Depth.CompareTo(ufo1.Depth);
			}

			// Token: 0x040075DE RID: 30174
			public static readonly MartianSky.<>c <>9 = new MartianSky.<>c();

			// Token: 0x040075DF RID: 30175
			public static Comparison<MartianSky.Ufo> <>9__13_0;
		}
	}
}
