using System;

namespace System.Reflection
{
	// Token: 0x020008B3 RID: 2227
	[Flags]
	[Serializable]
	internal enum PInvokeAttributes
	{
		// Token: 0x04002F47 RID: 12103
		NoMangle = 1,
		// Token: 0x04002F48 RID: 12104
		CharSetMask = 6,
		// Token: 0x04002F49 RID: 12105
		CharSetNotSpec = 0,
		// Token: 0x04002F4A RID: 12106
		CharSetAnsi = 2,
		// Token: 0x04002F4B RID: 12107
		CharSetUnicode = 4,
		// Token: 0x04002F4C RID: 12108
		CharSetAuto = 6,
		// Token: 0x04002F4D RID: 12109
		BestFitUseAssem = 0,
		// Token: 0x04002F4E RID: 12110
		BestFitEnabled = 16,
		// Token: 0x04002F4F RID: 12111
		BestFitDisabled = 32,
		// Token: 0x04002F50 RID: 12112
		BestFitMask = 48,
		// Token: 0x04002F51 RID: 12113
		ThrowOnUnmappableCharUseAssem = 0,
		// Token: 0x04002F52 RID: 12114
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04002F53 RID: 12115
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x04002F54 RID: 12116
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04002F55 RID: 12117
		SupportsLastError = 64,
		// Token: 0x04002F56 RID: 12118
		CallConvMask = 1792,
		// Token: 0x04002F57 RID: 12119
		CallConvWinapi = 256,
		// Token: 0x04002F58 RID: 12120
		CallConvCdecl = 512,
		// Token: 0x04002F59 RID: 12121
		CallConvStdcall = 768,
		// Token: 0x04002F5A RID: 12122
		CallConvThiscall = 1024,
		// Token: 0x04002F5B RID: 12123
		CallConvFastcall = 1280,
		// Token: 0x04002F5C RID: 12124
		MaxValue = 65535
	}
}
