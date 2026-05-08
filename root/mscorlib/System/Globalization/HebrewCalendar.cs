using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020009E5 RID: 2533
	[ComVisible(true)]
	[Serializable]
	public class HebrewCalendar : Calendar
	{
		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06005CD8 RID: 23768 RVA: 0x0013E9D6 File Offset: 0x0013CBD6
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HebrewCalendar.calendarMinValue;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06005CD9 RID: 23769 RVA: 0x0013E9DD File Offset: 0x0013CBDD
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HebrewCalendar.calendarMaxValue;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06005CDA RID: 23770 RVA: 0x00019B62 File Offset: 0x00017D62
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x0013B064 File Offset: 0x00139264
		public HebrewCalendar()
		{
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06005CDC RID: 23772 RVA: 0x00048AA1 File Offset: 0x00046CA1
		internal override int ID
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x0013E9E4 File Offset: 0x0013CBE4
		private static void CheckHebrewYearValue(int y, int era, string varName)
		{
			HebrewCalendar.CheckEraRange(era);
			if (y > 5999 || y < 5343)
			{
				throw new ArgumentOutOfRangeException(varName, string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 5343, 5999));
			}
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x0013EA38 File Offset: 0x0013CC38
		private void CheckHebrewMonthValue(int year, int month, int era)
		{
			int monthsInYear = this.GetMonthsInYear(year, era);
			if (month < 1 || month > monthsInYear)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, monthsInYear));
			}
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x0013EA84 File Offset: 0x0013CC84
		private void CheckHebrewDayValue(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, daysInMonth));
			}
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x0013EACF File Offset: 0x0013CCCF
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != HebrewCalendar.HebrewEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("Era value was not valid."));
			}
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x0013EAF4 File Offset: 0x0013CCF4
		private static void CheckTicksRange(long ticks)
		{
			if (ticks < HebrewCalendar.calendarMinValue.Ticks || ticks > HebrewCalendar.calendarMaxValue.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), HebrewCalendar.calendarMinValue, HebrewCalendar.calendarMaxValue));
			}
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x0013EB4E File Offset: 0x0013CD4E
		internal static int GetResult(HebrewCalendar.__DateBuffer result, int part)
		{
			switch (part)
			{
			case 0:
				return result.year;
			case 2:
				return result.month;
			case 3:
				return result.day;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Internal Error in DateTime and Calendar operations."));
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x0013EB8C File Offset: 0x0013CD8C
		internal static int GetLunarMonthDay(int gregorianYear, HebrewCalendar.__DateBuffer lunarDate)
		{
			int num = gregorianYear - 1583;
			if (num < 0 || num > 656)
			{
				throw new ArgumentOutOfRangeException("gregorianYear");
			}
			num *= 2;
			lunarDate.day = HebrewCalendar.HebrewTable[num];
			int num2 = HebrewCalendar.HebrewTable[num + 1];
			int day = lunarDate.day;
			if (day != 0)
			{
				switch (day)
				{
				case 30:
					lunarDate.month = 3;
					break;
				case 31:
					lunarDate.month = 5;
					lunarDate.day = 2;
					break;
				case 32:
					lunarDate.month = 5;
					lunarDate.day = 3;
					break;
				case 33:
					lunarDate.month = 3;
					lunarDate.day = 29;
					break;
				default:
					lunarDate.month = 4;
					break;
				}
			}
			else
			{
				lunarDate.month = 5;
				lunarDate.day = 1;
			}
			return num2;
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x0013EC4C File Offset: 0x0013CE4C
		internal virtual int GetDatePart(long ticks, int part)
		{
			HebrewCalendar.CheckTicksRange(ticks);
			DateTime dateTime = new DateTime(ticks);
			int year = dateTime.Year;
			int month = dateTime.Month;
			int day = dateTime.Day;
			HebrewCalendar.__DateBuffer _DateBuffer = new HebrewCalendar.__DateBuffer();
			_DateBuffer.year = year + 3760;
			int num = HebrewCalendar.GetLunarMonthDay(year, _DateBuffer);
			HebrewCalendar.__DateBuffer _DateBuffer2 = new HebrewCalendar.__DateBuffer();
			_DateBuffer2.year = _DateBuffer.year;
			_DateBuffer2.month = _DateBuffer.month;
			_DateBuffer2.day = _DateBuffer.day;
			long absoluteDate = GregorianCalendar.GetAbsoluteDate(year, month, day);
			if (month == 1 && day == 1)
			{
				return HebrewCalendar.GetResult(_DateBuffer2, part);
			}
			long num2 = absoluteDate - GregorianCalendar.GetAbsoluteDate(year, 1, 1);
			if (num2 + (long)_DateBuffer.day <= (long)HebrewCalendar.LunarMonthLen[num, _DateBuffer.month])
			{
				_DateBuffer2.day += (int)num2;
				return HebrewCalendar.GetResult(_DateBuffer2, part);
			}
			_DateBuffer2.month++;
			_DateBuffer2.day = 1;
			num2 -= (long)(HebrewCalendar.LunarMonthLen[num, _DateBuffer.month] - _DateBuffer.day);
			if (num2 > 1L)
			{
				while (num2 > (long)HebrewCalendar.LunarMonthLen[num, _DateBuffer2.month])
				{
					long num3 = num2;
					int[,] lunarMonthLen = HebrewCalendar.LunarMonthLen;
					int num4 = num;
					HebrewCalendar.__DateBuffer _DateBuffer3 = _DateBuffer2;
					int month2 = _DateBuffer3.month;
					_DateBuffer3.month = month2 + 1;
					num2 = num3 - (long)lunarMonthLen[num4, month2];
					if (_DateBuffer2.month > 13 || HebrewCalendar.LunarMonthLen[num, _DateBuffer2.month] == 0)
					{
						_DateBuffer2.year++;
						num = HebrewCalendar.HebrewTable[(year + 1 - 1583) * 2 + 1];
						_DateBuffer2.month = 1;
					}
				}
				_DateBuffer2.day += (int)(num2 - 1L);
			}
			return HebrewCalendar.GetResult(_DateBuffer2, part);
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x0013EE18 File Offset: 0x0013D018
		public override DateTime AddMonths(DateTime time, int months)
		{
			DateTime dateTime;
			try
			{
				int num = this.GetDatePart(time.Ticks, 0);
				int datePart = this.GetDatePart(time.Ticks, 2);
				int num2 = this.GetDatePart(time.Ticks, 3);
				int i;
				if (months >= 0)
				{
					int num3;
					for (i = datePart + months; i > (num3 = this.GetMonthsInYear(num, 0)); i -= num3)
					{
						num++;
					}
				}
				else if ((i = datePart + months) <= 0)
				{
					months = -months;
					months -= datePart;
					num--;
					int num3;
					while (months > (num3 = this.GetMonthsInYear(num, 0)))
					{
						num--;
						months -= num3;
					}
					num3 = this.GetMonthsInYear(num, 0);
					i = num3 - months;
				}
				int daysInMonth = this.GetDaysInMonth(num, i);
				if (num2 > daysInMonth)
				{
					num2 = daysInMonth;
				}
				dateTime = this.ToDateTime(num, i, num2, 0, 0, 0, 0);
				dateTime = new DateTime(dateTime.Ticks + time.Ticks % 864000000000L);
			}
			catch (ArgumentException)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Value to add was out of range."), Array.Empty<object>()));
			}
			return dateTime;
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x0013EF30 File Offset: 0x0013D130
		public override DateTime AddYears(DateTime time, int years)
		{
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			num += years;
			HebrewCalendar.CheckHebrewYearValue(num, 0, "years");
			int monthsInYear = this.GetMonthsInYear(num, 0);
			if (num2 > monthsInYear)
			{
				num2 = monthsInYear;
			}
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long num4 = this.ToDateTime(num, num2, num3, 0, 0, 0, 0).Ticks + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num4, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num4);
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x0013EFDA File Offset: 0x0013D1DA
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x0013B3F2 File Offset: 0x001395F2
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x0013EFEA File Offset: 0x0013D1EA
		internal static int GetHebrewYearType(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return HebrewCalendar.HebrewTable[(year - 3760 - 1583) * 2 + 1];
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x0013F010 File Offset: 0x0013D210
		public override int GetDayOfYear(DateTime time)
		{
			int year = this.GetYear(time);
			DateTime dateTime;
			if (year == 5343)
			{
				dateTime = new DateTime(1582, 9, 27);
			}
			else
			{
				dateTime = this.ToDateTime(year, 1, 1, 0, 0, 0, 0, 0);
			}
			return (int)((time.Ticks - dateTime.Ticks) / 864000000000L) + 1;
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x0013F06C File Offset: 0x0013D26C
		public override int GetDaysInMonth(int year, int month, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			int num = HebrewCalendar.LunarMonthLen[hebrewYearType, month];
			if (num == 0)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			return num;
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x0013F0B4 File Offset: 0x0013D2B4
		public override int GetDaysInYear(int year, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			if (hebrewYearType < 4)
			{
				return 352 + hebrewYearType;
			}
			return 382 + (hebrewYearType - 3);
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x0013F0E4 File Offset: 0x0013D2E4
		public override int GetEra(DateTime time)
		{
			return HebrewCalendar.HebrewEra;
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06005CEE RID: 23790 RVA: 0x0013F0EB File Offset: 0x0013D2EB
		public override int[] Eras
		{
			get
			{
				return new int[] { HebrewCalendar.HebrewEra };
			}
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x0013F0FB File Offset: 0x0013D2FB
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x0013F10B File Offset: 0x0013D30B
		public override int GetMonthsInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 12;
			}
			return 13;
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x0013F11C File Offset: 0x0013D31C
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x0013F12C File Offset: 0x0013D32C
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			if (this.IsLeapMonth(year, month, era))
			{
				this.CheckHebrewDayValue(year, month, day, era);
				return true;
			}
			if (this.IsLeapYear(year, 0) && month == 6 && day == 30)
			{
				return true;
			}
			this.CheckHebrewDayValue(year, month, day, era);
			return false;
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x0013F168 File Offset: 0x0013D368
		public override int GetLeapMonth(int year, int era)
		{
			if (this.IsLeapYear(year, era))
			{
				return 7;
			}
			return 0;
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x0013F177 File Offset: 0x0013D377
		public override bool IsLeapMonth(int year, int month, int era)
		{
			bool flag = this.IsLeapYear(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			return flag && month == 7;
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x0013F193 File Offset: 0x0013D393
		public override bool IsLeapYear(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return (7L * (long)year + 1L) % 19L < 7L;
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x0013F1B4 File Offset: 0x0013D3B4
		private static int GetDayDifference(int lunarYearType, int month1, int day1, int month2, int day2)
		{
			if (month1 == month2)
			{
				return day1 - day2;
			}
			bool flag = month1 > month2;
			if (flag)
			{
				int num = month1;
				int num2 = day1;
				month1 = month2;
				day1 = day2;
				month2 = num;
				day2 = num2;
			}
			int num3 = HebrewCalendar.LunarMonthLen[lunarYearType, month1] - day1;
			month1++;
			while (month1 < month2)
			{
				num3 += HebrewCalendar.LunarMonthLen[lunarYearType, month1++];
			}
			num3 += day2;
			if (!flag)
			{
				return -num3;
			}
			return num3;
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x0013F21C File Offset: 0x0013D41C
		private static DateTime HebrewToGregorian(int hebrewYear, int hebrewMonth, int hebrewDay, int hour, int minute, int second, int millisecond)
		{
			int num = hebrewYear - 3760;
			HebrewCalendar.__DateBuffer _DateBuffer = new HebrewCalendar.__DateBuffer();
			int lunarMonthDay = HebrewCalendar.GetLunarMonthDay(num, _DateBuffer);
			if (hebrewMonth == _DateBuffer.month && hebrewDay == _DateBuffer.day)
			{
				return new DateTime(num, 1, 1, hour, minute, second, millisecond);
			}
			int dayDifference = HebrewCalendar.GetDayDifference(lunarMonthDay, hebrewMonth, hebrewDay, _DateBuffer.month, _DateBuffer.day);
			DateTime dateTime = new DateTime(num, 1, 1);
			return new DateTime(dateTime.Ticks + (long)dayDifference * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x0013F2A8 File Offset: 0x0013D4A8
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			this.CheckHebrewMonthValue(year, month, era);
			this.CheckHebrewDayValue(year, month, day, era);
			DateTime dateTime = HebrewCalendar.HebrewToGregorian(year, month, day, hour, minute, second, millisecond);
			HebrewCalendar.CheckTicksRange(dateTime.Ticks);
			return dateTime;
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06005CF9 RID: 23801 RVA: 0x0013F2F5 File Offset: 0x0013D4F5
		// (set) Token: 0x06005CFA RID: 23802 RVA: 0x0013F31C File Offset: 0x0013D51C
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 5790);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value != 99)
				{
					HebrewCalendar.CheckHebrewYearValue(value, HebrewCalendar.HebrewEra, "value");
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x0013F340 File Offset: 0x0013D540
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
			if (year > 5999 || year < 5343)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 5343, 5999));
			}
			return year;
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x0013F3B8 File Offset: 0x0013D5B8
		// Note: this type is marked as 'beforefieldinit'.
		static HebrewCalendar()
		{
		}

		// Token: 0x04003844 RID: 14404
		public static readonly int HebrewEra = 1;

		// Token: 0x04003845 RID: 14405
		internal const int DatePartYear = 0;

		// Token: 0x04003846 RID: 14406
		internal const int DatePartDayOfYear = 1;

		// Token: 0x04003847 RID: 14407
		internal const int DatePartMonth = 2;

		// Token: 0x04003848 RID: 14408
		internal const int DatePartDay = 3;

		// Token: 0x04003849 RID: 14409
		internal const int DatePartDayOfWeek = 4;

		// Token: 0x0400384A RID: 14410
		private const int HebrewYearOf1AD = 3760;

		// Token: 0x0400384B RID: 14411
		private const int FirstGregorianTableYear = 1583;

		// Token: 0x0400384C RID: 14412
		private const int LastGregorianTableYear = 2239;

		// Token: 0x0400384D RID: 14413
		private const int TABLESIZE = 656;

		// Token: 0x0400384E RID: 14414
		private const int MinHebrewYear = 5343;

		// Token: 0x0400384F RID: 14415
		private const int MaxHebrewYear = 5999;

		// Token: 0x04003850 RID: 14416
		private static readonly int[] HebrewTable = new int[]
		{
			7, 3, 17, 3, 0, 4, 11, 2, 21, 6,
			1, 3, 13, 2, 25, 4, 5, 3, 16, 2,
			27, 6, 9, 1, 20, 2, 0, 6, 11, 3,
			23, 4, 4, 2, 14, 3, 27, 4, 8, 2,
			18, 3, 28, 6, 11, 1, 22, 5, 2, 3,
			12, 3, 25, 4, 6, 2, 16, 3, 26, 6,
			8, 2, 20, 1, 0, 6, 11, 2, 24, 4,
			4, 3, 15, 2, 25, 6, 8, 1, 19, 2,
			29, 6, 9, 3, 22, 4, 3, 2, 13, 3,
			25, 4, 6, 3, 17, 2, 27, 6, 7, 3,
			19, 2, 31, 4, 11, 3, 23, 4, 5, 2,
			15, 3, 25, 6, 6, 2, 19, 1, 29, 6,
			10, 2, 22, 4, 3, 3, 14, 2, 24, 6,
			6, 1, 17, 3, 28, 5, 8, 3, 20, 1,
			32, 5, 12, 3, 22, 6, 4, 1, 16, 2,
			26, 6, 6, 3, 17, 2, 0, 4, 10, 3,
			22, 4, 3, 2, 14, 3, 24, 6, 5, 2,
			17, 1, 28, 6, 9, 2, 19, 3, 31, 4,
			13, 2, 23, 6, 3, 3, 15, 1, 27, 5,
			7, 3, 17, 3, 29, 4, 11, 2, 21, 6,
			3, 1, 14, 2, 25, 6, 5, 3, 16, 2,
			28, 4, 9, 3, 20, 2, 0, 6, 12, 1,
			23, 6, 4, 2, 14, 3, 26, 4, 8, 2,
			18, 3, 0, 4, 10, 3, 21, 5, 1, 3,
			13, 1, 24, 5, 5, 3, 15, 3, 27, 4,
			8, 2, 19, 3, 29, 6, 10, 2, 22, 4,
			3, 3, 14, 2, 26, 4, 6, 3, 18, 2,
			28, 6, 10, 1, 20, 6, 2, 2, 12, 3,
			24, 4, 5, 2, 16, 3, 28, 4, 8, 3,
			19, 2, 0, 6, 12, 1, 23, 5, 3, 3,
			14, 3, 26, 4, 7, 2, 17, 3, 28, 6,
			9, 2, 21, 4, 1, 3, 13, 2, 25, 4,
			5, 3, 16, 2, 27, 6, 9, 1, 19, 3,
			0, 5, 11, 3, 23, 4, 4, 2, 14, 3,
			25, 6, 7, 1, 18, 2, 28, 6, 9, 3,
			21, 4, 2, 2, 12, 3, 25, 4, 6, 2,
			16, 3, 26, 6, 8, 2, 20, 1, 0, 6,
			11, 2, 22, 6, 4, 1, 15, 2, 25, 6,
			6, 3, 18, 1, 29, 5, 9, 3, 22, 4,
			2, 3, 13, 2, 23, 6, 4, 3, 15, 2,
			27, 4, 7, 3, 19, 2, 31, 4, 11, 3,
			21, 6, 3, 2, 15, 1, 25, 6, 6, 2,
			17, 3, 29, 4, 10, 2, 20, 6, 3, 1,
			13, 3, 24, 5, 4, 3, 16, 1, 27, 5,
			7, 3, 17, 3, 0, 4, 11, 2, 21, 6,
			1, 3, 13, 2, 25, 4, 5, 3, 16, 2,
			29, 4, 9, 3, 19, 6, 30, 2, 13, 1,
			23, 6, 4, 2, 14, 3, 27, 4, 8, 2,
			18, 3, 0, 4, 11, 3, 22, 5, 2, 3,
			14, 1, 26, 5, 6, 3, 16, 3, 28, 4,
			10, 2, 20, 6, 30, 3, 11, 2, 24, 4,
			4, 3, 15, 2, 25, 6, 8, 1, 19, 2,
			29, 6, 9, 3, 22, 4, 3, 2, 13, 3,
			25, 4, 7, 2, 17, 3, 27, 6, 9, 1,
			21, 5, 1, 3, 11, 3, 23, 4, 5, 2,
			15, 3, 25, 6, 6, 2, 19, 1, 29, 6,
			10, 2, 22, 4, 3, 3, 14, 2, 24, 6,
			6, 1, 18, 2, 28, 6, 8, 3, 20, 4,
			2, 2, 12, 3, 24, 4, 4, 3, 16, 2,
			26, 6, 6, 3, 17, 2, 0, 4, 10, 3,
			22, 4, 3, 2, 14, 3, 24, 6, 5, 2,
			17, 1, 28, 6, 9, 2, 21, 4, 1, 3,
			13, 2, 23, 6, 5, 1, 15, 3, 27, 5,
			7, 3, 19, 1, 0, 5, 10, 3, 22, 4,
			2, 3, 13, 2, 24, 6, 4, 3, 15, 2,
			27, 4, 8, 3, 20, 4, 1, 2, 11, 3,
			22, 6, 3, 2, 15, 1, 25, 6, 7, 2,
			17, 3, 29, 4, 10, 2, 21, 6, 1, 3,
			13, 1, 24, 5, 5, 3, 15, 3, 27, 4,
			8, 2, 19, 6, 1, 1, 12, 2, 22, 6,
			3, 3, 14, 2, 26, 4, 6, 3, 18, 2,
			28, 6, 10, 1, 20, 6, 2, 2, 12, 3,
			24, 4, 5, 2, 16, 3, 28, 4, 9, 2,
			19, 6, 30, 3, 12, 1, 23, 5, 3, 3,
			14, 3, 26, 4, 7, 2, 17, 3, 28, 6,
			9, 2, 21, 4, 1, 3, 13, 2, 25, 4,
			5, 3, 16, 2, 27, 6, 9, 1, 19, 6,
			30, 2, 11, 3, 23, 4, 4, 2, 14, 3,
			27, 4, 7, 3, 18, 2, 28, 6, 11, 1,
			22, 5, 2, 3, 12, 3, 25, 4, 6, 2,
			16, 3, 26, 6, 8, 2, 20, 4, 30, 3,
			11, 2, 24, 4, 4, 3, 15, 2, 25, 6,
			8, 1, 18, 3, 29, 5, 9, 3, 22, 4,
			3, 2, 13, 3, 23, 6, 6, 1, 17, 2,
			27, 6, 7, 3, 20, 4, 1, 2, 11, 3,
			23, 4, 5, 2, 15, 3, 25, 6, 6, 2,
			19, 1, 29, 6, 10, 2, 20, 6, 3, 1,
			14, 2, 24, 6, 4, 3, 17, 1, 28, 5,
			8, 3, 20, 4, 1, 3, 12, 2, 22, 6,
			2, 3, 14, 2, 26, 4, 6, 3, 17, 2,
			0, 4, 10, 3, 20, 6, 1, 2, 14, 1,
			24, 6, 5, 2, 15, 3, 28, 4, 9, 2,
			19, 6, 1, 1, 12, 3, 23, 5, 3, 3,
			15, 1, 27, 5, 7, 3, 17, 3, 29, 4,
			11, 2, 21, 6, 1, 3, 12, 2, 25, 4,
			5, 3, 16, 2, 28, 4, 9, 3, 19, 6,
			30, 2, 12, 1, 23, 6, 4, 2, 14, 3,
			26, 4, 8, 2, 18, 3, 0, 4, 10, 3,
			22, 5, 2, 3, 14, 1, 25, 5, 6, 3,
			16, 3, 28, 4, 9, 2, 20, 6, 30, 3,
			11, 2, 23, 4, 4, 3, 15, 2, 27, 4,
			7, 3, 19, 2, 29, 6, 11, 1, 21, 6,
			3, 2, 13, 3, 25, 4, 6, 2, 17, 3,
			27, 6, 9, 1, 20, 5, 30, 3, 10, 3,
			22, 4, 3, 2, 14, 3, 24, 6, 5, 2,
			17, 1, 28, 6, 9, 2, 21, 4, 1, 3,
			13, 2, 23, 6, 5, 1, 16, 2, 27, 6,
			7, 3, 19, 4, 30, 2, 11, 3, 23, 4,
			3, 3, 14, 2, 25, 6, 5, 3, 16, 2,
			28, 4, 9, 3, 21, 4, 2, 2, 12, 3,
			23, 6, 4, 2, 16, 1, 26, 6, 8, 2,
			20, 4, 30, 3, 11, 2, 22, 6, 4, 1,
			14, 3, 25, 5, 6, 3, 18, 1, 29, 5,
			9, 3, 22, 4, 2, 3, 13, 2, 23, 6,
			4, 3, 15, 2, 27, 4, 7, 3, 20, 4,
			1, 2, 11, 3, 21, 6, 3, 2, 15, 1,
			25, 6, 6, 2, 17, 3, 29, 4, 10, 2,
			20, 6, 3, 1, 13, 3, 24, 5, 4, 3,
			17, 1, 28, 5, 8, 3, 18, 6, 1, 1,
			12, 2, 22, 6, 2, 3, 14, 2, 26, 4,
			6, 3, 17, 2, 28, 6, 10, 1, 20, 6,
			1, 2, 12, 3, 24, 4, 5, 2, 15, 3,
			28, 4, 9, 2, 19, 6, 33, 3, 12, 1,
			23, 5, 3, 3, 13, 3, 25, 4, 6, 2,
			16, 3, 26, 6, 8, 2, 20, 4, 30, 3,
			11, 2, 24, 4, 4, 3, 15, 2, 25, 6,
			8, 1, 18, 6, 33, 2, 9, 3, 22, 4,
			3, 2, 13, 3, 25, 4, 6, 3, 17, 2,
			27, 6, 9, 1, 21, 5, 1, 3, 11, 3,
			23, 4, 5, 2, 15, 3, 25, 6, 6, 2,
			19, 4, 33, 3, 10, 2, 22, 4, 3, 3,
			14, 2, 24, 6, 6, 1
		};

		// Token: 0x04003851 RID: 14417
		private static readonly int[,] LunarMonthLen = new int[,]
		{
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0
			},
			{
				0, 30, 29, 29, 29, 30, 29, 30, 29, 30,
				29, 30, 29, 0
			},
			{
				0, 30, 29, 30, 29, 30, 29, 30, 29, 30,
				29, 30, 29, 0
			},
			{
				0, 30, 30, 30, 29, 30, 29, 30, 29, 30,
				29, 30, 29, 0
			},
			{
				0, 30, 29, 29, 29, 30, 30, 29, 30, 29,
				30, 29, 30, 29
			},
			{
				0, 30, 29, 30, 29, 30, 30, 29, 30, 29,
				30, 29, 30, 29
			},
			{
				0, 30, 30, 30, 29, 30, 30, 29, 30, 29,
				30, 29, 30, 29
			}
		};

		// Token: 0x04003852 RID: 14418
		internal static readonly DateTime calendarMinValue = new DateTime(1583, 1, 1);

		// Token: 0x04003853 RID: 14419
		internal static readonly DateTime calendarMaxValue = new DateTime(new DateTime(2239, 9, 29, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x04003854 RID: 14420
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 5790;

		// Token: 0x020009E6 RID: 2534
		internal class __DateBuffer
		{
			// Token: 0x06005CFD RID: 23805 RVA: 0x000025BE File Offset: 0x000007BE
			public __DateBuffer()
			{
			}

			// Token: 0x04003855 RID: 14421
			internal int year;

			// Token: 0x04003856 RID: 14422
			internal int month;

			// Token: 0x04003857 RID: 14423
			internal int day;
		}
	}
}
