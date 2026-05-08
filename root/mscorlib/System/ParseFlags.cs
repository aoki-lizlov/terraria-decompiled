using System;

namespace System
{
	// Token: 0x020000F2 RID: 242
	[Flags]
	internal enum ParseFlags
	{
		// Token: 0x04000FEB RID: 4075
		HaveYear = 1,
		// Token: 0x04000FEC RID: 4076
		HaveMonth = 2,
		// Token: 0x04000FED RID: 4077
		HaveDay = 4,
		// Token: 0x04000FEE RID: 4078
		HaveHour = 8,
		// Token: 0x04000FEF RID: 4079
		HaveMinute = 16,
		// Token: 0x04000FF0 RID: 4080
		HaveSecond = 32,
		// Token: 0x04000FF1 RID: 4081
		HaveTime = 64,
		// Token: 0x04000FF2 RID: 4082
		HaveDate = 128,
		// Token: 0x04000FF3 RID: 4083
		TimeZoneUsed = 256,
		// Token: 0x04000FF4 RID: 4084
		TimeZoneUtc = 512,
		// Token: 0x04000FF5 RID: 4085
		ParsedMonthName = 1024,
		// Token: 0x04000FF6 RID: 4086
		CaptureOffset = 2048,
		// Token: 0x04000FF7 RID: 4087
		YearDefault = 4096,
		// Token: 0x04000FF8 RID: 4088
		Rfc1123Pattern = 8192,
		// Token: 0x04000FF9 RID: 4089
		UtcSortPattern = 16384
	}
}
