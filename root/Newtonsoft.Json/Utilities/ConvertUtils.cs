using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000060 RID: 96
	internal static class ConvertUtils
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x000126F8 File Offset: 0x000108F8
		public static PrimitiveTypeCode GetTypeCode(Type t)
		{
			bool flag;
			return ConvertUtils.GetTypeCode(t, out flag);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00012710 File Offset: 0x00010910
		public static PrimitiveTypeCode GetTypeCode(Type t, out bool isEnum)
		{
			PrimitiveTypeCode primitiveTypeCode;
			if (ConvertUtils.TypeCodeMap.TryGetValue(t, ref primitiveTypeCode))
			{
				isEnum = false;
				return primitiveTypeCode;
			}
			if (t.IsEnum())
			{
				isEnum = true;
				return ConvertUtils.GetTypeCode(Enum.GetUnderlyingType(t));
			}
			if (ReflectionUtils.IsNullableType(t))
			{
				Type underlyingType = Nullable.GetUnderlyingType(t);
				if (underlyingType.IsEnum())
				{
					Type type = typeof(Nullable).MakeGenericType(new Type[] { Enum.GetUnderlyingType(underlyingType) });
					isEnum = true;
					return ConvertUtils.GetTypeCode(type);
				}
			}
			isEnum = false;
			return PrimitiveTypeCode.Object;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001278A File Offset: 0x0001098A
		public static TypeInformation GetTypeInformation(IConvertible convertable)
		{
			return ConvertUtils.PrimitiveTypeCodes[convertable.GetTypeCode()];
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00012798 File Offset: 0x00010998
		public static bool IsConvertible(Type t)
		{
			return typeof(IConvertible).IsAssignableFrom(t);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000127AA File Offset: 0x000109AA
		public static TimeSpan ParseTimeSpan(string input)
		{
			return TimeSpan.Parse(input, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000127B8 File Offset: 0x000109B8
		private static Func<object, object> CreateCastConverter(ConvertUtils.TypeConvertKey t)
		{
			MethodInfo methodInfo = t.TargetType.GetMethod("op_Implicit", new Type[] { t.InitialType });
			if (methodInfo == null)
			{
				methodInfo = t.TargetType.GetMethod("op_Explicit", new Type[] { t.InitialType });
			}
			if (methodInfo == null)
			{
				return null;
			}
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
			return (object o) => call(null, new object[] { o });
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00012840 File Offset: 0x00010A40
		internal static BigInteger ToBigInteger(object value)
		{
			if (value is BigInteger)
			{
				return (BigInteger)value;
			}
			string text = value as string;
			if (text != null)
			{
				return BigInteger.Parse(text, CultureInfo.InvariantCulture);
			}
			if (value is float)
			{
				return new BigInteger((float)value);
			}
			if (value is double)
			{
				return new BigInteger((double)value);
			}
			if (value is decimal)
			{
				return new BigInteger((decimal)value);
			}
			if (value is int)
			{
				return new BigInteger((int)value);
			}
			if (value is long)
			{
				return new BigInteger((long)value);
			}
			if (value is uint)
			{
				return new BigInteger((uint)value);
			}
			if (value is ulong)
			{
				return new BigInteger((ulong)value);
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				return new BigInteger(array);
			}
			throw new InvalidCastException("Cannot convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001292C File Offset: 0x00010B2C
		public static object FromBigInteger(BigInteger i, Type targetType)
		{
			if (targetType == typeof(decimal))
			{
				return (decimal)i;
			}
			if (targetType == typeof(double))
			{
				return (double)i;
			}
			if (targetType == typeof(float))
			{
				return (float)i;
			}
			if (targetType == typeof(ulong))
			{
				return (ulong)i;
			}
			if (targetType == typeof(bool))
			{
				return i != 0L;
			}
			object obj;
			try
			{
				obj = global::System.Convert.ChangeType((long)i, targetType, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Can not convert from BigInteger to {0}.".FormatWith(CultureInfo.InvariantCulture, targetType), ex);
			}
			return obj;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00012A18 File Offset: 0x00010C18
		public static object Convert(object initialValue, CultureInfo culture, Type targetType)
		{
			object obj;
			switch (ConvertUtils.TryConvertInternal(initialValue, culture, targetType, out obj))
			{
			case ConvertUtils.ConvertResult.Success:
				return obj;
			case ConvertUtils.ConvertResult.CannotConvertNull:
				throw new Exception("Can not convert null {0} into non-nullable {1}.".FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
			case ConvertUtils.ConvertResult.NotInstantiableType:
				throw new ArgumentException("Target type {0} is not a value type or a non-abstract class.".FormatWith(CultureInfo.InvariantCulture, targetType), "targetType");
			case ConvertUtils.ConvertResult.NoValidConversion:
				throw new InvalidOperationException("Can not convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
			default:
				throw new InvalidOperationException("Unexpected conversion result.");
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00012AA8 File Offset: 0x00010CA8
		private static bool TryConvert(object initialValue, CultureInfo culture, Type targetType, out object value)
		{
			bool flag;
			try
			{
				if (ConvertUtils.TryConvertInternal(initialValue, culture, targetType, out value) == ConvertUtils.ConvertResult.Success)
				{
					flag = true;
				}
				else
				{
					value = null;
					flag = false;
				}
			}
			catch
			{
				value = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00012AE4 File Offset: 0x00010CE4
		private static ConvertUtils.ConvertResult TryConvertInternal(object initialValue, CultureInfo culture, Type targetType, out object value)
		{
			if (initialValue == null)
			{
				throw new ArgumentNullException("initialValue");
			}
			if (ReflectionUtils.IsNullableType(targetType))
			{
				targetType = Nullable.GetUnderlyingType(targetType);
			}
			Type type = initialValue.GetType();
			if (targetType == type)
			{
				value = initialValue;
				return ConvertUtils.ConvertResult.Success;
			}
			if (ConvertUtils.IsConvertible(initialValue.GetType()) && ConvertUtils.IsConvertible(targetType))
			{
				if (targetType.IsEnum())
				{
					if (initialValue is string)
					{
						value = Enum.Parse(targetType, initialValue.ToString(), true);
						return ConvertUtils.ConvertResult.Success;
					}
					if (ConvertUtils.IsInteger(initialValue))
					{
						value = Enum.ToObject(targetType, initialValue);
						return ConvertUtils.ConvertResult.Success;
					}
				}
				value = global::System.Convert.ChangeType(initialValue, targetType, culture);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is DateTime && targetType == typeof(DateTimeOffset))
			{
				value = new DateTimeOffset((DateTime)initialValue);
				return ConvertUtils.ConvertResult.Success;
			}
			byte[] array = initialValue as byte[];
			if (array != null && targetType == typeof(Guid))
			{
				value = new Guid(array);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is Guid && targetType == typeof(byte[]))
			{
				value = ((Guid)initialValue).ToByteArray();
				return ConvertUtils.ConvertResult.Success;
			}
			string text = initialValue as string;
			if (text != null)
			{
				if (targetType == typeof(Guid))
				{
					value = new Guid(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(Uri))
				{
					value = new Uri(text, 0);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(TimeSpan))
				{
					value = ConvertUtils.ParseTimeSpan(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(byte[]))
				{
					value = global::System.Convert.FromBase64String(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(Version))
				{
					Version version;
					if (ConvertUtils.VersionTryParse(text, out version))
					{
						value = version;
						return ConvertUtils.ConvertResult.Success;
					}
					value = null;
					return ConvertUtils.ConvertResult.NoValidConversion;
				}
				else if (typeof(Type).IsAssignableFrom(targetType))
				{
					value = Type.GetType(text, true);
					return ConvertUtils.ConvertResult.Success;
				}
			}
			if (targetType == typeof(BigInteger))
			{
				value = ConvertUtils.ToBigInteger(initialValue);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is BigInteger)
			{
				value = ConvertUtils.FromBigInteger((BigInteger)initialValue, targetType);
				return ConvertUtils.ConvertResult.Success;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter != null && converter.CanConvertTo(targetType))
			{
				value = converter.ConvertTo(null, culture, initialValue, targetType);
				return ConvertUtils.ConvertResult.Success;
			}
			TypeConverter converter2 = TypeDescriptor.GetConverter(targetType);
			if (converter2 != null && converter2.CanConvertFrom(type))
			{
				value = converter2.ConvertFrom(null, culture, initialValue);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue == DBNull.Value)
			{
				if (ReflectionUtils.IsNullable(targetType))
				{
					value = ConvertUtils.EnsureTypeAssignable(null, type, targetType);
					return ConvertUtils.ConvertResult.Success;
				}
				value = null;
				return ConvertUtils.ConvertResult.CannotConvertNull;
			}
			else
			{
				INullable nullable = initialValue as INullable;
				if (nullable != null)
				{
					value = ConvertUtils.EnsureTypeAssignable(ConvertUtils.ToValue(nullable), type, targetType);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType.IsInterface() || targetType.IsGenericTypeDefinition() || targetType.IsAbstract())
				{
					value = null;
					return ConvertUtils.ConvertResult.NotInstantiableType;
				}
				value = null;
				return ConvertUtils.ConvertResult.NoValidConversion;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00012DAC File Offset: 0x00010FAC
		public static object ConvertOrCast(object initialValue, CultureInfo culture, Type targetType)
		{
			if (targetType == typeof(object))
			{
				return initialValue;
			}
			if (initialValue == null && ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			object obj;
			if (ConvertUtils.TryConvert(initialValue, culture, targetType, out obj))
			{
				return obj;
			}
			return ConvertUtils.EnsureTypeAssignable(initialValue, ReflectionUtils.GetObjectType(initialValue), targetType);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00012DF8 File Offset: 0x00010FF8
		private static object EnsureTypeAssignable(object value, Type initialType, Type targetType)
		{
			Type type = ((value != null) ? value.GetType() : null);
			if (value != null)
			{
				if (targetType.IsAssignableFrom(type))
				{
					return value;
				}
				Func<object, object> func = ConvertUtils.CastConverters.Get(new ConvertUtils.TypeConvertKey(type, targetType));
				if (func != null)
				{
					return func.Invoke(value);
				}
			}
			else if (ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			throw new ArgumentException("Could not cast or convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, ((initialType != null) ? initialType.ToString() : null) ?? "{null}", targetType));
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00012E74 File Offset: 0x00011074
		public static object ToValue(INullable nullableValue)
		{
			if (nullableValue == null)
			{
				return null;
			}
			if (nullableValue is SqlInt32)
			{
				return ConvertUtils.ToValue((SqlInt32)nullableValue);
			}
			if (nullableValue is SqlInt64)
			{
				return ConvertUtils.ToValue((SqlInt64)nullableValue);
			}
			if (nullableValue is SqlBoolean)
			{
				return ConvertUtils.ToValue((SqlBoolean)nullableValue);
			}
			if (nullableValue is SqlString)
			{
				return ConvertUtils.ToValue((SqlString)nullableValue);
			}
			if (nullableValue is SqlDateTime)
			{
				return ConvertUtils.ToValue((SqlDateTime)nullableValue);
			}
			throw new ArgumentException("Unsupported INullable type: {0}".FormatWith(CultureInfo.InvariantCulture, nullableValue.GetType()));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00012F1D File Offset: 0x0001111D
		public static bool VersionTryParse(string input, out Version result)
		{
			return Version.TryParse(input, ref result);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00012F28 File Offset: 0x00011128
		public static bool IsInteger(object value)
		{
			switch (ConvertUtils.GetTypeCode(value.GetType()))
			{
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
				return true;
			}
			return false;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00012F8C File Offset: 0x0001118C
		public static ParseResult Int32TryParse(char[] chars, int start, int length, out int value)
		{
			value = 0;
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int num = start + length;
			if (length > 10 || (length == 10 && chars[start] - '0' > '\u0002'))
			{
				for (int i = start; i < num; i++)
				{
					int num2 = (int)(chars[i] - '0');
					if (num2 < 0 || num2 > 9)
					{
						return ParseResult.Invalid;
					}
				}
				return ParseResult.Overflow;
			}
			for (int j = start; j < num; j++)
			{
				int num3 = (int)(chars[j] - '0');
				if (num3 < 0 || num3 > 9)
				{
					return ParseResult.Invalid;
				}
				int num4 = 10 * value - num3;
				if (num4 > value)
				{
					for (j++; j < num; j++)
					{
						num3 = (int)(chars[j] - '0');
						if (num3 < 0 || num3 > 9)
						{
							return ParseResult.Invalid;
						}
					}
					return ParseResult.Overflow;
				}
				value = num4;
			}
			if (!flag)
			{
				if (value == -2147483648)
				{
					return ParseResult.Overflow;
				}
				value = -value;
			}
			return ParseResult.Success;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001306C File Offset: 0x0001126C
		public static ParseResult Int64TryParse(char[] chars, int start, int length, out long value)
		{
			value = 0L;
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int num = start + length;
			if (length > 19)
			{
				for (int i = start; i < num; i++)
				{
					int num2 = (int)(chars[i] - '0');
					if (num2 < 0 || num2 > 9)
					{
						return ParseResult.Invalid;
					}
				}
				return ParseResult.Overflow;
			}
			for (int j = start; j < num; j++)
			{
				int num3 = (int)(chars[j] - '0');
				if (num3 < 0 || num3 > 9)
				{
					return ParseResult.Invalid;
				}
				long num4 = 10L * value - (long)num3;
				if (num4 > value)
				{
					for (j++; j < num; j++)
					{
						num3 = (int)(chars[j] - '0');
						if (num3 < 0 || num3 > 9)
						{
							return ParseResult.Invalid;
						}
					}
					return ParseResult.Overflow;
				}
				value = num4;
			}
			if (!flag)
			{
				if (value == -9223372036854775808L)
				{
					return ParseResult.Overflow;
				}
				value = -value;
			}
			return ParseResult.Success;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00013148 File Offset: 0x00011348
		public static ParseResult DecimalTryParse(char[] chars, int start, int length, out decimal value)
		{
			value = default(decimal);
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int i = start;
			int num = start + length;
			int num2 = num;
			int num3 = num;
			int num4 = 0;
			ulong num5 = 0UL;
			ulong num6 = 0UL;
			int num7 = 0;
			int num8 = 0;
			bool? flag2 = default(bool?);
			bool? flag3 = default(bool?);
			while (i < num)
			{
				char c = chars[i];
				if (c == '.')
				{
					goto IL_0074;
				}
				if (c != 'E' && c != 'e')
				{
					if (c < '0' || c > '9')
					{
						return ParseResult.Invalid;
					}
					if (i == start && c == '0')
					{
						i++;
						if (i != num)
						{
							c = chars[i];
							if (c == '.')
							{
								goto IL_0074;
							}
							if (c != 'e' && c != 'E')
							{
								return ParseResult.Invalid;
							}
							goto IL_0091;
						}
					}
					if (num7 < 29)
					{
						if (num7 == 28)
						{
							bool? flag4 = flag3;
							bool flag6;
							if (flag4 == null)
							{
								flag3 = new bool?(num5 > 7922816251426433759UL || (num5 == 7922816251426433759UL && (num6 > 354395033UL || (num6 == 354395033UL && c > '5'))));
								bool? flag5 = flag3;
								flag6 = flag5.GetValueOrDefault();
							}
							else
							{
								flag6 = flag4.GetValueOrDefault();
							}
							if (flag6)
							{
								goto IL_01FF;
							}
						}
						if (num7 < 19)
						{
							num5 = num5 * 10UL + (ulong)((long)(c - '0'));
						}
						else
						{
							num6 = num6 * 10UL + (ulong)((long)(c - '0'));
						}
						num7++;
						goto IL_021E;
					}
					IL_01FF:
					if (flag2 == null)
					{
						flag2 = new bool?(c >= '5');
					}
					num8++;
					goto IL_021E;
				}
				IL_0091:
				if (i == start)
				{
					return ParseResult.Invalid;
				}
				if (i == num2)
				{
					return ParseResult.Invalid;
				}
				i++;
				if (i == num)
				{
					return ParseResult.Invalid;
				}
				if (num2 < num)
				{
					num3 = i - 1;
				}
				c = chars[i];
				bool flag7 = false;
				if (c != '+')
				{
					if (c == '-')
					{
						flag7 = true;
						i++;
					}
				}
				else
				{
					i++;
				}
				while (i < num)
				{
					c = chars[i];
					if (c < '0' || c > '9')
					{
						return ParseResult.Invalid;
					}
					int num9 = 10 * num4 + (int)(c - '0');
					if (num4 < num9)
					{
						num4 = num9;
					}
					i++;
				}
				if (flag7)
				{
					num4 = -num4;
				}
				IL_021E:
				i++;
				continue;
				IL_0074:
				if (i == start)
				{
					return ParseResult.Invalid;
				}
				if (i + 1 == num)
				{
					return ParseResult.Invalid;
				}
				if (num2 != num)
				{
					return ParseResult.Invalid;
				}
				num2 = i + 1;
				goto IL_021E;
			}
			num4 += num8;
			num4 -= num3 - num2;
			if (num7 <= 19)
			{
				value = num5;
			}
			else
			{
				value = num5 * ConvertUtils.DecimalFactors[num7 - 20] + num6;
			}
			if (num4 > 0)
			{
				num7 += num4;
				if (num7 > 29)
				{
					return ParseResult.Overflow;
				}
				if (num7 == 29)
				{
					if (num4 > 1)
					{
						value *= ConvertUtils.DecimalFactors[num4 - 2];
						if (value > 7922816251426433759354395033m)
						{
							return ParseResult.Overflow;
						}
					}
					value *= 10m;
				}
				else
				{
					value *= ConvertUtils.DecimalFactors[num4 - 1];
				}
			}
			else
			{
				if (flag2 == true && num4 >= -28)
				{
					value += 1m;
				}
				if (num4 < 0)
				{
					if (num7 + num4 + 28 <= 0)
					{
						value = default(decimal);
						return ParseResult.Success;
					}
					if (num4 >= -28)
					{
						value /= ConvertUtils.DecimalFactors[-num4 - 1];
					}
					else
					{
						decimal[] decimalFactors = ConvertUtils.DecimalFactors;
						value /= decimalFactors[27];
						value /= decimalFactors[-num4 - 29];
					}
				}
			}
			if (flag)
			{
				value = -value;
			}
			return ParseResult.Success;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00013544 File Offset: 0x00011744
		private static decimal[] DecimalFactors
		{
			get
			{
				decimal[] array = ConvertUtils._decimalFactors;
				if (array == null)
				{
					array = new decimal[28];
					decimal num = 1m;
					for (int i = 0; i < array.Length; i++)
					{
						num = (array[i] = num * 10m);
					}
					ConvertUtils._decimalFactors = array;
				}
				return array;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00013593 File Offset: 0x00011793
		public static bool TryConvertGuid(string s, out Guid g)
		{
			return Guid.TryParseExact(s, "D", ref g);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000135A4 File Offset: 0x000117A4
		public static bool TryHexTextToInt(char[] text, int start, int end, out int value)
		{
			value = 0;
			for (int i = start; i < end; i++)
			{
				char c = text[i];
				int num;
				if (c <= '9' && c >= '0')
				{
					num = (int)(c - '0');
				}
				else if (c <= 'F' && c >= 'A')
				{
					num = (int)(c - '7');
				}
				else
				{
					if (c > 'f' || c < 'a')
					{
						value = 0;
						return false;
					}
					num = (int)(c - 'W');
				}
				value += num << (end - 1 - i) * 4;
			}
			return true;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00013610 File Offset: 0x00011810
		// Note: this type is marked as 'beforefieldinit'.
		static ConvertUtils()
		{
			Dictionary<Type, PrimitiveTypeCode> dictionary = new Dictionary<Type, PrimitiveTypeCode>();
			dictionary.Add(typeof(char), PrimitiveTypeCode.Char);
			dictionary.Add(typeof(char?), PrimitiveTypeCode.CharNullable);
			dictionary.Add(typeof(bool), PrimitiveTypeCode.Boolean);
			dictionary.Add(typeof(bool?), PrimitiveTypeCode.BooleanNullable);
			dictionary.Add(typeof(sbyte), PrimitiveTypeCode.SByte);
			dictionary.Add(typeof(sbyte?), PrimitiveTypeCode.SByteNullable);
			dictionary.Add(typeof(short), PrimitiveTypeCode.Int16);
			dictionary.Add(typeof(short?), PrimitiveTypeCode.Int16Nullable);
			dictionary.Add(typeof(ushort), PrimitiveTypeCode.UInt16);
			dictionary.Add(typeof(ushort?), PrimitiveTypeCode.UInt16Nullable);
			dictionary.Add(typeof(int), PrimitiveTypeCode.Int32);
			dictionary.Add(typeof(int?), PrimitiveTypeCode.Int32Nullable);
			dictionary.Add(typeof(byte), PrimitiveTypeCode.Byte);
			dictionary.Add(typeof(byte?), PrimitiveTypeCode.ByteNullable);
			dictionary.Add(typeof(uint), PrimitiveTypeCode.UInt32);
			dictionary.Add(typeof(uint?), PrimitiveTypeCode.UInt32Nullable);
			dictionary.Add(typeof(long), PrimitiveTypeCode.Int64);
			dictionary.Add(typeof(long?), PrimitiveTypeCode.Int64Nullable);
			dictionary.Add(typeof(ulong), PrimitiveTypeCode.UInt64);
			dictionary.Add(typeof(ulong?), PrimitiveTypeCode.UInt64Nullable);
			dictionary.Add(typeof(float), PrimitiveTypeCode.Single);
			dictionary.Add(typeof(float?), PrimitiveTypeCode.SingleNullable);
			dictionary.Add(typeof(double), PrimitiveTypeCode.Double);
			dictionary.Add(typeof(double?), PrimitiveTypeCode.DoubleNullable);
			dictionary.Add(typeof(DateTime), PrimitiveTypeCode.DateTime);
			dictionary.Add(typeof(DateTime?), PrimitiveTypeCode.DateTimeNullable);
			dictionary.Add(typeof(DateTimeOffset), PrimitiveTypeCode.DateTimeOffset);
			dictionary.Add(typeof(DateTimeOffset?), PrimitiveTypeCode.DateTimeOffsetNullable);
			dictionary.Add(typeof(decimal), PrimitiveTypeCode.Decimal);
			dictionary.Add(typeof(decimal?), PrimitiveTypeCode.DecimalNullable);
			dictionary.Add(typeof(Guid), PrimitiveTypeCode.Guid);
			dictionary.Add(typeof(Guid?), PrimitiveTypeCode.GuidNullable);
			dictionary.Add(typeof(TimeSpan), PrimitiveTypeCode.TimeSpan);
			dictionary.Add(typeof(TimeSpan?), PrimitiveTypeCode.TimeSpanNullable);
			dictionary.Add(typeof(BigInteger), PrimitiveTypeCode.BigInteger);
			dictionary.Add(typeof(BigInteger?), PrimitiveTypeCode.BigIntegerNullable);
			dictionary.Add(typeof(Uri), PrimitiveTypeCode.Uri);
			dictionary.Add(typeof(string), PrimitiveTypeCode.String);
			dictionary.Add(typeof(byte[]), PrimitiveTypeCode.Bytes);
			dictionary.Add(typeof(DBNull), PrimitiveTypeCode.DBNull);
			ConvertUtils.TypeCodeMap = dictionary;
			ConvertUtils.PrimitiveTypeCodes = new TypeInformation[]
			{
				new TypeInformation
				{
					Type = typeof(object),
					TypeCode = PrimitiveTypeCode.Empty
				},
				new TypeInformation
				{
					Type = typeof(object),
					TypeCode = PrimitiveTypeCode.Object
				},
				new TypeInformation
				{
					Type = typeof(object),
					TypeCode = PrimitiveTypeCode.DBNull
				},
				new TypeInformation
				{
					Type = typeof(bool),
					TypeCode = PrimitiveTypeCode.Boolean
				},
				new TypeInformation
				{
					Type = typeof(char),
					TypeCode = PrimitiveTypeCode.Char
				},
				new TypeInformation
				{
					Type = typeof(sbyte),
					TypeCode = PrimitiveTypeCode.SByte
				},
				new TypeInformation
				{
					Type = typeof(byte),
					TypeCode = PrimitiveTypeCode.Byte
				},
				new TypeInformation
				{
					Type = typeof(short),
					TypeCode = PrimitiveTypeCode.Int16
				},
				new TypeInformation
				{
					Type = typeof(ushort),
					TypeCode = PrimitiveTypeCode.UInt16
				},
				new TypeInformation
				{
					Type = typeof(int),
					TypeCode = PrimitiveTypeCode.Int32
				},
				new TypeInformation
				{
					Type = typeof(uint),
					TypeCode = PrimitiveTypeCode.UInt32
				},
				new TypeInformation
				{
					Type = typeof(long),
					TypeCode = PrimitiveTypeCode.Int64
				},
				new TypeInformation
				{
					Type = typeof(ulong),
					TypeCode = PrimitiveTypeCode.UInt64
				},
				new TypeInformation
				{
					Type = typeof(float),
					TypeCode = PrimitiveTypeCode.Single
				},
				new TypeInformation
				{
					Type = typeof(double),
					TypeCode = PrimitiveTypeCode.Double
				},
				new TypeInformation
				{
					Type = typeof(decimal),
					TypeCode = PrimitiveTypeCode.Decimal
				},
				new TypeInformation
				{
					Type = typeof(DateTime),
					TypeCode = PrimitiveTypeCode.DateTime
				},
				new TypeInformation
				{
					Type = typeof(object),
					TypeCode = PrimitiveTypeCode.Empty
				},
				new TypeInformation
				{
					Type = typeof(string),
					TypeCode = PrimitiveTypeCode.String
				}
			};
			ConvertUtils.CastConverters = new ThreadSafeStore<ConvertUtils.TypeConvertKey, Func<object, object>>(new Func<ConvertUtils.TypeConvertKey, Func<object, object>>(ConvertUtils.CreateCastConverter));
		}

		// Token: 0x04000244 RID: 580
		private static readonly Dictionary<Type, PrimitiveTypeCode> TypeCodeMap;

		// Token: 0x04000245 RID: 581
		private static readonly TypeInformation[] PrimitiveTypeCodes;

		// Token: 0x04000246 RID: 582
		private static readonly ThreadSafeStore<ConvertUtils.TypeConvertKey, Func<object, object>> CastConverters;

		// Token: 0x04000247 RID: 583
		private static decimal[] _decimalFactors;

		// Token: 0x02000131 RID: 305
		internal struct TypeConvertKey : IEquatable<ConvertUtils.TypeConvertKey>
		{
			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00031067 File Offset: 0x0002F267
			public Type InitialType
			{
				get
				{
					return this._initialType;
				}
			}

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0003106F File Offset: 0x0002F26F
			public Type TargetType
			{
				get
				{
					return this._targetType;
				}
			}

			// Token: 0x06000CBF RID: 3263 RVA: 0x00031077 File Offset: 0x0002F277
			public TypeConvertKey(Type initialType, Type targetType)
			{
				this._initialType = initialType;
				this._targetType = targetType;
			}

			// Token: 0x06000CC0 RID: 3264 RVA: 0x00031087 File Offset: 0x0002F287
			public override int GetHashCode()
			{
				return this._initialType.GetHashCode() ^ this._targetType.GetHashCode();
			}

			// Token: 0x06000CC1 RID: 3265 RVA: 0x000310A0 File Offset: 0x0002F2A0
			public override bool Equals(object obj)
			{
				return obj is ConvertUtils.TypeConvertKey && this.Equals((ConvertUtils.TypeConvertKey)obj);
			}

			// Token: 0x06000CC2 RID: 3266 RVA: 0x000310B8 File Offset: 0x0002F2B8
			public bool Equals(ConvertUtils.TypeConvertKey other)
			{
				return this._initialType == other._initialType && this._targetType == other._targetType;
			}

			// Token: 0x0400048B RID: 1163
			private readonly Type _initialType;

			// Token: 0x0400048C RID: 1164
			private readonly Type _targetType;
		}

		// Token: 0x02000132 RID: 306
		internal enum ConvertResult
		{
			// Token: 0x0400048E RID: 1166
			Success,
			// Token: 0x0400048F RID: 1167
			CannotConvertNull,
			// Token: 0x04000490 RID: 1168
			NotInstantiableType,
			// Token: 0x04000491 RID: 1169
			NoValidConversion
		}

		// Token: 0x02000133 RID: 307
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06000CC3 RID: 3267 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06000CC4 RID: 3268 RVA: 0x000310E0 File Offset: 0x0002F2E0
			internal object <CreateCastConverter>b__0(object o)
			{
				return this.call(null, new object[] { o });
			}

			// Token: 0x04000492 RID: 1170
			public MethodCall<object, object> call;
		}
	}
}
