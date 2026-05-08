using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000022 RID: 34
	public class RgbKey
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000047CF File Offset: 0x000029CF
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x000047D7 File Offset: 0x000029D7
		public int CurrentIntegerRepresentation
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentIntegerRepresentation>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CurrentIntegerRepresentation>k__BackingField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000047E0 File Offset: 0x000029E0
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x000047E8 File Offset: 0x000029E8
		public RgbKeyEffect Effect
		{
			[CompilerGenerated]
			get
			{
				return this.<Effect>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Effect>k__BackingField = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000047F1 File Offset: 0x000029F1
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000047F9 File Offset: 0x000029F9
		public Color BaseColor
		{
			[CompilerGenerated]
			get
			{
				return this.<BaseColor>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BaseColor>k__BackingField = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004802 File Offset: 0x00002A02
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000480A File Offset: 0x00002A0A
		public Color TargetColor
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetColor>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TargetColor>k__BackingField = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004813 File Offset: 0x00002A13
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000481B File Offset: 0x00002A1B
		public Color CurrentColor
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentColor>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CurrentColor>k__BackingField = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004824 File Offset: 0x00002A24
		public bool IsVisible
		{
			get
			{
				return this.Effect != RgbKeyEffect.Clear;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004834 File Offset: 0x00002A34
		public RgbKey(Keys key, string keyTriggerName)
		{
			this.Key = key;
			this.KeyTriggerName = keyTriggerName;
			this.BaseColor = Color.White;
			this.TargetColor = Color.White;
			this.CurrentColor = Color.White;
			this.Effect = RgbKeyEffect.Clear;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004893 File Offset: 0x00002A93
		public void SetIntegerRepresentation(int integerValue)
		{
			this.CurrentIntegerRepresentation = integerValue;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000489C File Offset: 0x00002A9C
		public void FadeTo(Color targetColor, float time)
		{
			this.TargetColor = targetColor;
			this._timeRemaining = time;
			this._totalTime = time;
			this.Effect = RgbKeyEffect.Fade;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000048BA File Offset: 0x00002ABA
		public void SetFlashing(Color flashColor, float time, float flashesPerSecond = 1f)
		{
			this.TargetColor = flashColor;
			this._timeRemaining = time;
			this._totalTime = time;
			this._effectRate = flashesPerSecond;
			this.Effect = RgbKeyEffect.Flashing;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000048DF File Offset: 0x00002ADF
		public void SetFlashing(Color baseColor, Color flashColor, float time, float flashesPerSecond = 1f)
		{
			this.SetBaseColor(baseColor);
			this.SetFlashing(flashColor, time, flashesPerSecond);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000048F2 File Offset: 0x00002AF2
		public void SetBaseColor(Color color)
		{
			this.BaseColor = color;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000048FB File Offset: 0x00002AFB
		public void SetTargetColor(Color color)
		{
			this.TargetColor = color;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004904 File Offset: 0x00002B04
		public void SetSolid()
		{
			this.Effect = RgbKeyEffect.Solid;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000490D File Offset: 0x00002B0D
		public void SetSolid(Color color)
		{
			this.BaseColor = color;
			this.Effect = RgbKeyEffect.Solid;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000491D File Offset: 0x00002B1D
		public void Clear()
		{
			this.Effect = RgbKeyEffect.Clear;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004928 File Offset: 0x00002B28
		internal void Update(float timeElapsed)
		{
			switch (this.Effect)
			{
			case RgbKeyEffect.Solid:
				this.UpdateSolidEffect();
				break;
			case RgbKeyEffect.Flashing:
				this.UpdateFlashingEffect();
				break;
			case RgbKeyEffect.Fade:
				this.UpdateFadeEffect();
				break;
			}
			this._timeRemaining = Math.Max(this._timeRemaining - timeElapsed, 0f);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000497E File Offset: 0x00002B7E
		private void UpdateSolidEffect()
		{
			this.CurrentColor = this.BaseColor;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000498C File Offset: 0x00002B8C
		private void UpdateFadeEffect()
		{
			float num = 0f;
			if (this._totalTime > 0f)
			{
				num = 1f - this._timeRemaining / this._totalTime;
			}
			this.CurrentColor = Color.Lerp(this.BaseColor, this.TargetColor, num);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000049D8 File Offset: 0x00002BD8
		private void UpdateFlashingEffect()
		{
			float num = (float)Math.Sin((double)(this._timeRemaining * this._effectRate * 6.2831855f)) * 0.5f + 0.5f;
			this.CurrentColor = Color.Lerp(this.BaseColor, this.TargetColor, num);
		}

		// Token: 0x04000052 RID: 82
		public readonly Keys Key;

		// Token: 0x04000053 RID: 83
		public readonly string KeyTriggerName;

		// Token: 0x04000054 RID: 84
		[CompilerGenerated]
		private int <CurrentIntegerRepresentation>k__BackingField;

		// Token: 0x04000055 RID: 85
		[CompilerGenerated]
		private RgbKeyEffect <Effect>k__BackingField;

		// Token: 0x04000056 RID: 86
		[CompilerGenerated]
		private Color <BaseColor>k__BackingField;

		// Token: 0x04000057 RID: 87
		[CompilerGenerated]
		private Color <TargetColor>k__BackingField;

		// Token: 0x04000058 RID: 88
		[CompilerGenerated]
		private Color <CurrentColor>k__BackingField;

		// Token: 0x04000059 RID: 89
		private float _timeRemaining;

		// Token: 0x0400005A RID: 90
		private float _totalTime = 1f;

		// Token: 0x0400005B RID: 91
		private float _effectRate = 1f;
	}
}
