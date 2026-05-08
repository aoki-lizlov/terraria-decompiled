using System;
using System.Globalization;

namespace System
{
	// Token: 0x0200015C RID: 348
	[Serializable]
	public readonly struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>, IFormattable, ISpanFormattable
	{
		// Token: 0x06000EFE RID: 3838 RVA: 0x0003D5BB File Offset: 0x0003B7BB
		public TimeSpan(long ticks)
		{
			this._ticks = ticks;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0003D5C4 File Offset: 0x0003B7C4
		public TimeSpan(int hours, int minutes, int seconds)
		{
			this._ticks = TimeSpan.TimeToTicks(hours, minutes, seconds);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003D5D4 File Offset: 0x0003B7D4
		public TimeSpan(int days, int hours, int minutes, int seconds)
		{
			this = new TimeSpan(days, hours, minutes, seconds, 0);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003D5E4 File Offset: 0x0003B7E4
		public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			long num = ((long)days * 3600L * 24L + (long)hours * 3600L + (long)minutes * 60L + (long)seconds) * 1000L + (long)milliseconds;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				throw new ArgumentOutOfRangeException(null, "TimeSpan overflowed because the duration is too long.");
			}
			this._ticks = num * 10000L;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0003D651 File Offset: 0x0003B851
		public long Ticks
		{
			get
			{
				return this._ticks;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0003D659 File Offset: 0x0003B859
		public int Days
		{
			get
			{
				return (int)(this._ticks / 864000000000L);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0003D66C File Offset: 0x0003B86C
		public int Hours
		{
			get
			{
				return (int)(this._ticks / 36000000000L % 24L);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0003D683 File Offset: 0x0003B883
		public int Milliseconds
		{
			get
			{
				return (int)(this._ticks / 10000L % 1000L);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0003D69A File Offset: 0x0003B89A
		public int Minutes
		{
			get
			{
				return (int)(this._ticks / 600000000L % 60L);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0003D6AE File Offset: 0x0003B8AE
		public int Seconds
		{
			get
			{
				return (int)(this._ticks / 10000000L % 60L);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0003D6C2 File Offset: 0x0003B8C2
		public double TotalDays
		{
			get
			{
				return (double)this._ticks * 1.1574074074074074E-12;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0003D6D5 File Offset: 0x0003B8D5
		public double TotalHours
		{
			get
			{
				return (double)this._ticks * 2.7777777777777777E-11;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0003D6E8 File Offset: 0x0003B8E8
		public double TotalMilliseconds
		{
			get
			{
				double num = (double)this._ticks * 0.0001;
				if (num > 922337203685477.0)
				{
					return 922337203685477.0;
				}
				if (num < -922337203685477.0)
				{
					return -922337203685477.0;
				}
				return num;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0003D734 File Offset: 0x0003B934
		public double TotalMinutes
		{
			get
			{
				return (double)this._ticks * 1.6666666666666667E-09;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0003D747 File Offset: 0x0003B947
		public double TotalSeconds
		{
			get
			{
				return (double)this._ticks * 1E-07;
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003D75C File Offset: 0x0003B95C
		public TimeSpan Add(TimeSpan ts)
		{
			long num = this._ticks + ts._ticks;
			if (this._ticks >> 63 == ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return new TimeSpan(num);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0003D7AB File Offset: 0x0003B9AB
		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			if (t1._ticks > t2._ticks)
			{
				return 1;
			}
			if (t1._ticks < t2._ticks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003D7D0 File Offset: 0x0003B9D0
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is TimeSpan))
			{
				throw new ArgumentException("Object must be of type TimeSpan.");
			}
			long ticks = ((TimeSpan)value)._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003D818 File Offset: 0x0003BA18
		public int CompareTo(TimeSpan value)
		{
			long ticks = value._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0003D843 File Offset: 0x0003BA43
		public static TimeSpan FromDays(double value)
		{
			return TimeSpan.Interval(value, 86400000);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003D850 File Offset: 0x0003BA50
		public TimeSpan Duration()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException("The duration cannot be returned for TimeSpan.MinValue because the absolute value of TimeSpan.MinValue exceeds the value of TimeSpan.MaxValue.");
			}
			return new TimeSpan((this._ticks >= 0L) ? this._ticks : (-this._ticks));
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003D88D File Offset: 0x0003BA8D
		public override bool Equals(object value)
		{
			return value is TimeSpan && this._ticks == ((TimeSpan)value)._ticks;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003D8AC File Offset: 0x0003BAAC
		public bool Equals(TimeSpan obj)
		{
			return this._ticks == obj._ticks;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0003D8AC File Offset: 0x0003BAAC
		public static bool Equals(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003D8BC File Offset: 0x0003BABC
		public override int GetHashCode()
		{
			return (int)this._ticks ^ (int)(this._ticks >> 32);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		public static TimeSpan FromHours(double value)
		{
			return TimeSpan.Interval(value, 3600000);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
		private static TimeSpan Interval(double value, int scale)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException("TimeSpan does not accept floating point Not-a-Number values.");
			}
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5));
			if (num > 922337203685477.0 || num < -922337203685477.0)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return new TimeSpan((long)num * 10000L);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0003D957 File Offset: 0x0003BB57
		public static TimeSpan FromMilliseconds(double value)
		{
			return TimeSpan.Interval(value, 1);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0003D960 File Offset: 0x0003BB60
		public static TimeSpan FromMinutes(double value)
		{
			return TimeSpan.Interval(value, 60000);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003D96D File Offset: 0x0003BB6D
		public TimeSpan Negate()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException("Negating the minimum value of a twos complement number is invalid.");
			}
			return new TimeSpan(-this._ticks);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003D998 File Offset: 0x0003BB98
		public static TimeSpan FromSeconds(double value)
		{
			return TimeSpan.Interval(value, 1000);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003D9A8 File Offset: 0x0003BBA8
		public TimeSpan Subtract(TimeSpan ts)
		{
			long num = this._ticks - ts._ticks;
			if (this._ticks >> 63 != ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return new TimeSpan(num);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003D9F7 File Offset: 0x0003BBF7
		public TimeSpan Multiply(double factor)
		{
			return this * factor;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003DA05 File Offset: 0x0003BC05
		public TimeSpan Divide(double divisor)
		{
			return this / divisor;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003DA13 File Offset: 0x0003BC13
		public double Divide(TimeSpan ts)
		{
			return this / ts;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0003DA21 File Offset: 0x0003BC21
		public static TimeSpan FromTicks(long value)
		{
			return new TimeSpan(value);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003DA2C File Offset: 0x0003BC2C
		internal static long TimeToTicks(int hour, int minute, int second)
		{
			long num = (long)hour * 3600L + (long)minute * 60L + (long)second;
			if (num > 922337203685L || num < -922337203685L)
			{
				throw new ArgumentOutOfRangeException(null, "TimeSpan overflowed because the duration is too long.");
			}
			return num * 10000000L;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003DA79 File Offset: 0x0003BC79
		private static void ValidateStyles(TimeSpanStyles style, string parameterName)
		{
			if (style != TimeSpanStyles.None && style != TimeSpanStyles.AssumeNegative)
			{
				throw new ArgumentException("An undefined TimeSpanStyles value is being used.", parameterName);
			}
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003DA8E File Offset: 0x0003BC8E
		public static TimeSpan Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.Parse(s, null);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003DAA6 File Offset: 0x0003BCA6
		public static TimeSpan Parse(string input, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.Parse(input, formatProvider);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003DABE File Offset: 0x0003BCBE
		public static TimeSpan Parse(ReadOnlySpan<char> input, IFormatProvider formatProvider = null)
		{
			return TimeSpanParse.Parse(input, formatProvider);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0003DAC7 File Offset: 0x0003BCC7
		public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return TimeSpanParse.ParseExact(input, format, formatProvider, TimeSpanStyles.None);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0003DAF0 File Offset: 0x0003BCF0
		public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003DB0A File Offset: 0x0003BD0A
		public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003DB3E File Offset: 0x0003BD3E
		public static TimeSpan ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles = TimeSpanStyles.None)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003DB54 File Offset: 0x0003BD54
		public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003DB79 File Offset: 0x0003BD79
		public static TimeSpan ParseExact(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles = TimeSpanStyles.None)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003DB8F File Offset: 0x0003BD8F
		public static bool TryParse(string s, out TimeSpan result)
		{
			if (s == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParse(s, null, out result);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003DBAA File Offset: 0x0003BDAA
		public static bool TryParse(ReadOnlySpan<char> s, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(s, null, out result);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003DBB4 File Offset: 0x0003BDB4
		public static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
		{
			if (input == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParse(input, formatProvider, out result);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0003DBCF File Offset: 0x0003BDCF
		public static bool TryParse(ReadOnlySpan<char> input, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(input, formatProvider, out result);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003DBD9 File Offset: 0x0003BDD9
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, out TimeSpan result)
		{
			if (input == null || format == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003DBFE File Offset: 0x0003BDFE
		public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0003DC0A File Offset: 0x0003BE0A
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, out TimeSpan result)
		{
			if (input == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003DC27 File Offset: 0x0003BE27
		public static bool TryParseExact(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0003DC33 File Offset: 0x0003BE33
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null || format == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0003DC65 File Offset: 0x0003BE65
		public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0003DC7D File Offset: 0x0003BE7D
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			if (input == null)
			{
				result = default(TimeSpan);
				return false;
			}
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003DCA7 File Offset: 0x0003BEA7
		public static bool TryParseExact(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpan.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003DCBF File Offset: 0x0003BEBF
		public override string ToString()
		{
			return TimeSpanFormat.Format(this, null, null);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003DCCE File Offset: 0x0003BECE
		public string ToString(string format)
		{
			return TimeSpanFormat.Format(this, format, null);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0003DCDD File Offset: 0x0003BEDD
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return TimeSpanFormat.Format(this, format, formatProvider);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003DCEC File Offset: 0x0003BEEC
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider formatProvider = null)
		{
			return TimeSpanFormat.TryFormat(this, destination, out charsWritten, format, formatProvider);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003DCFE File Offset: 0x0003BEFE
		public static TimeSpan operator -(TimeSpan t)
		{
			if (t._ticks == TimeSpan.MinValue._ticks)
			{
				throw new OverflowException("Negating the minimum value of a twos complement number is invalid.");
			}
			return new TimeSpan(-t._ticks);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003DD29 File Offset: 0x0003BF29
		public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000025CE File Offset: 0x000007CE
		public static TimeSpan operator +(TimeSpan t)
		{
			return t;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003DD33 File Offset: 0x0003BF33
		public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0003DD40 File Offset: 0x0003BF40
		public static TimeSpan operator *(TimeSpan timeSpan, double factor)
		{
			if (double.IsNaN(factor))
			{
				throw new ArgumentException("TimeSpan does not accept floating point Not-a-Number values.", "factor");
			}
			double num = Math.Round((double)timeSpan.Ticks * factor);
			if ((num > 9.223372036854776E+18) | (num < -9.223372036854776E+18))
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return TimeSpan.FromTicks((long)num);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003DDA2 File Offset: 0x0003BFA2
		public static TimeSpan operator *(double factor, TimeSpan timeSpan)
		{
			return timeSpan * factor;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003DDAC File Offset: 0x0003BFAC
		public static TimeSpan operator /(TimeSpan timeSpan, double divisor)
		{
			if (double.IsNaN(divisor))
			{
				throw new ArgumentException("TimeSpan does not accept floating point Not-a-Number values.", "divisor");
			}
			double num = Math.Round((double)timeSpan.Ticks / divisor);
			if (((num > 9.223372036854776E+18) | (num < -9.223372036854776E+18)) || double.IsNaN(num))
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return TimeSpan.FromTicks((long)num);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003DE16 File Offset: 0x0003C016
		public static double operator /(TimeSpan t1, TimeSpan t2)
		{
			return (double)t1.Ticks / (double)t2.Ticks;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003D8AC File Offset: 0x0003BAAC
		public static bool operator ==(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0003DE29 File Offset: 0x0003C029
		public static bool operator !=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks != t2._ticks;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003DE3C File Offset: 0x0003C03C
		public static bool operator <(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks < t2._ticks;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0003DE4C File Offset: 0x0003C04C
		public static bool operator <=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks <= t2._ticks;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003DE5F File Offset: 0x0003C05F
		public static bool operator >(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks > t2._ticks;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003DE6F File Offset: 0x0003C06F
		public static bool operator >=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks >= t2._ticks;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003DE82 File Offset: 0x0003C082
		// Note: this type is marked as 'beforefieldinit'.
		static TimeSpan()
		{
		}

		// Token: 0x0400118F RID: 4495
		public const long TicksPerMillisecond = 10000L;

		// Token: 0x04001190 RID: 4496
		private const double MillisecondsPerTick = 0.0001;

		// Token: 0x04001191 RID: 4497
		public const long TicksPerSecond = 10000000L;

		// Token: 0x04001192 RID: 4498
		private const double SecondsPerTick = 1E-07;

		// Token: 0x04001193 RID: 4499
		public const long TicksPerMinute = 600000000L;

		// Token: 0x04001194 RID: 4500
		private const double MinutesPerTick = 1.6666666666666667E-09;

		// Token: 0x04001195 RID: 4501
		public const long TicksPerHour = 36000000000L;

		// Token: 0x04001196 RID: 4502
		private const double HoursPerTick = 2.7777777777777777E-11;

		// Token: 0x04001197 RID: 4503
		public const long TicksPerDay = 864000000000L;

		// Token: 0x04001198 RID: 4504
		private const double DaysPerTick = 1.1574074074074074E-12;

		// Token: 0x04001199 RID: 4505
		private const int MillisPerSecond = 1000;

		// Token: 0x0400119A RID: 4506
		private const int MillisPerMinute = 60000;

		// Token: 0x0400119B RID: 4507
		private const int MillisPerHour = 3600000;

		// Token: 0x0400119C RID: 4508
		private const int MillisPerDay = 86400000;

		// Token: 0x0400119D RID: 4509
		internal const long MaxSeconds = 922337203685L;

		// Token: 0x0400119E RID: 4510
		internal const long MinSeconds = -922337203685L;

		// Token: 0x0400119F RID: 4511
		internal const long MaxMilliSeconds = 922337203685477L;

		// Token: 0x040011A0 RID: 4512
		internal const long MinMilliSeconds = -922337203685477L;

		// Token: 0x040011A1 RID: 4513
		internal const long TicksPerTenthSecond = 1000000L;

		// Token: 0x040011A2 RID: 4514
		public static readonly TimeSpan Zero = new TimeSpan(0L);

		// Token: 0x040011A3 RID: 4515
		public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);

		// Token: 0x040011A4 RID: 4516
		public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);

		// Token: 0x040011A5 RID: 4517
		internal readonly long _ticks;
	}
}
