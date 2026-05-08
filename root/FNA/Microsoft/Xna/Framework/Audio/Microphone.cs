using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x02000155 RID: 341
	public class Microphone
	{
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x0003D810 File Offset: 0x0003BA10
		public static ReadOnlyCollection<Microphone> All
		{
			get
			{
				if (Microphone.micList == null)
				{
					Microphone.micList = new ReadOnlyCollection<Microphone>(FNAPlatform.GetMicrophones());
				}
				return Microphone.micList;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0003D832 File Offset: 0x0003BA32
		public static Microphone Default
		{
			get
			{
				if (Microphone.All.Count == 0)
				{
					return null;
				}
				return Microphone.All[0];
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x0003D84D File Offset: 0x0003BA4D
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x0003D855 File Offset: 0x0003BA55
		public TimeSpan BufferDuration
		{
			get
			{
				return this.bufferDuration;
			}
			set
			{
				if (value.Milliseconds < 100 || value.Milliseconds > 1000 || value.Milliseconds % 10 != 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.bufferDuration = value;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x000136EB File Offset: 0x000118EB
		public bool IsHeadset
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0003D889 File Offset: 0x0003BA89
		public int SampleRate
		{
			get
			{
				return 44100;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x0003D890 File Offset: 0x0003BA90
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x0003D898 File Offset: 0x0003BA98
		public MicrophoneState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<State>k__BackingField = value;
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06001831 RID: 6193 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
		// (remove) Token: 0x06001832 RID: 6194 RVA: 0x0003D8DC File Offset: 0x0003BADC
		public event EventHandler<EventArgs> BufferReady
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.BufferReady;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.BufferReady, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.BufferReady;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.BufferReady, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0003D911 File Offset: 0x0003BB11
		internal Microphone(uint id, string name)
		{
			this.handle = id;
			this.Name = name;
			this.bufferDuration = TimeSpan.FromSeconds(1.0);
			this.State = MicrophoneState.Stopped;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0003D942 File Offset: 0x0003BB42
		public int GetData(byte[] buffer)
		{
			return this.GetData(buffer, 0, buffer.Length);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0003D950 File Offset: 0x0003BB50
		public int GetData(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentException("buffer is null!");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentException("offset");
			}
			if (count <= 0 || offset + count > buffer.Length)
			{
				throw new ArgumentException("count");
			}
			return FNAPlatform.GetMicrophoneSamples(this.handle, buffer, offset, count);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0003D9AA File Offset: 0x0003BBAA
		public TimeSpan GetSampleDuration(int sizeInBytes)
		{
			return SoundEffect.GetSampleDuration(sizeInBytes, this.SampleRate, AudioChannels.Mono);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0003D9B9 File Offset: 0x0003BBB9
		public int GetSampleSizeInBytes(TimeSpan duration)
		{
			return SoundEffect.GetSampleSizeInBytes(duration, this.SampleRate, AudioChannels.Mono);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0003D9C8 File Offset: 0x0003BBC8
		public void Start()
		{
			FNAPlatform.StartMicrophone(this.handle);
			this.State = MicrophoneState.Started;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0003D9E1 File Offset: 0x0003BBE1
		public void Stop()
		{
			FNAPlatform.StopMicrophone(this.handle);
			this.State = MicrophoneState.Stopped;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0003D9FA File Offset: 0x0003BBFA
		internal void CheckBuffer()
		{
			if (this.BufferReady != null && this.GetSampleDuration(FNAPlatform.GetMicrophoneQueuedBytes(this.handle)) > this.bufferDuration)
			{
				this.BufferReady(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000B0C RID: 2828
		[CompilerGenerated]
		private MicrophoneState <State>k__BackingField;

		// Token: 0x04000B0D RID: 2829
		public readonly string Name;

		// Token: 0x04000B0E RID: 2830
		private TimeSpan bufferDuration;

		// Token: 0x04000B0F RID: 2831
		private uint handle;

		// Token: 0x04000B10 RID: 2832
		internal static ReadOnlyCollection<Microphone> micList;

		// Token: 0x04000B11 RID: 2833
		[CompilerGenerated]
		private EventHandler<EventArgs> BufferReady;

		// Token: 0x04000B12 RID: 2834
		internal const int SAMPLERATE = 44100;
	}
}
