using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsvHelper.Configuration
{
	// Token: 0x02000035 RID: 53
	public class CsvClassMapCollection
	{
		// Token: 0x17000036 RID: 54
		public virtual CsvClassMap this[Type type]
		{
			get
			{
				Type type2 = type;
				while (!(type2 == type))
				{
					type2 = type.GetTypeInfo().BaseType;
					if (type2 == null)
					{
						return null;
					}
				}
				if (!this.data.ContainsKey(type2))
				{
					return null;
				}
				return this.data[type2];
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006E33 File Offset: 0x00005033
		public virtual CsvClassMap<T> Find<T>()
		{
			return (CsvClassMap<T>)this[typeof(T)];
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006E4C File Offset: 0x0000504C
		internal virtual void Add(CsvClassMap map)
		{
			Type type = Enumerable.First<Type>(this.GetGenericCsvClassMapType(map.GetType()).GetGenericArguments());
			if (this.data.ContainsKey(type))
			{
				this.data[type] = map;
				return;
			}
			this.data.Add(type, map);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006E9C File Offset: 0x0000509C
		internal virtual void Remove(Type classMapType)
		{
			if (!typeof(CsvClassMap).IsAssignableFrom(classMapType))
			{
				throw new ArgumentException("The class map type must inherit from CsvClassMap.");
			}
			Type type = Enumerable.First<Type>(this.GetGenericCsvClassMapType(classMapType).GetGenericArguments());
			this.data.Remove(type);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006EE5 File Offset: 0x000050E5
		internal virtual void Clear()
		{
			this.data.Clear();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006EF2 File Offset: 0x000050F2
		private Type GetGenericCsvClassMapType(Type type)
		{
			if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(CsvClassMap<>))
			{
				return type;
			}
			return this.GetGenericCsvClassMapType(type.GetTypeInfo().BaseType);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006F2B File Offset: 0x0000512B
		public CsvClassMapCollection()
		{
		}

		// Token: 0x0400003D RID: 61
		private readonly Dictionary<Type, CsvClassMap> data = new Dictionary<Type, CsvClassMap>();
	}
}
