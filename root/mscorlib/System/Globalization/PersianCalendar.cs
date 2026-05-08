using System;

namespace System.Globalization
{
	// Token: 0x020009DB RID: 2523
	[Serializable]
	public class PersianCalendar : Calendar
	{
		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06005BE6 RID: 23526 RVA: 0x0013B056 File Offset: 0x00139256
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return PersianCalendar.minDate;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06005BE7 RID: 23527 RVA: 0x0013B05D File Offset: 0x0013925D
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return PersianCalendar.maxDate;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06005BE8 RID: 23528 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x0013B064 File Offset: 0x00139264
		public PersianCalendar()
		{
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06005BEA RID: 23530 RVA: 0x00003FB7 File Offset: 0x000021B7
		internal override int BaseCalendarID
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06005BEB RID: 23531 RVA: 0x0013B06C File Offset: 0x0013926C
		internal override int ID
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x0013B070 File Offset: 0x00139270
		private long GetAbsoluteDatePersian(int year, int month, int day)
		{
			if (year >= 1 && year <= 9378 && month >= 1 && month <= 12)
			{
				int num = PersianCalendar.DaysInPreviousMonths(month) + day - 1;
				int num2 = (int)(365.242189 * (double)(year - 1));
				return CalendricalCalculationsHelper.PersianNewYearOnOrBefore(PersianCalendar.PersianEpoch + (long)num2 + 180L) + (long)num;
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x0013B0D8 File Offset: 0x001392D8
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < PersianCalendar.minDate.Ticks || ticks > PersianCalendar.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), PersianCalendar.minDate, PersianCalendar.maxDate));
			}
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x0013B132 File Offset: 0x00139332
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != PersianCalendar.PersianEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x0013B154 File Offset: 0x00139354
		internal static void CheckYearRange(int year, int era)
		{
			PersianCalendar.CheckEraRange(era);
			if (year < 1 || year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9378));
			}
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0013B1A4 File Offset: 0x001393A4
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378 && month > 10)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 10));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x0013B210 File Offset: 0x00139410
		private static int MonthFromOrdinalDay(int ordinalDay)
		{
			int num = 0;
			while (ordinalDay > PersianCalendar.DaysToMonth[num])
			{
				num++;
			}
			return num;
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x0013B230 File Offset: 0x00139430
		private static int DaysInPreviousMonths(int month)
		{
			month--;
			return PersianCalendar.DaysToMonth[month];
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x0013B240 File Offset: 0x00139440
		internal int GetDatePart(long ticks, int part)
		{
			PersianCalendar.CheckTicksRange(ticks);
			long num = ticks / 864000000000L + 1L;
			int num2 = (int)Math.Floor((double)(CalendricalCalculationsHelper.PersianNewYearOnOrBefore(num) - PersianCalendar.PersianEpoch) / 365.242189 + 0.5) + 1;
			if (part == 0)
			{
				return num2;
			}
			int num3 = (int)(num - CalendricalCalculationsHelper.GetNumberOfDays(this.ToDateTime(num2, 1, 1, 0, 0, 0, 0, 1)));
			if (part == 1)
			{
				return num3;
			}
			int num4 = PersianCalendar.MonthFromOrdinalDay(num3);
			if (part == 2)
			{
				return num4;
			}
			int num5 = num3 - PersianCalendar.DaysInPreviousMonths(num4);
			if (part == 3)
			{
				return num5;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Internal Error in DateTime and Calendar operations."));
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x0013B2DC File Offset: 0x001394DC
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -120000, 120000));
			}
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			int num4 = num2 - 1 + months;
			if (num4 >= 0)
			{
				num2 = num4 % 12 + 1;
				num += num4 / 12;
			}
			else
			{
				num2 = 12 + (num4 + 1) % 12;
				num += (num4 - 11) / 12;
			}
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long num5 = this.GetAbsoluteDatePersian(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num5, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num5);
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x0013B3D5 File Offset: 0x001395D5
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x0013B3E2 File Offset: 0x001395E2
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x0013B3F2 File Offset: 0x001395F2
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x0013B40B File Offset: 0x0013960B
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x0013B41C File Offset: 0x0013961C
		public override int GetDaysInMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			if (month == 10 && year == 9378)
			{
				return 13;
			}
			int num = PersianCalendar.DaysToMonth[month] - PersianCalendar.DaysToMonth[month - 1];
			if (month == 12 && !this.IsLeapYear(year))
			{
				num--;
			}
			return num;
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x0013B466 File Offset: 0x00139666
		public override int GetDaysInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return PersianCalendar.DaysToMonth[9] + 13;
			}
			if (!this.IsLeapYear(year, 0))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x0013B498 File Offset: 0x00139698
		public override int GetEra(DateTime time)
		{
			PersianCalendar.CheckTicksRange(time.Ticks);
			return PersianCalendar.PersianEra;
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06005BFC RID: 23548 RVA: 0x0013B4AB File Offset: 0x001396AB
		public override int[] Eras
		{
			get
			{
				return new int[] { PersianCalendar.PersianEra };
			}
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x0013B4BB File Offset: 0x001396BB
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x0013B4CB File Offset: 0x001396CB
		public override int GetMonthsInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return 10;
			}
			return 12;
		}

		// Token: 0x06005BFF RID: 23551 RVA: 0x0013B4E1 File Offset: 0x001396E1
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x06005C00 RID: 23552 RVA: 0x0013B4F4 File Offset: 0x001396F4
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
			}
			return this.IsLeapYear(year, era) && month == 12 && day == 30;
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x0013B556 File Offset: 0x00139756
		public override int GetLeapMonth(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return 0;
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x0013B560 File Offset: 0x00139760
		public override bool IsLeapMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x0013B56B File Offset: 0x0013976B
		public override bool IsLeapYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return year != 9378 && this.GetAbsoluteDatePersian(year + 1, 1, 1) - this.GetAbsoluteDatePersian(year, 1, 1) == 366L;
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x0013B59C File Offset: 0x0013979C
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
			}
			long absoluteDatePersian = this.GetAbsoluteDatePersian(year, month, day);
			if (absoluteDatePersian >= 0L)
			{
				return new DateTime(absoluteDatePersian * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06005C05 RID: 23557 RVA: 0x0013B625 File Offset: 0x00139825
		// (set) Token: 0x06005C06 RID: 23558 RVA: 0x0013B64C File Offset: 0x0013984C
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1410);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9378)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, 9378));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x0013B6A4 File Offset: 0x001398A4
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year < 100)
			{
				return base.ToFourDigitYear(year);
			}
			if (year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9378));
			}
			return year;
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x0013B710 File Offset: 0x00139910
		// Note: this type is marked as 'beforefieldinit'.
		static PersianCalendar()
		{
		}

		// Token: 0x040037A5 RID: 14245
		public static readonly int PersianEra = 1;

		// Token: 0x040037A6 RID: 14246
		internal static long PersianEpoch = new DateTime(622, 3, 22).Ticks / 864000000000L;

		// Token: 0x040037A7 RID: 14247
		private const int ApproximateHalfYear = 180;

		// Token: 0x040037A8 RID: 14248
		internal const int DatePartYear = 0;

		// Token: 0x040037A9 RID: 14249
		internal const int DatePartDayOfYear = 1;

		// Token: 0x040037AA RID: 14250
		internal const int DatePartMonth = 2;

		// Token: 0x040037AB RID: 14251
		internal const int DatePartDay = 3;

		// Token: 0x040037AC RID: 14252
		internal const int MonthsPerYear = 12;

		// Token: 0x040037AD RID: 14253
		internal static int[] DaysToMonth = new int[]
		{
			0, 31, 62, 93, 124, 155, 186, 216, 246, 276,
			306, 336, 366
		};

		// Token: 0x040037AE RID: 14254
		internal const int MaxCalendarYear = 9378;

		// Token: 0x040037AF RID: 14255
		internal const int MaxCalendarMonth = 10;

		// Token: 0x040037B0 RID: 14256
		internal const int MaxCalendarDay = 13;

		// Token: 0x040037B1 RID: 14257
		internal static DateTime minDate = new DateTime(622, 3, 22);

		// Token: 0x040037B2 RID: 14258
		internal static DateTime maxDate = DateTime.MaxValue;

		// Token: 0x040037B3 RID: 14259
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1410;
	}
}
