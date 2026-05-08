using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x020005D3 RID: 1491
	public class CueAudioTrack : IAudioTrack, IDisposable
	{
		// Token: 0x06003A85 RID: 14981 RVA: 0x0065555B File Offset: 0x0065375B
		public CueAudioTrack(SoundBank bank, string cueName)
		{
			this._soundBank = bank;
			this._cueName = cueName;
			this.Reuse();
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x00655577 File Offset: 0x00653777
		public bool IsPlaying
		{
			get
			{
				return this._cue.IsPlaying;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06003A87 RID: 14983 RVA: 0x00655584 File Offset: 0x00653784
		public bool IsStopped
		{
			get
			{
				return this._cue.IsStopped;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x00655591 File Offset: 0x00653791
		public bool IsPaused
		{
			get
			{
				return this._cue.IsPaused;
			}
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x0065559E File Offset: 0x0065379E
		public void Stop(AudioStopOptions options)
		{
			this._cue.Stop(options);
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x006555AC File Offset: 0x006537AC
		public void Play()
		{
			this._cue.Play();
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x006555B9 File Offset: 0x006537B9
		public void SetVariable(string variableName, float value)
		{
			this._cue.SetVariable(variableName, value);
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x006555C8 File Offset: 0x006537C8
		public void Resume()
		{
			this._cue.Resume();
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x006555D5 File Offset: 0x006537D5
		public void Reuse()
		{
			if (this._cue != null)
			{
				this.Stop(AudioStopOptions.Immediate);
			}
			this._cue = this._soundBank.GetCue(this._cueName);
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x006555FD File Offset: 0x006537FD
		public void Pause()
		{
			this._cue.Pause();
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x00009E46 File Offset: 0x00008046
		public void Dispose()
		{
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x00009E46 File Offset: 0x00008046
		public void Update()
		{
		}

		// Token: 0x04005DE4 RID: 24036
		private Cue _cue;

		// Token: 0x04005DE5 RID: 24037
		private string _cueName;

		// Token: 0x04005DE6 RID: 24038
		private SoundBank _soundBank;
	}
}
