using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using CsvHelper.TypeConversion;

namespace CsvHelper.Configuration
{
	// Token: 0x02000034 RID: 52
	public abstract class CsvClassMap
	{
		// Token: 0x06000182 RID: 386 RVA: 0x000069AC File Offset: 0x00004BAC
		[Obsolete("This method is deprecated and will be removed in the next major release. Specify your mappings in the constructor instead.", false)]
		public virtual void CreateMap()
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000069AE File Offset: 0x00004BAE
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000069B6 File Offset: 0x00004BB6
		public virtual NewExpression Constructor
		{
			[CompilerGenerated]
			get
			{
				return this.<Constructor>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Constructor>k__BackingField = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000069BF File Offset: 0x00004BBF
		public virtual CsvPropertyMapCollection PropertyMaps
		{
			get
			{
				return this.propertyMaps;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000069C7 File Offset: 0x00004BC7
		public virtual List<CsvPropertyReferenceMap> ReferenceMaps
		{
			get
			{
				return this.referenceMaps;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000069CF File Offset: 0x00004BCF
		internal CsvClassMap()
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000069F0 File Offset: 0x00004BF0
		[Obsolete("This method is deprecated and will be removed in the next major release.", false)]
		public virtual CsvPropertyMap PropertyMap<T>(Expression<Func<T, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.GetProperty<T>(expression);
			CsvPropertyMap csvPropertyMap = Enumerable.SingleOrDefault<CsvPropertyMap>(this.PropertyMaps, (CsvPropertyMap m) => m.Data.Property == property || (m.Data.Property.Name == property.Name && (m.Data.Property.DeclaringType.IsAssignableFrom(property.DeclaringType) || property.DeclaringType.IsAssignableFrom(m.Data.Property.DeclaringType))));
			if (csvPropertyMap != null)
			{
				return csvPropertyMap;
			}
			CsvPropertyMap csvPropertyMap2 = new CsvPropertyMap(property);
			csvPropertyMap2.Data.Index = this.GetMaxIndex() + 1;
			this.PropertyMaps.Add(csvPropertyMap2);
			return csvPropertyMap2;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006A58 File Offset: 0x00004C58
		public virtual void AutoMap(bool ignoreReferences = false, bool prefixReferenceHeaders = false)
		{
			LinkedList<Type> linkedList = new LinkedList<Type>();
			CsvClassMap.AutoMapInternal(this, ignoreReferences, prefixReferenceHeaders, linkedList, 0);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006A78 File Offset: 0x00004C78
		internal int GetMaxIndex()
		{
			if (this.PropertyMaps.Count == 0 && this.ReferenceMaps.Count == 0)
			{
				return -1;
			}
			List<int> list = new List<int>();
			if (this.PropertyMaps.Count > 0)
			{
				list.Add(Enumerable.Max<CsvPropertyMap>(this.PropertyMaps, (CsvPropertyMap pm) => pm.Data.Index));
			}
			list.AddRange(Enumerable.Select<CsvPropertyReferenceMap, int>(this.ReferenceMaps, (CsvPropertyReferenceMap referenceMap) => referenceMap.GetMaxIndex()));
			return Enumerable.Max(list);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006B1C File Offset: 0x00004D1C
		internal int ReIndex(int indexStart = 0)
		{
			foreach (CsvPropertyMap csvPropertyMap in this.PropertyMaps)
			{
				if (!csvPropertyMap.Data.IsIndexSet)
				{
					csvPropertyMap.Data.Index = indexStart + csvPropertyMap.Data.Index;
				}
			}
			foreach (CsvPropertyReferenceMap csvPropertyReferenceMap in this.ReferenceMaps)
			{
				indexStart = csvPropertyReferenceMap.Data.Mapping.ReIndex(indexStart);
			}
			return indexStart;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006BD4 File Offset: 0x00004DD4
		internal static void AutoMapInternal(CsvClassMap map, bool ignoreReferences, bool prefixReferenceHeaders, LinkedList<Type> mapParents, int indexStart = 0)
		{
			Type type = map.GetType().GetTypeInfo().BaseType.GetGenericArguments()[0];
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				throw new CsvConfigurationException("Types that inherit IEnumerable cannot be auto mapped. Did you accidentally call GetRecord or WriteRecord which acts on a single record instead of calling GetRecords or WriteRecords which acts on a list of records?");
			}
			foreach (PropertyInfo propertyInfo in type.GetProperties(20))
			{
				Type type2 = TypeConverterFactory.GetConverter(propertyInfo.PropertyType).GetType();
				if (!(type2 == typeof(EnumerableConverter)))
				{
					bool flag = type2 == typeof(DefaultTypeConverter);
					bool flag2 = propertyInfo.PropertyType.GetConstructor(new Type[0]) != null;
					if (flag && flag2)
					{
						if (!ignoreReferences && !CsvClassMap.CheckForCircularReference(propertyInfo.PropertyType, mapParents))
						{
							mapParents.AddLast(type);
							CsvClassMap csvClassMap = (CsvClassMap)ReflectionHelper.CreateInstance(typeof(DefaultCsvClassMap<>).MakeGenericType(new Type[] { propertyInfo.PropertyType }), new object[0]);
							CsvClassMap.AutoMapInternal(csvClassMap, false, prefixReferenceHeaders, mapParents, map.GetMaxIndex() + 1);
							if (csvClassMap.PropertyMaps.Count > 0 || csvClassMap.ReferenceMaps.Count > 0)
							{
								CsvPropertyReferenceMap csvPropertyReferenceMap = new CsvPropertyReferenceMap(propertyInfo, csvClassMap);
								if (prefixReferenceHeaders)
								{
									csvPropertyReferenceMap.Prefix(null);
								}
								map.ReferenceMaps.Add(csvPropertyReferenceMap);
							}
						}
					}
					else
					{
						CsvPropertyMap csvPropertyMap = new CsvPropertyMap(propertyInfo);
						csvPropertyMap.Data.Index = map.GetMaxIndex() + 1;
						if (csvPropertyMap.Data.TypeConverter.CanConvertFrom(typeof(string)) || (csvPropertyMap.Data.TypeConverter.CanConvertTo(typeof(string)) && !flag))
						{
							map.PropertyMaps.Add(csvPropertyMap);
						}
					}
				}
			}
			map.ReIndex(indexStart);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006DAC File Offset: 0x00004FAC
		internal static bool CheckForCircularReference(Type type, LinkedList<Type> mapParents)
		{
			if (mapParents.Count == 0)
			{
				return false;
			}
			LinkedListNode<Type> linkedListNode = mapParents.Last;
			while (!(linkedListNode.Value == type))
			{
				linkedListNode = linkedListNode.Previous;
				if (linkedListNode == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400003A RID: 58
		private readonly CsvPropertyMapCollection propertyMaps = new CsvPropertyMapCollection();

		// Token: 0x0400003B RID: 59
		private readonly List<CsvPropertyReferenceMap> referenceMaps = new List<CsvPropertyReferenceMap>();

		// Token: 0x0400003C RID: 60
		[CompilerGenerated]
		private NewExpression <Constructor>k__BackingField;

		// Token: 0x0200004A RID: 74
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0<T>
		{
			// Token: 0x06000265 RID: 613 RVA: 0x00002253 File Offset: 0x00000453
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x06000266 RID: 614 RVA: 0x00008130 File Offset: 0x00006330
			internal bool <PropertyMap>b__0(CsvPropertyMap m)
			{
				return m.Data.Property == this.property || (m.Data.Property.Name == this.property.Name && (m.Data.Property.DeclaringType.IsAssignableFrom(this.property.DeclaringType) || this.property.DeclaringType.IsAssignableFrom(m.Data.Property.DeclaringType)));
			}

			// Token: 0x04000091 RID: 145
			public PropertyInfo property;
		}

		// Token: 0x0200004B RID: 75
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000267 RID: 615 RVA: 0x000081BF File Offset: 0x000063BF
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000268 RID: 616 RVA: 0x00002253 File Offset: 0x00000453
			public <>c()
			{
			}

			// Token: 0x06000269 RID: 617 RVA: 0x000081CB File Offset: 0x000063CB
			internal int <GetMaxIndex>b__14_0(CsvPropertyMap pm)
			{
				return pm.Data.Index;
			}

			// Token: 0x0600026A RID: 618 RVA: 0x000081D8 File Offset: 0x000063D8
			internal int <GetMaxIndex>b__14_1(CsvPropertyReferenceMap referenceMap)
			{
				return referenceMap.GetMaxIndex();
			}

			// Token: 0x04000092 RID: 146
			public static readonly CsvClassMap.<>c <>9 = new CsvClassMap.<>c();

			// Token: 0x04000093 RID: 147
			public static Func<CsvPropertyMap, int> <>9__14_0;

			// Token: 0x04000094 RID: 148
			public static Func<CsvPropertyReferenceMap, int> <>9__14_1;
		}
	}
}
