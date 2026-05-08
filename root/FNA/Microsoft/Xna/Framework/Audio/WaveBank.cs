using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using MonoGame.Utilities;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x0200015E RID: 350
	public class WaveBank : IDisposable
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x0003F2B4 File Offset: 0x0003D4B4
		// (set) Token: 0x06001891 RID: 6289 RVA: 0x0003F2BC File Offset: 0x0003D4BC
		public bool IsDisposed
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDisposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsDisposed>k__BackingField = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0003F2C8 File Offset: 0x0003D4C8
		public bool IsPrepared
		{
			get
			{
				uint num;
				FAudio.FACTWaveBank_GetState(this.handle, out num);
				return (num & 4U) > 0U;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0003F2EC File Offset: 0x0003D4EC
		public bool IsInUse
		{
			get
			{
				uint num;
				FAudio.FACTWaveBank_GetState(this.handle, out num);
				return (num & 128U) > 0U;
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06001894 RID: 6292 RVA: 0x0003F314 File Offset: 0x0003D514
		// (remove) Token: 0x06001895 RID: 6293 RVA: 0x0003F34C File Offset: 0x0003D54C
		public event EventHandler<EventArgs> Disposing
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.Disposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.Disposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0003F384 File Offset: 0x0003D584
		public WaveBank(AudioEngine audioEngine, string nonStreamingWaveBankFilename)
		{
			if (audioEngine == null)
			{
				throw new ArgumentNullException("audioEngine");
			}
			if (string.IsNullOrEmpty(nonStreamingWaveBankFilename))
			{
				throw new ArgumentNullException("nonStreamingWaveBankFilename");
			}
			this.bankData = TitleContainer.ReadToPointer(nonStreamingWaveBankFilename, out this.bankDataLen);
			FAudio.FACTAudioEngine_CreateInMemoryWaveBank(audioEngine.handle, this.bankData, (uint)(int)this.bankDataLen, 0U, 0U, out this.handle);
			this.engine = audioEngine;
			this.selfReference = new WeakReference(this, true);
			this.engine.RegisterPointer(this.handle, this.selfReference);
			this.IsDisposed = false;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0003F424 File Offset: 0x0003D624
		public WaveBank(AudioEngine audioEngine, string streamingWaveBankFilename, int offset, short packetsize)
		{
			if (audioEngine == null)
			{
				throw new ArgumentNullException("audioEngine");
			}
			if (string.IsNullOrEmpty(streamingWaveBankFilename))
			{
				throw new ArgumentNullException("streamingWaveBankFilename");
			}
			string text = FileHelpers.NormalizeFilePathSeparators(streamingWaveBankFilename);
			if (!Path.IsPathRooted(text))
			{
				text = Path.Combine(TitleLocation.Path, text);
			}
			this.bankData = FAudio.FAudio_fopen(text);
			FAudio.FACTStreamingParameters factstreamingParameters = default(FAudio.FACTStreamingParameters);
			factstreamingParameters.file = this.bankData;
			FAudio.FACTAudioEngine_CreateStreamingWaveBank(audioEngine.handle, ref factstreamingParameters, out this.handle);
			this.engine = audioEngine;
			this.selfReference = new WeakReference(this, true);
			this.engine.RegisterPointer(this.handle, this.selfReference);
			this.IsDisposed = false;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0003F4DC File Offset: 0x0003D6DC
		~WaveBank()
		{
			if (!AudioEngine.ProgramExiting)
			{
				if (!this.IsDisposed && this.IsInUse)
				{
					GC.ReRegisterForFinalize(this);
				}
				else
				{
					this.Dispose(false);
				}
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0003F52C File Offset: 0x0003D72C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0003F53C File Offset: 0x0003D73C
		protected virtual void Dispose(bool disposing)
		{
			object gcSync = this.engine.gcSync;
			lock (gcSync)
			{
				if (!this.IsDisposed)
				{
					if (this.Disposing != null)
					{
						this.Disposing(this, null);
					}
					if (!this.engine.IsDisposed)
					{
						FAudio.FACTWaveBank_Destroy(this.handle);
					}
					this.OnWaveBankDestroyed();
				}
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0003F5B8 File Offset: 0x0003D7B8
		internal void OnWaveBankDestroyed()
		{
			this.IsDisposed = true;
			if (this.bankData != IntPtr.Zero)
			{
				if (this.bankDataLen != IntPtr.Zero)
				{
					FNAPlatform.FreeFilePointer(this.bankData);
					this.bankDataLen = IntPtr.Zero;
				}
				else
				{
					FAudio.FAudio_close(this.bankData);
				}
				this.bankData = IntPtr.Zero;
			}
			this.handle = IntPtr.Zero;
			this.selfReference = null;
		}

		// Token: 0x04000B3B RID: 2875
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x04000B3C RID: 2876
		private IntPtr handle;

		// Token: 0x04000B3D RID: 2877
		private AudioEngine engine;

		// Token: 0x04000B3E RID: 2878
		private WeakReference selfReference;

		// Token: 0x04000B3F RID: 2879
		private IntPtr bankData;

		// Token: 0x04000B40 RID: 2880
		private IntPtr bankDataLen;

		// Token: 0x04000B41 RID: 2881
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposing;
	}
}
