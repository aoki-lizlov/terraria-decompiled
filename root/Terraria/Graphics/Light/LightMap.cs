using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.Utilities;

namespace Terraria.Graphics.Light
{
	// Token: 0x020001FE RID: 510
	public class LightMap
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x0052C37F File Offset: 0x0052A57F
		// (set) Token: 0x06002106 RID: 8454 RVA: 0x0052C387 File Offset: 0x0052A587
		public int NonVisiblePadding
		{
			[CompilerGenerated]
			get
			{
				return this.<NonVisiblePadding>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NonVisiblePadding>k__BackingField = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x0052C390 File Offset: 0x0052A590
		// (set) Token: 0x06002108 RID: 8456 RVA: 0x0052C398 File Offset: 0x0052A598
		public int Width
		{
			[CompilerGenerated]
			get
			{
				return this.<Width>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Width>k__BackingField = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x0052C3A1 File Offset: 0x0052A5A1
		// (set) Token: 0x0600210A RID: 8458 RVA: 0x0052C3A9 File Offset: 0x0052A5A9
		public int Height
		{
			[CompilerGenerated]
			get
			{
				return this.<Height>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Height>k__BackingField = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x0052C3B2 File Offset: 0x0052A5B2
		// (set) Token: 0x0600210C RID: 8460 RVA: 0x0052C3BA File Offset: 0x0052A5BA
		public float LightDecayThroughAir
		{
			[CompilerGenerated]
			get
			{
				return this.<LightDecayThroughAir>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LightDecayThroughAir>k__BackingField = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x0052C3C3 File Offset: 0x0052A5C3
		// (set) Token: 0x0600210E RID: 8462 RVA: 0x0052C3CB File Offset: 0x0052A5CB
		public float LightDecayThroughSolid
		{
			[CompilerGenerated]
			get
			{
				return this.<LightDecayThroughSolid>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LightDecayThroughSolid>k__BackingField = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x0052C3D4 File Offset: 0x0052A5D4
		// (set) Token: 0x06002110 RID: 8464 RVA: 0x0052C3DC File Offset: 0x0052A5DC
		public float LightDecayThroughCrackedBrick
		{
			[CompilerGenerated]
			get
			{
				return this.<LightDecayThroughCrackedBrick>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LightDecayThroughCrackedBrick>k__BackingField = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x0052C3E5 File Offset: 0x0052A5E5
		// (set) Token: 0x06002112 RID: 8466 RVA: 0x0052C3ED File Offset: 0x0052A5ED
		public Vector3 LightDecayThroughWater
		{
			[CompilerGenerated]
			get
			{
				return this.<LightDecayThroughWater>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LightDecayThroughWater>k__BackingField = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x0052C3F6 File Offset: 0x0052A5F6
		// (set) Token: 0x06002114 RID: 8468 RVA: 0x0052C3FE File Offset: 0x0052A5FE
		public Vector3 LightDecayThroughHoney
		{
			[CompilerGenerated]
			get
			{
				return this.<LightDecayThroughHoney>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LightDecayThroughHoney>k__BackingField = value;
			}
		}

		// Token: 0x1700033D RID: 829
		public Vector3 this[int x, int y]
		{
			get
			{
				return this._colors[this.IndexOf(x, y)];
			}
			set
			{
				this._colors[this.IndexOf(x, y)] = value;
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0052C434 File Offset: 0x0052A634
		public LightMap()
		{
			this.LightDecayThroughAir = 0.91f;
			this.LightDecayThroughSolid = 0.56f;
			this.LightDecayThroughCrackedBrick = 0.8f;
			this.LightDecayThroughWater = new Vector3(0.88f, 0.96f, 1.015f) * 0.91f;
			this.LightDecayThroughHoney = new Vector3(0.75f, 0.7f, 0.6f) * 0.91f;
			this.Width = 203;
			this.Height = 203;
			this._colors = new Vector3[41209];
			this._mask = new LightMaskMode[41209];
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0052C4F1 File Offset: 0x0052A6F1
		public void GetLight(int x, int y, out Vector3 color)
		{
			color = this._colors[this.IndexOf(x, y)];
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0052C50C File Offset: 0x0052A70C
		public LightMaskMode GetMask(int x, int y)
		{
			return this._mask[this.IndexOf(x, y)];
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0052C520 File Offset: 0x0052A720
		public void Clear()
		{
			for (int i = 0; i < this._colors.Length; i++)
			{
				this._colors[i].X = 0f;
				this._colors[i].Y = 0f;
				this._colors[i].Z = 0f;
				this._mask[i] = LightMaskMode.None;
			}
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0052C58B File Offset: 0x0052A78B
		public void SetMaskAt(int x, int y, LightMaskMode mode)
		{
			this._mask[this.IndexOf(x, y)] = mode;
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0052C59D File Offset: 0x0052A79D
		public void Blur()
		{
			this.BlurPass();
			this.BlurPass();
			this._random.NextSeed();
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0052C5B6 File Offset: 0x0052A7B6
		private void BlurPass()
		{
			FastParallel.For(0, this.Width, delegate(int start, int end, object context)
			{
				for (int i = start; i < end; i++)
				{
					this.BlurLine(this.IndexOf(i, 0), this.IndexOf(i, this.Height - 1 - this.NonVisiblePadding), 1);
					this.BlurLine(this.IndexOf(i, this.Height - 1), this.IndexOf(i, this.NonVisiblePadding), -1);
				}
			}, null);
			FastParallel.For(0, this.Height, delegate(int start, int end, object context)
			{
				for (int j = start; j < end; j++)
				{
					this.BlurLine(this.IndexOf(0, j), this.IndexOf(this.Width - 1 - this.NonVisiblePadding, j), this.Height);
					this.BlurLine(this.IndexOf(this.Width - 1, j), this.IndexOf(this.NonVisiblePadding, j), -this.Height);
				}
			}, null);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0052C5EC File Offset: 0x0052A7EC
		private void BlurLine(int startIndex, int endIndex, int stride)
		{
			Vector3 zero = Vector3.Zero;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int num = startIndex; num != endIndex + stride; num += stride)
			{
				if (zero.X < this._colors[num].X)
				{
					zero.X = this._colors[num].X;
					flag = false;
				}
				else if (!flag)
				{
					if (zero.X < 0.0185f)
					{
						flag = true;
					}
					else
					{
						this._colors[num].X = zero.X;
					}
				}
				if (zero.Y < this._colors[num].Y)
				{
					zero.Y = this._colors[num].Y;
					flag2 = false;
				}
				else if (!flag2)
				{
					if (zero.Y < 0.0185f)
					{
						flag2 = true;
					}
					else
					{
						this._colors[num].Y = zero.Y;
					}
				}
				if (zero.Z < this._colors[num].Z)
				{
					zero.Z = this._colors[num].Z;
					flag3 = false;
				}
				else if (!flag3)
				{
					if (zero.Z < 0.0185f)
					{
						flag3 = true;
					}
					else
					{
						this._colors[num].Z = zero.Z;
					}
				}
				if (!flag || !flag3 || !flag2)
				{
					switch (this._mask[num])
					{
					case LightMaskMode.None:
						if (!flag)
						{
							zero.X *= this.LightDecayThroughAir;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughAir;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughAir;
						}
						break;
					case LightMaskMode.Solid:
						if (!flag)
						{
							zero.X *= this.LightDecayThroughSolid;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughSolid;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughSolid;
						}
						break;
					case LightMaskMode.Water:
					{
						float num2 = (float)this._random.WithModifier((ulong)((long)num)).Next(98, 100) / 100f;
						if (!flag)
						{
							zero.X *= this.LightDecayThroughWater.X * num2;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughWater.Y * num2;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughWater.Z * num2;
						}
						break;
					}
					case LightMaskMode.Honey:
						if (!flag)
						{
							zero.X *= this.LightDecayThroughHoney.X;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughHoney.Y;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughHoney.Z;
						}
						break;
					case LightMaskMode.CrackedBricks:
						if (!flag)
						{
							zero.X *= this.LightDecayThroughCrackedBrick;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughCrackedBrick;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughCrackedBrick;
						}
						break;
					}
				}
			}
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0052C90C File Offset: 0x0052AB0C
		private int IndexOf(int x, int y)
		{
			return x * this.Height + y;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0052C918 File Offset: 0x0052AB18
		public void SetSize(int width, int height)
		{
			if (width * height > this._colors.Length)
			{
				this._colors = new Vector3[width * height];
				this._mask = new LightMaskMode[width * height];
			}
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0052C954 File Offset: 0x0052AB54
		[CompilerGenerated]
		private void <BlurPass>b__46_0(int start, int end, object context)
		{
			for (int i = start; i < end; i++)
			{
				this.BlurLine(this.IndexOf(i, 0), this.IndexOf(i, this.Height - 1 - this.NonVisiblePadding), 1);
				this.BlurLine(this.IndexOf(i, this.Height - 1), this.IndexOf(i, this.NonVisiblePadding), -1);
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0052C9B8 File Offset: 0x0052ABB8
		[CompilerGenerated]
		private void <BlurPass>b__46_1(int start, int end, object context)
		{
			for (int i = start; i < end; i++)
			{
				this.BlurLine(this.IndexOf(0, i), this.IndexOf(this.Width - 1 - this.NonVisiblePadding, i), this.Height);
				this.BlurLine(this.IndexOf(this.Width - 1, i), this.IndexOf(this.NonVisiblePadding, i), -this.Height);
			}
		}

		// Token: 0x04004B7E RID: 19326
		[CompilerGenerated]
		private int <NonVisiblePadding>k__BackingField;

		// Token: 0x04004B7F RID: 19327
		[CompilerGenerated]
		private int <Width>k__BackingField;

		// Token: 0x04004B80 RID: 19328
		[CompilerGenerated]
		private int <Height>k__BackingField;

		// Token: 0x04004B81 RID: 19329
		private Vector3[] _colors;

		// Token: 0x04004B82 RID: 19330
		private LightMaskMode[] _mask;

		// Token: 0x04004B83 RID: 19331
		[CompilerGenerated]
		private float <LightDecayThroughAir>k__BackingField;

		// Token: 0x04004B84 RID: 19332
		[CompilerGenerated]
		private float <LightDecayThroughSolid>k__BackingField;

		// Token: 0x04004B85 RID: 19333
		[CompilerGenerated]
		private float <LightDecayThroughCrackedBrick>k__BackingField;

		// Token: 0x04004B86 RID: 19334
		[CompilerGenerated]
		private Vector3 <LightDecayThroughWater>k__BackingField;

		// Token: 0x04004B87 RID: 19335
		[CompilerGenerated]
		private Vector3 <LightDecayThroughHoney>k__BackingField;

		// Token: 0x04004B88 RID: 19336
		private FastRandom _random = FastRandom.CreateWithRandomSeed();

		// Token: 0x04004B89 RID: 19337
		private const int DEFAULT_WIDTH = 203;

		// Token: 0x04004B8A RID: 19338
		private const int DEFAULT_HEIGHT = 203;
	}
}
