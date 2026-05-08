using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000092 RID: 146
	public class JsonArrayContract : JsonContainerContract
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001B7B7 File Offset: 0x000199B7
		public Type CollectionItemType
		{
			[CompilerGenerated]
			get
			{
				return this.<CollectionItemType>k__BackingField;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001B7BF File Offset: 0x000199BF
		public bool IsMultidimensionalArray
		{
			[CompilerGenerated]
			get
			{
				return this.<IsMultidimensionalArray>k__BackingField;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001B7C7 File Offset: 0x000199C7
		internal bool IsArray
		{
			[CompilerGenerated]
			get
			{
				return this.<IsArray>k__BackingField;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001B7CF File Offset: 0x000199CF
		internal bool ShouldCreateWrapper
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldCreateWrapper>k__BackingField;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001B7D7 File Offset: 0x000199D7
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0001B7DF File Offset: 0x000199DF
		internal bool CanDeserialize
		{
			[CompilerGenerated]
			get
			{
				return this.<CanDeserialize>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CanDeserialize>k__BackingField = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001B7E8 File Offset: 0x000199E8
		internal ObjectConstructor<object> ParameterizedCreator
		{
			get
			{
				if (this._parameterizedCreator == null)
				{
					this._parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(this._parameterizedConstructor);
				}
				return this._parameterizedCreator;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001B80E File Offset: 0x00019A0E
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001B816 File Offset: 0x00019A16
		public ObjectConstructor<object> OverrideCreator
		{
			get
			{
				return this._overrideCreator;
			}
			set
			{
				this._overrideCreator = value;
				this.CanDeserialize = true;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001B826 File Offset: 0x00019A26
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001B82E File Offset: 0x00019A2E
		public bool HasParameterizedCreator
		{
			[CompilerGenerated]
			get
			{
				return this.<HasParameterizedCreator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HasParameterizedCreator>k__BackingField = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001B837 File Offset: 0x00019A37
		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				return this.HasParameterizedCreator || this._parameterizedCreator != null || this._parameterizedConstructor != null;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001B858 File Offset: 0x00019A58
		public JsonArrayContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Array;
			this.IsArray = base.CreatedType.IsArray;
			bool flag;
			Type type;
			if (this.IsArray)
			{
				this.CollectionItemType = ReflectionUtils.GetCollectionItemType(base.UnderlyingType);
				this.IsReadOnlyOrFixedSize = true;
				this._genericCollectionDefinitionType = typeof(List).MakeGenericType(new Type[] { this.CollectionItemType });
				flag = true;
				this.IsMultidimensionalArray = this.IsArray && base.UnderlyingType.GetArrayRank() > 1;
			}
			else if (typeof(IList).IsAssignableFrom(underlyingType))
			{
				if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(ICollection), out this._genericCollectionDefinitionType))
				{
					this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				}
				else
				{
					this.CollectionItemType = ReflectionUtils.GetCollectionItemType(underlyingType);
				}
				if (underlyingType == typeof(IList))
				{
					base.CreatedType = typeof(List<object>);
				}
				if (this.CollectionItemType != null)
				{
					this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(underlyingType, this.CollectionItemType);
				}
				this.IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(underlyingType, typeof(ReadOnlyCollection));
				flag = true;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(ICollection), out this._genericCollectionDefinitionType))
			{
				this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(underlyingType, typeof(ICollection)) || ReflectionUtils.IsGenericDefinition(underlyingType, typeof(IList)))
				{
					base.CreatedType = typeof(List).MakeGenericType(new Type[] { this.CollectionItemType });
				}
				if (ReflectionUtils.IsGenericDefinition(underlyingType, typeof(ISet)))
				{
					base.CreatedType = typeof(HashSet).MakeGenericType(new Type[] { this.CollectionItemType });
				}
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(underlyingType, this.CollectionItemType);
				flag = true;
				this.ShouldCreateWrapper = true;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(IEnumerable), out type))
			{
				this.CollectionItemType = type.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(IEnumerable)))
				{
					base.CreatedType = typeof(List).MakeGenericType(new Type[] { this.CollectionItemType });
				}
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(underlyingType, this.CollectionItemType);
				this.StoreFSharpListCreatorIfNecessary(underlyingType);
				if (underlyingType.IsGenericType() && underlyingType.GetGenericTypeDefinition() == typeof(IEnumerable))
				{
					this._genericCollectionDefinitionType = type;
					this.IsReadOnlyOrFixedSize = false;
					this.ShouldCreateWrapper = false;
					flag = true;
				}
				else
				{
					this._genericCollectionDefinitionType = typeof(List).MakeGenericType(new Type[] { this.CollectionItemType });
					this.IsReadOnlyOrFixedSize = true;
					this.ShouldCreateWrapper = true;
					flag = this.HasParameterizedCreatorInternal;
				}
			}
			else
			{
				flag = false;
				this.ShouldCreateWrapper = true;
			}
			this.CanDeserialize = flag;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001BB64 File Offset: 0x00019D64
		internal IWrappedCollection CreateWrapper(object list)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(CollectionWrapper<>).MakeGenericType(new Type[] { this.CollectionItemType });
				Type type;
				if (ReflectionUtils.InheritsGenericDefinition(this._genericCollectionDefinitionType, typeof(List)) || this._genericCollectionDefinitionType.GetGenericTypeDefinition() == typeof(IEnumerable))
				{
					type = typeof(ICollection).MakeGenericType(new Type[] { this.CollectionItemType });
				}
				else
				{
					type = this._genericCollectionDefinitionType;
				}
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[] { type });
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (IWrappedCollection)this._genericWrapperCreator(new object[] { list });
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001BC3C File Offset: 0x00019E3C
		internal IList CreateTemporaryCollection()
		{
			if (this._genericTemporaryCollectionCreator == null)
			{
				Type type = ((this.IsMultidimensionalArray || this.CollectionItemType == null) ? typeof(object) : this.CollectionItemType);
				Type type2 = typeof(List).MakeGenericType(new Type[] { type });
				this._genericTemporaryCollectionCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type2);
			}
			return (IList)this._genericTemporaryCollectionCreator.Invoke();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001BCB5 File Offset: 0x00019EB5
		private void StoreFSharpListCreatorIfNecessary(Type underlyingType)
		{
			if (!this.HasParameterizedCreatorInternal && underlyingType.Name == "FSharpList`1")
			{
				FSharpUtils.EnsureInitialized(underlyingType.Assembly());
				this._parameterizedCreator = FSharpUtils.CreateSeq(this.CollectionItemType);
			}
		}

		// Token: 0x040002A7 RID: 679
		[CompilerGenerated]
		private readonly Type <CollectionItemType>k__BackingField;

		// Token: 0x040002A8 RID: 680
		[CompilerGenerated]
		private readonly bool <IsMultidimensionalArray>k__BackingField;

		// Token: 0x040002A9 RID: 681
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x040002AA RID: 682
		private Type _genericWrapperType;

		// Token: 0x040002AB RID: 683
		private ObjectConstructor<object> _genericWrapperCreator;

		// Token: 0x040002AC RID: 684
		private Func<object> _genericTemporaryCollectionCreator;

		// Token: 0x040002AD RID: 685
		[CompilerGenerated]
		private readonly bool <IsArray>k__BackingField;

		// Token: 0x040002AE RID: 686
		[CompilerGenerated]
		private readonly bool <ShouldCreateWrapper>k__BackingField;

		// Token: 0x040002AF RID: 687
		[CompilerGenerated]
		private bool <CanDeserialize>k__BackingField;

		// Token: 0x040002B0 RID: 688
		private readonly ConstructorInfo _parameterizedConstructor;

		// Token: 0x040002B1 RID: 689
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x040002B2 RID: 690
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x040002B3 RID: 691
		[CompilerGenerated]
		private bool <HasParameterizedCreator>k__BackingField;
	}
}
