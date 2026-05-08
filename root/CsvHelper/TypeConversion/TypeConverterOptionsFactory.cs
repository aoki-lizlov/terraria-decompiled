using System;
using System.Collections.Generic;

namespace CsvHelper.TypeConversion
{
	// Token: 0x02000030 RID: 48
	public static class TypeConverterOptionsFactory
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00006754 File Offset: 0x00004954
		public static void AddOptions(Type type, TypeConverterOptions options)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			object obj = TypeConverterOptionsFactory.locker;
			lock (obj)
			{
				TypeConverterOptionsFactory.typeConverterOptions[type] = options;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000067BC File Offset: 0x000049BC
		public static void AddOptions<T>(TypeConverterOptions options)
		{
			TypeConverterOptionsFactory.AddOptions(typeof(T), options);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000067D0 File Offset: 0x000049D0
		public static void RemoveOptions(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = TypeConverterOptionsFactory.locker;
			lock (obj)
			{
				TypeConverterOptionsFactory.typeConverterOptions.Remove(type);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000682C File Offset: 0x00004A2C
		public static void RemoveOptions<T>()
		{
			TypeConverterOptionsFactory.RemoveOptions(typeof(T));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006840 File Offset: 0x00004A40
		public static TypeConverterOptions GetOptions(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException();
			}
			object obj = TypeConverterOptionsFactory.locker;
			TypeConverterOptions typeConverterOptions2;
			lock (obj)
			{
				TypeConverterOptions typeConverterOptions;
				if (!TypeConverterOptionsFactory.typeConverterOptions.TryGetValue(type, ref typeConverterOptions))
				{
					typeConverterOptions = new TypeConverterOptions();
					TypeConverterOptionsFactory.typeConverterOptions.Add(type, typeConverterOptions);
				}
				typeConverterOptions2 = typeConverterOptions;
			}
			return typeConverterOptions2;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000068AC File Offset: 0x00004AAC
		public static TypeConverterOptions GetOptions<T>()
		{
			return TypeConverterOptionsFactory.GetOptions(typeof(T));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000068BD File Offset: 0x00004ABD
		// Note: this type is marked as 'beforefieldinit'.
		static TypeConverterOptionsFactory()
		{
		}

		// Token: 0x04000038 RID: 56
		private static readonly Dictionary<Type, TypeConverterOptions> typeConverterOptions = new Dictionary<Type, TypeConverterOptions>();

		// Token: 0x04000039 RID: 57
		private static readonly object locker = new object();
	}
}
