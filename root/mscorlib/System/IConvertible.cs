using System;

namespace System
{
	// Token: 0x02000102 RID: 258
	[CLSCompliant(false)]
	public interface IConvertible
	{
		// Token: 0x06000A1D RID: 2589
		TypeCode GetTypeCode();

		// Token: 0x06000A1E RID: 2590
		bool ToBoolean(IFormatProvider provider);

		// Token: 0x06000A1F RID: 2591
		char ToChar(IFormatProvider provider);

		// Token: 0x06000A20 RID: 2592
		sbyte ToSByte(IFormatProvider provider);

		// Token: 0x06000A21 RID: 2593
		byte ToByte(IFormatProvider provider);

		// Token: 0x06000A22 RID: 2594
		short ToInt16(IFormatProvider provider);

		// Token: 0x06000A23 RID: 2595
		ushort ToUInt16(IFormatProvider provider);

		// Token: 0x06000A24 RID: 2596
		int ToInt32(IFormatProvider provider);

		// Token: 0x06000A25 RID: 2597
		uint ToUInt32(IFormatProvider provider);

		// Token: 0x06000A26 RID: 2598
		long ToInt64(IFormatProvider provider);

		// Token: 0x06000A27 RID: 2599
		ulong ToUInt64(IFormatProvider provider);

		// Token: 0x06000A28 RID: 2600
		float ToSingle(IFormatProvider provider);

		// Token: 0x06000A29 RID: 2601
		double ToDouble(IFormatProvider provider);

		// Token: 0x06000A2A RID: 2602
		decimal ToDecimal(IFormatProvider provider);

		// Token: 0x06000A2B RID: 2603
		DateTime ToDateTime(IFormatProvider provider);

		// Token: 0x06000A2C RID: 2604
		string ToString(IFormatProvider provider);

		// Token: 0x06000A2D RID: 2605
		object ToType(Type conversionType, IFormatProvider provider);
	}
}
