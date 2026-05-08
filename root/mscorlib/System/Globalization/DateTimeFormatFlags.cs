using System;

namespace System.Globalization
{
	// Token: 0x020009B3 RID: 2483
	[Flags]
	internal enum DateTimeFormatFlags
	{
		// Token: 0x04003610 RID: 13840
		None = 0,
		// Token: 0x04003611 RID: 13841
		UseGenitiveMonth = 1,
		// Token: 0x04003612 RID: 13842
		UseLeapYearMonth = 2,
		// Token: 0x04003613 RID: 13843
		UseSpacesInMonthNames = 4,
		// Token: 0x04003614 RID: 13844
		UseHebrewRule = 8,
		// Token: 0x04003615 RID: 13845
		UseSpacesInDayNames = 16,
		// Token: 0x04003616 RID: 13846
		UseDigitPrefixInTokens = 32,
		// Token: 0x04003617 RID: 13847
		NotInitialized = -1
	}
}
