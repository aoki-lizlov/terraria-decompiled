using System;

namespace System
{
	// Token: 0x020000F5 RID: 245
	internal enum TokenType
	{
		// Token: 0x04001016 RID: 4118
		NumberToken = 1,
		// Token: 0x04001017 RID: 4119
		YearNumberToken,
		// Token: 0x04001018 RID: 4120
		Am,
		// Token: 0x04001019 RID: 4121
		Pm,
		// Token: 0x0400101A RID: 4122
		MonthToken,
		// Token: 0x0400101B RID: 4123
		EndOfString,
		// Token: 0x0400101C RID: 4124
		DayOfWeekToken,
		// Token: 0x0400101D RID: 4125
		TimeZoneToken,
		// Token: 0x0400101E RID: 4126
		EraToken,
		// Token: 0x0400101F RID: 4127
		DateWordToken,
		// Token: 0x04001020 RID: 4128
		UnknownToken,
		// Token: 0x04001021 RID: 4129
		HebrewNumber,
		// Token: 0x04001022 RID: 4130
		JapaneseEraToken,
		// Token: 0x04001023 RID: 4131
		TEraToken,
		// Token: 0x04001024 RID: 4132
		IgnorableSymbol,
		// Token: 0x04001025 RID: 4133
		SEP_Unk = 256,
		// Token: 0x04001026 RID: 4134
		SEP_End = 512,
		// Token: 0x04001027 RID: 4135
		SEP_Space = 768,
		// Token: 0x04001028 RID: 4136
		SEP_Am = 1024,
		// Token: 0x04001029 RID: 4137
		SEP_Pm = 1280,
		// Token: 0x0400102A RID: 4138
		SEP_Date = 1536,
		// Token: 0x0400102B RID: 4139
		SEP_Time = 1792,
		// Token: 0x0400102C RID: 4140
		SEP_YearSuff = 2048,
		// Token: 0x0400102D RID: 4141
		SEP_MonthSuff = 2304,
		// Token: 0x0400102E RID: 4142
		SEP_DaySuff = 2560,
		// Token: 0x0400102F RID: 4143
		SEP_HourSuff = 2816,
		// Token: 0x04001030 RID: 4144
		SEP_MinuteSuff = 3072,
		// Token: 0x04001031 RID: 4145
		SEP_SecondSuff = 3328,
		// Token: 0x04001032 RID: 4146
		SEP_LocalTimeMark = 3584,
		// Token: 0x04001033 RID: 4147
		SEP_DateOrOffset = 3840,
		// Token: 0x04001034 RID: 4148
		RegularTokenMask = 255,
		// Token: 0x04001035 RID: 4149
		SeparatorTokenMask = 65280
	}
}
