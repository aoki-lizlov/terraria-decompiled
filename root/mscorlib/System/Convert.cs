using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000CE RID: 206
	public static class Convert
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x0001AC3C File Offset: 0x00018E3C
		private unsafe static bool TryDecodeFromUtf16(ReadOnlySpan<char> utf16, Span<byte> bytes, out int consumed, out int written)
		{
			ref char reference = ref MemoryMarshal.GetReference<char>(utf16);
			ref byte reference2 = ref MemoryMarshal.GetReference<byte>(bytes);
			int num = utf16.Length & -4;
			int length = bytes.Length;
			int i = 0;
			int num2 = 0;
			if (utf16.Length != 0)
			{
				ref sbyte ptr = ref Convert.s_decodingMap[0];
				int num3;
				if (length >= (num >> 2) * 3)
				{
					num3 = num - 4;
				}
				else
				{
					num3 = length / 3 * 4;
				}
				while (i < num3)
				{
					int num4 = Convert.Decode(Unsafe.Add<char>(ref reference, i), ref ptr);
					if (num4 < 0)
					{
						IL_0201:
						consumed = i;
						written = num2;
						return false;
					}
					Convert.WriteThreeLowOrderBytes(Unsafe.Add<byte>(ref reference2, num2), num4);
					num2 += 3;
					i += 4;
				}
				if (num3 != num - 4 || i == num)
				{
					goto IL_0201;
				}
				int num5 = (int)(*Unsafe.Add<char>(ref reference, num - 4));
				int num6 = (int)(*Unsafe.Add<char>(ref reference, num - 3));
				int num7 = (int)(*Unsafe.Add<char>(ref reference, num - 2));
				int num8 = (int)(*Unsafe.Add<char>(ref reference, num - 1));
				if (((long)(num5 | num6 | num7 | num8) & (long)((ulong)(-256))) != 0L)
				{
					goto IL_0201;
				}
				num5 = (int)(*Unsafe.Add<sbyte>(ref ptr, num5));
				num6 = (int)(*Unsafe.Add<sbyte>(ref ptr, num6));
				num5 <<= 18;
				num6 <<= 12;
				num5 |= num6;
				if (num8 != 61)
				{
					num7 = (int)(*Unsafe.Add<sbyte>(ref ptr, num7));
					num8 = (int)(*Unsafe.Add<sbyte>(ref ptr, num8));
					num7 <<= 6;
					num5 |= num8;
					num5 |= num7;
					if (num5 < 0 || num2 > length - 3)
					{
						goto IL_0201;
					}
					Convert.WriteThreeLowOrderBytes(Unsafe.Add<byte>(ref reference2, num2), num5);
					num2 += 3;
				}
				else if (num7 != 61)
				{
					num7 = (int)(*Unsafe.Add<sbyte>(ref ptr, num7));
					num7 <<= 6;
					num5 |= num7;
					if (num5 < 0 || num2 > length - 2)
					{
						goto IL_0201;
					}
					*Unsafe.Add<byte>(ref reference2, num2) = (byte)(num5 >> 16);
					*Unsafe.Add<byte>(ref reference2, num2 + 1) = (byte)(num5 >> 8);
					num2 += 2;
				}
				else
				{
					if (num5 < 0 || num2 > length - 1)
					{
						goto IL_0201;
					}
					*Unsafe.Add<byte>(ref reference2, num2) = (byte)(num5 >> 16);
					num2++;
				}
				i += 4;
				if (num != utf16.Length)
				{
					goto IL_0201;
				}
			}
			consumed = i;
			written = num2;
			return true;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001AE54 File Offset: 0x00019054
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int Decode(ref char encodedChars, ref sbyte decodingMap)
		{
			int num = (int)encodedChars;
			int num2 = (int)(*Unsafe.Add<char>(ref encodedChars, 1));
			int num3 = (int)(*Unsafe.Add<char>(ref encodedChars, 2));
			int num4 = (int)(*Unsafe.Add<char>(ref encodedChars, 3));
			if (((long)(num | num2 | num3 | num4) & (long)((ulong)(-256))) != 0L)
			{
				return -1;
			}
			num = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num));
			num2 = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num2));
			num3 = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num3));
			num4 = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num4));
			num <<= 18;
			num2 <<= 12;
			num3 <<= 6;
			num |= num4;
			num2 |= num3;
			return num | num2;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001AED1 File Offset: 0x000190D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteThreeLowOrderBytes(ref byte destination, int value)
		{
			destination = (byte)(value >> 16);
			*Unsafe.Add<byte>(ref destination, 1) = (byte)(value >> 8);
			*Unsafe.Add<byte>(ref destination, 2) = (byte)value;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001AEF0 File Offset: 0x000190F0
		public static TypeCode GetTypeCode(object value)
		{
			if (value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001AF14 File Offset: 0x00019114
		public static bool IsDBNull(object value)
		{
			if (value == global::System.DBNull.Value)
			{
				return true;
			}
			IConvertible convertible = value as IConvertible;
			return convertible != null && convertible.GetTypeCode() == TypeCode.DBNull;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001AF40 File Offset: 0x00019140
		public static object ChangeType(object value, TypeCode typeCode)
		{
			return Convert.ChangeType(value, typeCode, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001AF50 File Offset: 0x00019150
		public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider)
		{
			if (value == null && (typeCode == TypeCode.Empty || typeCode == TypeCode.String || typeCode == TypeCode.Object))
			{
				return null;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible == null)
			{
				throw new InvalidCastException("Object must implement IConvertible.");
			}
			switch (typeCode)
			{
			case TypeCode.Empty:
				throw new InvalidCastException("Object cannot be cast to Empty.");
			case TypeCode.Object:
				return value;
			case TypeCode.DBNull:
				throw new InvalidCastException("Object cannot be cast to DBNull.");
			case TypeCode.Boolean:
				return convertible.ToBoolean(provider);
			case TypeCode.Char:
				return convertible.ToChar(provider);
			case TypeCode.SByte:
				return convertible.ToSByte(provider);
			case TypeCode.Byte:
				return convertible.ToByte(provider);
			case TypeCode.Int16:
				return convertible.ToInt16(provider);
			case TypeCode.UInt16:
				return convertible.ToUInt16(provider);
			case TypeCode.Int32:
				return convertible.ToInt32(provider);
			case TypeCode.UInt32:
				return convertible.ToUInt32(provider);
			case TypeCode.Int64:
				return convertible.ToInt64(provider);
			case TypeCode.UInt64:
				return convertible.ToUInt64(provider);
			case TypeCode.Single:
				return convertible.ToSingle(provider);
			case TypeCode.Double:
				return convertible.ToDouble(provider);
			case TypeCode.Decimal:
				return convertible.ToDecimal(provider);
			case TypeCode.DateTime:
				return convertible.ToDateTime(provider);
			case TypeCode.String:
				return convertible.ToString(provider);
			}
			throw new ArgumentException("Unknown TypeCode value.");
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001B0BC File Offset: 0x000192BC
		internal static object DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (value.GetType() == targetType)
			{
				return value;
			}
			if (targetType == Convert.ConvertTypes[3])
			{
				return value.ToBoolean(provider);
			}
			if (targetType == Convert.ConvertTypes[4])
			{
				return value.ToChar(provider);
			}
			if (targetType == Convert.ConvertTypes[5])
			{
				return value.ToSByte(provider);
			}
			if (targetType == Convert.ConvertTypes[6])
			{
				return value.ToByte(provider);
			}
			if (targetType == Convert.ConvertTypes[7])
			{
				return value.ToInt16(provider);
			}
			if (targetType == Convert.ConvertTypes[8])
			{
				return value.ToUInt16(provider);
			}
			if (targetType == Convert.ConvertTypes[9])
			{
				return value.ToInt32(provider);
			}
			if (targetType == Convert.ConvertTypes[10])
			{
				return value.ToUInt32(provider);
			}
			if (targetType == Convert.ConvertTypes[11])
			{
				return value.ToInt64(provider);
			}
			if (targetType == Convert.ConvertTypes[12])
			{
				return value.ToUInt64(provider);
			}
			if (targetType == Convert.ConvertTypes[13])
			{
				return value.ToSingle(provider);
			}
			if (targetType == Convert.ConvertTypes[14])
			{
				return value.ToDouble(provider);
			}
			if (targetType == Convert.ConvertTypes[15])
			{
				return value.ToDecimal(provider);
			}
			if (targetType == Convert.ConvertTypes[16])
			{
				return value.ToDateTime(provider);
			}
			if (targetType == Convert.ConvertTypes[18])
			{
				return value.ToString(provider);
			}
			if (targetType == Convert.ConvertTypes[1])
			{
				return value;
			}
			if (targetType == Convert.EnumType)
			{
				return (Enum)value;
			}
			if (targetType == Convert.ConvertTypes[2])
			{
				throw new InvalidCastException("Object cannot be cast to DBNull.");
			}
			if (targetType == Convert.ConvertTypes[0])
			{
				throw new InvalidCastException("Object cannot be cast to Empty.");
			}
			throw new InvalidCastException(string.Format("Invalid cast from '{0}' to '{1}'.", value.GetType().FullName, targetType.FullName));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001B2AA File Offset: 0x000194AA
		public static object ChangeType(object value, Type conversionType)
		{
			return Convert.ChangeType(value, conversionType, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001B2B8 File Offset: 0x000194B8
		public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
		{
			if (conversionType == null)
			{
				throw new ArgumentNullException("conversionType");
			}
			if (value == null)
			{
				if (conversionType.IsValueType)
				{
					throw new InvalidCastException("Null object cannot be converted to a value type.");
				}
				return null;
			}
			else
			{
				IConvertible convertible = value as IConvertible;
				if (convertible == null)
				{
					if (value.GetType() == conversionType)
					{
						return value;
					}
					throw new InvalidCastException("Object must implement IConvertible.");
				}
				else
				{
					if (conversionType == Convert.ConvertTypes[3])
					{
						return convertible.ToBoolean(provider);
					}
					if (conversionType == Convert.ConvertTypes[4])
					{
						return convertible.ToChar(provider);
					}
					if (conversionType == Convert.ConvertTypes[5])
					{
						return convertible.ToSByte(provider);
					}
					if (conversionType == Convert.ConvertTypes[6])
					{
						return convertible.ToByte(provider);
					}
					if (conversionType == Convert.ConvertTypes[7])
					{
						return convertible.ToInt16(provider);
					}
					if (conversionType == Convert.ConvertTypes[8])
					{
						return convertible.ToUInt16(provider);
					}
					if (conversionType == Convert.ConvertTypes[9])
					{
						return convertible.ToInt32(provider);
					}
					if (conversionType == Convert.ConvertTypes[10])
					{
						return convertible.ToUInt32(provider);
					}
					if (conversionType == Convert.ConvertTypes[11])
					{
						return convertible.ToInt64(provider);
					}
					if (conversionType == Convert.ConvertTypes[12])
					{
						return convertible.ToUInt64(provider);
					}
					if (conversionType == Convert.ConvertTypes[13])
					{
						return convertible.ToSingle(provider);
					}
					if (conversionType == Convert.ConvertTypes[14])
					{
						return convertible.ToDouble(provider);
					}
					if (conversionType == Convert.ConvertTypes[15])
					{
						return convertible.ToDecimal(provider);
					}
					if (conversionType == Convert.ConvertTypes[16])
					{
						return convertible.ToDateTime(provider);
					}
					if (conversionType == Convert.ConvertTypes[18])
					{
						return convertible.ToString(provider);
					}
					if (conversionType == Convert.ConvertTypes[1])
					{
						return value;
					}
					return convertible.ToType(conversionType, provider);
				}
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001B481 File Offset: 0x00019681
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCharOverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a character.");
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001B48D File Offset: 0x0001968D
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowByteOverflowException()
		{
			throw new OverflowException("Value was either too large or too small for an unsigned byte.");
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001B499 File Offset: 0x00019699
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowSByteOverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a signed byte.");
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001B4A5 File Offset: 0x000196A5
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowInt16OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for an Int16.");
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001B4B1 File Offset: 0x000196B1
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowUInt16OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a UInt16.");
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001B4BD File Offset: 0x000196BD
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowInt32OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for an Int32.");
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001B4C9 File Offset: 0x000196C9
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowUInt32OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a UInt32.");
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001B4D5 File Offset: 0x000196D5
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowInt64OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for an Int64.");
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001B4E1 File Offset: 0x000196E1
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowUInt64OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a UInt64.");
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001B4ED File Offset: 0x000196ED
		public static bool ToBoolean(object value)
		{
			return value != null && ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001B500 File Offset: 0x00019700
		public static bool ToBoolean(object value, IFormatProvider provider)
		{
			return value != null && ((IConvertible)value).ToBoolean(provider);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000025CE File Offset: 0x000007CE
		public static bool ToBoolean(bool value)
		{
			return value;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001B513 File Offset: 0x00019713
		[CLSCompliant(false)]
		public static bool ToBoolean(sbyte value)
		{
			return value != 0;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001B519 File Offset: 0x00019719
		public static bool ToBoolean(char value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001B513 File Offset: 0x00019713
		public static bool ToBoolean(byte value)
		{
			return value > 0;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001B513 File Offset: 0x00019713
		public static bool ToBoolean(short value)
		{
			return value != 0;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001B513 File Offset: 0x00019713
		[CLSCompliant(false)]
		public static bool ToBoolean(ushort value)
		{
			return value > 0;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001B513 File Offset: 0x00019713
		public static bool ToBoolean(int value)
		{
			return value != 0;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001B513 File Offset: 0x00019713
		[CLSCompliant(false)]
		public static bool ToBoolean(uint value)
		{
			return value > 0U;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001B527 File Offset: 0x00019727
		public static bool ToBoolean(long value)
		{
			return value != 0L;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001B527 File Offset: 0x00019727
		[CLSCompliant(false)]
		public static bool ToBoolean(ulong value)
		{
			return value > 0UL;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001B52E File Offset: 0x0001972E
		public static bool ToBoolean(string value)
		{
			return value != null && bool.Parse(value);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001B52E File Offset: 0x0001972E
		public static bool ToBoolean(string value, IFormatProvider provider)
		{
			return value != null && bool.Parse(value);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001B53B File Offset: 0x0001973B
		public static bool ToBoolean(float value)
		{
			return value != 0f;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001B548 File Offset: 0x00019748
		public static bool ToBoolean(double value)
		{
			return value != 0.0;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001B559 File Offset: 0x00019759
		public static bool ToBoolean(decimal value)
		{
			return value != 0m;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001B566 File Offset: 0x00019766
		public static bool ToBoolean(DateTime value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001B574 File Offset: 0x00019774
		public static char ToChar(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(null);
			}
			return '\0';
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001B587 File Offset: 0x00019787
		public static char ToChar(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(provider);
			}
			return '\0';
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001B59A File Offset: 0x0001979A
		public static char ToChar(bool value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000025CE File Offset: 0x000007CE
		public static char ToChar(char value)
		{
			return value;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001B5A8 File Offset: 0x000197A8
		[CLSCompliant(false)]
		public static char ToChar(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000025CE File Offset: 0x000007CE
		public static char ToChar(byte value)
		{
			return (char)value;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001B5A8 File Offset: 0x000197A8
		public static char ToChar(short value)
		{
			if (value < 0)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static char ToChar(ushort value)
		{
			return (char)value;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001B5B5 File Offset: 0x000197B5
		public static char ToChar(int value)
		{
			if (value < 0 || value > 65535)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001B5CA File Offset: 0x000197CA
		[CLSCompliant(false)]
		public static char ToChar(uint value)
		{
			if (value > 65535U)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001B5DB File Offset: 0x000197DB
		public static char ToChar(long value)
		{
			if (value < 0L || value > 65535L)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001B5F2 File Offset: 0x000197F2
		[CLSCompliant(false)]
		public static char ToChar(ulong value)
		{
			if (value > 65535UL)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001B604 File Offset: 0x00019804
		public static char ToChar(string value)
		{
			return Convert.ToChar(value, null);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001B60D File Offset: 0x0001980D
		public static char ToChar(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length != 1)
			{
				throw new FormatException("String must be exactly one character long.");
			}
			return value[0];
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001B638 File Offset: 0x00019838
		public static char ToChar(float value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001B646 File Offset: 0x00019846
		public static char ToChar(double value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001B654 File Offset: 0x00019854
		public static char ToChar(decimal value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001B662 File Offset: 0x00019862
		public static char ToChar(DateTime value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001B670 File Offset: 0x00019870
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(null);
			}
			return 0;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001B683 File Offset: 0x00019883
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(provider);
			}
			return 0;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001B696 File Offset: 0x00019896
		[CLSCompliant(false)]
		public static sbyte ToSByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static sbyte ToSByte(sbyte value)
		{
			return value;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001B69E File Offset: 0x0001989E
		[CLSCompliant(false)]
		public static sbyte ToSByte(char value)
		{
			if (value > '\u007f')
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001B69E File Offset: 0x0001989E
		[CLSCompliant(false)]
		public static sbyte ToSByte(byte value)
		{
			if (value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001B6AC File Offset: 0x000198AC
		[CLSCompliant(false)]
		public static sbyte ToSByte(short value)
		{
			if (value < -128 || value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001B69E File Offset: 0x0001989E
		[CLSCompliant(false)]
		public static sbyte ToSByte(ushort value)
		{
			if (value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001B6AC File Offset: 0x000198AC
		[CLSCompliant(false)]
		public static sbyte ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001B6BF File Offset: 0x000198BF
		[CLSCompliant(false)]
		public static sbyte ToSByte(uint value)
		{
			if ((ulong)value > 127UL)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001B6CF File Offset: 0x000198CF
		[CLSCompliant(false)]
		public static sbyte ToSByte(long value)
		{
			if (value < -128L || value > 127L)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001B6E4 File Offset: 0x000198E4
		[CLSCompliant(false)]
		public static sbyte ToSByte(ulong value)
		{
			if (value > 127UL)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001B6F3 File Offset: 0x000198F3
		[CLSCompliant(false)]
		public static sbyte ToSByte(float value)
		{
			return Convert.ToSByte((double)value);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001B6FC File Offset: 0x000198FC
		[CLSCompliant(false)]
		public static sbyte ToSByte(double value)
		{
			return Convert.ToSByte(Convert.ToInt32(value));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001B709 File Offset: 0x00019909
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			return decimal.ToSByte(decimal.Round(value, 0));
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001B717 File Offset: 0x00019917
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return sbyte.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001B729 File Offset: 0x00019929
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, IFormatProvider provider)
		{
			return sbyte.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001B733 File Offset: 0x00019933
		[CLSCompliant(false)]
		public static sbyte ToSByte(DateTime value)
		{
			return ((IConvertible)value).ToSByte(null);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001B741 File Offset: 0x00019941
		public static byte ToByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(null);
			}
			return 0;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001B754 File Offset: 0x00019954
		public static byte ToByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(provider);
			}
			return 0;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001B696 File Offset: 0x00019896
		public static byte ToByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000025CE File Offset: 0x000007CE
		public static byte ToByte(byte value)
		{
			return value;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001B767 File Offset: 0x00019967
		public static byte ToByte(char value)
		{
			if (value > 'ÿ')
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001B778 File Offset: 0x00019978
		[CLSCompliant(false)]
		public static byte ToByte(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001B785 File Offset: 0x00019985
		public static byte ToByte(short value)
		{
			if (value < 0 || value > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001B767 File Offset: 0x00019967
		[CLSCompliant(false)]
		public static byte ToByte(ushort value)
		{
			if (value > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001B785 File Offset: 0x00019985
		public static byte ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001B79A File Offset: 0x0001999A
		[CLSCompliant(false)]
		public static byte ToByte(uint value)
		{
			if (value > 255U)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001B7AB File Offset: 0x000199AB
		public static byte ToByte(long value)
		{
			if (value < 0L || value > 255L)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001B7C2 File Offset: 0x000199C2
		[CLSCompliant(false)]
		public static byte ToByte(ulong value)
		{
			if (value > 255UL)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001B7D4 File Offset: 0x000199D4
		public static byte ToByte(float value)
		{
			return Convert.ToByte((double)value);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001B7DD File Offset: 0x000199DD
		public static byte ToByte(double value)
		{
			return Convert.ToByte(Convert.ToInt32(value));
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001B7EA File Offset: 0x000199EA
		public static byte ToByte(decimal value)
		{
			return decimal.ToByte(decimal.Round(value, 0));
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001B7F8 File Offset: 0x000199F8
		public static byte ToByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001B80A File Offset: 0x00019A0A
		public static byte ToByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001B819 File Offset: 0x00019A19
		public static byte ToByte(DateTime value)
		{
			return ((IConvertible)value).ToByte(null);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001B827 File Offset: 0x00019A27
		public static short ToInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(null);
			}
			return 0;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001B83A File Offset: 0x00019A3A
		public static short ToInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(provider);
			}
			return 0;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001B696 File Offset: 0x00019896
		public static short ToInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001B84D File Offset: 0x00019A4D
		public static short ToInt16(char value)
		{
			if (value > '翿')
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static short ToInt16(sbyte value)
		{
			return (short)value;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000025CE File Offset: 0x000007CE
		public static short ToInt16(byte value)
		{
			return (short)value;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001B84D File Offset: 0x00019A4D
		[CLSCompliant(false)]
		public static short ToInt16(ushort value)
		{
			if (value > 32767)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001B85E File Offset: 0x00019A5E
		public static short ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001B877 File Offset: 0x00019A77
		[CLSCompliant(false)]
		public static short ToInt16(uint value)
		{
			if ((ulong)value > 32767UL)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000025CE File Offset: 0x000007CE
		public static short ToInt16(short value)
		{
			return value;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001B88A File Offset: 0x00019A8A
		public static short ToInt16(long value)
		{
			if (value < -32768L || value > 32767L)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001B8A5 File Offset: 0x00019AA5
		[CLSCompliant(false)]
		public static short ToInt16(ulong value)
		{
			if (value > 32767UL)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001B8B7 File Offset: 0x00019AB7
		public static short ToInt16(float value)
		{
			return Convert.ToInt16((double)value);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001B8C0 File Offset: 0x00019AC0
		public static short ToInt16(double value)
		{
			return Convert.ToInt16(Convert.ToInt32(value));
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001B8CD File Offset: 0x00019ACD
		public static short ToInt16(decimal value)
		{
			return decimal.ToInt16(decimal.Round(value, 0));
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001B8DB File Offset: 0x00019ADB
		public static short ToInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001B8ED File Offset: 0x00019AED
		public static short ToInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001B8FC File Offset: 0x00019AFC
		public static short ToInt16(DateTime value)
		{
			return ((IConvertible)value).ToInt16(null);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001B90A File Offset: 0x00019B0A
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(null);
			}
			return 0;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001B91D File Offset: 0x00019B1D
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(provider);
			}
			return 0;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001B696 File Offset: 0x00019896
		[CLSCompliant(false)]
		public static ushort ToUInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static ushort ToUInt16(char value)
		{
			return (ushort)value;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001B930 File Offset: 0x00019B30
		[CLSCompliant(false)]
		public static ushort ToUInt16(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static ushort ToUInt16(byte value)
		{
			return (ushort)value;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001B930 File Offset: 0x00019B30
		[CLSCompliant(false)]
		public static ushort ToUInt16(short value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001B93D File Offset: 0x00019B3D
		[CLSCompliant(false)]
		public static ushort ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static ushort ToUInt16(ushort value)
		{
			return value;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001B952 File Offset: 0x00019B52
		[CLSCompliant(false)]
		public static ushort ToUInt16(uint value)
		{
			if (value > 65535U)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001B963 File Offset: 0x00019B63
		[CLSCompliant(false)]
		public static ushort ToUInt16(long value)
		{
			if (value < 0L || value > 65535L)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001B97A File Offset: 0x00019B7A
		[CLSCompliant(false)]
		public static ushort ToUInt16(ulong value)
		{
			if (value > 65535UL)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001B98C File Offset: 0x00019B8C
		[CLSCompliant(false)]
		public static ushort ToUInt16(float value)
		{
			return Convert.ToUInt16((double)value);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001B995 File Offset: 0x00019B95
		[CLSCompliant(false)]
		public static ushort ToUInt16(double value)
		{
			return Convert.ToUInt16(Convert.ToInt32(value));
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001B9A2 File Offset: 0x00019BA2
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			return decimal.ToUInt16(decimal.Round(value, 0));
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001B9B0 File Offset: 0x00019BB0
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001B9C2 File Offset: 0x00019BC2
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001B9D1 File Offset: 0x00019BD1
		[CLSCompliant(false)]
		public static ushort ToUInt16(DateTime value)
		{
			return ((IConvertible)value).ToUInt16(null);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001B9DF File Offset: 0x00019BDF
		public static int ToInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(null);
			}
			return 0;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001B9F2 File Offset: 0x00019BF2
		public static int ToInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(provider);
			}
			return 0;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001B696 File Offset: 0x00019896
		public static int ToInt32(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000025CE File Offset: 0x000007CE
		public static int ToInt32(char value)
		{
			return (int)value;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static int ToInt32(sbyte value)
		{
			return (int)value;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000025CE File Offset: 0x000007CE
		public static int ToInt32(byte value)
		{
			return (int)value;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000025CE File Offset: 0x000007CE
		public static int ToInt32(short value)
		{
			return (int)value;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static int ToInt32(ushort value)
		{
			return (int)value;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001BA05 File Offset: 0x00019C05
		[CLSCompliant(false)]
		public static int ToInt32(uint value)
		{
			if (value > 2147483647U)
			{
				Convert.ThrowInt32OverflowException();
			}
			return (int)value;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000025CE File Offset: 0x000007CE
		public static int ToInt32(int value)
		{
			return value;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001BA15 File Offset: 0x00019C15
		public static int ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				Convert.ThrowInt32OverflowException();
			}
			return (int)value;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001BA30 File Offset: 0x00019C30
		[CLSCompliant(false)]
		public static int ToInt32(ulong value)
		{
			if (value > 2147483647UL)
			{
				Convert.ThrowInt32OverflowException();
			}
			return (int)value;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001BA42 File Offset: 0x00019C42
		public static int ToInt32(float value)
		{
			return Convert.ToInt32((double)value);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001BA4C File Offset: 0x00019C4C
		public static int ToInt32(double value)
		{
			if (value >= 0.0)
			{
				if (value < 2147483647.5)
				{
					int num = (int)value;
					double num2 = value - (double)num;
					if (num2 > 0.5 || (num2 == 0.5 && (num & 1) != 0))
					{
						num++;
					}
					return num;
				}
			}
			else if (value >= -2147483648.5)
			{
				int num3 = (int)value;
				double num4 = value - (double)num3;
				if (num4 < -0.5 || (num4 == -0.5 && (num3 & 1) != 0))
				{
					num3--;
				}
				return num3;
			}
			throw new OverflowException("Value was either too large or too small for an Int32.");
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001BADD File Offset: 0x00019CDD
		public static int ToInt32(decimal value)
		{
			return decimal.ToInt32(decimal.Round(value, 0));
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001BAEB File Offset: 0x00019CEB
		public static int ToInt32(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001BAFD File Offset: 0x00019CFD
		public static int ToInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001BB0C File Offset: 0x00019D0C
		public static int ToInt32(DateTime value)
		{
			return ((IConvertible)value).ToInt32(null);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001BB1A File Offset: 0x00019D1A
		[CLSCompliant(false)]
		public static uint ToUInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(null);
			}
			return 0U;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001BB2D File Offset: 0x00019D2D
		[CLSCompliant(false)]
		public static uint ToUInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(provider);
			}
			return 0U;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001B696 File Offset: 0x00019896
		[CLSCompliant(false)]
		public static uint ToUInt32(bool value)
		{
			if (!value)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static uint ToUInt32(char value)
		{
			return (uint)value;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001BB40 File Offset: 0x00019D40
		[CLSCompliant(false)]
		public static uint ToUInt32(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static uint ToUInt32(byte value)
		{
			return (uint)value;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001BB40 File Offset: 0x00019D40
		[CLSCompliant(false)]
		public static uint ToUInt32(short value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static uint ToUInt32(ushort value)
		{
			return (uint)value;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001BB40 File Offset: 0x00019D40
		[CLSCompliant(false)]
		public static uint ToUInt32(int value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static uint ToUInt32(uint value)
		{
			return value;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001BB4C File Offset: 0x00019D4C
		[CLSCompliant(false)]
		public static uint ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)(-1)))
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001BB5F File Offset: 0x00019D5F
		[CLSCompliant(false)]
		public static uint ToUInt32(ulong value)
		{
			if (value > (ulong)(-1))
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001BB6D File Offset: 0x00019D6D
		[CLSCompliant(false)]
		public static uint ToUInt32(float value)
		{
			return Convert.ToUInt32((double)value);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001BB78 File Offset: 0x00019D78
		[CLSCompliant(false)]
		public static uint ToUInt32(double value)
		{
			if (value >= -0.5 && value < 4294967295.5)
			{
				uint num = (uint)value;
				double num2 = value - num;
				if (num2 > 0.5 || (num2 == 0.5 && (num & 1U) != 0U))
				{
					num += 1U;
				}
				return num;
			}
			throw new OverflowException("Value was either too large or too small for a UInt32.");
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001BBD3 File Offset: 0x00019DD3
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal value)
		{
			return decimal.ToUInt32(decimal.Round(value, 0));
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001BBE1 File Offset: 0x00019DE1
		[CLSCompliant(false)]
		public static uint ToUInt32(string value)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001BBF3 File Offset: 0x00019DF3
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001BC02 File Offset: 0x00019E02
		[CLSCompliant(false)]
		public static uint ToUInt32(DateTime value)
		{
			return ((IConvertible)value).ToUInt32(null);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001BC10 File Offset: 0x00019E10
		public static long ToInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(null);
			}
			return 0L;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001BC24 File Offset: 0x00019E24
		public static long ToInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(provider);
			}
			return 0L;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001BC38 File Offset: 0x00019E38
		public static long ToInt64(bool value)
		{
			return value ? 1L : 0L;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001BC42 File Offset: 0x00019E42
		public static long ToInt64(char value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001BC46 File Offset: 0x00019E46
		[CLSCompliant(false)]
		public static long ToInt64(sbyte value)
		{
			return (long)value;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001BC42 File Offset: 0x00019E42
		public static long ToInt64(byte value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001BC46 File Offset: 0x00019E46
		public static long ToInt64(short value)
		{
			return (long)value;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001BC42 File Offset: 0x00019E42
		[CLSCompliant(false)]
		public static long ToInt64(ushort value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001BC46 File Offset: 0x00019E46
		public static long ToInt64(int value)
		{
			return (long)value;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001BC42 File Offset: 0x00019E42
		[CLSCompliant(false)]
		public static long ToInt64(uint value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001BC4A File Offset: 0x00019E4A
		[CLSCompliant(false)]
		public static long ToInt64(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				Convert.ThrowInt64OverflowException();
			}
			return (long)value;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000025CE File Offset: 0x000007CE
		public static long ToInt64(long value)
		{
			return value;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001BC5E File Offset: 0x00019E5E
		public static long ToInt64(float value)
		{
			return Convert.ToInt64((double)value);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001BC67 File Offset: 0x00019E67
		public static long ToInt64(double value)
		{
			return checked((long)Math.Round(value));
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001BC70 File Offset: 0x00019E70
		public static long ToInt64(decimal value)
		{
			return decimal.ToInt64(decimal.Round(value, 0));
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001BC7E File Offset: 0x00019E7E
		public static long ToInt64(string value)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001BC91 File Offset: 0x00019E91
		public static long ToInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001BCA1 File Offset: 0x00019EA1
		public static long ToInt64(DateTime value)
		{
			return ((IConvertible)value).ToInt64(null);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001BCAF File Offset: 0x00019EAF
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(null);
			}
			return 0UL;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001BCC3 File Offset: 0x00019EC3
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(provider);
			}
			return 0UL;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001BCD7 File Offset: 0x00019ED7
		[CLSCompliant(false)]
		public static ulong ToUInt64(bool value)
		{
			if (!value)
			{
				return 0UL;
			}
			return 1UL;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001BC42 File Offset: 0x00019E42
		[CLSCompliant(false)]
		public static ulong ToUInt64(char value)
		{
			return (ulong)value;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001BCE1 File Offset: 0x00019EE1
		[CLSCompliant(false)]
		public static ulong ToUInt64(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001BC42 File Offset: 0x00019E42
		[CLSCompliant(false)]
		public static ulong ToUInt64(byte value)
		{
			return (ulong)value;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001BCE1 File Offset: 0x00019EE1
		[CLSCompliant(false)]
		public static ulong ToUInt64(short value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001BC42 File Offset: 0x00019E42
		[CLSCompliant(false)]
		public static ulong ToUInt64(ushort value)
		{
			return (ulong)value;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001BCE1 File Offset: 0x00019EE1
		[CLSCompliant(false)]
		public static ulong ToUInt64(int value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001BC42 File Offset: 0x00019E42
		[CLSCompliant(false)]
		public static ulong ToUInt64(uint value)
		{
			return (ulong)value;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001BCEE File Offset: 0x00019EEE
		[CLSCompliant(false)]
		public static ulong ToUInt64(long value)
		{
			if (value < 0L)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)value;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000025CE File Offset: 0x000007CE
		[CLSCompliant(false)]
		public static ulong ToUInt64(ulong value)
		{
			return value;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001BCFB File Offset: 0x00019EFB
		[CLSCompliant(false)]
		public static ulong ToUInt64(float value)
		{
			return Convert.ToUInt64((double)value);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001BD04 File Offset: 0x00019F04
		[CLSCompliant(false)]
		public static ulong ToUInt64(double value)
		{
			return checked((ulong)Math.Round(value));
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001BD0D File Offset: 0x00019F0D
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal value)
		{
			return decimal.ToUInt64(decimal.Round(value, 0));
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001BD1B File Offset: 0x00019F1B
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001BD2E File Offset: 0x00019F2E
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, NumberStyles.Integer, provider);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001BD3E File Offset: 0x00019F3E
		[CLSCompliant(false)]
		public static ulong ToUInt64(DateTime value)
		{
			return ((IConvertible)value).ToUInt64(null);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001BD4C File Offset: 0x00019F4C
		public static float ToSingle(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(null);
			}
			return 0f;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001BD63 File Offset: 0x00019F63
		public static float ToSingle(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(provider);
			}
			return 0f;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001BD7A File Offset: 0x00019F7A
		[CLSCompliant(false)]
		public static float ToSingle(sbyte value)
		{
			return (float)value;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001BD7A File Offset: 0x00019F7A
		public static float ToSingle(byte value)
		{
			return (float)value;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001BD7E File Offset: 0x00019F7E
		public static float ToSingle(char value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001BD7A File Offset: 0x00019F7A
		public static float ToSingle(short value)
		{
			return (float)value;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001BD7A File Offset: 0x00019F7A
		[CLSCompliant(false)]
		public static float ToSingle(ushort value)
		{
			return (float)value;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001BD7A File Offset: 0x00019F7A
		public static float ToSingle(int value)
		{
			return (float)value;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001BD8C File Offset: 0x00019F8C
		[CLSCompliant(false)]
		public static float ToSingle(uint value)
		{
			return value;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001BD7A File Offset: 0x00019F7A
		public static float ToSingle(long value)
		{
			return (float)value;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001BD8C File Offset: 0x00019F8C
		[CLSCompliant(false)]
		public static float ToSingle(ulong value)
		{
			return value;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000025CE File Offset: 0x000007CE
		public static float ToSingle(float value)
		{
			return value;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001BD7A File Offset: 0x00019F7A
		public static float ToSingle(double value)
		{
			return (float)value;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001BD91 File Offset: 0x00019F91
		public static float ToSingle(decimal value)
		{
			return (float)value;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001BD9A File Offset: 0x00019F9A
		public static float ToSingle(string value)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		public static float ToSingle(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001BDC7 File Offset: 0x00019FC7
		public static float ToSingle(bool value)
		{
			return (float)(value ? 1 : 0);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001BDD1 File Offset: 0x00019FD1
		public static float ToSingle(DateTime value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001BDDF File Offset: 0x00019FDF
		public static double ToDouble(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(null);
			}
			return 0.0;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001BDFA File Offset: 0x00019FFA
		public static double ToDouble(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(provider);
			}
			return 0.0;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001BE15 File Offset: 0x0001A015
		[CLSCompliant(false)]
		public static double ToDouble(sbyte value)
		{
			return (double)value;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001BE15 File Offset: 0x0001A015
		public static double ToDouble(byte value)
		{
			return (double)value;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001BE15 File Offset: 0x0001A015
		public static double ToDouble(short value)
		{
			return (double)value;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001BE19 File Offset: 0x0001A019
		public static double ToDouble(char value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001BE15 File Offset: 0x0001A015
		[CLSCompliant(false)]
		public static double ToDouble(ushort value)
		{
			return (double)value;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001BE15 File Offset: 0x0001A015
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001BE27 File Offset: 0x0001A027
		[CLSCompliant(false)]
		public static double ToDouble(uint value)
		{
			return value;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001BE15 File Offset: 0x0001A015
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001BE27 File Offset: 0x0001A027
		[CLSCompliant(false)]
		public static double ToDouble(ulong value)
		{
			return value;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001BE15 File Offset: 0x0001A015
		public static double ToDouble(float value)
		{
			return (double)value;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000025CE File Offset: 0x000007CE
		public static double ToDouble(double value)
		{
			return value;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001BE2C File Offset: 0x0001A02C
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001BE35 File Offset: 0x0001A035
		public static double ToDouble(string value)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001BE4F File Offset: 0x0001A04F
		public static double ToDouble(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001BE6A File Offset: 0x0001A06A
		public static double ToDouble(bool value)
		{
			return (double)(value ? 1 : 0);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001BE74 File Offset: 0x0001A074
		public static double ToDouble(DateTime value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001BE82 File Offset: 0x0001A082
		public static decimal ToDecimal(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return 0m;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001BE99 File Offset: 0x0001A099
		public static decimal ToDecimal(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(provider);
			}
			return 0m;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		[CLSCompliant(false)]
		public static decimal ToDecimal(sbyte value)
		{
			return value;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001BEB8 File Offset: 0x0001A0B8
		public static decimal ToDecimal(byte value)
		{
			return value;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		public static decimal ToDecimal(char value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001BECE File Offset: 0x0001A0CE
		public static decimal ToDecimal(short value)
		{
			return value;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001BED6 File Offset: 0x0001A0D6
		[CLSCompliant(false)]
		public static decimal ToDecimal(ushort value)
		{
			return value;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001BEDE File Offset: 0x0001A0DE
		public static decimal ToDecimal(int value)
		{
			return value;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001BEE6 File Offset: 0x0001A0E6
		[CLSCompliant(false)]
		public static decimal ToDecimal(uint value)
		{
			return value;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001BEEE File Offset: 0x0001A0EE
		public static decimal ToDecimal(long value)
		{
			return value;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001BEF6 File Offset: 0x0001A0F6
		[CLSCompliant(false)]
		public static decimal ToDecimal(ulong value)
		{
			return value;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001BEFE File Offset: 0x0001A0FE
		public static decimal ToDecimal(float value)
		{
			return (decimal)value;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001BF06 File Offset: 0x0001A106
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001BF0E File Offset: 0x0001A10E
		public static decimal ToDecimal(string value)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001BF24 File Offset: 0x0001A124
		public static decimal ToDecimal(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, NumberStyles.Number, provider);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000025CE File Offset: 0x000007CE
		public static decimal ToDecimal(decimal value)
		{
			return value;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001BF38 File Offset: 0x0001A138
		public static decimal ToDecimal(bool value)
		{
			return value ? 1 : 0;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001BF46 File Offset: 0x0001A146
		public static decimal ToDecimal(DateTime value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000025CE File Offset: 0x000007CE
		public static DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001BF54 File Offset: 0x0001A154
		public static DateTime ToDateTime(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(null);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001BF6B File Offset: 0x0001A16B
		public static DateTime ToDateTime(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(provider);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001BF82 File Offset: 0x0001A182
		public static DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001BF9A File Offset: 0x0001A19A
		public static DateTime ToDateTime(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, provider);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001BFAE File Offset: 0x0001A1AE
		[CLSCompliant(false)]
		public static DateTime ToDateTime(sbyte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001BFBC File Offset: 0x0001A1BC
		public static DateTime ToDateTime(byte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001BFCA File Offset: 0x0001A1CA
		public static DateTime ToDateTime(short value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001BFD8 File Offset: 0x0001A1D8
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ushort value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001BFE6 File Offset: 0x0001A1E6
		public static DateTime ToDateTime(int value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
		[CLSCompliant(false)]
		public static DateTime ToDateTime(uint value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001C002 File Offset: 0x0001A202
		public static DateTime ToDateTime(long value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001C010 File Offset: 0x0001A210
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ulong value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001C01E File Offset: 0x0001A21E
		public static DateTime ToDateTime(bool value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001C02C File Offset: 0x0001A22C
		public static DateTime ToDateTime(char value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001C03A File Offset: 0x0001A23A
		public static DateTime ToDateTime(float value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001C048 File Offset: 0x0001A248
		public static DateTime ToDateTime(double value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001C056 File Offset: 0x0001A256
		public static DateTime ToDateTime(decimal value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001C064 File Offset: 0x0001A264
		public static string ToString(object value)
		{
			return Convert.ToString(value, null);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001C070 File Offset: 0x0001A270
		public static string ToString(object value, IFormatProvider provider)
		{
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.ToString(provider);
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(null, provider);
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001C0B1 File Offset: 0x0001A2B1
		public static string ToString(bool value)
		{
			return value.ToString();
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001C0B1 File Offset: 0x0001A2B1
		public static string ToString(bool value, IFormatProvider provider)
		{
			return value.ToString();
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001C0BA File Offset: 0x0001A2BA
		public static string ToString(char value)
		{
			return char.ToString(value);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001C0C2 File Offset: 0x0001A2C2
		public static string ToString(char value, IFormatProvider provider)
		{
			return value.ToString();
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001C0CB File Offset: 0x0001A2CB
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001C0D9 File Offset: 0x0001A2D9
		[CLSCompliant(false)]
		public static string ToString(sbyte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001C0E3 File Offset: 0x0001A2E3
		public static string ToString(byte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001C0F1 File Offset: 0x0001A2F1
		public static string ToString(byte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001C0FB File Offset: 0x0001A2FB
		public static string ToString(short value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001C109 File Offset: 0x0001A309
		public static string ToString(short value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001C113 File Offset: 0x0001A313
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001C121 File Offset: 0x0001A321
		[CLSCompliant(false)]
		public static string ToString(ushort value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001C12B File Offset: 0x0001A32B
		public static string ToString(int value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001C139 File Offset: 0x0001A339
		public static string ToString(int value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001C143 File Offset: 0x0001A343
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001C151 File Offset: 0x0001A351
		[CLSCompliant(false)]
		public static string ToString(uint value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001C15B File Offset: 0x0001A35B
		public static string ToString(long value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001C169 File Offset: 0x0001A369
		public static string ToString(long value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001C173 File Offset: 0x0001A373
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001C181 File Offset: 0x0001A381
		[CLSCompliant(false)]
		public static string ToString(ulong value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001C18B File Offset: 0x0001A38B
		public static string ToString(float value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001C199 File Offset: 0x0001A399
		public static string ToString(float value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001C1A3 File Offset: 0x0001A3A3
		public static string ToString(double value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001C1B1 File Offset: 0x0001A3B1
		public static string ToString(double value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001C1BB File Offset: 0x0001A3BB
		public static string ToString(decimal value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001C1C9 File Offset: 0x0001A3C9
		public static string ToString(decimal value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001C1D3 File Offset: 0x0001A3D3
		public static string ToString(DateTime value)
		{
			return value.ToString();
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001C1DC File Offset: 0x0001A3DC
		public static string ToString(DateTime value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000025CE File Offset: 0x000007CE
		public static string ToString(string value)
		{
			return value;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000025CE File Offset: 0x000007CE
		public static string ToString(string value, IFormatProvider provider)
		{
			return value;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		public static byte ToByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4608);
			if (num < 0 || num > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)num;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001C23C File Offset: 0x0001A43C
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 5120);
			if (fromBase != 10 && num <= 255)
			{
				return (sbyte)num;
			}
			if (num < -128 || num > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)num;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001C2A0 File Offset: 0x0001A4A0
		public static short ToInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 6144);
			if (fromBase != 10 && num <= 65535)
			{
				return (short)num;
			}
			if (num < -32768 || num > 32767)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)num;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001C308 File Offset: 0x0001A508
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4608);
			if (num < 0 || num > 65535)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)num;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001C35C File Offset: 0x0001A55C
		public static int ToInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			return ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4096);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001C391 File Offset: 0x0001A591
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0U;
			}
			return (uint)ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4608);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001C3C6 File Offset: 0x0001A5C6
		public static long ToInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0L;
			}
			return ParseNumbers.StringToLong(value.AsSpan(), fromBase, 4096);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0UL;
			}
			return (ulong)ParseNumbers.StringToLong(value.AsSpan(), fromBase, 4608);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001C432 File Offset: 0x0001A632
		public static string ToString(byte value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 64);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001C45D File Offset: 0x0001A65D
		public static string ToString(short value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 128);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001C48B File Offset: 0x0001A68B
		public static string ToString(int value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			return ParseNumbers.IntToString(value, toBase, -1, ' ', 0);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001C4B5 File Offset: 0x0001A6B5
		public static string ToString(long value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			return ParseNumbers.LongToString(value, toBase, -1, ' ', 0);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001C4DF File Offset: 0x0001A6DF
		public static string ToBase64String(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(new ReadOnlySpan<byte>(inArray), Base64FormattingOptions.None);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001C4FB File Offset: 0x0001A6FB
		public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(new ReadOnlySpan<byte>(inArray), options);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001C517 File Offset: 0x0001A717
		public static string ToBase64String(byte[] inArray, int offset, int length)
		{
			return Convert.ToBase64String(inArray, offset, length, Base64FormattingOptions.None);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001C524 File Offset: 0x0001A724
		public static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Value must be positive.");
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", "Offset and length must refer to a position in the string.");
			}
			return Convert.ToBase64String(new ReadOnlySpan<byte>(inArray, offset, length), options);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001C590 File Offset: 0x0001A790
		public unsafe static string ToBase64String(ReadOnlySpan<byte> bytes, Base64FormattingOptions options = Base64FormattingOptions.None)
		{
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(string.Format("Illegal enum value: {0}.", (int)options), "options");
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			bool flag = options == Base64FormattingOptions.InsertLineBreaks;
			string text = string.FastAllocateString(Convert.ToBase64_CalculateAndValidateOutputLength(bytes.Length, flag));
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (string text2 = text)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					Convert.ConvertToBase64Array(ptr2, ptr, 0, bytes.Length, flag);
				}
			}
			return text;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001C619 File Offset: 0x0001A819
		public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
		{
			return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, Base64FormattingOptions.None);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001C628 File Offset: 0x0001A828
		public unsafe static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (outArray == null)
			{
				throw new ArgumentNullException("outArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (offsetIn < 0)
			{
				throw new ArgumentOutOfRangeException("offsetIn", "Value must be positive.");
			}
			if (offsetOut < 0)
			{
				throw new ArgumentOutOfRangeException("offsetOut", "Value must be positive.");
			}
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(string.Format("Illegal enum value: {0}.", (int)options), "options");
			}
			int num = inArray.Length;
			if (offsetIn > num - length)
			{
				throw new ArgumentOutOfRangeException("offsetIn", "Offset and length must refer to a position in the string.");
			}
			if (num == 0)
			{
				return 0;
			}
			bool flag = options == Base64FormattingOptions.InsertLineBreaks;
			int num2 = outArray.Length;
			int num3 = Convert.ToBase64_CalculateAndValidateOutputLength(length, flag);
			if (offsetOut > num2 - num3)
			{
				throw new ArgumentOutOfRangeException("offsetOut", "Either offset did not refer to a position in the string, or there is an insufficient length of destination character array.");
			}
			int num4;
			fixed (char* ptr = &outArray[offsetOut])
			{
				char* ptr2 = ptr;
				fixed (byte* ptr3 = &inArray[0])
				{
					byte* ptr4 = ptr3;
					num4 = Convert.ConvertToBase64Array(ptr2, ptr4, offsetIn, length, flag);
				}
			}
			return num4;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001C72C File Offset: 0x0001A92C
		public unsafe static bool TryToBase64Chars(ReadOnlySpan<byte> bytes, Span<char> chars, out int charsWritten, Base64FormattingOptions options = Base64FormattingOptions.None)
		{
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(string.Format("Illegal enum value: {0}.", (int)options), "options");
			}
			if (bytes.Length == 0)
			{
				charsWritten = 0;
				return true;
			}
			bool flag = options == Base64FormattingOptions.InsertLineBreaks;
			if (Convert.ToBase64_CalculateAndValidateOutputLength(bytes.Length, flag) > chars.Length)
			{
				charsWritten = 0;
				return false;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* ptr2 = reference2;
					charsWritten = Convert.ConvertToBase64Array(ptr, ptr2, 0, bytes.Length, flag);
					return true;
				}
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001C7B4 File Offset: 0x0001A9B4
		private unsafe static int ConvertToBase64Array(char* outChars, byte* inData, int offset, int length, bool insertLineBreaks)
		{
			int num = length % 3;
			int num2 = offset + (length - num);
			int num3 = 0;
			int num4 = 0;
			fixed (char* ptr = &Convert.base64Table[0])
			{
				char* ptr2 = ptr;
				int i;
				for (i = offset; i < num2; i += 3)
				{
					if (insertLineBreaks)
					{
						if (num4 == 76)
						{
							outChars[num3++] = '\r';
							outChars[num3++] = '\n';
							num4 = 0;
						}
						num4 += 4;
					}
					outChars[num3] = ptr2[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr2[((int)(inData[i] & 3) << 4) | ((inData[i + 1] & 240) >> 4)];
					outChars[num3 + 2] = ptr2[((int)(inData[i + 1] & 15) << 2) | ((inData[i + 2] & 192) >> 6)];
					outChars[num3 + 3] = ptr2[inData[i + 2] & 63];
					num3 += 4;
				}
				i = num2;
				if (insertLineBreaks && num != 0 && num4 == 76)
				{
					outChars[num3++] = '\r';
					outChars[num3++] = '\n';
				}
				if (num != 1)
				{
					if (num == 2)
					{
						outChars[num3] = ptr2[(inData[i] & 252) >> 2];
						outChars[num3 + 1] = ptr2[((int)(inData[i] & 3) << 4) | ((inData[i + 1] & 240) >> 4)];
						outChars[num3 + 2] = ptr2[(inData[i + 1] & 15) << 2];
						outChars[num3 + 3] = ptr2[64];
						num3 += 4;
					}
				}
				else
				{
					outChars[num3] = ptr2[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr2[(inData[i] & 3) << 4];
					outChars[num3 + 2] = ptr2[64];
					outChars[num3 + 3] = ptr2[64];
					num3 += 4;
				}
			}
			return num3;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		private static int ToBase64_CalculateAndValidateOutputLength(int inputLength, bool insertLineBreaks)
		{
			long num = (long)inputLength / 3L * 4L;
			num += ((inputLength % 3 != 0) ? 4L : 0L);
			if (num == 0L)
			{
				return 0;
			}
			if (insertLineBreaks)
			{
				long num2 = num / 76L;
				if (num % 76L == 0L)
				{
					num2 -= 1L;
				}
				num += num2 * 2L;
			}
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			return (int)num;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001CA14 File Offset: 0x0001AC14
		public unsafe static byte[] FromBase64String(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Convert.FromBase64CharPtr(ptr, s.Length);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001CA4B File Offset: 0x0001AC4B
		public static bool TryFromBase64String(string s, Span<byte> bytes, out int bytesWritten)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Convert.TryFromBase64Chars(s.AsSpan(), bytes, out bytesWritten);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001CA68 File Offset: 0x0001AC68
		public unsafe static bool TryFromBase64Chars(ReadOnlySpan<char> chars, Span<byte> bytes, out int bytesWritten)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)8], 4);
			bytesWritten = 0;
			while (chars.Length != 0)
			{
				int num;
				int num2;
				bool flag = Convert.TryDecodeFromUtf16(chars, bytes, out num, out num2);
				bytesWritten += num2;
				if (flag)
				{
					return true;
				}
				chars = chars.Slice(num);
				bytes = bytes.Slice(num2);
				if (((char)(*chars[0])).IsSpace())
				{
					int num3 = 1;
					while (num3 != chars.Length && ((char)(*chars[num3])).IsSpace())
					{
						num3++;
					}
					chars = chars.Slice(num3);
					if (num2 % 3 != 0 && chars.Length != 0)
					{
						bytesWritten = 0;
						return false;
					}
				}
				else
				{
					int num4;
					int num5;
					Convert.CopyToTempBufferWithoutWhiteSpace(chars, span, out num4, out num5);
					if ((num5 & 3) != 0)
					{
						bytesWritten = 0;
						return false;
					}
					span = span.Slice(0, num5);
					int num6;
					int num7;
					if (!Convert.TryDecodeFromUtf16(span, bytes, out num6, out num7))
					{
						bytesWritten = 0;
						return false;
					}
					bytesWritten += num7;
					chars = chars.Slice(num4);
					bytes = bytes.Slice(num7);
					if (num7 % 3 != 0)
					{
						for (int i = 0; i < chars.Length; i++)
						{
							if (!((char)(*chars[i])).IsSpace())
							{
								bytesWritten = 0;
								return false;
							}
						}
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001CB9C File Offset: 0x0001AD9C
		private unsafe static void CopyToTempBufferWithoutWhiteSpace(ReadOnlySpan<char> chars, Span<char> tempBuffer, out int consumed, out int charsWritten)
		{
			charsWritten = 0;
			for (int i = 0; i < chars.Length; i++)
			{
				char c = (char)(*chars[i]);
				if (!c.IsSpace())
				{
					int num = charsWritten;
					charsWritten = num + 1;
					*tempBuffer[num] = c;
					if (charsWritten == tempBuffer.Length)
					{
						consumed = i + 1;
						return;
					}
				}
			}
			consumed = chars.Length;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsSpace(this char c)
		{
			return c == ' ' || c == '\t' || c == '\r' || c == '\n';
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001CC14 File Offset: 0x0001AE14
		public unsafe static byte[] FromBase64CharArray(char[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Value must be positive.");
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", "Offset and length must refer to a position in the string.");
			}
			if (inArray.Length == 0)
			{
				return Array.Empty<byte>();
			}
			fixed (char* ptr = &inArray[0])
			{
				return Convert.FromBase64CharPtr(ptr + offset, length);
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001CC90 File Offset: 0x0001AE90
		private unsafe static byte[] FromBase64CharPtr(char* inputPtr, int inputLength)
		{
			while (inputLength > 0)
			{
				int num = (int)inputPtr[inputLength - 1];
				if (num != 32 && num != 10 && num != 13 && num != 9)
				{
					break;
				}
				inputLength--;
			}
			byte[] array = new byte[Convert.FromBase64_ComputeResultLength(inputPtr, inputLength)];
			int num2;
			if (!Convert.TryFromBase64Chars(new ReadOnlySpan<char>((void*)inputPtr, inputLength), array, out num2))
			{
				throw new FormatException("The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.");
			}
			return array;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
		private unsafe static int FromBase64_ComputeResultLength(char* inputPtr, int inputLength)
		{
			char* ptr = inputPtr + inputLength;
			int num = inputLength;
			int num2 = 0;
			while (inputPtr < ptr)
			{
				uint num3 = (uint)(*inputPtr);
				inputPtr++;
				if (num3 <= 32U)
				{
					num--;
				}
				else if (num3 == 61U)
				{
					num--;
					num2++;
				}
			}
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					num2 = 2;
				}
				else
				{
					if (num2 != 2)
					{
						throw new FormatException("The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.");
					}
					num2 = 1;
				}
			}
			return num / 4 * 3 + num2;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001CD5C File Offset: 0x0001AF5C
		// Note: this type is marked as 'beforefieldinit'.
		static Convert()
		{
		}

		// Token: 0x04000F07 RID: 3847
		private static readonly sbyte[] s_decodingMap = new sbyte[]
		{
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, 62, -1, -1, -1, 63, 52, 53,
			54, 55, 56, 57, 58, 59, 60, 61, -1, -1,
			-1, -1, -1, -1, -1, 0, 1, 2, 3, 4,
			5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
			15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
			25, -1, -1, -1, -1, -1, -1, 26, 27, 28,
			29, 30, 31, 32, 33, 34, 35, 36, 37, 38,
			39, 40, 41, 42, 43, 44, 45, 46, 47, 48,
			49, 50, 51, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1
		};

		// Token: 0x04000F08 RID: 3848
		private const byte EncodingPad = 61;

		// Token: 0x04000F09 RID: 3849
		internal static readonly Type[] ConvertTypes = new Type[]
		{
			typeof(Empty),
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(object),
			typeof(string)
		};

		// Token: 0x04000F0A RID: 3850
		private static readonly Type EnumType = typeof(Enum);

		// Token: 0x04000F0B RID: 3851
		internal static readonly char[] base64Table = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
			'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
			'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', '+', '/', '='
		};

		// Token: 0x04000F0C RID: 3852
		private const int base64LineBreakPosition = 76;

		// Token: 0x04000F0D RID: 3853
		public static readonly object DBNull = global::System.DBNull.Value;
	}
}
