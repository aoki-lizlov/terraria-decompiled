using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x02000152 RID: 338
	public sealed class Cue : IDisposable
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0003CD68 File Offset: 0x0003AF68
		public bool IsCreated
		{
			get
			{
				uint num;
				FAudio.FACTCue_GetState(this.handle, out num);
				return (num & 1U) > 0U;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0003CD89 File Offset: 0x0003AF89
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x0003CD91 File Offset: 0x0003AF91
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

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0003CD9C File Offset: 0x0003AF9C
		public bool IsPaused
		{
			get
			{
				uint num;
				FAudio.FACTCue_GetState(this.handle, out num);
				return (num & 64U) > 0U;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0003CDC0 File Offset: 0x0003AFC0
		public bool IsPlaying
		{
			get
			{
				uint num;
				FAudio.FACTCue_GetState(this.handle, out num);
				return (num & 8U) > 0U;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x0003CDE4 File Offset: 0x0003AFE4
		public bool IsPrepared
		{
			get
			{
				uint num;
				FAudio.FACTCue_GetState(this.handle, out num);
				return (num & 4U) > 0U;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0003CE08 File Offset: 0x0003B008
		public bool IsPreparing
		{
			get
			{
				uint num;
				FAudio.FACTCue_GetState(this.handle, out num);
				return (num & 2U) > 0U;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0003CE2C File Offset: 0x0003B02C
		public bool IsStopped
		{
			get
			{
				uint num;
				FAudio.FACTCue_GetState(this.handle, out num);
				return (num & 32U) > 0U;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0003CE50 File Offset: 0x0003B050
		public bool IsStopping
		{
			get
			{
				uint num;
				FAudio.FACTCue_GetState(this.handle, out num);
				return (num & 16U) > 0U;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0003CE72 File Offset: 0x0003B072
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x0003CE7A File Offset: 0x0003B07A
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06001806 RID: 6150 RVA: 0x0003CE84 File Offset: 0x0003B084
		// (remove) Token: 0x06001807 RID: 6151 RVA: 0x0003CEBC File Offset: 0x0003B0BC
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

		// Token: 0x06001808 RID: 6152 RVA: 0x0003CEF4 File Offset: 0x0003B0F4
		internal Cue(IntPtr cue, string name, SoundBank soundBank)
		{
			this.handle = cue;
			this.Name = name;
			this.bank = soundBank;
			this.selfReference = new WeakReference(this, true);
			this.bank.engine.RegisterPointer(this.handle, this.selfReference);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0003CF48 File Offset: 0x0003B148
		~Cue()
		{
			if (!AudioEngine.ProgramExiting)
			{
				if (!this.IsDisposed && this.IsPlaying)
				{
					GC.ReRegisterForFinalize(this);
				}
				else
				{
					this.Dispose(false);
				}
			}
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0003CF98 File Offset: 0x0003B198
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0003CFA8 File Offset: 0x0003B1A8
		public void Apply3D(AudioListener listener, AudioEmitter emitter)
		{
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			if (emitter == null)
			{
				throw new ArgumentNullException("emitter");
			}
			emitter.emitterData.ChannelCount = this.bank.dspSettings.SrcChannelCount;
			emitter.emitterData.CurveDistanceScaler = float.MaxValue;
			FAudio.FACT3DCalculate(this.bank.engine.handle3D, ref listener.listenerData, ref emitter.emitterData, ref this.bank.dspSettings);
			FAudio.FACT3DApply(ref this.bank.dspSettings, this.handle);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0003D040 File Offset: 0x0003B240
		public float GetVariable(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			ushort num = FAudio.FACTCue_GetVariableIndex(this.handle, name);
			if (num == 65535)
			{
				throw new InvalidOperationException("Invalid variable name!");
			}
			float num2;
			FAudio.FACTCue_GetVariable(this.handle, num, out num2);
			return num2;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0003D090 File Offset: 0x0003B290
		public void Pause()
		{
			FAudio.FACTCue_Pause(this.handle, 1);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0003D09F File Offset: 0x0003B29F
		public void Play()
		{
			FAudio.FACTCue_Play(this.handle);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0003D0AD File Offset: 0x0003B2AD
		public void Resume()
		{
			FAudio.FACTCue_Pause(this.handle, 0);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0003D0BC File Offset: 0x0003B2BC
		public void SetVariable(string name, float value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			ushort num = FAudio.FACTCue_GetVariableIndex(this.handle, name);
			if (num == 65535)
			{
				throw new InvalidOperationException("Invalid variable name!");
			}
			FAudio.FACTCue_SetVariable(this.handle, num, value);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0003D10A File Offset: 0x0003B30A
		public void Stop(AudioStopOptions options)
		{
			FAudio.FACTCue_Stop(this.handle, (options == AudioStopOptions.Immediate) ? 1U : 0U);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0003D11C File Offset: 0x0003B31C
		internal void OnCueDestroyed()
		{
			this.IsDisposed = true;
			this.handle = IntPtr.Zero;
			this.selfReference = null;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0003D138 File Offset: 0x0003B338
		private void Dispose(bool disposing)
		{
			object gcSync = this.bank.engine.gcSync;
			lock (gcSync)
			{
				if (!this.IsDisposed)
				{
					if (this.Disposing != null)
					{
						this.Disposing(this, null);
					}
					if (!this.bank.engine.IsDisposed)
					{
						FAudio.FACTCue_Destroy(this.handle);
					}
					this.OnCueDestroyed();
				}
			}
		}

		// Token: 0x04000AFF RID: 2815
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x04000B00 RID: 2816
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000B01 RID: 2817
		private IntPtr handle;

		// Token: 0x04000B02 RID: 2818
		private SoundBank bank;

		// Token: 0x04000B03 RID: 2819
		private WeakReference selfReference;

		// Token: 0x04000B04 RID: 2820
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposing;
	}
}
