using System;

namespace System.IO
{
	// Token: 0x02000969 RID: 2409
	[Flags]
	public enum FileAttributes
	{
		// Token: 0x040034A4 RID: 13476
		ReadOnly = 1,
		// Token: 0x040034A5 RID: 13477
		Hidden = 2,
		// Token: 0x040034A6 RID: 13478
		System = 4,
		// Token: 0x040034A7 RID: 13479
		Directory = 16,
		// Token: 0x040034A8 RID: 13480
		Archive = 32,
		// Token: 0x040034A9 RID: 13481
		Device = 64,
		// Token: 0x040034AA RID: 13482
		Normal = 128,
		// Token: 0x040034AB RID: 13483
		Temporary = 256,
		// Token: 0x040034AC RID: 13484
		SparseFile = 512,
		// Token: 0x040034AD RID: 13485
		ReparsePoint = 1024,
		// Token: 0x040034AE RID: 13486
		Compressed = 2048,
		// Token: 0x040034AF RID: 13487
		Offline = 4096,
		// Token: 0x040034B0 RID: 13488
		NotContentIndexed = 8192,
		// Token: 0x040034B1 RID: 13489
		Encrypted = 16384,
		// Token: 0x040034B2 RID: 13490
		IntegrityStream = 32768,
		// Token: 0x040034B3 RID: 13491
		NoScrubData = 131072
	}
}
