using System;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200002B RID: 43
	public static class MathHelper
	{
		// Token: 0x06000C42 RID: 3138 RVA: 0x000143D3 File Offset: 0x000125D3
		public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
		{
			return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000143E4 File Offset: 0x000125E4
		public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
		{
			double num = (double)(amount * amount);
			double num2 = num * (double)amount;
			return (float)(0.5 * (2.0 * (double)value2 + (double)((value3 - value1) * amount) + (2.0 * (double)value1 - 5.0 * (double)value2 + 4.0 * (double)value3 - (double)value4) * num + (3.0 * (double)value2 - (double)value1 - 3.0 * (double)value3 + (double)value4) * num2));
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0001446B File Offset: 0x0001266B
		public static float Clamp(float value, float min, float max)
		{
			value = ((value > max) ? max : value);
			value = ((value < min) ? min : value);
			return value;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00014482 File Offset: 0x00012682
		public static float Distance(float value1, float value2)
		{
			return Math.Abs(value1 - value2);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0001448C File Offset: 0x0001268C
		public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
		{
			double num = (double)value1;
			double num2 = (double)value2;
			double num3 = (double)tangent1;
			double num4 = (double)tangent2;
			double num5 = (double)amount;
			double num6 = num5 * num5 * num5;
			double num7 = num5 * num5;
			double num8;
			if (MathHelper.WithinEpsilon(amount, 0f))
			{
				num8 = (double)value1;
			}
			else if (MathHelper.WithinEpsilon(amount, 1f))
			{
				num8 = (double)value2;
			}
			else
			{
				num8 = (2.0 * num - 2.0 * num2 + num4 + num3) * num6 + (3.0 * num2 - 3.0 * num - 2.0 * num3 - num4) * num7 + num3 * num5 + num;
			}
			return (float)num8;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00014536 File Offset: 0x00012736
		public static float Lerp(float value1, float value2, float amount)
		{
			return value1 + (value2 - value1) * amount;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0001453F File Offset: 0x0001273F
		public static float Max(float value1, float value2)
		{
			if (value1 <= value2)
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00014548 File Offset: 0x00012748
		public static float Min(float value1, float value2)
		{
			if (value1 >= value2)
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00014554 File Offset: 0x00012754
		public static float SmoothStep(float value1, float value2, float amount)
		{
			float num = MathHelper.Clamp(amount, 0f, 1f);
			return MathHelper.Hermite(value1, 0f, value2, 0f, num);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00014586 File Offset: 0x00012786
		public static float ToDegrees(float radians)
		{
			return (float)((double)radians * 57.29577951308232);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00014595 File Offset: 0x00012795
		public static float ToRadians(float degrees)
		{
			return (float)((double)degrees * 0.017453292519943295);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000145A4 File Offset: 0x000127A4
		public static float WrapAngle(float angle)
		{
			if (angle > -3.1415927f && angle <= 3.1415927f)
			{
				return angle;
			}
			angle %= 6.2831855f;
			if (angle <= -3.1415927f)
			{
				return angle + 6.2831855f;
			}
			if (angle > 3.1415927f)
			{
				return angle - 6.2831855f;
			}
			return angle;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0001446B File Offset: 0x0001266B
		internal static int Clamp(int value, int min, int max)
		{
			value = ((value > max) ? max : value);
			value = ((value < min) ? min : value);
			return value;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000145E2 File Offset: 0x000127E2
		internal static bool WithinEpsilon(float floatA, float floatB)
		{
			return Math.Abs(floatA - floatB) < MathHelper.MachineEpsilonFloat;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000145F4 File Offset: 0x000127F4
		internal static int ClosestMSAAPower(int value)
		{
			if (value == 1)
			{
				return 0;
			}
			int num = value - 1;
			num |= num >> 1;
			num |= num >> 2;
			num |= num >> 4;
			num |= num >> 8;
			num |= num >> 16;
			num++;
			if (num == value)
			{
				return num;
			}
			return num >> 1;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00014638 File Offset: 0x00012838
		private static float GetMachineEpsilonFloat()
		{
			float num = 1f;
			float num2;
			do
			{
				num *= 0.5f;
				num2 = 1f + num;
			}
			while (num2 > 1f);
			return num;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00014664 File Offset: 0x00012864
		// Note: this type is marked as 'beforefieldinit'.
		static MathHelper()
		{
		}

		// Token: 0x04000588 RID: 1416
		public const float E = 2.7182817f;

		// Token: 0x04000589 RID: 1417
		public const float Log10E = 0.4342945f;

		// Token: 0x0400058A RID: 1418
		public const float Log2E = 1.442695f;

		// Token: 0x0400058B RID: 1419
		public const float Pi = 3.1415927f;

		// Token: 0x0400058C RID: 1420
		public const float PiOver2 = 1.5707964f;

		// Token: 0x0400058D RID: 1421
		public const float PiOver4 = 0.7853982f;

		// Token: 0x0400058E RID: 1422
		public const float TwoPi = 6.2831855f;

		// Token: 0x0400058F RID: 1423
		internal static readonly float MachineEpsilonFloat = MathHelper.GetMachineEpsilonFloat();
	}
}
