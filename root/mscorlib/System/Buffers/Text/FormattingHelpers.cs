using System;
using System.Runtime.CompilerServices;

namespace System.Buffers.Text
{
	// Token: 0x02000B4A RID: 2890
	internal static class FormattingHelpers
	{
		// Token: 0x06006A05 RID: 27141 RVA: 0x00167900 File Offset: 0x00165B00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(ulong value)
		{
			int num = 1;
			uint num2;
			if (value >= 10000000UL)
			{
				if (value >= 100000000000000UL)
				{
					num2 = (uint)(value / 100000000000000UL);
					num += 14;
				}
				else
				{
					num2 = (uint)(value / 10000000UL);
					num += 7;
				}
			}
			else
			{
				num2 = (uint)value;
			}
			if (num2 >= 10U)
			{
				if (num2 < 100U)
				{
					num++;
				}
				else if (num2 < 1000U)
				{
					num += 2;
				}
				else if (num2 < 10000U)
				{
					num += 3;
				}
				else if (num2 < 100000U)
				{
					num += 4;
				}
				else if (num2 < 1000000U)
				{
					num += 5;
				}
				else
				{
					num += 6;
				}
			}
			return num;
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x00167998 File Offset: 0x00165B98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(uint value)
		{
			int num = 1;
			if (value >= 100000U)
			{
				value /= 100000U;
				num += 5;
			}
			if (value >= 10U)
			{
				if (value < 100U)
				{
					num++;
				}
				else if (value < 1000U)
				{
					num += 2;
				}
				else if (value < 10000U)
				{
					num += 3;
				}
				else
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x001679F0 File Offset: 0x00165BF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountHexDigits(ulong value)
		{
			int num = 1;
			if (value > (ulong)(-1))
			{
				num += 8;
				value >>= 32;
			}
			if (value > 65535UL)
			{
				num += 4;
				value >>= 16;
			}
			if (value > 255UL)
			{
				num += 2;
				value >>= 8;
			}
			if (value > 15UL)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x00167A40 File Offset: 0x00165C40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char GetSymbolOrDefault(in StandardFormat format, char defaultSymbol)
		{
			char c = format.Symbol;
			if (c == '\0' && format.Precision == 0)
			{
				c = defaultSymbol;
			}
			return c;
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x00167A64 File Offset: 0x00165C64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void FillWithAsciiZeros(Span<byte> buffer)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				*buffer[i] = 48;
			}
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x00167A90 File Offset: 0x00165C90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteHexByte(byte value, Span<byte> buffer, int startingIndex = 0, FormattingHelpers.HexCasing casing = FormattingHelpers.HexCasing.Uppercase)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)((((-num & 28784U) >> 4) + num + (FormattingHelpers.HexCasing)47545U) | casing);
			*buffer[startingIndex + 1] = (byte)num2;
			*buffer[startingIndex] = (byte)(num2 >> 8);
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x00167AE4 File Offset: 0x00165CE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigits(ulong value, Span<byte> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num = 48UL + value;
				value /= 10UL;
				*buffer[i] = (byte)(num - value * 10UL);
			}
			*buffer[0] = (byte)(48UL + value);
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x00167B34 File Offset: 0x00165D34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigitsWithGroupSeparator(ulong value, Span<byte> buffer)
		{
			int num = 0;
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num2 = 48UL + value;
				value /= 10UL;
				*buffer[i] = (byte)(num2 - value * 10UL);
				if (num == 2)
				{
					*buffer[--i] = 44;
					num = 0;
				}
				else
				{
					num++;
				}
			}
			*buffer[0] = (byte)(48UL + value);
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x00167BA0 File Offset: 0x00165DA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteDigits(uint value, Span<byte> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				uint num = 48U + value;
				value /= 10U;
				*buffer[i] = (byte)(num - value * 10U);
			}
			*buffer[0] = (byte)(48U + value);
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x00167BEC File Offset: 0x00165DEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteFourDecimalDigits(uint value, Span<byte> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 3] = (byte)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 2] = (byte)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (byte)(num - value * 10U);
			*buffer[startingIndex] = (byte)(48U + value);
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x00167C60 File Offset: 0x00165E60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteTwoDecimalDigits(uint value, Span<byte> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (byte)(num - value * 10U);
			*buffer[startingIndex] = (byte)(48U + value);
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x00167C98 File Offset: 0x00165E98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong DivMod(ulong numerator, ulong denominator, out ulong modulo)
		{
			ulong num = numerator / denominator;
			modulo = numerator - num * denominator;
			return num;
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x00167CB4 File Offset: 0x00165EB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint DivMod(uint numerator, uint denominator, out uint modulo)
		{
			uint num = numerator / denominator;
			modulo = numerator - num * denominator;
			return num;
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x00167CD0 File Offset: 0x00165ED0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDecimalTrailingZeros(uint value, out uint valueWithoutTrailingZeros)
		{
			int num = 0;
			if (value != 0U)
			{
				for (;;)
				{
					uint num3;
					uint num2 = FormattingHelpers.DivMod(value, 10U, out num3);
					if (num3 != 0U)
					{
						break;
					}
					value = num2;
					num++;
				}
			}
			valueWithoutTrailingZeros = value;
			return num;
		}

		// Token: 0x04003CBE RID: 15550
		internal const string HexTableLower = "0123456789abcdef";

		// Token: 0x04003CBF RID: 15551
		internal const string HexTableUpper = "0123456789ABCDEF";

		// Token: 0x02000B4B RID: 2891
		public enum HexCasing : uint
		{
			// Token: 0x04003CC1 RID: 15553
			Uppercase,
			// Token: 0x04003CC2 RID: 15554
			Lowercase = 8224U
		}
	}
}
