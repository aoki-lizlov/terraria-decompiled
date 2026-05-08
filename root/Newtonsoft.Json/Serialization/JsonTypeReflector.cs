using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A3 RID: 163
	internal static class JsonTypeReflector
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x000223C1 File Offset: 0x000205C1
		public static T GetCachedAttribute<T>(object attributeProvider) where T : Attribute
		{
			return CachedAttributeGetter<T>.GetAttribute(attributeProvider);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000223CC File Offset: 0x000205CC
		public static bool CanTypeDescriptorConvertString(Type type, out TypeConverter typeConverter)
		{
			typeConverter = TypeDescriptor.GetConverter(type);
			if (typeConverter != null)
			{
				Type type2 = typeConverter.GetType();
				if (!string.Equals(type2.FullName, "System.ComponentModel.ComponentConverter", 4) && !string.Equals(type2.FullName, "System.ComponentModel.ReferenceConverter", 4) && !string.Equals(type2.FullName, "System.Windows.Forms.Design.DataSourceConverter", 4) && type2 != typeof(TypeConverter))
				{
					return typeConverter.CanConvertTo(typeof(string));
				}
			}
			return false;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0002244C File Offset: 0x0002064C
		public static DataContractAttribute GetDataContractAttribute(Type type)
		{
			Type type2 = type;
			while (type2 != null)
			{
				DataContractAttribute attribute = CachedAttributeGetter<DataContractAttribute>.GetAttribute(type2);
				if (attribute != null)
				{
					return attribute;
				}
				type2 = type2.BaseType();
			}
			return null;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0002247C File Offset: 0x0002067C
		public static DataMemberAttribute GetDataMemberAttribute(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType() == 4)
			{
				return CachedAttributeGetter<DataMemberAttribute>.GetAttribute(memberInfo);
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			DataMemberAttribute dataMemberAttribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo);
			if (dataMemberAttribute == null && propertyInfo.IsVirtual())
			{
				Type type = propertyInfo.DeclaringType;
				while (dataMemberAttribute == null && type != null)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(type, propertyInfo);
					if (propertyInfo2 != null && propertyInfo2.IsVirtual())
					{
						dataMemberAttribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo2);
					}
					type = type.BaseType();
				}
			}
			return dataMemberAttribute;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000224F4 File Offset: 0x000206F4
		public static MemberSerialization GetObjectMemberSerialization(Type objectType, bool ignoreSerializableAttribute)
		{
			JsonObjectAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonObjectAttribute>(objectType);
			if (cachedAttribute != null)
			{
				return cachedAttribute.MemberSerialization;
			}
			if (JsonTypeReflector.GetDataContractAttribute(objectType) != null)
			{
				return MemberSerialization.OptIn;
			}
			if (!ignoreSerializableAttribute && JsonTypeReflector.IsSerializable(objectType))
			{
				return MemberSerialization.Fields;
			}
			return MemberSerialization.OptOut;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002252C File Offset: 0x0002072C
		public static JsonConverter GetJsonConverter(object attributeProvider)
		{
			JsonConverterAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonConverterAttribute>(attributeProvider);
			if (cachedAttribute != null)
			{
				Func<object[], object> func = JsonTypeReflector.CreatorCache.Get(cachedAttribute.ConverterType);
				if (func != null)
				{
					return (JsonConverter)func.Invoke(cachedAttribute.ConverterParameters);
				}
			}
			return null;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002256A File Offset: 0x0002076A
		public static JsonConverter CreateJsonConverterInstance(Type converterType, object[] converterArgs)
		{
			return (JsonConverter)JsonTypeReflector.CreatorCache.Get(converterType).Invoke(converterArgs);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00022582 File Offset: 0x00020782
		public static NamingStrategy CreateNamingStrategyInstance(Type namingStrategyType, object[] converterArgs)
		{
			return (NamingStrategy)JsonTypeReflector.CreatorCache.Get(namingStrategyType).Invoke(converterArgs);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0002259A File Offset: 0x0002079A
		public static NamingStrategy GetContainerNamingStrategy(JsonContainerAttribute containerAttribute)
		{
			if (containerAttribute.NamingStrategyInstance == null)
			{
				if (containerAttribute.NamingStrategyType == null)
				{
					return null;
				}
				containerAttribute.NamingStrategyInstance = JsonTypeReflector.CreateNamingStrategyInstance(containerAttribute.NamingStrategyType, containerAttribute.NamingStrategyParameters);
			}
			return containerAttribute.NamingStrategyInstance;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000225D4 File Offset: 0x000207D4
		private static Func<object[], object> GetCreator(Type type)
		{
			Func<object> defaultConstructor = (ReflectionUtils.HasDefaultConstructor(type, false) ? JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type) : null);
			return delegate(object[] parameters)
			{
				object obj;
				try
				{
					if (parameters != null)
					{
						Type[] array = Enumerable.ToArray<Type>(Enumerable.Select<object, Type>(parameters, (object param) => param.GetType()));
						ConstructorInfo constructor = type.GetConstructor(array);
						if (!(null != constructor))
						{
							throw new JsonException("No matching parameterized constructor found for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
						}
						obj = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor)(parameters);
					}
					else
					{
						if (defaultConstructor == null)
						{
							throw new JsonException("No parameterless constructor defined for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
						}
						obj = defaultConstructor.Invoke();
					}
				}
				catch (Exception ex)
				{
					throw new JsonException("Error creating '{0}'.".FormatWith(CultureInfo.InvariantCulture, type), ex);
				}
				return obj;
			};
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00022621 File Offset: 0x00020821
		private static Type GetAssociatedMetadataType(Type type)
		{
			return JsonTypeReflector.AssociatedMetadataTypesCache.Get(type);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00022630 File Offset: 0x00020830
		private static Type GetAssociateMetadataTypeFromAttribute(Type type)
		{
			foreach (Attribute attribute in ReflectionUtils.GetAttributes(type, null, true))
			{
				Type type2 = attribute.GetType();
				if (string.Equals(type2.FullName, "System.ComponentModel.DataAnnotations.MetadataTypeAttribute", 4))
				{
					if (JsonTypeReflector._metadataTypeAttributeReflectionObject == null)
					{
						JsonTypeReflector._metadataTypeAttributeReflectionObject = ReflectionObject.Create(type2, new string[] { "MetadataClassType" });
					}
					return (Type)JsonTypeReflector._metadataTypeAttributeReflectionObject.GetValue(attribute, "MetadataClassType");
				}
			}
			return null;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000226AC File Offset: 0x000208AC
		private static T GetAttribute<T>(Type type) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(type);
			T t;
			if (associatedMetadataType != null)
			{
				t = ReflectionUtils.GetAttribute<T>(associatedMetadataType, true);
				if (t != null)
				{
					return t;
				}
			}
			t = ReflectionUtils.GetAttribute<T>(type, true);
			if (t != null)
			{
				return t;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				t = ReflectionUtils.GetAttribute<T>(interfaces[i], true);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00022720 File Offset: 0x00020920
		private static T GetAttribute<T>(MemberInfo memberInfo) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(memberInfo.DeclaringType);
			T t;
			if (associatedMetadataType != null)
			{
				MemberInfo memberInfoFromType = ReflectionUtils.GetMemberInfoFromType(associatedMetadataType, memberInfo);
				if (memberInfoFromType != null)
				{
					t = ReflectionUtils.GetAttribute<T>(memberInfoFromType, true);
					if (t != null)
					{
						return t;
					}
				}
			}
			t = ReflectionUtils.GetAttribute<T>(memberInfo, true);
			if (t != null)
			{
				return t;
			}
			if (memberInfo.DeclaringType != null)
			{
				Type[] interfaces = memberInfo.DeclaringType.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					MemberInfo memberInfoFromType2 = ReflectionUtils.GetMemberInfoFromType(interfaces[i], memberInfo);
					if (memberInfoFromType2 != null)
					{
						t = ReflectionUtils.GetAttribute<T>(memberInfoFromType2, true);
						if (t != null)
						{
							return t;
						}
					}
				}
			}
			return default(T);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000227D6 File Offset: 0x000209D6
		public static bool IsNonSerializable(object provider)
		{
			return JsonTypeReflector.GetCachedAttribute<NonSerializedAttribute>(provider) != null;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000227E1 File Offset: 0x000209E1
		public static bool IsSerializable(object provider)
		{
			return JsonTypeReflector.GetCachedAttribute<SerializableAttribute>(provider) != null;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000227EC File Offset: 0x000209EC
		public static T GetAttribute<T>(object provider) where T : Attribute
		{
			Type type = provider as Type;
			if (type != null)
			{
				return JsonTypeReflector.GetAttribute<T>(type);
			}
			MemberInfo memberInfo = provider as MemberInfo;
			if (memberInfo != null)
			{
				return JsonTypeReflector.GetAttribute<T>(memberInfo);
			}
			return ReflectionUtils.GetAttribute<T>(provider, true);
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00022830 File Offset: 0x00020A30
		public static bool DynamicCodeGeneration
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonTypeReflector._dynamicCodeGeneration == null)
				{
					try
					{
						new ReflectionPermission(2).Demand();
						new ReflectionPermission(8).Demand();
						new SecurityPermission(4).Demand();
						new SecurityPermission(2).Demand();
						new SecurityPermission(1).Demand();
						JsonTypeReflector._dynamicCodeGeneration = new bool?(true);
					}
					catch (Exception)
					{
						JsonTypeReflector._dynamicCodeGeneration = new bool?(false);
					}
				}
				return JsonTypeReflector._dynamicCodeGeneration.GetValueOrDefault();
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x000228B8 File Offset: 0x00020AB8
		public static bool FullyTrusted
		{
			get
			{
				if (JsonTypeReflector._fullyTrusted == null)
				{
					AppDomain currentDomain = AppDomain.CurrentDomain;
					JsonTypeReflector._fullyTrusted = new bool?(currentDomain.IsHomogenous && currentDomain.IsFullyTrusted);
				}
				return JsonTypeReflector._fullyTrusted.GetValueOrDefault();
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x000228FC File Offset: 0x00020AFC
		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				if (JsonTypeReflector.DynamicCodeGeneration)
				{
					return DynamicReflectionDelegateFactory.Instance;
				}
				return LateBoundReflectionDelegateFactory.Instance;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00022910 File Offset: 0x00020B10
		// Note: this type is marked as 'beforefieldinit'.
		static JsonTypeReflector()
		{
		}

		// Token: 0x0400031A RID: 794
		private static bool? _dynamicCodeGeneration;

		// Token: 0x0400031B RID: 795
		private static bool? _fullyTrusted;

		// Token: 0x0400031C RID: 796
		public const string IdPropertyName = "$id";

		// Token: 0x0400031D RID: 797
		public const string RefPropertyName = "$ref";

		// Token: 0x0400031E RID: 798
		public const string TypePropertyName = "$type";

		// Token: 0x0400031F RID: 799
		public const string ValuePropertyName = "$value";

		// Token: 0x04000320 RID: 800
		public const string ArrayValuesPropertyName = "$values";

		// Token: 0x04000321 RID: 801
		public const string ShouldSerializePrefix = "ShouldSerialize";

		// Token: 0x04000322 RID: 802
		public const string SpecifiedPostfix = "Specified";

		// Token: 0x04000323 RID: 803
		private static readonly ThreadSafeStore<Type, Func<object[], object>> CreatorCache = new ThreadSafeStore<Type, Func<object[], object>>(new Func<Type, Func<object[], object>>(JsonTypeReflector.GetCreator));

		// Token: 0x04000324 RID: 804
		private static readonly ThreadSafeStore<Type, Type> AssociatedMetadataTypesCache = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(JsonTypeReflector.GetAssociateMetadataTypeFromAttribute));

		// Token: 0x04000325 RID: 805
		private static ReflectionObject _metadataTypeAttributeReflectionObject;

		// Token: 0x0200014F RID: 335
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x06000D0F RID: 3343 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x06000D10 RID: 3344 RVA: 0x00031660 File Offset: 0x0002F860
			internal object <GetCreator>b__0(object[] parameters)
			{
				object obj;
				try
				{
					if (parameters != null)
					{
						Type[] array = Enumerable.ToArray<Type>(Enumerable.Select<object, Type>(parameters, (object param) => param.GetType()));
						ConstructorInfo constructor = this.type.GetConstructor(array);
						if (!(null != constructor))
						{
							throw new JsonException("No matching parameterized constructor found for '{0}'.".FormatWith(CultureInfo.InvariantCulture, this.type));
						}
						obj = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor)(parameters);
					}
					else
					{
						if (this.defaultConstructor == null)
						{
							throw new JsonException("No parameterless constructor defined for '{0}'.".FormatWith(CultureInfo.InvariantCulture, this.type));
						}
						obj = this.defaultConstructor.Invoke();
					}
				}
				catch (Exception ex)
				{
					throw new JsonException("Error creating '{0}'.".FormatWith(CultureInfo.InvariantCulture, this.type), ex);
				}
				return obj;
			}

			// Token: 0x040004D2 RID: 1234
			public Type type;

			// Token: 0x040004D3 RID: 1235
			public Func<object> defaultConstructor;
		}

		// Token: 0x02000150 RID: 336
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000D11 RID: 3345 RVA: 0x00031740 File Offset: 0x0002F940
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000D12 RID: 3346 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000D13 RID: 3347 RVA: 0x0003174C File Offset: 0x0002F94C
			internal Type <GetCreator>b__21_1(object param)
			{
				return param.GetType();
			}

			// Token: 0x040004D4 RID: 1236
			public static readonly JsonTypeReflector.<>c <>9 = new JsonTypeReflector.<>c();

			// Token: 0x040004D5 RID: 1237
			public static Func<object, Type> <>9__21_1;
		}
	}
}
