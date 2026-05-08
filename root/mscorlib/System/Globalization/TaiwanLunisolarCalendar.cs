using System;

namespace System.Globalization
{
	// Token: 0x020009F0 RID: 2544
	[Serializable]
	public class TaiwanLunisolarCalendar : EastAsianLunisolarCalendar
	{
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06005E1D RID: 24093 RVA: 0x00141E98 File Offset: 0x00140098
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanLunisolarCalendar.minDate;
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005E1E RID: 24094 RVA: 0x00141E9F File Offset: 0x0014009F
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return TaiwanLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x0013C897 File Offset: 0x0013AA97
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 384;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06005E20 RID: 24096 RVA: 0x00141EA6 File Offset: 0x001400A6
		internal override int MinCalendarYear
		{
			get
			{
				return 1912;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06005E21 RID: 24097 RVA: 0x00140AD0 File Offset: 0x0013ECD0
		internal override int MaxCalendarYear
		{
			get
			{
				return 2050;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06005E22 RID: 24098 RVA: 0x00141E98 File Offset: 0x00140098
		internal override DateTime MinDate
		{
			get
			{
				return TaiwanLunisolarCalendar.minDate;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06005E23 RID: 24099 RVA: 0x00141E9F File Offset: 0x0014009F
		internal override DateTime MaxDate
		{
			get
			{
				return TaiwanLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06005E24 RID: 24100 RVA: 0x00141EAD File Offset: 0x001400AD
		internal override EraInfo[] CalEraInfo
		{
			get
			{
				return TaiwanLunisolarCalendar.taiwanLunisolarEraInfo;
			}
		}

		// Token: 0x06005E25 RID: 24101 RVA: 0x00141EB4 File Offset: 0x001400B4
		internal override int GetYearInfo(int LunarYear, int Index)
		{
			if (LunarYear < 1912 || LunarYear > 2050)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1912, 2050));
			}
			return TaiwanLunisolarCalendar.yinfo[LunarYear - 1912, Index];
		}

		// Token: 0x06005E26 RID: 24102 RVA: 0x00141F16 File Offset: 0x00140116
		internal override int GetYear(int year, DateTime time)
		{
			return this.helper.GetYear(year, time);
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x00141F25 File Offset: 0x00140125
		internal override int GetGregorianYear(int year, int era)
		{
			return this.helper.GetGregorianYear(year, era);
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x00141F34 File Offset: 0x00140134
		public TaiwanLunisolarCalendar()
		{
			this.helper = new GregorianCalendarHelper(this, TaiwanLunisolarCalendar.taiwanLunisolarEraInfo);
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x00141F4D File Offset: 0x0014014D
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06005E2A RID: 24106 RVA: 0x0001A197 File Offset: 0x00018397
		internal override int BaseCalendarID
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x00141F5B File Offset: 0x0014015B
		internal override int ID
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06005E2C RID: 24108 RVA: 0x00141F5F File Offset: 0x0014015F
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x00141F6C File Offset: 0x0014016C
		// Note: this type is marked as 'beforefieldinit'.
		static TaiwanLunisolarCalendar()
		{
		}

		// Token: 0x040038C1 RID: 14529
		internal static EraInfo[] taiwanLunisolarEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1912, 1, 1, 1911, 1, 8088)
		};

		// Token: 0x040038C2 RID: 14530
		internal GregorianCalendarHelper helper;

		// Token: 0x040038C3 RID: 14531
		internal const int MIN_LUNISOLAR_YEAR = 1912;

		// Token: 0x040038C4 RID: 14532
		internal const int MAX_LUNISOLAR_YEAR = 2050;

		// Token: 0x040038C5 RID: 14533
		internal const int MIN_GREGORIAN_YEAR = 1912;

		// Token: 0x040038C6 RID: 14534
		internal const int MIN_GREGORIAN_MONTH = 2;

		// Token: 0x040038C7 RID: 14535
		internal const int MIN_GREGORIAN_DAY = 18;

		// Token: 0x040038C8 RID: 14536
		internal const int MAX_GREGORIAN_YEAR = 2051;

		// Token: 0x040038C9 RID: 14537
		internal const int MAX_GREGORIAN_MONTH = 2;

		// Token: 0x040038CA RID: 14538
		internal const int MAX_GREGORIAN_DAY = 10;

		// Token: 0x040038CB RID: 14539
		internal static DateTime minDate = new DateTime(1912, 2, 18);

		// Token: 0x040038CC RID: 14540
		internal static DateTime maxDate = new DateTime(new DateTime(2051, 2, 10, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x040038CD RID: 14541
		private static readonly int[,] yinfo = new int[,]
		{
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
			{ 3, 1, 23, 21936 }
		};
	}
}
