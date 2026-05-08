using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D3 RID: 211
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct DateTime : IComparable, IFormattable, IConvertible, IComparable<DateTime>, IEquatable<DateTime>, ISerializable, ISpanFormattable
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x0001D2CE File Offset: 0x0001B4CE
		public DateTime(long ticks)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
			this._dateData = (ulong)ticks;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001D2F8 File Offset: 0x0001B4F8
		private DateTime(ulong dateData)
		{
			this._dateData = dateData;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001D304 File Offset: 0x0001B504
		public DateTime(long ticks, DateTimeKind kind)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			this._dateData = (ulong)(ticks | ((long)kind << 62));
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001D358 File Offset: 0x0001B558
		internal DateTime(long ticks, DateTimeKind kind, bool isAmbiguousDst)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
			this._dateData = (ulong)(ticks | (isAmbiguousDst ? (-4611686018427387904L) : long.MinValue));
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001D3A5 File Offset: 0x0001B5A5
		public DateTime(int year, int month, int day)
		{
			this._dateData = (ulong)DateTime.DateToTicks(year, month, day);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001D3B5 File Offset: 0x0001B5B5
		public DateTime(int year, int month, int day, Calendar calendar)
		{
			this = new DateTime(year, month, day, 0, 0, 0, calendar);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001D3C5 File Offset: 0x0001B5C5
		public DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this._dateData = (ulong)(DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second));
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			this._dateData = (ulong)(num | ((long)kind << 62));
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001D430 File Offset: 0x0001B630
		public DateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			this._dateData = (ulong)calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001D46C File Offset: 0x0001B66C
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)num;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)(num | ((long)kind << 62));
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001D59C File Offset: 0x0001B79C
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)num;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001D638 File Offset: 0x0001B838
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)(num | ((long)kind << 62));
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001D6F4 File Offset: 0x0001B8F4
		private DateTime(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			bool flag = false;
			bool flag2 = false;
			long num = 0L;
			ulong num2 = 0UL;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "ticks"))
				{
					if (name == "dateData")
					{
						num2 = Convert.ToUInt64(enumerator.Value, CultureInfo.InvariantCulture);
						flag2 = true;
					}
				}
				else
				{
					num = Convert.ToInt64(enumerator.Value, CultureInfo.InvariantCulture);
					flag = true;
				}
			}
			if (flag2)
			{
				this._dateData = num2;
			}
			else
			{
				if (!flag)
				{
					throw new SerializationException("Invalid serialized DateTime data. Unable to find 'ticks' or 'dateData'.");
				}
				this._dateData = (ulong)num;
			}
			long internalTicks = this.InternalTicks;
			if (internalTicks < 0L || internalTicks > 3155378975999999999L)
			{
				throw new SerializationException("Invalid serialized DateTime data. Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001D7C6 File Offset: 0x0001B9C6
		internal long InternalTicks
		{
			get
			{
				return (long)(this._dateData & 4611686018427387903UL);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001D7D8 File Offset: 0x0001B9D8
		private ulong InternalKind
		{
			get
			{
				return this._dateData & 13835058055282163712UL;
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001D7EA File Offset: 0x0001B9EA
		public DateTime Add(TimeSpan value)
		{
			return this.AddTicks(value._ticks);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001D7F8 File Offset: 0x0001B9F8
		private DateTime Add(double value, int scale)
		{
			long num = (long)(value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5)));
			if (num <= -315537897600000L || num >= 315537897600000L)
			{
				throw new ArgumentOutOfRangeException("value", "Value to add was out of range.");
			}
			return this.AddTicks(num * 10000L);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001D862 File Offset: 0x0001BA62
		public DateTime AddDays(double value)
		{
			return this.Add(value, 86400000);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001D870 File Offset: 0x0001BA70
		public DateTime AddHours(double value)
		{
			return this.Add(value, 3600000);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001D87E File Offset: 0x0001BA7E
		public DateTime AddMilliseconds(double value)
		{
			return this.Add(value, 1);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001D888 File Offset: 0x0001BA88
		public DateTime AddMinutes(double value)
		{
			return this.Add(value, 60000);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001D898 File Offset: 0x0001BA98
		public DateTime AddMonths(int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", "Months value must be between +/-120000.");
			}
			int num;
			int num2;
			int num3;
			this.GetDatePart(out num, out num2, out num3);
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
			if (num < 1 || num > 9999)
			{
				throw new ArgumentOutOfRangeException("months", "The added or subtracted value results in an un-representable DateTime.");
			}
			int num5 = DateTime.DaysInMonth(num, num2);
			if (num3 > num5)
			{
				num3 = num5;
			}
			return new DateTime((ulong)((DateTime.DateToTicks(num, num2, num3) + this.InternalTicks % 864000000000L) | (long)this.InternalKind));
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001D951 File Offset: 0x0001BB51
		public DateTime AddSeconds(double value)
		{
			return this.Add(value, 1000);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001D960 File Offset: 0x0001BB60
		public DateTime AddTicks(long value)
		{
			long internalTicks = this.InternalTicks;
			if (value > 3155378975999999999L - internalTicks || value < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("value", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)((internalTicks + value) | (long)this.InternalKind));
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		public DateTime AddYears(int value)
		{
			if (value < -10000 || value > 10000)
			{
				throw new ArgumentOutOfRangeException("years", "Years value must be between +/-10000.");
			}
			return this.AddMonths(value * 12);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001D9D4 File Offset: 0x0001BBD4
		public static int Compare(DateTime t1, DateTime t2)
		{
			long internalTicks = t1.InternalTicks;
			long internalTicks2 = t2.InternalTicks;
			if (internalTicks > internalTicks2)
			{
				return 1;
			}
			if (internalTicks < internalTicks2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001D9FE File Offset: 0x0001BBFE
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is DateTime))
			{
				throw new ArgumentException("Object must be of type DateTime.");
			}
			return DateTime.Compare(this, (DateTime)value);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001DA29 File Offset: 0x0001BC29
		public int CompareTo(DateTime value)
		{
			return DateTime.Compare(this, value);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001DA38 File Offset: 0x0001BC38
		private static long DateToTicks(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = (DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					return (long)(num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1) * 864000000000L;
				}
			}
			throw new ArgumentOutOfRangeException(null, "Year, Month, and Day parameters describe an un-representable DateTime.");
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001DABC File Offset: 0x0001BCBC
		private static long TimeToTicks(int hour, int minute, int second)
		{
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return TimeSpan.TimeToTicks(hour, minute, second);
			}
			throw new ArgumentOutOfRangeException(null, "Hour, Minute, and Second parameters describe an un-representable DateTime.");
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001DAF0 File Offset: 0x0001BCF0
		public static int DaysInMonth(int year, int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
			int[] array = (DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001DB34 File Offset: 0x0001BD34
		internal static long DoubleDateToTicks(double value)
		{
			if (value >= 2958466.0 || value <= -657435.0)
			{
				throw new ArgumentException(" Not a legal OleAut date.");
			}
			long num = (long)(value * 86400000.0 + ((value >= 0.0) ? 0.5 : (-0.5)));
			if (num < 0L)
			{
				num -= num % 86400000L * 2L;
			}
			num += 59926435200000L;
			if (num < 0L || num >= 315537897600000L)
			{
				throw new ArgumentException("OleAut date did not convert to a DateTime correctly.");
			}
			return num * 10000L;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001DBD8 File Offset: 0x0001BDD8
		public override bool Equals(object value)
		{
			return value is DateTime && this.InternalTicks == ((DateTime)value).InternalTicks;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001DC05 File Offset: 0x0001BE05
		public bool Equals(DateTime value)
		{
			return this.InternalTicks == value.InternalTicks;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001DC16 File Offset: 0x0001BE16
		public static bool Equals(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks == t2.InternalTicks;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001DC28 File Offset: 0x0001BE28
		public static DateTime FromBinary(long dateData)
		{
			if ((dateData & -9223372036854775808L) == 0L)
			{
				return DateTime.FromBinaryRaw(dateData);
			}
			long num = dateData & 4611686018427387903L;
			if (num > 4611685154427387904L)
			{
				num -= 4611686018427387904L;
			}
			bool flag = false;
			long num2;
			if (num < 0L)
			{
				num2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MinValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else if (num > 3155378975999999999L)
			{
				num2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MaxValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else
			{
				DateTime dateTime = new DateTime(num, DateTimeKind.Utc);
				bool flag2 = false;
				num2 = TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, TimeZoneInfo.Local, out flag2, out flag).Ticks;
			}
			num += num2;
			if (num < 0L)
			{
				num += 864000000000L;
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.", "dateData");
			}
			return new DateTime(num, DateTimeKind.Local, flag);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001DD10 File Offset: 0x0001BF10
		internal static DateTime FromBinaryRaw(long dateData)
		{
			long num = dateData & 4611686018427387903L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.", "dateData");
			}
			return new DateTime((ulong)dateData);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001DD50 File Offset: 0x0001BF50
		public static DateTime FromFileTime(long fileTime)
		{
			return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001DD6B File Offset: 0x0001BF6B
		public static DateTime FromFileTimeUtc(long fileTime)
		{
			if (fileTime < 0L || fileTime > 2650467743999999999L)
			{
				throw new ArgumentOutOfRangeException("fileTime", "Not a valid Win32 FileTime.");
			}
			return new DateTime(fileTime + 504911232000000000L, DateTimeKind.Utc);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001DD9F File Offset: 0x0001BF9F
		public static DateTime FromOADate(double d)
		{
			return new DateTime(DateTime.DoubleDateToTicks(d), DateTimeKind.Unspecified);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001DDAD File Offset: 0x0001BFAD
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ticks", this.InternalTicks);
			info.AddValue("dateData", this._dateData);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001DDDF File Offset: 0x0001BFDF
		public bool IsDaylightSavingTime()
		{
			return this.Kind != DateTimeKind.Utc && TimeZoneInfo.Local.IsDaylightSavingTime(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001DDFD File Offset: 0x0001BFFD
		public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
		{
			return new DateTime(value.InternalTicks, kind);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001DE0C File Offset: 0x0001C00C
		public long ToBinary()
		{
			if (this.Kind == DateTimeKind.Local)
			{
				TimeSpan localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				long num = this.Ticks - localUtcOffset.Ticks;
				if (num < 0L)
				{
					num = 4611686018427387904L + num;
				}
				return num | long.MinValue;
			}
			return (long)this._dateData;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001DE61 File Offset: 0x0001C061
		public DateTime Date
		{
			get
			{
				long internalTicks = this.InternalTicks;
				return new DateTime((ulong)((internalTicks - internalTicks % 864000000000L) | (long)this.InternalKind));
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001DE84 File Offset: 0x0001C084
		private int GetDatePart(int part)
		{
			int i = (int)(this.InternalTicks / 864000000000L);
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
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
			int num5 = (i >> 5) + 1;
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

		// Token: 0x06000808 RID: 2056 RVA: 0x0001DF6C File Offset: 0x0001C16C
		internal void GetDatePart(out int year, out int month, out int day)
		{
			int i = (int)(this.InternalTicks / 864000000000L);
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
			year = num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			i -= num4 * 365;
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
			int num5 = (i >> 5) + 1;
			while (i >= array[num5])
			{
				num5++;
			}
			month = num5;
			day = i - array[num5 - 1] + 1;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001E046 File Offset: 0x0001C246
		public int Day
		{
			get
			{
				return this.GetDatePart(3);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001E04F File Offset: 0x0001C24F
		public DayOfWeek DayOfWeek
		{
			get
			{
				return (DayOfWeek)((this.InternalTicks / 864000000000L + 1L) % 7L);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001E068 File Offset: 0x0001C268
		public int DayOfYear
		{
			get
			{
				return this.GetDatePart(1);
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001E074 File Offset: 0x0001C274
		public override int GetHashCode()
		{
			long internalTicks = this.InternalTicks;
			return (int)internalTicks ^ (int)(internalTicks >> 32);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001E090 File Offset: 0x0001C290
		public int Hour
		{
			get
			{
				return (int)(this.InternalTicks / 36000000000L % 24L);
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001E0A7 File Offset: 0x0001C2A7
		internal bool IsAmbiguousDaylightSavingTime()
		{
			return this.InternalKind == 13835058055282163712UL;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		public DateTimeKind Kind
		{
			get
			{
				ulong internalKind = this.InternalKind;
				if (internalKind == 0UL)
				{
					return DateTimeKind.Unspecified;
				}
				if (internalKind != 4611686018427387904UL)
				{
					return DateTimeKind.Local;
				}
				return DateTimeKind.Utc;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001E0E6 File Offset: 0x0001C2E6
		public int Millisecond
		{
			get
			{
				return (int)(this.InternalTicks / 10000L % 1000L);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001E0FD File Offset: 0x0001C2FD
		public int Minute
		{
			get
			{
				return (int)(this.InternalTicks / 600000000L % 60L);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001E111 File Offset: 0x0001C311
		public int Month
		{
			get
			{
				return this.GetDatePart(2);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001E11C File Offset: 0x0001C31C
		public static DateTime Now
		{
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				bool flag = false;
				long ticks = TimeZoneInfo.GetDateTimeNowUtcOffsetFromUtc(utcNow, out flag).Ticks;
				long num = utcNow.Ticks + ticks;
				if (num > 3155378975999999999L)
				{
					return new DateTime(3155378975999999999L, DateTimeKind.Local);
				}
				if (num < 0L)
				{
					return new DateTime(0L, DateTimeKind.Local);
				}
				return new DateTime(num, DateTimeKind.Local, flag);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001E17F File Offset: 0x0001C37F
		public int Second
		{
			get
			{
				return (int)(this.InternalTicks / 10000000L % 60L);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001E193 File Offset: 0x0001C393
		public long Ticks
		{
			get
			{
				return this.InternalTicks;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001E19B File Offset: 0x0001C39B
		public TimeSpan TimeOfDay
		{
			get
			{
				return new TimeSpan(this.InternalTicks % 864000000000L);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001E1B4 File Offset: 0x0001C3B4
		public static DateTime Today
		{
			get
			{
				return DateTime.Now.Date;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001E1CE File Offset: 0x0001C3CE
		public int Year
		{
			get
			{
				return this.GetDatePart(0);
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001E1D7 File Offset: 0x0001C3D7
		public static bool IsLeapYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", "Year must be between 1 and 9999.");
			}
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001E20E File Offset: 0x0001C40E
		public static DateTime Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001E22B File Offset: 0x0001C42B
		public static DateTime Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001E249 File Offset: 0x0001C449
		public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001E272 File Offset: 0x0001C472
		public static DateTime Parse(ReadOnlySpan<char> s, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.None)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001E28C File Offset: 0x0001C48C
		public static DateTime ParseExact(string s, string format, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001E2BA File Offset: 0x0001C4BA
		public static DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001E2F3 File Offset: 0x0001C4F3
		public static DateTime ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001E30E File Offset: 0x0001C50E
		public static DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001E338 File Offset: 0x0001C538
		public static DateTime ParseExact(ReadOnlySpan<char> s, string[] formats, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001E353 File Offset: 0x0001C553
		public TimeSpan Subtract(DateTime value)
		{
			return new TimeSpan(this.InternalTicks - value.InternalTicks);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001E368 File Offset: 0x0001C568
		public DateTime Subtract(TimeSpan value)
		{
			long internalTicks = this.InternalTicks;
			long ticks = value._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("value", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)((internalTicks - ticks) | (long)this.InternalKind));
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
		private static double TicksToOADate(long value)
		{
			if (value == 0L)
			{
				return 0.0;
			}
			if (value < 864000000000L)
			{
				value += 599264352000000000L;
			}
			if (value < 31241376000000000L)
			{
				throw new OverflowException(" Not a legal OleAut date.");
			}
			long num = (value - 599264352000000000L) / 10000L;
			if (num < 0L)
			{
				long num2 = num % 86400000L;
				if (num2 != 0L)
				{
					num -= (86400000L + num2) * 2L;
				}
			}
			return (double)num / 86400000.0;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001E43C File Offset: 0x0001C63C
		public double ToOADate()
		{
			return DateTime.TicksToOADate(this.InternalTicks);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001E44C File Offset: 0x0001C64C
		public long ToFileTime()
		{
			return this.ToUniversalTime().ToFileTimeUtc();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001E468 File Offset: 0x0001C668
		public long ToFileTimeUtc()
		{
			long num = (((this.InternalKind & 9223372036854775808UL) != 0UL) ? this.ToUniversalTime().InternalTicks : this.InternalTicks) - 504911232000000000L;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(null, "Not a valid Win32 FileTime.");
			}
			return num;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
		public DateTime ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001E4C4 File Offset: 0x0001C6C4
		internal DateTime ToLocalTime(bool throwOnOverflow)
		{
			if (this.Kind == DateTimeKind.Local)
			{
				return this;
			}
			bool flag = false;
			bool flag2 = false;
			long ticks = TimeZoneInfo.GetUtcOffsetFromUtc(this, TimeZoneInfo.Local, out flag, out flag2).Ticks;
			long num = this.Ticks + ticks;
			if (num > 3155378975999999999L)
			{
				if (throwOnOverflow)
				{
					throw new ArgumentException("Specified argument was out of the range of valid values.");
				}
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			else
			{
				if (num >= 0L)
				{
					return new DateTime(num, DateTimeKind.Local, flag2);
				}
				if (throwOnOverflow)
				{
					throw new ArgumentException("Specified argument was out of the range of valid values.");
				}
				return new DateTime(0L, DateTimeKind.Local);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001E55A File Offset: 0x0001C75A
		public string ToLongDateString()
		{
			return DateTimeFormat.Format(this, "D", null);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001E56D File Offset: 0x0001C76D
		public string ToLongTimeString()
		{
			return DateTimeFormat.Format(this, "T", null);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001E580 File Offset: 0x0001C780
		public string ToShortDateString()
		{
			return DateTimeFormat.Format(this, "d", null);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001E593 File Offset: 0x0001C793
		public string ToShortTimeString()
		{
			return DateTimeFormat.Format(this, "t", null);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001E5A6 File Offset: 0x0001C7A6
		public override string ToString()
		{
			return DateTimeFormat.Format(this, null, null);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001E5B5 File Offset: 0x0001C7B5
		public string ToString(string format)
		{
			return DateTimeFormat.Format(this, format, null);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001E5C4 File Offset: 0x0001C7C4
		public string ToString(IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, null, provider);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001E5D3 File Offset: 0x0001C7D3
		public string ToString(string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, format, provider);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001E5E2 File Offset: 0x0001C7E2
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return DateTimeFormat.TryFormat(this, destination, out charsWritten, format, provider);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
		public DateTime ToUniversalTime()
		{
			return TimeZoneInfo.ConvertTimeToUtc(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001E602 File Offset: 0x0001C802
		public static bool TryParse(string s, out DateTime result)
		{
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001E622 File Offset: 0x0001C822
		public static bool TryParse(ReadOnlySpan<char> s, out DateTime result)
		{
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001E631 File Offset: 0x0001C831
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001E65D File Offset: 0x0001C85D
		public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001E678 File Offset: 0x0001C878
		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null || format == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001E6AF File Offset: 0x0001C8AF
		public static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001E6CC File Offset: 0x0001C8CC
		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001E6FB File Offset: 0x0001C8FB
		public static bool TryParseExact(ReadOnlySpan<char> s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001E718 File Offset: 0x0001C918
		public static DateTime operator +(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (ticks > 3155378975999999999L - internalTicks || ticks < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("t", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)((internalTicks + ticks) | (long)d.InternalKind));
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001E76C File Offset: 0x0001C96C
		public static DateTime operator -(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("t", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)((internalTicks - ticks) | (long)d.InternalKind));
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001E7BA File Offset: 0x0001C9BA
		public static TimeSpan operator -(DateTime d1, DateTime d2)
		{
			return new TimeSpan(d1.InternalTicks - d2.InternalTicks);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001DC16 File Offset: 0x0001BE16
		public static bool operator ==(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks == d2.InternalTicks;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		public static bool operator !=(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks != d2.InternalTicks;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001E7E5 File Offset: 0x0001C9E5
		public static bool operator <(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks < t2.InternalTicks;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001E7F7 File Offset: 0x0001C9F7
		public static bool operator <=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks <= t2.InternalTicks;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001E80C File Offset: 0x0001CA0C
		public static bool operator >(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks > t2.InternalTicks;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001E81E File Offset: 0x0001CA1E
		public static bool operator >=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks >= t2.InternalTicks;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001E833 File Offset: 0x0001CA33
		public string[] GetDateTimeFormats()
		{
			return this.GetDateTimeFormats(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001E840 File Offset: 0x0001CA40
		public string[] GetDateTimeFormats(IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, DateTimeFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001E853 File Offset: 0x0001CA53
		public string[] GetDateTimeFormats(char format)
		{
			return this.GetDateTimeFormats(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001E861 File Offset: 0x0001CA61
		public string[] GetDateTimeFormats(char format, IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, format, DateTimeFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001E875 File Offset: 0x0001CA75
		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001E879 File Offset: 0x0001CA79
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Boolean"));
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001E894 File Offset: 0x0001CA94
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Char"));
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001E8AF File Offset: 0x0001CAAF
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "SByte"));
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001E8CA File Offset: 0x0001CACA
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Byte"));
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001E8E5 File Offset: 0x0001CAE5
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Int16"));
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001E900 File Offset: 0x0001CB00
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "UInt16"));
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001E91B File Offset: 0x0001CB1B
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Int32"));
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001E936 File Offset: 0x0001CB36
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "UInt32"));
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001E951 File Offset: 0x0001CB51
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Int64"));
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001E96C File Offset: 0x0001CB6C
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "UInt64"));
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001E987 File Offset: 0x0001CB87
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Single"));
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001E9A2 File Offset: 0x0001CBA2
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Double"));
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001E9BD File Offset: 0x0001CBBD
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Decimal"));
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		internal static bool TryCreate(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime result)
		{
			result = DateTime.MinValue;
			if (year < 1 || year > 9999 || month < 1 || month > 12)
			{
				return false;
			}
			int[] array = (DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
			if (day < 1 || day > array[month] - array[month - 1])
			{
				return false;
			}
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				return false;
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				return false;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				return false;
			}
			result = new DateTime(num, DateTimeKind.Unspecified);
			return true;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001EABF File Offset: 0x0001CCBF
		public static DateTime UtcNow
		{
			get
			{
				return new DateTime((ulong)((DateTime.GetSystemTimeAsFileTime() + 504911232000000000L) | 4611686018427387904L));
			}
		}

		// Token: 0x0600085C RID: 2140
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSystemTimeAsFileTime();

		// Token: 0x0600085D RID: 2141 RVA: 0x0001EADF File Offset: 0x0001CCDF
		internal long ToBinaryRaw()
		{
			return (long)this._dateData;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001EAE8 File Offset: 0x0001CCE8
		// Note: this type is marked as 'beforefieldinit'.
		static DateTime()
		{
		}

		// Token: 0x04000F16 RID: 3862
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x04000F17 RID: 3863
		private const long TicksPerSecond = 10000000L;

		// Token: 0x04000F18 RID: 3864
		private const long TicksPerMinute = 600000000L;

		// Token: 0x04000F19 RID: 3865
		private const long TicksPerHour = 36000000000L;

		// Token: 0x04000F1A RID: 3866
		private const long TicksPerDay = 864000000000L;

		// Token: 0x04000F1B RID: 3867
		private const int MillisPerSecond = 1000;

		// Token: 0x04000F1C RID: 3868
		private const int MillisPerMinute = 60000;

		// Token: 0x04000F1D RID: 3869
		private const int MillisPerHour = 3600000;

		// Token: 0x04000F1E RID: 3870
		private const int MillisPerDay = 86400000;

		// Token: 0x04000F1F RID: 3871
		private const int DaysPerYear = 365;

		// Token: 0x04000F20 RID: 3872
		private const int DaysPer4Years = 1461;

		// Token: 0x04000F21 RID: 3873
		private const int DaysPer100Years = 36524;

		// Token: 0x04000F22 RID: 3874
		private const int DaysPer400Years = 146097;

		// Token: 0x04000F23 RID: 3875
		private const int DaysTo1601 = 584388;

		// Token: 0x04000F24 RID: 3876
		private const int DaysTo1899 = 693593;

		// Token: 0x04000F25 RID: 3877
		internal const int DaysTo1970 = 719162;

		// Token: 0x04000F26 RID: 3878
		private const int DaysTo10000 = 3652059;

		// Token: 0x04000F27 RID: 3879
		internal const long MinTicks = 0L;

		// Token: 0x04000F28 RID: 3880
		internal const long MaxTicks = 3155378975999999999L;

		// Token: 0x04000F29 RID: 3881
		private const long MaxMillis = 315537897600000L;

		// Token: 0x04000F2A RID: 3882
		internal const long UnixEpochTicks = 621355968000000000L;

		// Token: 0x04000F2B RID: 3883
		private const long FileTimeOffset = 504911232000000000L;

		// Token: 0x04000F2C RID: 3884
		private const long DoubleDateOffset = 599264352000000000L;

		// Token: 0x04000F2D RID: 3885
		private const long OADateMinAsTicks = 31241376000000000L;

		// Token: 0x04000F2E RID: 3886
		private const double OADateMinAsDouble = -657435.0;

		// Token: 0x04000F2F RID: 3887
		private const double OADateMaxAsDouble = 2958466.0;

		// Token: 0x04000F30 RID: 3888
		private const int DatePartYear = 0;

		// Token: 0x04000F31 RID: 3889
		private const int DatePartDayOfYear = 1;

		// Token: 0x04000F32 RID: 3890
		private const int DatePartMonth = 2;

		// Token: 0x04000F33 RID: 3891
		private const int DatePartDay = 3;

		// Token: 0x04000F34 RID: 3892
		private static readonly int[] s_daysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x04000F35 RID: 3893
		private static readonly int[] s_daysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x04000F36 RID: 3894
		public static readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);

		// Token: 0x04000F37 RID: 3895
		public static readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);

		// Token: 0x04000F38 RID: 3896
		public static readonly DateTime UnixEpoch = new DateTime(621355968000000000L, DateTimeKind.Utc);

		// Token: 0x04000F39 RID: 3897
		private const ulong TicksMask = 4611686018427387903UL;

		// Token: 0x04000F3A RID: 3898
		private const ulong FlagsMask = 13835058055282163712UL;

		// Token: 0x04000F3B RID: 3899
		private const ulong LocalMask = 9223372036854775808UL;

		// Token: 0x04000F3C RID: 3900
		private const long TicksCeiling = 4611686018427387904L;

		// Token: 0x04000F3D RID: 3901
		private const ulong KindUnspecified = 0UL;

		// Token: 0x04000F3E RID: 3902
		private const ulong KindUtc = 4611686018427387904UL;

		// Token: 0x04000F3F RID: 3903
		private const ulong KindLocal = 9223372036854775808UL;

		// Token: 0x04000F40 RID: 3904
		private const ulong KindLocalAmbiguousDst = 13835058055282163712UL;

		// Token: 0x04000F41 RID: 3905
		private const int KindShift = 62;

		// Token: 0x04000F42 RID: 3906
		private const string TicksField = "ticks";

		// Token: 0x04000F43 RID: 3907
		private const string DateDataField = "dateData";

		// Token: 0x04000F44 RID: 3908
		private readonly ulong _dateData;
	}
}
