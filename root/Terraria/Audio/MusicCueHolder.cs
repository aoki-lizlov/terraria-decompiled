using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x020005D8 RID: 1496
	public class MusicCueHolder
	{
		// Token: 0x06003AC0 RID: 15040 RVA: 0x00659270 File Offset: 0x00657470
		public MusicCueHolder(SoundBank soundBank, string cueName)
		{
			this._soundBank = soundBank;
			this._cueName = cueName;
			this._loadedCue = null;
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x00659290 File Offset: 0x00657490
		public void Pause()
		{
			if (this._loadedCue == null)
			{
				return;
			}
			if (this._loadedCue.IsPaused)
			{
				return;
			}
			if (!this._loadedCue.IsPlaying)
			{
				return;
			}
			try
			{
				this._loadedCue.Pause();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x006592E4 File Offset: 0x006574E4
		public void Resume()
		{
			if (this._loadedCue == null)
			{
				return;
			}
			if (!this._loadedCue.IsPaused)
			{
				return;
			}
			try
			{
				this._loadedCue.Resume();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x0065932C File Offset: 0x0065752C
		public void Stop()
		{
			if (this._loadedCue == null)
			{
				return;
			}
			this.SetVolume(0f);
			this._loadedCue.Stop(AudioStopOptions.Immediate);
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x0065934E File Offset: 0x0065754E
		public bool IsPlaying
		{
			get
			{
				return this._loadedCue != null && this._loadedCue.IsPlaying;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x00659365 File Offset: 0x00657565
		public bool IsOngoing
		{
			get
			{
				return this._loadedCue != null && (this._loadedCue.IsPlaying || !this._loadedCue.IsStopped);
			}
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x0065938E File Offset: 0x0065758E
		public void RestartAndTryPlaying(float volume)
		{
			this.PurgeCue();
			this.TryPlaying(volume);
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x0065939D File Offset: 0x0065759D
		private void PurgeCue()
		{
			if (this._loadedCue == null)
			{
				return;
			}
			this._loadedCue.Stop(AudioStopOptions.Immediate);
			this._loadedCue.Dispose();
			this._loadedCue = null;
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x006593C6 File Offset: 0x006575C6
		public void Play(float volume)
		{
			this.LoadTrack(false);
			this.SetVolume(volume);
			this._loadedCue.Play();
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x006593E1 File Offset: 0x006575E1
		public void TryPlaying(float volume)
		{
			this.LoadTrack(false);
			if (!this._loadedCue.IsPrepared)
			{
				return;
			}
			this.SetVolume(volume);
			if (!this._loadedCue.IsPlaying)
			{
				this._loadedCue.Play();
			}
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x00659417 File Offset: 0x00657617
		public void SetVolume(float volume)
		{
			this._lastSetVolume = volume;
			if (this._loadedCue != null)
			{
				this._loadedCue.SetVariable("Volume", this._lastSetVolume);
			}
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x0065943E File Offset: 0x0065763E
		private void LoadTrack(bool forceReload)
		{
			if (forceReload || this._loadedCue == null)
			{
				this._loadedCue = this._soundBank.GetCue(this._cueName);
			}
		}

		// Token: 0x04005E42 RID: 24130
		private SoundBank _soundBank;

		// Token: 0x04005E43 RID: 24131
		private string _cueName;

		// Token: 0x04005E44 RID: 24132
		private Cue _loadedCue;

		// Token: 0x04005E45 RID: 24133
		private float _lastSetVolume;
	}
}
