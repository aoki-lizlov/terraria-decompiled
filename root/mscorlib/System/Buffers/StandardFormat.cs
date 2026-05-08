using System;

namespace System.Buffers
{
	// Token: 0x02000B45 RID: 2885
	public readonly struct StandardFormat : IEquatable<StandardFormat>
	{
		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06006974 RID: 26996 RVA: 0x00165A7F File Offset: 0x00163C7F
		public char Symbol
		{
			get
			{
				return (char)this._format;
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06006975 RID: 26997 RVA: 0x00165A87 File Offset: 0x00163C87
		public byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06006976 RID: 26998 RVA: 0x00165A8F File Offset: 0x00163C8F
		public bool HasPrecision
		{
			get
			{
				return this._precision != byte.MaxValue;
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06006977 RID: 26999 RVA: 0x00165AA1 File Offset: 0x00163CA1
		public bool IsDefault
		{
			get
			{
				return this._format == 0 && this._precision == 0;
			}
		}

		// Token: 0x06006978 RID: 27000 RVA: 0x00165AB6 File Offset: 0x00163CB6
		public StandardFormat(char symbol, byte precision = 255)
		{
			if (precision != 255 && precision > 99)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_PrecisionTooLarge();
			}
			if (symbol != (char)((byte)symbol))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_SymbolDoesNotFit();
			}
			this._format = (byte)symbol;
			this._precision = precision;
		}

		// Token: 0x06006979 RID: 27001 RVA: 0x00165AE3 File Offset: 0x00163CE3
		public static implicit operator StandardFormat(char symbol)
		{
			return new StandardFormat(symbol, byte.MaxValue);
		}

		// Token: 0x0600697A RID: 27002 RVA: 0x00165AF0 File Offset: 0x00163CF0
		public static StandardFormat Parse(ReadOnlySpan<char> format)
		{
			StandardFormat standardFormat;
			StandardFormat.ParseHelper(format, out standardFormat, true);
			return standardFormat;
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x00165B08 File Offset: 0x00163D08
		public static StandardFormat Parse(string format)
		{
			if (format != null)
			{
				return StandardFormat.Parse(format.AsSpan());
			}
			return default(StandardFormat);
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x00165B2D File Offset: 0x00163D2D
		public static bool TryParse(ReadOnlySpan<char> format, out StandardFormat result)
		{
			return StandardFormat.ParseHelper(format, out result, false);
		}

		// Token: 0x0600697D RID: 27005 RVA: 0x00165B38 File Offset: 0x00163D38
		private unsafe static bool ParseHelper(ReadOnlySpan<char> format, out StandardFormat standardFormat, bool throws = false)
		{
			standardFormat = default(StandardFormat);
			if (format.Length == 0)
			{
				return true;
			}
			char c = (char)(*format[0]);
			byte b;
			if (format.Length == 1)
			{
				b = byte.MaxValue;
			}
			else
			{
				uint num = 0U;
				int i = 1;
				while (i < format.Length)
				{
					uint num2 = (uint)(*format[i] - 48);
					if (num2 > 9U)
					{
						if (!throws)
						{
							return false;
						}
						throw new FormatException(SR.Format("Characters following the format symbol must be a number of {0} or less.", 99));
					}
					else
					{
						num = num * 10U + num2;
						if (num > 99U)
						{
							if (!throws)
							{
								return false;
							}
							throw new FormatException(SR.Format("Precision cannot be larger than {0}.", 99));
						}
						else
						{
							i++;
						}
					}
				}
				b = (byte)num;
			}
			standardFormat = new StandardFormat(c, b);
			return true;
		}

		// Token: 0x0600697E RID: 27006 RVA: 0x00165BF4 File Offset: 0x00163DF4
		public override bool Equals(object obj)
		{
			if (obj is StandardFormat)
			{
				StandardFormat standardFormat = (StandardFormat)obj;
				return this.Equals(standardFormat);
			}
			return false;
		}

		// Token: 0x0600697F RID: 27007 RVA: 0x00165C19 File Offset: 0x00163E19
		public override int GetHashCode()
		{
			return this._format.GetHashCode() ^ this._precision.GetHashCode();
		}

		// Token: 0x06006980 RID: 27008 RVA: 0x00165C32 File Offset: 0x00163E32
		public bool Equals(StandardFormat other)
		{
			return this._format == other._format && this._precision == other._precision;
		}

		// Token: 0x06006981 RID: 27009 RVA: 0x00165C54 File Offset: 0x00163E54
		public unsafe override string ToString()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)6], 3);
			int num = this.Format(span);
			return new string(span.Slice(0, num));
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x00165C88 File Offset: 0x00163E88
		internal unsafe int Format(Span<char> destination)
		{
			int num = 0;
			char symbol = this.Symbol;
			if (symbol != '\0' && destination.Length == 3)
			{
				*destination[0] = symbol;
				num = 1;
				uint precision = (uint)this.Precision;
				if (precision != 255U)
				{
					if (precision >= 10U)
					{
						uint num2 = Math.DivRem(precision, 10U, out precision);
						*destination[1] = (char)(48U + num2 % 10U);
						num = 2;
					}
					*destination[num] = (char)(48U + precision);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06006983 RID: 27011 RVA: 0x00165CFC File Offset: 0x00163EFC
		public static bool operator ==(StandardFormat left, StandardFormat right)
		{
			return left.Equals(right);
		}

		// Token: 0x06006984 RID: 27012 RVA: 0x00165D06 File Offset: 0x00163F06
		public static bool operator !=(StandardFormat left, StandardFormat right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04003CAE RID: 15534
		public const byte NoPrecision = 255;

		// Token: 0x04003CAF RID: 15535
		public const byte MaxPrecision = 99;

		// Token: 0x04003CB0 RID: 15536
		private readonly byte _format;

		// Token: 0x04003CB1 RID: 15537
		private readonly byte _precision;

		// Token: 0x04003CB2 RID: 15538
		internal const int FormatStringLength = 3;
	}
}
