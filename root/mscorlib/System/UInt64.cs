using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x0200017F RID: 383
	[CLSCompliant(false)]
	[Serializable]
	public readonly struct UInt64 : IComparable, IConvertible, IFormattable, IComparable<ulong>, IEquatable<ulong>, ISpanFormattable
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x00048D90 File Offset: 0x00046F90
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is ulong))
			{
				throw new ArgumentException("Object must be of type UInt64.");
			}
			ulong num = (ulong)value;
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

		// Token: 0x0600122A RID: 4650 RVA: 0x00048DCB File Offset: 0x00046FCB
		public int CompareTo(ulong value)
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

		// Token: 0x0600122B RID: 4651 RVA: 0x00048DDC File Offset: 0x00046FDC
		public override bool Equals(object obj)
		{
			return obj is ulong && this == (ulong)obj;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00029F66 File Offset: 0x00028166
		[NonVersionable]
		public bool Equals(ulong obj)
		{
			return this == obj;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00048DF2 File Offset: 0x00046FF2
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00048DFE File Offset: 0x00046FFE
		public override string ToString()
		{
			return Number.FormatUInt64(this, null, null);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00048E0E File Offset: 0x0004700E
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt64(this, null, provider);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00048E1E File Offset: 0x0004701E
		public string ToString(string format)
		{
			return Number.FormatUInt64(this, format, null);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00048E2E File Offset: 0x0004702E
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt64(this, format, provider);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00048E3E File Offset: 0x0004703E
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatUInt64(this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00048E4C File Offset: 0x0004704C
		[CLSCompliant(false)]
		public static ulong Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00048E69 File Offset: 0x00047069
		[CLSCompliant(false)]
		public static ulong Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00048E8C File Offset: 0x0004708C
		[CLSCompliant(false)]
		public static ulong Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00048EAA File Offset: 0x000470AA
		[CLSCompliant(false)]
		public static ulong Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00048ECE File Offset: 0x000470CE
		[CLSCompliant(false)]
		public static ulong Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00048EE3 File Offset: 0x000470E3
		[CLSCompliant(false)]
		public static bool TryParse(string s, out ulong result)
		{
			if (s == null)
			{
				result = 0UL;
				return false;
			}
			return Number.TryParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00048F00 File Offset: 0x00047100
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<char> s, out ulong result)
		{
			return Number.TryParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00048F0F File Offset: 0x0004710F
		[CLSCompliant(false)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ulong result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0UL;
				return false;
			}
			return Number.TryParseUInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00048F33 File Offset: 0x00047133
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out ulong result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseUInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00048F49 File Offset: 0x00047149
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt64;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00048F4D File Offset: 0x0004714D
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00048F56 File Offset: 0x00047156
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00048F5F File Offset: 0x0004715F
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00048F68 File Offset: 0x00047168
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00048F71 File Offset: 0x00047171
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00048F7A File Offset: 0x0004717A
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00048F83 File Offset: 0x00047183
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00048F8C File Offset: 0x0004718C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00048F95 File Offset: 0x00047195
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0002A110 File Offset: 0x00028310
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00048F9E File Offset: 0x0004719E
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00048FA7 File Offset: 0x000471A7
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00048FB0 File Offset: 0x000471B0
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00048FB9 File Offset: 0x000471B9
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "UInt64", "DateTime"));
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00048FD4 File Offset: 0x000471D4
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04001249 RID: 4681
		private readonly ulong m_value;

		// Token: 0x0400124A RID: 4682
		public const ulong MaxValue = 18446744073709551615UL;

		// Token: 0x0400124B RID: 4683
		public const ulong MinValue = 0UL;
	}
}
