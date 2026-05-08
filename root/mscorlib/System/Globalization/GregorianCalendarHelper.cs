using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020009E3 RID: 2531
	[Serializable]
	internal class GregorianCalendarHelper
	{
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005CB9 RID: 23737 RVA: 0x0013DFE9 File Offset: 0x0013C1E9
		internal int MaxYear
		{
			get
			{
				return this.m_maxYear;
			}
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x0013DFF4 File Offset: 0x0013C1F4
		internal GregorianCalendarHelper(Calendar cal, EraInfo[] eraInfo)
		{
			this.m_Cal = cal;
			this.m_EraInfo = eraInfo;
			this.m_minDate = this.m_Cal.MinSupportedDateTime;
			this.m_maxYear = this.m_EraInfo[0].maxEraYear;
			this.m_minYear = this.m_EraInfo[0].minEraYear;
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x0013E058 File Offset: 0x0013C258
		private int GetYearOffset(int year, int era, bool throwOnError)
		{
			if (year < 0)
			{
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("year", "Non-negative number required.");
				}
				return -1;
			}
			else
			{
				if (era == 0)
				{
					era = this.m_Cal.CurrentEraValue;
				}
				int i = 0;
				while (i < this.m_EraInfo.Length)
				{
					if (era == this.m_EraInfo[i].era)
					{
						if (year >= this.m_EraInfo[i].minEraYear)
						{
							if (year <= this.m_EraInfo[i].maxEraYear)
							{
								return this.m_EraInfo[i].yearOffset;
							}
							if (!AppContextSwitches.EnforceJapaneseEraYearRanges)
							{
								int num = year - this.m_EraInfo[i].maxEraYear;
								for (int j = i - 1; j >= 0; j--)
								{
									if (num <= this.m_EraInfo[j].maxEraYear)
									{
										return this.m_EraInfo[i].yearOffset;
									}
									num -= this.m_EraInfo[j].maxEraYear;
								}
							}
						}
						if (throwOnError)
						{
							throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, "Valid values are between {0} and {1}, inclusive.", this.m_EraInfo[i].minEraYear, this.m_EraInfo[i].maxEraYear));
						}
						break;
					}
					else
					{
						i++;
					}
				}
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("era", "Era value was not valid.");
				}
				return -1;
			}
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x0013E190 File Offset: 0x0013C390
		internal int GetGregorianYear(int year, int era)
		{
			return this.GetYearOffset(year, era, true) + year;
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x0013E19D File Offset: 0x0013C39D
		internal bool IsValidYear(int year, int era)
		{
			return this.GetYearOffset(year, era, false) >= 0;
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x0013E1B0 File Offset: 0x0013C3B0
		internal virtual int GetDatePart(long ticks, int part)
		{
			this.CheckTicksRange(ticks);
			int i = (int)(ticks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			if (part == 0)
			{
				return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			}
			i -= num4 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			int num5 = i >> 6;
			while (i >= array[num5])
			{
				num5++;
			}
			if (part == 2)
			{
				return num5;
			}
			return i - array[num5 - 1] + 1;
		}

		// Token: 0x06005CBF RID: 23743 RVA: 0x0013E298 File Offset: 0x0013C498
		internal static long GetAbsoluteDate(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					return (long)(num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1);
				}
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x06005CC0 RID: 23744 RVA: 0x0013E323 File Offset: 0x0013C523
		internal static long DateToTicks(int year, int month, int day)
		{
			return GregorianCalendarHelper.GetAbsoluteDate(year, month, day) * 864000000000L;
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x0013E338 File Offset: 0x0013C538
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x0013E3C0 File Offset: 0x0013C5C0
		internal void CheckTicksRange(long ticks)
		{
			if (ticks < this.m_Cal.MinSupportedDateTime.Ticks || ticks > this.m_Cal.MaxSupportedDateTime.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime));
			}
		}

		// Token: 0x06005CC3 RID: 23747 RVA: 0x0013E438 File Offset: 0x0013C638
		public DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), -120000, 120000));
			}
			this.CheckTicksRange(time.Ticks);
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
			int[] array = ((num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long num6 = GregorianCalendarHelper.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num6, this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime);
			return new DateTime(num6);
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x0013E562 File Offset: 0x0013C762
		public DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x06005CC5 RID: 23749 RVA: 0x0013E56F File Offset: 0x0013C76F
		public int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x0013E57F File Offset: 0x0013C77F
		public DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			return (DayOfWeek)((time.Ticks / 864000000000L + 1L) % 7L);
		}

		// Token: 0x06005CC7 RID: 23751 RVA: 0x0013E5A6 File Offset: 0x0013C7A6
		public int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x0013E5B8 File Offset: 0x0013C7B8
		public int GetDaysInMonth(int year, int month, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x0013E617 File Offset: 0x0013C817
		public int GetDaysInYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x06005CCA RID: 23754 RVA: 0x0013E644 File Offset: 0x0013C844
		public int GetEra(DateTime time)
		{
			long ticks = time.Ticks;
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return this.m_EraInfo[i].era;
				}
			}
			throw new ArgumentOutOfRangeException(Environment.GetResourceString("Time value was out of era range."));
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06005CCB RID: 23755 RVA: 0x0013E69C File Offset: 0x0013C89C
		public int[] Eras
		{
			get
			{
				if (this.m_eras == null)
				{
					this.m_eras = new int[this.m_EraInfo.Length];
					for (int i = 0; i < this.m_EraInfo.Length; i++)
					{
						this.m_eras[i] = this.m_EraInfo[i].era;
					}
				}
				return (int[])this.m_eras.Clone();
			}
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x0013E6FC File Offset: 0x0013C8FC
		public int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x0013E70C File Offset: 0x0013C90C
		public int GetMonthsInYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return 12;
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x0013E71C File Offset: 0x0013C91C
		public int GetYear(DateTime time)
		{
			long ticks = time.Ticks;
			int datePart = this.GetDatePart(ticks, 0);
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return datePart - this.m_EraInfo[i].yearOffset;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("No Era was supplied."));
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x0013E77C File Offset: 0x0013C97C
		public int GetYear(int year, DateTime time)
		{
			long ticks = time.Ticks;
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return year - this.m_EraInfo[i].yearOffset;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("No Era was supplied."));
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x0013E7D4 File Offset: 0x0013C9D4
		public bool IsLeapDay(int year, int month, int day, int era)
		{
			if (day < 1 || day > this.GetDaysInMonth(year, month, era))
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, this.GetDaysInMonth(year, month, era)));
			}
			return this.IsLeapYear(year, era) && (month == 2 && day == 29);
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x0013E83F File Offset: 0x0013CA3F
		public int GetLeapMonth(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return 0;
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x0013E84C File Offset: 0x0013CA4C
		public bool IsLeapMonth(int year, int month, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, 12));
			}
			return false;
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x0013E899 File Offset: 0x0013CA99
		public bool IsLeapYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x0013E8C0 File Offset: 0x0013CAC0
		public DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			year = this.GetGregorianYear(year, era);
			long num = GregorianCalendarHelper.DateToTicks(year, month, day) + GregorianCalendarHelper.TimeToTicks(hour, minute, second, millisecond);
			this.CheckTicksRange(num);
			return new DateTime(num);
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x0013E8FC File Offset: 0x0013CAFC
		public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			this.CheckTicksRange(time.Ticks);
			return GregorianCalendar.GetDefaultInstance().GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x0013E918 File Offset: 0x0013CB18
		public int ToFourDigitYear(int year, int twoDigitYearMax)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Positive number required."));
			}
			if (year < 100)
			{
				int num = year % 100;
				return (twoDigitYearMax / 100 - ((num > twoDigitYearMax % 100) ? 1 : 0)) * 100 + num;
			}
			if (year < this.m_minYear || year > this.m_maxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), this.m_minYear, this.m_maxYear));
			}
			return year;
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x0013E9A6 File Offset: 0x0013CBA6
		// Note: this type is marked as 'beforefieldinit'.
		static GregorianCalendarHelper()
		{
		}

		// Token: 0x04003822 RID: 14370
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x04003823 RID: 14371
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x04003824 RID: 14372
		internal const long TicksPerMinute = 600000000L;

		// Token: 0x04003825 RID: 14373
		internal const long TicksPerHour = 36000000000L;

		// Token: 0x04003826 RID: 14374
		internal const long TicksPerDay = 864000000000L;

		// Token: 0x04003827 RID: 14375
		internal const int MillisPerSecond = 1000;

		// Token: 0x04003828 RID: 14376
		internal const int MillisPerMinute = 60000;

		// Token: 0x04003829 RID: 14377
		internal const int MillisPerHour = 3600000;

		// Token: 0x0400382A RID: 14378
		internal const int MillisPerDay = 86400000;

		// Token: 0x0400382B RID: 14379
		internal const int DaysPerYear = 365;

		// Token: 0x0400382C RID: 14380
		internal const int DaysPer4Years = 1461;

		// Token: 0x0400382D RID: 14381
		internal const int DaysPer100Years = 36524;

		// Token: 0x0400382E RID: 14382
		internal const int DaysPer400Years = 146097;

		// Token: 0x0400382F RID: 14383
		internal const int DaysTo10000 = 3652059;

		// Token: 0x04003830 RID: 14384
		internal const long MaxMillis = 315537897600000L;

		// Token: 0x04003831 RID: 14385
		internal const int DatePartYear = 0;

		// Token: 0x04003832 RID: 14386
		internal const int DatePartDayOfYear = 1;

		// Token: 0x04003833 RID: 14387
		internal const int DatePartMonth = 2;

		// Token: 0x04003834 RID: 14388
		internal const int DatePartDay = 3;

		// Token: 0x04003835 RID: 14389
		internal static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x04003836 RID: 14390
		internal static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x04003837 RID: 14391
		[OptionalField(VersionAdded = 1)]
		internal int m_maxYear = 9999;

		// Token: 0x04003838 RID: 14392
		[OptionalField(VersionAdded = 1)]
		internal int m_minYear;

		// Token: 0x04003839 RID: 14393
		internal Calendar m_Cal;

		// Token: 0x0400383A RID: 14394
		[OptionalField(VersionAdded = 1)]
		internal EraInfo[] m_EraInfo;

		// Token: 0x0400383B RID: 14395
		[OptionalField(VersionAdded = 1)]
		internal int[] m_eras;

		// Token: 0x0400383C RID: 14396
		[OptionalField(VersionAdded = 1)]
		internal DateTime m_minDate;
	}
}
