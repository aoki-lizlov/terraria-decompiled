using System;
using System.Globalization;

namespace System
{
	// Token: 0x020000F3 RID: 243
	internal ref struct DateTimeResult
	{
		// Token: 0x060009C0 RID: 2496 RVA: 0x00026F28 File Offset: 0x00025128
		internal void Init(ReadOnlySpan<char> originalDateTimeString)
		{
			this.originalDateTimeString = originalDateTimeString;
			this.Year = -1;
			this.Month = -1;
			this.Day = -1;
			this.fraction = -1.0;
			this.era = -1;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00026F5C File Offset: 0x0002515C
		internal void SetDate(int year, int month, int day)
		{
			this.Year = year;
			this.Month = month;
			this.Day = day;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00026F73 File Offset: 0x00025173
		internal void SetBadFormatSpecifierFailure()
		{
			this.SetBadFormatSpecifierFailure(ReadOnlySpan<char>.Empty);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00026F80 File Offset: 0x00025180
		internal void SetBadFormatSpecifierFailure(ReadOnlySpan<char> failedFormatSpecifier)
		{
			this.failure = ParseFailureKind.FormatWithFormatSpecifier;
			this.failureMessageID = "Format specifier was invalid.";
			this.failedFormatSpecifier = failedFormatSpecifier;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00026F9B File Offset: 0x0002519B
		internal void SetBadDateTimeFailure()
		{
			this.failure = ParseFailureKind.FormatWithOriginalDateTime;
			this.failureMessageID = "String was not recognized as a valid DateTime.";
			this.failureMessageFormatArgument = null;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00026FB6 File Offset: 0x000251B6
		internal void SetFailure(ParseFailureKind failure, string failureMessageID)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = null;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00026FCD File Offset: 0x000251CD
		internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = failureMessageFormatArgument;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00026FE4 File Offset: 0x000251E4
		internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = failureMessageFormatArgument;
			this.failureArgumentName = failureArgumentName;
		}

		// Token: 0x04000FFA RID: 4090
		internal int Year;

		// Token: 0x04000FFB RID: 4091
		internal int Month;

		// Token: 0x04000FFC RID: 4092
		internal int Day;

		// Token: 0x04000FFD RID: 4093
		internal int Hour;

		// Token: 0x04000FFE RID: 4094
		internal int Minute;

		// Token: 0x04000FFF RID: 4095
		internal int Second;

		// Token: 0x04001000 RID: 4096
		internal double fraction;

		// Token: 0x04001001 RID: 4097
		internal int era;

		// Token: 0x04001002 RID: 4098
		internal ParseFlags flags;

		// Token: 0x04001003 RID: 4099
		internal TimeSpan timeZoneOffset;

		// Token: 0x04001004 RID: 4100
		internal Calendar calendar;

		// Token: 0x04001005 RID: 4101
		internal DateTime parsedDate;

		// Token: 0x04001006 RID: 4102
		internal ParseFailureKind failure;

		// Token: 0x04001007 RID: 4103
		internal string failureMessageID;

		// Token: 0x04001008 RID: 4104
		internal object failureMessageFormatArgument;

		// Token: 0x04001009 RID: 4105
		internal string failureArgumentName;

		// Token: 0x0400100A RID: 4106
		internal ReadOnlySpan<char> originalDateTimeString;

		// Token: 0x0400100B RID: 4107
		internal ReadOnlySpan<char> failedFormatSpecifier;
	}
}
