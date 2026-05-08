using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009E8 RID: 2536
	[ComVisible(true)]
	[Serializable]
	public class JapaneseCalendar : Calendar
	{
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06005D23 RID: 23843 RVA: 0x0013FB66 File Offset: 0x0013DD66
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseCalendar.calendarMinValue;
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06005D24 RID: 23844 RVA: 0x0013B781 File Offset: 0x00139981
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06005D25 RID: 23845 RVA: 0x00003FB7 File Offset: 0x000021B7
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x0013FB70 File Offset: 0x0013DD70
		internal static EraInfo[] GetEraInfo()
		{
			if (JapaneseCalendar.japaneseEraInfo == null)
			{
				JapaneseCalendar.japaneseEraInfo = JapaneseCalendar.GetErasFromRegistry();
				if (JapaneseCalendar.japaneseEraInfo == null)
				{
					JapaneseCalendar.japaneseEraInfo = new EraInfo[]
					{
						new EraInfo(5, 2019, 5, 1, 2018, 1, 7981, "令和", "令", "R"),
						new EraInfo(4, 1989, 1, 8, 1988, 1, 31, "平成", "平", "H"),
						new EraInfo(3, 1926, 12, 25, 1925, 1, 64, "昭和", "昭", "S"),
						new EraInfo(2, 1912, 7, 30, 1911, 1, 15, "大正", "大", "T"),
						new EraInfo(1, 1868, 1, 1, 1867, 1, 45, "明治", "明", "M")
					};
				}
			}
			return JapaneseCalendar.japaneseEraInfo;
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		[SecuritySafeCritical]
		private static EraInfo[] GetErasFromRegistry()
		{
			return null;
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x0013FC7E File Offset: 0x0013DE7E
		private static int CompareEraRanges(EraInfo a, EraInfo b)
		{
			return b.ticks.CompareTo(a.ticks);
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x0013FC94 File Offset: 0x0013DE94
		private static EraInfo GetEraFromValue(string value, string data)
		{
			if (value == null || data == null)
			{
				return null;
			}
			if (value.Length != 10)
			{
				return null;
			}
			int num;
			int num2;
			int num3;
			if (!Number.TryParseInt32(value.Substring(0, 4), NumberStyles.None, NumberFormatInfo.InvariantInfo, out num) || !Number.TryParseInt32(value.Substring(5, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out num2) || !Number.TryParseInt32(value.Substring(8, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out num3))
			{
				return null;
			}
			string[] array = data.Split(new char[] { '_' });
			if (array.Length != 4)
			{
				return null;
			}
			if (array[0].Length == 0 || array[1].Length == 0 || array[2].Length == 0 || array[3].Length == 0)
			{
				return null;
			}
			return new EraInfo(0, num, num2, num3, num - 1, 1, 0, array[0], array[1], array[3]);
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x0013FD66 File Offset: 0x0013DF66
		internal static Calendar GetDefaultInstance()
		{
			if (JapaneseCalendar.s_defaultInstance == null)
			{
				JapaneseCalendar.s_defaultInstance = new JapaneseCalendar();
			}
			return JapaneseCalendar.s_defaultInstance;
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x0013FD84 File Offset: 0x0013DF84
		public JapaneseCalendar()
		{
			try
			{
				new CultureInfo("ja-JP");
			}
			catch (ArgumentException ex)
			{
				throw new TypeInitializationException(base.GetType().FullName, ex);
			}
			this.helper = new GregorianCalendarHelper(this, JapaneseCalendar.GetEraInfo());
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06005D2C RID: 23852 RVA: 0x00019B62 File Offset: 0x00017D62
		internal override int ID
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x0013FDD8 File Offset: 0x0013DFD8
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x0013FDE7 File Offset: 0x0013DFE7
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x0013FDF6 File Offset: 0x0013DFF6
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x0013FE06 File Offset: 0x0013E006
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x0013FE15 File Offset: 0x0013E015
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x0013FE23 File Offset: 0x0013E023
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x0013FE31 File Offset: 0x0013E031
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x0013FE3F File Offset: 0x0013E03F
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x0013FE4E File Offset: 0x0013E04E
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x0013FE5E File Offset: 0x0013E05E
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x0013FE6C File Offset: 0x0013E06C
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x0013FE7A File Offset: 0x0013E07A
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x0013FE88 File Offset: 0x0013E088
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x0013FE9A File Offset: 0x0013E09A
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x0013FEA9 File Offset: 0x0013E0A9
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x0013FEB8 File Offset: 0x0013E0B8
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x0013FEC8 File Offset: 0x0013E0C8
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x0013FEF0 File Offset: 0x0013E0F0
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

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06005D3F RID: 23871 RVA: 0x0013FF5A File Offset: 0x0013E15A
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x0013FF68 File Offset: 0x0013E168
		internal static string[] EraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].eraName;
			}
			return array;
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x0013FFA4 File Offset: 0x0013E1A4
		internal static string[] AbbrevEraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].abbrevEraName;
			}
			return array;
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x0013FFE0 File Offset: 0x0013E1E0
		internal static string[] EnglishEraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].englishEraName;
			}
			return array;
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x0014001C File Offset: 0x0013E21C
		internal override bool IsValidYear(int year, int era)
		{
			return this.helper.IsValidYear(year, era);
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06005D44 RID: 23876 RVA: 0x0014002B File Offset: 0x0013E22B
		// (set) Token: 0x06005D45 RID: 23877 RVA: 0x00140050 File Offset: 0x0013E250
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

		// Token: 0x06005D46 RID: 23878 RVA: 0x001400B3 File Offset: 0x0013E2B3
		// Note: this type is marked as 'beforefieldinit'.
		static JapaneseCalendar()
		{
		}

		// Token: 0x04003868 RID: 14440
		internal static readonly DateTime calendarMinValue = new DateTime(1868, 9, 8);

		// Token: 0x04003869 RID: 14441
		internal static volatile EraInfo[] japaneseEraInfo;

		// Token: 0x0400386A RID: 14442
		private const string c_japaneseErasHive = "System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras";

		// Token: 0x0400386B RID: 14443
		private const string c_japaneseErasHivePermissionList = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras";

		// Token: 0x0400386C RID: 14444
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x0400386D RID: 14445
		internal GregorianCalendarHelper helper;

		// Token: 0x0400386E RID: 14446
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 99;
	}
}
