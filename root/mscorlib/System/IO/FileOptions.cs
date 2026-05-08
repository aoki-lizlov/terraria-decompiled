using System;

namespace System.IO
{
	// Token: 0x02000928 RID: 2344
	[Flags]
	public enum FileOptions
	{
		// Token: 0x04003321 RID: 13089
		None = 0,
		// Token: 0x04003322 RID: 13090
		WriteThrough = -2147483648,
		// Token: 0x04003323 RID: 13091
		Asynchronous = 1073741824,
		// Token: 0x04003324 RID: 13092
		RandomAccess = 268435456,
		// Token: 0x04003325 RID: 13093
		DeleteOnClose = 67108864,
		// Token: 0x04003326 RID: 13094
		SequentialScan = 134217728,
		// Token: 0x04003327 RID: 13095
		Encrypted = 16384
	}
}
