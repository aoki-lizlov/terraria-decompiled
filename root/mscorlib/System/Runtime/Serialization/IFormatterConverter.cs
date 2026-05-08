using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000619 RID: 1561
	[CLSCompliant(false)]
	public interface IFormatterConverter
	{
		// Token: 0x06003BD4 RID: 15316
		object Convert(object value, Type type);

		// Token: 0x06003BD5 RID: 15317
		object Convert(object value, TypeCode typeCode);

		// Token: 0x06003BD6 RID: 15318
		bool ToBoolean(object value);

		// Token: 0x06003BD7 RID: 15319
		char ToChar(object value);

		// Token: 0x06003BD8 RID: 15320
		sbyte ToSByte(object value);

		// Token: 0x06003BD9 RID: 15321
		byte ToByte(object value);

		// Token: 0x06003BDA RID: 15322
		short ToInt16(object value);

		// Token: 0x06003BDB RID: 15323
		ushort ToUInt16(object value);

		// Token: 0x06003BDC RID: 15324
		int ToInt32(object value);

		// Token: 0x06003BDD RID: 15325
		uint ToUInt32(object value);

		// Token: 0x06003BDE RID: 15326
		long ToInt64(object value);

		// Token: 0x06003BDF RID: 15327
		ulong ToUInt64(object value);

		// Token: 0x06003BE0 RID: 15328
		float ToSingle(object value);

		// Token: 0x06003BE1 RID: 15329
		double ToDouble(object value);

		// Token: 0x06003BE2 RID: 15330
		decimal ToDecimal(object value);

		// Token: 0x06003BE3 RID: 15331
		DateTime ToDateTime(object value);

		// Token: 0x06003BE4 RID: 15332
		string ToString(object value);
	}
}
