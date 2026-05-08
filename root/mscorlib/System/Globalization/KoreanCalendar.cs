using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020009EB RID: 2539
	[ComVisible(true)]
	[Serializable]
	public class KoreanCalendar : Calendar
	{
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x0013B77A File Offset: 0x0013997A
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06005D7A RID: 23930 RVA: 0x0013B781 File Offset: 0x00139981
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06005D7B RID: 23931 RVA: 0x00003FB7 File Offset: 0x000021B7
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x00140858 File Offset: 0x0013EA58
		public KoreanCalendar()
		{
			try
			{
				new CultureInfo("ko-KR");
			}
			catch (ArgumentException ex)
			{
				throw new TypeInitializationException(base.GetType().FullName, ex);
			}
			this.helper = new GregorianCalendarHelper(this, KoreanCalendar.koreanEraInfo);
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06005D7D RID: 23933 RVA: 0x000348A8 File Offset: 0x00032AA8
		internal override int ID
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x001408AC File Offset: 0x0013EAAC
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x001408BB File Offset: 0x0013EABB
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x001408CA File Offset: 0x0013EACA
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x001408DA File Offset: 0x0013EADA
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x001408E9 File Offset: 0x0013EAE9
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x06005D83 RID: 23939 RVA: 0x001408F7 File Offset: 0x0013EAF7
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x06005D84 RID: 23940 RVA: 0x00140905 File Offset: 0x0013EB05
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x06005D85 RID: 23941 RVA: 0x00140913 File Offset: 0x0013EB13
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x06005D86 RID: 23942 RVA: 0x00140922 File Offset: 0x0013EB22
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06005D87 RID: 23943 RVA: 0x00140932 File Offset: 0x0013EB32
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x00140940 File Offset: 0x0013EB40
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x06005D89 RID: 23945 RVA: 0x0014094E File Offset: 0x0013EB4E
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x06005D8A RID: 23946 RVA: 0x0014095C File Offset: 0x0013EB5C
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x0014096E File Offset: 0x0013EB6E
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x0014097D File Offset: 0x0013EB7D
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x0014098C File Offset: 0x0013EB8C
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x0014099C File Offset: 0x0013EB9C
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x001409C1 File Offset: 0x0013EBC1
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06005D90 RID: 23952 RVA: 0x001409CE File Offset: 0x0013EBCE
		// (set) Token: 0x06005D91 RID: 23953 RVA: 0x001409F8 File Offset: 0x0013EBF8
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 4362);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.helper.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, this.helper.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x00140A5B File Offset: 0x0013EC5B
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			return this.helper.ToFourDigitYear(year, this.TwoDigitYearMax);
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x00140A88 File Offset: 0x0013EC88
		// Note: this type is marked as 'beforefieldinit'.
		static KoreanCalendar()
		{
		}

		// Token: 0x04003886 RID: 14470
		public const int KoreanEra = 1;

		// Token: 0x04003887 RID: 14471
		internal static EraInfo[] koreanEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1, 1, 1, -2333, 2334, 12332)
		};

		// Token: 0x04003888 RID: 14472
		internal GregorianCalendarHelper helper;

		// Token: 0x04003889 RID: 14473
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 4362;
	}
}
