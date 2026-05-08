using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public readonly struct Int64 : IComparable, IConvertible, IFormattable, IComparable<long>, IEquatable<long>, ISpanFormattable
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x00029F04 File Offset: 0x00028104
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is long))
			{
				throw new ArgumentException("Object must be of type Int64.");
			}
			long num = (long)value;
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

		// Token: 0x06000A99 RID: 2713 RVA: 0x00029F3F File Offset: 0x0002813F
		public int CompareTo(long value)
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

		// Token: 0x06000A9A RID: 2714 RVA: 0x00029F50 File Offset: 0x00028150
		public override bool Equals(object obj)
		{
			return obj is long && this == (long)obj;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00029F66 File Offset: 0x00028166
		[NonVersionable]
		public bool Equals(long obj)
		{
			return this == obj;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00029F6D File Offset: 0x0002816D
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00029F79 File Offset: 0x00028179
		public override string ToString()
		{
			return Number.FormatInt64(this, null, null);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00029F89 File Offset: 0x00028189
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt64(this, null, provider);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00029F99 File Offset: 0x00028199
		public string ToString(string format)
		{
			return Number.FormatInt64(this, format, null);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00029FA9 File Offset: 0x000281A9
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt64(this, format, provider);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00029FB9 File Offset: 0x000281B9
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatInt64(this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00029FC7 File Offset: 0x000281C7
		public static long Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00029FE4 File Offset: 0x000281E4
		public static long Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002A007 File Offset: 0x00028207
		public static long Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002A025 File Offset: 0x00028225
		public static long Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002A049 File Offset: 0x00028249
		public static long Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002A05E File Offset: 0x0002825E
		public static bool TryParse(string s, out long result)
		{
			if (s == null)
			{
				result = 0L;
				return false;
			}
			return Number.TryParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002A07B File Offset: 0x0002827B
		public static bool TryParse(ReadOnlySpan<char> s, out long result)
		{
			return Number.TryParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002A08A File Offset: 0x0002828A
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out long result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0L;
				return false;
			}
			return Number.TryParseInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002A0AE File Offset: 0x000282AE
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out long result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002A0C4 File Offset: 0x000282C4
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int64;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002A0C8 File Offset: 0x000282C8
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002A0D1 File Offset: 0x000282D1
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002A0DA File Offset: 0x000282DA
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002A0E3 File Offset: 0x000282E3
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002A0EC File Offset: 0x000282EC
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002A0F5 File Offset: 0x000282F5
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002A0FE File Offset: 0x000282FE
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002A107 File Offset: 0x00028307
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002A110 File Offset: 0x00028310
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002A114 File Offset: 0x00028314
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002A11D File Offset: 0x0002831D
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002A126 File Offset: 0x00028326
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002A12F File Offset: 0x0002832F
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002A138 File Offset: 0x00028338
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Int64", "DateTime"));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002A153 File Offset: 0x00028353
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040010DB RID: 4315
		private readonly long m_value;

		// Token: 0x040010DC RID: 4316
		public const long MaxValue = 9223372036854775807L;

		// Token: 0x040010DD RID: 4317
		public const long MinValue = -9223372036854775808L;
	}
}
