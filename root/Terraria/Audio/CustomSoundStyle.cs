using System;
using Microsoft.Xna.Framework.Audio;
using Terraria.Utilities;

namespace Terraria.Audio
{
	// Token: 0x020005CF RID: 1487
	public class CustomSoundStyle : SoundStyle
	{
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000379E9 File Offset: 0x00035BE9
		public override bool IsTrackable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public override int MaxTrackedInstances
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x00655505 File Offset: 0x00653705
		public CustomSoundStyle(SoundEffect soundEffect, SoundType type = SoundType.Sound, float volume = 1f, float pitchVariance = 0f)
			: base(volume, pitchVariance, type)
		{
			this._soundEffects = new SoundEffect[] { soundEffect };
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x00655521 File Offset: 0x00653721
		public CustomSoundStyle(SoundEffect[] soundEffects, SoundType type = SoundType.Sound, float volume = 1f, float pitchVariance = 0f)
			: base(volume, pitchVariance, type)
		{
			this._soundEffects = soundEffects;
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x00655534 File Offset: 0x00653734
		public override SoundEffect GetRandomSound()
		{
			return this._soundEffects[CustomSoundStyle.Random.Next(this._soundEffects.Length)];
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x0065554F File Offset: 0x0065374F
		// Note: this type is marked as 'beforefieldinit'.
		static CustomSoundStyle()
		{
		}

		// Token: 0x04005DE2 RID: 24034
		private static readonly UnifiedRandom Random = new UnifiedRandom();

		// Token: 0x04005DE3 RID: 24035
		private readonly SoundEffect[] _soundEffects;
	}
}
