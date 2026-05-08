using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009DC RID: 2524
	[ComVisible(true)]
	[Serializable]
	public abstract class Calendar : ICloneable
	{
		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06005C09 RID: 23561 RVA: 0x0013B77A File Offset: 0x0013997A
		[ComVisible(false)]
		public virtual DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06005C0A RID: 23562 RVA: 0x0013B781 File Offset: 0x00139981
		[ComVisible(false)]
		public virtual DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x0013B788 File Offset: 0x00139988
		protected Calendar()
		{
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06005C0C RID: 23564 RVA: 0x0011B48C File Offset: 0x0011968C
		internal virtual int ID
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06005C0D RID: 23565 RVA: 0x0013B79E File Offset: 0x0013999E
		internal virtual int BaseCalendarID
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06005C0E RID: 23566 RVA: 0x0000408A File Offset: 0x0000228A
		[ComVisible(false)]
		public virtual CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.Unknown;
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06005C0F RID: 23567 RVA: 0x0013B7A6 File Offset: 0x001399A6
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x0013B7AE File Offset: 0x001399AE
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((Calendar)obj).SetReadOnlyState(false);
			return obj;
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x0013B7C2 File Offset: 0x001399C2
		[ComVisible(false)]
		public static Calendar ReadOnly(Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (calendar.IsReadOnly)
			{
				return calendar;
			}
			Calendar calendar2 = (Calendar)calendar.MemberwiseClone();
			calendar2.SetReadOnlyState(true);
			return calendar2;
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x0013B7EE File Offset: 0x001399EE
		internal void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x0013B808 File Offset: 0x00139A08
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06005C14 RID: 23572 RVA: 0x0013B811 File Offset: 0x00139A11
		internal virtual int CurrentEraValue
		{
			get
			{
				if (this.m_currentEraValue == -1)
				{
					this.m_currentEraValue = CalendarData.GetCalendarData(this.BaseCalendarID).iCurrentEra;
				}
				return this.m_currentEraValue;
			}
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x0013B838 File Offset: 0x00139A38
		internal static void CheckAddResult(long ticks, DateTime minValue, DateTime maxValue)
		{
			if (ticks < minValue.Ticks || ticks > maxValue.Ticks)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("The result is out of the supported range for this calendar. The result should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), minValue, maxValue));
			}
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x0013B874 File Offset: 0x00139A74
		internal DateTime Add(DateTime time, double value, int scale)
		{
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5));
			if (num <= -315537897600000.0 || num >= 315537897600000.0)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Value to add was out of range."));
			}
			long num2 = (long)num;
			long num3 = time.Ticks + num2 * 10000L;
			Calendar.CheckAddResult(num3, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num3);
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x0013B8FE File Offset: 0x00139AFE
		public virtual DateTime AddMilliseconds(DateTime time, double milliseconds)
		{
			return this.Add(time, milliseconds, 1);
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x0013B909 File Offset: 0x00139B09
		public virtual DateTime AddDays(DateTime time, int days)
		{
			return this.Add(time, (double)days, 86400000);
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x0013B919 File Offset: 0x00139B19
		public virtual DateTime AddHours(DateTime time, int hours)
		{
			return this.Add(time, (double)hours, 3600000);
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x0013B929 File Offset: 0x00139B29
		public virtual DateTime AddMinutes(DateTime time, int minutes)
		{
			return this.Add(time, (double)minutes, 60000);
		}

		// Token: 0x06005C1B RID: 23579
		public abstract DateTime AddMonths(DateTime time, int months);

		// Token: 0x06005C1C RID: 23580 RVA: 0x0013B939 File Offset: 0x00139B39
		public virtual DateTime AddSeconds(DateTime time, int seconds)
		{
			return this.Add(time, (double)seconds, 1000);
		}

		// Token: 0x06005C1D RID: 23581 RVA: 0x0013B949 File Offset: 0x00139B49
		public virtual DateTime AddWeeks(DateTime time, int weeks)
		{
			return this.AddDays(time, weeks * 7);
		}

		// Token: 0x06005C1E RID: 23582
		public abstract DateTime AddYears(DateTime time, int years);

		// Token: 0x06005C1F RID: 23583
		public abstract int GetDayOfMonth(DateTime time);

		// Token: 0x06005C20 RID: 23584
		public abstract DayOfWeek GetDayOfWeek(DateTime time);

		// Token: 0x06005C21 RID: 23585
		public abstract int GetDayOfYear(DateTime time);

		// Token: 0x06005C22 RID: 23586 RVA: 0x0013B955 File Offset: 0x00139B55
		public virtual int GetDaysInMonth(int year, int month)
		{
			return this.GetDaysInMonth(year, month, 0);
		}

		// Token: 0x06005C23 RID: 23587
		public abstract int GetDaysInMonth(int year, int month, int era);

		// Token: 0x06005C24 RID: 23588 RVA: 0x0013B960 File Offset: 0x00139B60
		public virtual int GetDaysInYear(int year)
		{
			return this.GetDaysInYear(year, 0);
		}

		// Token: 0x06005C25 RID: 23589
		public abstract int GetDaysInYear(int year, int era);

		// Token: 0x06005C26 RID: 23590
		public abstract int GetEra(DateTime time);

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06005C27 RID: 23591
		public abstract int[] Eras { get; }

		// Token: 0x06005C28 RID: 23592 RVA: 0x0013B96A File Offset: 0x00139B6A
		public virtual int GetHour(DateTime time)
		{
			return (int)(time.Ticks / 36000000000L % 24L);
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x0013B982 File Offset: 0x00139B82
		public virtual double GetMilliseconds(DateTime time)
		{
			return (double)(time.Ticks / 10000L % 1000L);
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x0013B99A File Offset: 0x00139B9A
		public virtual int GetMinute(DateTime time)
		{
			return (int)(time.Ticks / 600000000L % 60L);
		}

		// Token: 0x06005C2B RID: 23595
		public abstract int GetMonth(DateTime time);

		// Token: 0x06005C2C RID: 23596 RVA: 0x0013B9AF File Offset: 0x00139BAF
		public virtual int GetMonthsInYear(int year)
		{
			return this.GetMonthsInYear(year, 0);
		}

		// Token: 0x06005C2D RID: 23597
		public abstract int GetMonthsInYear(int year, int era);

		// Token: 0x06005C2E RID: 23598 RVA: 0x0013B9B9 File Offset: 0x00139BB9
		public virtual int GetSecond(DateTime time)
		{
			return (int)(time.Ticks / 10000000L % 60L);
		}

		// Token: 0x06005C2F RID: 23599 RVA: 0x0013B9D0 File Offset: 0x00139BD0
		internal int GetFirstDayWeekOfYear(DateTime time, int firstDayOfWeek)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = (this.GetDayOfWeek(time) - (DayOfWeek)(num % 7) - firstDayOfWeek + 14) % 7;
			return (num + num2) / 7 + 1;
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x0013BA04 File Offset: 0x00139C04
		private int GetWeekOfYearFullDays(DateTime time, int firstDayOfWeek, int fullDays)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = this.GetDayOfWeek(time) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek - num2 + 14) % 7;
			if (num3 != 0 && num3 >= fullDays)
			{
				num3 -= 7;
			}
			int num4 = num - num3;
			if (num4 >= 0)
			{
				return num4 / 7 + 1;
			}
			if (time <= this.MinSupportedDateTime.AddDays((double)num))
			{
				return this.GetWeekOfYearOfMinSupportedDateTime(firstDayOfWeek, fullDays);
			}
			return this.GetWeekOfYearFullDays(time.AddDays((double)(-(double)(num + 1))), firstDayOfWeek, fullDays);
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x0013BA80 File Offset: 0x00139C80
		private int GetWeekOfYearOfMinSupportedDateTime(int firstDayOfWeek, int minimumDaysInFirstWeek)
		{
			int num = this.GetDayOfYear(this.MinSupportedDateTime) - 1;
			int num2 = this.GetDayOfWeek(this.MinSupportedDateTime) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek + 7 - num2) % 7;
			if (num3 == 0 || num3 >= minimumDaysInFirstWeek)
			{
				return 1;
			}
			int num4 = this.DaysInYearBeforeMinSupportedYear - 1;
			int num5 = num2 - 1 - num4 % 7;
			int num6 = (firstDayOfWeek - num5 + 14) % 7;
			int num7 = num4 - num6;
			if (num6 >= minimumDaysInFirstWeek)
			{
				num7 += 7;
			}
			return num7 / 7 + 1;
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06005C32 RID: 23602 RVA: 0x0013BAF2 File Offset: 0x00139CF2
		protected virtual int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 365;
			}
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x0013BAFC File Offset: 0x00139CFC
		public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			if (firstDayOfWeek < DayOfWeek.Sunday || firstDayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("firstDayOfWeek", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					DayOfWeek.Sunday,
					DayOfWeek.Saturday
				}));
			}
			switch (rule)
			{
			case CalendarWeekRule.FirstDay:
				return this.GetFirstDayWeekOfYear(time, (int)firstDayOfWeek);
			case CalendarWeekRule.FirstFullWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 7);
			case CalendarWeekRule.FirstFourDayWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 4);
			default:
				throw new ArgumentOutOfRangeException("rule", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					CalendarWeekRule.FirstDay,
					CalendarWeekRule.FirstFourDayWeek
				}));
			}
		}

		// Token: 0x06005C34 RID: 23604
		public abstract int GetYear(DateTime time);

		// Token: 0x06005C35 RID: 23605 RVA: 0x0013BB9B File Offset: 0x00139D9B
		public virtual bool IsLeapDay(int year, int month, int day)
		{
			return this.IsLeapDay(year, month, day, 0);
		}

		// Token: 0x06005C36 RID: 23606
		public abstract bool IsLeapDay(int year, int month, int day, int era);

		// Token: 0x06005C37 RID: 23607 RVA: 0x0013BBA7 File Offset: 0x00139DA7
		public virtual bool IsLeapMonth(int year, int month)
		{
			return this.IsLeapMonth(year, month, 0);
		}

		// Token: 0x06005C38 RID: 23608
		public abstract bool IsLeapMonth(int year, int month, int era);

		// Token: 0x06005C39 RID: 23609 RVA: 0x0013BBB2 File Offset: 0x00139DB2
		[ComVisible(false)]
		public virtual int GetLeapMonth(int year)
		{
			return this.GetLeapMonth(year, 0);
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x0013BBBC File Offset: 0x00139DBC
		[ComVisible(false)]
		public virtual int GetLeapMonth(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 0;
			}
			int monthsInYear = this.GetMonthsInYear(year, era);
			for (int i = 1; i <= monthsInYear; i++)
			{
				if (this.IsLeapMonth(year, i, era))
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x0013BBF8 File Offset: 0x00139DF8
		public virtual bool IsLeapYear(int year)
		{
			return this.IsLeapYear(year, 0);
		}

		// Token: 0x06005C3C RID: 23612
		public abstract bool IsLeapYear(int year, int era);

		// Token: 0x06005C3D RID: 23613 RVA: 0x0013BC04 File Offset: 0x00139E04
		public virtual DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			return this.ToDateTime(year, month, day, hour, minute, second, millisecond, 0);
		}

		// Token: 0x06005C3E RID: 23614
		public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

		// Token: 0x06005C3F RID: 23615 RVA: 0x0013BC24 File Offset: 0x00139E24
		internal virtual bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
		{
			result = DateTime.MinValue;
			bool flag;
			try
			{
				result = this.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
				flag = true;
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x0013BC74 File Offset: 0x00139E74
		internal virtual bool IsValidYear(int year, int era)
		{
			return year >= this.GetYear(this.MinSupportedDateTime) && year <= this.GetYear(this.MaxSupportedDateTime);
		}

		// Token: 0x06005C41 RID: 23617 RVA: 0x0013BC99 File Offset: 0x00139E99
		internal virtual bool IsValidMonth(int year, int month, int era)
		{
			return this.IsValidYear(year, era) && month >= 1 && month <= this.GetMonthsInYear(year, era);
		}

		// Token: 0x06005C42 RID: 23618 RVA: 0x0013BCB9 File Offset: 0x00139EB9
		internal virtual bool IsValidDay(int year, int month, int day, int era)
		{
			return this.IsValidMonth(year, month, era) && day >= 1 && day <= this.GetDaysInMonth(year, month, era);
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x0013BCDD File Offset: 0x00139EDD
		// (set) Token: 0x06005C44 RID: 23620 RVA: 0x0013BCE5 File Offset: 0x00139EE5
		public virtual int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				this.VerifyWritable();
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x0013BCF4 File Offset: 0x00139EF4
		public virtual int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year < 100)
			{
				return (this.TwoDigitYearMax / 100 - ((year > this.TwoDigitYearMax % 100) ? 1 : 0)) * 100 + year;
			}
			return year;
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x0013BD40 File Offset: 0x00139F40
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x0013BDC8 File Offset: 0x00139FC8
		[SecuritySafeCritical]
		internal static int GetSystemTwoDigitYearSetting(int CalID, int defaultYearValue)
		{
			int num = CalendarData.nativeGetTwoDigitYearMax(CalID);
			if (num < 0)
			{
				num = defaultYearValue;
			}
			return num;
		}

		// Token: 0x040037B4 RID: 14260
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x040037B5 RID: 14261
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x040037B6 RID: 14262
		internal const long TicksPerMinute = 600000000L;

		// Token: 0x040037B7 RID: 14263
		internal const long TicksPerHour = 36000000000L;

		// Token: 0x040037B8 RID: 14264
		internal const long TicksPerDay = 864000000000L;

		// Token: 0x040037B9 RID: 14265
		internal const int MillisPerSecond = 1000;

		// Token: 0x040037BA RID: 14266
		internal const int MillisPerMinute = 60000;

		// Token: 0x040037BB RID: 14267
		internal const int MillisPerHour = 3600000;

		// Token: 0x040037BC RID: 14268
		internal const int MillisPerDay = 86400000;

		// Token: 0x040037BD RID: 14269
		internal const int DaysPerYear = 365;

		// Token: 0x040037BE RID: 14270
		internal const int DaysPer4Years = 1461;

		// Token: 0x040037BF RID: 14271
		internal const int DaysPer100Years = 36524;

		// Token: 0x040037C0 RID: 14272
		internal const int DaysPer400Years = 146097;

		// Token: 0x040037C1 RID: 14273
		internal const int DaysTo10000 = 3652059;

		// Token: 0x040037C2 RID: 14274
		internal const long MaxMillis = 315537897600000L;

		// Token: 0x040037C3 RID: 14275
		internal const int CAL_GREGORIAN = 1;

		// Token: 0x040037C4 RID: 14276
		internal const int CAL_GREGORIAN_US = 2;

		// Token: 0x040037C5 RID: 14277
		internal const int CAL_JAPAN = 3;

		// Token: 0x040037C6 RID: 14278
		internal const int CAL_TAIWAN = 4;

		// Token: 0x040037C7 RID: 14279
		internal const int CAL_KOREA = 5;

		// Token: 0x040037C8 RID: 14280
		internal const int CAL_HIJRI = 6;

		// Token: 0x040037C9 RID: 14281
		internal const int CAL_THAI = 7;

		// Token: 0x040037CA RID: 14282
		internal const int CAL_HEBREW = 8;

		// Token: 0x040037CB RID: 14283
		internal const int CAL_GREGORIAN_ME_FRENCH = 9;

		// Token: 0x040037CC RID: 14284
		internal const int CAL_GREGORIAN_ARABIC = 10;

		// Token: 0x040037CD RID: 14285
		internal const int CAL_GREGORIAN_XLIT_ENGLISH = 11;

		// Token: 0x040037CE RID: 14286
		internal const int CAL_GREGORIAN_XLIT_FRENCH = 12;

		// Token: 0x040037CF RID: 14287
		internal const int CAL_JULIAN = 13;

		// Token: 0x040037D0 RID: 14288
		internal const int CAL_JAPANESELUNISOLAR = 14;

		// Token: 0x040037D1 RID: 14289
		internal const int CAL_CHINESELUNISOLAR = 15;

		// Token: 0x040037D2 RID: 14290
		internal const int CAL_SAKA = 16;

		// Token: 0x040037D3 RID: 14291
		internal const int CAL_LUNAR_ETO_CHN = 17;

		// Token: 0x040037D4 RID: 14292
		internal const int CAL_LUNAR_ETO_KOR = 18;

		// Token: 0x040037D5 RID: 14293
		internal const int CAL_LUNAR_ETO_ROKUYOU = 19;

		// Token: 0x040037D6 RID: 14294
		internal const int CAL_KOREANLUNISOLAR = 20;

		// Token: 0x040037D7 RID: 14295
		internal const int CAL_TAIWANLUNISOLAR = 21;

		// Token: 0x040037D8 RID: 14296
		internal const int CAL_PERSIAN = 22;

		// Token: 0x040037D9 RID: 14297
		internal const int CAL_UMALQURA = 23;

		// Token: 0x040037DA RID: 14298
		internal int m_currentEraValue = -1;

		// Token: 0x040037DB RID: 14299
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x040037DC RID: 14300
		public const int CurrentEra = 0;

		// Token: 0x040037DD RID: 14301
		internal int twoDigitYearMax = -1;
	}
}
