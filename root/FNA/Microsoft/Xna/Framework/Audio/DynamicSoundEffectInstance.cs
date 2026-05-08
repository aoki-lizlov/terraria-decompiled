using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x02000153 RID: 339
	public sealed class DynamicSoundEffectInstance : SoundEffectInstance
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0003D1C0 File Offset: 0x0003B3C0
		public int PendingBufferCount
		{
			get
			{
				return this.queuedBuffers.Count;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x000136EB File Offset: 0x000118EB
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x00009E6B File Offset: 0x0000806B
		public override bool IsLooped
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06001817 RID: 6167 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		// (remove) Token: 0x06001818 RID: 6168 RVA: 0x0003D208 File Offset: 0x0003B408
		public event EventHandler<EventArgs> BufferNeeded
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.BufferNeeded;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.BufferNeeded, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.BufferNeeded;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.BufferNeeded, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0003D240 File Offset: 0x0003B440
		public DynamicSoundEffectInstance(int sampleRate, AudioChannels channels)
			: base(null)
		{
			this.sampleRate = sampleRate;
			this.channels = channels;
			this.isDynamic = true;
			this.format = default(FAudio.FAudioWaveFormatEx);
			this.format.wFormatTag = 1;
			this.format.nChannels = (ushort)channels;
			this.format.nSamplesPerSec = (uint)sampleRate;
			this.format.wBitsPerSample = 16;
			this.format.nBlockAlign = 2 * this.format.nChannels;
			this.format.nAvgBytesPerSec = (uint)this.format.nBlockAlign * this.format.nSamplesPerSec;
			this.format.cbSize = 0;
			this.queuedBuffers = new List<IntPtr>();
			this.queuedSizes = new List<uint>();
			base.InitDSPSettings((uint)this.format.nChannels);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0003D318 File Offset: 0x0003B518
		~DynamicSoundEffectInstance()
		{
			base.Dispose();
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0003D344 File Offset: 0x0003B544
		public TimeSpan GetSampleDuration(int sizeInBytes)
		{
			return SoundEffect.GetSampleDuration(sizeInBytes, this.sampleRate, this.channels);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0003D358 File Offset: 0x0003B558
		public int GetSampleSizeInBytes(TimeSpan duration)
		{
			return SoundEffect.GetSampleSizeInBytes(duration, this.sampleRate, this.channels);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0003D36C File Offset: 0x0003B56C
		public override void Play()
		{
			this.Update();
			base.Play();
			List<DynamicSoundEffectInstance> streams = FrameworkDispatcher.Streams;
			lock (streams)
			{
				if (!FrameworkDispatcher.Streams.Contains(this))
				{
					FrameworkDispatcher.Streams.Add(this);
				}
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0003D3CC File Offset: 0x0003B5CC
		public void SubmitBuffer(byte[] buffer)
		{
			this.SubmitBuffer(buffer, 0, buffer.Length);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0003D3DC File Offset: 0x0003B5DC
		public void SubmitBuffer(byte[] buffer, int offset, int count)
		{
			IntPtr intPtr = FNAPlatform.Malloc(count);
			Marshal.Copy(buffer, offset, intPtr, count);
			List<IntPtr> list = this.queuedBuffers;
			lock (list)
			{
				this.queuedBuffers.Add(intPtr);
				if (base.State != SoundState.Stopped)
				{
					FAudio.FAudioBuffer faudioBuffer = default(FAudio.FAudioBuffer);
					faudioBuffer.AudioBytes = (uint)count;
					faudioBuffer.pAudioData = intPtr;
					faudioBuffer.PlayLength = faudioBuffer.AudioBytes / (uint)this.channels / (uint)(this.format.wBitsPerSample / 8);
					FAudio.FAudioSourceVoice_SubmitSourceBuffer(this.handle, ref faudioBuffer, IntPtr.Zero);
				}
				else
				{
					this.queuedSizes.Add((uint)count);
				}
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0003D49C File Offset: 0x0003B69C
		public void SubmitFloatBufferEXT(float[] buffer)
		{
			this.SubmitFloatBufferEXT(buffer, 0, buffer.Length);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0003D4AC File Offset: 0x0003B6AC
		public void SubmitFloatBufferEXT(float[] buffer, int offset, int count)
		{
			if (base.State != SoundState.Stopped && this.format.wFormatTag == 1)
			{
				throw new InvalidOperationException("Submit a float buffer before Playing!");
			}
			this.format.wFormatTag = 3;
			this.format.wBitsPerSample = 32;
			this.format.nBlockAlign = 4 * this.format.nChannels;
			this.format.nAvgBytesPerSec = (uint)this.format.nBlockAlign * this.format.nSamplesPerSec;
			IntPtr intPtr = FNAPlatform.Malloc(count * 4);
			Marshal.Copy(buffer, offset, intPtr, count);
			List<IntPtr> list = this.queuedBuffers;
			lock (list)
			{
				this.queuedBuffers.Add(intPtr);
				if (base.State != SoundState.Stopped)
				{
					FAudio.FAudioBuffer faudioBuffer = default(FAudio.FAudioBuffer);
					faudioBuffer.AudioBytes = (uint)(count * 4);
					faudioBuffer.pAudioData = intPtr;
					faudioBuffer.PlayLength = faudioBuffer.AudioBytes / (uint)this.channels / (uint)(this.format.wBitsPerSample / 8);
					FAudio.FAudioSourceVoice_SubmitSourceBuffer(this.handle, ref faudioBuffer, IntPtr.Zero);
				}
				else
				{
					this.queuedSizes.Add((uint)(count * 4));
				}
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0003D5E8 File Offset: 0x0003B7E8
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0003D5F4 File Offset: 0x0003B7F4
		internal void QueueInitialBuffers()
		{
			FAudio.FAudioBuffer faudioBuffer = default(FAudio.FAudioBuffer);
			List<IntPtr> list = this.queuedBuffers;
			lock (list)
			{
				for (int i = 0; i < this.queuedBuffers.Count; i++)
				{
					faudioBuffer.AudioBytes = this.queuedSizes[i];
					faudioBuffer.pAudioData = this.queuedBuffers[i];
					faudioBuffer.PlayLength = faudioBuffer.AudioBytes / (uint)this.channels / (uint)(this.format.wBitsPerSample / 8);
					FAudio.FAudioSourceVoice_SubmitSourceBuffer(this.handle, ref faudioBuffer, IntPtr.Zero);
				}
				this.queuedSizes.Clear();
			}
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0003D6B4 File Offset: 0x0003B8B4
		internal void ClearBuffers()
		{
			List<IntPtr> list = this.queuedBuffers;
			lock (list)
			{
				foreach (IntPtr intPtr in this.queuedBuffers)
				{
					FNAPlatform.Free(intPtr);
				}
				this.queuedBuffers.Clear();
				this.queuedSizes.Clear();
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0003D74C File Offset: 0x0003B94C
		internal void Update()
		{
			if (base.State != SoundState.Playing)
			{
				return;
			}
			if (this.handle != IntPtr.Zero)
			{
				FAudio.FAudioVoiceState faudioVoiceState;
				FAudio.FAudioSourceVoice_GetState(this.handle, out faudioVoiceState, 256U);
				while ((long)this.PendingBufferCount > (long)((ulong)faudioVoiceState.BuffersQueued))
				{
					List<IntPtr> list = this.queuedBuffers;
					lock (list)
					{
						FNAPlatform.Free(this.queuedBuffers[0]);
						this.queuedBuffers.RemoveAt(0);
					}
				}
			}
			int num = 3 - this.PendingBufferCount;
			while (num > 0 && this.BufferNeeded != null)
			{
				this.BufferNeeded(this, null);
				num--;
			}
		}

		// Token: 0x04000B05 RID: 2821
		internal FAudio.FAudioWaveFormatEx format;

		// Token: 0x04000B06 RID: 2822
		private int sampleRate;

		// Token: 0x04000B07 RID: 2823
		private AudioChannels channels;

		// Token: 0x04000B08 RID: 2824
		private List<IntPtr> queuedBuffers;

		// Token: 0x04000B09 RID: 2825
		private List<uint> queuedSizes;

		// Token: 0x04000B0A RID: 2826
		private const int MINIMUM_BUFFER_CHECK = 3;

		// Token: 0x04000B0B RID: 2827
		[CompilerGenerated]
		private EventHandler<EventArgs> BufferNeeded;
	}
}
