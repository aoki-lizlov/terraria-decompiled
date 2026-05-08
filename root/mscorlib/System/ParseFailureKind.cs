using System;

namespace System
{
	// Token: 0x020000F1 RID: 241
	internal enum ParseFailureKind
	{
		// Token: 0x04000FE2 RID: 4066
		None,
		// Token: 0x04000FE3 RID: 4067
		ArgumentNull,
		// Token: 0x04000FE4 RID: 4068
		Format,
		// Token: 0x04000FE5 RID: 4069
		FormatWithParameter,
		// Token: 0x04000FE6 RID: 4070
		FormatWithOriginalDateTime,
		// Token: 0x04000FE7 RID: 4071
		FormatWithFormatSpecifier,
		// Token: 0x04000FE8 RID: 4072
		FormatWithOriginalDateTimeAndParameter,
		// Token: 0x04000FE9 RID: 4073
		FormatBadDateTimeCalendar
	}
}
