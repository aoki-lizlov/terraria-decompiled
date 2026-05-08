using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020009DE RID: 2526
	[Serializable]
	public class ChineseLunisolarCalendar : EastAsianLunisolarCalendar
	{
		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06005C54 RID: 23636 RVA: 0x0013C889 File Offset: 0x0013AA89
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return ChineseLunisolarCalendar.minDate;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06005C55 RID: 23637 RVA: 0x0013C890 File Offset: 0x0013AA90
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return ChineseLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06005C56 RID: 23638 RVA: 0x0013C897 File Offset: 0x0013AA97
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 384;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06005C57 RID: 23639 RVA: 0x0013C89E File Offset: 0x0013AA9E
		internal override int MinCalendarYear
		{
			get
			{
				return 1901;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06005C58 RID: 23640 RVA: 0x0013C8A5 File Offset: 0x0013AAA5
		internal override int MaxCalendarYear
		{
			get
			{
				return 2100;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06005C59 RID: 23641 RVA: 0x0013C889 File Offset: 0x0013AA89
		internal override DateTime MinDate
		{
			get
			{
				return ChineseLunisolarCalendar.minDate;
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06005C5A RID: 23642 RVA: 0x0013C890 File Offset: 0x0013AA90
		internal override DateTime MaxDate
		{
			get
			{
				return ChineseLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06005C5B RID: 23643 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		internal override EraInfo[] CalEraInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x0013C8AC File Offset: 0x0013AAAC
		internal override int GetYearInfo(int LunarYear, int Index)
		{
			if (LunarYear < 1901 || LunarYear > 2100)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1901, 2100));
			}
			return ChineseLunisolarCalendar.yinfo[LunarYear - 1901, Index];
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x000025F2 File Offset: 0x000007F2
		internal override int GetYear(int year, DateTime time)
		{
			return year;
		}

		// Token: 0x06005C5E RID: 23646 RVA: 0x0013C910 File Offset: 0x0013AB10
		internal override int GetGregorianYear(int year, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
			if (year < 1901 || year > 2100)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1901, 2100));
			}
			return year;
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x0013C97D File Offset: 0x0013AB7D
		public ChineseLunisolarCalendar()
		{
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x0013C985 File Offset: 0x0013AB85
		[ComVisible(false)]
		public override int GetEra(DateTime time)
		{
			base.CheckTicksRange(time.Ticks);
			return 1;
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06005C61 RID: 23649 RVA: 0x0006BAF6 File Offset: 0x00069CF6
		internal override int ID
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06005C62 RID: 23650 RVA: 0x00003FB7 File Offset: 0x000021B7
		internal override int BaseCalendarID
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06005C63 RID: 23651 RVA: 0x0013C995 File Offset: 0x0013AB95
		[ComVisible(false)]
		public override int[] Eras
		{
			get
			{
				return new int[] { 1 };
			}
		}

		// Token: 0x06005C64 RID: 23652 RVA: 0x0013C9A4 File Offset: 0x0013ABA4
		// Note: this type is marked as 'beforefieldinit'.
		static ChineseLunisolarCalendar()
		{
		}

		// Token: 0x040037F5 RID: 14325
		public const int ChineseEra = 1;

		// Token: 0x040037F6 RID: 14326
		internal const int MIN_LUNISOLAR_YEAR = 1901;

		// Token: 0x040037F7 RID: 14327
		internal const int MAX_LUNISOLAR_YEAR = 2100;

		// Token: 0x040037F8 RID: 14328
		internal const int MIN_GREGORIAN_YEAR = 1901;

		// Token: 0x040037F9 RID: 14329
		internal const int MIN_GREGORIAN_MONTH = 2;

		// Token: 0x040037FA RID: 14330
		internal const int MIN_GREGORIAN_DAY = 19;

		// Token: 0x040037FB RID: 14331
		internal const int MAX_GREGORIAN_YEAR = 2101;

		// Token: 0x040037FC RID: 14332
		internal const int MAX_GREGORIAN_MONTH = 1;

		// Token: 0x040037FD RID: 14333
		internal const int MAX_GREGORIAN_DAY = 28;

		// Token: 0x040037FE RID: 14334
		internal static DateTime minDate = new DateTime(1901, 2, 19);

		// Token: 0x040037FF RID: 14335
		internal static DateTime maxDate = new DateTime(new DateTime(2101, 1, 28, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x04003800 RID: 14336
		private static readonly int[,] yinfo = new int[,]
		{
			{ 0, 2, 19, 19168 },
			{ 0, 2, 8, 42352 },
			{ 5, 1, 29, 21096 },
			{ 0, 2, 16, 53856 },
			{ 0, 2, 4, 55632 },
			{ 4, 1, 25, 27304 },
			{ 0, 2, 13, 22176 },
			{ 0, 2, 2, 39632 },
			{ 2, 1, 22, 19176 },
			{ 0, 2, 10, 19168 },
			{ 6, 1, 30, 42200 },
			{ 0, 2, 18, 42192 },
			{ 0, 2, 6, 53840 },
			{ 5, 1, 26, 54568 },
			{ 0, 2, 14, 46400 },
			{ 0, 2, 3, 54944 },
			{ 2, 1, 23, 38608 },
			{ 0, 2, 11, 38320 },
			{ 7, 2, 1, 18872 },
			{ 0, 2, 20, 18800 },
			{ 0, 2, 8, 42160 },
			{ 5, 1, 28, 45656 },
			{ 0, 2, 16, 27216 },
			{ 0, 2, 5, 27968 },
			{ 4, 1, 24, 44456 },
			{ 0, 2, 13, 11104 },
			{ 0, 2, 2, 38256 },
			{ 2, 1, 23, 18808 },
			{ 0, 2, 10, 18800 },
			{ 6, 1, 30, 25776 },
			{ 0, 2, 17, 54432 },
			{ 0, 2, 6, 59984 },
			{ 5, 1, 26, 27976 },
			{ 0, 2, 14, 23248 },
			{ 0, 2, 4, 11104 },
			{ 3, 1, 24, 37744 },
			{ 0, 2, 11, 37600 },
			{ 7, 1, 31, 51560 },
			{ 0, 2, 19, 51536 },
			{ 0, 2, 8, 54432 },
			{ 6, 1, 27, 55888 },
			{ 0, 2, 15, 46416 },
			{ 0, 2, 5, 22176 },
			{ 4, 1, 25, 43736 },
			{ 0, 2, 13, 9680 },
			{ 0, 2, 2, 37584 },
			{ 2, 1, 22, 51544 },
			{ 0, 2, 10, 43344 },
			{ 7, 1, 29, 46248 },
			{ 0, 2, 17, 27808 },
			{ 0, 2, 6, 46416 },
			{ 5, 1, 27, 21928 },
			{ 0, 2, 14, 19872 },
			{ 0, 2, 3, 42416 },
			{ 3, 1, 24, 21176 },
			{ 0, 2, 12, 21168 },
			{ 8, 1, 31, 43344 },
			{ 0, 2, 18, 59728 },
			{ 0, 2, 8, 27296 },
			{ 6, 1, 28, 44368 },
			{ 0, 2, 15, 43856 },
			{ 0, 2, 5, 19296 },
			{ 4, 1, 25, 42352 },
			{ 0, 2, 13, 42352 },
			{ 0, 2, 2, 21088 },
			{ 3, 1, 21, 59696 },
			{ 0, 2, 9, 55632 },
			{ 7, 1, 30, 23208 },
			{ 0, 2, 17, 22176 },
			{ 0, 2, 6, 38608 },
			{ 5, 1, 27, 19176 },
			{ 0, 2, 15, 19152 },
			{ 0, 2, 3, 42192 },
			{ 4, 1, 23, 53864 },
			{ 0, 2, 11, 53840 },
			{ 8, 1, 31, 54568 },
			{ 0, 2, 18, 46400 },
			{ 0, 2, 7, 46752 },
			{ 6, 1, 28, 38608 },
			{ 0, 2, 16, 38320 },
			{ 0, 2, 5, 18864 },
			{ 4, 1, 25, 42168 },
			{ 0, 2, 13, 42160 },
			{ 10, 2, 2, 45656 },
			{ 0, 2, 20, 27216 },
			{ 0, 2, 9, 27968 },
			{ 6, 1, 29, 44448 },
			{ 0, 2, 17, 43872 },
			{ 0, 2, 6, 38256 },
			{ 5, 1, 27, 18808 },
			{ 0, 2, 15, 18800 },
			{ 0, 2, 4, 25776 },
			{ 3, 1, 23, 27216 },
			{ 0, 2, 10, 59984 },
			{ 8, 1, 31, 27432 },
			{ 0, 2, 19, 23232 },
			{ 0, 2, 7, 43872 },
			{ 5, 1, 28, 37736 },
			{ 0, 2, 16, 37600 },
			{ 0, 2, 5, 51552 },
			{ 4, 1, 24, 54440 },
			{ 0, 2, 12, 54432 },
			{ 0, 2, 1, 55888 },
			{ 2, 1, 22, 23208 },
			{ 0, 2, 9, 22176 },
			{ 7, 1, 29, 43736 },
			{ 0, 2, 18, 9680 },
			{ 0, 2, 7, 37584 },
			{ 5, 1, 26, 51544 },
			{ 0, 2, 14, 43344 },
			{ 0, 2, 3, 46240 },
			{ 4, 1, 23, 46416 },
			{ 0, 2, 10, 44368 },
			{ 9, 1, 31, 21928 },
			{ 0, 2, 19, 19360 },
			{ 0, 2, 8, 42416 },
			{ 6, 1, 28, 21176 },
			{ 0, 2, 16, 21168 },
			{ 0, 2, 5, 43312 },
			{ 4, 1, 25, 29864 },
			{ 0, 2, 12, 27296 },
			{ 0, 2, 1, 44368 },
			{ 2, 1, 22, 19880 },
			{ 0, 2, 10, 19296 },
			{ 6, 1, 29, 42352 },
			{ 0, 2, 17, 42208 },
			{ 0, 2, 6, 53856 },
			{ 5, 1, 26, 59696 },
			{ 0, 2, 13, 54576 },
			{ 0, 2, 3, 23200 },
			{ 3, 1, 23, 27472 },
			{ 0, 2, 11, 38608 },
			{ 11, 1, 31, 19176 },
			{ 0, 2, 19, 19152 },
			{ 0, 2, 8, 42192 },
			{ 6, 1, 28, 53848 },
			{ 0, 2, 15, 53840 },
			{ 0, 2, 4, 54560 },
			{ 5, 1, 24, 55968 },
			{ 0, 2, 12, 46496 },
			{ 0, 2, 1, 22224 },
			{ 2, 1, 22, 19160 },
			{ 0, 2, 10, 18864 },
			{ 7, 1, 30, 42168 },
			{ 0, 2, 17, 42160 },
			{ 0, 2, 6, 43600 },
			{ 5, 1, 26, 46376 },
			{ 0, 2, 14, 27936 },
			{ 0, 2, 2, 44448 },
			{ 3, 1, 23, 21936 },
			{ 0, 2, 11, 37744 },
			{ 8, 2, 1, 18808 },
			{ 0, 2, 19, 18800 },
			{ 0, 2, 8, 25776 },
			{ 6, 1, 28, 27216 },
			{ 0, 2, 15, 59984 },
			{ 0, 2, 4, 27424 },
			{ 4, 1, 24, 43872 },
			{ 0, 2, 12, 43744 },
			{ 0, 2, 2, 37600 },
			{ 3, 1, 21, 51568 },
			{ 0, 2, 9, 51552 },
			{ 7, 1, 29, 54440 },
			{ 0, 2, 17, 54432 },
			{ 0, 2, 5, 55888 },
			{ 5, 1, 26, 23208 },
			{ 0, 2, 14, 22176 },
			{ 0, 2, 3, 42704 },
			{ 4, 1, 23, 21224 },
			{ 0, 2, 11, 21200 },
			{ 8, 1, 31, 43352 },
			{ 0, 2, 19, 43344 },
			{ 0, 2, 7, 46240 },
			{ 6, 1, 27, 46416 },
			{ 0, 2, 15, 44368 },
			{ 0, 2, 5, 21920 },
			{ 4, 1, 24, 42448 },
			{ 0, 2, 12, 42416 },
			{ 0, 2, 2, 21168 },
			{ 3, 1, 22, 43320 },
			{ 0, 2, 9, 26928 },
			{ 7, 1, 29, 29336 },
			{ 0, 2, 17, 27296 },
			{ 0, 2, 6, 44368 },
			{ 5, 1, 26, 19880 },
			{ 0, 2, 14, 19296 },
			{ 0, 2, 3, 42352 },
			{ 4, 1, 24, 21104 },
			{ 0, 2, 10, 53856 },
			{ 8, 1, 30, 59696 },
			{ 0, 2, 18, 54560 },
			{ 0, 2, 7, 55968 },
			{ 6, 1, 27, 27472 },
			{ 0, 2, 15, 22224 },
			{ 0, 2, 5, 19168 },
			{ 4, 1, 25, 42216 },
			{ 0, 2, 12, 42192 },
			{ 0, 2, 1, 53584 },
			{ 2, 1, 21, 55592 },
			{ 0, 2, 9, 54560 }
		};
	}
}
