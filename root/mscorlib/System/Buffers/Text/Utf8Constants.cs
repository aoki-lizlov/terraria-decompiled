using System;

namespace System.Buffers.Text
{
	// Token: 0x02000B4D RID: 2893
	internal static class Utf8Constants
	{
		// Token: 0x06006A1F RID: 27167 RVA: 0x00168492 File Offset: 0x00166692
		// Note: this type is marked as 'beforefieldinit'.
		static Utf8Constants()
		{
		}

		// Token: 0x04003CC7 RID: 15559
		public const byte Colon = 58;

		// Token: 0x04003CC8 RID: 15560
		public const byte Comma = 44;

		// Token: 0x04003CC9 RID: 15561
		public const byte Minus = 45;

		// Token: 0x04003CCA RID: 15562
		public const byte Period = 46;

		// Token: 0x04003CCB RID: 15563
		public const byte Plus = 43;

		// Token: 0x04003CCC RID: 15564
		public const byte Slash = 47;

		// Token: 0x04003CCD RID: 15565
		public const byte Space = 32;

		// Token: 0x04003CCE RID: 15566
		public const byte Hyphen = 45;

		// Token: 0x04003CCF RID: 15567
		public const byte Separator = 44;

		// Token: 0x04003CD0 RID: 15568
		public const int GroupSize = 3;

		// Token: 0x04003CD1 RID: 15569
		public static readonly TimeSpan s_nullUtcOffset = TimeSpan.MinValue;

		// Token: 0x04003CD2 RID: 15570
		public const int DateTimeMaxUtcOffsetHours = 14;

		// Token: 0x04003CD3 RID: 15571
		public const int DateTimeNumFractionDigits = 7;

		// Token: 0x04003CD4 RID: 15572
		public const int MaxDateTimeFraction = 9999999;

		// Token: 0x04003CD5 RID: 15573
		public const ulong BillionMaxUIntValue = 4294967295000000000UL;

		// Token: 0x04003CD6 RID: 15574
		public const uint Billion = 1000000000U;
	}
}
