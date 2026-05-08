using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000141 RID: 321
	[CLSCompliant(false)]
	[Serializable]
	public readonly struct SByte : IComparable, IConvertible, IFormattable, IComparable<sbyte>, IEquatable<sbyte>, ISpanFormattable
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x000345AA File Offset: 0x000327AA
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is sbyte))
			{
				throw new ArgumentException("Object must be of type SByte.");
			}
			return (int)(this - (sbyte)obj);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x000345CD File Offset: 0x000327CD
		public int CompareTo(sbyte value)
		{
			return (int)(this - value);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000345D3 File Offset: 0x000327D3
		public override bool Equals(object obj)
		{
			return obj is sbyte && this == (sbyte)obj;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000345E9 File Offset: 0x000327E9
		[NonVersionable]
		public bool Equals(sbyte obj)
		{
			return this == obj;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000345F0 File Offset: 0x000327F0
		public override int GetHashCode()
		{
			return (int)this ^ ((int)this << 8);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000345F9 File Offset: 0x000327F9
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, null);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00034609 File Offset: 0x00032809
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, provider);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00034619 File Offset: 0x00032819
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00034624 File Offset: 0x00032824
		public string ToString(string format, IFormatProvider provider)
		{
			if (this < 0 && format != null && format.Length > 0 && (format[0] == 'X' || format[0] == 'x'))
			{
				return Number.FormatUInt32((uint)this & 255U, format, provider);
			}
			return Number.FormatInt32((int)this, format, provider);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0003467C File Offset: 0x0003287C
		public unsafe bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			if (this < 0 && format.Length > 0 && (*format[0] == 88 || *format[0] == 120))
			{
				return Number.TryFormatUInt32((uint)this & 255U, format, provider, destination, out charsWritten);
			}
			return Number.TryFormatInt32((int)this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000346D1 File Offset: 0x000328D1
		[CLSCompliant(false)]
		public static sbyte Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000346EE File Offset: 0x000328EE
		[CLSCompliant(false)]
		public static sbyte Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return sbyte.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00034711 File Offset: 0x00032911
		[CLSCompliant(false)]
		public static sbyte Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003472F File Offset: 0x0003292F
		[CLSCompliant(false)]
		public static sbyte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return sbyte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00034753 File Offset: 0x00032953
		[CLSCompliant(false)]
		public static sbyte Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00034768 File Offset: 0x00032968
		private static sbyte Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return sbyte.Parse(s, style, info);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00034784 File Offset: 0x00032984
		private static sbyte Parse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for a signed byte.", ex);
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 255)
				{
					throw new OverflowException("Value was either too large or too small for a signed byte.");
				}
				return (sbyte)num;
			}
			else
			{
				if (num < -128 || num > 127)
				{
					throw new OverflowException("Value was either too large or too small for a signed byte.");
				}
				return (sbyte)num;
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000347F8 File Offset: 0x000329F8
		[CLSCompliant(false)]
		public static bool TryParse(string s, out sbyte result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00034814 File Offset: 0x00032A14
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<char> s, out sbyte result)
		{
			return sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00034823 File Offset: 0x00032A23
		[CLSCompliant(false)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out sbyte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return sbyte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00034846 File Offset: 0x00032A46
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out sbyte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003485C File Offset: 0x00032A5C
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out sbyte result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 255)
				{
					return false;
				}
				result = (sbyte)num;
				return true;
			}
			else
			{
				if (num < -128 || num > 127)
				{
					return false;
				}
				result = (sbyte)num;
				return true;
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000348A8 File Offset: 0x00032AA8
		public TypeCode GetTypeCode()
		{
			return TypeCode.SByte;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000348AB File Offset: 0x00032AAB
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000348B4 File Offset: 0x00032AB4
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000348BD File Offset: 0x00032ABD
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000348C1 File Offset: 0x00032AC1
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000348CA File Offset: 0x00032ACA
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000348D3 File Offset: 0x00032AD3
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x000348BD File Offset: 0x00032ABD
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000348DC File Offset: 0x00032ADC
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000348E5 File Offset: 0x00032AE5
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000348EE File Offset: 0x00032AEE
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000348F7 File Offset: 0x00032AF7
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00034900 File Offset: 0x00032B00
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00034909 File Offset: 0x00032B09
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00034912 File Offset: 0x00032B12
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "SByte", "DateTime"));
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003492D File Offset: 0x00032B2D
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0400115C RID: 4444
		private readonly sbyte m_value;

		// Token: 0x0400115D RID: 4445
		public const sbyte MaxValue = 127;

		// Token: 0x0400115E RID: 4446
		public const sbyte MinValue = -128;
	}
}
