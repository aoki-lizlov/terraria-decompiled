using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x02000015 RID: 21
	public class Equalizer
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00004924 File Offset: 0x00002B24
		internal Equalizer()
		{
			this.InitBlock();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004932 File Offset: 0x00002B32
		internal Equalizer(float[] settings)
		{
			this.InitBlock();
			this.FromFloatArray = settings;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004947 File Offset: 0x00002B47
		internal Equalizer(Equalizer.EQFunction eq)
		{
			this.InitBlock();
			this.FromEQFunction = eq;
		}

		// Token: 0x17000017 RID: 23
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000495C File Offset: 0x00002B5C
		internal float[] FromFloatArray
		{
			set
			{
				this.Reset();
				int num = ((value.Length > 32) ? 32 : value.Length);
				for (int i = 0; i < num; i++)
				{
					this._Settings[i] = this.Limit(value[i]);
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000499B File Offset: 0x00002B9B
		internal virtual Equalizer FromEqualizer
		{
			set
			{
				if (value != this)
				{
					this.FromFloatArray = value._Settings;
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000049B0 File Offset: 0x00002BB0
		internal Equalizer.EQFunction FromEQFunction
		{
			set
			{
				this.Reset();
				int num = 32;
				for (int i = 0; i < num; i++)
				{
					this._Settings[i] = this.Limit(value.GetBand(i));
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000049E7 File Offset: 0x00002BE7
		internal virtual int BandCount
		{
			get
			{
				return this._Settings.Length;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000049F4 File Offset: 0x00002BF4
		internal virtual float[] BandFactors
		{
			get
			{
				float[] array = new float[32];
				for (int i = 0; i < 32; i++)
				{
					array[i] = this.GetBandFactor(this._Settings[i]);
				}
				return array;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004A28 File Offset: 0x00002C28
		private void InitBlock()
		{
			this._Settings = new float[32];
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004A38 File Offset: 0x00002C38
		internal void Reset()
		{
			for (int i = 0; i < 32; i++)
			{
				this._Settings[i] = 0f;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004A60 File Offset: 0x00002C60
		internal float SetBand(int band, float neweq)
		{
			float num = 0f;
			if (band >= 0 && band < 32)
			{
				num = this._Settings[band];
				this._Settings[band] = this.Limit(neweq);
			}
			return num;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004A98 File Offset: 0x00002C98
		internal float GetBand(int band)
		{
			float num = 0f;
			if (band >= 0 && band < 32)
			{
				num = this._Settings[band];
			}
			return num;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004ABE File Offset: 0x00002CBE
		private float Limit(float eq)
		{
			if (eq == float.NegativeInfinity)
			{
				return eq;
			}
			if (eq > 1f)
			{
				return 1f;
			}
			if (eq < -1f)
			{
				return -1f;
			}
			return eq;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004AE7 File Offset: 0x00002CE7
		internal float GetBandFactor(float eq)
		{
			if (eq == float.NegativeInfinity)
			{
				return 0f;
			}
			return (float)Math.Pow(2.0, (double)eq);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004B08 File Offset: 0x00002D08
		// Note: this type is marked as 'beforefieldinit'.
		static Equalizer()
		{
		}

		// Token: 0x0400005E RID: 94
		private const int BANDS = 32;

		// Token: 0x0400005F RID: 95
		internal const float BAND_NOT_PRESENT = float.NegativeInfinity;

		// Token: 0x04000060 RID: 96
		internal static readonly Equalizer PassThruEq = new Equalizer();

		// Token: 0x04000061 RID: 97
		private float[] _Settings;

		// Token: 0x02000034 RID: 52
		internal abstract class EQFunction
		{
			// Token: 0x0600017B RID: 379 RVA: 0x0001B7B8 File Offset: 0x000199B8
			internal virtual float GetBand(int band)
			{
				return 0f;
			}

			// Token: 0x0600017C RID: 380 RVA: 0x0001B7BF File Offset: 0x000199BF
			protected EQFunction()
			{
			}
		}
	}
}
