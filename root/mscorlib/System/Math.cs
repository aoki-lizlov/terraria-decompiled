using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x0200011B RID: 283
	public static class Math
	{
		// Token: 0x06000AFA RID: 2810 RVA: 0x0002A97C File Offset: 0x00028B7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short Abs(short value)
		{
			if (value < 0)
			{
				value = -value;
				if (value < 0)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002A991 File Offset: 0x00028B91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Abs(int value)
		{
			if (value < 0)
			{
				value = -value;
				if (value < 0)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002A9A5 File Offset: 0x00028BA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long Abs(long value)
		{
			if (value < 0L)
			{
				value = -value;
				if (value < 0L)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002A9BB File Offset: 0x00028BBB
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte Abs(sbyte value)
		{
			if (value < 0)
			{
				value = -value;
				if (value < 0)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002A9D0 File Offset: 0x00028BD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Abs(decimal value)
		{
			return decimal.Abs(ref value);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002A9D9 File Offset: 0x00028BD9
		[StackTraceHidden]
		private static void ThrowAbsOverflow()
		{
			throw new OverflowException("Negating the minimum value of a twos complement number is invalid.");
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002A9E5 File Offset: 0x00028BE5
		public static long BigMul(int a, int b)
		{
			return (long)a * (long)b;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002A9EC File Offset: 0x00028BEC
		public static int DivRem(int a, int b, out int result)
		{
			int num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002AA08 File Offset: 0x00028C08
		public static long DivRem(long a, long b, out long result)
		{
			long num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002AA24 File Offset: 0x00028C24
		internal static uint DivRem(uint a, uint b, out uint result)
		{
			uint num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002AA40 File Offset: 0x00028C40
		internal static ulong DivRem(ulong a, ulong b, out ulong result)
		{
			ulong num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002AA59 File Offset: 0x00028C59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Ceiling(decimal d)
		{
			return decimal.Ceiling(d);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002AA61 File Offset: 0x00028C61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte Clamp(byte value, byte min, byte max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<byte>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002AA7B File Offset: 0x00028C7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Clamp(decimal value, decimal min, decimal max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<decimal>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002AAA4 File Offset: 0x00028CA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Clamp(double value, double min, double max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<double>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002AABE File Offset: 0x00028CBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short Clamp(short value, short min, short max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<short>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002AAD8 File Offset: 0x00028CD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Clamp(int value, int min, int max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<int>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002AAF2 File Offset: 0x00028CF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long Clamp(long value, long min, long max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<long>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002AB0C File Offset: 0x00028D0C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte Clamp(sbyte value, sbyte min, sbyte max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<sbyte>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002AB26 File Offset: 0x00028D26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp(float value, float min, float max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<float>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002AB40 File Offset: 0x00028D40
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort Clamp(ushort value, ushort min, ushort max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<ushort>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002AB5A File Offset: 0x00028D5A
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Clamp(uint value, uint min, uint max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<uint>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002AB74 File Offset: 0x00028D74
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Clamp(ulong value, ulong min, ulong max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<ulong>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002AB8E File Offset: 0x00028D8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Floor(decimal d)
		{
			return decimal.Floor(d);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002AB98 File Offset: 0x00028D98
		public static double IEEERemainder(double x, double y)
		{
			if (double.IsNaN(x))
			{
				return x;
			}
			if (double.IsNaN(y))
			{
				return y;
			}
			double num = x % y;
			if (double.IsNaN(num))
			{
				return double.NaN;
			}
			if (num == 0.0 && double.IsNegative(x))
			{
				return -0.0;
			}
			double num2 = num - Math.Abs(y) * (double)Math.Sign(x);
			if (Math.Abs(num2) == Math.Abs(num))
			{
				double num3 = x / y;
				if (Math.Abs(Math.Round(num3)) > Math.Abs(num3))
				{
					return num2;
				}
				return num;
			}
			else
			{
				if (Math.Abs(num2) < Math.Abs(num))
				{
					return num2;
				}
				return num;
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002AC38 File Offset: 0x00028E38
		public static double Log(double a, double newBase)
		{
			if (double.IsNaN(a))
			{
				return a;
			}
			if (double.IsNaN(newBase))
			{
				return newBase;
			}
			if (newBase == 1.0)
			{
				return double.NaN;
			}
			if (a != 1.0 && (newBase == 0.0 || double.IsPositiveInfinity(newBase)))
			{
				return double.NaN;
			}
			return Math.Log(a) / Math.Log(newBase);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002ACA6 File Offset: 0x00028EA6
		[NonVersionable]
		public static byte Max(byte val1, byte val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002ACAF File Offset: 0x00028EAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static decimal Max(decimal val1, decimal val2)
		{
			return *decimal.Max(ref val1, ref val2);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002ACBF File Offset: 0x00028EBF
		public static double Max(double val1, double val2)
		{
			if (val1 > val2)
			{
				return val1;
			}
			if (double.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002ACA6 File Offset: 0x00028EA6
		[NonVersionable]
		public static short Max(short val1, short val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002ACA6 File Offset: 0x00028EA6
		[NonVersionable]
		public static int Max(int val1, int val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002ACA6 File Offset: 0x00028EA6
		[NonVersionable]
		public static long Max(long val1, long val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002ACA6 File Offset: 0x00028EA6
		[CLSCompliant(false)]
		[NonVersionable]
		public static sbyte Max(sbyte val1, sbyte val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002ACD2 File Offset: 0x00028ED2
		public static float Max(float val1, float val2)
		{
			if (val1 > val2)
			{
				return val1;
			}
			if (float.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002ACA6 File Offset: 0x00028EA6
		[CLSCompliant(false)]
		[NonVersionable]
		public static ushort Max(ushort val1, ushort val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002ACE5 File Offset: 0x00028EE5
		[CLSCompliant(false)]
		[NonVersionable]
		public static uint Max(uint val1, uint val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002ACE5 File Offset: 0x00028EE5
		[CLSCompliant(false)]
		[NonVersionable]
		public static ulong Max(ulong val1, ulong val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002ACEE File Offset: 0x00028EEE
		[NonVersionable]
		public static byte Min(byte val1, byte val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002ACF7 File Offset: 0x00028EF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static decimal Min(decimal val1, decimal val2)
		{
			return *decimal.Min(ref val1, ref val2);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002AD07 File Offset: 0x00028F07
		public static double Min(double val1, double val2)
		{
			if (val1 < val2)
			{
				return val1;
			}
			if (double.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002ACEE File Offset: 0x00028EEE
		[NonVersionable]
		public static short Min(short val1, short val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002ACEE File Offset: 0x00028EEE
		[NonVersionable]
		public static int Min(int val1, int val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002ACEE File Offset: 0x00028EEE
		[NonVersionable]
		public static long Min(long val1, long val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002ACEE File Offset: 0x00028EEE
		[CLSCompliant(false)]
		[NonVersionable]
		public static sbyte Min(sbyte val1, sbyte val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002AD1A File Offset: 0x00028F1A
		public static float Min(float val1, float val2)
		{
			if (val1 < val2)
			{
				return val1;
			}
			if (float.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002ACEE File Offset: 0x00028EEE
		[CLSCompliant(false)]
		[NonVersionable]
		public static ushort Min(ushort val1, ushort val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002AD2D File Offset: 0x00028F2D
		[CLSCompliant(false)]
		[NonVersionable]
		public static uint Min(uint val1, uint val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002AD2D File Offset: 0x00028F2D
		[CLSCompliant(false)]
		[NonVersionable]
		public static ulong Min(ulong val1, ulong val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002AD36 File Offset: 0x00028F36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Round(decimal d)
		{
			return decimal.Round(d, 0);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002AD3F File Offset: 0x00028F3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Round(decimal d, int decimals)
		{
			return decimal.Round(d, decimals);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002AD48 File Offset: 0x00028F48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Round(decimal d, MidpointRounding mode)
		{
			return decimal.Round(d, 0, mode);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002AD52 File Offset: 0x00028F52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Round(decimal d, int decimals, MidpointRounding mode)
		{
			return decimal.Round(d, decimals, mode);
		}

		// Token: 0x06000B2E RID: 2862
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Round(double a);

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002AD5C File Offset: 0x00028F5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Round(double value, int digits)
		{
			return Math.Round(value, digits, MidpointRounding.ToEven);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002AD66 File Offset: 0x00028F66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Round(double value, MidpointRounding mode)
		{
			return Math.Round(value, 0, mode);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002AD70 File Offset: 0x00028F70
		public unsafe static double Round(double value, int digits, MidpointRounding mode)
		{
			if (digits < 0 || digits > 15)
			{
				throw new ArgumentOutOfRangeException("digits", "Rounding digits must be between 0 and 15, inclusive.");
			}
			if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not valid for this usage of the type {1}.", mode, "MidpointRounding"), "mode");
			}
			if (Math.Abs(value) < Math.doubleRoundLimit)
			{
				double num = Math.roundPower10Double[digits];
				value *= num;
				if (mode == MidpointRounding.AwayFromZero)
				{
					double num2 = Math.ModF(value, &value);
					if (Math.Abs(num2) >= 0.5)
					{
						value += (double)Math.Sign(num2);
					}
				}
				else
				{
					value = Math.Round(value);
				}
				value /= num;
			}
			return value;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002AE12 File Offset: 0x00029012
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(decimal value)
		{
			return decimal.Sign(ref value);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002AE1B File Offset: 0x0002901B
		public static int Sign(double value)
		{
			if (value < 0.0)
			{
				return -1;
			}
			if (value > 0.0)
			{
				return 1;
			}
			if (value == 0.0)
			{
				return 0;
			}
			throw new ArithmeticException("Function does not accept floating point Not-a-Number values.");
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002AE51 File Offset: 0x00029051
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(short value)
		{
			return Math.Sign((int)value);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002AE59 File Offset: 0x00029059
		public static int Sign(int value)
		{
			return (value >> 31) | (int)((uint)(-(uint)value) >> 31);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002AE65 File Offset: 0x00029065
		public static int Sign(long value)
		{
			return (int)((value >> 63) | (long)((ulong)(-(ulong)value) >> 63));
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002AE51 File Offset: 0x00029051
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(sbyte value)
		{
			return Math.Sign((int)value);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002AE72 File Offset: 0x00029072
		public static int Sign(float value)
		{
			if (value < 0f)
			{
				return -1;
			}
			if (value > 0f)
			{
				return 1;
			}
			if (value == 0f)
			{
				return 0;
			}
			throw new ArithmeticException("Function does not accept floating point Not-a-Number values.");
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002AE9C File Offset: 0x0002909C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Truncate(decimal d)
		{
			return decimal.Truncate(d);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002AEA4 File Offset: 0x000290A4
		public unsafe static double Truncate(double d)
		{
			Math.ModF(d, &d);
			return d;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002AEB4 File Offset: 0x000290B4
		private static double copysign(double x, double y)
		{
			long num = BitConverter.DoubleToInt64Bits(x);
			long num2 = BitConverter.DoubleToInt64Bits(y);
			if ((num ^ num2) >> 63 != 0L)
			{
				return BitConverter.Int64BitsToDouble(num ^ long.MinValue);
			}
			return x;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002AEE9 File Offset: 0x000290E9
		private static void ThrowMinMaxException<T>(T min, T max)
		{
			throw new ArgumentException(SR.Format("'{0}' cannot be greater than {1}.", min, max));
		}

		// Token: 0x06000B3D RID: 2877
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Abs(double value);

		// Token: 0x06000B3E RID: 2878
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Abs(float value);

		// Token: 0x06000B3F RID: 2879
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Acos(double d);

		// Token: 0x06000B40 RID: 2880
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Acosh(double d);

		// Token: 0x06000B41 RID: 2881
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Asin(double d);

		// Token: 0x06000B42 RID: 2882
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Asinh(double d);

		// Token: 0x06000B43 RID: 2883
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan(double d);

		// Token: 0x06000B44 RID: 2884
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan2(double y, double x);

		// Token: 0x06000B45 RID: 2885
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atanh(double d);

		// Token: 0x06000B46 RID: 2886
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cbrt(double d);

		// Token: 0x06000B47 RID: 2887
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Ceiling(double a);

		// Token: 0x06000B48 RID: 2888
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cos(double d);

		// Token: 0x06000B49 RID: 2889
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cosh(double value);

		// Token: 0x06000B4A RID: 2890
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exp(double d);

		// Token: 0x06000B4B RID: 2891
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Floor(double d);

		// Token: 0x06000B4C RID: 2892
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log(double d);

		// Token: 0x06000B4D RID: 2893
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log10(double d);

		// Token: 0x06000B4E RID: 2894
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Pow(double x, double y);

		// Token: 0x06000B4F RID: 2895
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sin(double a);

		// Token: 0x06000B50 RID: 2896
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sinh(double value);

		// Token: 0x06000B51 RID: 2897
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sqrt(double d);

		// Token: 0x06000B52 RID: 2898
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tan(double a);

		// Token: 0x06000B53 RID: 2899
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tanh(double value);

		// Token: 0x06000B54 RID: 2900
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double FMod(double x, double y);

		// Token: 0x06000B55 RID: 2901
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern double ModF(double x, double* intptr);

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002AF06 File Offset: 0x00029106
		// Note: this type is marked as 'beforefieldinit'.
		static Math()
		{
		}

		// Token: 0x040010F5 RID: 4341
		public const double E = 2.718281828459045;

		// Token: 0x040010F6 RID: 4342
		public const double PI = 3.141592653589793;

		// Token: 0x040010F7 RID: 4343
		private const int maxRoundingDigits = 15;

		// Token: 0x040010F8 RID: 4344
		private static double doubleRoundLimit = 10000000000000000.0;

		// Token: 0x040010F9 RID: 4345
		private static double[] roundPower10Double = new double[]
		{
			1.0, 10.0, 100.0, 1000.0, 10000.0, 100000.0, 1000000.0, 10000000.0, 100000000.0, 1000000000.0,
			10000000000.0, 100000000000.0, 1000000000000.0, 10000000000000.0, 100000000000000.0, 1000000000000000.0
		};
	}
}
