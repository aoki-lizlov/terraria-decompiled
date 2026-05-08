using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x0200017E RID: 382
	[CLSCompliant(false)]
	[Serializable]
	public readonly struct UInt32 : IComparable, IConvertible, IFormattable, IComparable<uint>, IEquatable<uint>, ISpanFormattable
	{
		// Token: 0x06001206 RID: 4614 RVA: 0x00048B3C File Offset: 0x00046D3C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is uint))
			{
				throw new ArgumentException("Object must be of type UInt32.");
			}
			uint num = (uint)value;
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

		// Token: 0x06001207 RID: 4615 RVA: 0x00048B77 File Offset: 0x00046D77
		public int CompareTo(uint value)
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

		// Token: 0x06001208 RID: 4616 RVA: 0x00048B88 File Offset: 0x00046D88
		public override bool Equals(object obj)
		{
			return obj is uint && this == (uint)obj;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00048B9E File Offset: 0x00046D9E
		[NonVersionable]
		public bool Equals(uint obj)
		{
			return this == obj;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00048BA5 File Offset: 0x00046DA5
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00048BA9 File Offset: 0x00046DA9
		public override string ToString()
		{
			return Number.FormatUInt32(this, null, null);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00048BB9 File Offset: 0x00046DB9
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt32(this, null, provider);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00048BC9 File Offset: 0x00046DC9
		public string ToString(string format)
		{
			return Number.FormatUInt32(this, format, null);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00048BD9 File Offset: 0x00046DD9
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt32(this, format, provider);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00048BE9 File Offset: 0x00046DE9
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatUInt32(this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00048BF7 File Offset: 0x00046DF7
		[CLSCompliant(false)]
		public static uint Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00048C14 File Offset: 0x00046E14
		[CLSCompliant(false)]
		public static uint Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt32(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00048C37 File Offset: 0x00046E37
		[CLSCompliant(false)]
		public static uint Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00048C55 File Offset: 0x00046E55
		[CLSCompliant(false)]
		public static uint Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00048C79 File Offset: 0x00046E79
		[CLSCompliant(false)]
		public static uint Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00048C8E File Offset: 0x00046E8E
		[CLSCompliant(false)]
		public static bool TryParse(string s, out uint result)
		{
			if (s == null)
			{
				result = 0U;
				return false;
			}
			return Number.TryParseUInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00048CAA File Offset: 0x00046EAA
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<char> s, out uint result)
		{
			return Number.TryParseUInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00048CB9 File Offset: 0x00046EB9
		[CLSCompliant(false)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out uint result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0U;
				return false;
			}
			return Number.TryParseUInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00048CDC File Offset: 0x00046EDC
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out uint result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseUInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00048CF2 File Offset: 0x00046EF2
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt32;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00048CF6 File Offset: 0x00046EF6
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00048CFF File Offset: 0x00046EFF
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00048D08 File Offset: 0x00046F08
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00048D11 File Offset: 0x00046F11
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00048D1A File Offset: 0x00046F1A
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00048D23 File Offset: 0x00046F23
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00048D2C File Offset: 0x00046F2C
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00048BA5 File Offset: 0x00046DA5
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00048D35 File Offset: 0x00046F35
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00048D3E File Offset: 0x00046F3E
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00048D47 File Offset: 0x00046F47
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00048D50 File Offset: 0x00046F50
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00048D59 File Offset: 0x00046F59
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00048D62 File Offset: 0x00046F62
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "UInt32", "DateTime"));
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00048D7D File Offset: 0x00046F7D
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04001246 RID: 4678
		private readonly uint m_value;

		// Token: 0x04001247 RID: 4679
		public const uint MaxValue = 4294967295U;

		// Token: 0x04001248 RID: 4680
		public const uint MinValue = 0U;
	}
}
