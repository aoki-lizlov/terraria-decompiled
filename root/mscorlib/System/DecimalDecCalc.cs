using System;

namespace System
{
	// Token: 0x02000199 RID: 409
	internal static class DecimalDecCalc
	{
		// Token: 0x0600132B RID: 4907 RVA: 0x0004E40C File Offset: 0x0004C60C
		private static uint D32DivMod1E9(uint hi32, ref uint lo32)
		{
			ulong num = ((ulong)hi32 << 32) | (ulong)lo32;
			lo32 = (uint)(num / 1000000000UL);
			return (uint)(num % 1000000000UL);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0004E437 File Offset: 0x0004C637
		internal static uint DecDivMod1E9(ref MutableDecimal value)
		{
			return DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(0U, ref value.High), ref value.Mid), ref value.Low);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004E45B File Offset: 0x0004C65B
		internal static void DecAddInt32(ref MutableDecimal value, uint i)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, i) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004E488 File Offset: 0x0004C688
		private static bool D32AddCarry(ref uint value, uint i)
		{
			uint num = value;
			uint num2 = num + i;
			value = num2;
			return num2 < num || num2 < i;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004E4AC File Offset: 0x0004C6AC
		internal static void DecMul10(ref MutableDecimal value)
		{
			MutableDecimal mutableDecimal = value;
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecAdd(ref value, mutableDecimal);
			DecimalDecCalc.DecShiftLeft(ref value);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0004E4DC File Offset: 0x0004C6DC
		private static void DecShiftLeft(ref MutableDecimal value)
		{
			uint num = (((value.Low & 2147483648U) != 0U) ? 1U : 0U);
			uint num2 = (((value.Mid & 2147483648U) != 0U) ? 1U : 0U);
			value.Low <<= 1;
			value.Mid = (value.Mid << 1) | num;
			value.High = (value.High << 1) | num2;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004E540 File Offset: 0x0004C740
		private static void DecAdd(ref MutableDecimal value, MutableDecimal d)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, d.Low) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			if (DecimalDecCalc.D32AddCarry(ref value.Mid, d.Mid))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			DecimalDecCalc.D32AddCarry(ref value.High, d.High);
		}
	}
}
