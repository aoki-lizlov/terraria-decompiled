using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200011C RID: 284
	public static class MathF
	{
		// Token: 0x06000B57 RID: 2903 RVA: 0x0002AF2D File Offset: 0x0002912D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float x)
		{
			return Math.Abs(x);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002AF38 File Offset: 0x00029138
		public static float IEEERemainder(float x, float y)
		{
			if (float.IsNaN(x))
			{
				return x;
			}
			if (float.IsNaN(y))
			{
				return y;
			}
			float num = x % y;
			if (float.IsNaN(num))
			{
				return float.NaN;
			}
			if (num == 0f && float.IsNegative(x))
			{
				return -0f;
			}
			float num2 = num - MathF.Abs(y) * (float)MathF.Sign(x);
			if (MathF.Abs(num2) == MathF.Abs(num))
			{
				float num3 = x / y;
				if (MathF.Abs(MathF.Round(num3)) > MathF.Abs(num3))
				{
					return num2;
				}
				return num;
			}
			else
			{
				if (MathF.Abs(num2) < MathF.Abs(num))
				{
					return num2;
				}
				return num;
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002AFCC File Offset: 0x000291CC
		public static float Log(float x, float y)
		{
			if (float.IsNaN(x))
			{
				return x;
			}
			if (float.IsNaN(y))
			{
				return y;
			}
			if (y == 1f)
			{
				return float.NaN;
			}
			if (x != 1f && (y == 0f || float.IsPositiveInfinity(y)))
			{
				return float.NaN;
			}
			return MathF.Log(x) / MathF.Log(y);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002B026 File Offset: 0x00029226
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Max(float x, float y)
		{
			return Math.Max(x, y);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002B02F File Offset: 0x0002922F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Min(float x, float y)
		{
			return Math.Min(x, y);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002B038 File Offset: 0x00029238
		[Intrinsic]
		public static float Round(float x)
		{
			if (x == (float)((int)x))
			{
				return x;
			}
			float num = MathF.Floor(x + 0.5f);
			if (x == MathF.Floor(x) + 0.5f && MathF.FMod(num, 2f) != 0f)
			{
				num -= 1f;
			}
			return MathF.CopySign(num, x);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002B08A File Offset: 0x0002928A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Round(float x, int digits)
		{
			return MathF.Round(x, digits, MidpointRounding.ToEven);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002B094 File Offset: 0x00029294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Round(float x, MidpointRounding mode)
		{
			return MathF.Round(x, 0, mode);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002B0A0 File Offset: 0x000292A0
		public unsafe static float Round(float x, int digits, MidpointRounding mode)
		{
			if (digits < 0 || digits > 6)
			{
				throw new ArgumentOutOfRangeException("digits", "Rounding digits must be between 0 and 15, inclusive.");
			}
			if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(SR.Format("The Enum type should contain one and only one instance field.", mode, "MidpointRounding"), "mode");
			}
			if (MathF.Abs(x) < MathF.singleRoundLimit)
			{
				float num = MathF.roundPower10Single[digits];
				x *= num;
				if (mode == MidpointRounding.AwayFromZero)
				{
					float num2 = MathF.ModF(x, &x);
					if (MathF.Abs(num2) >= 0.5f)
					{
						x += (float)MathF.Sign(num2);
					}
				}
				else
				{
					x = MathF.Round(x);
				}
				x /= num;
			}
			return x;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002B13D File Offset: 0x0002933D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(float x)
		{
			return Math.Sign(x);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002B145 File Offset: 0x00029345
		public unsafe static float Truncate(float x)
		{
			MathF.ModF(x, &x);
			return x;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002B154 File Offset: 0x00029354
		private static float CopySign(float x, float y)
		{
			int num = BitConverter.SingleToInt32Bits(x);
			int num2 = BitConverter.SingleToInt32Bits(y);
			if ((num ^ num2) >> 31 != 0)
			{
				return BitConverter.Int32BitsToSingle(num ^ int.MinValue);
			}
			return x;
		}

		// Token: 0x06000B63 RID: 2915
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Acos(float x);

		// Token: 0x06000B64 RID: 2916
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Acosh(float x);

		// Token: 0x06000B65 RID: 2917
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Asin(float x);

		// Token: 0x06000B66 RID: 2918
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Asinh(float x);

		// Token: 0x06000B67 RID: 2919
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Atan(float x);

		// Token: 0x06000B68 RID: 2920
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Atan2(float y, float x);

		// Token: 0x06000B69 RID: 2921
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Atanh(float x);

		// Token: 0x06000B6A RID: 2922
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Cbrt(float x);

		// Token: 0x06000B6B RID: 2923
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Ceiling(float x);

		// Token: 0x06000B6C RID: 2924
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Cos(float x);

		// Token: 0x06000B6D RID: 2925
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Cosh(float x);

		// Token: 0x06000B6E RID: 2926
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Exp(float x);

		// Token: 0x06000B6F RID: 2927
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Floor(float x);

		// Token: 0x06000B70 RID: 2928
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Log(float x);

		// Token: 0x06000B71 RID: 2929
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Log10(float x);

		// Token: 0x06000B72 RID: 2930
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Pow(float x, float y);

		// Token: 0x06000B73 RID: 2931
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Sin(float x);

		// Token: 0x06000B74 RID: 2932
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Sinh(float x);

		// Token: 0x06000B75 RID: 2933
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Sqrt(float x);

		// Token: 0x06000B76 RID: 2934
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Tan(float x);

		// Token: 0x06000B77 RID: 2935
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Tanh(float x);

		// Token: 0x06000B78 RID: 2936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float FMod(float x, float y);

		// Token: 0x06000B79 RID: 2937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern float ModF(float x, float* intptr);

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002B185 File Offset: 0x00029385
		// Note: this type is marked as 'beforefieldinit'.
		static MathF()
		{
		}

		// Token: 0x040010FA RID: 4346
		public const float E = 2.7182817f;

		// Token: 0x040010FB RID: 4347
		public const float PI = 3.1415927f;

		// Token: 0x040010FC RID: 4348
		private const int maxRoundingDigits = 6;

		// Token: 0x040010FD RID: 4349
		private static float[] roundPower10Single = new float[] { 1f, 10f, 100f, 1000f, 10000f, 100000f, 1000000f };

		// Token: 0x040010FE RID: 4350
		private static float singleRoundLimit = 100000000f;
	}
}
