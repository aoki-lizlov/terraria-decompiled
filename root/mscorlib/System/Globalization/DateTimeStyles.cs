using System;

namespace System.Globalization
{
	// Token: 0x020009BA RID: 2490
	[Flags]
	public enum DateTimeStyles
	{
		// Token: 0x040036A8 RID: 13992
		None = 0,
		// Token: 0x040036A9 RID: 13993
		AllowLeadingWhite = 1,
		// Token: 0x040036AA RID: 13994
		AllowTrailingWhite = 2,
		// Token: 0x040036AB RID: 13995
		AllowInnerWhite = 4,
		// Token: 0x040036AC RID: 13996
		AllowWhiteSpaces = 7,
		// Token: 0x040036AD RID: 13997
		NoCurrentDateDefault = 8,
		// Token: 0x040036AE RID: 13998
		AdjustToUniversal = 16,
		// Token: 0x040036AF RID: 13999
		AssumeLocal = 32,
		// Token: 0x040036B0 RID: 14000
		AssumeUniversal = 64,
		// Token: 0x040036B1 RID: 14001
		RoundtripKind = 128
	}
}
