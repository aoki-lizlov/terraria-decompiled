using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x020005CD RID: 1485
	public abstract class ASoundEffectBasedAudioTrack : IAudioTrack, IDisposable
	{
		// Token: 0x06003A3E RID: 14910 RVA: 0x00655318 File Offset: 0x00653518
		public ASoundEffectBasedAudioTrack()
		{
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x00655340 File Offset: 0x00653540
		protected void CreateSoundEffect(int sampleRate, AudioChannels channels)
		{
			this._sampleRate = sampleRate;
			this._channels = channels;
			this._soundEffectInstance = new DynamicSoundEffectInstance(this._sampleRate, this._channels);
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x00655367 File Offset: 0x00653567
		private void _soundEffectInstance_BufferNeeded(object sender, EventArgs e)
		{
			this.PrepareBuffer();
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x0065536F File Offset: 0x0065356F
		public void Update()
		{
			if (!this.IsPlaying || this._soundEffectInstance.PendingBufferCount >= 8)
			{
				return;
			}
			this.PrepareBuffer();
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x00655390 File Offset: 0x00653590
		protected void ResetBuffer()
		{
			for (int i = 0; i < this._bufferToSubmit.Length; i++)
			{
				this._bufferToSubmit[i] = 0;
			}
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x006553BC File Offset: 0x006535BC
		protected void PrepareBuffer()
		{
			for (int i = 0; i < 2; i++)
			{
				this.ReadAheadPutAChunkIntoTheBuffer();
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x006553DB File Offset: 0x006535DB
		public bool IsPlaying
		{
			get
			{
				return this._soundEffectInstance.State == SoundState.Playing;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x006553EB File Offset: 0x006535EB
		public bool IsStopped
		{
			get
			{
				return this._soundEffectInstance.State == SoundState.Stopped;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x006553FB File Offset: 0x006535FB
		public bool IsPaused
		{
			get
			{
				return this._soundEffectInstance.State == SoundState.Paused;
			}
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x0065540B File Offset: 0x0065360B
		public void Stop(AudioStopOptions options)
		{
			this._soundEffectInstance.Stop(options == AudioStopOptions.Immediate);
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x0065541C File Offset: 0x0065361C
		public void Play()
		{
			this.PrepareToPlay();
			this._soundEffectInstance.Play();
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x0065542F File Offset: 0x0065362F
		public void Pause()
		{
			this._soundEffectInstance.Pause();
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x0065543C File Offset: 0x0065363C
		public void SetVariable(string variableName, float value)
		{
			if (variableName == "Volume")
			{
				float num = this.ReMapVolumeToMatchXact(value);
				this._soundEffectInstance.Volume = num;
				return;
			}
			if (variableName == "Pitch")
			{
				this._soundEffectInstance.Pitch = value;
				return;
			}
			if (!(variableName == "Pan"))
			{
				return;
			}
			this._soundEffectInstance.Pan = value;
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x006554A0 File Offset: 0x006536A0
		private float ReMapVolumeToMatchXact(float musicVolume)
		{
			double num = 31.0 * (double)musicVolume - 25.0 - 11.94;
			return (float)Math.Pow(10.0, num / 20.0);
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x006554E8 File Offset: 0x006536E8
		public void Resume()
		{
			this._soundEffectInstance.Resume();
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x006554F5 File Offset: 0x006536F5
		protected virtual void PrepareToPlay()
		{
			this.ResetBuffer();
		}

		// Token: 0x06003A4E RID: 14926
		public abstract void Reuse();

		// Token: 0x06003A4F RID: 14927 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void ReadAheadPutAChunkIntoTheBuffer()
		{
		}

		// Token: 0x06003A50 RID: 14928
		public abstract void Dispose();

		// Token: 0x04005DDA RID: 24026
		protected const int bufferLength = 4096;

		// Token: 0x04005DDB RID: 24027
		protected const int bufferCountPerSubmit = 2;

		// Token: 0x04005DDC RID: 24028
		protected const int buffersToCoverFor = 8;

		// Token: 0x04005DDD RID: 24029
		protected byte[] _bufferToSubmit = new byte[4096];

		// Token: 0x04005DDE RID: 24030
		protected float[] _temporaryBuffer = new float[2048];

		// Token: 0x04005DDF RID: 24031
		private int _sampleRate;

		// Token: 0x04005DE0 RID: 24032
		private AudioChannels _channels;

		// Token: 0x04005DE1 RID: 24033
		protected DynamicSoundEffectInstance _soundEffectInstance;
	}
}
