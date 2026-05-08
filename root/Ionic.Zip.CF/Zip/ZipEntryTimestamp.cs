using System;

namespace Ionic.Zip
{
	// Token: 0x02000020 RID: 32
	[Flags]
	public enum ZipEntryTimestamp
	{
		// Token: 0x0400009F RID: 159
		None = 0,
		// Token: 0x040000A0 RID: 160
		DOS = 1,
		// Token: 0x040000A1 RID: 161
		Windows = 2,
		// Token: 0x040000A2 RID: 162
		Unix = 4,
		// Token: 0x040000A3 RID: 163
		InfoZip1 = 8
	}
}
