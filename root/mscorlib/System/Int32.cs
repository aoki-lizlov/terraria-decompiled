using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public readonly struct Int32 : IComparable, IConvertible, IFormattable, IComparable<int>, IEquatable<int>, ISpanFormattable
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x00029CB0 File Offset: 0x00027EB0
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is int))
			{
				throw new ArgumentException("Object must be of type Int32.");
			}
			int num = (int)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00029CEB File Offset: 0x00027EEB
		public int CompareTo(int value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00029CFC File Offset: 0x00027EFC
		public override bool Equals(object obj)
		{
			return obj is int && this == (int)obj;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00029D12 File Offset: 0x00027F12
		[NonVersionable]
		public bool Equals(int obj)
		{
			return this == obj;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00029D19 File Offset: 0x00027F19
		public override int GetHashCode()
		{
			return this;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00029D1D File Offset: 0x00027F1D
		public override string ToString()
		{
			return Number.FormatInt32(this, null, null);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00029D2D File Offset: 0x00027F2D
		public string ToString(string format)
		{
			return Number.FormatInt32(this, format, null);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00029D3D File Offset: 0x00027F3D
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32(this, null, provider);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00029D4D File Offset: 0x00027F4D
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32(this, format, provider);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00029D5D File Offset: 0x00027F5D
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatInt32(this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00029D6B File Offset: 0x00027F6B
		public static int Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00029D88 File Offset: 0x00027F88
		public static int Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00029DAB File Offset: 0x00027FAB
		public static int Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00029DC9 File Offset: 0x00027FC9
		public static int Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00029DED File Offset: 0x00027FED
		public static int Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00029E02 File Offset: 0x00028002
		public static bool TryParse(string s, out int result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return Number.TryParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00029E1E File Offset: 0x0002801E
		public static bool TryParse(ReadOnlySpan<char> s, out int result)
		{
			return Number.TryParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00029E2D File Offset: 0x0002802D
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out int result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00029E50 File Offset: 0x00028050
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out int result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00029E66 File Offset: 0x00028066
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00029E6A File Offset: 0x0002806A
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00029E73 File Offset: 0x00028073
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00029E7C File Offset: 0x0002807C
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00029E85 File Offset: 0x00028085
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00029E8E File Offset: 0x0002808E
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00029E97 File Offset: 0x00028097
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00029D19 File Offset: 0x00027F19
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00029EA0 File Offset: 0x000280A0
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00029EA9 File Offset: 0x000280A9
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00029EB2 File Offset: 0x000280B2
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00029EBB File Offset: 0x000280BB
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00029EC4 File Offset: 0x000280C4
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00029ECD File Offset: 0x000280CD
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00029ED6 File Offset: 0x000280D6
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Int32", "DateTime"));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00029EF1 File Offset: 0x000280F1
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040010D8 RID: 4312
		private readonly int m_value;

		// Token: 0x040010D9 RID: 4313
		public const int MaxValue = 2147483647;

		// Token: 0x040010DA RID: 4314
		public const int MinValue = -2147483648;
	}
}
