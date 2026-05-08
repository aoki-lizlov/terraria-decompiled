using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CsvHelper.Configuration
{
	// Token: 0x02000036 RID: 54
	public abstract class CsvClassMap<T> : CsvClassMap
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00006F3E File Offset: 0x0000513E
		public virtual void ConstructUsing(Expression<Func<T>> expression)
		{
			this.Constructor = ReflectionHelper.GetConstructor<T>(expression);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006F4C File Offset: 0x0000514C
		public virtual CsvPropertyMap Map(Expression<Func<T, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.GetProperty<T>(expression);
			CsvPropertyMap csvPropertyMap = Enumerable.SingleOrDefault<CsvPropertyMap>(this.PropertyMaps, (CsvPropertyMap m) => m.Data.Property == property || (m.Data.Property.Name == property.Name && (m.Data.Property.DeclaringType.GetTypeInfo().IsAssignableFrom(property.DeclaringType.GetTypeInfo()) || property.DeclaringType.IsAssignableFrom(m.Data.Property.DeclaringType))));
			if (csvPropertyMap != null)
			{
				return csvPropertyMap;
			}
			CsvPropertyMap csvPropertyMap2 = new CsvPropertyMap(property);
			csvPropertyMap2.Data.Index = base.GetMaxIndex() + 1;
			this.PropertyMaps.Add(csvPropertyMap2);
			return csvPropertyMap2;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006FB4 File Offset: 0x000051B4
		public virtual CsvPropertyReferenceMap References<TClassMap>(Expression<Func<T, object>> expression, params object[] constructorArgs) where TClassMap : CsvClassMap
		{
			PropertyInfo property = ReflectionHelper.GetProperty<T>(expression);
			CsvPropertyReferenceMap csvPropertyReferenceMap = Enumerable.SingleOrDefault<CsvPropertyReferenceMap>(this.ReferenceMaps, (CsvPropertyReferenceMap m) => m.Data.Property == property || (m.Data.Property.Name == property.Name && (m.Data.Property.DeclaringType.IsAssignableFrom(property.DeclaringType) || property.DeclaringType.IsAssignableFrom(m.Data.Property.DeclaringType))));
			if (csvPropertyReferenceMap != null)
			{
				return csvPropertyReferenceMap;
			}
			TClassMap tclassMap = ReflectionHelper.CreateInstance<TClassMap>(constructorArgs);
			tclassMap.CreateMap();
			tclassMap.ReIndex(base.GetMaxIndex() + 1);
			CsvPropertyReferenceMap csvPropertyReferenceMap2 = new CsvPropertyReferenceMap(property, tclassMap);
			this.ReferenceMaps.Add(csvPropertyReferenceMap2);
			return csvPropertyReferenceMap2;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007038 File Offset: 0x00005238
		[Obsolete("This method is deprecated and will be removed in the next major release. Use References<TClassMap>( Expression<Func<T, object>> expression, params object[] constructorArgs ) instead.", false)]
		protected virtual CsvPropertyReferenceMap References(Type type, Expression<Func<T, object>> expression, params object[] constructorArgs)
		{
			PropertyInfo property = ReflectionHelper.GetProperty<T>(expression);
			CsvPropertyReferenceMap csvPropertyReferenceMap = Enumerable.SingleOrDefault<CsvPropertyReferenceMap>(this.ReferenceMaps, (CsvPropertyReferenceMap m) => m.Data.Property == property || (m.Data.Property.Name == property.Name && (m.Data.Property.DeclaringType.IsAssignableFrom(property.DeclaringType) || property.DeclaringType.IsAssignableFrom(m.Data.Property.DeclaringType))));
			if (csvPropertyReferenceMap != null)
			{
				return csvPropertyReferenceMap;
			}
			CsvClassMap csvClassMap = (CsvClassMap)ReflectionHelper.CreateInstance(type, constructorArgs);
			csvClassMap.CreateMap();
			csvClassMap.ReIndex(base.GetMaxIndex() + 1);
			CsvPropertyReferenceMap csvPropertyReferenceMap2 = new CsvPropertyReferenceMap(property, csvClassMap);
			this.ReferenceMaps.Add(csvPropertyReferenceMap2);
			return csvPropertyReferenceMap2;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000070B0 File Offset: 0x000052B0
		protected CsvClassMap()
		{
		}

		// Token: 0x0200004C RID: 76
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x0600026B RID: 619 RVA: 0x00002253 File Offset: 0x00000453
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x0600026C RID: 620 RVA: 0x000081E0 File Offset: 0x000063E0
			internal bool <Map>b__0(CsvPropertyMap m)
			{
				return m.Data.Property == this.property || (m.Data.Property.Name == this.property.Name && (m.Data.Property.DeclaringType.GetTypeInfo().IsAssignableFrom(this.property.DeclaringType.GetTypeInfo()) || this.property.DeclaringType.IsAssignableFrom(m.Data.Property.DeclaringType)));
			}

			// Token: 0x04000095 RID: 149
			public PropertyInfo property;
		}

		// Token: 0x0200004D RID: 77
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0<TClassMap> where TClassMap : CsvClassMap
		{
			// Token: 0x0600026D RID: 621 RVA: 0x00002253 File Offset: 0x00000453
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x0600026E RID: 622 RVA: 0x0000827C File Offset: 0x0000647C
			internal bool <References>b__0(CsvPropertyReferenceMap m)
			{
				return m.Data.Property == this.property || (m.Data.Property.Name == this.property.Name && (m.Data.Property.DeclaringType.IsAssignableFrom(this.property.DeclaringType) || this.property.DeclaringType.IsAssignableFrom(m.Data.Property.DeclaringType)));
			}

			// Token: 0x04000096 RID: 150
			public PropertyInfo property;
		}

		// Token: 0x0200004E RID: 78
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x0600026F RID: 623 RVA: 0x00002253 File Offset: 0x00000453
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000270 RID: 624 RVA: 0x0000830C File Offset: 0x0000650C
			internal bool <References>b__0(CsvPropertyReferenceMap m)
			{
				return m.Data.Property == this.property || (m.Data.Property.Name == this.property.Name && (m.Data.Property.DeclaringType.IsAssignableFrom(this.property.DeclaringType) || this.property.DeclaringType.IsAssignableFrom(m.Data.Property.DeclaringType)));
			}

			// Token: 0x04000097 RID: 151
			public PropertyInfo property;
		}
	}
}
