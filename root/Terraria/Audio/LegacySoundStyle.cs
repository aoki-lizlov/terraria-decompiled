using System;
using Microsoft.Xna.Framework.Audio;
using Terraria.Utilities;

namespace Terraria.Audio
{
	// Token: 0x020005D6 RID: 1494
	public class LegacySoundStyle : SoundStyle
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x00658FE4 File Offset: 0x006571E4
		public int Style
		{
			get
			{
				if (this.Variations != 1)
				{
					return LegacySoundStyle.Random.Next(this._style, this._style + this.Variations);
				}
				return this._style;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x00659013 File Offset: 0x00657213
		public override bool IsTrackable
		{
			get
			{
				return this.SoundId == 42;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x0065901F File Offset: 0x0065721F
		public override int MaxTrackedInstances
		{
			get
			{
				return this._maxTrackedInstances;
			}
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x00659027 File Offset: 0x00657227
		public LegacySoundStyle(int soundId, int style, SoundType type = SoundType.Sound, int maxTrackedInstances = 0)
			: base(type)
		{
			this._style = style;
			this.Variations = 1;
			this.SoundId = soundId;
			this._maxTrackedInstances = maxTrackedInstances;
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x0065904D File Offset: 0x0065724D
		public LegacySoundStyle(int soundId, int style, int variations, SoundType type = SoundType.Sound, int maxTrackedInstances = 0)
			: base(type)
		{
			this._style = style;
			this.Variations = variations;
			this.SoundId = soundId;
			this._maxTrackedInstances = maxTrackedInstances;
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x00659074 File Offset: 0x00657274
		private LegacySoundStyle(int soundId, int style, int variations, SoundType type, float volume, float pitchVariance, int maxTrackedInstances)
			: base(volume, pitchVariance, type)
		{
			this._style = style;
			this.Variations = variations;
			this.SoundId = soundId;
			this._maxTrackedInstances = maxTrackedInstances;
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x0065909F File Offset: 0x0065729F
		public LegacySoundStyle WithVolume(float volume)
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, base.Type, volume, base.PitchVariance, this.MaxTrackedInstances);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x006590CB File Offset: 0x006572CB
		public LegacySoundStyle WithPitchVariance(float pitchVariance)
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, base.Type, base.Volume, pitchVariance, this.MaxTrackedInstances);
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x006590F7 File Offset: 0x006572F7
		public LegacySoundStyle AsMusic()
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, SoundType.Music, base.Volume, base.PitchVariance, this.MaxTrackedInstances);
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x00659123 File Offset: 0x00657323
		public LegacySoundStyle AsAmbient()
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, SoundType.Ambient, base.Volume, base.PitchVariance, this.MaxTrackedInstances);
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x0065914F File Offset: 0x0065734F
		public LegacySoundStyle AsSound()
		{
			return new LegacySoundStyle(this.SoundId, this._style, this.Variations, SoundType.Sound, base.Volume, base.PitchVariance, this.MaxTrackedInstances);
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x0065917B File Offset: 0x0065737B
		public bool Includes(int soundId, int style)
		{
			return this.SoundId == soundId && style >= this._style && style < this._style + this.Variations;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x006591A1 File Offset: 0x006573A1
		public override SoundEffect GetRandomSound()
		{
			if (this.IsTrackable)
			{
				return SoundEngine.GetTrackableSoundByStyleId(this.Style);
			}
			return null;
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x006591B8 File Offset: 0x006573B8
		// Note: this type is marked as 'beforefieldinit'.
		static LegacySoundStyle()
		{
		}

		// Token: 0x04005E3B RID: 24123
		private static readonly UnifiedRandom Random = new UnifiedRandom();

		// Token: 0x04005E3C RID: 24124
		private readonly int _style;

		// Token: 0x04005E3D RID: 24125
		public readonly int Variations;

		// Token: 0x04005E3E RID: 24126
		public readonly int SoundId;

		// Token: 0x04005E3F RID: 24127
		public readonly int _maxTrackedInstances;
	}
}
