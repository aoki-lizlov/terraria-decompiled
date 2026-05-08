using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200024B RID: 587
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public readonly struct Decimal : IFormattable, IComparable, IConvertible, IComparable<decimal>, IEquatable<decimal>, IDeserializationCallback, ISpanFormattable
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0006AF38 File Offset: 0x00069138
		internal uint High
		{
			get
			{
				return (uint)this.hi;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0006AF40 File Offset: 0x00069140
		internal uint Low
		{
			get
			{
				return (uint)this.lo;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0006AF48 File Offset: 0x00069148
		internal uint Mid
		{
			get
			{
				return (uint)this.mid;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0006AF50 File Offset: 0x00069150
		internal bool IsNegative
		{
			get
			{
				return this.flags < 0;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0006AF5B File Offset: 0x0006915B
		internal int Scale
		{
			get
			{
				return (int)((byte)(this.flags >> 16));
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x0006AF67 File Offset: 0x00069167
		private ulong Low64
		{
			get
			{
				if (!BitConverter.IsLittleEndian)
				{
					return ((ulong)this.Mid << 32) | (ulong)this.Low;
				}
				return this.ulomidLE;
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0006AF89 File Offset: 0x00069189
		private static ref decimal.DecCalc AsMutable(ref decimal d)
		{
			return Unsafe.As<decimal, decimal.DecCalc>(ref d);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0006AF91 File Offset: 0x00069191
		internal static uint DecDivMod1E9(ref decimal value)
		{
			return decimal.DecCalc.DecDivMod1E9(decimal.AsMutable(ref value));
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0006AF9E File Offset: 0x0006919E
		public Decimal(int value)
		{
			if (value >= 0)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				value = -value;
			}
			this.lo = value;
			this.mid = 0;
			this.hi = 0;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0006AFD1 File Offset: 0x000691D1
		[CLSCompliant(false)]
		public Decimal(uint value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = 0;
			this.hi = 0;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0006AFEF File Offset: 0x000691EF
		public Decimal(long value)
		{
			if (value >= 0L)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				value = -value;
			}
			this.lo = (int)value;
			this.mid = (int)(value >> 32);
			this.hi = 0;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0006B028 File Offset: 0x00069228
		[CLSCompliant(false)]
		public Decimal(ulong value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = (int)(value >> 32);
			this.hi = 0;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0006B04B File Offset: 0x0006924B
		public Decimal(float value)
		{
			decimal.DecCalc.VarDecFromR4(value, decimal.AsMutable(ref this));
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0006B059 File Offset: 0x00069259
		public Decimal(double value)
		{
			decimal.DecCalc.VarDecFromR8(value, decimal.AsMutable(ref this));
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0006B068 File Offset: 0x00069268
		public static decimal FromOACurrency(long cy)
		{
			bool flag = false;
			ulong num;
			if (cy < 0L)
			{
				flag = true;
				num = (ulong)(-(ulong)cy);
			}
			else
			{
				num = (ulong)cy;
			}
			int num2 = 4;
			if (num != 0UL)
			{
				while (num2 != 0 && num % 10UL == 0UL)
				{
					num2--;
					num /= 10UL;
				}
			}
			return new decimal((int)num, (int)(num >> 32), 0, flag, (byte)num2);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0006B0B0 File Offset: 0x000692B0
		public static long ToOACurrency(decimal value)
		{
			return decimal.DecCalc.VarCyFromDec(decimal.AsMutable(ref value));
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0006B0BE File Offset: 0x000692BE
		private static bool IsValid(int flags)
		{
			return (flags & 2130771967) == 0 && (flags & 16711680) <= 1835008;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0006B0DC File Offset: 0x000692DC
		public Decimal(int[] bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			if (bits.Length == 4)
			{
				int num = bits[3];
				if (decimal.IsValid(num))
				{
					this.lo = bits[0];
					this.mid = bits[1];
					this.hi = bits[2];
					this.flags = num;
					return;
				}
			}
			throw new ArgumentException("Decimal byte array constructor requires an array of length four containing valid decimal bytes.");
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0006B138 File Offset: 0x00069338
		public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
		{
			if (scale > 28)
			{
				throw new ArgumentOutOfRangeException("scale", "Decimal's scale value must be between 0 and 28, inclusive.");
			}
			this.lo = lo;
			this.mid = mid;
			this.hi = hi;
			this.flags = (int)scale << 16;
			if (isNegative)
			{
				this.flags |= int.MinValue;
			}
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0006B191 File Offset: 0x00069391
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			if (!decimal.IsValid(this.flags))
			{
				throw new SerializationException("Value was either too large or too small for a Decimal.");
			}
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0006B1AB File Offset: 0x000693AB
		private Decimal(int lo, int mid, int hi, int flags)
		{
			if (decimal.IsValid(flags))
			{
				this.lo = lo;
				this.mid = mid;
				this.hi = hi;
				this.flags = flags;
				return;
			}
			throw new ArgumentException("Decimal byte array constructor requires an array of length four containing valid decimal bytes.");
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0006B1DE File Offset: 0x000693DE
		private Decimal(in decimal d, int flags)
		{
			this = d;
			this.flags = flags;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0006B1F3 File Offset: 0x000693F3
		internal static decimal Abs(ref decimal d)
		{
			return new decimal(in d, d.flags & int.MaxValue);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0006B207 File Offset: 0x00069407
		public static decimal Add(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), false);
			return d1;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0006B220 File Offset: 0x00069420
		public static decimal Ceiling(decimal d)
		{
			int num = d.flags;
			if ((num & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(num >> 16)), decimal.DecCalc.RoundingMode.Ceiling);
			}
			return d;
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0006B250 File Offset: 0x00069450
		public static int Compare(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0006B25C File Offset: 0x0006945C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is decimal))
			{
				throw new ArgumentException("Object must be of type Decimal.");
			}
			decimal num = (decimal)value;
			return decimal.DecCalc.VarDecCmp(in this, in num);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0006B290 File Offset: 0x00069490
		public int CompareTo(decimal value)
		{
			return decimal.DecCalc.VarDecCmp(in this, in value);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0006B29A File Offset: 0x0006949A
		public static decimal Divide(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecDiv(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0006B2B0 File Offset: 0x000694B0
		public override bool Equals(object value)
		{
			if (value is decimal)
			{
				decimal num = (decimal)value;
				return decimal.DecCalc.VarDecCmp(in this, in num) == 0;
			}
			return false;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0006B2D9 File Offset: 0x000694D9
		public bool Equals(decimal value)
		{
			return decimal.DecCalc.VarDecCmp(in this, in value) == 0;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0006B2E6 File Offset: 0x000694E6
		public override int GetHashCode()
		{
			return decimal.DecCalc.GetHashCode(in this);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0006B2EE File Offset: 0x000694EE
		public static bool Equals(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) == 0;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0006B2FC File Offset: 0x000694FC
		public static decimal Floor(decimal d)
		{
			int num = d.flags;
			if ((num & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(num >> 16)), decimal.DecCalc.RoundingMode.Floor);
			}
			return d;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0006B32C File Offset: 0x0006952C
		public override string ToString()
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0006B344 File Offset: 0x00069544
		public string ToString(string format)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0006B35C File Offset: 0x0006955C
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0006B375 File Offset: 0x00069575
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0006B38E File Offset: 0x0006958E
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatDecimal(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0006B3A5 File Offset: 0x000695A5
		public static decimal Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0006B3C3 File Offset: 0x000695C3
		public static decimal Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0006B3E6 File Offset: 0x000695E6
		public static decimal Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0006B405 File Offset: 0x00069605
		public static decimal Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0006B429 File Offset: 0x00069629
		public static decimal Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Number, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0006B43E File Offset: 0x0006963E
		public static bool TryParse(string s, out decimal result)
		{
			if (s == null)
			{
				result = 0m;
				return false;
			}
			return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0006B45F File Offset: 0x0006965F
		public static bool TryParse(ReadOnlySpan<char> s, out decimal result)
		{
			return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0006B46F File Offset: 0x0006966F
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out decimal result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				result = 0m;
				return false;
			}
			return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0006B496 File Offset: 0x00069696
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out decimal result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0006B4AC File Offset: 0x000696AC
		public static int[] GetBits(decimal d)
		{
			return new int[] { d.lo, d.mid, d.hi, d.flags };
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0006B4D8 File Offset: 0x000696D8
		internal static void GetBytes(in decimal d, byte[] buffer)
		{
			buffer[0] = (byte)d.lo;
			buffer[1] = (byte)(d.lo >> 8);
			buffer[2] = (byte)(d.lo >> 16);
			buffer[3] = (byte)(d.lo >> 24);
			buffer[4] = (byte)d.mid;
			buffer[5] = (byte)(d.mid >> 8);
			buffer[6] = (byte)(d.mid >> 16);
			buffer[7] = (byte)(d.mid >> 24);
			buffer[8] = (byte)d.hi;
			buffer[9] = (byte)(d.hi >> 8);
			buffer[10] = (byte)(d.hi >> 16);
			buffer[11] = (byte)(d.hi >> 24);
			buffer[12] = (byte)d.flags;
			buffer[13] = (byte)(d.flags >> 8);
			buffer[14] = (byte)(d.flags >> 16);
			buffer[15] = (byte)(d.flags >> 24);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0006B5AC File Offset: 0x000697AC
		internal static decimal ToDecimal(byte[] buffer)
		{
			int num = (int)buffer[0] | ((int)buffer[1] << 8) | ((int)buffer[2] << 16) | ((int)buffer[3] << 24);
			int num2 = (int)buffer[4] | ((int)buffer[5] << 8) | ((int)buffer[6] << 16) | ((int)buffer[7] << 24);
			int num3 = (int)buffer[8] | ((int)buffer[9] << 8) | ((int)buffer[10] << 16) | ((int)buffer[11] << 24);
			int num4 = (int)buffer[12] | ((int)buffer[13] << 8) | ((int)buffer[14] << 16) | ((int)buffer[15] << 24);
			return new decimal(num, num2, num3, num4);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0006B627 File Offset: 0x00069827
		internal static readonly ref decimal Max(ref decimal d1, ref decimal d2)
		{
			if (decimal.DecCalc.VarDecCmp(in d1, in d2) < 0)
			{
				return ref d2;
			}
			return ref d1;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0006B636 File Offset: 0x00069836
		internal static readonly ref decimal Min(ref decimal d1, ref decimal d2)
		{
			if (decimal.DecCalc.VarDecCmp(in d1, in d2) >= 0)
			{
				return ref d2;
			}
			return ref d1;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0006B645 File Offset: 0x00069845
		public static decimal Remainder(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMod(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0006B65B File Offset: 0x0006985B
		public static decimal Multiply(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMul(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0006B671 File Offset: 0x00069871
		public static decimal Negate(decimal d)
		{
			return new decimal(in d, d.flags ^ int.MinValue);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0006B686 File Offset: 0x00069886
		public static decimal Round(decimal d)
		{
			return decimal.Round(ref d, 0, MidpointRounding.ToEven);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0006B691 File Offset: 0x00069891
		public static decimal Round(decimal d, int decimals)
		{
			return decimal.Round(ref d, decimals, MidpointRounding.ToEven);
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0006B69C File Offset: 0x0006989C
		public static decimal Round(decimal d, MidpointRounding mode)
		{
			return decimal.Round(ref d, 0, mode);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0006B6A7 File Offset: 0x000698A7
		public static decimal Round(decimal d, int decimals, MidpointRounding mode)
		{
			return decimal.Round(ref d, decimals, mode);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0006B6B4 File Offset: 0x000698B4
		private static decimal Round(ref decimal d, int decimals, MidpointRounding mode)
		{
			if (decimals > 28)
			{
				throw new ArgumentOutOfRangeException("decimals", "Decimal can only round to between 0 and 28 digits of precision.");
			}
			if (mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not valid for this usage of the type {1}.", mode, "MidpointRounding"), "mode");
			}
			int num = d.Scale - decimals;
			if (num > 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)num, (decimal.DecCalc.RoundingMode)mode);
			}
			return d;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0006B71A File Offset: 0x0006991A
		internal static int Sign(ref decimal d)
		{
			if ((d.lo | d.mid | d.hi) != 0)
			{
				return (d.flags >> 31) | 1;
			}
			return 0;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0006B73F File Offset: 0x0006993F
		public static decimal Subtract(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), true);
			return d1;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0006B758 File Offset: 0x00069958
		public static byte ToByte(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.", ex);
			}
			if (num != (uint)((byte)num))
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.");
			}
			return (byte)num;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0006B7A0 File Offset: 0x000699A0
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for a signed byte.", ex);
			}
			if (num != (int)((sbyte)num))
			{
				throw new OverflowException("Value was either too large or too small for a signed byte.");
			}
			return (sbyte)num;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0006B7E8 File Offset: 0x000699E8
		public static short ToInt16(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for an Int16.", ex);
			}
			if (num != (int)((short)num))
			{
				throw new OverflowException("Value was either too large or too small for an Int16.");
			}
			return (short)num;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0006B830 File Offset: 0x00069A30
		public static double ToDouble(decimal d)
		{
			return decimal.DecCalc.VarR8FromDec(in d);
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0006B83C File Offset: 0x00069A3C
		public static int ToInt32(decimal d)
		{
			decimal.Truncate(ref d);
			if ((d.hi | d.mid) == 0)
			{
				int num = d.lo;
				if (!d.IsNegative)
				{
					if (num >= 0)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0)
					{
						return num;
					}
				}
			}
			throw new OverflowException("Value was either too large or too small for an Int32.");
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0006B888 File Offset: 0x00069A88
		public static long ToInt64(decimal d)
		{
			decimal.Truncate(ref d);
			if (d.hi == 0)
			{
				long num = (long)d.Low64;
				if (!d.IsNegative)
				{
					if (num >= 0L)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0L)
					{
						return num;
					}
				}
			}
			throw new OverflowException("Value was either too large or too small for an Int64.");
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0006B8D0 File Offset: 0x00069AD0
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for a UInt16.", ex);
			}
			if (num != (uint)((ushort)num))
			{
				throw new OverflowException("Value was either too large or too small for a UInt16.");
			}
			return (ushort)num;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0006B918 File Offset: 0x00069B18
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal d)
		{
			decimal.Truncate(ref d);
			if ((d.hi | d.mid) == 0)
			{
				uint low = d.Low;
				if (!d.IsNegative || low == 0U)
				{
					return low;
				}
			}
			throw new OverflowException("Value was either too large or too small for a UInt32.");
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0006B95C File Offset: 0x00069B5C
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal d)
		{
			decimal.Truncate(ref d);
			if (d.hi == 0)
			{
				ulong low = d.Low64;
				if (!d.IsNegative || low == 0UL)
				{
					return low;
				}
			}
			throw new OverflowException("Value was either too large or too small for a UInt64.");
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0006B998 File Offset: 0x00069B98
		public static float ToSingle(decimal d)
		{
			return decimal.DecCalc.VarR4FromDec(in d);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0006B9A1 File Offset: 0x00069BA1
		public static decimal Truncate(decimal d)
		{
			decimal.Truncate(ref d);
			return d;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0006B9AC File Offset: 0x00069BAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Truncate(ref decimal d)
		{
			int num = d.flags;
			if ((num & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(num >> 16)), decimal.DecCalc.RoundingMode.Truncate);
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0006B9DA File Offset: 0x00069BDA
		public static implicit operator decimal(byte value)
		{
			return new decimal((uint)value);
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0006B9E2 File Offset: 0x00069BE2
		[CLSCompliant(false)]
		public static implicit operator decimal(sbyte value)
		{
			return new decimal((int)value);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0006B9E2 File Offset: 0x00069BE2
		public static implicit operator decimal(short value)
		{
			return new decimal((int)value);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0006B9DA File Offset: 0x00069BDA
		[CLSCompliant(false)]
		public static implicit operator decimal(ushort value)
		{
			return new decimal((uint)value);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0006B9DA File Offset: 0x00069BDA
		public static implicit operator decimal(char value)
		{
			return new decimal((uint)value);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0006B9E2 File Offset: 0x00069BE2
		public static implicit operator decimal(int value)
		{
			return new decimal(value);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0006B9DA File Offset: 0x00069BDA
		[CLSCompliant(false)]
		public static implicit operator decimal(uint value)
		{
			return new decimal(value);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0006B9EA File Offset: 0x00069BEA
		public static implicit operator decimal(long value)
		{
			return new decimal(value);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0006B9F2 File Offset: 0x00069BF2
		[CLSCompliant(false)]
		public static implicit operator decimal(ulong value)
		{
			return new decimal(value);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0006B9FA File Offset: 0x00069BFA
		public static explicit operator decimal(float value)
		{
			return new decimal(value);
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0006BA02 File Offset: 0x00069C02
		public static explicit operator decimal(double value)
		{
			return new decimal(value);
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0006BA0A File Offset: 0x00069C0A
		public static explicit operator byte(decimal value)
		{
			return decimal.ToByte(value);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0006BA12 File Offset: 0x00069C12
		[CLSCompliant(false)]
		public static explicit operator sbyte(decimal value)
		{
			return decimal.ToSByte(value);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0006BA1C File Offset: 0x00069C1C
		public static explicit operator char(decimal value)
		{
			ushort num;
			try
			{
				num = decimal.ToUInt16(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for a character.", ex);
			}
			return (char)num;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0006BA50 File Offset: 0x00069C50
		public static explicit operator short(decimal value)
		{
			return decimal.ToInt16(value);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0006BA58 File Offset: 0x00069C58
		[CLSCompliant(false)]
		public static explicit operator ushort(decimal value)
		{
			return decimal.ToUInt16(value);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0006BA60 File Offset: 0x00069C60
		public static explicit operator int(decimal value)
		{
			return decimal.ToInt32(value);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0006BA68 File Offset: 0x00069C68
		[CLSCompliant(false)]
		public static explicit operator uint(decimal value)
		{
			return decimal.ToUInt32(value);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0006BA70 File Offset: 0x00069C70
		public static explicit operator long(decimal value)
		{
			return decimal.ToInt64(value);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0006BA78 File Offset: 0x00069C78
		[CLSCompliant(false)]
		public static explicit operator ulong(decimal value)
		{
			return decimal.ToUInt64(value);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0006BA80 File Offset: 0x00069C80
		public static explicit operator float(decimal value)
		{
			return decimal.ToSingle(value);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0006BA88 File Offset: 0x00069C88
		public static explicit operator double(decimal value)
		{
			return decimal.ToDouble(value);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x000025CE File Offset: 0x000007CE
		public static decimal operator +(decimal d)
		{
			return d;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0006B671 File Offset: 0x00069871
		public static decimal operator -(decimal d)
		{
			return new decimal(in d, d.flags ^ int.MinValue);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0006BA90 File Offset: 0x00069C90
		public static decimal operator ++(decimal d)
		{
			return decimal.Add(d, 1m);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0006BA9D File Offset: 0x00069C9D
		public static decimal operator --(decimal d)
		{
			return decimal.Subtract(d, 1m);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0006B207 File Offset: 0x00069407
		public static decimal operator +(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), false);
			return d1;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0006B73F File Offset: 0x0006993F
		public static decimal operator -(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), true);
			return d1;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0006B65B File Offset: 0x0006985B
		public static decimal operator *(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMul(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0006B29A File Offset: 0x0006949A
		public static decimal operator /(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecDiv(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0006B645 File Offset: 0x00069845
		public static decimal operator %(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMod(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0006B2EE File Offset: 0x000694EE
		public static bool operator ==(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) == 0;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0006BAAA File Offset: 0x00069CAA
		public static bool operator !=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) != 0;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0006BAB8 File Offset: 0x00069CB8
		public static bool operator <(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) < 0;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0006BAC6 File Offset: 0x00069CC6
		public static bool operator <=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) <= 0;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0006BAD7 File Offset: 0x00069CD7
		public static bool operator >(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) > 0;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0006BAE5 File Offset: 0x00069CE5
		public static bool operator >=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) >= 0;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0006BAF6 File Offset: 0x00069CF6
		public TypeCode GetTypeCode()
		{
			return TypeCode.Decimal;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0006BAFA File Offset: 0x00069CFA
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0006BB07 File Offset: 0x00069D07
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Decimal", "Char"));
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0006BB22 File Offset: 0x00069D22
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0006BB2F File Offset: 0x00069D2F
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0006BB3C File Offset: 0x00069D3C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0006BB49 File Offset: 0x00069D49
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0006BB56 File Offset: 0x00069D56
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0006BB63 File Offset: 0x00069D63
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0006BB70 File Offset: 0x00069D70
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0006BB7D File Offset: 0x00069D7D
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0006BB8A File Offset: 0x00069D8A
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0006BB97 File Offset: 0x00069D97
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0006BBA4 File Offset: 0x00069DA4
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0006BBAC File Offset: 0x00069DAC
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Decimal", "DateTime"));
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0006BBC7 File Offset: 0x00069DC7
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0006BBDC File Offset: 0x00069DDC
		// Note: this type is marked as 'beforefieldinit'.
		static Decimal()
		{
		}

		// Token: 0x040018C8 RID: 6344
		private const int SignMask = -2147483648;

		// Token: 0x040018C9 RID: 6345
		private const int ScaleMask = 16711680;

		// Token: 0x040018CA RID: 6346
		private const int ScaleShift = 16;

		// Token: 0x040018CB RID: 6347
		public const decimal Zero = 0m;

		// Token: 0x040018CC RID: 6348
		public const decimal One = 1m;

		// Token: 0x040018CD RID: 6349
		public const decimal MinusOne = -1m;

		// Token: 0x040018CE RID: 6350
		public const decimal MaxValue = 79228162514264337593543950335m;

		// Token: 0x040018CF RID: 6351
		public const decimal MinValue = -79228162514264337593543950335m;

		// Token: 0x040018D0 RID: 6352
		[FieldOffset(0)]
		private readonly int flags;

		// Token: 0x040018D1 RID: 6353
		[FieldOffset(4)]
		private readonly int hi;

		// Token: 0x040018D2 RID: 6354
		[FieldOffset(8)]
		private readonly int lo;

		// Token: 0x040018D3 RID: 6355
		[FieldOffset(12)]
		private readonly int mid;

		// Token: 0x040018D4 RID: 6356
		[NonSerialized]
		[FieldOffset(8)]
		private readonly ulong ulomidLE;

		// Token: 0x0200024C RID: 588
		[StructLayout(LayoutKind.Explicit)]
		private struct DecCalc
		{
			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0006BC28 File Offset: 0x00069E28
			// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x0006BC30 File Offset: 0x00069E30
			private uint High
			{
				get
				{
					return this.uhi;
				}
				set
				{
					this.uhi = value;
				}
			}

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x06001CEA RID: 7402 RVA: 0x0006BC39 File Offset: 0x00069E39
			// (set) Token: 0x06001CEB RID: 7403 RVA: 0x0006BC41 File Offset: 0x00069E41
			private uint Low
			{
				get
				{
					return this.ulo;
				}
				set
				{
					this.ulo = value;
				}
			}

			// Token: 0x17000362 RID: 866
			// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0006BC4A File Offset: 0x00069E4A
			// (set) Token: 0x06001CED RID: 7405 RVA: 0x0006BC52 File Offset: 0x00069E52
			private uint Mid
			{
				get
				{
					return this.umid;
				}
				set
				{
					this.umid = value;
				}
			}

			// Token: 0x17000363 RID: 867
			// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0006BC5B File Offset: 0x00069E5B
			private bool IsNegative
			{
				get
				{
					return this.uflags < 0U;
				}
			}

			// Token: 0x17000364 RID: 868
			// (get) Token: 0x06001CEF RID: 7407 RVA: 0x0006BC66 File Offset: 0x00069E66
			private int Scale
			{
				get
				{
					return (int)((byte)(this.uflags >> 16));
				}
			}

			// Token: 0x17000365 RID: 869
			// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0006BC72 File Offset: 0x00069E72
			// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0006BC94 File Offset: 0x00069E94
			private ulong Low64
			{
				get
				{
					if (!BitConverter.IsLittleEndian)
					{
						return ((ulong)this.umid << 32) | (ulong)this.ulo;
					}
					return this.ulomidLE;
				}
				set
				{
					if (BitConverter.IsLittleEndian)
					{
						this.ulomidLE = value;
						return;
					}
					this.umid = (uint)(value >> 32);
					this.ulo = (uint)value;
				}
			}

			// Token: 0x06001CF2 RID: 7410 RVA: 0x0006BCB8 File Offset: 0x00069EB8
			private unsafe static uint GetExponent(float f)
			{
				return (uint)((byte)(*(uint*)(&f) >> 23));
			}

			// Token: 0x06001CF3 RID: 7411 RVA: 0x0006BCC2 File Offset: 0x00069EC2
			private unsafe static uint GetExponent(double d)
			{
				return (uint)((ulong)(*(long*)(&d)) >> 52) & 2047U;
			}

			// Token: 0x06001CF4 RID: 7412 RVA: 0x00031FE6 File Offset: 0x000301E6
			private static ulong UInt32x32To64(uint a, uint b)
			{
				return (ulong)a * (ulong)b;
			}

			// Token: 0x06001CF5 RID: 7413 RVA: 0x0006BCD4 File Offset: 0x00069ED4
			private static void UInt64x64To128(ulong a, ulong b, ref decimal.DecCalc result)
			{
				ulong num = decimal.DecCalc.UInt32x32To64((uint)a, (uint)b);
				ulong num2 = decimal.DecCalc.UInt32x32To64((uint)a, (uint)(b >> 32));
				ulong num3 = decimal.DecCalc.UInt32x32To64((uint)(a >> 32), (uint)(b >> 32));
				num3 += num2 >> 32;
				num += (num2 <<= 32);
				if (num < num2)
				{
					num3 += 1UL;
				}
				num2 = decimal.DecCalc.UInt32x32To64((uint)(a >> 32), (uint)b);
				num3 += num2 >> 32;
				num += (num2 <<= 32);
				if (num < num2)
				{
					num3 += 1UL;
				}
				if (num3 > (ulong)(-1))
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				result.Low64 = num;
				result.High = (uint)num3;
			}

			// Token: 0x06001CF6 RID: 7414 RVA: 0x0006BD68 File Offset: 0x00069F68
			private static uint Div96By32(ref decimal.DecCalc.Buf12 bufNum, uint den)
			{
				if (bufNum.U2 != 0U)
				{
					ulong num = bufNum.High64;
					ulong num2 = num / (ulong)den;
					bufNum.High64 = num2;
					num = (num - (ulong)((uint)num2 * den) << 32) | (ulong)bufNum.U0;
					if (num == 0UL)
					{
						return 0U;
					}
					uint num3 = (uint)(num / (ulong)den);
					bufNum.U0 = num3;
					return (uint)num - num3 * den;
				}
				else
				{
					ulong num = bufNum.Low64;
					if (num == 0UL)
					{
						return 0U;
					}
					ulong num2 = num / (ulong)den;
					bufNum.Low64 = num2;
					return (uint)(num - num2 * (ulong)den);
				}
			}

			// Token: 0x06001CF7 RID: 7415 RVA: 0x0006BDDC File Offset: 0x00069FDC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static bool Div96ByConst(ref ulong high64, ref uint low, uint pow)
			{
				ulong num = high64 / (ulong)pow;
				uint num2 = (uint)(((high64 - num * (ulong)pow << 32) + (ulong)low) / (ulong)pow);
				if (low == num2 * pow)
				{
					high64 = num;
					low = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06001CF8 RID: 7416 RVA: 0x0006BE14 File Offset: 0x0006A014
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void Unscale(ref uint low, ref ulong high64, ref int scale)
			{
				while ((byte)low == 0 && scale >= 8 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 100000000U))
				{
					scale -= 8;
				}
				if ((low & 15U) == 0U && scale >= 4 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 10000U))
				{
					scale -= 4;
				}
				if ((low & 3U) == 0U && scale >= 2 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 100U))
				{
					scale -= 2;
				}
				if ((low & 1U) == 0U && scale >= 1 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 10U))
				{
					scale--;
				}
			}

			// Token: 0x06001CF9 RID: 7417 RVA: 0x0006BE9C File Offset: 0x0006A09C
			private static uint Div96By64(ref decimal.DecCalc.Buf12 bufNum, ulong den)
			{
				uint u = bufNum.U2;
				if (u == 0U)
				{
					ulong num = bufNum.Low64;
					if (num < den)
					{
						return 0U;
					}
					uint num2 = (uint)(num / den);
					num -= (ulong)num2 * den;
					bufNum.Low64 = num;
					return num2;
				}
				else
				{
					uint num3 = (uint)(den >> 32);
					ulong num;
					uint num2;
					if (u >= num3)
					{
						num = bufNum.Low64;
						num -= den << 32;
						num2 = 0U;
						do
						{
							num2 -= 1U;
							num += den;
						}
						while (num >= den);
						bufNum.Low64 = num;
						return num2;
					}
					ulong high = bufNum.High64;
					if (high < (ulong)num3)
					{
						return 0U;
					}
					num2 = (uint)(high / (ulong)num3);
					num = (ulong)bufNum.U0 | (high - (ulong)(num2 * num3) << 32);
					ulong num4 = decimal.DecCalc.UInt32x32To64(num2, (uint)den);
					num -= num4;
					if (num > ~num4)
					{
						do
						{
							num2 -= 1U;
							num += den;
						}
						while (num >= den);
					}
					bufNum.Low64 = num;
					return num2;
				}
			}

			// Token: 0x06001CFA RID: 7418 RVA: 0x0006BF58 File Offset: 0x0006A158
			private static uint Div128By96(ref decimal.DecCalc.Buf16 bufNum, ref decimal.DecCalc.Buf12 bufDen)
			{
				ulong high = bufNum.High64;
				uint u = bufDen.U2;
				if (high < (ulong)u)
				{
					return 0U;
				}
				uint num = (uint)(high / (ulong)u);
				uint num2 = (uint)high - num * u;
				ulong num3 = decimal.DecCalc.UInt32x32To64(num, bufDen.U0);
				ulong num4 = decimal.DecCalc.UInt32x32To64(num, bufDen.U1);
				num4 += num3 >> 32;
				num3 = (ulong)((uint)num3) | (num4 << 32);
				num4 >>= 32;
				ulong num5 = bufNum.Low64;
				num5 -= num3;
				num2 -= (uint)num4;
				if (num5 > ~num3)
				{
					num2 -= 1U;
					if (num2 < ~(uint)num4)
					{
						goto IL_00B4;
					}
				}
				else if (num2 <= ~(uint)num4)
				{
					goto IL_00B4;
				}
				num3 = bufDen.Low64;
				do
				{
					num -= 1U;
					num5 += num3;
					num2 += u;
				}
				while ((num5 >= num3 || num2++ >= u) && num2 >= u);
				IL_00B4:
				bufNum.Low64 = num5;
				bufNum.U2 = num2;
				return num;
			}

			// Token: 0x06001CFB RID: 7419 RVA: 0x0006C02C File Offset: 0x0006A22C
			private static uint IncreaseScale(ref decimal.DecCalc.Buf12 bufNum, uint power)
			{
				ulong num = decimal.DecCalc.UInt32x32To64(bufNum.U0, power);
				bufNum.U0 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U1, power);
				bufNum.U1 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U2, power);
				bufNum.U2 = (uint)num;
				return (uint)(num >> 32);
			}

			// Token: 0x06001CFC RID: 7420 RVA: 0x0006C08C File Offset: 0x0006A28C
			private static void IncreaseScale64(ref decimal.DecCalc.Buf12 bufNum, uint power)
			{
				ulong num = decimal.DecCalc.UInt32x32To64(bufNum.U0, power);
				bufNum.U0 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U1, power);
				bufNum.High64 = num;
			}

			// Token: 0x06001CFD RID: 7421 RVA: 0x0006C0CC File Offset: 0x0006A2CC
			private unsafe static int ScaleResult(decimal.DecCalc.Buf24* bufRes, uint hiRes, int scale)
			{
				int num = 0;
				if (hiRes > 2U)
				{
					num = (int)(hiRes * 32U - 64U - 1U);
					num -= decimal.DecCalc.LeadingZeroCount(*(uint*)(bufRes + (ulong)hiRes * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24)));
					num = (num * 77 >> 8) + 1;
					if (num > scale)
					{
						goto IL_01CC;
					}
				}
				if (num < scale - 28)
				{
					num = scale - 28;
				}
				if (num != 0)
				{
					scale -= num;
					uint num2 = 0U;
					uint num3 = 0U;
					for (;;)
					{
						num2 |= num3;
						uint num5;
						uint num4;
						switch (num)
						{
						case 1:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10U);
							break;
						case 2:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100U);
							break;
						case 3:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000U);
							break;
						case 4:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10000U);
							break;
						case 5:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100000U);
							break;
						case 6:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000000U);
							break;
						case 7:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10000000U);
							break;
						case 8:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100000000U);
							break;
						default:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000000000U);
							break;
						}
						*(int*)(bufRes + (ulong)hiRes * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24)) = (int)num5;
						if (num5 == 0U && hiRes != 0U)
						{
							hiRes -= 1U;
						}
						num -= 9;
						if (num <= 0)
						{
							if (hiRes > 2U)
							{
								if (scale == 0)
								{
									goto IL_01CC;
								}
								num = 1;
								scale--;
							}
							else
							{
								num4 >>= 1;
								if (num4 > num3 || (num4 >= num3 && ((*(uint*)bufRes & 1U) | num2) == 0U))
								{
									break;
								}
								uint num6 = *(uint*)bufRes + 1U;
								*(int*)bufRes = (int)num6;
								if (num6 != 0U)
								{
									break;
								}
								uint num7 = 0U;
								do
								{
									decimal.DecCalc.Buf24* ptr = bufRes + (ulong)(num7 += 1U) * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24);
									num6 = *(uint*)ptr + 1U;
									*(int*)ptr = (int)num6;
								}
								while (num6 == 0U);
								if (num7 <= 2U)
								{
									break;
								}
								if (scale == 0)
								{
									goto IL_01CC;
								}
								hiRes = num7;
								num2 = 0U;
								num3 = 0U;
								num = 1;
								scale--;
							}
						}
					}
				}
				return scale;
				IL_01CC:
				throw new OverflowException("Value was either too large or too small for a Decimal.");
			}

			// Token: 0x06001CFE RID: 7422 RVA: 0x0006C2B0 File Offset: 0x0006A4B0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private unsafe static uint DivByConst(uint* result, uint hiRes, out uint quotient, out uint remainder, uint power)
			{
				uint num = result[(ulong)hiRes * 4UL / 4UL];
				remainder = num - (quotient = num / power) * power;
				for (uint num2 = hiRes - 1U; num2 >= 0U; num2 -= 1U)
				{
					ulong num3 = (ulong)result[(ulong)num2 * 4UL / 4UL] + ((ulong)remainder << 32);
					remainder = (uint)num3 - (result[(ulong)num2 * 4UL / 4UL] = (uint)(num3 / (ulong)power)) * power;
				}
				return power;
			}

			// Token: 0x06001CFF RID: 7423 RVA: 0x0006C314 File Offset: 0x0006A514
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static int LeadingZeroCount(uint value)
			{
				int num = 1;
				if ((value & 4294901760U) == 0U)
				{
					value <<= 16;
					num += 16;
				}
				if ((value & 4278190080U) == 0U)
				{
					value <<= 8;
					num += 8;
				}
				if ((value & 4026531840U) == 0U)
				{
					value <<= 4;
					num += 4;
				}
				if ((value & 3221225472U) == 0U)
				{
					value <<= 2;
					num += 2;
				}
				return num + ((int)value >> 31);
			}

			// Token: 0x06001D00 RID: 7424 RVA: 0x0006C374 File Offset: 0x0006A574
			private static int OverflowUnscale(ref decimal.DecCalc.Buf12 bufQuo, int scale, bool sticky)
			{
				if (--scale < 0)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				bufQuo.U2 = 429496729U;
				ulong num = 25769803776UL + (ulong)bufQuo.U1;
				uint num2 = (uint)(num / 10UL);
				bufQuo.U1 = num2;
				ulong num3 = (num - (ulong)(num2 * 10U) << 32) + (ulong)bufQuo.U0;
				num2 = (uint)(num3 / 10UL);
				bufQuo.U0 = num2;
				uint num4 = (uint)(num3 - (ulong)(num2 * 10U));
				if (num4 > 5U || (num4 == 5U && (sticky || (bufQuo.U0 & 1U) != 0U)))
				{
					decimal.DecCalc.Add32To96(ref bufQuo, 1U);
				}
				return scale;
			}

			// Token: 0x06001D01 RID: 7425 RVA: 0x0006C404 File Offset: 0x0006A604
			private static int SearchScale(ref decimal.DecCalc.Buf12 bufQuo, int scale)
			{
				uint u = bufQuo.U2;
				ulong low = bufQuo.Low64;
				int num = 0;
				if (u <= 429496729U)
				{
					decimal.DecCalc.PowerOvfl[] powerOvflValues = decimal.DecCalc.PowerOvflValues;
					if (scale > 19)
					{
						num = 28 - scale;
						if (u < powerOvflValues[num - 1].Hi)
						{
							goto IL_00D1;
						}
					}
					else if (u < 4U || (u == 4U && low <= 5441186219426131129UL))
					{
						return 9;
					}
					if (u > 42949U)
					{
						if (u > 4294967U)
						{
							num = 2;
							if (u > 42949672U)
							{
								num--;
							}
						}
						else
						{
							num = 4;
							if (u > 429496U)
							{
								num--;
							}
						}
					}
					else if (u > 429U)
					{
						num = 6;
						if (u > 4294U)
						{
							num--;
						}
					}
					else
					{
						num = 8;
						if (u > 42U)
						{
							num--;
						}
					}
					if (u == powerOvflValues[num - 1].Hi && low > powerOvflValues[num - 1].MidLo)
					{
						num--;
					}
				}
				IL_00D1:
				if (num + scale < 0)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				return num;
			}

			// Token: 0x06001D02 RID: 7426 RVA: 0x0006C4F4 File Offset: 0x0006A6F4
			private static bool Add32To96(ref decimal.DecCalc.Buf12 bufNum, uint value)
			{
				if ((bufNum.Low64 += (ulong)value) < (ulong)value)
				{
					uint num = bufNum.U2 + 1U;
					bufNum.U2 = num;
					if (num == 0U)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06001D03 RID: 7427 RVA: 0x0006C52C File Offset: 0x0006A72C
			internal unsafe static void DecAddSub(ref decimal.DecCalc d1, ref decimal.DecCalc d2, bool sign)
			{
				ulong num = d1.Low64;
				uint num2 = d1.High;
				uint num3 = d1.uflags;
				uint num4 = d2.uflags;
				uint num5 = num4 ^ num3;
				sign ^= (num5 & 2147483648U) > 0U;
				if ((num5 & 16711680U) != 0U)
				{
					uint num6 = num3;
					num3 = (num4 & 16711680U) | (num3 & 2147483648U);
					int i = (int)(num3 - num6) >> 16;
					if (i < 0)
					{
						i = -i;
						num3 = num6;
						if (sign)
						{
							num3 ^= 2147483648U;
						}
						num = d2.Low64;
						num2 = d2.High;
						d2 = d1;
					}
					ulong num10;
					if (num2 == 0U)
					{
						if (num <= (ulong)(-1))
						{
							if ((uint)num == 0U)
							{
								uint num7 = num3 & 2147483648U;
								if (sign)
								{
									num7 ^= 2147483648U;
								}
								d1 = d2;
								d1.uflags = (d2.uflags & 16711680U) | num7;
								return;
							}
							while (i > 9)
							{
								i -= 9;
								num = decimal.DecCalc.UInt32x32To64((uint)num, 1000000000U);
								if (num > (ulong)(-1))
								{
									goto IL_0106;
								}
							}
							num = decimal.DecCalc.UInt32x32To64((uint)num, decimal.DecCalc.s_powers10[i]);
							goto IL_0441;
						}
						do
						{
							IL_0106:
							uint num8 = 1000000000U;
							if (i < 9)
							{
								num8 = decimal.DecCalc.s_powers10[i];
							}
							ulong num9 = decimal.DecCalc.UInt32x32To64((uint)num, num8);
							num10 = decimal.DecCalc.UInt32x32To64((uint)(num >> 32), num8) + (num9 >> 32);
							num = (ulong)((uint)num9) + (num10 << 32);
							num2 = (uint)(num10 >> 32);
							if ((i -= 9) <= 0)
							{
								goto IL_0441;
							}
						}
						while (num2 == 0U);
					}
					do
					{
						uint num8 = 1000000000U;
						if (i < 9)
						{
							num8 = decimal.DecCalc.s_powers10[i];
						}
						ulong num9 = decimal.DecCalc.UInt32x32To64((uint)num, num8);
						num10 = decimal.DecCalc.UInt32x32To64((uint)(num >> 32), num8) + (num9 >> 32);
						num = (ulong)((uint)num9) + (num10 << 32);
						num10 >>= 32;
						num10 += decimal.DecCalc.UInt32x32To64(num2, num8);
						i -= 9;
						if (num10 > (ulong)(-1))
						{
							goto IL_01CF;
						}
						num2 = (uint)num10;
					}
					while (i > 0);
					goto IL_0441;
					IL_01CF:
					decimal.DecCalc.Buf24 buf;
					buf.Low64 = num;
					buf.Mid64 = num10;
					uint num11 = 3U;
					while (i > 0)
					{
						uint num8 = 1000000000U;
						if (i < 9)
						{
							num8 = decimal.DecCalc.s_powers10[i];
						}
						num10 = 0UL;
						uint* ptr = (uint*)(&buf);
						uint num12 = 0U;
						do
						{
							num10 += decimal.DecCalc.UInt32x32To64(ptr[(ulong)num12 * 4UL / 4UL], num8);
							ptr[(ulong)num12 * 4UL / 4UL] = (uint)num10;
							num12 += 1U;
							num10 >>= 32;
						}
						while (num12 <= num11);
						if ((uint)num10 != 0U)
						{
							ptr[(IntPtr)((ulong)(num11 += 1U) * 4UL)] = (uint)num10;
						}
						i -= 9;
					}
					num10 = buf.Low64;
					num = d2.Low64;
					uint u = buf.U2;
					num2 = d2.High;
					if (sign)
					{
						num = num10 - num;
						num2 = u - num2;
						if (num > num10)
						{
							num2 -= 1U;
							if (num2 < u)
							{
								goto IL_034C;
							}
						}
						else if (num2 <= u)
						{
							goto IL_034C;
						}
						uint* ptr2 = (uint*)(&buf);
						uint num13 = 3U;
						uint num14;
						do
						{
							uint* ptr3 = ptr2 + (IntPtr)((ulong)num13++ * 4UL);
							num14 = *ptr3;
							*ptr3 = num14 - 1U;
						}
						while (num14 == 0U);
						if (ptr2[(ulong)num11 * 4UL / 4UL] == 0U && (num11 -= 1U) <= 2U)
						{
							goto IL_04AA;
						}
					}
					else
					{
						num += num10;
						num2 += u;
						if (num < num10)
						{
							num2 += 1U;
							if (num2 > u)
							{
								goto IL_034C;
							}
						}
						else if (num2 >= u)
						{
							goto IL_034C;
						}
						uint* ptr4 = (uint*)(&buf);
						uint num15 = 3U;
						do
						{
							uint* ptr5 = ptr4 + (IntPtr)((ulong)num15++ * 4UL);
							uint num14 = *ptr5 + 1U;
							*ptr5 = num14;
							if (num14 != 0U)
							{
								goto IL_034C;
							}
						}
						while (num11 >= num15);
						ptr4[(ulong)num15 * 4UL / 4UL] = 1U;
						num11 = num15;
					}
					IL_034C:
					buf.Low64 = num;
					buf.U2 = num2;
					i = decimal.DecCalc.ScaleResult(&buf, num11, (int)((byte)(num3 >> 16)));
					num3 = (num3 & 4278255615U) | (uint)((uint)i << 16);
					num = buf.Low64;
					num2 = buf.U2;
					goto IL_04AA;
				}
				IL_0441:
				ulong num16 = num;
				uint num17 = num2;
				if (sign)
				{
					num = num16 - d2.Low64;
					num2 = num17 - d2.High;
					if (num > num16)
					{
						num2 -= 1U;
						if (num2 < num17)
						{
							goto IL_04AA;
						}
					}
					else if (num2 <= num17)
					{
						goto IL_04AA;
					}
					num3 ^= 2147483648U;
					num2 = ~num2;
					num = -num;
					if (num == 0UL)
					{
						num2 += 1U;
					}
				}
				else
				{
					num = num16 + d2.Low64;
					num2 = num17 + d2.High;
					if (num < num16)
					{
						num2 += 1U;
						if (num2 > num17)
						{
							goto IL_04AA;
						}
					}
					else if (num2 >= num17)
					{
						goto IL_04AA;
					}
					if ((num3 & 16711680U) == 0U)
					{
						throw new OverflowException("Value was either too large or too small for a Decimal.");
					}
					num3 -= 65536U;
					ulong num18 = (ulong)num2 + 4294967296UL;
					num2 = (uint)(num18 / 10UL);
					ulong num19 = (num18 - (ulong)(num2 * 10U) << 32) + (num >> 32);
					uint num20 = (uint)(num19 / 10UL);
					ulong num21 = (num19 - (ulong)(num20 * 10U) << 32) + (ulong)((uint)num);
					num = (ulong)num20;
					num <<= 32;
					num20 = (uint)(num21 / 10UL);
					num += (ulong)num20;
					num20 = (uint)num21 - num20 * 10U;
					if (num20 >= 5U && (num20 > 5U || (num & 1UL) != 0UL) && (num += 1UL) == 0UL)
					{
						num2 += 1U;
					}
				}
				IL_04AA:
				d1.uflags = num3;
				d1.High = num2;
				d1.Low64 = num;
			}

			// Token: 0x06001D04 RID: 7428 RVA: 0x0006C9F8 File Offset: 0x0006ABF8
			internal static long VarCyFromDec(ref decimal.DecCalc pdecIn)
			{
				int num = pdecIn.Scale - 4;
				long num5;
				if (num < 0)
				{
					if (pdecIn.High != 0U)
					{
						goto IL_0093;
					}
					uint num2 = decimal.DecCalc.s_powers10[-num];
					ulong num3 = decimal.DecCalc.UInt32x32To64(num2, pdecIn.Mid);
					if (num3 > (ulong)(-1))
					{
						goto IL_0093;
					}
					ulong num4 = decimal.DecCalc.UInt32x32To64(num2, pdecIn.Low);
					num4 += (num3 <<= 32);
					if (num4 < num3)
					{
						goto IL_0093;
					}
					num5 = (long)num4;
				}
				else
				{
					if (num != 0)
					{
						decimal.DecCalc.InternalRound(ref pdecIn, (uint)num, decimal.DecCalc.RoundingMode.ToEven);
					}
					if (pdecIn.High != 0U)
					{
						goto IL_0093;
					}
					num5 = (long)pdecIn.Low64;
				}
				if (num5 >= 0L || (num5 == -9223372036854775808L && pdecIn.IsNegative))
				{
					if (pdecIn.IsNegative)
					{
						num5 = -num5;
					}
					return num5;
				}
				IL_0093:
				throw new OverflowException("Value was either too large or too small for a Currency.");
			}

			// Token: 0x06001D05 RID: 7429 RVA: 0x0006CAA4 File Offset: 0x0006ACA4
			internal static int VarDecCmp(in decimal d1, in decimal d2)
			{
				if ((d2.Low | d2.Mid | d2.High) == 0U)
				{
					if ((d1.Low | d1.Mid | d1.High) == 0U)
					{
						return 0;
					}
					return (d1.flags >> 31) | 1;
				}
				else
				{
					if ((d1.Low | d1.Mid | d1.High) == 0U)
					{
						return -((d2.flags >> 31) | 1);
					}
					int num = (d1.flags >> 31) - (d2.flags >> 31);
					if (num != 0)
					{
						return num;
					}
					return decimal.DecCalc.VarDecCmpSub(in d1, in d2);
				}
			}

			// Token: 0x06001D06 RID: 7430 RVA: 0x0006CB30 File Offset: 0x0006AD30
			private static int VarDecCmpSub(in decimal d1, in decimal d2)
			{
				int flags = d2.flags;
				int num = (flags >> 31) | 1;
				int num2 = flags - d1.flags;
				ulong num3 = d1.Low64;
				uint num4 = d1.High;
				ulong num5 = d2.Low64;
				uint num6 = d2.High;
				if (num2 != 0)
				{
					num2 >>= 16;
					if (num2 < 0)
					{
						num2 = -num2;
						num = -num;
						ulong num7 = num3;
						num3 = num5;
						num5 = num7;
						uint num8 = num4;
						num4 = num6;
						num6 = num8;
					}
					for (;;)
					{
						uint num9 = ((num2 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num2]);
						ulong num10 = decimal.DecCalc.UInt32x32To64((uint)num3, num9);
						ulong num11 = decimal.DecCalc.UInt32x32To64((uint)(num3 >> 32), num9) + (num10 >> 32);
						num3 = (ulong)((uint)num10) + (num11 << 32);
						num11 >>= 32;
						num11 += decimal.DecCalc.UInt32x32To64(num4, num9);
						if (num11 > (ulong)(-1))
						{
							break;
						}
						num4 = (uint)num11;
						if ((num2 -= 9) <= 0)
						{
							goto IL_00BC;
						}
					}
					return num;
				}
				IL_00BC:
				uint num12 = num4 - num6;
				if (num12 != 0U)
				{
					if (num12 > num4)
					{
						num = -num;
					}
					return num;
				}
				ulong num13 = num3 - num5;
				if (num13 == 0UL)
				{
					num = 0;
				}
				else if (num13 > num3)
				{
					num = -num;
				}
				return num;
			}

			// Token: 0x06001D07 RID: 7431 RVA: 0x0006CC24 File Offset: 0x0006AE24
			internal unsafe static void VarDecMul(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				int num = (int)((byte)(d1.uflags + d2.uflags >> 16));
				decimal.DecCalc.Buf24 buf;
				uint num6;
				if ((d1.High | d1.Mid) == 0U)
				{
					ulong num4;
					if ((d2.High | d2.Mid) == 0U)
					{
						ulong num2 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
						if (num > 28)
						{
							if (num > 47)
							{
								goto IL_03CD;
							}
							num -= 29;
							ulong num3 = decimal.DecCalc.s_ulongPowers10[num];
							num4 = num2 / num3;
							ulong num5 = num2 - num4 * num3;
							num2 = num4;
							num3 >>= 1;
							if (num5 >= num3 && (num5 > num3 || ((uint)num2 & 1U) > 0U))
							{
								num2 += 1UL;
							}
							num = 28;
						}
						d1.Low64 = num2;
						d1.uflags = ((d2.uflags ^ d1.uflags) & 2147483648U) | (uint)((uint)num << 16);
						return;
					}
					num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
					buf.U0 = (uint)num4;
					num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Mid) + (num4 >> 32);
					buf.U1 = (uint)num4;
					num4 >>= 32;
					if (d2.High != 0U)
					{
						num4 += decimal.DecCalc.UInt32x32To64(d1.Low, d2.High);
						if (num4 > (ulong)(-1))
						{
							buf.Mid64 = num4;
							num6 = 3U;
							goto IL_0381;
						}
					}
					if ((uint)num4 != 0U)
					{
						buf.U2 = (uint)num4;
						num6 = 2U;
						goto IL_0381;
					}
					num6 = 1U;
				}
				else if ((d2.High | d2.Mid) == 0U)
				{
					ulong num4 = decimal.DecCalc.UInt32x32To64(d2.Low, d1.Low);
					buf.U0 = (uint)num4;
					num4 = decimal.DecCalc.UInt32x32To64(d2.Low, d1.Mid) + (num4 >> 32);
					buf.U1 = (uint)num4;
					num4 >>= 32;
					if (d1.High != 0U)
					{
						num4 += decimal.DecCalc.UInt32x32To64(d2.Low, d1.High);
						if (num4 > (ulong)(-1))
						{
							buf.Mid64 = num4;
							num6 = 3U;
							goto IL_0381;
						}
					}
					if ((uint)num4 != 0U)
					{
						buf.U2 = (uint)num4;
						num6 = 2U;
						goto IL_0381;
					}
					num6 = 1U;
				}
				else
				{
					ulong num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
					buf.U0 = (uint)num4;
					ulong num7 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Mid) + (num4 >> 32);
					num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.Low);
					num4 += num7;
					buf.U1 = (uint)num4;
					if (num4 < num7)
					{
						num7 = (num4 >> 32) | 4294967296UL;
					}
					else
					{
						num7 = num4 >> 32;
					}
					num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.Mid) + num7;
					if ((d1.High | d2.High) > 0U)
					{
						num7 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.High);
						num4 += num7;
						uint num8 = 0U;
						if (num4 < num7)
						{
							num8 = 1U;
						}
						num7 = decimal.DecCalc.UInt32x32To64(d1.High, d2.Low);
						num4 += num7;
						buf.U2 = (uint)num4;
						if (num4 < num7)
						{
							num8 += 1U;
						}
						num7 = ((ulong)num8 << 32) | (num4 >> 32);
						num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.High);
						num4 += num7;
						num8 = 0U;
						if (num4 < num7)
						{
							num8 = 1U;
						}
						num7 = decimal.DecCalc.UInt32x32To64(d1.High, d2.Mid);
						num4 += num7;
						buf.U3 = (uint)num4;
						if (num4 < num7)
						{
							num8 += 1U;
						}
						num4 = ((ulong)num8 << 32) | (num4 >> 32);
						buf.High64 = decimal.DecCalc.UInt32x32To64(d1.High, d2.High) + num4;
						num6 = 5U;
					}
					else if (num4 != 0UL)
					{
						buf.Mid64 = num4;
						num6 = 3U;
					}
					else
					{
						num6 = 1U;
					}
				}
				uint* ptr = (uint*)(&buf);
				while (ptr[num6] == 0U)
				{
					if (num6 == 0U)
					{
						goto IL_03CD;
					}
					num6 -= 1U;
				}
				IL_0381:
				if (num6 > 2U || num > 28)
				{
					num = decimal.DecCalc.ScaleResult(&buf, num6, num);
				}
				d1.Low64 = buf.Low64;
				d1.High = buf.U2;
				d1.uflags = ((d2.uflags ^ d1.uflags) & 2147483648U) | (uint)((uint)num << 16);
				return;
				IL_03CD:
				d1 = default(decimal.DecCalc);
			}

			// Token: 0x06001D08 RID: 7432 RVA: 0x0006D008 File Offset: 0x0006B208
			internal static void VarDecFromR4(float input, out decimal.DecCalc result)
			{
				result = default(decimal.DecCalc);
				int num = (int)(decimal.DecCalc.GetExponent(input) - 126U);
				if (num < -94)
				{
					return;
				}
				if (num > 96)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				uint num2 = 0U;
				if (input < 0f)
				{
					input = -input;
					num2 = 2147483648U;
				}
				double num3 = (double)input;
				int num4 = 6 - (num * 19728 >> 16);
				if (num4 >= 0)
				{
					if (num4 > 28)
					{
						num4 = 28;
					}
					num3 *= decimal.DecCalc.s_doublePowers10[num4];
				}
				else if (num4 != -1 || num3 >= 10000000.0)
				{
					num3 /= decimal.DecCalc.s_doublePowers10[-num4];
				}
				else
				{
					num4 = 0;
				}
				if (num3 < 1000000.0 && num4 < 28)
				{
					num3 *= 10.0;
					num4++;
				}
				uint num5 = (uint)((int)num3);
				num3 -= (double)num5;
				if (num3 > 0.5 || (num3 == 0.5 && (num5 & 1U) != 0U))
				{
					num5 += 1U;
				}
				if (num5 == 0U)
				{
					return;
				}
				if (num4 < 0)
				{
					num4 = -num4;
					if (num4 < 10)
					{
						result.Low64 = decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4]);
					}
					else if (num4 > 18)
					{
						decimal.DecCalc.UInt64x64To128(decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4 - 18]), 1000000000000000000UL, ref result);
					}
					else
					{
						ulong num6 = decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4 - 9]);
						ulong num7 = decimal.DecCalc.UInt32x32To64(1000000000U, (uint)(num6 >> 32));
						num6 = decimal.DecCalc.UInt32x32To64(1000000000U, (uint)num6);
						result.Low = (uint)num6;
						num7 += num6 >> 32;
						result.Mid = (uint)num7;
						num7 >>= 32;
						result.High = (uint)num7;
					}
				}
				else
				{
					int num8 = num4;
					if (num8 > 6)
					{
						num8 = 6;
					}
					if ((num5 & 15U) == 0U && num8 >= 4)
					{
						uint num9 = num5 / 10000U;
						if (num5 == num9 * 10000U)
						{
							num5 = num9;
							num4 -= 4;
							num8 -= 4;
						}
					}
					if ((num5 & 3U) == 0U && num8 >= 2)
					{
						uint num10 = num5 / 100U;
						if (num5 == num10 * 100U)
						{
							num5 = num10;
							num4 -= 2;
							num8 -= 2;
						}
					}
					if ((num5 & 1U) == 0U && num8 >= 1)
					{
						uint num11 = num5 / 10U;
						if (num5 == num11 * 10U)
						{
							num5 = num11;
							num4--;
						}
					}
					num2 |= (uint)((uint)num4 << 16);
					result.Low = num5;
				}
				result.uflags = num2;
			}

			// Token: 0x06001D09 RID: 7433 RVA: 0x0006D240 File Offset: 0x0006B440
			internal static void VarDecFromR8(double input, out decimal.DecCalc result)
			{
				result = default(decimal.DecCalc);
				int num = (int)(decimal.DecCalc.GetExponent(input) - 1022U);
				if (num < -94)
				{
					return;
				}
				if (num > 96)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				uint num2 = 0U;
				if (input < 0.0)
				{
					input = -input;
					num2 = 2147483648U;
				}
				double num3 = input;
				int num4 = 14 - (num * 19728 >> 16);
				if (num4 >= 0)
				{
					if (num4 > 28)
					{
						num4 = 28;
					}
					num3 *= decimal.DecCalc.s_doublePowers10[num4];
				}
				else if (num4 != -1 || num3 >= 1000000000000000.0)
				{
					num3 /= decimal.DecCalc.s_doublePowers10[-num4];
				}
				else
				{
					num4 = 0;
				}
				if (num3 < 100000000000000.0 && num4 < 28)
				{
					num3 *= 10.0;
					num4++;
				}
				ulong num5 = (ulong)((long)num3);
				num3 -= (double)num5;
				if (num3 > 0.5 || (num3 == 0.5 && (num5 & 1UL) != 0UL))
				{
					num5 += 1UL;
				}
				if (num5 == 0UL)
				{
					return;
				}
				if (num4 < 0)
				{
					num4 = -num4;
					if (num4 < 10)
					{
						uint num6 = decimal.DecCalc.s_powers10[num4];
						ulong num7 = decimal.DecCalc.UInt32x32To64((uint)num5, num6);
						ulong num8 = decimal.DecCalc.UInt32x32To64((uint)(num5 >> 32), num6);
						result.Low = (uint)num7;
						num8 += num7 >> 32;
						result.Mid = (uint)num8;
						num8 >>= 32;
						result.High = (uint)num8;
					}
					else
					{
						decimal.DecCalc.UInt64x64To128(num5, decimal.DecCalc.s_ulongPowers10[num4 - 1], ref result);
					}
				}
				else
				{
					int num9 = num4;
					if (num9 > 14)
					{
						num9 = 14;
					}
					if ((byte)num5 == 0 && num9 >= 8)
					{
						ulong num10 = num5 / 100000000UL;
						if ((uint)num5 == (uint)(num10 * 100000000UL))
						{
							num5 = num10;
							num4 -= 8;
							num9 -= 8;
						}
					}
					if (((uint)num5 & 15U) == 0U && num9 >= 4)
					{
						ulong num11 = num5 / 10000UL;
						if ((uint)num5 == (uint)(num11 * 10000UL))
						{
							num5 = num11;
							num4 -= 4;
							num9 -= 4;
						}
					}
					if (((uint)num5 & 3U) == 0U && num9 >= 2)
					{
						ulong num12 = num5 / 100UL;
						if ((uint)num5 == (uint)(num12 * 100UL))
						{
							num5 = num12;
							num4 -= 2;
							num9 -= 2;
						}
					}
					if (((uint)num5 & 1U) == 0U && num9 >= 1)
					{
						ulong num13 = num5 / 10UL;
						if ((uint)num5 == (uint)(num13 * 10UL))
						{
							num5 = num13;
							num4--;
						}
					}
					num2 |= (uint)((uint)num4 << 16);
					result.Low64 = num5;
				}
				result.uflags = num2;
			}

			// Token: 0x06001D0A RID: 7434 RVA: 0x0006D483 File Offset: 0x0006B683
			internal static float VarR4FromDec(in decimal value)
			{
				return (float)decimal.DecCalc.VarR8FromDec(in value);
			}

			// Token: 0x06001D0B RID: 7435 RVA: 0x0006D48C File Offset: 0x0006B68C
			internal static double VarR8FromDec(in decimal value)
			{
				double num = (value.Low64 + value.High * 1.8446744073709552E+19) / decimal.DecCalc.s_doublePowers10[value.Scale];
				if (value.IsNegative)
				{
					num = -num;
				}
				return num;
			}

			// Token: 0x06001D0C RID: 7436 RVA: 0x0006D4D0 File Offset: 0x0006B6D0
			internal static int GetHashCode(in decimal d)
			{
				if ((d.Low | d.Mid | d.High) == 0U)
				{
					return 0;
				}
				uint num = (uint)d.flags;
				if ((num & 16711680U) == 0U || (d.Low & 1U) != 0U)
				{
					return (int)(num ^ d.High ^ d.Mid ^ d.Low);
				}
				int num2 = (int)((byte)(num >> 16));
				uint low = d.Low;
				ulong num3 = ((ulong)d.High << 32) | (ulong)d.Mid;
				decimal.DecCalc.Unscale(ref low, ref num3, ref num2);
				num = (num & 4278255615U) | (uint)((uint)num2 << 16);
				return (int)(num ^ (uint)(num3 >> 32) ^ (uint)num3 ^ low);
			}

			// Token: 0x06001D0D RID: 7437 RVA: 0x0006D56C File Offset: 0x0006B76C
			internal unsafe static void VarDecDiv(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				int num = (int)((sbyte)(d1.uflags - d2.uflags >> 16));
				bool flag = false;
				decimal.DecCalc.Buf12 buf;
				if ((d2.High | d2.Mid) == 0U)
				{
					uint low = d2.Low;
					if (low == 0U)
					{
						throw new DivideByZeroException();
					}
					buf.Low64 = d1.Low64;
					buf.U2 = d1.High;
					uint num2 = decimal.DecCalc.Div96By32(ref buf, low);
					for (;;)
					{
						int num3;
						if (num2 == 0U)
						{
							if (num >= 0)
							{
								goto IL_03D2;
							}
							num3 = Math.Min(9, -num);
						}
						else
						{
							flag = true;
							if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
							{
								break;
							}
						}
						uint num4 = decimal.DecCalc.s_powers10[num3];
						num += num3;
						if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
						{
							goto IL_048A;
						}
						ulong num5 = decimal.DecCalc.UInt32x32To64(num2, num4);
						uint num6 = (uint)(num5 / (ulong)low);
						num2 = (uint)num5 - num6 * low;
						if (!decimal.DecCalc.Add32To96(ref buf, num6))
						{
							goto Block_11;
						}
					}
					uint num7 = num2 << 1;
					if (num7 < num2)
					{
						goto IL_0449;
					}
					if (num7 < low)
					{
						goto IL_03D2;
					}
					if (num7 > low)
					{
						goto IL_0449;
					}
					if ((buf.U0 & 1U) != 0U)
					{
						goto IL_0449;
					}
					goto IL_03D2;
					Block_11:
					num = decimal.DecCalc.OverflowUnscale(ref buf, num, num2 > 0U);
				}
				else
				{
					uint num7 = d2.High;
					if (num7 == 0U)
					{
						num7 = d2.Mid;
					}
					int num3 = decimal.DecCalc.LeadingZeroCount(num7);
					decimal.DecCalc.Buf16 buf2;
					buf2.Low64 = d1.Low64 << num3;
					buf2.High64 = (ulong)d1.Mid + ((ulong)d1.High << 32) >> 32 - num3;
					ulong num8 = d2.Low64 << num3;
					if (d2.High == 0U)
					{
						buf.U1 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2.U1), num8);
						buf.U0 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2), num8);
						for (;;)
						{
							if (buf2.Low64 == 0UL)
							{
								if (num >= 0)
								{
									goto IL_03D2;
								}
								num3 = Math.Min(9, -num);
							}
							else
							{
								flag = true;
								if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
								{
									break;
								}
							}
							uint num4 = decimal.DecCalc.s_powers10[num3];
							num += num3;
							if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
							{
								goto IL_048A;
							}
							decimal.DecCalc.IncreaseScale64(ref *(decimal.DecCalc.Buf12*)(&buf2), num4);
							num7 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2), num8);
							if (!decimal.DecCalc.Add32To96(ref buf, num7))
							{
								goto Block_22;
							}
						}
						ulong num9 = buf2.Low64;
						if (num9 < 0UL || (num9 <<= 1) > num8)
						{
							goto IL_0449;
						}
						if (num9 == num8 && (buf.U0 & 1U) != 0U)
						{
							goto IL_0449;
						}
						goto IL_03D2;
						Block_22:
						num = decimal.DecCalc.OverflowUnscale(ref buf, num, buf2.Low64 > 0UL);
					}
					else
					{
						decimal.DecCalc.Buf12 buf3;
						buf3.Low64 = num8;
						buf3.U2 = (uint)((ulong)d2.Mid + ((ulong)d2.High << 32) >> 32 - num3);
						buf.Low64 = (ulong)decimal.DecCalc.Div128By96(ref buf2, ref buf3);
						for (;;)
						{
							if ((buf2.Low64 | (ulong)buf2.U2) == 0UL)
							{
								if (num >= 0)
								{
									goto IL_03D2;
								}
								num3 = Math.Min(9, -num);
							}
							else
							{
								flag = true;
								if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
								{
									break;
								}
							}
							uint num4 = decimal.DecCalc.s_powers10[num3];
							num += num3;
							if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
							{
								goto IL_048A;
							}
							buf2.U3 = decimal.DecCalc.IncreaseScale(ref *(decimal.DecCalc.Buf12*)(&buf2), num4);
							num7 = decimal.DecCalc.Div128By96(ref buf2, ref buf3);
							if (!decimal.DecCalc.Add32To96(ref buf, num7))
							{
								goto Block_33;
							}
						}
						if (buf2.U2 < 0U)
						{
							goto IL_0449;
						}
						num7 = buf2.U1 >> 31;
						buf2.Low64 <<= 1;
						buf2.U2 = (buf2.U2 << 1) + num7;
						if (buf2.U2 > buf3.U2)
						{
							goto IL_0449;
						}
						if (buf2.U2 != buf3.U2)
						{
							goto IL_03D2;
						}
						if (buf2.Low64 > buf3.Low64)
						{
							goto IL_0449;
						}
						if (buf2.Low64 == buf3.Low64 && (buf.U0 & 1U) != 0U)
						{
							goto IL_0449;
						}
						goto IL_03D2;
						Block_33:
						num = decimal.DecCalc.OverflowUnscale(ref buf, num, (buf2.Low64 | buf2.High64) > 0UL);
					}
				}
				IL_03D2:
				if (flag)
				{
					uint u = buf.U0;
					ulong high = buf.High64;
					decimal.DecCalc.Unscale(ref u, ref high, ref num);
					d1.Low = u;
					d1.Mid = (uint)high;
					d1.High = (uint)(high >> 32);
				}
				else
				{
					d1.Low64 = buf.Low64;
					d1.High = buf.U2;
				}
				d1.uflags = ((d1.uflags ^ d2.uflags) & 2147483648U) | (uint)((uint)num << 16);
				return;
				IL_0449:
				ulong num10 = buf.Low64 + 1UL;
				buf.Low64 = num10;
				if (num10 != 0UL)
				{
					goto IL_03D2;
				}
				uint num11 = buf.U2 + 1U;
				buf.U2 = num11;
				if (num11 == 0U)
				{
					num = decimal.DecCalc.OverflowUnscale(ref buf, num, true);
					goto IL_03D2;
				}
				goto IL_03D2;
				IL_048A:
				throw new OverflowException("Value was either too large or too small for a Decimal.");
			}

			// Token: 0x06001D0E RID: 7438 RVA: 0x0006DA10 File Offset: 0x0006BC10
			internal static void VarDecMod(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				if ((d2.ulo | d2.umid | d2.uhi) == 0U)
				{
					throw new DivideByZeroException();
				}
				if ((d1.ulo | d1.umid | d1.uhi) == 0U)
				{
					return;
				}
				d2.uflags = (d2.uflags & 2147483647U) | (d1.uflags & 2147483648U);
				int num = decimal.DecCalc.VarDecCmpSub(Unsafe.As<decimal.DecCalc, decimal>(ref d1), Unsafe.As<decimal.DecCalc, decimal>(ref d2));
				if (num == 0)
				{
					d1.ulo = 0U;
					d1.umid = 0U;
					d1.uhi = 0U;
					if (d2.uflags > d1.uflags)
					{
						d1.uflags = d2.uflags;
					}
					return;
				}
				if ((num ^ (int)(d1.uflags & 2147483648U)) < 0)
				{
					return;
				}
				int num2 = (int)((sbyte)(d1.uflags - d2.uflags >> 16));
				if (num2 > 0)
				{
					do
					{
						uint num3 = ((num2 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num2]);
						ulong num4 = decimal.DecCalc.UInt32x32To64(d2.Low, num3);
						d2.Low = (uint)num4;
						num4 >>= 32;
						num4 += ((ulong)d2.Mid + ((ulong)d2.High << 32)) * (ulong)num3;
						d2.Mid = (uint)num4;
						d2.High = (uint)(num4 >> 32);
					}
					while ((num2 -= 9) > 0);
					num2 = 0;
				}
				for (;;)
				{
					if (num2 < 0)
					{
						d1.uflags = d2.uflags;
						decimal.DecCalc.Buf12 buf;
						buf.Low64 = d1.Low64;
						buf.U2 = d1.High;
						uint num6;
						do
						{
							int num5 = decimal.DecCalc.SearchScale(ref buf, 28 + num2);
							if (num5 == 0)
							{
								break;
							}
							num6 = ((num5 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num5]);
							num2 += num5;
							ulong num7 = decimal.DecCalc.UInt32x32To64(buf.U0, num6);
							buf.U0 = (uint)num7;
							num7 >>= 32;
							buf.High64 = num7 + buf.High64 * (ulong)num6;
						}
						while (num6 == 1000000000U && num2 < 0);
						d1.Low64 = buf.Low64;
						d1.High = buf.U2;
					}
					if (d1.High == 0U)
					{
						break;
					}
					if ((d2.High | d2.Mid) != 0U)
					{
						goto IL_024C;
					}
					uint low = d2.Low;
					ulong num8 = ((ulong)d1.High << 32) | (ulong)d1.Mid;
					num8 = (num8 % (ulong)low << 32) | (ulong)d1.Low;
					d1.Low64 = num8 % (ulong)low;
					d1.High = 0U;
					if (num2 >= 0)
					{
						return;
					}
				}
				d1.Low64 %= d2.Low64;
				return;
				IL_024C:
				decimal.DecCalc.VarDecModFull(ref d1, ref d2, num2);
			}

			// Token: 0x06001D0F RID: 7439 RVA: 0x0006DC7C File Offset: 0x0006BE7C
			private unsafe static void VarDecModFull(ref decimal.DecCalc d1, ref decimal.DecCalc d2, int scale)
			{
				uint num = d2.High;
				if (num == 0U)
				{
					num = d2.Mid;
				}
				int num2 = decimal.DecCalc.LeadingZeroCount(num);
				decimal.DecCalc.Buf28 buf;
				buf.Buf24.Low64 = d1.Low64 << num2;
				buf.Buf24.Mid64 = (ulong)d1.Mid + ((ulong)d1.High << 32) >> 32 - num2;
				uint num3 = 3U;
				while (scale < 0)
				{
					uint num4 = ((scale <= -9) ? 1000000000U : decimal.DecCalc.s_powers10[-scale]);
					uint* ptr = (uint*)(&buf);
					ulong num5 = decimal.DecCalc.UInt32x32To64(buf.Buf24.U0, num4);
					buf.Buf24.U0 = (uint)num5;
					int num6 = 1;
					while ((long)num6 <= (long)((ulong)num3))
					{
						num5 >>= 32;
						num5 += decimal.DecCalc.UInt32x32To64(ptr[num6], num4);
						ptr[num6] = (uint)num5;
						num6++;
					}
					if (num5 > 2147483647UL)
					{
						ptr[(IntPtr)((ulong)(num3 += 1U) * 4UL)] = (uint)(num5 >> 32);
					}
					scale += 9;
				}
				if (d2.High == 0U)
				{
					ulong num7 = d2.Low64 << num2;
					switch (num3)
					{
					case 4U:
						goto IL_015A;
					case 5U:
						break;
					case 6U:
						decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U4), num7);
						break;
					default:
						goto IL_016F;
					}
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U3), num7);
					IL_015A:
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U2), num7);
					IL_016F:
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U1), num7);
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf), num7);
					d1.Low64 = buf.Buf24.Low64 >> num2;
					d1.High = 0U;
					return;
				}
				decimal.DecCalc.Buf12 buf2;
				buf2.Low64 = d2.Low64 << num2;
				buf2.U2 = (uint)((ulong)d2.Mid + ((ulong)d2.High << 32) >> 32 - num2);
				switch (num3)
				{
				case 4U:
					goto IL_0225;
				case 5U:
					break;
				case 6U:
					decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf.Buf24.U3), ref buf2);
					break;
				default:
					goto IL_023A;
				}
				decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf.Buf24.U2), ref buf2);
				IL_0225:
				decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf.Buf24.U1), ref buf2);
				IL_023A:
				decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf), ref buf2);
				d1.Low64 = (buf.Buf24.Low64 >> num2) + ((ulong)buf.Buf24.U2 << 32 - num2 << 32);
				d1.High = buf.Buf24.U2 >> num2;
			}

			// Token: 0x06001D10 RID: 7440 RVA: 0x0006DF14 File Offset: 0x0006C114
			internal static void InternalRound(ref decimal.DecCalc d, uint scale, decimal.DecCalc.RoundingMode mode)
			{
				d.uflags -= scale << 16;
				uint num = 0U;
				uint num5;
				while (scale >= 9U)
				{
					scale -= 9U;
					uint num2 = d.uhi;
					uint num4;
					if (num2 == 0U)
					{
						ulong low = d.Low64;
						ulong num3 = low / 1000000000UL;
						d.Low64 = num3;
						num4 = (uint)(low - num3 * 1000000000UL);
					}
					else
					{
						num4 = num2 - (d.uhi = num2 / 1000000000U) * 1000000000U;
						num2 = d.umid;
						if ((num2 | num4) != 0U)
						{
							num4 = num2 - (d.umid = (uint)((((ulong)num4 << 32) | (ulong)num2) / 1000000000UL)) * 1000000000U;
						}
						num2 = d.ulo;
						if ((num2 | num4) != 0U)
						{
							num4 = num2 - (d.ulo = (uint)((((ulong)num4 << 32) | (ulong)num2) / 1000000000UL)) * 1000000000U;
						}
					}
					num5 = 1000000000U;
					if (scale == 0U)
					{
						IL_0194:
						if (mode != decimal.DecCalc.RoundingMode.Truncate)
						{
							if (mode == decimal.DecCalc.RoundingMode.ToEven)
							{
								num4 <<= 1;
								if ((num | (d.ulo & 1U)) != 0U)
								{
									num4 += 1U;
								}
								if (num5 >= num4)
								{
									return;
								}
							}
							else if (mode == decimal.DecCalc.RoundingMode.AwayFromZero)
							{
								num4 <<= 1;
								if (num5 > num4)
								{
									return;
								}
							}
							else if (mode == decimal.DecCalc.RoundingMode.Floor)
							{
								if ((num4 | num) == 0U)
								{
									return;
								}
								if (!d.IsNegative)
								{
									return;
								}
							}
							else if ((num4 | num) == 0U || d.IsNegative)
							{
								return;
							}
							ulong num6 = d.Low64 + 1UL;
							d.Low64 = num6;
							if (num6 == 0UL)
							{
								d.uhi += 1U;
							}
						}
						return;
					}
					num |= num4;
				}
				num5 = decimal.DecCalc.s_powers10[(int)scale];
				uint num7 = d.uhi;
				if (num7 == 0U)
				{
					ulong low2 = d.Low64;
					if (low2 != 0UL)
					{
						ulong num8 = low2 / (ulong)num5;
						d.Low64 = num8;
						uint num4 = (uint)(low2 - num8 * (ulong)num5);
						goto IL_0194;
					}
					if (mode > decimal.DecCalc.RoundingMode.Truncate)
					{
						uint num4 = 0U;
						goto IL_0194;
					}
					return;
				}
				else
				{
					uint num4 = num7 - (d.uhi = num7 / num5) * num5;
					num7 = d.umid;
					if ((num7 | num4) != 0U)
					{
						num4 = num7 - (d.umid = (uint)((((ulong)num4 << 32) | (ulong)num7) / (ulong)num5)) * num5;
					}
					num7 = d.ulo;
					if ((num7 | num4) != 0U)
					{
						num4 = num7 - (d.ulo = (uint)((((ulong)num4 << 32) | (ulong)num7) / (ulong)num5)) * num5;
						goto IL_0194;
					}
					goto IL_0194;
				}
			}

			// Token: 0x06001D11 RID: 7441 RVA: 0x0006E124 File Offset: 0x0006C324
			internal static uint DecDivMod1E9(ref decimal.DecCalc value)
			{
				ulong num = ((ulong)value.uhi << 32) + (ulong)value.umid;
				ulong num2 = num / 1000000000UL;
				value.uhi = (uint)(num2 >> 32);
				value.umid = (uint)num2;
				ulong num3 = (num - (ulong)((uint)num2 * 1000000000U) << 32) + (ulong)value.ulo;
				uint num4 = (uint)(num3 / 1000000000UL);
				value.ulo = num4;
				return (uint)num3 - num4 * 1000000000U;
			}

			// Token: 0x06001D12 RID: 7442 RVA: 0x0006E190 File Offset: 0x0006C390
			// Note: this type is marked as 'beforefieldinit'.
			static DecCalc()
			{
			}

			// Token: 0x040018D5 RID: 6357
			[FieldOffset(0)]
			private uint uflags;

			// Token: 0x040018D6 RID: 6358
			[FieldOffset(4)]
			private uint uhi;

			// Token: 0x040018D7 RID: 6359
			[FieldOffset(8)]
			private uint ulo;

			// Token: 0x040018D8 RID: 6360
			[FieldOffset(12)]
			private uint umid;

			// Token: 0x040018D9 RID: 6361
			[FieldOffset(8)]
			private ulong ulomidLE;

			// Token: 0x040018DA RID: 6362
			private const uint SignMask = 2147483648U;

			// Token: 0x040018DB RID: 6363
			private const uint ScaleMask = 16711680U;

			// Token: 0x040018DC RID: 6364
			private const int DEC_SCALE_MAX = 28;

			// Token: 0x040018DD RID: 6365
			private const uint TenToPowerNine = 1000000000U;

			// Token: 0x040018DE RID: 6366
			private const ulong TenToPowerEighteen = 1000000000000000000UL;

			// Token: 0x040018DF RID: 6367
			private const int MaxInt32Scale = 9;

			// Token: 0x040018E0 RID: 6368
			private const int MaxInt64Scale = 19;

			// Token: 0x040018E1 RID: 6369
			private static readonly uint[] s_powers10 = new uint[] { 1U, 10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U };

			// Token: 0x040018E2 RID: 6370
			private static readonly ulong[] s_ulongPowers10 = new ulong[]
			{
				10UL, 100UL, 1000UL, 10000UL, 100000UL, 1000000UL, 10000000UL, 100000000UL, 1000000000UL, 10000000000UL,
				100000000000UL, 1000000000000UL, 10000000000000UL, 100000000000000UL, 1000000000000000UL, 10000000000000000UL, 100000000000000000UL, 1000000000000000000UL, 10000000000000000000UL
			};

			// Token: 0x040018E3 RID: 6371
			private static readonly double[] s_doublePowers10 = new double[]
			{
				1.0, 10.0, 100.0, 1000.0, 10000.0, 100000.0, 1000000.0, 10000000.0, 100000000.0, 1000000000.0,
				10000000000.0, 100000000000.0, 1000000000000.0, 10000000000000.0, 100000000000000.0, 1000000000000000.0, 10000000000000000.0, 1E+17, 1E+18, 1E+19,
				1E+20, 1E+21, 1E+22, 1E+23, 1E+24, 1E+25, 1E+26, 1E+27, 1E+28, 1E+29,
				1E+30, 1E+31, 1E+32, 1E+33, 1E+34, 1E+35, 1E+36, 1E+37, 1E+38, 1E+39,
				1E+40, 1E+41, 1E+42, 1E+43, 1E+44, 1E+45, 1E+46, 1E+47, 1E+48, 1E+49,
				1E+50, 1E+51, 1E+52, 1E+53, 1E+54, 1E+55, 1E+56, 1E+57, 1E+58, 1E+59,
				1E+60, 1E+61, 1E+62, 1E+63, 1E+64, 1E+65, 1E+66, 1E+67, 1E+68, 1E+69,
				1E+70, 1E+71, 1E+72, 1E+73, 1E+74, 1E+75, 1E+76, 1E+77, 1E+78, 1E+79,
				1E+80
			};

			// Token: 0x040018E4 RID: 6372
			private static readonly decimal.DecCalc.PowerOvfl[] PowerOvflValues = new decimal.DecCalc.PowerOvfl[]
			{
				new decimal.DecCalc.PowerOvfl(429496729U, 2576980377U, 2576980377U),
				new decimal.DecCalc.PowerOvfl(42949672U, 4123168604U, 687194767U),
				new decimal.DecCalc.PowerOvfl(4294967U, 1271310319U, 2645699854U),
				new decimal.DecCalc.PowerOvfl(429496U, 3133608139U, 694066715U),
				new decimal.DecCalc.PowerOvfl(42949U, 2890341191U, 2216890319U),
				new decimal.DecCalc.PowerOvfl(4294U, 4154504685U, 2369172679U),
				new decimal.DecCalc.PowerOvfl(429U, 2133437386U, 4102387834U),
				new decimal.DecCalc.PowerOvfl(42U, 4078814305U, 410238783U)
			};

			// Token: 0x0200024D RID: 589
			internal enum RoundingMode
			{
				// Token: 0x040018E6 RID: 6374
				ToEven,
				// Token: 0x040018E7 RID: 6375
				AwayFromZero,
				// Token: 0x040018E8 RID: 6376
				Truncate,
				// Token: 0x040018E9 RID: 6377
				Floor,
				// Token: 0x040018EA RID: 6378
				Ceiling
			}

			// Token: 0x0200024E RID: 590
			private struct PowerOvfl
			{
				// Token: 0x06001D13 RID: 7443 RVA: 0x0006E2C2 File Offset: 0x0006C4C2
				public PowerOvfl(uint hi, uint mid, uint lo)
				{
					this.Hi = hi;
					this.MidLo = ((ulong)mid << 32) + (ulong)lo;
				}

				// Token: 0x040018EB RID: 6379
				public readonly uint Hi;

				// Token: 0x040018EC RID: 6380
				public readonly ulong MidLo;
			}

			// Token: 0x0200024F RID: 591
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf12
			{
				// Token: 0x17000366 RID: 870
				// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0006E2D9 File Offset: 0x0006C4D9
				// (set) Token: 0x06001D15 RID: 7445 RVA: 0x0006E2FB File Offset: 0x0006C4FB
				public ulong Low64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U1 << 32) | (ulong)this.U0;
						}
						return this.ulo64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.ulo64LE = value;
							return;
						}
						this.U1 = (uint)(value >> 32);
						this.U0 = (uint)value;
					}
				}

				// Token: 0x17000367 RID: 871
				// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0006E31F File Offset: 0x0006C51F
				// (set) Token: 0x06001D17 RID: 7447 RVA: 0x0006E341 File Offset: 0x0006C541
				public ulong High64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U2 << 32) | (ulong)this.U1;
						}
						return this.uhigh64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.uhigh64LE = value;
							return;
						}
						this.U2 = (uint)(value >> 32);
						this.U1 = (uint)value;
					}
				}

				// Token: 0x040018ED RID: 6381
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x040018EE RID: 6382
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x040018EF RID: 6383
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x040018F0 RID: 6384
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x040018F1 RID: 6385
				[FieldOffset(4)]
				private ulong uhigh64LE;
			}

			// Token: 0x02000250 RID: 592
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf16
			{
				// Token: 0x17000368 RID: 872
				// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0006E365 File Offset: 0x0006C565
				// (set) Token: 0x06001D19 RID: 7449 RVA: 0x0006E387 File Offset: 0x0006C587
				public ulong Low64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U1 << 32) | (ulong)this.U0;
						}
						return this.ulo64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.ulo64LE = value;
							return;
						}
						this.U1 = (uint)(value >> 32);
						this.U0 = (uint)value;
					}
				}

				// Token: 0x17000369 RID: 873
				// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0006E3AB File Offset: 0x0006C5AB
				// (set) Token: 0x06001D1B RID: 7451 RVA: 0x0006E3CD File Offset: 0x0006C5CD
				public ulong High64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U3 << 32) | (ulong)this.U2;
						}
						return this.uhigh64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.uhigh64LE = value;
							return;
						}
						this.U3 = (uint)(value >> 32);
						this.U2 = (uint)value;
					}
				}

				// Token: 0x040018F2 RID: 6386
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x040018F3 RID: 6387
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x040018F4 RID: 6388
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x040018F5 RID: 6389
				[FieldOffset(12)]
				public uint U3;

				// Token: 0x040018F6 RID: 6390
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x040018F7 RID: 6391
				[FieldOffset(8)]
				private ulong uhigh64LE;
			}

			// Token: 0x02000251 RID: 593
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf24
			{
				// Token: 0x1700036A RID: 874
				// (get) Token: 0x06001D1C RID: 7452 RVA: 0x0006E3F1 File Offset: 0x0006C5F1
				// (set) Token: 0x06001D1D RID: 7453 RVA: 0x0006E413 File Offset: 0x0006C613
				public ulong Low64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U1 << 32) | (ulong)this.U0;
						}
						return this.ulo64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.ulo64LE = value;
							return;
						}
						this.U1 = (uint)(value >> 32);
						this.U0 = (uint)value;
					}
				}

				// Token: 0x1700036B RID: 875
				// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0006E437 File Offset: 0x0006C637
				// (set) Token: 0x06001D1F RID: 7455 RVA: 0x0006E459 File Offset: 0x0006C659
				public ulong Mid64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U3 << 32) | (ulong)this.U2;
						}
						return this.umid64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.umid64LE = value;
							return;
						}
						this.U3 = (uint)(value >> 32);
						this.U2 = (uint)value;
					}
				}

				// Token: 0x1700036C RID: 876
				// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0006E47D File Offset: 0x0006C67D
				// (set) Token: 0x06001D21 RID: 7457 RVA: 0x0006E49F File Offset: 0x0006C69F
				public ulong High64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U5 << 32) | (ulong)this.U4;
						}
						return this.uhigh64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.uhigh64LE = value;
							return;
						}
						this.U5 = (uint)(value >> 32);
						this.U4 = (uint)value;
					}
				}

				// Token: 0x1700036D RID: 877
				// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00019E33 File Offset: 0x00018033
				public int Length
				{
					get
					{
						return 6;
					}
				}

				// Token: 0x040018F8 RID: 6392
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x040018F9 RID: 6393
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x040018FA RID: 6394
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x040018FB RID: 6395
				[FieldOffset(12)]
				public uint U3;

				// Token: 0x040018FC RID: 6396
				[FieldOffset(16)]
				public uint U4;

				// Token: 0x040018FD RID: 6397
				[FieldOffset(20)]
				public uint U5;

				// Token: 0x040018FE RID: 6398
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x040018FF RID: 6399
				[FieldOffset(8)]
				private ulong umid64LE;

				// Token: 0x04001900 RID: 6400
				[FieldOffset(16)]
				private ulong uhigh64LE;
			}

			// Token: 0x02000252 RID: 594
			private struct Buf28
			{
				// Token: 0x1700036E RID: 878
				// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00029C12 File Offset: 0x00027E12
				public int Length
				{
					get
					{
						return 7;
					}
				}

				// Token: 0x04001901 RID: 6401
				public decimal.DecCalc.Buf24 Buf24;

				// Token: 0x04001902 RID: 6402
				public uint U6;
			}
		}
	}
}
