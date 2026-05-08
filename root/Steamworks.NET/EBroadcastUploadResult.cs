using System;

namespace Steamworks
{
	// Token: 0x02000158 RID: 344
	public enum EBroadcastUploadResult
	{
		// Token: 0x0400088D RID: 2189
		k_EBroadcastUploadResultNone,
		// Token: 0x0400088E RID: 2190
		k_EBroadcastUploadResultOK,
		// Token: 0x0400088F RID: 2191
		k_EBroadcastUploadResultInitFailed,
		// Token: 0x04000890 RID: 2192
		k_EBroadcastUploadResultFrameFailed,
		// Token: 0x04000891 RID: 2193
		k_EBroadcastUploadResultTimeout,
		// Token: 0x04000892 RID: 2194
		k_EBroadcastUploadResultBandwidthExceeded,
		// Token: 0x04000893 RID: 2195
		k_EBroadcastUploadResultLowFPS,
		// Token: 0x04000894 RID: 2196
		k_EBroadcastUploadResultMissingKeyFrames,
		// Token: 0x04000895 RID: 2197
		k_EBroadcastUploadResultNoConnection,
		// Token: 0x04000896 RID: 2198
		k_EBroadcastUploadResultRelayFailed,
		// Token: 0x04000897 RID: 2199
		k_EBroadcastUploadResultSettingsChanged,
		// Token: 0x04000898 RID: 2200
		k_EBroadcastUploadResultMissingAudio,
		// Token: 0x04000899 RID: 2201
		k_EBroadcastUploadResultTooFarBehind,
		// Token: 0x0400089A RID: 2202
		k_EBroadcastUploadResultTranscodeBehind,
		// Token: 0x0400089B RID: 2203
		k_EBroadcastUploadResultNotAllowedToPlay,
		// Token: 0x0400089C RID: 2204
		k_EBroadcastUploadResultBusy,
		// Token: 0x0400089D RID: 2205
		k_EBroadcastUploadResultBanned,
		// Token: 0x0400089E RID: 2206
		k_EBroadcastUploadResultAlreadyActive,
		// Token: 0x0400089F RID: 2207
		k_EBroadcastUploadResultForcedOff,
		// Token: 0x040008A0 RID: 2208
		k_EBroadcastUploadResultAudioBehind,
		// Token: 0x040008A1 RID: 2209
		k_EBroadcastUploadResultShutdown,
		// Token: 0x040008A2 RID: 2210
		k_EBroadcastUploadResultDisconnect,
		// Token: 0x040008A3 RID: 2211
		k_EBroadcastUploadResultVideoInitFailed,
		// Token: 0x040008A4 RID: 2212
		k_EBroadcastUploadResultAudioInitFailed
	}
}
