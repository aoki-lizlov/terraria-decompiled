using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	public readonly struct Double : IComparable, IConvertible, IFormattable, IComparable<double>, IEquatable<double>, ISpanFormattable
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFinite(double d)
		{
			return (BitConverter.DoubleToInt64Bits(d) & long.MaxValue) < 9218868437227405312L;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001FC09 File Offset: 0x0001DE09
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInfinity(double d)
		{
			return (BitConverter.DoubleToInt64Bits(d) & long.MaxValue) == 9218868437227405312L;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001FC26 File Offset: 0x0001DE26
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(double d)
		{
			return (BitConverter.DoubleToInt64Bits(d) & long.MaxValue) > 9218868437227405312L;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001FC43 File Offset: 0x0001DE43
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegative(double d)
		{
			return (BitConverter.DoubleToInt64Bits(d) & long.MinValue) == long.MinValue;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001FC60 File Offset: 0x0001DE60
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegativeInfinity(double d)
		{
			return d == double.NegativeInfinity;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001FC70 File Offset: 0x0001DE70
		[NonVersionable]
		public static bool IsNormal(double d)
		{
			long num = BitConverter.DoubleToInt64Bits(d);
			num &= long.MaxValue;
			return num < 9218868437227405312L && num != 0L && (num & 9218868437227405312L) != 0L;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001FCB0 File Offset: 0x0001DEB0
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPositiveInfinity(double d)
		{
			return d == double.PositiveInfinity;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		[NonVersionable]
		public static bool IsSubnormal(double d)
		{
			long num = BitConverter.DoubleToInt64Bits(d);
			num &= long.MaxValue;
			return num < 9218868437227405312L && num != 0L && (num & 9218868437227405312L) == 0L;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001FD00 File Offset: 0x0001DF00
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is double))
			{
				throw new ArgumentException("Object must be of type Double.");
			}
			double num = (double)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			if (this == num)
			{
				return 0;
			}
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001FD57 File Offset: 0x0001DF57
		public int CompareTo(double value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			if (this == value)
			{
				return 0;
			}
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001FD84 File Offset: 0x0001DF84
		public override bool Equals(object obj)
		{
			if (!(obj is double))
			{
				return false;
			}
			double num = (double)obj;
			return num == this || (double.IsNaN(num) && double.IsNaN(this));
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001FDBA File Offset: 0x0001DFBA
		[NonVersionable]
		public static bool operator ==(double left, double right)
		{
			return left == right;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001FDC0 File Offset: 0x0001DFC0
		[NonVersionable]
		public static bool operator !=(double left, double right)
		{
			return left != right;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001FDC9 File Offset: 0x0001DFC9
		[NonVersionable]
		public static bool operator <(double left, double right)
		{
			return left < right;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001FDCF File Offset: 0x0001DFCF
		[NonVersionable]
		public static bool operator >(double left, double right)
		{
			return left > right;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001FDD5 File Offset: 0x0001DFD5
		[NonVersionable]
		public static bool operator <=(double left, double right)
		{
			return left <= right;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001FDDE File Offset: 0x0001DFDE
		[NonVersionable]
		public static bool operator >=(double left, double right)
		{
			return left >= right;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001FDE7 File Offset: 0x0001DFE7
		public bool Equals(double obj)
		{
			return obj == this || (double.IsNaN(obj) && double.IsNaN(this));
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001FE04 File Offset: 0x0001E004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			long num = BitConverter.DoubleToInt64Bits(this);
			if (((num - 1L) & 9223372036854775807L) >= 9218868437227405312L)
			{
				num &= 9218868437227405312L;
			}
			return (int)num ^ (int)(num >> 32);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001FE46 File Offset: 0x0001E046
		public override string ToString()
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001FE55 File Offset: 0x0001E055
		public string ToString(string format)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001FE64 File Offset: 0x0001E064
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001FE74 File Offset: 0x0001E074
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001FE84 File Offset: 0x0001E084
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatDouble(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001FE97 File Offset: 0x0001E097
		public static double Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDouble(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		public static double Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDouble(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001FEDB File Offset: 0x0001E0DB
		public static double Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDouble(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001FEFD File Offset: 0x0001E0FD
		public static double Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDouble(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001FF21 File Offset: 0x0001E121
		public static double Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseDouble(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001FF36 File Offset: 0x0001E136
		public static bool TryParse(string s, out double result)
		{
			if (s == null)
			{
				result = 0.0;
				return false;
			}
			return double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001FF5E File Offset: 0x0001E15E
		public static bool TryParse(ReadOnlySpan<char> s, out double result)
		{
			return double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001FF71 File Offset: 0x0001E171
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				result = 0.0;
				return false;
			}
			return double.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001FF9C File Offset: 0x0001E19C
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out double result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001FFB4 File Offset: 0x0001E1B4
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out double result)
		{
			if (!Number.TryParseDouble(s, style, info, out result))
			{
				ReadOnlySpan<char> readOnlySpan = s.Trim();
				if (readOnlySpan.EqualsOrdinal(info.PositiveInfinitySymbol))
				{
					result = double.PositiveInfinity;
				}
				else if (readOnlySpan.EqualsOrdinal(info.NegativeInfinitySymbol))
				{
					result = double.NegativeInfinity;
				}
				else
				{
					if (!readOnlySpan.EqualsOrdinal(info.NaNSymbol))
					{
						return false;
					}
					result = double.NaN;
				}
			}
			return true;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00020036 File Offset: 0x0001E236
		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002003A File Offset: 0x0001E23A
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00020043 File Offset: 0x0001E243
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Double", "Char"));
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002005E File Offset: 0x0001E25E
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00020067 File Offset: 0x0001E267
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00020070 File Offset: 0x0001E270
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00020079 File Offset: 0x0001E279
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00020082 File Offset: 0x0001E282
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002008B File Offset: 0x0001E28B
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00020094 File Offset: 0x0001E294
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002009D File Offset: 0x0001E29D
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000200A6 File Offset: 0x0001E2A6
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000200AF File Offset: 0x0001E2AF
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000200B3 File Offset: 0x0001E2B3
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000200BC File Offset: 0x0001E2BC
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Double", "DateTime"));
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000200D7 File Offset: 0x0001E2D7
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000F5C RID: 3932
		private readonly double m_value;

		// Token: 0x04000F5D RID: 3933
		public const double MinValue = -1.7976931348623157E+308;

		// Token: 0x04000F5E RID: 3934
		public const double MaxValue = 1.7976931348623157E+308;

		// Token: 0x04000F5F RID: 3935
		public const double Epsilon = 5E-324;

		// Token: 0x04000F60 RID: 3936
		public const double NegativeInfinity = double.NegativeInfinity;

		// Token: 0x04000F61 RID: 3937
		public const double PositiveInfinity = double.PositiveInfinity;

		// Token: 0x04000F62 RID: 3938
		public const double NaN = double.NaN;

		// Token: 0x04000F63 RID: 3939
		internal const double NegativeZero = -0.0;
	}
}
