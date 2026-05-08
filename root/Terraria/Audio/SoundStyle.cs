using System;
using Microsoft.Xna.Framework.Audio;
using Terraria.Utilities;

namespace Terraria.Audio
{
	// Token: 0x020005DD RID: 1501
	public abstract class SoundStyle
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x00659A48 File Offset: 0x00657C48
		public float Volume
		{
			get
			{
				return this._volume;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x00659A50 File Offset: 0x00657C50
		public float PitchVariance
		{
			get
			{
				return this._pitchVariance;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x00659A58 File Offset: 0x00657C58
		public SoundType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06003AE6 RID: 15078
		public abstract bool IsTrackable { get; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06003AE7 RID: 15079
		public abstract int MaxTrackedInstances { get; }

		// Token: 0x06003AE8 RID: 15080 RVA: 0x00659A60 File Offset: 0x00657C60
		public SoundStyle(float volume, float pitchVariance, SoundType type = SoundType.Sound)
		{
			this._volume = volume;
			this._pitchVariance = pitchVariance;
			this._type = type;
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x00659A7D File Offset: 0x00657C7D
		public SoundStyle(SoundType type = SoundType.Sound)
		{
			this._volume = 1f;
			this._pitchVariance = 0f;
			this._type = type;
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x00659AA2 File Offset: 0x00657CA2
		public float GetRandomPitch()
		{
			return SoundStyle._random.NextFloat() * this.PitchVariance - this.PitchVariance * 0.5f;
		}

		// Token: 0x06003AEB RID: 15083
		public abstract SoundEffect GetRandomSound();

		// Token: 0x06003AEC RID: 15084 RVA: 0x00659AC2 File Offset: 0x00657CC2
		// Note: this type is marked as 'beforefieldinit'.
		static SoundStyle()
		{
		}

		// Token: 0x04005E4F RID: 24143
		private static UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04005E50 RID: 24144
		private float _volume;

		// Token: 0x04005E51 RID: 24145
		private float _pitchVariance;

		// Token: 0x04005E52 RID: 24146
		private SoundType _type;
	}
}
