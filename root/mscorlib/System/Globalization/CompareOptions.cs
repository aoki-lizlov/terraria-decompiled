using System;

namespace System.Globalization
{
	// Token: 0x020009AF RID: 2479
	[Flags]
	public enum CompareOptions
	{
		// Token: 0x040035F7 RID: 13815
		None = 0,
		// Token: 0x040035F8 RID: 13816
		IgnoreCase = 1,
		// Token: 0x040035F9 RID: 13817
		IgnoreNonSpace = 2,
		// Token: 0x040035FA RID: 13818
		IgnoreSymbols = 4,
		// Token: 0x040035FB RID: 13819
		IgnoreKanaType = 8,
		// Token: 0x040035FC RID: 13820
		IgnoreWidth = 16,
		// Token: 0x040035FD RID: 13821
		OrdinalIgnoreCase = 268435456,
		// Token: 0x040035FE RID: 13822
		StringSort = 536870912,
		// Token: 0x040035FF RID: 13823
		Ordinal = 1073741824
	}
}
