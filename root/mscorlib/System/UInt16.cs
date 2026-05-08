using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x0200017D RID: 381
	[CLSCompliant(false)]
	[Serializable]
	public readonly struct UInt16 : IComparable, IConvertible, IFormattable, IComparable<ushort>, IEquatable<ushort>, ISpanFormattable
	{
		// Token: 0x060011E1 RID: 4577 RVA: 0x000488A4 File Offset: 0x00046AA4
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is ushort)
			{
				return (int)(this - (ushort)value);
			}
			throw new ArgumentException("Object must be of type UInt16.");
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00019F4E File Offset: 0x0001814E
		public int CompareTo(ushort value)
		{
			return (int)(this - value);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000488C7 File Offset: 0x00046AC7
		public override bool Equals(object obj)
		{
			return obj is ushort && this == (ushort)obj;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00019F24 File Offset: 0x00018124
		[NonVersionable]
		public bool Equals(ushort obj)
		{
			return this == obj;
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0001A1B5 File Offset: 0x000183B5
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x000488DD File Offset: 0x00046ADD
		public override string ToString()
		{
			return Number.FormatUInt32((uint)this, null, null);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x000488ED File Offset: 0x00046AED
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, null, provider);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000488FD File Offset: 0x00046AFD
		public string ToString(string format)
		{
			return Number.FormatUInt32((uint)this, format, null);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0004890D File Offset: 0x00046B0D
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, format, provider);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0004891D File Offset: 0x00046B1D
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatUInt32((uint)this, format, provider, destination, out charsWritten);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0004892B File Offset: 0x00046B2B
		[CLSCompliant(false)]
		public static ushort Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00048948 File Offset: 0x00046B48
		[CLSCompliant(false)]
		public static ushort Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return ushort.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0004896B File Offset: 0x00046B6B
		[CLSCompliant(false)]
		public static ushort Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00048989 File Offset: 0x00046B89
		[CLSCompliant(false)]
		public static ushort Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return ushort.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000489AD File Offset: 0x00046BAD
		[CLSCompliant(false)]
		public static ushort Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000489C4 File Offset: 0x00046BC4
		private static ushort Parse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info)
		{
			uint num = 0U;
			try
			{
				num = Number.ParseUInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for a UInt16.", ex);
			}
			if (num > 65535U)
			{
				throw new OverflowException("Value was either too large or too small for a UInt16.");
			}
			return (ushort)num;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00048A10 File Offset: 0x00046C10
		[CLSCompliant(false)]
		public static bool TryParse(string s, out ushort result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return ushort.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00048A2C File Offset: 0x00046C2C
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<char> s, out ushort result)
		{
			return ushort.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00048A3B File Offset: 0x00046C3B
		[CLSCompliant(false)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ushort result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return ushort.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00048A5E File Offset: 0x00046C5E
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out ushort result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00048A74 File Offset: 0x00046C74
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out ushort result)
		{
			result = 0;
			uint num;
			if (!Number.TryParseUInt32(s, style, info, out num))
			{
				return false;
			}
			if (num > 65535U)
			{
				return false;
			}
			result = (ushort)num;
			return true;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00048AA1 File Offset: 0x00046CA1
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt16;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00048AA4 File Offset: 0x00046CA4
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00048AAD File Offset: 0x00046CAD
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00048AB6 File Offset: 0x00046CB6
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00048ABF File Offset: 0x00046CBF
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00048AC8 File Offset: 0x00046CC8
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0001A1B5 File Offset: 0x000183B5
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00048AD1 File Offset: 0x00046CD1
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00048ADA File Offset: 0x00046CDA
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00048AE3 File Offset: 0x00046CE3
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00048AEC File Offset: 0x00046CEC
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00048AF5 File Offset: 0x00046CF5
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00048AFE File Offset: 0x00046CFE
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00048B07 File Offset: 0x00046D07
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00048B10 File Offset: 0x00046D10
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "UInt16", "DateTime"));
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00048B2B File Offset: 0x00046D2B
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04001243 RID: 4675
		private readonly ushort m_value;

		// Token: 0x04001244 RID: 4676
		public const ushort MaxValue = 65535;

		// Token: 0x04001245 RID: 4677
		public const ushort MinValue = 0;
	}
}
