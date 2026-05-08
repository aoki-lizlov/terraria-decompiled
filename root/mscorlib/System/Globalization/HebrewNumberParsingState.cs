using System;

namespace System.Globalization
{
	// Token: 0x020009C0 RID: 2496
	internal enum HebrewNumberParsingState
	{
		// Token: 0x040036BF RID: 14015
		InvalidHebrewNumber,
		// Token: 0x040036C0 RID: 14016
		NotHebrewDigit,
		// Token: 0x040036C1 RID: 14017
		FoundEndOfHebrewNumber,
		// Token: 0x040036C2 RID: 14018
		ContinueParsing
	}
}
