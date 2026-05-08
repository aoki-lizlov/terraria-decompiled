using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x0200015B RID: 347
	public sealed class SoundEffect : IDisposable
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x0003DF32 File Offset: 0x0003C132
		public TimeSpan Duration
		{
			get
			{
				return TimeSpan.FromSeconds(this.handle.PlayLength / this.sampleRate);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0003DF4F File Offset: 0x0003C14F
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x0003DF57 File Offset: 0x0003C157
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0003DF60 File Offset: 0x0003C160
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x0003DF68 File Offset: 0x0003C168
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0003DF74 File Offset: 0x0003C174
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x0003DF93 File Offset: 0x0003C193
		public static float MasterVolume
		{
			get
			{
				float num;
				FAudio.FAudioVoice_GetVolume(SoundEffect.Device().MasterVoice, out num);
				return num;
			}
			set
			{
				FAudio.FAudioVoice_SetVolume(SoundEffect.Device().MasterVoice, value, 0U);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0003DFA7 File Offset: 0x0003C1A7
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x0003DFB3 File Offset: 0x0003C1B3
		public static float DistanceScale
		{
			get
			{
				return SoundEffect.Device().CurveDistanceScaler;
			}
			set
			{
				if (value <= 0f)
				{
					throw new ArgumentOutOfRangeException("value <= 0.0f");
				}
				SoundEffect.Device().CurveDistanceScaler = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0003DFD3 File Offset: 0x0003C1D3
		// (set) Token: 0x06001862 RID: 6242 RVA: 0x0003DFDF File Offset: 0x0003C1DF
		public static float DopplerScale
		{
			get
			{
				return SoundEffect.Device().DopplerScale;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException("value < 0.0f");
				}
				SoundEffect.Device().DopplerScale = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0003DFFF File Offset: 0x0003C1FF
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x0003E00C File Offset: 0x0003C20C
		public static float SpeedOfSound
		{
			get
			{
				return SoundEffect.Device().SpeedOfSound;
			}
			set
			{
				SoundEffect.FAudioContext faudioContext = SoundEffect.Device();
				faudioContext.SpeedOfSound = value;
				FAudio.F3DAudioInitialize(faudioContext.DeviceDetails.OutputFormat.dwChannelMask, faudioContext.SpeedOfSound, faudioContext.Handle3D);
			}
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0003E048 File Offset: 0x0003C248
		public SoundEffect(byte[] buffer, int sampleRate, AudioChannels channels)
			: this(null, buffer, 0, buffer.Length, null, 1, (ushort)channels, (uint)sampleRate, (uint)(sampleRate * (int)((ushort)channels * 2)), (ushort)channels * 2, 16, 0, 0)
		{
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0003E078 File Offset: 0x0003C278
		public SoundEffect(byte[] buffer, int offset, int count, int sampleRate, AudioChannels channels, int loopStart, int loopLength)
			: this(null, buffer, offset, count, null, 1, (ushort)channels, (uint)sampleRate, (uint)(sampleRate * (int)((ushort)channels * 2)), (ushort)channels * 2, 16, loopStart, loopLength)
		{
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0003E0AC File Offset: 0x0003C2AC
		internal unsafe SoundEffect(string name, byte[] buffer, int offset, int count, byte[] extraData, ushort wFormatTag, ushort nChannels, uint nSamplesPerSec, uint nAvgBytesPerSec, ushort nBlockAlign, ushort wBitsPerSample, int loopStart, int loopLength)
		{
			SoundEffect.Device();
			this.Name = name;
			this.channels = nChannels;
			this.sampleRate = nSamplesPerSec;
			this.loopStart = (uint)loopStart;
			this.loopLength = (uint)loopLength;
			if (extraData == null)
			{
				this.formatPtr = FNAPlatform.Malloc(MarshalHelper.SizeOf<FAudio.FAudioWaveFormatEx>());
			}
			else
			{
				this.formatPtr = FNAPlatform.Malloc(MarshalHelper.SizeOf<FAudio.FAudioWaveFormatEx>() + extraData.Length);
				Marshal.Copy(extraData, 0, this.formatPtr + MarshalHelper.SizeOf<FAudio.FAudioWaveFormatEx>(), extraData.Length);
			}
			FAudio.FAudioWaveFormatEx* ptr = (FAudio.FAudioWaveFormatEx*)(void*)this.formatPtr;
			ptr->wFormatTag = wFormatTag;
			ptr->nChannels = nChannels;
			ptr->nSamplesPerSec = nSamplesPerSec;
			ptr->nAvgBytesPerSec = nAvgBytesPerSec;
			ptr->nBlockAlign = nBlockAlign;
			ptr->wBitsPerSample = wBitsPerSample;
			ptr->cbSize = (ushort)((extraData == null) ? 0 : extraData.Length);
			this.handle = default(FAudio.FAudioBuffer);
			this.handle.Flags = 64U;
			this.handle.pContext = IntPtr.Zero;
			this.handle.AudioBytes = (uint)count;
			this.handle.pAudioData = FNAPlatform.Malloc(count);
			Marshal.Copy(buffer, offset, this.handle.pAudioData, count);
			this.handle.PlayBegin = 0U;
			if (wFormatTag == 1)
			{
				this.handle.PlayLength = (uint)(count / (int)nChannels / (int)(wBitsPerSample / 8));
			}
			else if (wFormatTag == 2)
			{
				this.handle.PlayLength = (uint)(count / (int)nBlockAlign * (int)((nBlockAlign / nChannels - 6) * 2));
			}
			else if (wFormatTag == 358)
			{
				FAudio.FAudioXMA2WaveFormatEx* ptr2 = (FAudio.FAudioXMA2WaveFormatEx*)(void*)this.formatPtr;
				this.handle.PlayLength = ptr2->dwPlayLength;
			}
			this.handle.LoopBegin = 0U;
			this.handle.LoopLength = 0U;
			this.handle.LoopCount = 0U;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0003E28C File Offset: 0x0003C48C
		~SoundEffect()
		{
			if (!SoundEffect.FAudioContext.ProgramExiting && this.Instances.Count > 0)
			{
				GC.ReRegisterForFinalize(this);
			}
			else
			{
				this.Dispose();
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0003E2D8 File Offset: 0x0003C4D8
		public void Dispose()
		{
			if (!this.IsDisposed)
			{
				WeakReference[] array = this.Instances.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					object target = array[i].Target;
					if (target != null)
					{
						(target as IDisposable).Dispose();
					}
				}
				this.Instances.Clear();
				FNAPlatform.Free(this.formatPtr);
				FNAPlatform.Free(this.handle.pAudioData);
				this.IsDisposed = true;
			}
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0003E355 File Offset: 0x0003C555
		public bool Play()
		{
			return this.Play(1f, 0f, 0f);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0003E36C File Offset: 0x0003C56C
		public bool Play(float volume, float pitch, float pan)
		{
			SoundEffectInstance soundEffectInstance = new SoundEffectInstance(this);
			soundEffectInstance.Volume = volume;
			soundEffectInstance.Pitch = pitch;
			soundEffectInstance.Pan = pan;
			soundEffectInstance.Play();
			if (soundEffectInstance.State != SoundState.Playing)
			{
				soundEffectInstance.Dispose();
				return false;
			}
			return true;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0003E3AC File Offset: 0x0003C5AC
		public SoundEffectInstance CreateInstance()
		{
			return new SoundEffectInstance(this);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0003E3B4 File Offset: 0x0003C5B4
		public static TimeSpan GetSampleDuration(int sizeInBytes, int sampleRate, AudioChannels channels)
		{
			sizeInBytes /= 2;
			int num = (int)((float)(sizeInBytes / (int)channels) / ((float)sampleRate / 1000f));
			return new TimeSpan(0, 0, 0, 0, num);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0003E3DF File Offset: 0x0003C5DF
		public static int GetSampleSizeInBytes(TimeSpan duration, int sampleRate, AudioChannels channels)
		{
			return (int)(duration.TotalSeconds * (double)sampleRate * (double)channels * 2.0);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0003E3FC File Offset: 0x0003C5FC
		public static SoundEffect FromStream(Stream stream)
		{
			int num = 0;
			int num2 = 0;
			ushort num4;
			ushort num5;
			uint num6;
			uint num7;
			ushort num8;
			ushort num9;
			byte[] array;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				if (new string(binaryReader.ReadChars(4)) != "RIFF")
				{
					throw new NotSupportedException("Specified stream is not a wave file.");
				}
				binaryReader.ReadUInt32();
				if (new string(binaryReader.ReadChars(4)) != "WAVE")
				{
					throw new NotSupportedException("Specified stream is not a wave file.");
				}
				string text = new string(binaryReader.ReadChars(4));
				while (text != "fmt ")
				{
					binaryReader.ReadBytes(binaryReader.ReadInt32());
					text = new string(binaryReader.ReadChars(4));
				}
				int num3 = binaryReader.ReadInt32();
				num4 = binaryReader.ReadUInt16();
				num5 = binaryReader.ReadUInt16();
				num6 = binaryReader.ReadUInt32();
				num7 = binaryReader.ReadUInt32();
				num8 = binaryReader.ReadUInt16();
				num9 = binaryReader.ReadUInt16();
				if (num3 > 16)
				{
					binaryReader.ReadBytes(num3 - 16);
				}
				string text2 = new string(binaryReader.ReadChars(4));
				while (text2.ToLowerInvariant() != "data")
				{
					binaryReader.ReadBytes(binaryReader.ReadInt32());
					text2 = new string(binaryReader.ReadChars(4));
				}
				if (text2 != "data")
				{
					throw new NotSupportedException("Specified wave file is not supported.");
				}
				int num10 = binaryReader.ReadInt32();
				array = binaryReader.ReadBytes(num10);
				while (binaryReader.PeekChar() != -1)
				{
					char[] array2 = binaryReader.ReadChars(4);
					if (array2.Length < 4)
					{
						break;
					}
					byte[] array3 = binaryReader.ReadBytes(4);
					if (array3.Length < 4)
					{
						break;
					}
					string text3 = new string(array2);
					int num11 = BitConverter.ToInt32(array3, 0);
					if (text3 == "smpl")
					{
						binaryReader.ReadUInt32();
						binaryReader.ReadUInt32();
						binaryReader.ReadUInt32();
						binaryReader.ReadUInt32();
						binaryReader.ReadUInt32();
						binaryReader.ReadUInt32();
						binaryReader.ReadUInt32();
						uint num12 = binaryReader.ReadUInt32();
						int num13 = binaryReader.ReadInt32();
						int num14 = 0;
						while ((long)num14 < (long)((ulong)num12))
						{
							binaryReader.ReadUInt32();
							binaryReader.ReadUInt32();
							int num15 = binaryReader.ReadInt32();
							int num16 = binaryReader.ReadInt32();
							binaryReader.ReadUInt32();
							binaryReader.ReadUInt32();
							if (num14 == 0)
							{
								num = num15;
								num2 = num16;
							}
							num14++;
						}
						if (num13 != 0)
						{
							binaryReader.ReadBytes(num13);
						}
					}
					else
					{
						binaryReader.ReadBytes(num11);
					}
				}
			}
			return new SoundEffect(null, array, 0, array.Length, null, num4, num5, num6, num7, num8, num9, num, num2 - num);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0003E6BC File Offset: 0x0003C8BC
		internal static SoundEffect.FAudioContext Device()
		{
			if (SoundEffect.FAudioContext.Context != null)
			{
				return SoundEffect.FAudioContext.Context;
			}
			object obj = SoundEffect.createLock;
			lock (obj)
			{
				if (SoundEffect.FAudioContext.Context != null)
				{
					return SoundEffect.FAudioContext.Context;
				}
				SoundEffect.FAudioContext.Create();
				if (SoundEffect.FAudioContext.Context == null)
				{
					throw new NoAudioHardwareException();
				}
			}
			return SoundEffect.FAudioContext.Context;
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0003E72C File Offset: 0x0003C92C
		// Note: this type is marked as 'beforefieldinit'.
		static SoundEffect()
		{
		}

		// Token: 0x04000B1E RID: 2846
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x04000B1F RID: 2847
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000B20 RID: 2848
		internal List<WeakReference> Instances = new List<WeakReference>();

		// Token: 0x04000B21 RID: 2849
		internal FAudio.FAudioBuffer handle;

		// Token: 0x04000B22 RID: 2850
		internal IntPtr formatPtr;

		// Token: 0x04000B23 RID: 2851
		internal ushort channels;

		// Token: 0x04000B24 RID: 2852
		internal uint sampleRate;

		// Token: 0x04000B25 RID: 2853
		internal uint loopStart;

		// Token: 0x04000B26 RID: 2854
		internal uint loopLength;

		// Token: 0x04000B27 RID: 2855
		private static readonly object createLock = new object();

		// Token: 0x020003E5 RID: 997
		internal class FAudioContext
		{
			// Token: 0x06001B15 RID: 6933 RVA: 0x0003FBEC File Offset: 0x0003DDEC
			private FAudioContext(IntPtr ctx, uint devices)
			{
				this.Handle = ctx;
				uint num;
				for (num = 0U; num < devices; num += 1U)
				{
					FAudio.FAudio_GetDeviceDetails(this.Handle, num, out this.DeviceDetails);
					if ((this.DeviceDetails.Role & FAudio.FAudioDeviceRole.FAudioDefaultGameDevice) == FAudio.FAudioDeviceRole.FAudioDefaultGameDevice)
					{
						break;
					}
				}
				if (num == devices)
				{
					num = 0U;
					FAudio.FAudio_GetDeviceDetails(this.Handle, num, out this.DeviceDetails);
				}
				if (FAudio.FAudio_CreateMasteringVoice(this.Handle, out this.MasterVoice, 0U, 0U, 0U, num, IntPtr.Zero) != 0U)
				{
					FAudio.FAudio_Release(ctx);
					this.Handle = IntPtr.Zero;
					FNALoggerEXT.LogError("Failed to create mastering voice!");
					return;
				}
				this.CurveDistanceScaler = 1f;
				this.DopplerScale = 1f;
				this.SpeedOfSound = 343.5f;
				this.Handle3D = new byte[20];
				FAudio.F3DAudioInitialize(this.DeviceDetails.OutputFormat.dwChannelMask, this.SpeedOfSound, this.Handle3D);
				SoundEffect.FAudioContext.Context = this;
			}

			// Token: 0x06001B16 RID: 6934 RVA: 0x0003FCE4 File Offset: 0x0003DEE4
			public void Dispose()
			{
				if (this.ReverbVoice != IntPtr.Zero)
				{
					FAudio.FAudioVoice_DestroyVoice(this.ReverbVoice);
					this.ReverbVoice = IntPtr.Zero;
					FNAPlatform.Free(this.reverbSends.pSends);
				}
				if (this.MasterVoice != IntPtr.Zero)
				{
					FAudio.FAudioVoice_DestroyVoice(this.MasterVoice);
				}
				if (this.Handle != IntPtr.Zero)
				{
					FAudio.FAudio_Release(this.Handle);
				}
			}

			// Token: 0x06001B17 RID: 6935 RVA: 0x0003FD6C File Offset: 0x0003DF6C
			public unsafe void AttachReverb(IntPtr voice)
			{
				if (this.ReverbVoice == IntPtr.Zero)
				{
					IntPtr intPtr;
					FAudio.FAudioCreateReverb(out intPtr, 0U);
					IntPtr intPtr2 = FNAPlatform.Malloc(MarshalHelper.SizeOf<FAudio.FAudioEffectChain>());
					FAudio.FAudioEffectChain* ptr = (FAudio.FAudioEffectChain*)(void*)intPtr2;
					ptr->EffectCount = 1U;
					ptr->pEffectDescriptors = FNAPlatform.Malloc(MarshalHelper.SizeOf<FAudio.FAudioEffectDescriptor>());
					FAudio.FAudioEffectDescriptor* ptr2 = (FAudio.FAudioEffectDescriptor*)(void*)ptr->pEffectDescriptors;
					ptr2->InitialState = 1;
					ptr2->OutputChannels = ((this.DeviceDetails.OutputFormat.Format.nChannels == 6) ? 6U : 1U);
					ptr2->pEffect = intPtr;
					FAudio.FAudio_CreateSubmixVoice(this.Handle, out this.ReverbVoice, 1U, this.DeviceDetails.OutputFormat.Format.nSamplesPerSec, 0U, 0U, IntPtr.Zero, intPtr2);
					FAudio.FAPOBase_Release(intPtr);
					FNAPlatform.Free(ptr->pEffectDescriptors);
					FNAPlatform.Free(intPtr2);
					IntPtr intPtr3 = FNAPlatform.Malloc(MarshalHelper.SizeOf<FAudio.FAudioFXReverbParameters>());
					FAudio.FAudioFXReverbParameters* ptr3 = (FAudio.FAudioFXReverbParameters*)(void*)intPtr3;
					ptr3->WetDryMix = 100f;
					ptr3->ReflectionsDelay = 7U;
					ptr3->ReverbDelay = 11;
					ptr3->RearDelay = 5;
					ptr3->PositionLeft = 6;
					ptr3->PositionRight = 6;
					ptr3->PositionMatrixLeft = 27;
					ptr3->PositionMatrixRight = 27;
					ptr3->EarlyDiffusion = 15;
					ptr3->LateDiffusion = 15;
					ptr3->LowEQGain = 8;
					ptr3->LowEQCutoff = 4;
					ptr3->HighEQGain = 8;
					ptr3->HighEQCutoff = 6;
					ptr3->RoomFilterFreq = 5000f;
					ptr3->RoomFilterMain = -10f;
					ptr3->RoomFilterHF = -1f;
					ptr3->ReflectionsGain = -26.02f;
					ptr3->ReverbGain = 10f;
					ptr3->DecayTime = 1.49f;
					ptr3->Density = 100f;
					ptr3->RoomSize = 100f;
					FAudio.FAudioVoice_SetEffectParameters(this.ReverbVoice, 0U, intPtr3, (uint)MarshalHelper.SizeOf<FAudio.FAudioFXReverbParameters>(), 0U);
					FNAPlatform.Free(intPtr3);
					this.reverbSends = default(FAudio.FAudioVoiceSends);
					this.reverbSends.SendCount = 2U;
					this.reverbSends.pSends = FNAPlatform.Malloc(2 * MarshalHelper.SizeOf<FAudio.FAudioSendDescriptor>());
					FAudio.FAudioSendDescriptor* ptr4 = (FAudio.FAudioSendDescriptor*)(void*)this.reverbSends.pSends;
					ptr4->Flags = 0U;
					ptr4->pOutputVoice = this.MasterVoice;
					ptr4[1].Flags = 0U;
					ptr4[1].pOutputVoice = this.ReverbVoice;
				}
				FAudio.FAudioVoice_SetOutputVoices(voice, ref this.reverbSends);
			}

			// Token: 0x06001B18 RID: 6936 RVA: 0x0003FFF8 File Offset: 0x0003E1F8
			public static void Create()
			{
				IntPtr intPtr;
				try
				{
					FAudio.FAudioCreate(out intPtr, 0U, uint.MaxValue);
				}
				catch (Exception ex)
				{
					FNALoggerEXT.LogWarn("FAudio failed to load: " + ex.ToString());
					return;
				}
				uint num;
				FAudio.FAudio_GetDeviceCount(intPtr, out num);
				if (num == 0U)
				{
					FAudio.FAudio_Release(intPtr);
					return;
				}
				SoundEffect.FAudioContext faudioContext = new SoundEffect.FAudioContext(intPtr, num);
				if (faudioContext.Handle == IntPtr.Zero)
				{
					faudioContext.Dispose();
					return;
				}
				SoundEffect.FAudioContext.Context = faudioContext;
				AppDomain.CurrentDomain.ProcessExit += SoundEffect.FAudioContext.ProgramExit;
			}

			// Token: 0x06001B19 RID: 6937 RVA: 0x00040090 File Offset: 0x0003E290
			private static void ProgramExit(object sender, EventArgs e)
			{
				SoundEffect.FAudioContext.ProgramExiting = true;
				if (SoundEffect.FAudioContext.Context != null)
				{
					GC.Collect();
					SoundEffect.FAudioContext.Context.Dispose();
				}
			}

			// Token: 0x04001E12 RID: 7698
			public static SoundEffect.FAudioContext Context;

			// Token: 0x04001E13 RID: 7699
			public static bool ProgramExiting;

			// Token: 0x04001E14 RID: 7700
			public readonly IntPtr Handle;

			// Token: 0x04001E15 RID: 7701
			public readonly byte[] Handle3D;

			// Token: 0x04001E16 RID: 7702
			public readonly IntPtr MasterVoice;

			// Token: 0x04001E17 RID: 7703
			public readonly FAudio.FAudioDeviceDetails DeviceDetails;

			// Token: 0x04001E18 RID: 7704
			public float CurveDistanceScaler;

			// Token: 0x04001E19 RID: 7705
			public float DopplerScale;

			// Token: 0x04001E1A RID: 7706
			public float SpeedOfSound;

			// Token: 0x04001E1B RID: 7707
			public IntPtr ReverbVoice;

			// Token: 0x04001E1C RID: 7708
			private FAudio.FAudioVoiceSends reverbSends;
		}
	}
}
