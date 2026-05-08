using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x020005CA RID: 1482
	public class ActiveSound
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x00654ED7 File Offset: 0x006530D7
		// (set) Token: 0x06003A2B RID: 14891 RVA: 0x00654EDF File Offset: 0x006530DF
		public SoundEffectInstance Sound
		{
			[CompilerGenerated]
			get
			{
				return this.<Sound>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Sound>k__BackingField = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x00654EE8 File Offset: 0x006530E8
		// (set) Token: 0x06003A2D RID: 14893 RVA: 0x00654EF0 File Offset: 0x006530F0
		public SoundStyle Style
		{
			[CompilerGenerated]
			get
			{
				return this.<Style>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Style>k__BackingField = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x00654EF9 File Offset: 0x006530F9
		public bool IsPlaying
		{
			get
			{
				return this.Sound != null && this.Sound.State == SoundState.Playing;
			}
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x00654F13 File Offset: 0x00653113
		private void UseOverrides(SoundPlayOverrides overrides)
		{
			if (overrides.Volume != null)
			{
				this.Volume = overrides.Volume.Value;
			}
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x00654F38 File Offset: 0x00653138
		public ActiveSound(SoundStyle style, Vector2 position, SoundPlayOverrides overrides)
		{
			this.Position = position;
			this.Volume = 1f;
			this.Pitch = style.PitchVariance;
			this.IsGlobal = false;
			this.Style = style;
			this.UseOverrides(overrides);
			this.Play();
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x00654F84 File Offset: 0x00653184
		public ActiveSound(SoundStyle style)
		{
			this.Position = Vector2.Zero;
			this.Volume = 1f;
			this.Pitch = style.PitchVariance;
			this.IsGlobal = true;
			this.Style = style;
			this.Play();
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x00654FC4 File Offset: 0x006531C4
		public ActiveSound(SoundStyle style, Vector2 position, ActiveSound.LoopedPlayCondition condition, SoundPlayOverrides overrides)
		{
			this.Position = position;
			this.Volume = 1f;
			this.Pitch = style.PitchVariance;
			this.IsGlobal = false;
			this.Style = style;
			this.UseOverrides(overrides);
			this.PlayLooped(condition);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x00655014 File Offset: 0x00653214
		private void Play()
		{
			SoundEffectInstance soundEffectInstance = this.Style.GetRandomSound().CreateInstance();
			this.Sound = soundEffectInstance;
			soundEffectInstance.Pitch += this.Style.GetRandomPitch();
			this.Pitch = soundEffectInstance.Pitch;
			soundEffectInstance.Volume = this.DetermineIntendedVolume();
			soundEffectInstance.Play();
			SoundInstanceGarbageCollector.Track(soundEffectInstance);
			this.Update();
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x0065507C File Offset: 0x0065327C
		private void PlayLooped(ActiveSound.LoopedPlayCondition condition)
		{
			SoundEffectInstance soundEffectInstance = this.Style.GetRandomSound().CreateInstance();
			this.Sound = soundEffectInstance;
			soundEffectInstance.Pitch += this.Style.GetRandomPitch();
			this.Pitch = soundEffectInstance.Pitch;
			soundEffectInstance.IsLooped = true;
			this.Condition = condition;
			soundEffectInstance.Play();
			SoundInstanceGarbageCollector.Track(soundEffectInstance);
			this.Update();
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x006550E5 File Offset: 0x006532E5
		public void Stop()
		{
			if (this.Sound != null)
			{
				this.Sound.Stop();
			}
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x006550FA File Offset: 0x006532FA
		public void Pause()
		{
			if (this.Sound != null && this.Sound.State == SoundState.Playing)
			{
				this.Sound.Pause();
			}
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x0065511C File Offset: 0x0065331C
		public void Resume()
		{
			if (this.Sound != null && this.Sound.State == SoundState.Paused)
			{
				this.Sound.Resume();
			}
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x00655140 File Offset: 0x00653340
		public void Update()
		{
			if (this.Sound == null)
			{
				return;
			}
			if (this.Condition != null && !this.Condition())
			{
				this.Sound.Stop(true);
				return;
			}
			float num = this.DetermineIntendedVolume();
			this.Sound.Volume = num;
			this.Sound.Pitch = this.Pitch;
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x0065519C File Offset: 0x0065339C
		private float DetermineIntendedVolume()
		{
			float num = 1f;
			if (!this.IsGlobal)
			{
				Vector2 vector = this.Position - Main.Camera.Center;
				this.Sound.Pan = MathHelper.Clamp(vector.X / ((float)Main.MaxWorldViewSize.X * 0.5f), -1f, 1f);
				num = MathHelper.Clamp(1f - vector.Length() / LegacySoundPlayer.SoundAttenuationDistance, 0f, 1f);
			}
			num *= this.Style.Volume * this.Volume;
			switch (this.Style.Type)
			{
			case SoundType.Sound:
				num *= Main.soundVolume;
				break;
			case SoundType.Ambient:
				num *= Main.ambientVolume;
				break;
			case SoundType.Music:
				num *= Main.musicVolume;
				break;
			}
			return MathHelper.Clamp(num, 0f, 1f);
		}

		// Token: 0x04005DD0 RID: 24016
		[CompilerGenerated]
		private SoundEffectInstance <Sound>k__BackingField;

		// Token: 0x04005DD1 RID: 24017
		public readonly bool IsGlobal;

		// Token: 0x04005DD2 RID: 24018
		public Vector2 Position;

		// Token: 0x04005DD3 RID: 24019
		public float Volume;

		// Token: 0x04005DD4 RID: 24020
		public float Pitch;

		// Token: 0x04005DD5 RID: 24021
		[CompilerGenerated]
		private SoundStyle <Style>k__BackingField;

		// Token: 0x04005DD6 RID: 24022
		public ActiveSound.LoopedPlayCondition Condition;

		// Token: 0x020009CA RID: 2506
		// (Invoke) Token: 0x06004A57 RID: 19031
		public delegate bool LoopedPlayCondition();
	}
}
