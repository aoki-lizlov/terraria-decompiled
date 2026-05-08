using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D1 RID: 209
	[Serializable]
	public sealed class DBNull : ISerializable, IConvertible
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x000025BE File Offset: 0x000007BE
		private DBNull()
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001D250 File Offset: 0x0001B450
		private DBNull(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException("Only one DBNull instance may exist, and calls to DBNull deserialization methods are not allowed.");
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001D262 File Offset: 0x0001B462
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetUnitySerializationInfo(info, 2);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00004091 File Offset: 0x00002291
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00004091 File Offset: 0x00002291
		public string ToString(IFormatProvider provider)
		{
			return string.Empty;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00015289 File Offset: 0x00013489
		public TypeCode GetTypeCode()
		{
			return TypeCode.DBNull;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001D26B File Offset: 0x0001B46B
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001D26B File Offset: 0x0001B46B
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001D26B File Offset: 0x0001B46B
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001D26B File Offset: 0x0001B46B
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001D26B File Offset: 0x0001B46B
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001D26B File Offset: 0x0001B46B
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001D26B File Offset: 0x0001B46B
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001D26B File Offset: 0x0001B46B
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001D26B File Offset: 0x0001B46B
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001D26B File Offset: 0x0001B46B
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001D26B File Offset: 0x0001B46B
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001D26B File Offset: 0x0001B46B
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001D26B File Offset: 0x0001B46B
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001D26B File Offset: 0x0001B46B
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001D277 File Offset: 0x0001B477
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001D281 File Offset: 0x0001B481
		// Note: this type is marked as 'beforefieldinit'.
		static DBNull()
		{
		}

		// Token: 0x04000F15 RID: 3861
		public static readonly DBNull Value = new DBNull();
	}
}
