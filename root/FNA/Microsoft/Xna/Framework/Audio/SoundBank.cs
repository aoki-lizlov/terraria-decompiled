using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x0200015A RID: 346
	public class SoundBank : IDisposable
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0003DAE6 File Offset: 0x0003BCE6
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x0003DAEE File Offset: 0x0003BCEE
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

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0003DAF8 File Offset: 0x0003BCF8
		public bool IsInUse
		{
			get
			{
				uint num;
				FAudio.FACTSoundBank_GetState(this.handle, out num);
				return (num & 128U) > 0U;
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x0600184E RID: 6222 RVA: 0x0003DB20 File Offset: 0x0003BD20
		// (remove) Token: 0x0600184F RID: 6223 RVA: 0x0003DB58 File Offset: 0x0003BD58
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

		// Token: 0x06001850 RID: 6224 RVA: 0x0003DB90 File Offset: 0x0003BD90
		public SoundBank(AudioEngine audioEngine, string filename)
		{
			if (audioEngine == null)
			{
				throw new ArgumentNullException("audioEngine");
			}
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			IntPtr intPtr2;
			IntPtr intPtr = TitleContainer.ReadToPointer(filename, out intPtr2);
			FAudio.FACTAudioEngine_CreateSoundBank(audioEngine.handle, intPtr, (uint)(int)intPtr2, 0U, 0U, out this.handle);
			FNAPlatform.FreeFilePointer(intPtr);
			this.engine = audioEngine;
			this.selfReference = new WeakReference(this, true);
			this.dspSettings = default(FAudio.F3DAUDIO_DSP_SETTINGS);
			this.dspSettings.SrcChannelCount = 1U;
			this.dspSettings.DstChannelCount = (uint)this.engine.channels;
			this.dspSettings.pMatrixCoefficients = FNAPlatform.Malloc((int)(4U * this.dspSettings.SrcChannelCount * this.dspSettings.DstChannelCount));
			this.engine.RegisterPointer(this.handle, this.selfReference);
			this.IsDisposed = false;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0003DC84 File Offset: 0x0003BE84
		~SoundBank()
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

		// Token: 0x06001852 RID: 6226 RVA: 0x0003DCD4 File Offset: 0x0003BED4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0003DCE4 File Offset: 0x0003BEE4
		protected void Dispose(bool disposing)
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
						FAudio.FACTSoundBank_Destroy(this.handle);
					}
					this.OnSoundBankDestroyed();
				}
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0003DD60 File Offset: 0x0003BF60
		public Cue GetCue(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			ushort num = FAudio.FACTSoundBank_GetCueIndex(this.handle, name);
			if (num == 65535)
			{
				throw new InvalidOperationException("Invalid cue name!");
			}
			IntPtr intPtr;
			FAudio.FACTSoundBank_Prepare(this.handle, num, 0U, 0, out intPtr);
			return new Cue(intPtr, name, this);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0003DDBC File Offset: 0x0003BFBC
		public void PlayCue(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			ushort num = FAudio.FACTSoundBank_GetCueIndex(this.handle, name);
			if (num == 65535)
			{
				throw new InvalidOperationException("Invalid cue name!");
			}
			FAudio.FACTSoundBank_Play(this.handle, num, 0U, 0, IntPtr.Zero);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0003DE10 File Offset: 0x0003C010
		public void PlayCue(string name, AudioListener listener, AudioEmitter emitter)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			if (emitter == null)
			{
				throw new ArgumentNullException("emitter");
			}
			ushort num = FAudio.FACTSoundBank_GetCueIndex(this.handle, name);
			if (num == 65535)
			{
				throw new InvalidOperationException("Invalid cue name!");
			}
			emitter.emitterData.ChannelCount = this.dspSettings.SrcChannelCount;
			emitter.emitterData.CurveDistanceScaler = float.MaxValue;
			FAudio.FACT3DCalculate(this.engine.handle3D, ref listener.listenerData, ref emitter.emitterData, ref this.dspSettings);
			FAudio.FACTSoundBank_Play3D(this.handle, num, 0U, 0, ref this.dspSettings, IntPtr.Zero);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0003DED0 File Offset: 0x0003C0D0
		internal void OnSoundBankDestroyed()
		{
			this.IsDisposed = true;
			this.handle = IntPtr.Zero;
			this.selfReference = null;
			if (this.dspSettings.pMatrixCoefficients != IntPtr.Zero)
			{
				FNAPlatform.Free(this.dspSettings.pMatrixCoefficients);
				this.dspSettings.pMatrixCoefficients = IntPtr.Zero;
			}
		}

		// Token: 0x04000B18 RID: 2840
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x04000B19 RID: 2841
		internal AudioEngine engine;

		// Token: 0x04000B1A RID: 2842
		internal FAudio.F3DAUDIO_DSP_SETTINGS dspSettings;

		// Token: 0x04000B1B RID: 2843
		private IntPtr handle;

		// Token: 0x04000B1C RID: 2844
		private WeakReference selfReference;

		// Token: 0x04000B1D RID: 2845
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposing;
	}
}
