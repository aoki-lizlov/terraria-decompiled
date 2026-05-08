using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x020009D6 RID: 2518
	public static class ISOWeek
	{
		// Token: 0x06005BB3 RID: 23475 RVA: 0x0013A0C8 File Offset: 0x001382C8
		public static int GetWeekOfYear(DateTime date)
		{
			int weekNumber = ISOWeek.GetWeekNumber(date);
			if (weekNumber < 1)
			{
				return ISOWeek.GetWeeksInYear(date.Year - 1);
			}
			if (weekNumber > ISOWeek.GetWeeksInYear(date.Year))
			{
				return 1;
			}
			return weekNumber;
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x0013A104 File Offset: 0x00138304
		public static int GetYear(DateTime date)
		{
			int weekNumber = ISOWeek.GetWeekNumber(date);
			if (weekNumber < 1)
			{
				return date.Year - 1;
			}
			if (weekNumber > ISOWeek.GetWeeksInYear(date.Year))
			{
				return date.Year + 1;
			}
			return date.Year;
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x0013A146 File Offset: 0x00138346
		public static DateTime GetYearStart(int year)
		{
			return ISOWeek.ToDateTime(year, 1, DayOfWeek.Monday);
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x0013A150 File Offset: 0x00138350
		public static DateTime GetYearEnd(int year)
		{
			return ISOWeek.ToDateTime(year, ISOWeek.GetWeeksInYear(year), DayOfWeek.Sunday);
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x0013A15F File Offset: 0x0013835F
		public static int GetWeeksInYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", "Year must be between 1 and 9999.");
			}
			if (ISOWeek.<GetWeeksInYear>g__P|8_0(year) == 4 || ISOWeek.<GetWeeksInYear>g__P|8_0(year - 1) == 3)
			{
				return 53;
			}
			return 52;
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x0013A198 File Offset: 0x00138398
		public static DateTime ToDateTime(int year, int week, DayOfWeek dayOfWeek)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", "Year must be between 1 and 9999.");
			}
			if (week < 1 || week > 53)
			{
				throw new ArgumentOutOfRangeException("week", "The week parameter must be in the range 1 through 53.");
			}
			if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > (DayOfWeek)7)
			{
				throw new ArgumentOutOfRangeException("dayOfWeek", "The DayOfWeek enumeration must be in the range 0 through 6.");
			}
			DateTime dateTime = new DateTime(year, 1, 4);
			int num = ISOWeek.GetWeekday(dateTime.DayOfWeek) + 3;
			int num2 = week * 7 + ISOWeek.GetWeekday(dayOfWeek) - num;
			return new DateTime(year, 1, 1).AddDays((double)(num2 - 1));
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x0013A22C File Offset: 0x0013842C
		private static int GetWeekNumber(DateTime date)
		{
			return (date.DayOfYear - ISOWeek.GetWeekday(date.DayOfWeek) + 10) / 7;
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x0013A247 File Offset: 0x00138447
		private static int GetWeekday(DayOfWeek dayOfWeek)
		{
			if (dayOfWeek != DayOfWeek.Sunday)
			{
				return (int)dayOfWeek;
			}
			return 7;
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x0013A24F File Offset: 0x0013844F
		[CompilerGenerated]
		internal static int <GetWeeksInYear>g__P|8_0(int y)
		{
			return (y + y / 4 - y / 100 + y / 400) % 7;
		}

		// Token: 0x0400377D RID: 14205
		private const int WeeksInLongYear = 53;

		// Token: 0x0400377E RID: 14206
		private const int WeeksInShortYear = 52;

		// Token: 0x0400377F RID: 14207
		private const int MinWeek = 1;

		// Token: 0x04003780 RID: 14208
		private const int MaxWeek = 53;
	}
}
