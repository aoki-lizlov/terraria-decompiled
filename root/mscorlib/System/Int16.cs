using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public readonly struct Int16 : IComparable, IConvertible, IFormattable, IComparable<short>, IEquatable<short>, ISpanFormattable
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x00029923 File Offset: 0x00027B23
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is short)
			{
				return (int)(this - (short)value);
			}
			throw new ArgumentException("Object must be of type Int16.");
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00029946 File Offset: 0x00027B46
		public int CompareTo(short value)
		{
			return (int)(this - value);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002994C File Offset: 0x00027B4C
		public override bool Equals(object obj)
		{
			return obj is short && this == (short)obj;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00029962 File Offset: 0x00027B62
		[NonVersionable]
		public bool Equals(short obj)
		{
			return this == obj;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00029969 File Offset: 0x00027B69
		public override int GetHashCode()
		{
			return (int)((ushort)this) | ((int)this << 16);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00029974 File Offset: 0x00027B74
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, null);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00029984 File Offset: 0x00027B84
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, provider);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00029994 File Offset: 0x00027B94
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000299A0 File Offset: 0x00027BA0
		public string ToString(string format, IFormatProvider provider)
		{
			if (this < 0 && format != null && format.Length > 0 && (format[0] == 'X' || format[0] == 'x'))
			{
				return Number.FormatUInt32((uint)this & 65535U, format, provider);
			}
			return Number.FormatInt32((int)this, format, provider);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000299F8 File Offset: 0x00027BF8
		public unsafe bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			if (this < 0 && format.Length > 0 && (*format[0] == 88 || *format[0] == 120))
			{
				return Number.TryFormatUInt32((uint)this & 65535U, format, provider, destination, out charsWritten);
			}
			return Number.TryFormatInt32((int)this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00029A4D File Offset: 0x00027C4D
		public static short Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00029A6A File Offset: 0x00027C6A
		public static short Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return short.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00029A8D File Offset: 0x00027C8D
		public static short Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00029AAB File Offset: 0x00027CAB
		public static short Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return short.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00029ACF File Offset: 0x00027CCF
		public static short Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return short.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00029AE4 File Offset: 0x00027CE4
		private static short Parse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for an Int16.", ex);
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 65535)
				{
					throw new OverflowException("Value was either too large or too small for an Int16.");
				}
				return (short)num;
			}
			else
			{
				if (num < -32768 || num > 32767)
				{
					throw new OverflowException("Value was either too large or too small for an Int16.");
				}
				return (short)num;
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00029B5C File Offset: 0x00027D5C
		public static bool TryParse(string s, out short result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00029B78 File Offset: 0x00027D78
		public static bool TryParse(ReadOnlySpan<char> s, out short result)
		{
			return short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00029B87 File Offset: 0x00027D87
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out short result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return short.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00029BAA File Offset: 0x00027DAA
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out short result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return short.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00029BC0 File Offset: 0x00027DC0
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out short result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 65535)
				{
					return false;
				}
				result = (short)num;
				return true;
			}
			else
			{
				if (num < -32768 || num > 32767)
				{
					return false;
				}
				result = (short)num;
				return true;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00029C12 File Offset: 0x00027E12
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int16;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00029C15 File Offset: 0x00027E15
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00029C1E File Offset: 0x00027E1E
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00029C27 File Offset: 0x00027E27
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00029C30 File Offset: 0x00027E30
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00029C39 File Offset: 0x00027E39
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00029C3D File Offset: 0x00027E3D
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00029C46 File Offset: 0x00027E46
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00029C4F File Offset: 0x00027E4F
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00029C58 File Offset: 0x00027E58
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00029C61 File Offset: 0x00027E61
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00029C6A File Offset: 0x00027E6A
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00029C73 File Offset: 0x00027E73
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00029C7C File Offset: 0x00027E7C
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00029C85 File Offset: 0x00027E85
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Int16", "DateTime"));
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00029CA0 File Offset: 0x00027EA0
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040010D5 RID: 4309
		private readonly short m_value;

		// Token: 0x040010D6 RID: 4310
		public const short MaxValue = 32767;

		// Token: 0x040010D7 RID: 4311
		public const short MinValue = -32768;
	}
}
