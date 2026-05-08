using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020009EF RID: 2543
	[ComVisible(true)]
	[Serializable]
	public class TaiwanCalendar : Calendar
	{
		// Token: 0x06005E01 RID: 24065 RVA: 0x00141BE7 File Offset: 0x0013FDE7
		internal static Calendar GetDefaultInstance()
		{
			if (TaiwanCalendar.s_defaultInstance == null)
			{
				TaiwanCalendar.s_defaultInstance = new TaiwanCalendar();
			}
			return TaiwanCalendar.s_defaultInstance;
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06005E02 RID: 24066 RVA: 0x00141C05 File Offset: 0x0013FE05
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanCalendar.calendarMinValue;
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005E03 RID: 24067 RVA: 0x0013B781 File Offset: 0x00139981
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06005E04 RID: 24068 RVA: 0x00003FB7 File Offset: 0x000021B7
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x00141C0C File Offset: 0x0013FE0C
		public TaiwanCalendar()
		{
			try
			{
				new CultureInfo("zh-TW");
			}
			catch (ArgumentException ex)
			{
				throw new TypeInitializationException(base.GetType().FullName, ex);
			}
			this.helper = new GregorianCalendarHelper(this, TaiwanCalendar.taiwanEraInfo);
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06005E06 RID: 24070 RVA: 0x0001A197 File Offset: 0x00018397
		internal override int ID
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06005E07 RID: 24071 RVA: 0x00141C60 File Offset: 0x0013FE60
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x00141C6F File Offset: 0x0013FE6F
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x06005E09 RID: 24073 RVA: 0x00141C7E File Offset: 0x0013FE7E
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x00141C8E File Offset: 0x0013FE8E
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x00141C9D File Offset: 0x0013FE9D
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x06005E0C RID: 24076 RVA: 0x00141CAB File Offset: 0x0013FEAB
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x06005E0D RID: 24077 RVA: 0x00141CB9 File Offset: 0x0013FEB9
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x06005E0E RID: 24078 RVA: 0x00141CC7 File Offset: 0x0013FEC7
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x06005E0F RID: 24079 RVA: 0x00141CD6 File Offset: 0x0013FED6
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06005E10 RID: 24080 RVA: 0x00141CE6 File Offset: 0x0013FEE6
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x06005E11 RID: 24081 RVA: 0x00141CF4 File Offset: 0x0013FEF4
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x06005E12 RID: 24082 RVA: 0x00141D02 File Offset: 0x0013FF02
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x06005E13 RID: 24083 RVA: 0x00141D10 File Offset: 0x0013FF10
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x06005E14 RID: 24084 RVA: 0x00141D22 File Offset: 0x0013FF22
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x06005E15 RID: 24085 RVA: 0x00141D31 File Offset: 0x0013FF31
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x00141D40 File Offset: 0x0013FF40
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x00141D50 File Offset: 0x0013FF50
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06005E18 RID: 24088 RVA: 0x00141D75 File Offset: 0x0013FF75
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06005E19 RID: 24089 RVA: 0x0014002B File Offset: 0x0013E22B
		// (set) Token: 0x06005E1A RID: 24090 RVA: 0x00141D84 File Offset: 0x0013FF84
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 99);
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

		// Token: 0x06005E1B RID: 24091 RVA: 0x00141DE8 File Offset: 0x0013FFE8
		public override int ToFourDigitYear(int year)
		{
			if (year <= 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Positive number required."));
			}
			if (year > this.helper.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, this.helper.MaxYear));
			}
			return year;
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x00141E54 File Offset: 0x00140054
		// Note: this type is marked as 'beforefieldinit'.
		static TaiwanCalendar()
		{
		}

		// Token: 0x040038BC RID: 14524
		internal static EraInfo[] taiwanEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1912, 1, 1, 1911, 1, 8088)
		};

		// Token: 0x040038BD RID: 14525
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x040038BE RID: 14526
		internal GregorianCalendarHelper helper;

		// Token: 0x040038BF RID: 14527
		internal static readonly DateTime calendarMinValue = new DateTime(1912, 1, 1);

		// Token: 0x040038C0 RID: 14528
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 99;
	}
}
