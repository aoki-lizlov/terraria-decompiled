using System;

namespace System
{
	// Token: 0x020001DC RID: 476
	internal enum TypeNameFormatFlags
	{
		// Token: 0x04001478 RID: 5240
		FormatBasic,
		// Token: 0x04001479 RID: 5241
		FormatNamespace,
		// Token: 0x0400147A RID: 5242
		FormatFullInst,
		// Token: 0x0400147B RID: 5243
		FormatAssembly = 4,
		// Token: 0x0400147C RID: 5244
		FormatSignature = 8,
		// Token: 0x0400147D RID: 5245
		FormatNoVersion = 16,
		// Token: 0x0400147E RID: 5246
		FormatAngleBrackets = 64,
		// Token: 0x0400147F RID: 5247
		FormatStubInfo = 128,
		// Token: 0x04001480 RID: 5248
		FormatGenericParam = 256,
		// Token: 0x04001481 RID: 5249
		FormatSerialization = 259
	}
}
