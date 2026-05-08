using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000143 RID: 323
	[Serializable]
	public readonly struct Single : IComparable, IConvertible, IFormattable, IComparable<float>, IEquatable<float>, ISpanFormattable
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x0003493D File Offset: 0x00032B3D
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFinite(float f)
		{
			return (BitConverter.SingleToInt32Bits(f) & int.MaxValue) < 2139095040;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00034952 File Offset: 0x00032B52
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInfinity(float f)
		{
			return (BitConverter.SingleToInt32Bits(f) & int.MaxValue) == 2139095040;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00034967 File Offset: 0x00032B67
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(float f)
		{
			return (BitConverter.SingleToInt32Bits(f) & int.MaxValue) > 2139095040;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0003497C File Offset: 0x00032B7C
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegative(float f)
		{
			return (BitConverter.SingleToInt32Bits(f) & int.MinValue) == int.MinValue;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00034991 File Offset: 0x00032B91
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegativeInfinity(float f)
		{
			return f == float.NegativeInfinity;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003499C File Offset: 0x00032B9C
		[NonVersionable]
		public static bool IsNormal(float f)
		{
			int num = BitConverter.SingleToInt32Bits(f);
			num &= int.MaxValue;
			return num < 2139095040 && num != 0 && (num & 2139095040) != 0;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000349CF File Offset: 0x00032BCF
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPositiveInfinity(float f)
		{
			return f == float.PositiveInfinity;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000349DC File Offset: 0x00032BDC
		[NonVersionable]
		public static bool IsSubnormal(float f)
		{
			int num = BitConverter.SingleToInt32Bits(f);
			num &= int.MaxValue;
			return num < 2139095040 && num != 0 && (num & 2139095040) == 0;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00034A10 File Offset: 0x00032C10
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is float))
			{
				throw new ArgumentException("Object must be of type Single.");
			}
			float num = (float)value;
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
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00034A67 File Offset: 0x00032C67
		public int CompareTo(float value)
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
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0001FDBA File Offset: 0x0001DFBA
		[NonVersionable]
		public static bool operator ==(float left, float right)
		{
			return left == right;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0001FDC0 File Offset: 0x0001DFC0
		[NonVersionable]
		public static bool operator !=(float left, float right)
		{
			return left != right;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0001FDC9 File Offset: 0x0001DFC9
		[NonVersionable]
		public static bool operator <(float left, float right)
		{
			return left < right;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0001FDCF File Offset: 0x0001DFCF
		[NonVersionable]
		public static bool operator >(float left, float right)
		{
			return left > right;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0001FDD5 File Offset: 0x0001DFD5
		[NonVersionable]
		public static bool operator <=(float left, float right)
		{
			return left <= right;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0001FDDE File Offset: 0x0001DFDE
		[NonVersionable]
		public static bool operator >=(float left, float right)
		{
			return left >= right;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00034A94 File Offset: 0x00032C94
		public override bool Equals(object obj)
		{
			if (!(obj is float))
			{
				return false;
			}
			float num = (float)obj;
			return num == this || (float.IsNaN(num) && float.IsNaN(this));
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00034ACA File Offset: 0x00032CCA
		public bool Equals(float obj)
		{
			return obj == this || (float.IsNaN(obj) && float.IsNaN(this));
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00034AE4 File Offset: 0x00032CE4
		public override int GetHashCode()
		{
			int num = BitConverter.SingleToInt32Bits(this);
			if (((num - 1) & 2147483647) >= 2139095040)
			{
				num &= 2139095040;
			}
			return num;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00034B12 File Offset: 0x00032D12
		public override string ToString()
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00034B21 File Offset: 0x00032D21
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00034B31 File Offset: 0x00032D31
		public string ToString(string format)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00034B40 File Offset: 0x00032D40
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00034B50 File Offset: 0x00032D50
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatSingle(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00034B63 File Offset: 0x00032D63
		public static float Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseSingle(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00034B84 File Offset: 0x00032D84
		public static float Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseSingle(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00034BA7 File Offset: 0x00032DA7
		public static float Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseSingle(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00034BC9 File Offset: 0x00032DC9
		public static float Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseSingle(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00034BED File Offset: 0x00032DED
		public static float Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseSingle(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00034C02 File Offset: 0x00032E02
		public static bool TryParse(string s, out float result)
		{
			if (s == null)
			{
				result = 0f;
				return false;
			}
			return float.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00034C26 File Offset: 0x00032E26
		public static bool TryParse(ReadOnlySpan<char> s, out float result)
		{
			return float.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00034C39 File Offset: 0x00032E39
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				result = 0f;
				return false;
			}
			return float.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00034C60 File Offset: 0x00032E60
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out float result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00034C78 File Offset: 0x00032E78
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out float result)
		{
			if (!Number.TryParseSingle(s, style, info, out result))
			{
				ReadOnlySpan<char> readOnlySpan = s.Trim();
				if (readOnlySpan.EqualsOrdinal(info.PositiveInfinitySymbol))
				{
					result = float.PositiveInfinity;
				}
				else if (readOnlySpan.EqualsOrdinal(info.NegativeInfinitySymbol))
				{
					result = float.NegativeInfinity;
				}
				else
				{
					if (!readOnlySpan.EqualsOrdinal(info.NaNSymbol))
					{
						return false;
					}
					result = float.NaN;
				}
			}
			return true;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00034CEE File Offset: 0x00032EEE
		public TypeCode GetTypeCode()
		{
			return TypeCode.Single;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00034CF2 File Offset: 0x00032EF2
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00034CFB File Offset: 0x00032EFB
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Single", "Char"));
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00034D16 File Offset: 0x00032F16
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00034D1F File Offset: 0x00032F1F
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00034D28 File Offset: 0x00032F28
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00034D31 File Offset: 0x00032F31
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00034D3A File Offset: 0x00032F3A
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00034D43 File Offset: 0x00032F43
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00034D4C File Offset: 0x00032F4C
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00034D55 File Offset: 0x00032F55
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00034D5E File Offset: 0x00032F5E
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00034D62 File Offset: 0x00032F62
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00034D6B File Offset: 0x00032F6B
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00034D74 File Offset: 0x00032F74
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Single", "DateTime"));
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00034D8F File Offset: 0x00032F8F
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0400115F RID: 4447
		private readonly float m_value;

		// Token: 0x04001160 RID: 4448
		public const float MinValue = -3.4028235E+38f;

		// Token: 0x04001161 RID: 4449
		public const float Epsilon = 1E-45f;

		// Token: 0x04001162 RID: 4450
		public const float MaxValue = 3.4028235E+38f;

		// Token: 0x04001163 RID: 4451
		public const float PositiveInfinity = float.PositiveInfinity;

		// Token: 0x04001164 RID: 4452
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x04001165 RID: 4453
		public const float NaN = float.NaN;

		// Token: 0x04001166 RID: 4454
		internal const float NegativeZero = -0f;
	}
}
