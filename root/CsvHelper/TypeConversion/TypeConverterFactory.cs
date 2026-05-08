using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200002E RID: 46
	public static class TypeConverterFactory
	{
		// Token: 0x0600015C RID: 348 RVA: 0x000061AC File Offset: 0x000043AC
		static TypeConverterFactory()
		{
			TypeConverterFactory.CreateDefaultConverters();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000061C8 File Offset: 0x000043C8
		public static void AddConverter(Type type, ITypeConverter typeConverter)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (typeConverter == null)
			{
				throw new ArgumentNullException("typeConverter");
			}
			object obj = TypeConverterFactory.locker;
			lock (obj)
			{
				TypeConverterFactory.typeConverters[type] = typeConverter;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006230 File Offset: 0x00004430
		public static void AddConverter<T>(ITypeConverter typeConverter)
		{
			if (typeConverter == null)
			{
				throw new ArgumentNullException("typeConverter");
			}
			object obj = TypeConverterFactory.locker;
			lock (obj)
			{
				TypeConverterFactory.typeConverters[typeof(T)] = typeConverter;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000628C File Offset: 0x0000448C
		public static void RemoveConverter(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = TypeConverterFactory.locker;
			lock (obj)
			{
				TypeConverterFactory.typeConverters.Remove(type);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000062E8 File Offset: 0x000044E8
		public static void RemoveConverter<T>()
		{
			TypeConverterFactory.RemoveConverter(typeof(T));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000062FC File Offset: 0x000044FC
		public static ITypeConverter GetConverter(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = TypeConverterFactory.locker;
			lock (obj)
			{
				ITypeConverter typeConverter;
				if (TypeConverterFactory.typeConverters.TryGetValue(type, ref typeConverter))
				{
					return typeConverter;
				}
			}
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				return TypeConverterFactory.GetConverter(typeof(IEnumerable));
			}
			if (typeof(Enum).IsAssignableFrom(type))
			{
				TypeConverterFactory.AddConverter(type, new EnumConverter(type));
				return TypeConverterFactory.GetConverter(type);
			}
			if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable))
			{
				TypeConverterFactory.AddConverter(type, new NullableConverter(type));
				return TypeConverterFactory.GetConverter(type);
			}
			return new DefaultTypeConverter();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000063E4 File Offset: 0x000045E4
		public static ITypeConverter GetConverter<T>()
		{
			return TypeConverterFactory.GetConverter(typeof(T));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000063F8 File Offset: 0x000045F8
		private static void CreateDefaultConverters()
		{
			TypeConverterFactory.AddConverter(typeof(bool), new BooleanConverter());
			TypeConverterFactory.AddConverter(typeof(byte), new ByteConverter());
			TypeConverterFactory.AddConverter(typeof(char), new CharConverter());
			TypeConverterFactory.AddConverter(typeof(DateTime), new DateTimeConverter());
			TypeConverterFactory.AddConverter(typeof(DateTimeOffset), new DateTimeOffsetConverter());
			TypeConverterFactory.AddConverter(typeof(decimal), new DecimalConverter());
			TypeConverterFactory.AddConverter(typeof(double), new DoubleConverter());
			TypeConverterFactory.AddConverter(typeof(float), new SingleConverter());
			TypeConverterFactory.AddConverter(typeof(Guid), new GuidConverter());
			TypeConverterFactory.AddConverter(typeof(short), new Int16Converter());
			TypeConverterFactory.AddConverter(typeof(int), new Int32Converter());
			TypeConverterFactory.AddConverter(typeof(long), new Int64Converter());
			TypeConverterFactory.AddConverter(typeof(sbyte), new SByteConverter());
			TypeConverterFactory.AddConverter(typeof(string), new StringConverter());
			TypeConverterFactory.AddConverter(typeof(TimeSpan), new TimeSpanConverter());
			TypeConverterFactory.AddConverter(typeof(ushort), new UInt16Converter());
			TypeConverterFactory.AddConverter(typeof(uint), new UInt32Converter());
			TypeConverterFactory.AddConverter(typeof(ulong), new UInt64Converter());
			TypeConverterFactory.AddConverter(typeof(IEnumerable), new EnumerableConverter());
		}

		// Token: 0x0400002F RID: 47
		private static readonly Dictionary<Type, ITypeConverter> typeConverters = new Dictionary<Type, ITypeConverter>();

		// Token: 0x04000030 RID: 48
		private static readonly object locker = new object();
	}
}
