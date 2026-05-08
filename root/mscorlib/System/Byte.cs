using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	public readonly struct Byte : IComparable, IConvertible, IFormattable, IComparable<byte>, IEquatable<byte>, ISpanFormattable
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x00019C28 File Offset: 0x00017E28
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is byte))
			{
				throw new ArgumentException("Object must be of type Byte.");
			}
			return (int)(this - (byte)value);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00019C4B File Offset: 0x00017E4B
		public int CompareTo(byte value)
		{
			return (int)(this - value);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00019C51 File Offset: 0x00017E51
		public override bool Equals(object obj)
		{
			return obj is byte && this == (byte)obj;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000199E7 File Offset: 0x00017BE7
		[NonVersionable]
		public bool Equals(byte obj)
		{
			return this == obj;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00019B65 File Offset: 0x00017D65
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00019C67 File Offset: 0x00017E67
		public static byte Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00019C84 File Offset: 0x00017E84
		public static byte Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00019CA7 File Offset: 0x00017EA7
		public static byte Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00019CC5 File Offset: 0x00017EC5
		public static byte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00019CE9 File Offset: 0x00017EE9
		public static byte Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00019D00 File Offset: 0x00017F00
		private static byte Parse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.", ex);
			}
			if (num < 0 || num > 255)
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.");
			}
			return (byte)num;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00019D50 File Offset: 0x00017F50
		public static bool TryParse(string s, out byte result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return byte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00019D6C File Offset: 0x00017F6C
		public static bool TryParse(ReadOnlySpan<char> s, out byte result)
		{
			return byte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00019D7B File Offset: 0x00017F7B
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out byte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return byte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00019D9E File Offset: 0x00017F9E
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out byte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00019DB4 File Offset: 0x00017FB4
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out byte result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if (num < 0 || num > 255)
			{
				return false;
			}
			result = (byte)num;
			return true;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00019DE5 File Offset: 0x00017FE5
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, null);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00019DF5 File Offset: 0x00017FF5
		public string ToString(string format)
		{
			return Number.FormatInt32((int)this, format, null);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00019E05 File Offset: 0x00018005
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, provider);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00019E15 File Offset: 0x00018015
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, format, provider);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00019E25 File Offset: 0x00018025
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatInt32((int)this, format, provider, destination, out charsWritten);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00019E33 File Offset: 0x00018033
		public TypeCode GetTypeCode()
		{
			return TypeCode.Byte;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00019E36 File Offset: 0x00018036
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00019E3F File Offset: 0x0001803F
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00019E48 File Offset: 0x00018048
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00019B65 File Offset: 0x00017D65
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00019E51 File Offset: 0x00018051
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00019E5A File Offset: 0x0001805A
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00019E63 File Offset: 0x00018063
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00019E6C File Offset: 0x0001806C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00019E75 File Offset: 0x00018075
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00019E7E File Offset: 0x0001807E
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00019E87 File Offset: 0x00018087
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00019E90 File Offset: 0x00018090
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00019E99 File Offset: 0x00018099
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00019EA2 File Offset: 0x000180A2
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Byte", "DateTime"));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019EBD File Offset: 0x000180BD
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000EF7 RID: 3831
		private readonly byte m_value;

		// Token: 0x04000EF8 RID: 3832
		public const byte MaxValue = 255;

		// Token: 0x04000EF9 RID: 3833
		public const byte MinValue = 0;
	}
}
