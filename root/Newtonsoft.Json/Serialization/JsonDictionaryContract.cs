using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000099 RID: 153
	public class JsonDictionaryContract : JsonContainerContract
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001C082 File Offset: 0x0001A282
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0001C08A File Offset: 0x0001A28A
		public Func<string, string> DictionaryKeyResolver
		{
			[CompilerGenerated]
			get
			{
				return this.<DictionaryKeyResolver>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DictionaryKeyResolver>k__BackingField = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001C093 File Offset: 0x0001A293
		public Type DictionaryKeyType
		{
			[CompilerGenerated]
			get
			{
				return this.<DictionaryKeyType>k__BackingField;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001C09B File Offset: 0x0001A29B
		public Type DictionaryValueType
		{
			[CompilerGenerated]
			get
			{
				return this.<DictionaryValueType>k__BackingField;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001C0A3 File Offset: 0x0001A2A3
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0001C0AB File Offset: 0x0001A2AB
		internal JsonContract KeyContract
		{
			[CompilerGenerated]
			get
			{
				return this.<KeyContract>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<KeyContract>k__BackingField = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001C0B4 File Offset: 0x0001A2B4
		internal bool ShouldCreateWrapper
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldCreateWrapper>k__BackingField;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001C0BC File Offset: 0x0001A2BC
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001C0E2 File Offset: 0x0001A2E2
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0001C0EA File Offset: 0x0001A2EA
		public ObjectConstructor<object> OverrideCreator
		{
			get
			{
				return this._overrideCreator;
			}
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001C0F3 File Offset: 0x0001A2F3
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0001C0FB File Offset: 0x0001A2FB
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

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001C104 File Offset: 0x0001A304
		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				return this.HasParameterizedCreator || this._parameterizedCreator != null || this._parameterizedConstructor != null;
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001C124 File Offset: 0x0001A324
		public JsonDictionaryContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Dictionary;
			Type type;
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(IDictionary), out this._genericCollectionDefinitionType))
			{
				type = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				type2 = this._genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(IDictionary)))
				{
					base.CreatedType = typeof(Dictionary).MakeGenericType(new Type[] { type, type2 });
				}
			}
			else
			{
				ReflectionUtils.GetDictionaryKeyValueTypes(base.UnderlyingType, out type, out type2);
				if (base.UnderlyingType == typeof(IDictionary))
				{
					base.CreatedType = typeof(Dictionary<object, object>);
				}
			}
			if (type != null && type2 != null)
			{
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(base.CreatedType, typeof(KeyValuePair).MakeGenericType(new Type[] { type, type2 }), typeof(IDictionary).MakeGenericType(new Type[] { type, type2 }));
				if (!this.HasParameterizedCreatorInternal && underlyingType.Name == "FSharpMap`2")
				{
					FSharpUtils.EnsureInitialized(underlyingType.Assembly());
					this._parameterizedCreator = FSharpUtils.CreateMap(type, type2);
				}
			}
			this.ShouldCreateWrapper = !typeof(IDictionary).IsAssignableFrom(base.CreatedType);
			this.DictionaryKeyType = type;
			this.DictionaryValueType = type2;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001C2A4 File Offset: 0x0001A4A4
		internal IWrappedDictionary CreateWrapper(object dictionary)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(DictionaryWrapper<, >).MakeGenericType(new Type[] { this.DictionaryKeyType, this.DictionaryValueType });
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[] { this._genericCollectionDefinitionType });
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (IWrappedDictionary)this._genericWrapperCreator(new object[] { dictionary });
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001C32C File Offset: 0x0001A52C
		internal IDictionary CreateTemporaryDictionary()
		{
			if (this._genericTemporaryDictionaryCreator == null)
			{
				Type type = typeof(Dictionary).MakeGenericType(new Type[]
				{
					this.DictionaryKeyType ?? typeof(object),
					this.DictionaryValueType ?? typeof(object)
				});
				this._genericTemporaryDictionaryCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type);
			}
			return (IDictionary)this._genericTemporaryDictionaryCreator.Invoke();
		}

		// Token: 0x040002D3 RID: 723
		[CompilerGenerated]
		private Func<string, string> <DictionaryKeyResolver>k__BackingField;

		// Token: 0x040002D4 RID: 724
		[CompilerGenerated]
		private readonly Type <DictionaryKeyType>k__BackingField;

		// Token: 0x040002D5 RID: 725
		[CompilerGenerated]
		private readonly Type <DictionaryValueType>k__BackingField;

		// Token: 0x040002D6 RID: 726
		[CompilerGenerated]
		private JsonContract <KeyContract>k__BackingField;

		// Token: 0x040002D7 RID: 727
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x040002D8 RID: 728
		private Type _genericWrapperType;

		// Token: 0x040002D9 RID: 729
		private ObjectConstructor<object> _genericWrapperCreator;

		// Token: 0x040002DA RID: 730
		private Func<object> _genericTemporaryDictionaryCreator;

		// Token: 0x040002DB RID: 731
		[CompilerGenerated]
		private readonly bool <ShouldCreateWrapper>k__BackingField;

		// Token: 0x040002DC RID: 732
		private readonly ConstructorInfo _parameterizedConstructor;

		// Token: 0x040002DD RID: 733
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x040002DE RID: 734
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x040002DF RID: 735
		[CompilerGenerated]
		private bool <HasParameterizedCreator>k__BackingField;
	}
}
