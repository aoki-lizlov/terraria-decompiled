using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020009EA RID: 2538
	[ComVisible(true)]
	[Serializable]
	public class JulianCalendar : Calendar
	{
		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06005D59 RID: 23897 RVA: 0x0013B77A File Offset: 0x0013997A
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06005D5A RID: 23898 RVA: 0x0013B781 File Offset: 0x00139981
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06005D5B RID: 23899 RVA: 0x00003FB7 File Offset: 0x000021B7
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x0014027C File Offset: 0x0013E47C
		public JulianCalendar()
		{
			this.twoDigitYearMax = 2029;
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06005D5D RID: 23901 RVA: 0x00034CEE File Offset: 0x00032EEE
		internal override int ID
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x0014029A File Offset: 0x0013E49A
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != JulianCalendar.JulianEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x001402BC File Offset: 0x0013E4BC
		internal void CheckYearEraRange(int year, int era)
		{
			JulianCalendar.CheckEraRange(era);
			if (year <= 0 || year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, this.MaxYear));
			}
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0014030C File Offset: 0x0013E50C
		internal static void CheckMonthRange(int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x0014032C File Offset: 0x0013E52C
		internal static void CheckDayRange(int year, int month, int day)
		{
			if (year == 1 && month == 1 && day < 3)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
			}
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num = array[month] - array[month - 1];
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, num));
			}
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x001403A8 File Offset: 0x0013E5A8
		internal static int GetDatePart(long ticks, int part)
		{
			int i = (int)((ticks + 1728000000000L) / 864000000000L);
			int num = i / 1461;
			i -= num * 1461;
			int num2 = i / 365;
			if (num2 == 4)
			{
				num2 = 3;
			}
			if (part == 0)
			{
				return num * 4 + num2 + 1;
			}
			i -= num2 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = ((num2 == 3) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num3 = i >> 6;
			while (i >= array[num3])
			{
				num3++;
			}
			if (part == 2)
			{
				return num3;
			}
			return i - array[num3 - 1] + 1;
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x00140444 File Offset: 0x0013E644
		internal static long DateToTicks(int year, int month, int day)
		{
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num = year - 1;
			return (long)(num * 365 + num / 4 + array[month - 1] + day - 1 - 2) * 864000000000L;
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x0014048C File Offset: 0x0013E68C
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -120000, 120000));
			}
			int num = JulianCalendar.GetDatePart(time.Ticks, 0);
			int num2 = JulianCalendar.GetDatePart(time.Ticks, 2);
			int num3 = JulianCalendar.GetDatePart(time.Ticks, 3);
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
			int[] array = ((num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long num6 = JulianCalendar.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num6, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num6);
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x0013B3D5 File Offset: 0x001395D5
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x0014059C File Offset: 0x0013E79C
		public override int GetDayOfMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x0013B3F2 File Offset: 0x001395F2
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x001405AB File Offset: 0x0013E7AB
		public override int GetDayOfYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x001405BC File Offset: 0x0013E7BC
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x001405F2 File Offset: 0x0013E7F2
		public override int GetDaysInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x00140609 File Offset: 0x0013E809
		public override int GetEra(DateTime time)
		{
			return JulianCalendar.JulianEra;
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x00140610 File Offset: 0x0013E810
		public override int GetMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x0014061F File Offset: 0x0013E81F
		public override int[] Eras
		{
			get
			{
				return new int[] { JulianCalendar.JulianEra };
			}
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x0014062F File Offset: 0x0013E82F
		public override int GetMonthsInYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 12;
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x0014063B File Offset: 0x0013E83B
		public override int GetYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x0014064A File Offset: 0x0013E84A
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			JulianCalendar.CheckMonthRange(month);
			if (this.IsLeapYear(year, era))
			{
				JulianCalendar.CheckDayRange(year, month, day);
				return month == 2 && day == 29;
			}
			JulianCalendar.CheckDayRange(year, month, day);
			return false;
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x0014067A File Offset: 0x0013E87A
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 0;
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x00140685 File Offset: 0x0013E885
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			return false;
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x00140696 File Offset: 0x0013E896
		public override bool IsLeapYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return year % 4 == 0;
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x001406A8 File Offset: 0x0013E8A8
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			JulianCalendar.CheckDayRange(year, month, day);
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 999));
			}
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return new DateTime(JulianCalendar.DateToTicks(year, month, day) + new TimeSpan(0, hour, minute, second, millisecond).Ticks);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06005D75 RID: 23925 RVA: 0x0013BCDD File Offset: 0x00139EDD
		// (set) Token: 0x06005D76 RID: 23926 RVA: 0x00140760 File Offset: 0x0013E960
		public override int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, this.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x001407BC File Offset: 0x0013E9BC
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument must be between {0} and {1}."), 1, this.MaxYear));
			}
			return base.ToFourDigitYear(year);
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x00140822 File Offset: 0x0013EA22
		// Note: this type is marked as 'beforefieldinit'.
		static JulianCalendar()
		{
		}

		// Token: 0x0400387C RID: 14460
		public static readonly int JulianEra = 1;

		// Token: 0x0400387D RID: 14461
		private const int DatePartYear = 0;

		// Token: 0x0400387E RID: 14462
		private const int DatePartDayOfYear = 1;

		// Token: 0x0400387F RID: 14463
		private const int DatePartMonth = 2;

		// Token: 0x04003880 RID: 14464
		private const int DatePartDay = 3;

		// Token: 0x04003881 RID: 14465
		private const int JulianDaysPerYear = 365;

		// Token: 0x04003882 RID: 14466
		private const int JulianDaysPer4Years = 1461;

		// Token: 0x04003883 RID: 14467
		private static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x04003884 RID: 14468
		private static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x04003885 RID: 14469
		internal int MaxYear = 9999;
	}
}
