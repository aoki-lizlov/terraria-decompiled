using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ObjCRuntime;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x0200014F RID: 335
	public class AudioEngine : IDisposable
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0003C403 File Offset: 0x0003A603
		public ReadOnlyCollection<RendererDetail> RendererDetails
		{
			get
			{
				return new ReadOnlyCollection<RendererDetail>(this.rendererDetails);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x0003C410 File Offset: 0x0003A610
		// (set) Token: 0x060017E2 RID: 6114 RVA: 0x0003C418 File Offset: 0x0003A618
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

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060017E3 RID: 6115 RVA: 0x0003C424 File Offset: 0x0003A624
		// (remove) Token: 0x060017E4 RID: 6116 RVA: 0x0003C45C File Offset: 0x0003A65C
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

		// Token: 0x060017E5 RID: 6117 RVA: 0x0003C491 File Offset: 0x0003A691
		public AudioEngine(string settingsFile)
			: this(settingsFile, new TimeSpan(0, 0, 0, 0, 250), null)
		{
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0003C4AC File Offset: 0x0003A6AC
		public unsafe AudioEngine(string settingsFile, TimeSpan lookAheadTime, string rendererId)
		{
			if (string.IsNullOrEmpty(settingsFile))
			{
				throw new ArgumentNullException("settingsFile");
			}
			FAudio.FACTCreateEngine(0U, out this.handle);
			ushort num;
			FAudio.FACTAudioEngine_GetRendererCount(this.handle, out num);
			if (num == 0)
			{
				FAudio.FACTAudioEngine_Release(this.handle);
				throw new NoAudioHardwareException();
			}
			this.rendererDetails = new RendererDetail[(int)num];
			byte[] array = new byte[510];
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				FAudio.FACTRendererDetails factrendererDetails;
				FAudio.FACTAudioEngine_GetRendererDetails(this.handle, num2, out factrendererDetails);
				Marshal.Copy((IntPtr)((void*)(&factrendererDetails.displayName.FixedElementField)), array, 0, array.Length);
				string text = Encoding.Unicode.GetString(array).TrimEnd(new char[1]);
				Marshal.Copy((IntPtr)((void*)(&factrendererDetails.rendererID.FixedElementField)), array, 0, array.Length);
				string text2 = Encoding.Unicode.GetString(array).TrimEnd(new char[1]);
				this.rendererDetails[(int)num2] = new RendererDetail(text, text2);
			}
			IntPtr intPtr2;
			IntPtr intPtr = TitleContainer.ReadToPointer(settingsFile, out intPtr2);
			FAudio.FACTRuntimeParameters factruntimeParameters = default(FAudio.FACTRuntimeParameters);
			factruntimeParameters.pGlobalSettingsBuffer = intPtr;
			factruntimeParameters.globalSettingsBufferSize = (uint)(int)intPtr2;
			factruntimeParameters.globalSettingsFlags = 1U;
			this.xactNotificationFunc = new FAudio.FACTNotificationCallback(AudioEngine.OnXACTNotification);
			factruntimeParameters.fnNotificationCallback = Marshal.GetFunctionPointerForDelegate(this.xactNotificationFunc);
			factruntimeParameters.lookAheadTime = (uint)lookAheadTime.Milliseconds;
			if (!string.IsNullOrEmpty(rendererId))
			{
				factruntimeParameters.pRendererID = Marshal.StringToHGlobalAuto(rendererId);
			}
			if (FAudio.FACTAudioEngine_Initialize(this.handle, ref factruntimeParameters) != 0U)
			{
				throw new InvalidOperationException("Engine initialization failed!");
			}
			if (factruntimeParameters.pRendererID != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(factruntimeParameters.pRendererID);
			}
			this.handle3D = new byte[20];
			FAudio.FACT3DInitialize(this.handle, this.handle3D);
			FAudio.FAudioWaveFormatExtensible faudioWaveFormatExtensible;
			FAudio.FACTAudioEngine_GetFinalMixFormat(this.handle, out faudioWaveFormatExtensible);
			this.channels = faudioWaveFormatExtensible.Format.nChannels;
			this.notificationDesc = default(FAudio.FACTNotificationDescription);
			this.notificationDesc.flags = 1;
			this.notificationDesc.type = 7;
			FAudio.FACTAudioEngine_RegisterNotification(this.handle, ref this.notificationDesc);
			this.notificationDesc.type = 6;
			FAudio.FACTAudioEngine_RegisterNotification(this.handle, ref this.notificationDesc);
			this.notificationDesc.type = 4;
			FAudio.FACTAudioEngine_RegisterNotification(this.handle, ref this.notificationDesc);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0003C728 File Offset: 0x0003A928
		static AudioEngine()
		{
			AppDomain.CurrentDomain.ProcessExit += AudioEngine.ProgramExit;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0003C760 File Offset: 0x0003A960
		~AudioEngine()
		{
			this.Dispose(false);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0003C790 File Offset: 0x0003A990
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0003C7A0 File Offset: 0x0003A9A0
		public AudioCategory GetCategory(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			ushort num = FAudio.FACTAudioEngine_GetCategory(this.handle, name);
			if (num == 65535)
			{
				throw new InvalidOperationException("Invalid category name!");
			}
			return new AudioCategory(this, num, name);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0003C7E8 File Offset: 0x0003A9E8
		public float GetGlobalVariable(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			ushort num = FAudio.FACTAudioEngine_GetGlobalVariableIndex(this.handle, name);
			if (num == 65535)
			{
				throw new InvalidOperationException("Invalid variable name!");
			}
			float num2;
			FAudio.FACTAudioEngine_GetGlobalVariable(this.handle, num, out num2);
			return num2;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0003C838 File Offset: 0x0003AA38
		public void SetGlobalVariable(string name, float value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			ushort num = FAudio.FACTAudioEngine_GetGlobalVariableIndex(this.handle, name);
			if (num == 65535)
			{
				throw new InvalidOperationException("Invalid variable name!");
			}
			FAudio.FACTAudioEngine_SetGlobalVariable(this.handle, num, value);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0003C886 File Offset: 0x0003AA86
		public void Update()
		{
			FAudio.FACTAudioEngine_DoWork(this.handle);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0003C894 File Offset: 0x0003AA94
		protected virtual void Dispose(bool disposing)
		{
			object obj = this.gcSync;
			lock (obj)
			{
				if (!this.IsDisposed)
				{
					if (this.Disposing != null)
					{
						this.Disposing(this, null);
					}
					FAudio.FACTAudioEngine_ShutDown(this.handle);
					FAudio.FACTAudioEngine_Release(this.handle);
					this.rendererDetails = null;
					this.IsDisposed = true;
				}
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0003C914 File Offset: 0x0003AB14
		internal void RegisterPointer(IntPtr ptr, WeakReference reference)
		{
			Dictionary<IntPtr, WeakReference> dictionary = AudioEngine.xactPtrs;
			lock (dictionary)
			{
				AudioEngine.xactPtrs[ptr] = reference;
			}
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0003C95C File Offset: 0x0003AB5C
		[MonoPInvokeCallback(typeof(FAudio.FACTNotificationCallback))]
		private unsafe static void OnXACTNotification(IntPtr notification)
		{
			FAudio.FACTNotification* ptr = (FAudio.FACTNotification*)(void*)notification;
			if (ptr->type == 7)
			{
				IntPtr pWaveBank = ptr->anon.waveBank.pWaveBank;
				Dictionary<IntPtr, WeakReference> dictionary = AudioEngine.xactPtrs;
				lock (dictionary)
				{
					WeakReference weakReference;
					if (AudioEngine.xactPtrs.TryGetValue(pWaveBank, out weakReference) && weakReference.IsAlive)
					{
						(weakReference.Target as WaveBank).OnWaveBankDestroyed();
					}
					AudioEngine.xactPtrs.Remove(pWaveBank);
					return;
				}
			}
			if (ptr->type == 6)
			{
				IntPtr pSoundBank = ptr->anon.soundBank.pSoundBank;
				Dictionary<IntPtr, WeakReference> dictionary = AudioEngine.xactPtrs;
				lock (dictionary)
				{
					WeakReference weakReference;
					if (AudioEngine.xactPtrs.TryGetValue(pSoundBank, out weakReference) && weakReference.IsAlive)
					{
						(weakReference.Target as SoundBank).OnSoundBankDestroyed();
					}
					AudioEngine.xactPtrs.Remove(pSoundBank);
					return;
				}
			}
			if (ptr->type == 4)
			{
				IntPtr pCue = ptr->anon.cue.pCue;
				Dictionary<IntPtr, WeakReference> dictionary = AudioEngine.xactPtrs;
				lock (dictionary)
				{
					WeakReference weakReference;
					if (AudioEngine.xactPtrs.TryGetValue(pCue, out weakReference) && weakReference.IsAlive)
					{
						(weakReference.Target as Cue).OnCueDestroyed();
					}
					AudioEngine.xactPtrs.Remove(pCue);
				}
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0003CAE4 File Offset: 0x0003ACE4
		internal static void ProgramExit(object sender, EventArgs e)
		{
			AudioEngine.ProgramExiting = true;
		}

		// Token: 0x04000AEE RID: 2798
		public const int ContentVersion = 46;

		// Token: 0x04000AEF RID: 2799
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x04000AF0 RID: 2800
		internal readonly IntPtr handle;

		// Token: 0x04000AF1 RID: 2801
		internal readonly byte[] handle3D;

		// Token: 0x04000AF2 RID: 2802
		internal readonly ushort channels;

		// Token: 0x04000AF3 RID: 2803
		internal readonly object gcSync = new object();

		// Token: 0x04000AF4 RID: 2804
		private RendererDetail[] rendererDetails;

		// Token: 0x04000AF5 RID: 2805
		private readonly FAudio.FACTNotificationCallback xactNotificationFunc;

		// Token: 0x04000AF6 RID: 2806
		private FAudio.FACTNotificationDescription notificationDesc;

		// Token: 0x04000AF7 RID: 2807
		private static readonly AudioEngine.IntPtrComparer comparer = new AudioEngine.IntPtrComparer();

		// Token: 0x04000AF8 RID: 2808
		private static readonly Dictionary<IntPtr, WeakReference> xactPtrs = new Dictionary<IntPtr, WeakReference>(AudioEngine.comparer);

		// Token: 0x04000AF9 RID: 2809
		internal static bool ProgramExiting = false;

		// Token: 0x04000AFA RID: 2810
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposing;

		// Token: 0x020003E4 RID: 996
		private class IntPtrComparer : IEqualityComparer<IntPtr>
		{
			// Token: 0x06001B12 RID: 6930 RVA: 0x0003F791 File Offset: 0x0003D991
			public bool Equals(IntPtr x, IntPtr y)
			{
				return x == y;
			}

			// Token: 0x06001B13 RID: 6931 RVA: 0x0003F79A File Offset: 0x0003D99A
			public int GetHashCode(IntPtr obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x06001B14 RID: 6932 RVA: 0x000136F5 File Offset: 0x000118F5
			public IntPtrComparer()
			{
			}
		}
	}
}
