using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000002 RID: 2
public static class FAudio
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private static int Utf8Size(string str)
	{
		return str.Length * 4 + 1;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
	private unsafe static byte* Utf8Encode(string str, byte* buffer, int bufferSize)
	{
		fixed (string text = str)
		{
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			Encoding.UTF8.GetBytes(ptr, str.Length + 1, buffer, bufferSize);
		}
		return buffer;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
	private unsafe static byte* Utf8Encode(string str)
	{
		int num = str.Length * 4 + 1;
		byte* ptr = (byte*)(void*)Marshal.AllocHGlobal(num);
		fixed (string text = str)
		{
			char* ptr2 = text;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			Encoding.UTF8.GetBytes(ptr2, str.Length + 1, ptr, num);
		}
		return ptr;
	}

	// Token: 0x06000004 RID: 4
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioLinkedVersion();

	// Token: 0x06000005 RID: 5
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioCreate(out IntPtr ppFAudio, uint Flags, uint XAudio2Processor);

	// Token: 0x06000006 RID: 6
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_AddRef(IntPtr audio);

	// Token: 0x06000007 RID: 7
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_Release(IntPtr audio);

	// Token: 0x06000008 RID: 8
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_GetDeviceCount(IntPtr audio, out uint pCount);

	// Token: 0x06000009 RID: 9
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_GetDeviceDetails(IntPtr audio, uint Index, out FAudio.FAudioDeviceDetails pDeviceDetails);

	// Token: 0x0600000A RID: 10
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_Initialize(IntPtr audio, uint Flags, uint XAudio2Processor);

	// Token: 0x0600000B RID: 11
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_RegisterForCallbacks(IntPtr audio, IntPtr pCallback);

	// Token: 0x0600000C RID: 12
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudio_UnregisterForCallbacks(IntPtr audio, IntPtr pCallback);

	// Token: 0x0600000D RID: 13
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_CreateSourceVoice(IntPtr audio, out IntPtr ppSourceVoice, ref FAudio.FAudioWaveFormatEx pSourceFormat, uint Flags, float MaxFrequencyRatio, IntPtr pCallback, IntPtr pSendList, IntPtr pEffectChain);

	// Token: 0x0600000E RID: 14
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_CreateSourceVoice(IntPtr audio, out IntPtr ppSourceVoice, IntPtr pSourceFormat, uint Flags, float MaxFrequencyRatio, IntPtr pCallback, IntPtr pSendList, IntPtr pEffectChain);

	// Token: 0x0600000F RID: 15
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_CreateSubmixVoice(IntPtr audio, out IntPtr ppSubmixVoice, uint InputChannels, uint InputSampleRate, uint Flags, uint ProcessingStage, IntPtr pSendList, IntPtr pEffectChain);

	// Token: 0x06000010 RID: 16
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_CreateMasteringVoice(IntPtr audio, out IntPtr ppMasteringVoice, uint InputChannels, uint InputSampleRate, uint Flags, uint DeviceIndex, IntPtr pEffectChain);

	// Token: 0x06000011 RID: 17
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudio_StartEngine(IntPtr audio);

	// Token: 0x06000012 RID: 18
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudio_StopEngine(IntPtr audio);

	// Token: 0x06000013 RID: 19
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl, EntryPoint = "FAudio_CommitOperationSet")]
	public static extern uint FAudio_CommitChanges(IntPtr audio, uint OperationSet);

	// Token: 0x06000014 RID: 20
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudio_GetPerformanceData(IntPtr audio, out FAudio.FAudioPerformanceData pPerfData);

	// Token: 0x06000015 RID: 21
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudio_SetDebugConfiguration(IntPtr audio, ref FAudio.FAudioDebugConfiguration pDebugConfiguration, IntPtr pReserved);

	// Token: 0x06000016 RID: 22
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudio_GetProcessingQuantum(IntPtr audio, out uint quantumNumerator, out uint quantumDenominator);

	// Token: 0x06000017 RID: 23
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioVoice_GetVoiceDetails(IntPtr voice, out FAudio.FAudioVoiceDetails pVoiceDetails);

	// Token: 0x06000018 RID: 24
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_SetOutputVoices(IntPtr voice, ref FAudio.FAudioVoiceSends pSendList);

	// Token: 0x06000019 RID: 25
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_SetEffectChain(IntPtr voice, ref FAudio.FAudioEffectChain pEffectChain);

	// Token: 0x0600001A RID: 26
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_EnableEffect(IntPtr voice, uint EffectIndex, uint OperationSet);

	// Token: 0x0600001B RID: 27
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_DisableEffect(IntPtr voice, uint EffectIndex, uint OperationSet);

	// Token: 0x0600001C RID: 28
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioVoice_GetEffectState(IntPtr voice, uint EffectIndex, out int pEnabled);

	// Token: 0x0600001D RID: 29
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_SetEffectParameters(IntPtr voice, uint EffectIndex, IntPtr pParameters, uint ParametersByteSize, uint OperationSet);

	// Token: 0x0600001E RID: 30
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_GetEffectParameters(IntPtr voice, uint EffectIndex, IntPtr pParameters, uint ParametersByteSize);

	// Token: 0x0600001F RID: 31
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_SetFilterParameters(IntPtr voice, ref FAudio.FAudioFilterParameters pParameters, uint OperationSet);

	// Token: 0x06000020 RID: 32
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioVoice_GetFilterParameters(IntPtr voice, out FAudio.FAudioFilterParameters pParameters);

	// Token: 0x06000021 RID: 33
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_SetOutputFilterParameters(IntPtr voice, IntPtr pDestinationVoice, ref FAudio.FAudioFilterParameters pParameters, uint OperationSet);

	// Token: 0x06000022 RID: 34
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioVoice_GetOutputFilterParameters(IntPtr voice, IntPtr pDestinationVoice, out FAudio.FAudioFilterParameters pParameters);

	// Token: 0x06000023 RID: 35
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_SetVolume(IntPtr voice, float Volume, uint OperationSet);

	// Token: 0x06000024 RID: 36
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioVoice_GetVolume(IntPtr voice, out float pVolume);

	// Token: 0x06000025 RID: 37
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_SetChannelVolumes(IntPtr voice, uint Channels, float[] pVolumes, uint OperationSet);

	// Token: 0x06000026 RID: 38
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioVoice_GetChannelVolumes(IntPtr voice, uint Channels, float[] pVolumes);

	// Token: 0x06000027 RID: 39
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_SetOutputMatrix(IntPtr voice, IntPtr pDestinationVoice, uint SourceChannels, uint DestinationChannels, IntPtr pLevelMatrix, uint OperationSet);

	// Token: 0x06000028 RID: 40
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioVoice_GetOutputMatrix(IntPtr voice, IntPtr pDestinationVoice, uint SourceChannels, uint DestinationChannels, float[] pLevelMatrix);

	// Token: 0x06000029 RID: 41
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioVoice_DestroyVoice(IntPtr voice);

	// Token: 0x0600002A RID: 42
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioVoice_DestroyVoiceSafeEXT(IntPtr voice);

	// Token: 0x0600002B RID: 43
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioSourceVoice_Start(IntPtr voice, uint Flags, uint OperationSet);

	// Token: 0x0600002C RID: 44
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioSourceVoice_Stop(IntPtr voice, uint Flags, uint OperationSet);

	// Token: 0x0600002D RID: 45
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioSourceVoice_SubmitSourceBuffer(IntPtr voice, ref FAudio.FAudioBuffer pBuffer, IntPtr pBufferWMA);

	// Token: 0x0600002E RID: 46
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioSourceVoice_SubmitSourceBuffer(IntPtr voice, ref FAudio.FAudioBuffer pBuffer, ref FAudio.FAudioBufferWMA pBufferWMA);

	// Token: 0x0600002F RID: 47
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioSourceVoice_FlushSourceBuffers(IntPtr voice);

	// Token: 0x06000030 RID: 48
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioSourceVoice_Discontinuity(IntPtr voice);

	// Token: 0x06000031 RID: 49
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioSourceVoice_ExitLoop(IntPtr voice, uint OperationSet);

	// Token: 0x06000032 RID: 50
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioSourceVoice_GetState(IntPtr voice, out FAudio.FAudioVoiceState pVoiceState, uint Flags);

	// Token: 0x06000033 RID: 51
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioSourceVoice_SetFrequencyRatio(IntPtr voice, float Ratio, uint OperationSet);

	// Token: 0x06000034 RID: 52
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudioSourceVoice_GetFrequencyRatio(IntPtr voice, out float pRatio);

	// Token: 0x06000035 RID: 53
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioSourceVoice_SetSourceSampleRate(IntPtr voice, uint NewSourceSampleRate);

	// Token: 0x06000036 RID: 54
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioCreateReverb(out IntPtr ppApo, uint Flags);

	// Token: 0x06000037 RID: 55
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioCreateReverb9(out IntPtr ppApo, uint Flags);

	// Token: 0x06000038 RID: 56
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioCreateCollectorEXT(out IntPtr ppApo, uint flags, IntPtr pBuffer, uint bufferLength);

	// Token: 0x06000039 RID: 57
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAudioCreateCollectorWithCustomAllocatorEXT(out IntPtr ppApo, uint flags, IntPtr pBuffer, uint bufferLength, IntPtr customMalloc, IntPtr customFree, IntPtr customRealloc);

	// Token: 0x0600003A RID: 58
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint CreateFAPOBase(IntPtr fapo, IntPtr pRegistrationProperties, IntPtr pParameterBlocks, uint parameterBlockByteSize, byte fProducer);

	// Token: 0x0600003B RID: 59
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint CreateFAPOBaseWithCustomAllocatorEXT(IntPtr fapo, IntPtr pRegistrationProperties, IntPtr pParameterBlocks, uint parameterBlockByteSize, byte fProducer, IntPtr customMalloc, IntPtr customFree, IntPtr customRealloc);

	// Token: 0x0600003C RID: 60
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FAPOBase_Release(IntPtr fapo);

	// Token: 0x0600003D RID: 61
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCreateEngine(uint dwCreationFlags, out IntPtr ppEngine);

	// Token: 0x0600003E RID: 62
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_AddRef(IntPtr pEngine);

	// Token: 0x0600003F RID: 63
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_Release(IntPtr pEngine);

	// Token: 0x06000040 RID: 64
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_GetRendererCount(IntPtr pEngine, out ushort pnRendererCount);

	// Token: 0x06000041 RID: 65
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_GetRendererDetails(IntPtr pEngine, ushort nRendererIndex, out FAudio.FACTRendererDetails pRendererDetails);

	// Token: 0x06000042 RID: 66
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_GetFinalMixFormat(IntPtr pEngine, out FAudio.FAudioWaveFormatExtensible pFinalMixFormat);

	// Token: 0x06000043 RID: 67
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_Initialize(IntPtr pEngine, ref FAudio.FACTRuntimeParameters pParams);

	// Token: 0x06000044 RID: 68
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_ShutDown(IntPtr pEngine);

	// Token: 0x06000045 RID: 69
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_DoWork(IntPtr pEngine);

	// Token: 0x06000046 RID: 70
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_CreateSoundBank(IntPtr pEngine, IntPtr pvBuffer, uint dwSize, uint dwFlags, uint dwAllocAttributes, out IntPtr ppSoundBank);

	// Token: 0x06000047 RID: 71
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_CreateInMemoryWaveBank(IntPtr pEngine, IntPtr pvBuffer, uint dwSize, uint dwFlags, uint dwAllocAttributes, out IntPtr ppWaveBank);

	// Token: 0x06000048 RID: 72
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_CreateStreamingWaveBank(IntPtr pEngine, ref FAudio.FACTStreamingParameters pParms, out IntPtr ppWaveBank);

	// Token: 0x06000049 RID: 73
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern uint FACTAudioEngine_PrepareWave(IntPtr pEngine, uint dwFlags, byte* szWavePath, uint wStreamingPacketSize, uint dwAlignment, uint dwPlayOffset, byte nLoopCount, out IntPtr ppWave);

	// Token: 0x0600004A RID: 74 RVA: 0x000020E4 File Offset: 0x000002E4
	public unsafe static uint FACTAudioEngine_PrepareWave(IntPtr pEngine, uint dwFlags, string szWavePath, uint wStreamingPacketSize, uint dwAlignment, uint dwPlayOffset, byte nLoopCount, out IntPtr ppWave)
	{
		byte* ptr = FAudio.Utf8Encode(szWavePath);
		uint num = FAudio.FACTAudioEngine_PrepareWave(pEngine, dwFlags, ptr, wStreamingPacketSize, dwAlignment, dwPlayOffset, nLoopCount, out ppWave);
		Marshal.FreeHGlobal((IntPtr)((void*)ptr));
		return num;
	}

	// Token: 0x0600004B RID: 75
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_PrepareInMemoryWave(IntPtr pEngine, uint dwFlags, FAudio.FACTWaveBankEntry entry, uint[] pdwSeekTable, byte[] pbWaveData, uint dwPlayOffset, byte nLoopCount, out IntPtr ppWave);

	// Token: 0x0600004C RID: 76
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_PrepareStreamingWave(IntPtr pEngine, uint dwFlags, FAudio.FACTWaveBankEntry entry, FAudio.FACTStreamingParameters streamingParams, uint dwAlignment, uint[] pdwSeekTable, byte[] pbWaveData, uint dwPlayOffset, byte nLoopCount, out IntPtr ppWave);

	// Token: 0x0600004D RID: 77
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_RegisterNotification(IntPtr pEngine, ref FAudio.FACTNotificationDescription pNotificationDescription);

	// Token: 0x0600004E RID: 78
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_UnRegisterNotification(IntPtr pEngine, ref FAudio.FACTNotificationDescription pNotificationDescription);

	// Token: 0x0600004F RID: 79
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern ushort FACTAudioEngine_GetCategory(IntPtr pEngine, byte* szFriendlyName);

	// Token: 0x06000050 RID: 80 RVA: 0x00002114 File Offset: 0x00000314
	public unsafe static ushort FACTAudioEngine_GetCategory(IntPtr pEngine, string szFriendlyName)
	{
		int num = FAudio.Utf8Size(szFriendlyName);
		byte* ptr = stackalloc byte[(UIntPtr)num];
		return FAudio.FACTAudioEngine_GetCategory(pEngine, FAudio.Utf8Encode(szFriendlyName, ptr, num));
	}

	// Token: 0x06000051 RID: 81
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_Stop(IntPtr pEngine, ushort nCategory, uint dwFlags);

	// Token: 0x06000052 RID: 82
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_SetVolume(IntPtr pEngine, ushort nCategory, float volume);

	// Token: 0x06000053 RID: 83
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_Pause(IntPtr pEngine, ushort nCategory, int fPause);

	// Token: 0x06000054 RID: 84
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern ushort FACTAudioEngine_GetGlobalVariableIndex(IntPtr pEngine, byte* szFriendlyName);

	// Token: 0x06000055 RID: 85 RVA: 0x0000213C File Offset: 0x0000033C
	public unsafe static ushort FACTAudioEngine_GetGlobalVariableIndex(IntPtr pEngine, string szFriendlyName)
	{
		int num = FAudio.Utf8Size(szFriendlyName);
		byte* ptr = stackalloc byte[(UIntPtr)num];
		return FAudio.FACTAudioEngine_GetGlobalVariableIndex(pEngine, FAudio.Utf8Encode(szFriendlyName, ptr, num));
	}

	// Token: 0x06000056 RID: 86
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_SetGlobalVariable(IntPtr pEngine, ushort nIndex, float nValue);

	// Token: 0x06000057 RID: 87
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTAudioEngine_GetGlobalVariable(IntPtr pEngine, ushort nIndex, out float pnValue);

	// Token: 0x06000058 RID: 88
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern ushort FACTSoundBank_GetCueIndex(IntPtr pSoundBank, byte* szFriendlyName);

	// Token: 0x06000059 RID: 89 RVA: 0x00002164 File Offset: 0x00000364
	public unsafe static ushort FACTSoundBank_GetCueIndex(IntPtr pSoundBank, string szFriendlyName)
	{
		int num = FAudio.Utf8Size(szFriendlyName);
		byte* ptr = stackalloc byte[(UIntPtr)num];
		return FAudio.FACTSoundBank_GetCueIndex(pSoundBank, FAudio.Utf8Encode(szFriendlyName, ptr, num));
	}

	// Token: 0x0600005A RID: 90
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTSoundBank_GetNumCues(IntPtr pSoundBank, out ushort pnNumCues);

	// Token: 0x0600005B RID: 91
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTSoundBank_GetCueProperties(IntPtr pSoundBank, ushort nCueIndex, out FAudio.FACTCueProperties pProperties);

	// Token: 0x0600005C RID: 92
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTSoundBank_Prepare(IntPtr pSoundBank, ushort nCueIndex, uint dwFlags, int timeOffset, out IntPtr ppCue);

	// Token: 0x0600005D RID: 93
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTSoundBank_Play(IntPtr pSoundBank, ushort nCueIndex, uint dwFlags, int timeOffset, out IntPtr ppCue);

	// Token: 0x0600005E RID: 94
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTSoundBank_Play(IntPtr pSoundBank, ushort nCueIndex, uint dwFlags, int timeOffset, IntPtr ppCue);

	// Token: 0x0600005F RID: 95
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTSoundBank_Play3D(IntPtr pSoundBank, ushort nCueIndex, uint dwFlags, int timeOffset, ref FAudio.F3DAUDIO_DSP_SETTINGS pDSPSettings, IntPtr ppCue);

	// Token: 0x06000060 RID: 96
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTSoundBank_Stop(IntPtr pSoundBank, ushort nCueIndex, uint dwFlags);

	// Token: 0x06000061 RID: 97
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTSoundBank_Destroy(IntPtr pSoundBank);

	// Token: 0x06000062 RID: 98
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTSoundBank_GetState(IntPtr pSoundBank, out uint pdwState);

	// Token: 0x06000063 RID: 99
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWaveBank_Destroy(IntPtr pWaveBank);

	// Token: 0x06000064 RID: 100
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWaveBank_GetState(IntPtr pWaveBank, out uint pdwState);

	// Token: 0x06000065 RID: 101
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWaveBank_GetNumWaves(IntPtr pWaveBank, out ushort pnNumWaves);

	// Token: 0x06000066 RID: 102
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern ushort FACTWaveBank_GetWaveIndex(IntPtr pWaveBank, byte* szFriendlyName);

	// Token: 0x06000067 RID: 103 RVA: 0x0000218C File Offset: 0x0000038C
	public unsafe static ushort FACTWaveBank_GetWaveIndex(IntPtr pWaveBank, string szFriendlyName)
	{
		int num = FAudio.Utf8Size(szFriendlyName);
		byte* ptr = stackalloc byte[(UIntPtr)num];
		return FAudio.FACTWaveBank_GetWaveIndex(pWaveBank, FAudio.Utf8Encode(szFriendlyName, ptr, num));
	}

	// Token: 0x06000068 RID: 104
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWaveBank_GetWaveProperties(IntPtr pWaveBank, ushort nWaveIndex, out FAudio.FACTWaveProperties pWaveProperties);

	// Token: 0x06000069 RID: 105
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWaveBank_Prepare(IntPtr pWaveBank, ushort nWaveIndex, uint dwFlags, uint dwPlayOffset, byte nLoopCount, out IntPtr ppWave);

	// Token: 0x0600006A RID: 106
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWaveBank_Play(IntPtr pWaveBank, ushort nWaveIndex, uint dwFlags, uint dwPlayOffset, byte nLoopCount, out IntPtr ppWave);

	// Token: 0x0600006B RID: 107
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWaveBank_Stop(IntPtr pWaveBank, ushort nWaveIndex, uint dwFlags);

	// Token: 0x0600006C RID: 108
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWave_Destroy(IntPtr pWave);

	// Token: 0x0600006D RID: 109
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWave_Play(IntPtr pWave);

	// Token: 0x0600006E RID: 110
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWave_Stop(IntPtr pWave, uint dwFlags);

	// Token: 0x0600006F RID: 111
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWave_Pause(IntPtr pWave, int fPause);

	// Token: 0x06000070 RID: 112
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWave_GetState(IntPtr pWave, out uint pdwState);

	// Token: 0x06000071 RID: 113
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWave_SetPitch(IntPtr pWave, short pitch);

	// Token: 0x06000072 RID: 114
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWave_SetVolume(IntPtr pWave, float volume);

	// Token: 0x06000073 RID: 115
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWave_SetMatrixCoefficients(IntPtr pWave, uint uSrcChannelCount, uint uDstChannelCount, float[] pMatrixCoefficients);

	// Token: 0x06000074 RID: 116
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTWave_GetProperties(IntPtr pWave, out FAudio.FACTWaveInstanceProperties pProperties);

	// Token: 0x06000075 RID: 117
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_Destroy(IntPtr pCue);

	// Token: 0x06000076 RID: 118
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_Play(IntPtr pCue);

	// Token: 0x06000077 RID: 119
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_Stop(IntPtr pCue, uint dwFlags);

	// Token: 0x06000078 RID: 120
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_GetState(IntPtr pCue, out uint pdwState);

	// Token: 0x06000079 RID: 121
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_SetMatrixCoefficients(IntPtr pCue, uint uSrcChannelCount, uint uDstChannelCount, float[] pMatrixCoefficients);

	// Token: 0x0600007A RID: 122
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern ushort FACTCue_GetVariableIndex(IntPtr pCue, byte* szFriendlyName);

	// Token: 0x0600007B RID: 123 RVA: 0x000021B4 File Offset: 0x000003B4
	public unsafe static ushort FACTCue_GetVariableIndex(IntPtr pCue, string szFriendlyName)
	{
		int num = FAudio.Utf8Size(szFriendlyName);
		byte* ptr = stackalloc byte[(UIntPtr)num];
		return FAudio.FACTCue_GetVariableIndex(pCue, FAudio.Utf8Encode(szFriendlyName, ptr, num));
	}

	// Token: 0x0600007C RID: 124
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_SetVariable(IntPtr pCue, ushort nIndex, float nValue);

	// Token: 0x0600007D RID: 125
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_GetVariable(IntPtr pCue, ushort nIndex, out float nValue);

	// Token: 0x0600007E RID: 126
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_Pause(IntPtr pCue, int fPause);

	// Token: 0x0600007F RID: 127
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_GetProperties(IntPtr pCue, out IntPtr ppProperties);

	// Token: 0x06000080 RID: 128
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_SetOutputVoices(IntPtr pCue, IntPtr pSendList);

	// Token: 0x06000081 RID: 129
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACTCue_SetOutputVoiceMatrix(IntPtr pCue, IntPtr pDestinationVoice, uint SourceChannels, uint DestinationChannels, float[] pLevelMatrix);

	// Token: 0x06000082 RID: 130
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void F3DAudioInitialize(uint SpeakerChannelMask, float SpeedOfSound, byte[] Instance);

	// Token: 0x06000083 RID: 131
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint F3DAudioInitialize8(uint SpeakerChannelMask, float SpeedOfSound, byte[] Instance);

	// Token: 0x06000084 RID: 132
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void F3DAudioCalculate(byte[] Instance, ref FAudio.F3DAUDIO_LISTENER pListener, ref FAudio.F3DAUDIO_EMITTER pEmitter, uint Flags, ref FAudio.F3DAUDIO_DSP_SETTINGS pDSPSettings);

	// Token: 0x06000085 RID: 133
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACT3DInitialize(IntPtr pEngine, byte[] D3FInstance);

	// Token: 0x06000086 RID: 134
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACT3DCalculate(byte[] F3DInstance, ref FAudio.F3DAUDIO_LISTENER pListener, ref FAudio.F3DAUDIO_EMITTER pEmitter, ref FAudio.F3DAUDIO_DSP_SETTINGS pDSPSettings);

	// Token: 0x06000087 RID: 135
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint FACT3DApply(ref FAudio.F3DAUDIO_DSP_SETTINGS pDSPSettings, IntPtr pCue);

	// Token: 0x06000088 RID: 136
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void XNA_SongInit();

	// Token: 0x06000089 RID: 137
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void XNA_SongQuit();

	// Token: 0x0600008A RID: 138
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern float XNA_PlaySong(byte* name);

	// Token: 0x0600008B RID: 139 RVA: 0x000021DC File Offset: 0x000003DC
	public unsafe static float XNA_PlaySong(string name)
	{
		int num = FAudio.Utf8Size(name);
		byte* ptr = stackalloc byte[(UIntPtr)num];
		return FAudio.XNA_PlaySong(FAudio.Utf8Encode(name, ptr, num));
	}

	// Token: 0x0600008C RID: 140
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void XNA_PauseSong();

	// Token: 0x0600008D RID: 141
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void XNA_ResumeSong();

	// Token: 0x0600008E RID: 142
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void XNA_StopSong();

	// Token: 0x0600008F RID: 143
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void XNA_SetSongVolume(float volume);

	// Token: 0x06000090 RID: 144
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint XNA_GetSongEnded();

	// Token: 0x06000091 RID: 145
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void XNA_EnableVisualization(uint enable);

	// Token: 0x06000092 RID: 146
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint XNA_VisualizationEnabled();

	// Token: 0x06000093 RID: 147
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void XNA_GetSongVisualizationData(float[] frequencies, float[] samples, uint count);

	// Token: 0x06000094 RID: 148
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern IntPtr FAudio_fopen(byte* path);

	// Token: 0x06000095 RID: 149 RVA: 0x00002204 File Offset: 0x00000404
	public unsafe static IntPtr FAudio_fopen(string path)
	{
		int num = FAudio.Utf8Size(path);
		byte* ptr = stackalloc byte[(UIntPtr)num];
		return FAudio.FAudio_fopen(FAudio.Utf8Encode(path, ptr, num));
	}

	// Token: 0x06000096 RID: 150
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr FAudio_memopen(IntPtr mem, int len);

	// Token: 0x06000097 RID: 151
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr FAudio_memptr(IntPtr io, IntPtr offset);

	// Token: 0x06000098 RID: 152
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FAudio_close(IntPtr io);

	// Token: 0x06000099 RID: 153
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern FAudio.stb_vorbis_info stb_vorbis_get_info(IntPtr f);

	// Token: 0x0600009A RID: 154
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern FAudio.stb_vorbis_comment stb_vorbis_get_comment(IntPtr f);

	// Token: 0x0600009B RID: 155
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_get_error(IntPtr f);

	// Token: 0x0600009C RID: 156
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void stb_vorbis_close(IntPtr f);

	// Token: 0x0600009D RID: 157
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_get_sample_offset(IntPtr f);

	// Token: 0x0600009E RID: 158
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint stb_vorbis_get_file_offset(IntPtr f);

	// Token: 0x0600009F RID: 159
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr stb_vorbis_open_memory(IntPtr data, int len, out int error, IntPtr alloc_buffer);

	// Token: 0x060000A0 RID: 160
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern IntPtr stb_vorbis_open_filename(byte* filename, out int error, IntPtr alloc_buffer);

	// Token: 0x060000A1 RID: 161 RVA: 0x0000222C File Offset: 0x0000042C
	public unsafe static IntPtr stb_vorbis_open_filename(string filename, out int error, IntPtr alloc_buffer)
	{
		int num = FAudio.Utf8Size(filename);
		byte* ptr = stackalloc byte[(UIntPtr)num];
		return FAudio.stb_vorbis_open_filename(FAudio.Utf8Encode(filename, ptr, num), out error, alloc_buffer);
	}

	// Token: 0x060000A2 RID: 162
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr stb_vorbis_open_file(IntPtr f, int close_handle_on_close, out int error, IntPtr alloc_buffer);

	// Token: 0x060000A3 RID: 163
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr stb_vorbis_open_file_section(IntPtr f, int close_handle_on_close, out int error, IntPtr alloc_buffer, uint len);

	// Token: 0x060000A4 RID: 164
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_seek_frame(IntPtr f, uint sample_number);

	// Token: 0x060000A5 RID: 165
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_seek(IntPtr f, uint sample_number);

	// Token: 0x060000A6 RID: 166
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_seek_start(IntPtr f);

	// Token: 0x060000A7 RID: 167
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern uint stb_vorbis_stream_length_in_samples(IntPtr f);

	// Token: 0x060000A8 RID: 168
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern float stb_vorbis_stream_length_in_seconds(IntPtr f);

	// Token: 0x060000A9 RID: 169
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_get_frame_float(IntPtr f, out int channels, ref float[][] output);

	// Token: 0x060000AA RID: 170
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_get_frame_float(IntPtr f, IntPtr channels, ref float[][] output);

	// Token: 0x060000AB RID: 171
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_get_samples_float_interleaved(IntPtr f, int channels, float[] buffer, int num_floats);

	// Token: 0x060000AC RID: 172
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_get_samples_float_interleaved(IntPtr f, int channels, IntPtr buffer, int num_floats);

	// Token: 0x060000AD RID: 173
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern int stb_vorbis_get_samples_float(IntPtr f, int channels, float[][] buffer, int num_samples);

	// Token: 0x060000AE RID: 174
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr qoa_open_from_memory(IntPtr bytes, uint size, int free_on_close);

	// Token: 0x060000AF RID: 175
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	private unsafe static extern IntPtr qoa_open_from_filename(byte* filename);

	// Token: 0x060000B0 RID: 176 RVA: 0x00002254 File Offset: 0x00000454
	public unsafe static IntPtr qoa_open_from_filename(string filename)
	{
		int num = FAudio.Utf8Size(filename);
		byte* ptr = stackalloc byte[(UIntPtr)num];
		return FAudio.qoa_open_from_filename(FAudio.Utf8Encode(filename, ptr, num));
	}

	// Token: 0x060000B1 RID: 177
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void qoa_attributes(IntPtr qoa, out uint channels, out uint samplerate, out uint samples_per_channel_per_frame, out uint total_samples_per_channel);

	// Token: 0x060000B2 RID: 178
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public unsafe static extern uint qoa_decode_next_frame(IntPtr qoa, short* sample_data);

	// Token: 0x060000B3 RID: 179
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void qoa_seek_frame(IntPtr qoa, int frame_index);

	// Token: 0x060000B4 RID: 180
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public unsafe static extern void qoa_decode_entire(IntPtr qoa, short* sample_data);

	// Token: 0x060000B5 RID: 181
	[DllImport("FAudio", CallingConvention = CallingConvention.Cdecl)]
	public static extern void qoa_close(IntPtr qoa);

	// Token: 0x060000B6 RID: 182 RVA: 0x0000227C File Offset: 0x0000047C
	// Note: this type is marked as 'beforefieldinit'.
	static FAudio()
	{
	}

	// Token: 0x04000001 RID: 1
	private const string nativeLibName = "FAudio";

	// Token: 0x04000002 RID: 2
	public const uint FAUDIO_TARGET_VERSION = 8U;

	// Token: 0x04000003 RID: 3
	public const uint FAUDIO_ABI_VERSION = 0U;

	// Token: 0x04000004 RID: 4
	public const uint FAUDIO_MAJOR_VERSION = 26U;

	// Token: 0x04000005 RID: 5
	public const uint FAUDIO_MINOR_VERSION = 3U;

	// Token: 0x04000006 RID: 6
	public const uint FAUDIO_PATCH_VERSION = 0U;

	// Token: 0x04000007 RID: 7
	public const uint FAUDIO_COMPILED_VERSION = 260300U;

	// Token: 0x04000008 RID: 8
	public const uint FAUDIO_DEFAULT_PROCESSOR = 4294967295U;

	// Token: 0x04000009 RID: 9
	public const uint FAUDIO_MAX_BUFFER_BYTES = 2147483648U;

	// Token: 0x0400000A RID: 10
	public const uint FAUDIO_MAX_QUEUED_BUFFERS = 64U;

	// Token: 0x0400000B RID: 11
	public const uint FAUDIO_MAX_AUDIO_CHANNELS = 64U;

	// Token: 0x0400000C RID: 12
	public const uint FAUDIO_MIN_SAMPLE_RATE = 1000U;

	// Token: 0x0400000D RID: 13
	public const uint FAUDIO_MAX_SAMPLE_RATE = 200000U;

	// Token: 0x0400000E RID: 14
	public const float FAUDIO_MAX_VOLUME_LEVEL = 16777216f;

	// Token: 0x0400000F RID: 15
	public const float FAUDIO_MIN_FREQ_RATIO = 0.0009765625f;

	// Token: 0x04000010 RID: 16
	public const float FAUDIO_MAX_FREQ_RATIO = 1024f;

	// Token: 0x04000011 RID: 17
	public const float FAUDIO_DEFAULT_FREQ_RATIO = 2f;

	// Token: 0x04000012 RID: 18
	public const float FAUDIO_MAX_FILTER_ONEOVERQ = 1.5f;

	// Token: 0x04000013 RID: 19
	public const float FAUDIO_MAX_FILTER_FREQUENCY = 1f;

	// Token: 0x04000014 RID: 20
	public const uint FAUDIO_MAX_LOOP_COUNT = 254U;

	// Token: 0x04000015 RID: 21
	public const uint FAUDIO_COMMIT_NOW = 0U;

	// Token: 0x04000016 RID: 22
	public const uint FAUDIO_COMMIT_ALL = 0U;

	// Token: 0x04000017 RID: 23
	public const uint FAUDIO_INVALID_OPSET = 4294967295U;

	// Token: 0x04000018 RID: 24
	public const uint FAUDIO_NO_LOOP_REGION = 0U;

	// Token: 0x04000019 RID: 25
	public const uint FAUDIO_LOOP_INFINITE = 255U;

	// Token: 0x0400001A RID: 26
	public const uint FAUDIO_DEFAULT_CHANNELS = 0U;

	// Token: 0x0400001B RID: 27
	public const uint FAUDIO_DEFAULT_SAMPLERATE = 0U;

	// Token: 0x0400001C RID: 28
	public const uint FAUDIO_DEBUG_ENGINE = 1U;

	// Token: 0x0400001D RID: 29
	public const uint FAUDIO_VOICE_NOPITCH = 2U;

	// Token: 0x0400001E RID: 30
	public const uint FAUDIO_VOICE_NOSRC = 4U;

	// Token: 0x0400001F RID: 31
	public const uint FAUDIO_VOICE_USEFILTER = 8U;

	// Token: 0x04000020 RID: 32
	public const uint FAUDIO_VOICE_MUSIC = 16U;

	// Token: 0x04000021 RID: 33
	public const uint FAUDIO_PLAY_TAILS = 32U;

	// Token: 0x04000022 RID: 34
	public const uint FAUDIO_END_OF_STREAM = 64U;

	// Token: 0x04000023 RID: 35
	public const uint FAUDIO_SEND_USEFILTER = 128U;

	// Token: 0x04000024 RID: 36
	public const uint FAUDIO_VOICE_NOSAMPLESPLAYED = 256U;

	// Token: 0x04000025 RID: 37
	public const uint FAUDIO_1024_QUANTUM = 32768U;

	// Token: 0x04000026 RID: 38
	public const FAudio.FAudioFilterType FAUDIO_DEFAULT_FILTER_TYPE = FAudio.FAudioFilterType.FAudioLowPassFilter;

	// Token: 0x04000027 RID: 39
	public const float FAUDIO_DEFAULT_FILTER_FREQUENCY = 1f;

	// Token: 0x04000028 RID: 40
	public const float FAUDIO_DEFAULT_FILTER_ONEOVERQ = 1f;

	// Token: 0x04000029 RID: 41
	public const ushort FAUDIO_LOG_ERRORS = 1;

	// Token: 0x0400002A RID: 42
	public const ushort FAUDIO_LOG_WARNINGS = 2;

	// Token: 0x0400002B RID: 43
	public const ushort FAUDIO_LOG_INFO = 4;

	// Token: 0x0400002C RID: 44
	public const ushort FAUDIO_LOG_DETAIL = 8;

	// Token: 0x0400002D RID: 45
	public const ushort FAUDIO_LOG_API_CALLS = 16;

	// Token: 0x0400002E RID: 46
	public const ushort FAUDIO_LOG_FUNC_CALLS = 32;

	// Token: 0x0400002F RID: 47
	public const ushort FAUDIO_LOG_TIMING = 64;

	// Token: 0x04000030 RID: 48
	public const ushort FAUDIO_LOG_LOCKS = 128;

	// Token: 0x04000031 RID: 49
	public const ushort FAUDIO_LOG_MEMORY = 256;

	// Token: 0x04000032 RID: 50
	public const ushort FAUDIO_LOG_STREAMING = 4096;

	// Token: 0x04000033 RID: 51
	public const float FAUDIOFX_REVERB_DEFAULT_WET_DRY_MIX = 100f;

	// Token: 0x04000034 RID: 52
	public const uint FAUDIOFX_REVERB_DEFAULT_REFLECTIONS_DELAY = 5U;

	// Token: 0x04000035 RID: 53
	public const byte FAUDIOFX_REVERB_DEFAULT_REVERB_DELAY = 5;

	// Token: 0x04000036 RID: 54
	public const byte FAUDIOFX_REVERB_DEFAULT_REAR_DELAY = 5;

	// Token: 0x04000037 RID: 55
	public const byte FAUDIOFX_REVERB_DEFAULT_7POINT1_SIDE_DELAY = 5;

	// Token: 0x04000038 RID: 56
	public const byte FAUDIOFX_REVERB_DEFAULT_7POINT1_REAR_DELAY = 20;

	// Token: 0x04000039 RID: 57
	public const byte FAUDIOFX_REVERB_DEFAULT_POSITION = 6;

	// Token: 0x0400003A RID: 58
	public const byte FAUDIOFX_REVERB_DEFAULT_POSITION_MATRIX = 27;

	// Token: 0x0400003B RID: 59
	public const byte FAUDIOFX_REVERB_DEFAULT_EARLY_DIFFUSION = 8;

	// Token: 0x0400003C RID: 60
	public const byte FAUDIOFX_REVERB_DEFAULT_LATE_DIFFUSION = 8;

	// Token: 0x0400003D RID: 61
	public const byte FAUDIOFX_REVERB_DEFAULT_LOW_EQ_GAIN = 8;

	// Token: 0x0400003E RID: 62
	public const byte FAUDIOFX_REVERB_DEFAULT_LOW_EQ_CUTOFF = 4;

	// Token: 0x0400003F RID: 63
	public const byte FAUDIOFX_REVERB_DEFAULT_HIGH_EQ_GAIN = 8;

	// Token: 0x04000040 RID: 64
	public const byte FAUDIOFX_REVERB_DEFAULT_HIGH_EQ_CUTOFF = 4;

	// Token: 0x04000041 RID: 65
	public const float FAUDIOFX_REVERB_DEFAULT_ROOM_FILTER_FREQ = 5000f;

	// Token: 0x04000042 RID: 66
	public const float FAUDIOFX_REVERB_DEFAULT_ROOM_FILTER_MAIN = 0f;

	// Token: 0x04000043 RID: 67
	public const float FAUDIOFX_REVERB_DEFAULT_ROOM_FILTER_HF = 0f;

	// Token: 0x04000044 RID: 68
	public const float FAUDIOFX_REVERB_DEFAULT_REFLECTIONS_GAIN = 0f;

	// Token: 0x04000045 RID: 69
	public const float FAUDIOFX_REVERB_DEFAULT_REVERB_GAIN = 0f;

	// Token: 0x04000046 RID: 70
	public const float FAUDIOFX_REVERB_DEFAULT_DECAY_TIME = 1f;

	// Token: 0x04000047 RID: 71
	public const float FAUDIOFX_REVERB_DEFAULT_DENSITY = 100f;

	// Token: 0x04000048 RID: 72
	public const float FAUDIOFX_REVERB_DEFAULT_ROOM_SIZE = 100f;

	// Token: 0x04000049 RID: 73
	public const int FAPO_MIN_CHANNELS = 1;

	// Token: 0x0400004A RID: 74
	public const int FAPO_MAX_CHANNELS = 64;

	// Token: 0x0400004B RID: 75
	public const int FAPO_MIN_FRAMERATE = 1000;

	// Token: 0x0400004C RID: 76
	public const int FAPO_MAX_FRAMERATE = 200000;

	// Token: 0x0400004D RID: 77
	public const int FAPO_REGISTRATION_STRING_LENGTH = 256;

	// Token: 0x0400004E RID: 78
	public const int FACT_CONTENT_VERSION = 46;

	// Token: 0x0400004F RID: 79
	public const uint FACT_FLAG_MANAGEDATA = 1U;

	// Token: 0x04000050 RID: 80
	public const uint FACT_FLAG_STOP_RELEASE = 0U;

	// Token: 0x04000051 RID: 81
	public const uint FACT_FLAG_STOP_IMMEDIATE = 1U;

	// Token: 0x04000052 RID: 82
	public const uint FACT_FLAG_BACKGROUND_MUSIC = 2U;

	// Token: 0x04000053 RID: 83
	public const uint FACT_FLAG_UNITS_MS = 4U;

	// Token: 0x04000054 RID: 84
	public const uint FACT_FLAG_UNITS_SAMPLES = 8U;

	// Token: 0x04000055 RID: 85
	public const uint FACT_STATE_CREATED = 1U;

	// Token: 0x04000056 RID: 86
	public const uint FACT_STATE_PREPARING = 2U;

	// Token: 0x04000057 RID: 87
	public const uint FACT_STATE_PREPARED = 4U;

	// Token: 0x04000058 RID: 88
	public const uint FACT_STATE_PLAYING = 8U;

	// Token: 0x04000059 RID: 89
	public const uint FACT_STATE_STOPPING = 16U;

	// Token: 0x0400005A RID: 90
	public const uint FACT_STATE_STOPPED = 32U;

	// Token: 0x0400005B RID: 91
	public const uint FACT_STATE_PAUSED = 64U;

	// Token: 0x0400005C RID: 92
	public const uint FACT_STATE_INUSE = 128U;

	// Token: 0x0400005D RID: 93
	public const uint FACT_STATE_PREPAREFAILED = 2147483648U;

	// Token: 0x0400005E RID: 94
	public const short FACTPITCH_MIN = -1200;

	// Token: 0x0400005F RID: 95
	public const short FACTPITCH_MAX = 1200;

	// Token: 0x04000060 RID: 96
	public const short FACTPITCH_MIN_TOTAL = -2400;

	// Token: 0x04000061 RID: 97
	public const short FACTPITCH_MAX_TOTAL = 2400;

	// Token: 0x04000062 RID: 98
	public const float FACTVOLUME_MIN = 0f;

	// Token: 0x04000063 RID: 99
	public const float FACTVOLUME_MAX = 16777216f;

	// Token: 0x04000064 RID: 100
	public const ushort FACTINDEX_INVALID = 65535;

	// Token: 0x04000065 RID: 101
	public const ushort FACTVARIABLEINDEX_INVALID = 65535;

	// Token: 0x04000066 RID: 102
	public const ushort FACTCATEGORY_INVALID = 65535;

	// Token: 0x04000067 RID: 103
	public const uint FACT_ENGINE_LOOKAHEAD_DEFAULT = 250U;

	// Token: 0x04000068 RID: 104
	public const byte FACTNOTIFICATIONTYPE_CUEPREPARED = 1;

	// Token: 0x04000069 RID: 105
	public const byte FACTNOTIFICATIONTYPE_CUEPLAY = 2;

	// Token: 0x0400006A RID: 106
	public const byte FACTNOTIFICATIONTYPE_CUESTOP = 3;

	// Token: 0x0400006B RID: 107
	public const byte FACTNOTIFICATIONTYPE_CUEDESTROYED = 4;

	// Token: 0x0400006C RID: 108
	public const byte FACTNOTIFICATIONTYPE_MARKER = 5;

	// Token: 0x0400006D RID: 109
	public const byte FACTNOTIFICATIONTYPE_SOUNDBANKDESTROYED = 6;

	// Token: 0x0400006E RID: 110
	public const byte FACTNOTIFICATIONTYPE_WAVEBANKDESTROYED = 7;

	// Token: 0x0400006F RID: 111
	public const byte FACTNOTIFICATIONTYPE_LOCALVARIABLECHANGED = 8;

	// Token: 0x04000070 RID: 112
	public const byte FACTNOTIFICATIONTYPE_GLOBALVARIABLECHANGED = 9;

	// Token: 0x04000071 RID: 113
	public const byte FACTNOTIFICATIONTYPE_GUICONNECTED = 10;

	// Token: 0x04000072 RID: 114
	public const byte FACTNOTIFICATIONTYPE_GUIDISCONNECTED = 11;

	// Token: 0x04000073 RID: 115
	public const byte FACTNOTIFICATIONTYPE_WAVEPREPARED = 12;

	// Token: 0x04000074 RID: 116
	public const byte FACTNOTIFICATIONTYPE_WAVEPLAY = 13;

	// Token: 0x04000075 RID: 117
	public const byte FACTNOTIFICATIONTYPE_WAVESTOP = 14;

	// Token: 0x04000076 RID: 118
	public const byte FACTNOTIFICATIONTYPE_WAVELOOPED = 15;

	// Token: 0x04000077 RID: 119
	public const byte FACTNOTIFICATIONTYPE_WAVEDESTROYED = 16;

	// Token: 0x04000078 RID: 120
	public const byte FACTNOTIFICATIONTYPE_WAVEBANKPREPARED = 17;

	// Token: 0x04000079 RID: 121
	public const byte FACTNOTIFICATIONTYPE_WAVEBANKSTREAMING_INVALIDCONTENT = 18;

	// Token: 0x0400007A RID: 122
	public const byte FACT_FLAG_NOTIFICATION_PERSIST = 1;

	// Token: 0x0400007B RID: 123
	public const uint FACT_WAVEBANK_TYPE_BUFFER = 0U;

	// Token: 0x0400007C RID: 124
	public const uint FACT_WAVEBANK_TYPE_STREAMING = 1U;

	// Token: 0x0400007D RID: 125
	public const uint FACT_WAVEBANK_TYPE_MASK = 1U;

	// Token: 0x0400007E RID: 126
	public const uint FACT_WAVEBANK_FLAGS_ENTRYNAMES = 65536U;

	// Token: 0x0400007F RID: 127
	public const uint FACT_WAVEBANK_FLAGS_COMPACT = 131072U;

	// Token: 0x04000080 RID: 128
	public const uint FACT_WAVEBANK_FLAGS_SYNC_DISABLED = 262144U;

	// Token: 0x04000081 RID: 129
	public const uint FACT_WAVEBANK_FLAGS_SEEKTABLES = 524288U;

	// Token: 0x04000082 RID: 130
	public const uint FACT_WAVEBANK_FLAGS_MASK = 983040U;

	// Token: 0x04000083 RID: 131
	public const uint SPEAKER_FRONT_LEFT = 1U;

	// Token: 0x04000084 RID: 132
	public const uint SPEAKER_FRONT_RIGHT = 2U;

	// Token: 0x04000085 RID: 133
	public const uint SPEAKER_FRONT_CENTER = 4U;

	// Token: 0x04000086 RID: 134
	public const uint SPEAKER_LOW_FREQUENCY = 8U;

	// Token: 0x04000087 RID: 135
	public const uint SPEAKER_BACK_LEFT = 16U;

	// Token: 0x04000088 RID: 136
	public const uint SPEAKER_BACK_RIGHT = 32U;

	// Token: 0x04000089 RID: 137
	public const uint SPEAKER_FRONT_LEFT_OF_CENTER = 64U;

	// Token: 0x0400008A RID: 138
	public const uint SPEAKER_FRONT_RIGHT_OF_CENTER = 128U;

	// Token: 0x0400008B RID: 139
	public const uint SPEAKER_BACK_CENTER = 256U;

	// Token: 0x0400008C RID: 140
	public const uint SPEAKER_SIDE_LEFT = 512U;

	// Token: 0x0400008D RID: 141
	public const uint SPEAKER_SIDE_RIGHT = 1024U;

	// Token: 0x0400008E RID: 142
	public const uint SPEAKER_TOP_CENTER = 2048U;

	// Token: 0x0400008F RID: 143
	public const uint SPEAKER_TOP_FRONT_LEFT = 4096U;

	// Token: 0x04000090 RID: 144
	public const uint SPEAKER_TOP_FRONT_CENTER = 8192U;

	// Token: 0x04000091 RID: 145
	public const uint SPEAKER_TOP_FRONT_RIGHT = 16384U;

	// Token: 0x04000092 RID: 146
	public const uint SPEAKER_TOP_BACK_LEFT = 32768U;

	// Token: 0x04000093 RID: 147
	public const uint SPEAKER_TOP_BACK_CENTER = 65536U;

	// Token: 0x04000094 RID: 148
	public const uint SPEAKER_TOP_BACK_RIGHT = 131072U;

	// Token: 0x04000095 RID: 149
	public const uint SPEAKER_MONO = 4U;

	// Token: 0x04000096 RID: 150
	public const uint SPEAKER_STEREO = 3U;

	// Token: 0x04000097 RID: 151
	public const uint SPEAKER_2POINT1 = 11U;

	// Token: 0x04000098 RID: 152
	public const uint SPEAKER_SURROUND = 263U;

	// Token: 0x04000099 RID: 153
	public const uint SPEAKER_QUAD = 51U;

	// Token: 0x0400009A RID: 154
	public const uint SPEAKER_4POINT1 = 59U;

	// Token: 0x0400009B RID: 155
	public const uint SPEAKER_5POINT1 = 63U;

	// Token: 0x0400009C RID: 156
	public const uint SPEAKER_7POINT1 = 255U;

	// Token: 0x0400009D RID: 157
	public const uint SPEAKER_5POINT1_SURROUND = 1551U;

	// Token: 0x0400009E RID: 158
	public const uint SPEAKER_7POINT1_SURROUND = 1599U;

	// Token: 0x0400009F RID: 159
	public const uint SPEAKER_XBOX = 63U;

	// Token: 0x040000A0 RID: 160
	public const float F3DAUDIO_PI = 3.1415927f;

	// Token: 0x040000A1 RID: 161
	public const float F3DAUDIO_2PI = 6.2831855f;

	// Token: 0x040000A2 RID: 162
	public const uint F3DAUDIO_CALCULATE_MATRIX = 1U;

	// Token: 0x040000A3 RID: 163
	public const uint F3DAUDIO_CALCULATE_DELAY = 2U;

	// Token: 0x040000A4 RID: 164
	public const uint F3DAUDIO_CALCULATE_LPF_DIRECT = 4U;

	// Token: 0x040000A5 RID: 165
	public const uint F3DAUDIO_CALCULATE_LPF_REVERB = 8U;

	// Token: 0x040000A6 RID: 166
	public const uint F3DAUDIO_CALCULATE_REVERB = 16U;

	// Token: 0x040000A7 RID: 167
	public const uint F3DAUDIO_CALCULATE_DOPPLER = 32U;

	// Token: 0x040000A8 RID: 168
	public const uint F3DAUDIO_CALCULATE_EMITTER_ANGLE = 64U;

	// Token: 0x040000A9 RID: 169
	public const uint F3DAUDIO_CALCULATE_ZEROCENTER = 65536U;

	// Token: 0x040000AA RID: 170
	public const uint F3DAUDIO_CALCULATE_REDIRECT_TO_LFE = 131072U;

	// Token: 0x040000AB RID: 171
	public const int F3DAUDIO_HANDLE_BYTESIZE = 20;

	// Token: 0x040000AC RID: 172
	public const float LEFT_AZIMUTH = 4.712389f;

	// Token: 0x040000AD RID: 173
	public const float RIGHT_AZIMUTH = 1.5707964f;

	// Token: 0x040000AE RID: 174
	public const float FRONT_LEFT_AZIMUTH = 5.4977875f;

	// Token: 0x040000AF RID: 175
	public const float FRONT_RIGHT_AZIMUTH = 0.7853982f;

	// Token: 0x040000B0 RID: 176
	public const float FRONT_CENTER_AZIMUTH = 0f;

	// Token: 0x040000B1 RID: 177
	public const float LOW_FREQUENCY_AZIMUTH = 6.2831855f;

	// Token: 0x040000B2 RID: 178
	public const float BACK_LEFT_AZIMUTH = 3.926991f;

	// Token: 0x040000B3 RID: 179
	public const float BACK_RIGHT_AZIMUTH = 2.3561945f;

	// Token: 0x040000B4 RID: 180
	public const float BACK_CENTER_AZIMUTH = 3.1415927f;

	// Token: 0x040000B5 RID: 181
	public const float FRONT_LEFT_OF_CENTER_AZIMUTH = 5.8904862f;

	// Token: 0x040000B6 RID: 182
	public const float FRONT_RIGHT_OF_CENTER_AZIMUTH = 0.3926991f;

	// Token: 0x040000B7 RID: 183
	public static readonly float[] aStereoLayout = new float[] { 4.712389f, 1.5707964f };

	// Token: 0x040000B8 RID: 184
	public static readonly float[] a2Point1Layout = new float[] { 4.712389f, 1.5707964f, 6.2831855f };

	// Token: 0x040000B9 RID: 185
	public static readonly float[] aQuadLayout = new float[] { 5.4977875f, 0.7853982f, 3.926991f, 2.3561945f };

	// Token: 0x040000BA RID: 186
	public static readonly float[] a4Point1Layout = new float[] { 5.4977875f, 0.7853982f, 6.2831855f, 3.926991f, 2.3561945f };

	// Token: 0x040000BB RID: 187
	public static readonly float[] a5Point1Layout = new float[] { 5.4977875f, 0.7853982f, 0f, 6.2831855f, 3.926991f, 2.3561945f };

	// Token: 0x040000BC RID: 188
	public static readonly float[] a7Point1Layout = new float[] { 5.4977875f, 0.7853982f, 0f, 6.2831855f, 3.926991f, 2.3561945f, 4.712389f, 1.5707964f };

	// Token: 0x02000161 RID: 353
	[Flags]
	public enum FAudioDeviceRole
	{
		// Token: 0x04000B4C RID: 2892
		FAudioNotDefaultDevice = 0,
		// Token: 0x04000B4D RID: 2893
		FAudioDefaultConsoleDevice = 1,
		// Token: 0x04000B4E RID: 2894
		FAudioDefaultMultimediaDevice = 2,
		// Token: 0x04000B4F RID: 2895
		FAudioDefaultCommunicationsDevice = 4,
		// Token: 0x04000B50 RID: 2896
		FAudioDefaultGameDevice = 8,
		// Token: 0x04000B51 RID: 2897
		FAudioGlobalDefaultDevice = 15,
		// Token: 0x04000B52 RID: 2898
		FAudioInvalidDeviceRole = -16
	}

	// Token: 0x02000162 RID: 354
	public enum FAudioFilterType
	{
		// Token: 0x04000B54 RID: 2900
		FAudioLowPassFilter,
		// Token: 0x04000B55 RID: 2901
		FAudioBandPassFilter,
		// Token: 0x04000B56 RID: 2902
		FAudioHighPassFilter,
		// Token: 0x04000B57 RID: 2903
		FAudioNotchFilter
	}

	// Token: 0x02000163 RID: 355
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioGUID
	{
		// Token: 0x04000B58 RID: 2904
		public uint Data1;

		// Token: 0x04000B59 RID: 2905
		public ushort Data2;

		// Token: 0x04000B5A RID: 2906
		public ushort Data3;

		// Token: 0x04000B5B RID: 2907
		[FixedBuffer(typeof(byte), 8)]
		public FAudio.FAudioGUID.<Data4>e__FixedBuffer Data4;

		// Token: 0x020003ED RID: 1005
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		public struct <Data4>e__FixedBuffer
		{
			// Token: 0x04001E1D RID: 7709
			public byte FixedElementField;
		}
	}

	// Token: 0x02000164 RID: 356
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioWaveFormatEx
	{
		// Token: 0x04000B5C RID: 2908
		public ushort wFormatTag;

		// Token: 0x04000B5D RID: 2909
		public ushort nChannels;

		// Token: 0x04000B5E RID: 2910
		public uint nSamplesPerSec;

		// Token: 0x04000B5F RID: 2911
		public uint nAvgBytesPerSec;

		// Token: 0x04000B60 RID: 2912
		public ushort nBlockAlign;

		// Token: 0x04000B61 RID: 2913
		public ushort wBitsPerSample;

		// Token: 0x04000B62 RID: 2914
		public ushort cbSize;
	}

	// Token: 0x02000165 RID: 357
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioWaveFormatExtensible
	{
		// Token: 0x04000B63 RID: 2915
		public FAudio.FAudioWaveFormatEx Format;

		// Token: 0x04000B64 RID: 2916
		public ushort Samples;

		// Token: 0x04000B65 RID: 2917
		public uint dwChannelMask;

		// Token: 0x04000B66 RID: 2918
		public FAudio.FAudioGUID SubFormat;
	}

	// Token: 0x02000166 RID: 358
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioADPCMCoefSet
	{
		// Token: 0x04000B67 RID: 2919
		public short iCoef1;

		// Token: 0x04000B68 RID: 2920
		public short iCoef2;
	}

	// Token: 0x02000167 RID: 359
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioADPCMWaveFormat
	{
		// Token: 0x04000B69 RID: 2921
		public FAudio.FAudioWaveFormatEx wfx;

		// Token: 0x04000B6A RID: 2922
		public ushort wSamplesPerBlock;

		// Token: 0x04000B6B RID: 2923
		public ushort wNumCoef;

		// Token: 0x04000B6C RID: 2924
		public IntPtr aCoef;
	}

	// Token: 0x02000168 RID: 360
	public struct FAudioXMA2WaveFormatEx
	{
		// Token: 0x04000B6D RID: 2925
		public FAudio.FAudioWaveFormatEx wfx;

		// Token: 0x04000B6E RID: 2926
		public ushort wNumStreams;

		// Token: 0x04000B6F RID: 2927
		public uint dwChannelMask;

		// Token: 0x04000B70 RID: 2928
		public uint dwSamplesEncoded;

		// Token: 0x04000B71 RID: 2929
		public uint dwBytesPerBlock;

		// Token: 0x04000B72 RID: 2930
		public uint dwPlayBegin;

		// Token: 0x04000B73 RID: 2931
		public uint dwPlayLength;

		// Token: 0x04000B74 RID: 2932
		public uint dwLoopBegin;

		// Token: 0x04000B75 RID: 2933
		public uint dwLoopLength;

		// Token: 0x04000B76 RID: 2934
		public byte bLoopCount;

		// Token: 0x04000B77 RID: 2935
		public byte bEncoderVersion;

		// Token: 0x04000B78 RID: 2936
		public ushort wBlockCount;
	}

	// Token: 0x02000169 RID: 361
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioDeviceDetails
	{
		// Token: 0x04000B79 RID: 2937
		[FixedBuffer(typeof(short), 256)]
		public FAudio.FAudioDeviceDetails.<DeviceID>e__FixedBuffer DeviceID;

		// Token: 0x04000B7A RID: 2938
		[FixedBuffer(typeof(short), 256)]
		public FAudio.FAudioDeviceDetails.<DisplayName>e__FixedBuffer DisplayName;

		// Token: 0x04000B7B RID: 2939
		public FAudio.FAudioDeviceRole Role;

		// Token: 0x04000B7C RID: 2940
		public FAudio.FAudioWaveFormatExtensible OutputFormat;

		// Token: 0x020003EE RID: 1006
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <DeviceID>e__FixedBuffer
		{
			// Token: 0x04001E1E RID: 7710
			public short FixedElementField;
		}

		// Token: 0x020003EF RID: 1007
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <DisplayName>e__FixedBuffer
		{
			// Token: 0x04001E1F RID: 7711
			public short FixedElementField;
		}
	}

	// Token: 0x0200016A RID: 362
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioVoiceDetails
	{
		// Token: 0x04000B7D RID: 2941
		public uint CreationFlags;

		// Token: 0x04000B7E RID: 2942
		public uint ActiveFlags;

		// Token: 0x04000B7F RID: 2943
		public uint InputChannels;

		// Token: 0x04000B80 RID: 2944
		public uint InputSampleRate;
	}

	// Token: 0x0200016B RID: 363
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioSendDescriptor
	{
		// Token: 0x04000B81 RID: 2945
		public uint Flags;

		// Token: 0x04000B82 RID: 2946
		public IntPtr pOutputVoice;
	}

	// Token: 0x0200016C RID: 364
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioVoiceSends
	{
		// Token: 0x04000B83 RID: 2947
		public uint SendCount;

		// Token: 0x04000B84 RID: 2948
		public IntPtr pSends;
	}

	// Token: 0x0200016D RID: 365
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioEffectDescriptor
	{
		// Token: 0x04000B85 RID: 2949
		public IntPtr pEffect;

		// Token: 0x04000B86 RID: 2950
		public int InitialState;

		// Token: 0x04000B87 RID: 2951
		public uint OutputChannels;
	}

	// Token: 0x0200016E RID: 366
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioEffectChain
	{
		// Token: 0x04000B88 RID: 2952
		public uint EffectCount;

		// Token: 0x04000B89 RID: 2953
		public IntPtr pEffectDescriptors;
	}

	// Token: 0x0200016F RID: 367
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioFilterParameters
	{
		// Token: 0x04000B8A RID: 2954
		public FAudio.FAudioFilterType Type;

		// Token: 0x04000B8B RID: 2955
		public float Frequency;

		// Token: 0x04000B8C RID: 2956
		public float OneOverQ;
	}

	// Token: 0x02000170 RID: 368
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioBuffer
	{
		// Token: 0x04000B8D RID: 2957
		public uint Flags;

		// Token: 0x04000B8E RID: 2958
		public uint AudioBytes;

		// Token: 0x04000B8F RID: 2959
		public IntPtr pAudioData;

		// Token: 0x04000B90 RID: 2960
		public uint PlayBegin;

		// Token: 0x04000B91 RID: 2961
		public uint PlayLength;

		// Token: 0x04000B92 RID: 2962
		public uint LoopBegin;

		// Token: 0x04000B93 RID: 2963
		public uint LoopLength;

		// Token: 0x04000B94 RID: 2964
		public uint LoopCount;

		// Token: 0x04000B95 RID: 2965
		public IntPtr pContext;
	}

	// Token: 0x02000171 RID: 369
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioBufferWMA
	{
		// Token: 0x04000B96 RID: 2966
		public IntPtr pDecodedPacketCumulativeBytes;

		// Token: 0x04000B97 RID: 2967
		public uint PacketCount;
	}

	// Token: 0x02000172 RID: 370
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioVoiceState
	{
		// Token: 0x04000B98 RID: 2968
		public IntPtr pCurrentBufferContext;

		// Token: 0x04000B99 RID: 2969
		public uint BuffersQueued;

		// Token: 0x04000B9A RID: 2970
		public ulong SamplesPlayed;
	}

	// Token: 0x02000173 RID: 371
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioPerformanceData
	{
		// Token: 0x04000B9B RID: 2971
		public ulong AudioCyclesSinceLastQuery;

		// Token: 0x04000B9C RID: 2972
		public ulong TotalCyclesSinceLastQuery;

		// Token: 0x04000B9D RID: 2973
		public uint MinimumCyclesPerQuantum;

		// Token: 0x04000B9E RID: 2974
		public uint MaximumCyclesPerQuantum;

		// Token: 0x04000B9F RID: 2975
		public uint MemoryUsageInBytes;

		// Token: 0x04000BA0 RID: 2976
		public uint CurrentLatencyInSamples;

		// Token: 0x04000BA1 RID: 2977
		public uint GlitchesSinceEngineStarted;

		// Token: 0x04000BA2 RID: 2978
		public uint ActiveSourceVoiceCount;

		// Token: 0x04000BA3 RID: 2979
		public uint TotalSourceVoiceCount;

		// Token: 0x04000BA4 RID: 2980
		public uint ActiveSubmixVoiceCount;

		// Token: 0x04000BA5 RID: 2981
		public uint ActiveResamplerCount;

		// Token: 0x04000BA6 RID: 2982
		public uint ActiveMatrixMixCount;

		// Token: 0x04000BA7 RID: 2983
		public uint ActiveXmaSourceVoices;

		// Token: 0x04000BA8 RID: 2984
		public uint ActiveXmaStreams;
	}

	// Token: 0x02000174 RID: 372
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FAudioDebugConfiguration
	{
		// Token: 0x04000BA9 RID: 2985
		public uint TraceMask;

		// Token: 0x04000BAA RID: 2986
		public uint BreakMask;

		// Token: 0x04000BAB RID: 2987
		public int LogThreadID;

		// Token: 0x04000BAC RID: 2988
		public int LogFileline;

		// Token: 0x04000BAD RID: 2989
		public int LogFunctionName;

		// Token: 0x04000BAE RID: 2990
		public int LogTiming;
	}

	// Token: 0x02000175 RID: 373
	// (Invoke) Token: 0x0600189E RID: 6302
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnCriticalErrorFunc(IntPtr engineCallback, uint Error);

	// Token: 0x02000176 RID: 374
	// (Invoke) Token: 0x060018A2 RID: 6306
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnProcessingPassEndFunc(IntPtr engineCallback);

	// Token: 0x02000177 RID: 375
	// (Invoke) Token: 0x060018A6 RID: 6310
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnProcessingPassStartFunc(IntPtr engineCallback);

	// Token: 0x02000178 RID: 376
	public struct FAudioEngineCallback
	{
		// Token: 0x04000BAF RID: 2991
		public IntPtr OnCriticalError;

		// Token: 0x04000BB0 RID: 2992
		public IntPtr OnProcessingPassEnd;

		// Token: 0x04000BB1 RID: 2993
		public IntPtr OnProcessingPassStart;
	}

	// Token: 0x02000179 RID: 377
	// (Invoke) Token: 0x060018AA RID: 6314
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnBufferEndFunc(IntPtr voiceCallback, IntPtr pBufferContext);

	// Token: 0x0200017A RID: 378
	// (Invoke) Token: 0x060018AE RID: 6318
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnBufferStartFunc(IntPtr voiceCallback, IntPtr pBufferContext);

	// Token: 0x0200017B RID: 379
	// (Invoke) Token: 0x060018B2 RID: 6322
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnLoopEndFunc(IntPtr voiceCallback, IntPtr pBufferContext);

	// Token: 0x0200017C RID: 380
	// (Invoke) Token: 0x060018B6 RID: 6326
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnStreamEndFunc(IntPtr voiceCallback);

	// Token: 0x0200017D RID: 381
	// (Invoke) Token: 0x060018BA RID: 6330
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnVoiceErrorFunc(IntPtr voiceCallback, IntPtr pBufferContext, uint Error);

	// Token: 0x0200017E RID: 382
	// (Invoke) Token: 0x060018BE RID: 6334
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnVoiceProcessingPassEndFunc(IntPtr voiceCallback);

	// Token: 0x0200017F RID: 383
	// (Invoke) Token: 0x060018C2 RID: 6338
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void OnVoiceProcessingPassStartFunc(IntPtr voiceCallback, uint BytesRequired);

	// Token: 0x02000180 RID: 384
	public struct FAudioVoiceCallback
	{
		// Token: 0x04000BB2 RID: 2994
		public IntPtr OnBufferEnd;

		// Token: 0x04000BB3 RID: 2995
		public IntPtr OnBufferStart;

		// Token: 0x04000BB4 RID: 2996
		public IntPtr OnLoopEnd;

		// Token: 0x04000BB5 RID: 2997
		public IntPtr OnStreamEnd;

		// Token: 0x04000BB6 RID: 2998
		public IntPtr OnVoiceError;

		// Token: 0x04000BB7 RID: 2999
		public IntPtr OnVoiceProcessingPassEnd;

		// Token: 0x04000BB8 RID: 3000
		public IntPtr OnVoiceProcessingPassStart;
	}

	// Token: 0x02000181 RID: 385
	public struct FAudioFXReverbParameters
	{
		// Token: 0x04000BB9 RID: 3001
		public float WetDryMix;

		// Token: 0x04000BBA RID: 3002
		public uint ReflectionsDelay;

		// Token: 0x04000BBB RID: 3003
		public byte ReverbDelay;

		// Token: 0x04000BBC RID: 3004
		public byte RearDelay;

		// Token: 0x04000BBD RID: 3005
		public byte PositionLeft;

		// Token: 0x04000BBE RID: 3006
		public byte PositionRight;

		// Token: 0x04000BBF RID: 3007
		public byte PositionMatrixLeft;

		// Token: 0x04000BC0 RID: 3008
		public byte PositionMatrixRight;

		// Token: 0x04000BC1 RID: 3009
		public byte EarlyDiffusion;

		// Token: 0x04000BC2 RID: 3010
		public byte LateDiffusion;

		// Token: 0x04000BC3 RID: 3011
		public byte LowEQGain;

		// Token: 0x04000BC4 RID: 3012
		public byte LowEQCutoff;

		// Token: 0x04000BC5 RID: 3013
		public byte HighEQGain;

		// Token: 0x04000BC6 RID: 3014
		public byte HighEQCutoff;

		// Token: 0x04000BC7 RID: 3015
		public float RoomFilterFreq;

		// Token: 0x04000BC8 RID: 3016
		public float RoomFilterMain;

		// Token: 0x04000BC9 RID: 3017
		public float RoomFilterHF;

		// Token: 0x04000BCA RID: 3018
		public float ReflectionsGain;

		// Token: 0x04000BCB RID: 3019
		public float ReverbGain;

		// Token: 0x04000BCC RID: 3020
		public float DecayTime;

		// Token: 0x04000BCD RID: 3021
		public float Density;

		// Token: 0x04000BCE RID: 3022
		public float RoomSize;
	}

	// Token: 0x02000182 RID: 386
	public struct FAudioFXReverbParameters9
	{
		// Token: 0x04000BCF RID: 3023
		public float WetDryMix;

		// Token: 0x04000BD0 RID: 3024
		public uint ReflectionsDelay;

		// Token: 0x04000BD1 RID: 3025
		public byte ReverbDelay;

		// Token: 0x04000BD2 RID: 3026
		public byte RearDelay;

		// Token: 0x04000BD3 RID: 3027
		public byte SideDelay;

		// Token: 0x04000BD4 RID: 3028
		public byte PositionLeft;

		// Token: 0x04000BD5 RID: 3029
		public byte PositionRight;

		// Token: 0x04000BD6 RID: 3030
		public byte PositionMatrixLeft;

		// Token: 0x04000BD7 RID: 3031
		public byte PositionMatrixRight;

		// Token: 0x04000BD8 RID: 3032
		public byte EarlyDiffusion;

		// Token: 0x04000BD9 RID: 3033
		public byte LateDiffusion;

		// Token: 0x04000BDA RID: 3034
		public byte LowEQGain;

		// Token: 0x04000BDB RID: 3035
		public byte LowEQCutoff;

		// Token: 0x04000BDC RID: 3036
		public byte HighEQGain;

		// Token: 0x04000BDD RID: 3037
		public byte HighEQCutoff;

		// Token: 0x04000BDE RID: 3038
		public float RoomFilterFreq;

		// Token: 0x04000BDF RID: 3039
		public float RoomFilterMain;

		// Token: 0x04000BE0 RID: 3040
		public float RoomFilterHF;

		// Token: 0x04000BE1 RID: 3041
		public float ReflectionsGain;

		// Token: 0x04000BE2 RID: 3042
		public float ReverbGain;

		// Token: 0x04000BE3 RID: 3043
		public float DecayTime;

		// Token: 0x04000BE4 RID: 3044
		public float Density;

		// Token: 0x04000BE5 RID: 3045
		public float RoomSize;
	}

	// Token: 0x02000183 RID: 387
	public enum FAPOBufferFlags
	{
		// Token: 0x04000BE7 RID: 3047
		FAPO_BUFFER_SILENT,
		// Token: 0x04000BE8 RID: 3048
		FAPO_BUFFER_VALID
	}

	// Token: 0x02000184 RID: 388
	[Flags]
	public enum FAPOMiscFlags : uint
	{
		// Token: 0x04000BEA RID: 3050
		FAPO_FLAG_CHANNELS_MUST_MATCH = 1U,
		// Token: 0x04000BEB RID: 3051
		FAPO_FLAG_FRAMERATE_MUST_MATCH = 2U,
		// Token: 0x04000BEC RID: 3052
		FAPO_FLAG_BITSPERSAMPLE_MUST_MATCH = 4U,
		// Token: 0x04000BED RID: 3053
		FAPO_FLAG_BUFFERCOUNT_MUST_MATCH = 8U,
		// Token: 0x04000BEE RID: 3054
		FAPO_FLAG_INPLACE_SUPPORTED = 16U,
		// Token: 0x04000BEF RID: 3055
		FAPO_FLAG_INPLACE_REQUIRED = 32U
	}

	// Token: 0x02000185 RID: 389
	// (Invoke) Token: 0x060018C6 RID: 6342
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int AddRefFunc(IntPtr fapo);

	// Token: 0x02000186 RID: 390
	// (Invoke) Token: 0x060018CA RID: 6346
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int ReleaseFunc(IntPtr fapo);

	// Token: 0x02000187 RID: 391
	// (Invoke) Token: 0x060018CE RID: 6350
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate uint GetRegistrationPropertiesFunc(IntPtr fapo, IntPtr ppRegistrationProperties);

	// Token: 0x02000188 RID: 392
	// (Invoke) Token: 0x060018D2 RID: 6354
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate uint IsInputFormatSupportedFunc(IntPtr fapo, IntPtr pOutputFormat, IntPtr pRequestedInputFormat, IntPtr ppSupportedInputFormat);

	// Token: 0x02000189 RID: 393
	// (Invoke) Token: 0x060018D6 RID: 6358
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate uint IsOutputFormatSupportedFunc(IntPtr fapo, IntPtr pInputFormat, IntPtr pRequestedOutputFormat, IntPtr ppSupportedOutputFormat);

	// Token: 0x0200018A RID: 394
	// (Invoke) Token: 0x060018DA RID: 6362
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate uint InitializeFunc(IntPtr fapo, IntPtr pData, uint DataByteSize);

	// Token: 0x0200018B RID: 395
	// (Invoke) Token: 0x060018DE RID: 6366
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ResetFunc(IntPtr fapo);

	// Token: 0x0200018C RID: 396
	// (Invoke) Token: 0x060018E2 RID: 6370
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate uint LockForProcessFunc(IntPtr fapo, uint InputLockedParameterCount, ref FAudio.FAPOLockForProcessBufferParameters pInputLockedParameters, uint OutputLockedParameterCount, ref FAudio.FAPOLockForProcessBufferParameters pOutputLockedParameters);

	// Token: 0x0200018D RID: 397
	// (Invoke) Token: 0x060018E6 RID: 6374
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void UnlockForProcessFunc(IntPtr fapo);

	// Token: 0x0200018E RID: 398
	// (Invoke) Token: 0x060018EA RID: 6378
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ProcessFunc(IntPtr fapo, uint InputProcessParameterCount, ref FAudio.FAPOProcessBufferParameters pInputProcessParameters, uint OutputProcessParameterCount, ref FAudio.FAPOProcessBufferParameters pOutputProcessParameters, int IsEnabled);

	// Token: 0x0200018F RID: 399
	// (Invoke) Token: 0x060018EE RID: 6382
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate uint CalcInputFramesFunc(IntPtr fapo, uint OutputFrameCount);

	// Token: 0x02000190 RID: 400
	// (Invoke) Token: 0x060018F2 RID: 6386
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate uint CalcOutputFramesFunc(IntPtr fapo, uint InputFrameCount);

	// Token: 0x02000191 RID: 401
	// (Invoke) Token: 0x060018F6 RID: 6390
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SetParametersFunc(IntPtr fapo, IntPtr pParameters, uint ParameterByteSize);

	// Token: 0x02000192 RID: 402
	// (Invoke) Token: 0x060018FA RID: 6394
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void GetParametersFunc(IntPtr fapo, IntPtr pParameters, uint ParameterByteSize);

	// Token: 0x02000193 RID: 403
	public struct FAPO
	{
		// Token: 0x04000BF0 RID: 3056
		public IntPtr AddRef;

		// Token: 0x04000BF1 RID: 3057
		public IntPtr Release;

		// Token: 0x04000BF2 RID: 3058
		public IntPtr GetRegistrationProperties;

		// Token: 0x04000BF3 RID: 3059
		public IntPtr IsInputFormatSupported;

		// Token: 0x04000BF4 RID: 3060
		public IntPtr IsOutputFormatSupported;

		// Token: 0x04000BF5 RID: 3061
		public IntPtr Initialize;

		// Token: 0x04000BF6 RID: 3062
		public IntPtr Reset;

		// Token: 0x04000BF7 RID: 3063
		public IntPtr LockForProcess;

		// Token: 0x04000BF8 RID: 3064
		public IntPtr UnlockForProcess;

		// Token: 0x04000BF9 RID: 3065
		public IntPtr Process;

		// Token: 0x04000BFA RID: 3066
		public IntPtr CalcInputFrames;

		// Token: 0x04000BFB RID: 3067
		public IntPtr CalcOutputFrames;

		// Token: 0x04000BFC RID: 3068
		public IntPtr SetParameters;

		// Token: 0x04000BFD RID: 3069
		public IntPtr GetParameters;
	}

	// Token: 0x02000194 RID: 404
	public struct FAPORegistrationProperties
	{
		// Token: 0x04000BFE RID: 3070
		public Guid clsid;

		// Token: 0x04000BFF RID: 3071
		[FixedBuffer(typeof(char), 256)]
		public FAudio.FAPORegistrationProperties.<FriendlyName>e__FixedBuffer FriendlyName;

		// Token: 0x04000C00 RID: 3072
		[FixedBuffer(typeof(char), 256)]
		public FAudio.FAPORegistrationProperties.<CopyrightInfo>e__FixedBuffer CopyrightInfo;

		// Token: 0x04000C01 RID: 3073
		public uint MajorVersion;

		// Token: 0x04000C02 RID: 3074
		public uint MinorVersion;

		// Token: 0x04000C03 RID: 3075
		public uint Flags;

		// Token: 0x04000C04 RID: 3076
		public uint MinInputBufferCount;

		// Token: 0x04000C05 RID: 3077
		public uint MaxInputBufferCount;

		// Token: 0x04000C06 RID: 3078
		public uint MinOutputBufferCount;

		// Token: 0x04000C07 RID: 3079
		public uint MaxOutputBufferCount;

		// Token: 0x020003F0 RID: 1008
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <CopyrightInfo>e__FixedBuffer
		{
			// Token: 0x04001E20 RID: 7712
			public char FixedElementField;
		}

		// Token: 0x020003F1 RID: 1009
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <FriendlyName>e__FixedBuffer
		{
			// Token: 0x04001E21 RID: 7713
			public char FixedElementField;
		}
	}

	// Token: 0x02000195 RID: 405
	public struct FAPOLockForProcessBufferParameters
	{
		// Token: 0x04000C08 RID: 3080
		public IntPtr pFormat;

		// Token: 0x04000C09 RID: 3081
		private uint MaxFrameCount;
	}

	// Token: 0x02000196 RID: 406
	public struct FAPOProcessBufferParameters
	{
		// Token: 0x04000C0A RID: 3082
		public IntPtr pBuffer;

		// Token: 0x04000C0B RID: 3083
		public FAudio.FAPOBufferFlags BufferFlags;

		// Token: 0x04000C0C RID: 3084
		public uint ValidFrameCount;
	}

	// Token: 0x02000197 RID: 407
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FAPOBase
	{
		// Token: 0x04000C0D RID: 3085
		public FAudio.FAPO FAPO;

		// Token: 0x04000C0E RID: 3086
		public IntPtr Destructor;

		// Token: 0x04000C0F RID: 3087
		public IntPtr OnSetParameters;

		// Token: 0x04000C10 RID: 3088
		private IntPtr m_pRegistrationProperties;

		// Token: 0x04000C11 RID: 3089
		private IntPtr m_pfnMatrixMixFunction;

		// Token: 0x04000C12 RID: 3090
		private IntPtr m_pfl32MatrixCoefficients;

		// Token: 0x04000C13 RID: 3091
		private uint m_nSrcFormatType;

		// Token: 0x04000C14 RID: 3092
		private byte m_fIsScalarMatrix;

		// Token: 0x04000C15 RID: 3093
		private byte m_fIsLocked;

		// Token: 0x04000C16 RID: 3094
		private IntPtr m_pParameterBlocks;

		// Token: 0x04000C17 RID: 3095
		private IntPtr m_pCurrentParameters;

		// Token: 0x04000C18 RID: 3096
		private IntPtr m_pCurrentParametersInternal;

		// Token: 0x04000C19 RID: 3097
		private uint m_uCurrentParametersIndex;

		// Token: 0x04000C1A RID: 3098
		private uint m_uParameterBlockByteSize;

		// Token: 0x04000C1B RID: 3099
		private byte m_fNewerResultsReady;

		// Token: 0x04000C1C RID: 3100
		private byte m_fProducer;

		// Token: 0x04000C1D RID: 3101
		private int m_lReferenceCount;

		// Token: 0x04000C1E RID: 3102
		private IntPtr pMalloc;

		// Token: 0x04000C1F RID: 3103
		private IntPtr pFree;

		// Token: 0x04000C20 RID: 3104
		private IntPtr pRealloc;
	}

	// Token: 0x02000198 RID: 408
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FAudioFXCollectorState
	{
		// Token: 0x04000C21 RID: 3105
		public uint WriteOffset;
	}

	// Token: 0x02000199 RID: 409
	// (Invoke) Token: 0x060018FE RID: 6398
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate int FACTReadFileCallback(IntPtr hFile, IntPtr buffer, uint nNumberOfBytesToRead, IntPtr lpOverlapped);

	// Token: 0x0200019A RID: 410
	// (Invoke) Token: 0x06001902 RID: 6402
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate int FACTGetOverlappedResultCallback(IntPtr hFile, IntPtr lpOverlapped, out uint lpNumberOfBytesTransferred, int bWait);

	// Token: 0x0200019B RID: 411
	// (Invoke) Token: 0x06001906 RID: 6406
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void FACTNotificationCallback(IntPtr pNotification);

	// Token: 0x0200019C RID: 412
	public enum FACTWaveBankSegIdx
	{
		// Token: 0x04000C23 RID: 3107
		FACT_WAVEBANK_SEGIDX_BANKDATA,
		// Token: 0x04000C24 RID: 3108
		FACT_WAVEBANK_SEGIDX_ENTRYMETADATA,
		// Token: 0x04000C25 RID: 3109
		FACT_WAVEBANK_SEGIDX_SEEKTABLES,
		// Token: 0x04000C26 RID: 3110
		FACT_WAVEBANK_SEGIDX_ENTRYNAMES,
		// Token: 0x04000C27 RID: 3111
		FACT_WAVEBANK_SEGIDX_ENTRYWAVEDATA,
		// Token: 0x04000C28 RID: 3112
		FACT_WAVEBANK_SEGIDX_COUNT
	}

	// Token: 0x0200019D RID: 413
	public struct FACTRendererDetails
	{
		// Token: 0x04000C29 RID: 3113
		[FixedBuffer(typeof(short), 255)]
		public FAudio.FACTRendererDetails.<rendererID>e__FixedBuffer rendererID;

		// Token: 0x04000C2A RID: 3114
		[FixedBuffer(typeof(short), 255)]
		public FAudio.FACTRendererDetails.<displayName>e__FixedBuffer displayName;

		// Token: 0x04000C2B RID: 3115
		public int defaultDevice;

		// Token: 0x020003F2 RID: 1010
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 510)]
		public struct <displayName>e__FixedBuffer
		{
			// Token: 0x04001E22 RID: 7714
			public short FixedElementField;
		}

		// Token: 0x020003F3 RID: 1011
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 510)]
		public struct <rendererID>e__FixedBuffer
		{
			// Token: 0x04001E23 RID: 7715
			public short FixedElementField;
		}
	}

	// Token: 0x0200019E RID: 414
	public struct FACTOverlapped
	{
		// Token: 0x04000C2C RID: 3116
		public IntPtr Internal;

		// Token: 0x04000C2D RID: 3117
		public IntPtr InternalHigh;

		// Token: 0x04000C2E RID: 3118
		public uint Offset;

		// Token: 0x04000C2F RID: 3119
		public uint OffsetHigh;

		// Token: 0x04000C30 RID: 3120
		public IntPtr hEvent;
	}

	// Token: 0x0200019F RID: 415
	public struct FACTFileIOCallbacks
	{
		// Token: 0x04000C31 RID: 3121
		public IntPtr readFileCallback;

		// Token: 0x04000C32 RID: 3122
		public IntPtr getOverlappedResultCallback;
	}

	// Token: 0x020001A0 RID: 416
	public struct FACTRuntimeParameters
	{
		// Token: 0x04000C33 RID: 3123
		public uint lookAheadTime;

		// Token: 0x04000C34 RID: 3124
		public IntPtr pGlobalSettingsBuffer;

		// Token: 0x04000C35 RID: 3125
		public uint globalSettingsBufferSize;

		// Token: 0x04000C36 RID: 3126
		public uint globalSettingsFlags;

		// Token: 0x04000C37 RID: 3127
		public uint globalSettingsAllocAttributes;

		// Token: 0x04000C38 RID: 3128
		public FAudio.FACTFileIOCallbacks fileIOCallbacks;

		// Token: 0x04000C39 RID: 3129
		public IntPtr fnNotificationCallback;

		// Token: 0x04000C3A RID: 3130
		public IntPtr pRendererID;

		// Token: 0x04000C3B RID: 3131
		public IntPtr pXAudio2;

		// Token: 0x04000C3C RID: 3132
		public IntPtr pMasteringVoice;
	}

	// Token: 0x020001A1 RID: 417
	public struct FACTStreamingParameters
	{
		// Token: 0x04000C3D RID: 3133
		public IntPtr file;

		// Token: 0x04000C3E RID: 3134
		public uint offset;

		// Token: 0x04000C3F RID: 3135
		public uint flags;

		// Token: 0x04000C40 RID: 3136
		public ushort packetSize;
	}

	// Token: 0x020001A2 RID: 418
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTWaveBankRegion
	{
		// Token: 0x04000C41 RID: 3137
		public uint dwOffset;

		// Token: 0x04000C42 RID: 3138
		public uint dwLength;
	}

	// Token: 0x020001A3 RID: 419
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTWaveBankSampleRegion
	{
		// Token: 0x04000C43 RID: 3139
		public uint dwStartSample;

		// Token: 0x04000C44 RID: 3140
		public uint dwTotalSamples;
	}

	// Token: 0x020001A4 RID: 420
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTWaveBankMiniWaveFormat
	{
		// Token: 0x04000C45 RID: 3141
		public uint dwValue;
	}

	// Token: 0x020001A5 RID: 421
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTWaveBankEntry
	{
		// Token: 0x04000C46 RID: 3142
		public uint dwFlagsAndDuration;

		// Token: 0x04000C47 RID: 3143
		public FAudio.FACTWaveBankMiniWaveFormat Format;

		// Token: 0x04000C48 RID: 3144
		public FAudio.FACTWaveBankRegion PlayRegion;

		// Token: 0x04000C49 RID: 3145
		public FAudio.FACTWaveBankSampleRegion LoopRegion;
	}

	// Token: 0x020001A6 RID: 422
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTWaveBankData
	{
		// Token: 0x04000C4A RID: 3146
		public uint dwFlags;

		// Token: 0x04000C4B RID: 3147
		public uint dwEntryCount;

		// Token: 0x04000C4C RID: 3148
		[FixedBuffer(typeof(char), 64)]
		public FAudio.FACTWaveBankData.<szBankName>e__FixedBuffer szBankName;

		// Token: 0x04000C4D RID: 3149
		public uint dwEntryMetaDataElementSize;

		// Token: 0x04000C4E RID: 3150
		public uint dwEntryNameElementSize;

		// Token: 0x04000C4F RID: 3151
		public uint dwAlignment;

		// Token: 0x04000C50 RID: 3152
		public FAudio.FACTWaveBankMiniWaveFormat CompactFormat;

		// Token: 0x04000C51 RID: 3153
		public ulong BuildTime;

		// Token: 0x020003F4 RID: 1012
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <szBankName>e__FixedBuffer
		{
			// Token: 0x04001E24 RID: 7716
			public char FixedElementField;
		}
	}

	// Token: 0x020001A7 RID: 423
	public struct FACTWaveProperties
	{
		// Token: 0x04000C52 RID: 3154
		[FixedBuffer(typeof(byte), 64)]
		public FAudio.FACTWaveProperties.<friendlyName>e__FixedBuffer friendlyName;

		// Token: 0x04000C53 RID: 3155
		public FAudio.FACTWaveBankMiniWaveFormat format;

		// Token: 0x04000C54 RID: 3156
		public uint durationInSamples;

		// Token: 0x04000C55 RID: 3157
		public FAudio.FACTWaveBankSampleRegion loopRegion;

		// Token: 0x04000C56 RID: 3158
		public int streaming;

		// Token: 0x020003F5 RID: 1013
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <friendlyName>e__FixedBuffer
		{
			// Token: 0x04001E25 RID: 7717
			public byte FixedElementField;
		}
	}

	// Token: 0x020001A8 RID: 424
	public struct FACTWaveInstanceProperties
	{
		// Token: 0x04000C57 RID: 3159
		public FAudio.FACTWaveProperties properties;

		// Token: 0x04000C58 RID: 3160
		public int backgroundMusic;
	}

	// Token: 0x020001A9 RID: 425
	public struct FACTCueProperties
	{
		// Token: 0x04000C59 RID: 3161
		[FixedBuffer(typeof(char), 255)]
		public FAudio.FACTCueProperties.<friendlyName>e__FixedBuffer friendlyName;

		// Token: 0x04000C5A RID: 3162
		public int interactive;

		// Token: 0x04000C5B RID: 3163
		public ushort iaVariableIndex;

		// Token: 0x04000C5C RID: 3164
		public ushort numVariations;

		// Token: 0x04000C5D RID: 3165
		public byte maxInstances;

		// Token: 0x04000C5E RID: 3166
		public byte currentInstances;

		// Token: 0x020003F6 RID: 1014
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 510)]
		public struct <friendlyName>e__FixedBuffer
		{
			// Token: 0x04001E26 RID: 7718
			public char FixedElementField;
		}
	}

	// Token: 0x020001AA RID: 426
	public struct FACTTrackProperties
	{
		// Token: 0x04000C5F RID: 3167
		public uint duration;

		// Token: 0x04000C60 RID: 3168
		public ushort numVariations;

		// Token: 0x04000C61 RID: 3169
		public byte numChannels;

		// Token: 0x04000C62 RID: 3170
		public ushort waveVariation;

		// Token: 0x04000C63 RID: 3171
		public byte loopCount;
	}

	// Token: 0x020001AB RID: 427
	public struct FACTVariationProperties
	{
		// Token: 0x04000C64 RID: 3172
		public ushort index;

		// Token: 0x04000C65 RID: 3173
		public byte weight;

		// Token: 0x04000C66 RID: 3174
		public float iaVariableMin;

		// Token: 0x04000C67 RID: 3175
		public float iaVariableMax;

		// Token: 0x04000C68 RID: 3176
		public int linger;
	}

	// Token: 0x020001AC RID: 428
	public struct FACTSoundProperties
	{
		// Token: 0x04000C69 RID: 3177
		public ushort category;

		// Token: 0x04000C6A RID: 3178
		public byte priority;

		// Token: 0x04000C6B RID: 3179
		public short pitch;

		// Token: 0x04000C6C RID: 3180
		public float volume;

		// Token: 0x04000C6D RID: 3181
		public ushort numTracks;

		// Token: 0x04000C6E RID: 3182
		public FAudio.FACTTrackProperties arrTrackProperties;
	}

	// Token: 0x020001AD RID: 429
	public struct FACTSoundVariationProperties
	{
		// Token: 0x04000C6F RID: 3183
		public FAudio.FACTVariationProperties variationProperties;

		// Token: 0x04000C70 RID: 3184
		public FAudio.FACTSoundProperties soundProperties;
	}

	// Token: 0x020001AE RID: 430
	public struct FACTCueInstanceProperties
	{
		// Token: 0x04000C71 RID: 3185
		public uint allocAttributes;

		// Token: 0x04000C72 RID: 3186
		public FAudio.FACTCueProperties cueProperties;

		// Token: 0x04000C73 RID: 3187
		public FAudio.FACTSoundVariationProperties activeVariationProperties;
	}

	// Token: 0x020001AF RID: 431
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTNotificationDescription
	{
		// Token: 0x04000C74 RID: 3188
		public byte type;

		// Token: 0x04000C75 RID: 3189
		public byte flags;

		// Token: 0x04000C76 RID: 3190
		public IntPtr pSoundBank;

		// Token: 0x04000C77 RID: 3191
		public IntPtr pWaveBank;

		// Token: 0x04000C78 RID: 3192
		public IntPtr pCue;

		// Token: 0x04000C79 RID: 3193
		public IntPtr pWave;

		// Token: 0x04000C7A RID: 3194
		public ushort cueIndex;

		// Token: 0x04000C7B RID: 3195
		public ushort waveIndex;

		// Token: 0x04000C7C RID: 3196
		public IntPtr pvContext;
	}

	// Token: 0x020001B0 RID: 432
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTNotificationCue
	{
		// Token: 0x04000C7D RID: 3197
		public ushort cueIndex;

		// Token: 0x04000C7E RID: 3198
		public IntPtr pSoundBank;

		// Token: 0x04000C7F RID: 3199
		public IntPtr pCue;
	}

	// Token: 0x020001B1 RID: 433
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTNotificationMarker
	{
		// Token: 0x04000C80 RID: 3200
		public ushort cueIndex;

		// Token: 0x04000C81 RID: 3201
		public IntPtr pSoundBank;

		// Token: 0x04000C82 RID: 3202
		public IntPtr pCue;

		// Token: 0x04000C83 RID: 3203
		public uint marker;
	}

	// Token: 0x020001B2 RID: 434
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTNotificationSoundBank
	{
		// Token: 0x04000C84 RID: 3204
		public IntPtr pSoundBank;
	}

	// Token: 0x020001B3 RID: 435
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTNotificationWaveBank
	{
		// Token: 0x04000C85 RID: 3205
		public IntPtr pWaveBank;
	}

	// Token: 0x020001B4 RID: 436
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTNotificationVariable
	{
		// Token: 0x04000C86 RID: 3206
		public ushort cueIndex;

		// Token: 0x04000C87 RID: 3207
		public IntPtr pSoundBank;

		// Token: 0x04000C88 RID: 3208
		public IntPtr pCue;

		// Token: 0x04000C89 RID: 3209
		public ushort variableIndex;

		// Token: 0x04000C8A RID: 3210
		public float variableValue;

		// Token: 0x04000C8B RID: 3211
		public int local;
	}

	// Token: 0x020001B5 RID: 437
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTNotificationGUI
	{
		// Token: 0x04000C8C RID: 3212
		public uint reserved;
	}

	// Token: 0x020001B6 RID: 438
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTNotificationWave
	{
		// Token: 0x04000C8D RID: 3213
		public IntPtr pWaveBank;

		// Token: 0x04000C8E RID: 3214
		public ushort waveIndex;

		// Token: 0x04000C8F RID: 3215
		public ushort cueIndex;

		// Token: 0x04000C90 RID: 3216
		public IntPtr pSoundBank;

		// Token: 0x04000C91 RID: 3217
		public IntPtr pCue;

		// Token: 0x04000C92 RID: 3218
		public IntPtr pWave;
	}

	// Token: 0x020001B7 RID: 439
	[StructLayout(LayoutKind.Explicit)]
	public struct FACTNotification_union
	{
		// Token: 0x04000C93 RID: 3219
		[FieldOffset(0)]
		public FAudio.FACTNotificationCue cue;

		// Token: 0x04000C94 RID: 3220
		[FieldOffset(0)]
		public FAudio.FACTNotificationMarker marker;

		// Token: 0x04000C95 RID: 3221
		[FieldOffset(0)]
		public FAudio.FACTNotificationSoundBank soundBank;

		// Token: 0x04000C96 RID: 3222
		[FieldOffset(0)]
		public FAudio.FACTNotificationWaveBank waveBank;

		// Token: 0x04000C97 RID: 3223
		[FieldOffset(0)]
		public FAudio.FACTNotificationVariable variable;

		// Token: 0x04000C98 RID: 3224
		[FieldOffset(0)]
		public FAudio.FACTNotificationGUI gui;

		// Token: 0x04000C99 RID: 3225
		[FieldOffset(0)]
		public FAudio.FACTNotificationWave wave;
	}

	// Token: 0x020001B8 RID: 440
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FACTNotification
	{
		// Token: 0x04000C9A RID: 3226
		public byte type;

		// Token: 0x04000C9B RID: 3227
		public int timeStamp;

		// Token: 0x04000C9C RID: 3228
		public IntPtr pvContext;

		// Token: 0x04000C9D RID: 3229
		public FAudio.FACTNotification_union anon;
	}

	// Token: 0x020001B9 RID: 441
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct F3DAUDIO_VECTOR
	{
		// Token: 0x04000C9E RID: 3230
		public float x;

		// Token: 0x04000C9F RID: 3231
		public float y;

		// Token: 0x04000CA0 RID: 3232
		public float z;
	}

	// Token: 0x020001BA RID: 442
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct F3DAUDIO_DISTANCE_CURVE_POINT
	{
		// Token: 0x04000CA1 RID: 3233
		public float Distance;

		// Token: 0x04000CA2 RID: 3234
		public float DSPSetting;
	}

	// Token: 0x020001BB RID: 443
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct F3DAUDIO_DISTANCE_CURVE
	{
		// Token: 0x04000CA3 RID: 3235
		public IntPtr pPoints;

		// Token: 0x04000CA4 RID: 3236
		public uint PointCount;
	}

	// Token: 0x020001BC RID: 444
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct F3DAUDIO_CONE
	{
		// Token: 0x04000CA5 RID: 3237
		public float InnerAngle;

		// Token: 0x04000CA6 RID: 3238
		public float OuterAngle;

		// Token: 0x04000CA7 RID: 3239
		public float InnerVolume;

		// Token: 0x04000CA8 RID: 3240
		public float OuterVolume;

		// Token: 0x04000CA9 RID: 3241
		public float InnerLPF;

		// Token: 0x04000CAA RID: 3242
		public float OuterLPF;

		// Token: 0x04000CAB RID: 3243
		public float InnerReverb;

		// Token: 0x04000CAC RID: 3244
		public float OuterReverb;
	}

	// Token: 0x020001BD RID: 445
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct F3DAUDIO_LISTENER
	{
		// Token: 0x04000CAD RID: 3245
		public FAudio.F3DAUDIO_VECTOR OrientFront;

		// Token: 0x04000CAE RID: 3246
		public FAudio.F3DAUDIO_VECTOR OrientTop;

		// Token: 0x04000CAF RID: 3247
		public FAudio.F3DAUDIO_VECTOR Position;

		// Token: 0x04000CB0 RID: 3248
		public FAudio.F3DAUDIO_VECTOR Velocity;

		// Token: 0x04000CB1 RID: 3249
		public IntPtr pCone;
	}

	// Token: 0x020001BE RID: 446
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct F3DAUDIO_EMITTER
	{
		// Token: 0x04000CB2 RID: 3250
		public IntPtr pCone;

		// Token: 0x04000CB3 RID: 3251
		public FAudio.F3DAUDIO_VECTOR OrientFront;

		// Token: 0x04000CB4 RID: 3252
		public FAudio.F3DAUDIO_VECTOR OrientTop;

		// Token: 0x04000CB5 RID: 3253
		public FAudio.F3DAUDIO_VECTOR Position;

		// Token: 0x04000CB6 RID: 3254
		public FAudio.F3DAUDIO_VECTOR Velocity;

		// Token: 0x04000CB7 RID: 3255
		public float InnerRadius;

		// Token: 0x04000CB8 RID: 3256
		public float InnerRadiusAngle;

		// Token: 0x04000CB9 RID: 3257
		public uint ChannelCount;

		// Token: 0x04000CBA RID: 3258
		public float ChannelRadius;

		// Token: 0x04000CBB RID: 3259
		public IntPtr pChannelAzimuths;

		// Token: 0x04000CBC RID: 3260
		public IntPtr pVolumeCurve;

		// Token: 0x04000CBD RID: 3261
		public IntPtr pLFECurve;

		// Token: 0x04000CBE RID: 3262
		public IntPtr pLPFDirectCurve;

		// Token: 0x04000CBF RID: 3263
		public IntPtr pLPFReverbCurve;

		// Token: 0x04000CC0 RID: 3264
		public IntPtr pReverbCurve;

		// Token: 0x04000CC1 RID: 3265
		public float CurveDistanceScaler;

		// Token: 0x04000CC2 RID: 3266
		public float DopplerScaler;
	}

	// Token: 0x020001BF RID: 447
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct F3DAUDIO_DSP_SETTINGS
	{
		// Token: 0x04000CC3 RID: 3267
		public IntPtr pMatrixCoefficients;

		// Token: 0x04000CC4 RID: 3268
		public IntPtr pDelayTimes;

		// Token: 0x04000CC5 RID: 3269
		public uint SrcChannelCount;

		// Token: 0x04000CC6 RID: 3270
		public uint DstChannelCount;

		// Token: 0x04000CC7 RID: 3271
		public float LPFDirectCoefficient;

		// Token: 0x04000CC8 RID: 3272
		public float LPFReverbCoefficient;

		// Token: 0x04000CC9 RID: 3273
		public float ReverbLevel;

		// Token: 0x04000CCA RID: 3274
		public float DopplerFactor;

		// Token: 0x04000CCB RID: 3275
		public float EmitterToListenerAngle;

		// Token: 0x04000CCC RID: 3276
		public float EmitterToListenerDistance;

		// Token: 0x04000CCD RID: 3277
		public float EmitterVelocityComponent;

		// Token: 0x04000CCE RID: 3278
		public float ListenerVelocityComponent;
	}

	// Token: 0x020001C0 RID: 448
	// (Invoke) Token: 0x0600190A RID: 6410
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr FAudio_readfunc(IntPtr data, IntPtr dst, IntPtr size, IntPtr count);

	// Token: 0x020001C1 RID: 449
	// (Invoke) Token: 0x0600190E RID: 6414
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate long FAudio_seekfunc(IntPtr data, long offset, int whence);

	// Token: 0x020001C2 RID: 450
	// (Invoke) Token: 0x06001912 RID: 6418
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int FAudio_closefunc(IntPtr data);

	// Token: 0x020001C3 RID: 451
	public struct FAudioIOStream
	{
		// Token: 0x04000CCF RID: 3279
		public IntPtr data;

		// Token: 0x04000CD0 RID: 3280
		public IntPtr read;

		// Token: 0x04000CD1 RID: 3281
		public IntPtr seek;

		// Token: 0x04000CD2 RID: 3282
		public IntPtr close;

		// Token: 0x04000CD3 RID: 3283
		public IntPtr ioLock;
	}

	// Token: 0x020001C4 RID: 452
	public struct stb_vorbis_alloc
	{
		// Token: 0x04000CD4 RID: 3284
		public IntPtr alloc_buffer;

		// Token: 0x04000CD5 RID: 3285
		public int alloc_buffer_length_in_bytes;
	}

	// Token: 0x020001C5 RID: 453
	public struct stb_vorbis_info
	{
		// Token: 0x04000CD6 RID: 3286
		public uint sample_rate;

		// Token: 0x04000CD7 RID: 3287
		public int channels;

		// Token: 0x04000CD8 RID: 3288
		public uint setup_memory_required;

		// Token: 0x04000CD9 RID: 3289
		public uint setup_temp_memory_required;

		// Token: 0x04000CDA RID: 3290
		public uint temp_memory_required;

		// Token: 0x04000CDB RID: 3291
		public int max_frame_size;
	}

	// Token: 0x020001C6 RID: 454
	public struct stb_vorbis_comment
	{
		// Token: 0x04000CDC RID: 3292
		public IntPtr vendor;

		// Token: 0x04000CDD RID: 3293
		public int comment_list_length;

		// Token: 0x04000CDE RID: 3294
		public IntPtr comment_list;
	}
}
