using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x0200015C RID: 348
	public class SoundEffectInstance : IDisposable
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0003E738 File Offset: 0x0003C938
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x0003E740 File Offset: 0x0003C940
		public bool IsDisposed
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDisposed>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsDisposed>k__BackingField = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0003E749 File Offset: 0x0003C949
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x0003E751 File Offset: 0x0003C951
		public virtual bool IsLooped
		{
			get
			{
				return this.INTERNAL_looped;
			}
			set
			{
				if (this.hasStarted)
				{
					throw new InvalidOperationException();
				}
				this.INTERNAL_looped = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0003E768 File Offset: 0x0003C968
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x0003E770 File Offset: 0x0003C970
		public float Pan
		{
			get
			{
				return this.INTERNAL_pan;
			}
			set
			{
				if (this.IsDisposed)
				{
					throw new ObjectDisposedException("SoundEffectInstance");
				}
				if (value > 1f || value < -1f)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.INTERNAL_pan = value;
				if (this.is3D)
				{
					return;
				}
				this.SetPanMatrixCoefficients();
				if (this.handle != IntPtr.Zero)
				{
					FAudio.FAudioVoice_SetOutputMatrix(this.handle, SoundEffect.Device().MasterVoice, this.dspSettings.SrcChannelCount, this.dspSettings.DstChannelCount, this.dspSettings.pMatrixCoefficients, 0U);
				}
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0003E80B File Offset: 0x0003CA0B
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x0003E813 File Offset: 0x0003CA13
		public float Pitch
		{
			get
			{
				return this.INTERNAL_pitch;
			}
			set
			{
				this.INTERNAL_pitch = MathHelper.Clamp(value, -1f, 1f);
				if (this.handle != IntPtr.Zero)
				{
					this.UpdatePitch();
				}
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0003E844 File Offset: 0x0003CA44
		public SoundState State
		{
			get
			{
				if (!this.isDynamic && this.handle != IntPtr.Zero && this.INTERNAL_state == SoundState.Playing)
				{
					FAudio.FAudioVoiceState faudioVoiceState;
					FAudio.FAudioSourceVoice_GetState(this.handle, out faudioVoiceState, 256U);
					if (faudioVoiceState.BuffersQueued == 0U)
					{
						this.Stop(true);
					}
				}
				return this.INTERNAL_state;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0003E89A File Offset: 0x0003CA9A
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x0003E8A2 File Offset: 0x0003CAA2
		public float Volume
		{
			get
			{
				return this.INTERNAL_volume;
			}
			set
			{
				this.INTERNAL_volume = value;
				if (this.handle != IntPtr.Zero)
				{
					FAudio.FAudioVoice_SetVolume(this.handle, this.INTERNAL_volume, 0U);
				}
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0003E8D0 File Offset: 0x0003CAD0
		internal SoundEffectInstance(SoundEffect parent = null)
		{
			SoundEffect.Device();
			this.selfReference = new WeakReference(this, true);
			this.parentEffect = parent;
			this.isDynamic = this is DynamicSoundEffectInstance;
			this.hasStarted = false;
			this.is3D = false;
			this.usingReverb = false;
			this.INTERNAL_state = SoundState.Stopped;
			if (!this.isDynamic)
			{
				this.InitDSPSettings((uint)this.parentEffect.channels);
			}
			if (this.parentEffect != null)
			{
				this.parentEffect.Instances.Add(this.selfReference);
			}
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0003E974 File Offset: 0x0003CB74
		~SoundEffectInstance()
		{
			if (!SoundEffect.FAudioContext.ProgramExiting && !this.IsDisposed && this.State == SoundState.Playing)
			{
				GC.ReRegisterForFinalize(this);
			}
			else
			{
				this.Dispose();
			}
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0003E9C0 File Offset: 0x0003CBC0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0003E9D0 File Offset: 0x0003CBD0
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
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException("SoundEffectInstance");
			}
			this.is3D = true;
			SoundEffect.FAudioContext faudioContext = SoundEffect.Device();
			emitter.emitterData.CurveDistanceScaler = faudioContext.CurveDistanceScaler;
			emitter.emitterData.ChannelCount = this.dspSettings.SrcChannelCount;
			FAudio.F3DAudioCalculate(faudioContext.Handle3D, ref listener.listenerData, ref emitter.emitterData, 33U, ref this.dspSettings);
			if (this.handle != IntPtr.Zero)
			{
				this.UpdatePitch();
				FAudio.FAudioVoice_SetOutputMatrix(this.handle, SoundEffect.Device().MasterVoice, this.dspSettings.SrcChannelCount, this.dspSettings.DstChannelCount, this.dspSettings.pMatrixCoefficients, 0U);
			}
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0003EAAF File Offset: 0x0003CCAF
		public void Apply3D(AudioListener[] listeners, AudioEmitter emitter)
		{
			if (listeners == null)
			{
				throw new ArgumentNullException("listeners");
			}
			if (listeners.Length == 1)
			{
				this.Apply3D(listeners[0], emitter);
				return;
			}
			throw new NotSupportedException("Only one listener is supported.");
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0003EADC File Offset: 0x0003CCDC
		public virtual void Play()
		{
			if (this.State == SoundState.Playing)
			{
				return;
			}
			if (this.State == SoundState.Paused)
			{
				FAudio.FAudioSourceVoice_Start(this.handle, 0U, 0U);
				this.INTERNAL_state = SoundState.Playing;
				return;
			}
			SoundEffect.FAudioContext faudioContext = SoundEffect.Device();
			if (this.isDynamic)
			{
				FAudio.FAudio_CreateSourceVoice(faudioContext.Handle, out this.handle, ref (this as DynamicSoundEffectInstance).format, 8U, SoundEffectInstance.maxFreqRatio, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			}
			else
			{
				FAudio.FAudio_CreateSourceVoice(faudioContext.Handle, out this.handle, this.parentEffect.formatPtr, 8U, SoundEffectInstance.maxFreqRatio, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			}
			if (this.handle == IntPtr.Zero)
			{
				return;
			}
			FAudio.FAudio_AddRef(faudioContext.Handle);
			FAudio.FAudioVoice_SetVolume(this.handle, this.INTERNAL_volume, 0U);
			this.UpdatePitch();
			if (this.is3D || this.Pan != 0f)
			{
				FAudio.FAudioVoice_SetOutputMatrix(this.handle, SoundEffect.Device().MasterVoice, this.dspSettings.SrcChannelCount, this.dspSettings.DstChannelCount, this.dspSettings.pMatrixCoefficients, 0U);
			}
			if (this.isDynamic)
			{
				(this as DynamicSoundEffectInstance).QueueInitialBuffers();
			}
			else
			{
				if (this.IsLooped)
				{
					this.parentEffect.handle.LoopCount = 255U;
					this.parentEffect.handle.LoopBegin = this.parentEffect.loopStart;
					this.parentEffect.handle.LoopLength = this.parentEffect.loopLength;
				}
				else
				{
					this.parentEffect.handle.LoopCount = 0U;
					this.parentEffect.handle.LoopBegin = 0U;
					this.parentEffect.handle.LoopLength = 0U;
				}
				FAudio.FAudioSourceVoice_SubmitSourceBuffer(this.handle, ref this.parentEffect.handle, IntPtr.Zero);
			}
			FAudio.FAudioSourceVoice_Start(this.handle, 0U, 0U);
			this.INTERNAL_state = SoundState.Playing;
			this.hasStarted = true;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0003ECE2 File Offset: 0x0003CEE2
		public void Pause()
		{
			if (this.handle != IntPtr.Zero && this.State == SoundState.Playing)
			{
				FAudio.FAudioSourceVoice_Stop(this.handle, 0U, 0U);
				this.INTERNAL_state = SoundState.Paused;
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0003ED14 File Offset: 0x0003CF14
		public void Resume()
		{
			SoundState state = this.State;
			if (this.handle == IntPtr.Zero)
			{
				this.Play();
				return;
			}
			if (state == SoundState.Paused)
			{
				FAudio.FAudioSourceVoice_Start(this.handle, 0U, 0U);
				this.INTERNAL_state = SoundState.Playing;
			}
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0003ED5A File Offset: 0x0003CF5A
		public void Stop()
		{
			this.Stop(true);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0003ED64 File Offset: 0x0003CF64
		public void Stop(bool immediate)
		{
			if (this.handle == IntPtr.Zero)
			{
				return;
			}
			if (immediate)
			{
				FAudio.FAudioSourceVoice_Stop(this.handle, 0U, 0U);
				FAudio.FAudioSourceVoice_FlushSourceBuffers(this.handle);
				FAudio.FAudioVoice_DestroyVoice(this.handle);
				FAudio.FAudio_Release(SoundEffect.Device().Handle);
				this.handle = IntPtr.Zero;
				this.usingReverb = false;
				this.INTERNAL_state = SoundState.Stopped;
				if (this.isDynamic)
				{
					List<DynamicSoundEffectInstance> streams = FrameworkDispatcher.Streams;
					lock (streams)
					{
						FrameworkDispatcher.Streams.Remove(this as DynamicSoundEffectInstance);
					}
					(this as DynamicSoundEffectInstance).ClearBuffers();
					return;
				}
			}
			else
			{
				if (this.isDynamic)
				{
					throw new InvalidOperationException();
				}
				FAudio.FAudioSourceVoice_ExitLoop(this.handle, 0U);
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0003EE44 File Offset: 0x0003D044
		protected virtual void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				this.Stop(true);
				if (this.parentEffect != null)
				{
					this.parentEffect.Instances.Remove(this.selfReference);
				}
				this.selfReference = null;
				FNAPlatform.Free(this.dspSettings.pMatrixCoefficients);
				this.IsDisposed = true;
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0003EEA4 File Offset: 0x0003D0A4
		internal unsafe void InitDSPSettings(uint srcChannels)
		{
			this.dspSettings = default(FAudio.F3DAUDIO_DSP_SETTINGS);
			this.dspSettings.DopplerFactor = 1f;
			this.dspSettings.SrcChannelCount = srcChannels;
			this.dspSettings.DstChannelCount = (uint)SoundEffect.Device().DeviceDetails.OutputFormat.Format.nChannels;
			int num = (int)(4U * this.dspSettings.SrcChannelCount * this.dspSettings.DstChannelCount);
			this.dspSettings.pMatrixCoefficients = FNAPlatform.Malloc(num);
			byte* ptr = (byte*)(void*)this.dspSettings.pMatrixCoefficients;
			for (int i = 0; i < num; i++)
			{
				ptr[i] = 0;
			}
			this.SetPanMatrixCoefficients();
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0003EF58 File Offset: 0x0003D158
		internal unsafe void INTERNAL_applyReverb(float rvGain)
		{
			if (this.handle == IntPtr.Zero)
			{
				return;
			}
			if (!this.usingReverb)
			{
				SoundEffect.Device().AttachReverb(this.handle);
				this.usingReverb = true;
			}
			float* ptr = (float*)(void*)this.dspSettings.pMatrixCoefficients;
			*ptr = rvGain;
			if (this.dspSettings.SrcChannelCount == 2U)
			{
				ptr[1] = rvGain;
			}
			FAudio.FAudioVoice_SetOutputMatrix(this.handle, SoundEffect.Device().ReverbVoice, this.dspSettings.SrcChannelCount, 1U, this.dspSettings.pMatrixCoefficients, 0U);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0003EFEC File Offset: 0x0003D1EC
		internal void INTERNAL_applyLowPassFilter(float cutoff)
		{
			if (this.handle == IntPtr.Zero)
			{
				return;
			}
			FAudio.FAudioFilterParameters faudioFilterParameters = default(FAudio.FAudioFilterParameters);
			faudioFilterParameters.Type = FAudio.FAudioFilterType.FAudioLowPassFilter;
			faudioFilterParameters.Frequency = cutoff;
			faudioFilterParameters.OneOverQ = 1f;
			FAudio.FAudioVoice_SetFilterParameters(this.handle, ref faudioFilterParameters, 0U);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0003F040 File Offset: 0x0003D240
		internal void INTERNAL_applyHighPassFilter(float cutoff)
		{
			if (this.handle == IntPtr.Zero)
			{
				return;
			}
			FAudio.FAudioFilterParameters faudioFilterParameters = default(FAudio.FAudioFilterParameters);
			faudioFilterParameters.Type = FAudio.FAudioFilterType.FAudioHighPassFilter;
			faudioFilterParameters.Frequency = cutoff;
			faudioFilterParameters.OneOverQ = 1f;
			FAudio.FAudioVoice_SetFilterParameters(this.handle, ref faudioFilterParameters, 0U);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0003F094 File Offset: 0x0003D294
		internal void INTERNAL_applyBandPassFilter(float center)
		{
			if (this.handle == IntPtr.Zero)
			{
				return;
			}
			FAudio.FAudioFilterParameters faudioFilterParameters = default(FAudio.FAudioFilterParameters);
			faudioFilterParameters.Type = FAudio.FAudioFilterType.FAudioBandPassFilter;
			faudioFilterParameters.Frequency = center;
			faudioFilterParameters.OneOverQ = 1f;
			FAudio.FAudioVoice_SetFilterParameters(this.handle, ref faudioFilterParameters, 0U);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0003F0E8 File Offset: 0x0003D2E8
		private void UpdatePitch()
		{
			float dopplerScale = SoundEffect.Device().DopplerScale;
			float num;
			if (!this.is3D || dopplerScale == 0f)
			{
				num = 1f;
			}
			else
			{
				num = this.dspSettings.DopplerFactor * dopplerScale;
			}
			FAudio.FAudioSourceVoice_SetFrequencyRatio(this.handle, (float)Math.Pow(2.0, (double)this.INTERNAL_pitch) * num, 0U);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0003F14C File Offset: 0x0003D34C
		private unsafe void SetPanMatrixCoefficients()
		{
			float* ptr = (float*)(void*)this.dspSettings.pMatrixCoefficients;
			if (this.dspSettings.SrcChannelCount == 1U)
			{
				if (this.dspSettings.DstChannelCount == 1U)
				{
					*ptr = 1f;
					return;
				}
				*ptr = ((this.INTERNAL_pan > 0f) ? (1f - this.INTERNAL_pan) : 1f);
				ptr[1] = ((this.INTERNAL_pan < 0f) ? (1f + this.INTERNAL_pan) : 1f);
				return;
			}
			else
			{
				if (this.dspSettings.DstChannelCount == 1U)
				{
					*ptr = 1f;
					ptr[1] = 1f;
					return;
				}
				if (this.INTERNAL_pan <= 0f)
				{
					*ptr = 0.5f * this.INTERNAL_pan + 1f;
					ptr[1] = 0.5f * -this.INTERNAL_pan;
					ptr[2] = 0f;
					ptr[3] = this.INTERNAL_pan + 1f;
					return;
				}
				*ptr = -this.INTERNAL_pan + 1f;
				ptr[1] = 0f;
				ptr[2] = 0.5f * this.INTERNAL_pan;
				ptr[3] = 0.5f * -this.INTERNAL_pan + 1f;
				return;
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0003F28B File Offset: 0x0003D48B
		// Note: this type is marked as 'beforefieldinit'.
		static SoundEffectInstance()
		{
		}

		// Token: 0x04000B28 RID: 2856
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x04000B29 RID: 2857
		private bool INTERNAL_looped;

		// Token: 0x04000B2A RID: 2858
		private float INTERNAL_pan;

		// Token: 0x04000B2B RID: 2859
		private float INTERNAL_pitch;

		// Token: 0x04000B2C RID: 2860
		private SoundState INTERNAL_state = SoundState.Stopped;

		// Token: 0x04000B2D RID: 2861
		private float INTERNAL_volume = 1f;

		// Token: 0x04000B2E RID: 2862
		internal IntPtr handle;

		// Token: 0x04000B2F RID: 2863
		internal bool isDynamic;

		// Token: 0x04000B30 RID: 2864
		private SoundEffect parentEffect;

		// Token: 0x04000B31 RID: 2865
		private WeakReference selfReference;

		// Token: 0x04000B32 RID: 2866
		private bool hasStarted;

		// Token: 0x04000B33 RID: 2867
		private bool is3D;

		// Token: 0x04000B34 RID: 2868
		private bool usingReverb;

		// Token: 0x04000B35 RID: 2869
		private FAudio.F3DAUDIO_DSP_SETTINGS dspSettings;

		// Token: 0x04000B36 RID: 2870
		private static readonly float maxFreqRatio = ((Environment.GetEnvironmentVariable("FNA_SOUNDEFFECT_UNCAPPED_PITCH") == "1") ? 1024f : 2f);
	}
}
