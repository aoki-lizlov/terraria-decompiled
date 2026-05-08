using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008D RID: 141
	public class DefaultContractResolver : IContractResolver
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000197A1 File Offset: 0x000179A1
		internal static IContractResolver Instance
		{
			get
			{
				return DefaultContractResolver._instance;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x000197A8 File Offset: 0x000179A8
		public bool DynamicCodeGeneration
		{
			get
			{
				return JsonTypeReflector.DynamicCodeGeneration;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000197AF File Offset: 0x000179AF
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x000197B7 File Offset: 0x000179B7
		[Obsolete("DefaultMembersSearchFlags is obsolete. To modify the members serialized inherit from DefaultContractResolver and override the GetSerializableMembers method instead.")]
		public BindingFlags DefaultMembersSearchFlags
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultMembersSearchFlags>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DefaultMembersSearchFlags>k__BackingField = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000197C0 File Offset: 0x000179C0
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x000197C8 File Offset: 0x000179C8
		public bool SerializeCompilerGeneratedMembers
		{
			[CompilerGenerated]
			get
			{
				return this.<SerializeCompilerGeneratedMembers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SerializeCompilerGeneratedMembers>k__BackingField = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x000197D1 File Offset: 0x000179D1
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x000197D9 File Offset: 0x000179D9
		public bool IgnoreSerializableInterface
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreSerializableInterface>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnoreSerializableInterface>k__BackingField = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000197E2 File Offset: 0x000179E2
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x000197EA File Offset: 0x000179EA
		public bool IgnoreSerializableAttribute
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreSerializableAttribute>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnoreSerializableAttribute>k__BackingField = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000197F3 File Offset: 0x000179F3
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x000197FB File Offset: 0x000179FB
		public NamingStrategy NamingStrategy
		{
			[CompilerGenerated]
			get
			{
				return this.<NamingStrategy>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NamingStrategy>k__BackingField = value;
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00019804 File Offset: 0x00017A04
		public DefaultContractResolver()
		{
			this.IgnoreSerializableAttribute = true;
			this.DefaultMembersSearchFlags = 20;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00019834 File Offset: 0x00017A34
		public virtual JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Dictionary<Type, JsonContract> dictionary = this._contractCache;
			JsonContract jsonContract;
			if (dictionary == null || !dictionary.TryGetValue(type, ref jsonContract))
			{
				jsonContract = this.CreateContract(type);
				object typeContractCacheLock = this._typeContractCacheLock;
				lock (typeContractCacheLock)
				{
					dictionary = this._contractCache;
					Dictionary<Type, JsonContract> dictionary2 = ((dictionary != null) ? new Dictionary<Type, JsonContract>(dictionary) : new Dictionary<Type, JsonContract>());
					dictionary2[type] = jsonContract;
					this._contractCache = dictionary2;
				}
			}
			return jsonContract;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000198CC File Offset: 0x00017ACC
		protected virtual List<MemberInfo> GetSerializableMembers(Type objectType)
		{
			bool ignoreSerializableAttribute = this.IgnoreSerializableAttribute;
			MemberSerialization objectMemberSerialization = JsonTypeReflector.GetObjectMemberSerialization(objectType, ignoreSerializableAttribute);
			IEnumerable<MemberInfo> enumerable = Enumerable.Where<MemberInfo>(ReflectionUtils.GetFieldsAndProperties(objectType, 60), (MemberInfo m) => !ReflectionUtils.IsIndexedProperty(m));
			List<MemberInfo> list = new List<MemberInfo>();
			if (objectMemberSerialization != MemberSerialization.Fields)
			{
				DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(objectType);
				List<MemberInfo> list2 = Enumerable.ToList<MemberInfo>(Enumerable.Where<MemberInfo>(ReflectionUtils.GetFieldsAndProperties(objectType, this.DefaultMembersSearchFlags), (MemberInfo m) => !ReflectionUtils.IsIndexedProperty(m)));
				foreach (MemberInfo memberInfo in enumerable)
				{
					if (this.SerializeCompilerGeneratedMembers || !memberInfo.IsDefined(typeof(CompilerGeneratedAttribute), true))
					{
						if (list2.Contains(memberInfo))
						{
							list.Add(memberInfo);
						}
						else if (JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(memberInfo) != null)
						{
							list.Add(memberInfo);
						}
						else if (JsonTypeReflector.GetAttribute<JsonRequiredAttribute>(memberInfo) != null)
						{
							list.Add(memberInfo);
						}
						else if (dataContractAttribute != null && JsonTypeReflector.GetAttribute<DataMemberAttribute>(memberInfo) != null)
						{
							list.Add(memberInfo);
						}
						else if (objectMemberSerialization == MemberSerialization.Fields && memberInfo.MemberType() == 4)
						{
							list.Add(memberInfo);
						}
					}
				}
				Type type;
				if (objectType.AssignableToTypeName("System.Data.Objects.DataClasses.EntityObject", false, out type))
				{
					list = Enumerable.ToList<MemberInfo>(Enumerable.Where<MemberInfo>(list, new Func<MemberInfo, bool>(this.ShouldSerializeEntityMember)));
				}
			}
			else
			{
				foreach (MemberInfo memberInfo2 in enumerable)
				{
					FieldInfo fieldInfo = memberInfo2 as FieldInfo;
					if (fieldInfo != null && !fieldInfo.IsStatic)
					{
						list.Add(memberInfo2);
					}
				}
			}
			return list;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00019AAC File Offset: 0x00017CAC
		private bool ShouldSerializeEntityMember(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			return !(propertyInfo != null) || !propertyInfo.PropertyType.IsGenericType() || !(propertyInfo.PropertyType.GetGenericTypeDefinition().FullName == "System.Data.Objects.DataClasses.EntityReference`1");
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00019AF8 File Offset: 0x00017CF8
		protected virtual JsonObjectContract CreateObjectContract(Type objectType)
		{
			JsonObjectContract jsonObjectContract = new JsonObjectContract(objectType);
			this.InitializeContract(jsonObjectContract);
			bool ignoreSerializableAttribute = this.IgnoreSerializableAttribute;
			jsonObjectContract.MemberSerialization = JsonTypeReflector.GetObjectMemberSerialization(jsonObjectContract.NonNullableUnderlyingType, ignoreSerializableAttribute);
			jsonObjectContract.Properties.AddRange(this.CreateProperties(jsonObjectContract.NonNullableUnderlyingType, jsonObjectContract.MemberSerialization));
			Func<string, string> func = null;
			JsonObjectAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonObjectAttribute>(jsonObjectContract.NonNullableUnderlyingType);
			if (cachedAttribute != null)
			{
				jsonObjectContract.ItemRequired = cachedAttribute._itemRequired;
				if (cachedAttribute.NamingStrategyType != null)
				{
					NamingStrategy namingStrategy = JsonTypeReflector.GetContainerNamingStrategy(cachedAttribute);
					func = (string s) => namingStrategy.GetDictionaryKey(s);
				}
			}
			if (func == null)
			{
				func = new Func<string, string>(this.ResolveExtensionDataName);
			}
			jsonObjectContract.ExtensionDataNameResolver = func;
			if (jsonObjectContract.IsInstantiable)
			{
				ConstructorInfo attributeConstructor = this.GetAttributeConstructor(jsonObjectContract.NonNullableUnderlyingType);
				if (attributeConstructor != null)
				{
					jsonObjectContract.OverrideCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(attributeConstructor);
					jsonObjectContract.CreatorParameters.AddRange(this.CreateConstructorParameters(attributeConstructor, jsonObjectContract.Properties));
				}
				else if (jsonObjectContract.MemberSerialization == MemberSerialization.Fields)
				{
					if (JsonTypeReflector.FullyTrusted)
					{
						jsonObjectContract.DefaultCreator = new Func<object>(jsonObjectContract.GetUninitializedObject);
					}
				}
				else if (jsonObjectContract.DefaultCreator == null || jsonObjectContract.DefaultCreatorNonPublic)
				{
					ConstructorInfo parameterizedConstructor = this.GetParameterizedConstructor(jsonObjectContract.NonNullableUnderlyingType);
					if (parameterizedConstructor != null)
					{
						jsonObjectContract.ParameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(parameterizedConstructor);
						jsonObjectContract.CreatorParameters.AddRange(this.CreateConstructorParameters(parameterizedConstructor, jsonObjectContract.Properties));
					}
				}
				else if (jsonObjectContract.NonNullableUnderlyingType.IsValueType())
				{
					ConstructorInfo immutableConstructor = this.GetImmutableConstructor(jsonObjectContract.NonNullableUnderlyingType, jsonObjectContract.Properties);
					if (immutableConstructor != null)
					{
						jsonObjectContract.OverrideCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(immutableConstructor);
						jsonObjectContract.CreatorParameters.AddRange(this.CreateConstructorParameters(immutableConstructor, jsonObjectContract.Properties));
					}
				}
			}
			MemberInfo extensionDataMemberForType = this.GetExtensionDataMemberForType(jsonObjectContract.NonNullableUnderlyingType);
			if (extensionDataMemberForType != null)
			{
				DefaultContractResolver.SetExtensionDataDelegates(jsonObjectContract, extensionDataMemberForType);
			}
			return jsonObjectContract;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00019CF8 File Offset: 0x00017EF8
		private MemberInfo GetExtensionDataMemberForType(Type type)
		{
			return Enumerable.LastOrDefault<MemberInfo>(Enumerable.SelectMany<Type, MemberInfo>(this.GetClassHierarchyForType(type), delegate(Type baseType)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				list.AddRange(baseType.GetProperties(54));
				list.AddRange(baseType.GetFields(54));
				return list;
			}), delegate(MemberInfo m)
			{
				MemberTypes memberTypes = m.MemberType();
				if (memberTypes != 16 && memberTypes != 4)
				{
					return false;
				}
				if (!m.IsDefined(typeof(JsonExtensionDataAttribute), false))
				{
					return false;
				}
				if (!ReflectionUtils.CanReadMemberValue(m, true))
				{
					throw new JsonException("Invalid extension data attribute on '{0}'. Member '{1}' must have a getter.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(m.DeclaringType), m.Name));
				}
				Type type2;
				if (ReflectionUtils.ImplementsGenericDefinition(ReflectionUtils.GetMemberUnderlyingType(m), typeof(IDictionary), out type2))
				{
					Type type3 = type2.GetGenericArguments()[0];
					Type type4 = type2.GetGenericArguments()[1];
					if (type3.IsAssignableFrom(typeof(string)) && type4.IsAssignableFrom(typeof(JToken)))
					{
						return true;
					}
				}
				throw new JsonException("Invalid extension data attribute on '{0}'. Member '{1}' type must implement IDictionary<string, JToken>.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(m.DeclaringType), m.Name));
			});
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00019D54 File Offset: 0x00017F54
		private static void SetExtensionDataDelegates(JsonObjectContract contract, MemberInfo member)
		{
			JsonExtensionDataAttribute attribute = ReflectionUtils.GetAttribute<JsonExtensionDataAttribute>(member);
			if (attribute == null)
			{
				return;
			}
			Type memberUnderlyingType = ReflectionUtils.GetMemberUnderlyingType(member);
			Type type;
			ReflectionUtils.ImplementsGenericDefinition(memberUnderlyingType, typeof(IDictionary), out type);
			Type type2 = type.GetGenericArguments()[0];
			Type type3 = type.GetGenericArguments()[1];
			Type type4;
			if (ReflectionUtils.IsGenericDefinition(memberUnderlyingType, typeof(IDictionary)))
			{
				type4 = typeof(Dictionary).MakeGenericType(new Type[] { type2, type3 });
			}
			else
			{
				type4 = memberUnderlyingType;
			}
			Func<object, object> getExtensionDataDictionary = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(member);
			if (attribute.ReadData)
			{
				Action<object, object> setExtensionDataDictionary = (ReflectionUtils.CanSetMemberValue(member, true, false) ? JsonTypeReflector.ReflectionDelegateFactory.CreateSet<object>(member) : null);
				Func<object> createExtensionDataDictionary = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type4);
				MethodInfo method = memberUnderlyingType.GetMethod("Add", new Type[] { type2, type3 });
				MethodCall<object, object> setExtensionDataDictionaryValue = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
				ExtensionDataSetter extensionDataSetter = delegate(object o, string key, object value)
				{
					object obj = getExtensionDataDictionary.Invoke(o);
					if (obj == null)
					{
						if (setExtensionDataDictionary == null)
						{
							throw new JsonSerializationException("Cannot set value onto extension data member '{0}'. The extension data collection is null and it cannot be set.".FormatWith(CultureInfo.InvariantCulture, member.Name));
						}
						obj = createExtensionDataDictionary.Invoke();
						setExtensionDataDictionary.Invoke(o, obj);
					}
					setExtensionDataDictionaryValue(obj, new object[] { key, value });
				};
				contract.ExtensionDataSetter = extensionDataSetter;
			}
			if (attribute.WriteData)
			{
				ConstructorInfo constructorInfo = Enumerable.First<ConstructorInfo>(typeof(DefaultContractResolver.EnumerableDictionaryWrapper<, >).MakeGenericType(new Type[] { type2, type3 }).GetConstructors());
				ObjectConstructor<object> createEnumerableWrapper = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructorInfo);
				ExtensionDataGetter extensionDataGetter = delegate(object o)
				{
					object obj2 = getExtensionDataDictionary.Invoke(o);
					if (obj2 == null)
					{
						return null;
					}
					return (IEnumerable<KeyValuePair<object, object>>)createEnumerableWrapper(new object[] { obj2 });
				};
				contract.ExtensionDataGetter = extensionDataGetter;
			}
			contract.ExtensionDataValueType = type3;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00019F1C File Offset: 0x0001811C
		private ConstructorInfo GetAttributeConstructor(Type objectType)
		{
			IEnumerator<ConstructorInfo> enumerator = Enumerable.Where<ConstructorInfo>(objectType.GetConstructors(52), (ConstructorInfo c) => c.IsDefined(typeof(JsonConstructorAttribute), true)).GetEnumerator();
			if (enumerator.MoveNext())
			{
				ConstructorInfo constructorInfo = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new JsonException("Multiple constructors with the JsonConstructorAttribute.");
				}
				return constructorInfo;
			}
			else
			{
				if (objectType == typeof(Version))
				{
					return objectType.GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int)
					});
				}
				return null;
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00019FD4 File Offset: 0x000181D4
		private ConstructorInfo GetImmutableConstructor(Type objectType, JsonPropertyCollection memberProperties)
		{
			IEnumerator<ConstructorInfo> enumerator = objectType.GetConstructors().GetEnumerator();
			if (enumerator.MoveNext())
			{
				ConstructorInfo constructorInfo = enumerator.Current;
				if (!enumerator.MoveNext())
				{
					ParameterInfo[] parameters = constructorInfo.GetParameters();
					if (parameters.Length != 0)
					{
						foreach (ParameterInfo parameterInfo in parameters)
						{
							JsonProperty jsonProperty = this.MatchProperty(memberProperties, parameterInfo.Name, parameterInfo.ParameterType);
							if (jsonProperty == null || jsonProperty.Writable)
							{
								return null;
							}
						}
						return constructorInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001A054 File Offset: 0x00018254
		private ConstructorInfo GetParameterizedConstructor(Type objectType)
		{
			ConstructorInfo[] constructors = objectType.GetConstructors(20);
			if (constructors.Length == 1)
			{
				return constructors[0];
			}
			return null;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001A078 File Offset: 0x00018278
		protected virtual IList<JsonProperty> CreateConstructorParameters(ConstructorInfo constructor, JsonPropertyCollection memberProperties)
		{
			ParameterInfo[] parameters = constructor.GetParameters();
			JsonPropertyCollection jsonPropertyCollection = new JsonPropertyCollection(constructor.DeclaringType);
			foreach (ParameterInfo parameterInfo in parameters)
			{
				JsonProperty jsonProperty = this.MatchProperty(memberProperties, parameterInfo.Name, parameterInfo.ParameterType);
				if (jsonProperty != null || parameterInfo.Name != null)
				{
					JsonProperty jsonProperty2 = this.CreatePropertyFromConstructorParameter(jsonProperty, parameterInfo);
					if (jsonProperty2 != null)
					{
						jsonPropertyCollection.AddProperty(jsonProperty2);
					}
				}
			}
			return jsonPropertyCollection;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001A0E4 File Offset: 0x000182E4
		private JsonProperty MatchProperty(JsonPropertyCollection properties, string name, Type type)
		{
			if (name == null)
			{
				return null;
			}
			JsonProperty closestMatchProperty = properties.GetClosestMatchProperty(name);
			if (closestMatchProperty == null || closestMatchProperty.PropertyType != type)
			{
				return null;
			}
			return closestMatchProperty;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001A114 File Offset: 0x00018314
		protected virtual JsonProperty CreatePropertyFromConstructorParameter(JsonProperty matchingMemberProperty, ParameterInfo parameterInfo)
		{
			JsonProperty jsonProperty = new JsonProperty();
			jsonProperty.PropertyType = parameterInfo.ParameterType;
			jsonProperty.AttributeProvider = new ReflectionAttributeProvider(parameterInfo);
			bool flag;
			this.SetPropertySettingsFromAttributes(jsonProperty, parameterInfo, parameterInfo.Name, parameterInfo.Member.DeclaringType, MemberSerialization.OptOut, out flag);
			jsonProperty.Readable = false;
			jsonProperty.Writable = true;
			if (matchingMemberProperty != null)
			{
				jsonProperty.PropertyName = ((jsonProperty.PropertyName != parameterInfo.Name) ? jsonProperty.PropertyName : matchingMemberProperty.PropertyName);
				jsonProperty.Converter = jsonProperty.Converter ?? matchingMemberProperty.Converter;
				jsonProperty.MemberConverter = jsonProperty.MemberConverter ?? matchingMemberProperty.MemberConverter;
				if (!jsonProperty._hasExplicitDefaultValue && matchingMemberProperty._hasExplicitDefaultValue)
				{
					jsonProperty.DefaultValue = matchingMemberProperty.DefaultValue;
				}
				JsonProperty jsonProperty2 = jsonProperty;
				Required? required = jsonProperty._required;
				jsonProperty2._required = ((required != null) ? required : matchingMemberProperty._required);
				JsonProperty jsonProperty3 = jsonProperty;
				bool? isReference = jsonProperty.IsReference;
				jsonProperty3.IsReference = ((isReference != null) ? isReference : matchingMemberProperty.IsReference);
				JsonProperty jsonProperty4 = jsonProperty;
				NullValueHandling? nullValueHandling = jsonProperty.NullValueHandling;
				jsonProperty4.NullValueHandling = ((nullValueHandling != null) ? nullValueHandling : matchingMemberProperty.NullValueHandling);
				JsonProperty jsonProperty5 = jsonProperty;
				DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling;
				jsonProperty5.DefaultValueHandling = ((defaultValueHandling != null) ? defaultValueHandling : matchingMemberProperty.DefaultValueHandling);
				JsonProperty jsonProperty6 = jsonProperty;
				ReferenceLoopHandling? referenceLoopHandling = jsonProperty.ReferenceLoopHandling;
				jsonProperty6.ReferenceLoopHandling = ((referenceLoopHandling != null) ? referenceLoopHandling : matchingMemberProperty.ReferenceLoopHandling);
				JsonProperty jsonProperty7 = jsonProperty;
				ObjectCreationHandling? objectCreationHandling = jsonProperty.ObjectCreationHandling;
				jsonProperty7.ObjectCreationHandling = ((objectCreationHandling != null) ? objectCreationHandling : matchingMemberProperty.ObjectCreationHandling);
				JsonProperty jsonProperty8 = jsonProperty;
				TypeNameHandling? typeNameHandling = jsonProperty.TypeNameHandling;
				jsonProperty8.TypeNameHandling = ((typeNameHandling != null) ? typeNameHandling : matchingMemberProperty.TypeNameHandling);
			}
			return jsonProperty;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001A2C2 File Offset: 0x000184C2
		protected virtual JsonConverter ResolveContractConverter(Type objectType)
		{
			return JsonTypeReflector.GetJsonConverter(objectType);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001A2CA File Offset: 0x000184CA
		private Func<object> GetDefaultCreator(Type createdType)
		{
			return JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(createdType);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001A2D8 File Offset: 0x000184D8
		private void InitializeContract(JsonContract contract)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(contract.NonNullableUnderlyingType);
			if (cachedAttribute != null)
			{
				contract.IsReference = cachedAttribute._isReference;
			}
			else
			{
				DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(contract.NonNullableUnderlyingType);
				if (dataContractAttribute != null && dataContractAttribute.IsReference)
				{
					contract.IsReference = new bool?(true);
				}
			}
			contract.Converter = this.ResolveContractConverter(contract.NonNullableUnderlyingType);
			contract.InternalConverter = JsonSerializer.GetMatchingConverter(DefaultContractResolver.BuiltInConverters, contract.NonNullableUnderlyingType);
			if (contract.IsInstantiable && (ReflectionUtils.HasDefaultConstructor(contract.CreatedType, true) || contract.CreatedType.IsValueType()))
			{
				contract.DefaultCreator = this.GetDefaultCreator(contract.CreatedType);
				contract.DefaultCreatorNonPublic = !contract.CreatedType.IsValueType() && ReflectionUtils.GetDefaultConstructor(contract.CreatedType) == null;
			}
			this.ResolveCallbackMethods(contract, contract.NonNullableUnderlyingType);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001A3B8 File Offset: 0x000185B8
		private void ResolveCallbackMethods(JsonContract contract, Type t)
		{
			List<SerializationCallback> list;
			List<SerializationCallback> list2;
			List<SerializationCallback> list3;
			List<SerializationCallback> list4;
			List<SerializationErrorCallback> list5;
			this.GetCallbackMethodsForType(t, out list, out list2, out list3, out list4, out list5);
			if (list != null)
			{
				contract.OnSerializingCallbacks.AddRange(list);
			}
			if (list2 != null)
			{
				contract.OnSerializedCallbacks.AddRange(list2);
			}
			if (list3 != null)
			{
				contract.OnDeserializingCallbacks.AddRange(list3);
			}
			if (list4 != null)
			{
				contract.OnDeserializedCallbacks.AddRange(list4);
			}
			if (list5 != null)
			{
				contract.OnErrorCallbacks.AddRange(list5);
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001A424 File Offset: 0x00018624
		private void GetCallbackMethodsForType(Type type, out List<SerializationCallback> onSerializing, out List<SerializationCallback> onSerialized, out List<SerializationCallback> onDeserializing, out List<SerializationCallback> onDeserialized, out List<SerializationErrorCallback> onError)
		{
			onSerializing = null;
			onSerialized = null;
			onDeserializing = null;
			onDeserialized = null;
			onError = null;
			foreach (Type type2 in this.GetClassHierarchyForType(type))
			{
				MethodInfo methodInfo = null;
				MethodInfo methodInfo2 = null;
				MethodInfo methodInfo3 = null;
				MethodInfo methodInfo4 = null;
				MethodInfo methodInfo5 = null;
				bool flag = DefaultContractResolver.ShouldSkipSerializing(type2);
				bool flag2 = DefaultContractResolver.ShouldSkipDeserialized(type2);
				foreach (MethodInfo methodInfo6 in type2.GetMethods(54))
				{
					if (!methodInfo6.ContainsGenericParameters)
					{
						Type type3 = null;
						ParameterInfo[] parameters = methodInfo6.GetParameters();
						if (!flag && DefaultContractResolver.IsValidCallback(methodInfo6, parameters, typeof(OnSerializingAttribute), methodInfo, ref type3))
						{
							onSerializing = onSerializing ?? new List<SerializationCallback>();
							onSerializing.Add(JsonContract.CreateSerializationCallback(methodInfo6));
							methodInfo = methodInfo6;
						}
						if (DefaultContractResolver.IsValidCallback(methodInfo6, parameters, typeof(OnSerializedAttribute), methodInfo2, ref type3))
						{
							onSerialized = onSerialized ?? new List<SerializationCallback>();
							onSerialized.Add(JsonContract.CreateSerializationCallback(methodInfo6));
							methodInfo2 = methodInfo6;
						}
						if (DefaultContractResolver.IsValidCallback(methodInfo6, parameters, typeof(OnDeserializingAttribute), methodInfo3, ref type3))
						{
							onDeserializing = onDeserializing ?? new List<SerializationCallback>();
							onDeserializing.Add(JsonContract.CreateSerializationCallback(methodInfo6));
							methodInfo3 = methodInfo6;
						}
						if (!flag2 && DefaultContractResolver.IsValidCallback(methodInfo6, parameters, typeof(OnDeserializedAttribute), methodInfo4, ref type3))
						{
							onDeserialized = onDeserialized ?? new List<SerializationCallback>();
							onDeserialized.Add(JsonContract.CreateSerializationCallback(methodInfo6));
							methodInfo4 = methodInfo6;
						}
						if (DefaultContractResolver.IsValidCallback(methodInfo6, parameters, typeof(OnErrorAttribute), methodInfo5, ref type3))
						{
							onError = onError ?? new List<SerializationErrorCallback>();
							onError.Add(JsonContract.CreateSerializationErrorCallback(methodInfo6));
							methodInfo5 = methodInfo6;
						}
					}
				}
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001A614 File Offset: 0x00018814
		private static bool ShouldSkipDeserialized(Type t)
		{
			return (t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(ConcurrentDictionary)) || (t.Name == "FSharpSet`1" || t.Name == "FSharpMap`2");
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001A669 File Offset: 0x00018869
		private static bool ShouldSkipSerializing(Type t)
		{
			return t.Name == "FSharpSet`1" || t.Name == "FSharpMap`2";
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001A694 File Offset: 0x00018894
		private List<Type> GetClassHierarchyForType(Type type)
		{
			List<Type> list = new List<Type>();
			Type type2 = type;
			while (type2 != null && type2 != typeof(object))
			{
				list.Add(type2);
				type2 = type2.BaseType();
			}
			list.Reverse();
			return list;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001A6DC File Offset: 0x000188DC
		protected virtual JsonDictionaryContract CreateDictionaryContract(Type objectType)
		{
			JsonDictionaryContract jsonDictionaryContract = new JsonDictionaryContract(objectType);
			this.InitializeContract(jsonDictionaryContract);
			JsonContainerAttribute attribute = JsonTypeReflector.GetAttribute<JsonContainerAttribute>(objectType);
			if (((attribute != null) ? attribute.NamingStrategyType : null) != null)
			{
				NamingStrategy namingStrategy = JsonTypeReflector.GetContainerNamingStrategy(attribute);
				jsonDictionaryContract.DictionaryKeyResolver = (string s) => namingStrategy.GetDictionaryKey(s);
			}
			else
			{
				jsonDictionaryContract.DictionaryKeyResolver = new Func<string, string>(this.ResolveDictionaryKey);
			}
			ConstructorInfo attributeConstructor = this.GetAttributeConstructor(jsonDictionaryContract.NonNullableUnderlyingType);
			if (attributeConstructor != null)
			{
				ParameterInfo[] parameters = attributeConstructor.GetParameters();
				Type type = ((jsonDictionaryContract.DictionaryKeyType != null && jsonDictionaryContract.DictionaryValueType != null) ? typeof(IEnumerable).MakeGenericType(new Type[] { typeof(KeyValuePair).MakeGenericType(new Type[] { jsonDictionaryContract.DictionaryKeyType, jsonDictionaryContract.DictionaryValueType }) }) : typeof(IDictionary));
				if (parameters.Length == 0)
				{
					jsonDictionaryContract.HasParameterizedCreator = false;
				}
				else
				{
					if (parameters.Length != 1 || !type.IsAssignableFrom(parameters[0].ParameterType))
					{
						throw new JsonException("Constructor for '{0}' must have no parameters or a single parameter that implements '{1}'.".FormatWith(CultureInfo.InvariantCulture, jsonDictionaryContract.UnderlyingType, type));
					}
					jsonDictionaryContract.HasParameterizedCreator = true;
				}
				jsonDictionaryContract.OverrideCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(attributeConstructor);
			}
			return jsonDictionaryContract;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001A834 File Offset: 0x00018A34
		protected virtual JsonArrayContract CreateArrayContract(Type objectType)
		{
			JsonArrayContract jsonArrayContract = new JsonArrayContract(objectType);
			this.InitializeContract(jsonArrayContract);
			ConstructorInfo attributeConstructor = this.GetAttributeConstructor(jsonArrayContract.NonNullableUnderlyingType);
			if (attributeConstructor != null)
			{
				ParameterInfo[] parameters = attributeConstructor.GetParameters();
				Type type = ((jsonArrayContract.CollectionItemType != null) ? typeof(IEnumerable).MakeGenericType(new Type[] { jsonArrayContract.CollectionItemType }) : typeof(IEnumerable));
				if (parameters.Length == 0)
				{
					jsonArrayContract.HasParameterizedCreator = false;
				}
				else
				{
					if (parameters.Length != 1 || !type.IsAssignableFrom(parameters[0].ParameterType))
					{
						throw new JsonException("Constructor for '{0}' must have no parameters or a single parameter that implements '{1}'.".FormatWith(CultureInfo.InvariantCulture, jsonArrayContract.UnderlyingType, type));
					}
					jsonArrayContract.HasParameterizedCreator = true;
				}
				jsonArrayContract.OverrideCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(attributeConstructor);
			}
			return jsonArrayContract;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001A904 File Offset: 0x00018B04
		protected virtual JsonPrimitiveContract CreatePrimitiveContract(Type objectType)
		{
			JsonPrimitiveContract jsonPrimitiveContract = new JsonPrimitiveContract(objectType);
			this.InitializeContract(jsonPrimitiveContract);
			return jsonPrimitiveContract;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001A920 File Offset: 0x00018B20
		protected virtual JsonLinqContract CreateLinqContract(Type objectType)
		{
			JsonLinqContract jsonLinqContract = new JsonLinqContract(objectType);
			this.InitializeContract(jsonLinqContract);
			return jsonLinqContract;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001A93C File Offset: 0x00018B3C
		protected virtual JsonISerializableContract CreateISerializableContract(Type objectType)
		{
			JsonISerializableContract jsonISerializableContract = new JsonISerializableContract(objectType);
			this.InitializeContract(jsonISerializableContract);
			ConstructorInfo constructor = jsonISerializableContract.NonNullableUnderlyingType.GetConstructor(52, null, new Type[]
			{
				typeof(SerializationInfo),
				typeof(StreamingContext)
			}, null);
			if (constructor != null)
			{
				ObjectConstructor<object> objectConstructor = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
				jsonISerializableContract.ISerializableCreator = objectConstructor;
			}
			return jsonISerializableContract;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		protected virtual JsonDynamicContract CreateDynamicContract(Type objectType)
		{
			JsonDynamicContract jsonDynamicContract = new JsonDynamicContract(objectType);
			this.InitializeContract(jsonDynamicContract);
			JsonContainerAttribute attribute = JsonTypeReflector.GetAttribute<JsonContainerAttribute>(objectType);
			if (((attribute != null) ? attribute.NamingStrategyType : null) != null)
			{
				NamingStrategy namingStrategy = JsonTypeReflector.GetContainerNamingStrategy(attribute);
				jsonDynamicContract.PropertyNameResolver = (string s) => namingStrategy.GetDictionaryKey(s);
			}
			else
			{
				jsonDynamicContract.PropertyNameResolver = new Func<string, string>(this.ResolveDictionaryKey);
			}
			jsonDynamicContract.Properties.AddRange(this.CreateProperties(objectType, MemberSerialization.OptOut));
			return jsonDynamicContract;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001AA28 File Offset: 0x00018C28
		protected virtual JsonStringContract CreateStringContract(Type objectType)
		{
			JsonStringContract jsonStringContract = new JsonStringContract(objectType);
			this.InitializeContract(jsonStringContract);
			return jsonStringContract;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001AA44 File Offset: 0x00018C44
		protected virtual JsonContract CreateContract(Type objectType)
		{
			if (DefaultContractResolver.IsJsonPrimitiveType(objectType))
			{
				return this.CreatePrimitiveContract(objectType);
			}
			Type type = ReflectionUtils.EnsureNotNullableType(objectType);
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (cachedAttribute is JsonObjectAttribute)
			{
				return this.CreateObjectContract(objectType);
			}
			if (cachedAttribute is JsonArrayAttribute)
			{
				return this.CreateArrayContract(objectType);
			}
			if (cachedAttribute is JsonDictionaryAttribute)
			{
				return this.CreateDictionaryContract(objectType);
			}
			if (type == typeof(JToken) || type.IsSubclassOf(typeof(JToken)))
			{
				return this.CreateLinqContract(objectType);
			}
			if (CollectionUtils.IsDictionaryType(type))
			{
				return this.CreateDictionaryContract(objectType);
			}
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				return this.CreateArrayContract(objectType);
			}
			if (DefaultContractResolver.CanConvertToString(type))
			{
				return this.CreateStringContract(objectType);
			}
			if (!this.IgnoreSerializableInterface && typeof(ISerializable).IsAssignableFrom(type))
			{
				return this.CreateISerializableContract(objectType);
			}
			if (typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type))
			{
				return this.CreateDynamicContract(objectType);
			}
			if (DefaultContractResolver.IsIConvertible(type))
			{
				return this.CreatePrimitiveContract(type);
			}
			return this.CreateObjectContract(objectType);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001AB58 File Offset: 0x00018D58
		internal static bool IsJsonPrimitiveType(Type t)
		{
			PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(t);
			return typeCode != PrimitiveTypeCode.Empty && typeCode != PrimitiveTypeCode.Object;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001AB78 File Offset: 0x00018D78
		internal static bool IsIConvertible(Type t)
		{
			return (typeof(IConvertible).IsAssignableFrom(t) || (ReflectionUtils.IsNullableType(t) && typeof(IConvertible).IsAssignableFrom(Nullable.GetUnderlyingType(t)))) && !typeof(JToken).IsAssignableFrom(t);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001ABCC File Offset: 0x00018DCC
		internal static bool CanConvertToString(Type type)
		{
			TypeConverter typeConverter;
			return JsonTypeReflector.CanTypeDescriptorConvertString(type, out typeConverter) || (type == typeof(Type) || type.IsSubclassOf(typeof(Type)));
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001AC0C File Offset: 0x00018E0C
		private static bool IsValidCallback(MethodInfo method, ParameterInfo[] parameters, Type attributeType, MethodInfo currentCallback, ref Type prevAttributeType)
		{
			if (!method.IsDefined(attributeType, false))
			{
				return false;
			}
			if (currentCallback != null)
			{
				throw new JsonException("Invalid attribute. Both '{0}' and '{1}' in type '{2}' have '{3}'.".FormatWith(CultureInfo.InvariantCulture, method, currentCallback, DefaultContractResolver.GetClrTypeFullName(method.DeclaringType), attributeType));
			}
			if (prevAttributeType != null)
			{
				throw new JsonException("Invalid Callback. Method '{3}' in type '{2}' has both '{0}' and '{1}'.".FormatWith(CultureInfo.InvariantCulture, prevAttributeType, attributeType, DefaultContractResolver.GetClrTypeFullName(method.DeclaringType), method));
			}
			if (method.IsVirtual)
			{
				throw new JsonException("Virtual Method '{0}' of type '{1}' cannot be marked with '{2}' attribute.".FormatWith(CultureInfo.InvariantCulture, method, DefaultContractResolver.GetClrTypeFullName(method.DeclaringType), attributeType));
			}
			if (method.ReturnType != typeof(void))
			{
				throw new JsonException("Serialization Callback '{1}' in type '{0}' must return void.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(method.DeclaringType), method));
			}
			if (attributeType == typeof(OnErrorAttribute))
			{
				if (parameters == null || parameters.Length != 2 || parameters[0].ParameterType != typeof(StreamingContext) || parameters[1].ParameterType != typeof(ErrorContext))
				{
					throw new JsonException("Serialization Error Callback '{1}' in type '{0}' must have two parameters of type '{2}' and '{3}'.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(method.DeclaringType), method, typeof(StreamingContext), typeof(ErrorContext)));
				}
			}
			else if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != typeof(StreamingContext))
			{
				throw new JsonException("Serialization Callback '{1}' in type '{0}' must have a single parameter of type '{2}'.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(method.DeclaringType), method, typeof(StreamingContext)));
			}
			prevAttributeType = attributeType;
			return true;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001ADBA File Offset: 0x00018FBA
		internal static string GetClrTypeFullName(Type type)
		{
			if (type.IsGenericTypeDefinition() || !type.ContainsGenericParameters())
			{
				return type.FullName;
			}
			return "{0}.{1}".FormatWith(CultureInfo.InvariantCulture, type.Namespace, type.Name);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		protected virtual IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			List<MemberInfo> serializableMembers = this.GetSerializableMembers(type);
			if (serializableMembers == null)
			{
				throw new JsonSerializationException("Null collection of serializable members returned.");
			}
			PropertyNameTable nameTable = this.GetNameTable();
			JsonPropertyCollection jsonPropertyCollection = new JsonPropertyCollection(type);
			foreach (MemberInfo memberInfo in serializableMembers)
			{
				JsonProperty jsonProperty = this.CreateProperty(memberInfo, memberSerialization);
				if (jsonProperty != null)
				{
					PropertyNameTable propertyNameTable = nameTable;
					lock (propertyNameTable)
					{
						jsonProperty.PropertyName = nameTable.Add(jsonProperty.PropertyName);
					}
					jsonPropertyCollection.AddProperty(jsonProperty);
				}
			}
			return Enumerable.ToList<JsonProperty>(Enumerable.OrderBy<JsonProperty, int>(jsonPropertyCollection, delegate(JsonProperty p)
			{
				int? order = p.Order;
				if (order == null)
				{
					return -1;
				}
				return order.GetValueOrDefault();
			}));
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001AED8 File Offset: 0x000190D8
		internal virtual PropertyNameTable GetNameTable()
		{
			return this._nameTable;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001AEE0 File Offset: 0x000190E0
		protected virtual IValueProvider CreateMemberValueProvider(MemberInfo member)
		{
			IValueProvider valueProvider;
			if (this.DynamicCodeGeneration)
			{
				valueProvider = new DynamicValueProvider(member);
			}
			else
			{
				valueProvider = new ReflectionValueProvider(member);
			}
			return valueProvider;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001AF08 File Offset: 0x00019108
		protected virtual JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty jsonProperty = new JsonProperty();
			jsonProperty.PropertyType = ReflectionUtils.GetMemberUnderlyingType(member);
			jsonProperty.DeclaringType = member.DeclaringType;
			jsonProperty.ValueProvider = this.CreateMemberValueProvider(member);
			jsonProperty.AttributeProvider = new ReflectionAttributeProvider(member);
			bool flag;
			this.SetPropertySettingsFromAttributes(jsonProperty, member, member.Name, member.DeclaringType, memberSerialization, out flag);
			if (memberSerialization != MemberSerialization.Fields)
			{
				jsonProperty.Readable = ReflectionUtils.CanReadMemberValue(member, flag);
				jsonProperty.Writable = ReflectionUtils.CanSetMemberValue(member, flag, jsonProperty.HasMemberAttribute);
			}
			else
			{
				jsonProperty.Readable = true;
				jsonProperty.Writable = true;
			}
			jsonProperty.ShouldSerialize = this.CreateShouldSerializeTest(member);
			this.SetIsSpecifiedActions(jsonProperty, member, flag);
			return jsonProperty;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001AFB0 File Offset: 0x000191B0
		private void SetPropertySettingsFromAttributes(JsonProperty property, object attributeProvider, string name, Type declaringType, MemberSerialization memberSerialization, out bool allowNonPublicAccess)
		{
			bool dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(declaringType) != null;
			MemberInfo memberInfo = attributeProvider as MemberInfo;
			DataMemberAttribute dataMemberAttribute;
			if (dataContractAttribute && memberInfo != null)
			{
				dataMemberAttribute = JsonTypeReflector.GetDataMemberAttribute(memberInfo);
			}
			else
			{
				dataMemberAttribute = null;
			}
			JsonPropertyAttribute attribute = JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(attributeProvider);
			bool attribute2 = JsonTypeReflector.GetAttribute<JsonRequiredAttribute>(attributeProvider) != null;
			string text;
			bool flag;
			if (attribute != null && attribute.PropertyName != null)
			{
				text = attribute.PropertyName;
				flag = true;
			}
			else if (dataMemberAttribute != null && dataMemberAttribute.Name != null)
			{
				text = dataMemberAttribute.Name;
				flag = true;
			}
			else
			{
				text = name;
				flag = false;
			}
			JsonContainerAttribute attribute3 = JsonTypeReflector.GetAttribute<JsonContainerAttribute>(declaringType);
			NamingStrategy namingStrategy;
			if (((attribute != null) ? attribute.NamingStrategyType : null) != null)
			{
				namingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(attribute.NamingStrategyType, attribute.NamingStrategyParameters);
			}
			else if (((attribute3 != null) ? attribute3.NamingStrategyType : null) != null)
			{
				namingStrategy = JsonTypeReflector.GetContainerNamingStrategy(attribute3);
			}
			else
			{
				namingStrategy = this.NamingStrategy;
			}
			if (namingStrategy != null)
			{
				property.PropertyName = namingStrategy.GetPropertyName(text, flag);
			}
			else
			{
				property.PropertyName = this.ResolvePropertyName(text);
			}
			property.UnderlyingName = name;
			bool flag2 = false;
			if (attribute != null)
			{
				property._required = attribute._required;
				property.Order = attribute._order;
				property.DefaultValueHandling = attribute._defaultValueHandling;
				flag2 = true;
				property.NullValueHandling = attribute._nullValueHandling;
				property.ReferenceLoopHandling = attribute._referenceLoopHandling;
				property.ObjectCreationHandling = attribute._objectCreationHandling;
				property.TypeNameHandling = attribute._typeNameHandling;
				property.IsReference = attribute._isReference;
				property.ItemIsReference = attribute._itemIsReference;
				property.ItemConverter = ((attribute.ItemConverterType != null) ? JsonTypeReflector.CreateJsonConverterInstance(attribute.ItemConverterType, attribute.ItemConverterParameters) : null);
				property.ItemReferenceLoopHandling = attribute._itemReferenceLoopHandling;
				property.ItemTypeNameHandling = attribute._itemTypeNameHandling;
			}
			else
			{
				property.NullValueHandling = default(NullValueHandling?);
				property.ReferenceLoopHandling = default(ReferenceLoopHandling?);
				property.ObjectCreationHandling = default(ObjectCreationHandling?);
				property.TypeNameHandling = default(TypeNameHandling?);
				property.IsReference = default(bool?);
				property.ItemIsReference = default(bool?);
				property.ItemConverter = null;
				property.ItemReferenceLoopHandling = default(ReferenceLoopHandling?);
				property.ItemTypeNameHandling = default(TypeNameHandling?);
				if (dataMemberAttribute != null)
				{
					property._required = new Required?(dataMemberAttribute.IsRequired ? Required.AllowNull : Required.Default);
					property.Order = ((dataMemberAttribute.Order != -1) ? new int?(dataMemberAttribute.Order) : default(int?));
					property.DefaultValueHandling = ((!dataMemberAttribute.EmitDefaultValue) ? new DefaultValueHandling?(DefaultValueHandling.Ignore) : default(DefaultValueHandling?));
					flag2 = true;
				}
			}
			if (attribute2)
			{
				property._required = new Required?(Required.Always);
				flag2 = true;
			}
			property.HasMemberAttribute = flag2;
			bool flag3 = JsonTypeReflector.GetAttribute<JsonIgnoreAttribute>(attributeProvider) != null || JsonTypeReflector.GetAttribute<JsonExtensionDataAttribute>(attributeProvider) != null || JsonTypeReflector.IsNonSerializable(attributeProvider);
			if (memberSerialization != MemberSerialization.OptIn)
			{
				bool flag4 = JsonTypeReflector.GetAttribute<IgnoreDataMemberAttribute>(attributeProvider) != null;
				property.Ignored = flag3 || flag4;
			}
			else
			{
				property.Ignored = flag3 || !flag2;
			}
			property.Converter = JsonTypeReflector.GetJsonConverter(attributeProvider);
			property.MemberConverter = JsonTypeReflector.GetJsonConverter(attributeProvider);
			DefaultValueAttribute attribute4 = JsonTypeReflector.GetAttribute<DefaultValueAttribute>(attributeProvider);
			if (attribute4 != null)
			{
				property.DefaultValue = attribute4.Value;
			}
			allowNonPublicAccess = false;
			if ((this.DefaultMembersSearchFlags & 32) == 32)
			{
				allowNonPublicAccess = true;
			}
			if (flag2)
			{
				allowNonPublicAccess = true;
			}
			if (memberSerialization == MemberSerialization.Fields)
			{
				allowNonPublicAccess = true;
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001B30C File Offset: 0x0001950C
		private Predicate<object> CreateShouldSerializeTest(MemberInfo member)
		{
			MethodInfo method = member.DeclaringType.GetMethod("ShouldSerialize" + member.Name, ReflectionUtils.EmptyTypes);
			if (method == null || method.ReturnType != typeof(bool))
			{
				return null;
			}
			MethodCall<object, object> shouldSerializeCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object o) => (bool)shouldSerializeCall(o, new object[0]);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001B380 File Offset: 0x00019580
		private void SetIsSpecifiedActions(JsonProperty property, MemberInfo member, bool allowNonPublicAccess)
		{
			MemberInfo memberInfo = member.DeclaringType.GetProperty(member.Name + "Specified");
			if (memberInfo == null)
			{
				memberInfo = member.DeclaringType.GetField(member.Name + "Specified");
			}
			if (memberInfo == null || ReflectionUtils.GetMemberUnderlyingType(memberInfo) != typeof(bool))
			{
				return;
			}
			Func<object, object> specifiedPropertyGet = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(memberInfo);
			property.GetIsSpecified = (object o) => (bool)specifiedPropertyGet.Invoke(o);
			if (ReflectionUtils.CanSetMemberValue(memberInfo, allowNonPublicAccess, false))
			{
				property.SetIsSpecified = JsonTypeReflector.ReflectionDelegateFactory.CreateSet<object>(memberInfo);
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001B433 File Offset: 0x00019633
		protected virtual string ResolvePropertyName(string propertyName)
		{
			if (this.NamingStrategy != null)
			{
				return this.NamingStrategy.GetPropertyName(propertyName, false);
			}
			return propertyName;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001B44C File Offset: 0x0001964C
		protected virtual string ResolveExtensionDataName(string extensionDataName)
		{
			if (this.NamingStrategy != null)
			{
				return this.NamingStrategy.GetExtensionDataName(extensionDataName);
			}
			return extensionDataName;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001B464 File Offset: 0x00019664
		protected virtual string ResolveDictionaryKey(string dictionaryKey)
		{
			if (this.NamingStrategy != null)
			{
				return this.NamingStrategy.GetDictionaryKey(dictionaryKey);
			}
			return this.ResolvePropertyName(dictionaryKey);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001B482 File Offset: 0x00019682
		public string GetResolvedPropertyName(string propertyName)
		{
			return this.ResolvePropertyName(propertyName);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001B48C File Offset: 0x0001968C
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultContractResolver()
		{
		}

		// Token: 0x04000295 RID: 661
		private static readonly IContractResolver _instance = new DefaultContractResolver();

		// Token: 0x04000296 RID: 662
		private static readonly JsonConverter[] BuiltInConverters = new JsonConverter[]
		{
			new EntityKeyMemberConverter(),
			new ExpandoObjectConverter(),
			new XmlNodeConverter(),
			new BinaryConverter(),
			new DataSetConverter(),
			new DataTableConverter(),
			new DiscriminatedUnionConverter(),
			new KeyValuePairConverter(),
			new BsonObjectIdConverter(),
			new RegexConverter()
		};

		// Token: 0x04000297 RID: 663
		private readonly object _typeContractCacheLock = new object();

		// Token: 0x04000298 RID: 664
		private readonly PropertyNameTable _nameTable = new PropertyNameTable();

		// Token: 0x04000299 RID: 665
		private Dictionary<Type, JsonContract> _contractCache;

		// Token: 0x0400029A RID: 666
		[CompilerGenerated]
		private BindingFlags <DefaultMembersSearchFlags>k__BackingField;

		// Token: 0x0400029B RID: 667
		[CompilerGenerated]
		private bool <SerializeCompilerGeneratedMembers>k__BackingField;

		// Token: 0x0400029C RID: 668
		[CompilerGenerated]
		private bool <IgnoreSerializableInterface>k__BackingField;

		// Token: 0x0400029D RID: 669
		[CompilerGenerated]
		private bool <IgnoreSerializableAttribute>k__BackingField;

		// Token: 0x0400029E RID: 670
		[CompilerGenerated]
		private NamingStrategy <NamingStrategy>k__BackingField;

		// Token: 0x0200013E RID: 318
		internal class EnumerableDictionaryWrapper<TEnumeratorKey, TEnumeratorValue> : IEnumerable<KeyValuePair<object, object>>, IEnumerable
		{
			// Token: 0x06000CE5 RID: 3301 RVA: 0x0003135B File Offset: 0x0002F55B
			public EnumerableDictionaryWrapper(IEnumerable<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, "e");
				this._e = e;
			}

			// Token: 0x06000CE6 RID: 3302 RVA: 0x00031375 File Offset: 0x0002F575
			public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
			{
				foreach (KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair in this._e)
				{
					yield return new KeyValuePair<object, object>(keyValuePair.Key, keyValuePair.Value);
				}
				IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06000CE7 RID: 3303 RVA: 0x00031384 File Offset: 0x0002F584
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040004AB RID: 1195
			private readonly IEnumerable<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;

			// Token: 0x0200017A RID: 378
			[CompilerGenerated]
			private sealed class <GetEnumerator>d__2 : IEnumerator<KeyValuePair<object, object>>, IDisposable, IEnumerator
			{
				// Token: 0x06000DEE RID: 3566 RVA: 0x000342F2 File Offset: 0x000324F2
				[DebuggerHidden]
				public <GetEnumerator>d__2(int <>1__state)
				{
					this.<>1__state = <>1__state;
				}

				// Token: 0x06000DEF RID: 3567 RVA: 0x00034304 File Offset: 0x00032504
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
					int num = this.<>1__state;
					if (num == -3 || num == 1)
					{
						try
						{
						}
						finally
						{
							this.<>m__Finally1();
						}
					}
				}

				// Token: 0x06000DF0 RID: 3568 RVA: 0x0003433C File Offset: 0x0003253C
				bool IEnumerator.MoveNext()
				{
					bool flag;
					try
					{
						int num = this.<>1__state;
						if (num != 0)
						{
							if (num != 1)
							{
								return false;
							}
							this.<>1__state = -3;
						}
						else
						{
							this.<>1__state = -1;
							enumerator = this._e.GetEnumerator();
							this.<>1__state = -3;
						}
						if (!enumerator.MoveNext())
						{
							this.<>m__Finally1();
							enumerator = null;
							flag = false;
						}
						else
						{
							KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = enumerator.Current;
							this.<>2__current = new KeyValuePair<object, object>(keyValuePair.Key, keyValuePair.Value);
							this.<>1__state = 1;
							flag = true;
						}
					}
					catch
					{
						this.System.IDisposable.Dispose();
						throw;
					}
					return flag;
				}

				// Token: 0x06000DF1 RID: 3569 RVA: 0x00034400 File Offset: 0x00032600
				private void <>m__Finally1()
				{
					this.<>1__state = -1;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}

				// Token: 0x170002B3 RID: 691
				// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003441C File Offset: 0x0003261C
				KeyValuePair<object, object> IEnumerator<KeyValuePair<object, object>>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000DF3 RID: 3571 RVA: 0x00024289 File Offset: 0x00022489
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x170002B4 RID: 692
				// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00034424 File Offset: 0x00032624
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x0400059E RID: 1438
				private int <>1__state;

				// Token: 0x0400059F RID: 1439
				private KeyValuePair<object, object> <>2__current;

				// Token: 0x040005A0 RID: 1440
				public DefaultContractResolver.EnumerableDictionaryWrapper<TEnumeratorKey, TEnumeratorValue> <>4__this;

				// Token: 0x040005A1 RID: 1441
				private IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> <>7__wrap1;
			}
		}

		// Token: 0x0200013F RID: 319
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000CE8 RID: 3304 RVA: 0x0003138C File Offset: 0x0002F58C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000CEA RID: 3306 RVA: 0x00031398 File Offset: 0x0002F598
			internal bool <GetSerializableMembers>b__31_0(MemberInfo m)
			{
				return !ReflectionUtils.IsIndexedProperty(m);
			}

			// Token: 0x06000CEB RID: 3307 RVA: 0x00031398 File Offset: 0x0002F598
			internal bool <GetSerializableMembers>b__31_1(MemberInfo m)
			{
				return !ReflectionUtils.IsIndexedProperty(m);
			}

			// Token: 0x06000CEC RID: 3308 RVA: 0x000313A3 File Offset: 0x0002F5A3
			internal IEnumerable<MemberInfo> <GetExtensionDataMemberForType>b__34_0(Type baseType)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				list.AddRange(baseType.GetProperties(54));
				list.AddRange(baseType.GetFields(54));
				return list;
			}

			// Token: 0x06000CED RID: 3309 RVA: 0x000313C8 File Offset: 0x0002F5C8
			internal bool <GetExtensionDataMemberForType>b__34_1(MemberInfo m)
			{
				MemberTypes memberTypes = m.MemberType();
				if (memberTypes != 16 && memberTypes != 4)
				{
					return false;
				}
				if (!m.IsDefined(typeof(JsonExtensionDataAttribute), false))
				{
					return false;
				}
				if (!ReflectionUtils.CanReadMemberValue(m, true))
				{
					throw new JsonException("Invalid extension data attribute on '{0}'. Member '{1}' must have a getter.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(m.DeclaringType), m.Name));
				}
				Type type;
				if (ReflectionUtils.ImplementsGenericDefinition(ReflectionUtils.GetMemberUnderlyingType(m), typeof(IDictionary), out type))
				{
					Type type2 = type.GetGenericArguments()[0];
					Type type3 = type.GetGenericArguments()[1];
					if (type2.IsAssignableFrom(typeof(string)) && type3.IsAssignableFrom(typeof(JToken)))
					{
						return true;
					}
				}
				throw new JsonException("Invalid extension data attribute on '{0}'. Member '{1}' type must implement IDictionary<string, JToken>.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(m.DeclaringType), m.Name));
			}

			// Token: 0x06000CEE RID: 3310 RVA: 0x0003149F File Offset: 0x0002F69F
			internal bool <GetAttributeConstructor>b__37_0(ConstructorInfo c)
			{
				return c.IsDefined(typeof(JsonConstructorAttribute), true);
			}

			// Token: 0x06000CEF RID: 3311 RVA: 0x000314B4 File Offset: 0x0002F6B4
			internal int <CreateProperties>b__64_0(JsonProperty p)
			{
				int? order = p.Order;
				if (order == null)
				{
					return -1;
				}
				return order.GetValueOrDefault();
			}

			// Token: 0x040004AC RID: 1196
			public static readonly DefaultContractResolver.<>c <>9 = new DefaultContractResolver.<>c();

			// Token: 0x040004AD RID: 1197
			public static Func<MemberInfo, bool> <>9__31_0;

			// Token: 0x040004AE RID: 1198
			public static Func<MemberInfo, bool> <>9__31_1;

			// Token: 0x040004AF RID: 1199
			public static Func<Type, IEnumerable<MemberInfo>> <>9__34_0;

			// Token: 0x040004B0 RID: 1200
			public static Func<MemberInfo, bool> <>9__34_1;

			// Token: 0x040004B1 RID: 1201
			public static Func<ConstructorInfo, bool> <>9__37_0;

			// Token: 0x040004B2 RID: 1202
			public static Func<JsonProperty, int> <>9__64_0;
		}

		// Token: 0x02000140 RID: 320
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0
		{
			// Token: 0x06000CF0 RID: 3312 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x06000CF1 RID: 3313 RVA: 0x000314DA File Offset: 0x0002F6DA
			internal string <CreateObjectContract>b__0(string s)
			{
				return this.namingStrategy.GetDictionaryKey(s);
			}

			// Token: 0x040004B3 RID: 1203
			public NamingStrategy namingStrategy;
		}

		// Token: 0x02000141 RID: 321
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_0
		{
			// Token: 0x06000CF2 RID: 3314 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass35_0()
			{
			}

			// Token: 0x040004B4 RID: 1204
			public Func<object, object> getExtensionDataDictionary;

			// Token: 0x040004B5 RID: 1205
			public MemberInfo member;
		}

		// Token: 0x02000142 RID: 322
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_1
		{
			// Token: 0x06000CF3 RID: 3315 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass35_1()
			{
			}

			// Token: 0x06000CF4 RID: 3316 RVA: 0x000314E8 File Offset: 0x0002F6E8
			internal void <SetExtensionDataDelegates>b__0(object o, string key, object value)
			{
				object obj = this.CS$<>8__locals1.getExtensionDataDictionary.Invoke(o);
				if (obj == null)
				{
					if (this.setExtensionDataDictionary == null)
					{
						throw new JsonSerializationException("Cannot set value onto extension data member '{0}'. The extension data collection is null and it cannot be set.".FormatWith(CultureInfo.InvariantCulture, this.CS$<>8__locals1.member.Name));
					}
					obj = this.createExtensionDataDictionary.Invoke();
					this.setExtensionDataDictionary.Invoke(o, obj);
				}
				this.setExtensionDataDictionaryValue(obj, new object[] { key, value });
			}

			// Token: 0x040004B6 RID: 1206
			public Action<object, object> setExtensionDataDictionary;

			// Token: 0x040004B7 RID: 1207
			public Func<object> createExtensionDataDictionary;

			// Token: 0x040004B8 RID: 1208
			public MethodCall<object, object> setExtensionDataDictionaryValue;

			// Token: 0x040004B9 RID: 1209
			public DefaultContractResolver.<>c__DisplayClass35_0 CS$<>8__locals1;
		}

		// Token: 0x02000143 RID: 323
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_2
		{
			// Token: 0x06000CF5 RID: 3317 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass35_2()
			{
			}

			// Token: 0x06000CF6 RID: 3318 RVA: 0x0003156C File Offset: 0x0002F76C
			internal IEnumerable<KeyValuePair<object, object>> <SetExtensionDataDelegates>b__1(object o)
			{
				object obj = this.CS$<>8__locals2.getExtensionDataDictionary.Invoke(o);
				if (obj == null)
				{
					return null;
				}
				return (IEnumerable<KeyValuePair<object, object>>)this.createEnumerableWrapper(new object[] { obj });
			}

			// Token: 0x040004BA RID: 1210
			public ObjectConstructor<object> createEnumerableWrapper;

			// Token: 0x040004BB RID: 1211
			public DefaultContractResolver.<>c__DisplayClass35_0 CS$<>8__locals2;
		}

		// Token: 0x02000144 RID: 324
		[CompilerGenerated]
		private sealed class <>c__DisplayClass51_0
		{
			// Token: 0x06000CF7 RID: 3319 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass51_0()
			{
			}

			// Token: 0x06000CF8 RID: 3320 RVA: 0x000315AA File Offset: 0x0002F7AA
			internal string <CreateDictionaryContract>b__0(string s)
			{
				return this.namingStrategy.GetDictionaryKey(s);
			}

			// Token: 0x040004BC RID: 1212
			public NamingStrategy namingStrategy;
		}

		// Token: 0x02000145 RID: 325
		[CompilerGenerated]
		private sealed class <>c__DisplayClass56_0
		{
			// Token: 0x06000CF9 RID: 3321 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass56_0()
			{
			}

			// Token: 0x06000CFA RID: 3322 RVA: 0x000315B8 File Offset: 0x0002F7B8
			internal string <CreateDynamicContract>b__0(string s)
			{
				return this.namingStrategy.GetDictionaryKey(s);
			}

			// Token: 0x040004BD RID: 1213
			public NamingStrategy namingStrategy;
		}

		// Token: 0x02000146 RID: 326
		[CompilerGenerated]
		private sealed class <>c__DisplayClass69_0
		{
			// Token: 0x06000CFB RID: 3323 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass69_0()
			{
			}

			// Token: 0x06000CFC RID: 3324 RVA: 0x000315C6 File Offset: 0x0002F7C6
			internal bool <CreateShouldSerializeTest>b__0(object o)
			{
				return (bool)this.shouldSerializeCall(o, new object[0]);
			}

			// Token: 0x040004BE RID: 1214
			public MethodCall<object, object> shouldSerializeCall;
		}

		// Token: 0x02000147 RID: 327
		[CompilerGenerated]
		private sealed class <>c__DisplayClass70_0
		{
			// Token: 0x06000CFD RID: 3325 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass70_0()
			{
			}

			// Token: 0x06000CFE RID: 3326 RVA: 0x000315DF File Offset: 0x0002F7DF
			internal bool <SetIsSpecifiedActions>b__0(object o)
			{
				return (bool)this.specifiedPropertyGet.Invoke(o);
			}

			// Token: 0x040004BF RID: 1215
			public Func<object, object> specifiedPropertyGet;
		}
	}
}
