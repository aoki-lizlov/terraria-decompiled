using System;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public readonly struct Boolean : IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x0001996F File Offset: 0x00017B6F
		public override int GetHashCode()
		{
			if (!this)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00019978 File Offset: 0x00017B78
		public override string ToString()
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00019989 File Offset: 0x00017B89
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00019994 File Offset: 0x00017B94
		public bool TryFormat(Span<char> destination, out int charsWritten)
		{
			string text = (this ? "True" : "False");
			if (text.AsSpan().TryCopyTo(destination))
			{
				charsWritten = text.Length;
				return true;
			}
			charsWritten = 0;
			return false;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000199D1 File Offset: 0x00017BD1
		public override bool Equals(object obj)
		{
			return obj is bool && this == (bool)obj;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000199E7 File Offset: 0x00017BE7
		[NonVersionable]
		public bool Equals(bool obj)
		{
			return this == obj;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000199EE File Offset: 0x00017BEE
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is bool))
			{
				throw new ArgumentException("Object must be of type Boolean.");
			}
			if (this == (bool)obj)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00019A1B File Offset: 0x00017C1B
		public int CompareTo(bool value)
		{
			if (this == value)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019A2B File Offset: 0x00017C2B
		public static bool Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return bool.Parse(value.AsSpan());
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019A48 File Offset: 0x00017C48
		public static bool Parse(ReadOnlySpan<char> value)
		{
			bool flag;
			if (!bool.TryParse(value, out flag))
			{
				throw new FormatException("String was not recognized as a valid Boolean.");
			}
			return flag;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019A6B File Offset: 0x00017C6B
		public static bool TryParse(string value, out bool result)
		{
			if (value == null)
			{
				result = false;
				return false;
			}
			return bool.TryParse(value.AsSpan(), out result);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00019A84 File Offset: 0x00017C84
		public static bool TryParse(ReadOnlySpan<char> value, out bool result)
		{
			ReadOnlySpan<char> readOnlySpan = "True".AsSpan();
			if (readOnlySpan.EqualsOrdinalIgnoreCase(value))
			{
				result = true;
				return true;
			}
			ReadOnlySpan<char> readOnlySpan2 = "False".AsSpan();
			if (readOnlySpan2.EqualsOrdinalIgnoreCase(value))
			{
				result = false;
				return true;
			}
			value = bool.TrimWhiteSpaceAndNull(value);
			if (readOnlySpan.EqualsOrdinalIgnoreCase(value))
			{
				result = true;
				return true;
			}
			if (readOnlySpan2.EqualsOrdinalIgnoreCase(value))
			{
				result = false;
				return true;
			}
			result = false;
			return false;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00019AEC File Offset: 0x00017CEC
		private unsafe static ReadOnlySpan<char> TrimWhiteSpaceAndNull(ReadOnlySpan<char> value)
		{
			int num = 0;
			while (num < value.Length && (char.IsWhiteSpace((char)(*value[num])) || *value[num] == 0))
			{
				num++;
			}
			int num2 = value.Length - 1;
			while (num2 >= num && (char.IsWhiteSpace((char)(*value[num2])) || *value[num2] == 0))
			{
				num2--;
			}
			return value.Slice(num, num2 - num + 1);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00019B62 File Offset: 0x00017D62
		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00019B65 File Offset: 0x00017D65
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00019B69 File Offset: 0x00017D69
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Boolean", "Char"));
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00019B84 File Offset: 0x00017D84
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00019B8D File Offset: 0x00017D8D
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00019B96 File Offset: 0x00017D96
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00019B9F File Offset: 0x00017D9F
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00019BA8 File Offset: 0x00017DA8
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00019BB1 File Offset: 0x00017DB1
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00019BBA File Offset: 0x00017DBA
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00019BC3 File Offset: 0x00017DC3
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00019BCC File Offset: 0x00017DCC
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00019BD5 File Offset: 0x00017DD5
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00019BDE File Offset: 0x00017DDE
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00019BE7 File Offset: 0x00017DE7
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Boolean", "DateTime"));
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00019C02 File Offset: 0x00017E02
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00019C12 File Offset: 0x00017E12
		// Note: this type is marked as 'beforefieldinit'.
		static Boolean()
		{
		}

		// Token: 0x04000EF0 RID: 3824
		private readonly bool m_value;

		// Token: 0x04000EF1 RID: 3825
		internal const int True = 1;

		// Token: 0x04000EF2 RID: 3826
		internal const int False = 0;

		// Token: 0x04000EF3 RID: 3827
		internal const string TrueLiteral = "True";

		// Token: 0x04000EF4 RID: 3828
		internal const string FalseLiteral = "False";

		// Token: 0x04000EF5 RID: 3829
		public static readonly string TrueString = "True";

		// Token: 0x04000EF6 RID: 3830
		public static readonly string FalseString = "False";
	}
}
