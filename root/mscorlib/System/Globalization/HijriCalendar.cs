using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009E7 RID: 2535
	[ComVisible(true)]
	[Serializable]
	public class HijriCalendar : Calendar
	{
		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06005CFE RID: 23806 RVA: 0x0013F440 File Offset: 0x0013D640
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HijriCalendar.calendarMinValue;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06005CFF RID: 23807 RVA: 0x0013F447 File Offset: 0x0013D647
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HijriCalendar.calendarMaxValue;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06005D00 RID: 23808 RVA: 0x00015289 File Offset: 0x00013489
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunarCalendar;
			}
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x0013F44E File Offset: 0x0013D64E
		public HijriCalendar()
		{
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06005D02 RID: 23810 RVA: 0x00019E33 File Offset: 0x00018033
		internal override int ID
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06005D03 RID: 23811 RVA: 0x0013F461 File Offset: 0x0013D661
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 354;
			}
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x0013F468 File Offset: 0x0013D668
		private long GetAbsoluteDateHijri(int y, int m, int d)
		{
			return this.DaysUpToHijriYear(y) + (long)HijriCalendar.HijriMonthDays[m - 1] + (long)d - 1L - (long)this.HijriAdjustment;
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x0013F48C File Offset: 0x0013D68C
		private long DaysUpToHijriYear(int HijriYear)
		{
			int num = (HijriYear - 1) / 30 * 30;
			int i = HijriYear - num - 1;
			long num2 = (long)num * 10631L / 30L + 227013L;
			while (i > 0)
			{
				num2 += (long)(354 + (this.IsLeapYear(i, 0) ? 1 : 0));
				i--;
			}
			return num2;
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06005D06 RID: 23814 RVA: 0x0013F4E1 File Offset: 0x0013D6E1
		// (set) Token: 0x06005D07 RID: 23815 RVA: 0x0013F504 File Offset: 0x0013D704
		public int HijriAdjustment
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_HijriAdvance == -2147483648)
				{
					this.m_HijriAdvance = HijriCalendar.GetAdvanceHijriDate();
				}
				return this.m_HijriAdvance;
			}
			set
			{
				if (value < -2 || value > 2)
				{
					throw new ArgumentOutOfRangeException("HijriAdjustment", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument must be between {0} and {1}."), -2, 2));
				}
				base.VerifyWritable();
				this.m_HijriAdvance = value;
			}
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x0000408A File Offset: 0x0000228A
		[SecurityCritical]
		private static int GetAdvanceHijriDate()
		{
			return 0;
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x0013F554 File Offset: 0x0013D754
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < HijriCalendar.calendarMinValue.Ticks || ticks > HijriCalendar.calendarMaxValue.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), HijriCalendar.calendarMinValue, HijriCalendar.calendarMaxValue));
			}
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x0013F5AE File Offset: 0x0013D7AE
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != HijriCalendar.HijriEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x0013F5D0 File Offset: 0x0013D7D0
		internal static void CheckYearRange(int year, int era)
		{
			HijriCalendar.CheckEraRange(era);
			if (year < 1 || year > 9666)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9666));
			}
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x0013F620 File Offset: 0x0013D820
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			if (year == 9666 && month > 4)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 4));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
		}

		// Token: 0x06005D0D RID: 23821 RVA: 0x0013F68C File Offset: 0x0013D88C
		internal virtual int GetDatePart(long ticks, int part)
		{
			HijriCalendar.CheckTicksRange(ticks);
			long num = ticks / 864000000000L + 1L;
			num += (long)this.HijriAdjustment;
			int num2 = (int)((num - 227013L) * 30L / 10631L) + 1;
			long num3 = this.DaysUpToHijriYear(num2);
			long num4 = (long)this.GetDaysInYear(num2, 0);
			if (num < num3)
			{
				num3 -= num4;
				num2--;
			}
			else if (num == num3)
			{
				num2--;
				num3 -= (long)this.GetDaysInYear(num2, 0);
			}
			else if (num > num3 + num4)
			{
				num3 += num4;
				num2++;
			}
			if (part == 0)
			{
				return num2;
			}
			int num5 = 1;
			num -= num3;
			if (part == 1)
			{
				return (int)num;
			}
			while (num5 <= 12 && num > (long)HijriCalendar.HijriMonthDays[num5 - 1])
			{
				num5++;
			}
			num5--;
			if (part == 2)
			{
				return num5;
			}
			int num6 = (int)(num - (long)HijriCalendar.HijriMonthDays[num5 - 1]);
			if (part == 3)
			{
				return num6;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Internal Error in DateTime and Calendar operations."));
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x0013F778 File Offset: 0x0013D978
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
			long num5 = this.GetAbsoluteDateHijri(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num5, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num5);
		}

		// Token: 0x06005D0F RID: 23823 RVA: 0x0013B3D5 File Offset: 0x001395D5
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x06005D10 RID: 23824 RVA: 0x0013F871 File Offset: 0x0013DA71
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x0013B3F2 File Offset: 0x001395F2
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x06005D12 RID: 23826 RVA: 0x0013F881 File Offset: 0x0013DA81
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x06005D13 RID: 23827 RVA: 0x0013F891 File Offset: 0x0013DA91
		public override int GetDaysInMonth(int year, int month, int era)
		{
			HijriCalendar.CheckYearMonthRange(year, month, era);
			if (month == 12)
			{
				if (!this.IsLeapYear(year, 0))
				{
					return 29;
				}
				return 30;
			}
			else
			{
				if (month % 2 != 1)
				{
					return 29;
				}
				return 30;
			}
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x0013F8BB File Offset: 0x0013DABB
		public override int GetDaysInYear(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			if (!this.IsLeapYear(year, 0))
			{
				return 354;
			}
			return 355;
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x0013F8D9 File Offset: 0x0013DAD9
		public override int GetEra(DateTime time)
		{
			HijriCalendar.CheckTicksRange(time.Ticks);
			return HijriCalendar.HijriEra;
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06005D16 RID: 23830 RVA: 0x0013F8EC File Offset: 0x0013DAEC
		public override int[] Eras
		{
			get
			{
				return new int[] { HijriCalendar.HijriEra };
			}
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x0013F8FC File Offset: 0x0013DAFC
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x0013F90C File Offset: 0x0013DB0C
		public override int GetMonthsInYear(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			return 12;
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x0013F917 File Offset: 0x0013DB17
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x0013F928 File Offset: 0x0013DB28
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
			}
			return this.IsLeapYear(year, era) && month == 12 && day == 30;
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x0013F98A File Offset: 0x0013DB8A
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			return 0;
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x0013F994 File Offset: 0x0013DB94
		public override bool IsLeapMonth(int year, int month, int era)
		{
			HijriCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x0013F99F File Offset: 0x0013DB9F
		public override bool IsLeapYear(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			return (year * 11 + 14) % 30 < 11;
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x0013F9B8 File Offset: 0x0013DBB8
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Day must be between 1 and {0} for month {1}."), daysInMonth, month));
			}
			long absoluteDateHijri = this.GetAbsoluteDateHijri(year, month, day);
			if (absoluteDateHijri >= 0L)
			{
				return new DateTime(absoluteDateHijri * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06005D1F RID: 23839 RVA: 0x0013FA41 File Offset: 0x0013DC41
		// (set) Token: 0x06005D20 RID: 23840 RVA: 0x0013FA68 File Offset: 0x0013DC68
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1451);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9666)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, 9666));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x0013FAC0 File Offset: 0x0013DCC0
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
			if (year > 9666)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 9666));
			}
			return year;
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x0013FB2B File Offset: 0x0013DD2B
		// Note: this type is marked as 'beforefieldinit'.
		static HijriCalendar()
		{
		}

		// Token: 0x04003858 RID: 14424
		public static readonly int HijriEra = 1;

		// Token: 0x04003859 RID: 14425
		internal const int DatePartYear = 0;

		// Token: 0x0400385A RID: 14426
		internal const int DatePartDayOfYear = 1;

		// Token: 0x0400385B RID: 14427
		internal const int DatePartMonth = 2;

		// Token: 0x0400385C RID: 14428
		internal const int DatePartDay = 3;

		// Token: 0x0400385D RID: 14429
		internal const int MinAdvancedHijri = -2;

		// Token: 0x0400385E RID: 14430
		internal const int MaxAdvancedHijri = 2;

		// Token: 0x0400385F RID: 14431
		internal static readonly int[] HijriMonthDays = new int[]
		{
			0, 30, 59, 89, 118, 148, 177, 207, 236, 266,
			295, 325, 355
		};

		// Token: 0x04003860 RID: 14432
		private const string HijriAdvanceRegKeyEntry = "AddHijriDate";

		// Token: 0x04003861 RID: 14433
		private int m_HijriAdvance = int.MinValue;

		// Token: 0x04003862 RID: 14434
		internal const int MaxCalendarYear = 9666;

		// Token: 0x04003863 RID: 14435
		internal const int MaxCalendarMonth = 4;

		// Token: 0x04003864 RID: 14436
		internal const int MaxCalendarDay = 3;

		// Token: 0x04003865 RID: 14437
		internal static readonly DateTime calendarMinValue = new DateTime(622, 7, 18);

		// Token: 0x04003866 RID: 14438
		internal static readonly DateTime calendarMaxValue = DateTime.MaxValue;

		// Token: 0x04003867 RID: 14439
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1451;
	}
}
