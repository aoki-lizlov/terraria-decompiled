using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200006F RID: 111
	internal static class ReflectionUtils
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x000168A2 File Offset: 0x00014AA2
		static ReflectionUtils()
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000168B0 File Offset: 0x00014AB0
		public static bool IsVirtual(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
			if (methodInfo != null && methodInfo.IsVirtual)
			{
				return true;
			}
			methodInfo = propertyInfo.GetSetMethod(true);
			return methodInfo != null && methodInfo.IsVirtual;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00016900 File Offset: 0x00014B00
		public static MethodInfo GetBaseDefinition(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			if (getMethod != null)
			{
				return getMethod.GetBaseDefinition();
			}
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			if (setMethod == null)
			{
				return null;
			}
			return setMethod.GetBaseDefinition();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00016942 File Offset: 0x00014B42
		public static bool IsPublic(PropertyInfo property)
		{
			return (property.GetGetMethod() != null && property.GetGetMethod().IsPublic) || (property.GetSetMethod() != null && property.GetSetMethod().IsPublic);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001697F File Offset: 0x00014B7F
		public static Type GetObjectType(object v)
		{
			if (v == null)
			{
				return null;
			}
			return v.GetType();
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001698C File Offset: 0x00014B8C
		public static string GetTypeName(Type t, TypeNameAssemblyFormatHandling assemblyFormat, ISerializationBinder binder)
		{
			string fullyQualifiedTypeName = ReflectionUtils.GetFullyQualifiedTypeName(t, binder);
			if (assemblyFormat == TypeNameAssemblyFormatHandling.Simple)
			{
				return ReflectionUtils.RemoveAssemblyDetails(fullyQualifiedTypeName);
			}
			if (assemblyFormat != TypeNameAssemblyFormatHandling.Full)
			{
				throw new ArgumentOutOfRangeException();
			}
			return fullyQualifiedTypeName;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000169B8 File Offset: 0x00014BB8
		private static string GetFullyQualifiedTypeName(Type t, ISerializationBinder binder)
		{
			if (binder != null)
			{
				string text;
				string text2;
				binder.BindToName(t, out text, out text2);
				return text2 + ((text == null) ? "" : (", " + text));
			}
			return t.AssemblyQualifiedName;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000169F8 File Offset: 0x00014BF8
		private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				char c = fullyQualifiedTypeName.get_Chars(i);
				if (c != ',')
				{
					if (c != '[')
					{
						if (c != ']')
						{
							if (!flag2)
							{
								stringBuilder.Append(c);
							}
						}
						else
						{
							flag = false;
							flag2 = false;
							stringBuilder.Append(c);
						}
					}
					else
					{
						flag = false;
						flag2 = false;
						stringBuilder.Append(c);
					}
				}
				else if (!flag)
				{
					flag = true;
					stringBuilder.Append(c);
				}
				else
				{
					flag2 = true;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00016A81 File Offset: 0x00014C81
		public static bool HasDefaultConstructor(Type t, bool nonPublic)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsValueType() || ReflectionUtils.GetDefaultConstructor(t, nonPublic) != null;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00016AA5 File Offset: 0x00014CA5
		public static ConstructorInfo GetDefaultConstructor(Type t)
		{
			return ReflectionUtils.GetDefaultConstructor(t, false);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00016AB0 File Offset: 0x00014CB0
		public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic)
		{
			BindingFlags bindingFlags = 20;
			if (nonPublic)
			{
				bindingFlags |= 32;
			}
			return Enumerable.SingleOrDefault<ConstructorInfo>(t.GetConstructors(bindingFlags), (ConstructorInfo c) => !Enumerable.Any<ParameterInfo>(c.GetParameters()));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00016AF3 File Offset: 0x00014CF3
		public static bool IsNullable(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return !t.IsValueType() || ReflectionUtils.IsNullableType(t);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00016B10 File Offset: 0x00014D10
		public static bool IsNullableType(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(Nullable);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00016B3C File Offset: 0x00014D3C
		public static Type EnsureNotNullableType(Type t)
		{
			if (!ReflectionUtils.IsNullableType(t))
			{
				return t;
			}
			return Nullable.GetUnderlyingType(t);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00016B4E File Offset: 0x00014D4E
		public static bool IsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == genericInterfaceDefinition;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00016B68 File Offset: 0x00014D68
		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			Type type2;
			return ReflectionUtils.ImplementsGenericDefinition(type, genericInterfaceDefinition, out type2);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00016B80 File Offset: 0x00014D80
		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");
			if (!genericInterfaceDefinition.IsInterface() || !genericInterfaceDefinition.IsGenericTypeDefinition())
			{
				throw new ArgumentNullException("'{0}' is not a generic interface definition.".FormatWith(CultureInfo.InvariantCulture, genericInterfaceDefinition));
			}
			if (type.IsInterface() && type.IsGenericType())
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericInterfaceDefinition == genericTypeDefinition)
				{
					implementingType = type;
					return true;
				}
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType())
				{
					Type genericTypeDefinition2 = type2.GetGenericTypeDefinition();
					if (genericInterfaceDefinition == genericTypeDefinition2)
					{
						implementingType = type2;
						return true;
					}
				}
			}
			implementingType = null;
			return false;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00016C2C File Offset: 0x00014E2C
		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition)
		{
			Type type2;
			return ReflectionUtils.InheritsGenericDefinition(type, genericClassDefinition, out type2);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00016C44 File Offset: 0x00014E44
		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");
			if (!genericClassDefinition.IsClass() || !genericClassDefinition.IsGenericTypeDefinition())
			{
				throw new ArgumentNullException("'{0}' is not a generic class definition.".FormatWith(CultureInfo.InvariantCulture, genericClassDefinition));
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00016C98 File Offset: 0x00014E98
		private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, out Type implementingType)
		{
			if (currentType.IsGenericType())
			{
				Type genericTypeDefinition = currentType.GetGenericTypeDefinition();
				if (genericClassDefinition == genericTypeDefinition)
				{
					implementingType = currentType;
					return true;
				}
			}
			if (currentType.BaseType() == null)
			{
				implementingType = null;
				return false;
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(currentType.BaseType(), genericClassDefinition, out implementingType);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00016CE4 File Offset: 0x00014EE4
		public static Type GetCollectionItemType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(type, typeof(IEnumerable), out type2))
			{
				if (type2.IsGenericTypeDefinition())
				{
					throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
				}
				return type2.GetGenericArguments()[0];
			}
			else
			{
				if (typeof(IEnumerable).IsAssignableFrom(type))
				{
					return null;
				}
				throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00016D70 File Offset: 0x00014F70
		public static void GetDictionaryKeyValueTypes(Type dictionaryType, out Type keyType, out Type valueType)
		{
			ValidationUtils.ArgumentNotNull(dictionaryType, "dictionaryType");
			Type type;
			if (ReflectionUtils.ImplementsGenericDefinition(dictionaryType, typeof(IDictionary), out type))
			{
				if (type.IsGenericTypeDefinition())
				{
					throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
				}
				Type[] genericArguments = type.GetGenericArguments();
				keyType = genericArguments[0];
				valueType = genericArguments[1];
				return;
			}
			else
			{
				if (typeof(IDictionary).IsAssignableFrom(dictionaryType))
				{
					keyType = null;
					valueType = null;
					return;
				}
				throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00016DFC File Offset: 0x00014FFC
		public static Type GetMemberUnderlyingType(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes <= 4)
			{
				if (memberTypes == 2)
				{
					return ((EventInfo)member).EventHandlerType;
				}
				if (memberTypes == 4)
				{
					return ((FieldInfo)member).FieldType;
				}
			}
			else
			{
				if (memberTypes == 8)
				{
					return ((MethodInfo)member).ReturnType;
				}
				if (memberTypes == 16)
				{
					return ((PropertyInfo)member).PropertyType;
				}
			}
			throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo, EventInfo or MethodInfo", "member");
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00016E74 File Offset: 0x00015074
		public static bool IsIndexedProperty(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			PropertyInfo propertyInfo = member as PropertyInfo;
			return propertyInfo != null && ReflectionUtils.IsIndexedProperty(propertyInfo);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00016EA4 File Offset: 0x000150A4
		public static bool IsIndexedProperty(PropertyInfo property)
		{
			ValidationUtils.ArgumentNotNull(property, "property");
			return property.GetIndexParameters().Length != 0;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00016EBC File Offset: 0x000150BC
		public static object GetMemberValue(MemberInfo member, object target)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes != 4)
			{
				if (memberTypes == 16)
				{
					try
					{
						return ((PropertyInfo)member).GetValue(target, null);
					}
					catch (TargetParameterCountException ex)
					{
						throw new ArgumentException("MemberInfo '{0}' has index parameters".FormatWith(CultureInfo.InvariantCulture, member.Name), ex);
					}
				}
				throw new ArgumentException("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, member.Name), "member");
			}
			return ((FieldInfo)member).GetValue(target);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00016F60 File Offset: 0x00015160
		public static void SetMemberValue(MemberInfo member, object target, object value)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == 4)
			{
				((FieldInfo)member).SetValue(target, value);
				return;
			}
			if (memberTypes != 16)
			{
				throw new ArgumentException("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, member.Name), "member");
			}
			((PropertyInfo)member).SetValue(target, value, null);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00016FD4 File Offset: 0x000151D4
		public static bool CanReadMemberValue(MemberInfo member, bool nonPublic)
		{
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == 4)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return nonPublic || fieldInfo.IsPublic;
			}
			if (memberTypes != 16)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanRead && (nonPublic || propertyInfo.GetGetMethod(nonPublic) != null);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00017030 File Offset: 0x00015230
		public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly)
		{
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == 4)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return !fieldInfo.IsLiteral && (!fieldInfo.IsInitOnly || canSetReadOnly) && (nonPublic || fieldInfo.IsPublic);
			}
			if (memberTypes != 16)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanWrite && (nonPublic || propertyInfo.GetSetMethod(nonPublic) != null);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000170A4 File Offset: 0x000152A4
		public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.AddRange(ReflectionUtils.GetFields(type, bindingAttr));
			list.AddRange(ReflectionUtils.GetProperties(type, bindingAttr));
			List<MemberInfo> list2 = new List<MemberInfo>(list.Count);
			foreach (IGrouping<string, MemberInfo> grouping in Enumerable.GroupBy<MemberInfo, string>(list, (MemberInfo m) => m.Name))
			{
				if (Enumerable.Count<MemberInfo>(grouping) == 1)
				{
					list2.Add(Enumerable.First<MemberInfo>(grouping));
				}
				else
				{
					IList<MemberInfo> list3 = new List<MemberInfo>();
					foreach (MemberInfo memberInfo in grouping)
					{
						if (list3.Count == 0)
						{
							list3.Add(memberInfo);
						}
						else if (!ReflectionUtils.IsOverridenGenericMember(memberInfo, bindingAttr) || memberInfo.Name == "Item")
						{
							list3.Add(memberInfo);
						}
					}
					list2.AddRange(list3);
				}
			}
			return list2;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000171CC File Offset: 0x000153CC
		private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr)
		{
			if (memberInfo.MemberType() != 16)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			if (!propertyInfo.IsVirtual())
			{
				return false;
			}
			Type declaringType = propertyInfo.DeclaringType;
			if (!declaringType.IsGenericType())
			{
				return false;
			}
			Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
			if (genericTypeDefinition == null)
			{
				return false;
			}
			MemberInfo[] member = genericTypeDefinition.GetMember(propertyInfo.Name, bindingAttr);
			return member.Length != 0 && ReflectionUtils.GetMemberUnderlyingType(member[0]).IsGenericParameter;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001723F File Offset: 0x0001543F
		public static T GetAttribute<T>(object attributeProvider) where T : Attribute
		{
			return ReflectionUtils.GetAttribute<T>(attributeProvider, true);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00017248 File Offset: 0x00015448
		public static T GetAttribute<T>(object attributeProvider, bool inherit) where T : Attribute
		{
			T[] attributes = ReflectionUtils.GetAttributes<T>(attributeProvider, inherit);
			if (attributes == null)
			{
				return default(T);
			}
			return Enumerable.FirstOrDefault<T>(attributes);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00017270 File Offset: 0x00015470
		public static T[] GetAttributes<T>(object attributeProvider, bool inherit) where T : Attribute
		{
			Attribute[] attributes = ReflectionUtils.GetAttributes(attributeProvider, typeof(T), inherit);
			T[] array = attributes as T[];
			if (array != null)
			{
				return array;
			}
			return Enumerable.ToArray<T>(Enumerable.Cast<T>(attributes));
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000172A8 File Offset: 0x000154A8
		public static Attribute[] GetAttributes(object attributeProvider, Type attributeType, bool inherit)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			Type type = attributeProvider as Type;
			if (type != null)
			{
				return Enumerable.ToArray<Attribute>(Enumerable.Cast<Attribute>((attributeType != null) ? type.GetCustomAttributes(attributeType, inherit) : type.GetCustomAttributes(inherit)));
			}
			Assembly assembly = attributeProvider as Assembly;
			if (assembly != null)
			{
				if (!(attributeType != null))
				{
					return Attribute.GetCustomAttributes(assembly);
				}
				return Attribute.GetCustomAttributes(assembly, attributeType);
			}
			else
			{
				MemberInfo memberInfo = attributeProvider as MemberInfo;
				if (memberInfo != null)
				{
					if (!(attributeType != null))
					{
						return Attribute.GetCustomAttributes(memberInfo, inherit);
					}
					return Attribute.GetCustomAttributes(memberInfo, attributeType, inherit);
				}
				else
				{
					Module module = attributeProvider as Module;
					if (module != null)
					{
						if (!(attributeType != null))
						{
							return Attribute.GetCustomAttributes(module, inherit);
						}
						return Attribute.GetCustomAttributes(module, attributeType, inherit);
					}
					else
					{
						ParameterInfo parameterInfo = attributeProvider as ParameterInfo;
						if (parameterInfo == null)
						{
							ICustomAttributeProvider customAttributeProvider = (ICustomAttributeProvider)attributeProvider;
							return (Attribute[])((attributeType != null) ? customAttributeProvider.GetCustomAttributes(attributeType, inherit) : customAttributeProvider.GetCustomAttributes(inherit));
						}
						if (!(attributeType != null))
						{
							return Attribute.GetCustomAttributes(parameterInfo, inherit);
						}
						return Attribute.GetCustomAttributes(parameterInfo, attributeType, inherit);
					}
				}
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000173C8 File Offset: 0x000155C8
		public static TypeNameKey SplitFullyQualifiedTypeName(string fullyQualifiedTypeName)
		{
			int? assemblyDelimiterIndex = ReflectionUtils.GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
			string text;
			string text2;
			if (assemblyDelimiterIndex != null)
			{
				text = fullyQualifiedTypeName.Trim(0, assemblyDelimiterIndex.GetValueOrDefault());
				text2 = fullyQualifiedTypeName.Trim(assemblyDelimiterIndex.GetValueOrDefault() + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.GetValueOrDefault() - 1);
			}
			else
			{
				text = fullyQualifiedTypeName;
				text2 = null;
			}
			return new TypeNameKey(text2, text);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00017424 File Offset: 0x00015624
		private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
		{
			int num = 0;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				char c = fullyQualifiedTypeName.get_Chars(i);
				if (c != ',')
				{
					if (c != '[')
					{
						if (c == ']')
						{
							num--;
						}
					}
					else
					{
						num++;
					}
				}
				else if (num == 0)
				{
					return new int?(i);
				}
			}
			return default(int?);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001747C File Offset: 0x0001567C
		public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo)
		{
			MemberTypes memberTypes = memberInfo.MemberType();
			if (memberTypes == 16)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				Type[] array = Enumerable.ToArray<Type>(Enumerable.Select<ParameterInfo, Type>(propertyInfo.GetIndexParameters(), (ParameterInfo p) => p.ParameterType));
				return targetType.GetProperty(propertyInfo.Name, 60, null, propertyInfo.PropertyType, array, null);
			}
			return Enumerable.SingleOrDefault<MemberInfo>(targetType.GetMember(memberInfo.Name, memberInfo.MemberType(), 60));
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000174FD File Offset: 0x000156FD
		public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<MemberInfo> list = new List<MemberInfo>(targetType.GetFields(bindingAttr));
			ReflectionUtils.GetChildPrivateFields(list, targetType, bindingAttr);
			return Enumerable.Cast<FieldInfo>(list);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00017524 File Offset: 0x00015724
		private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr)
		{
			if ((bindingAttr & 32) != null)
			{
				BindingFlags bindingFlags = bindingAttr.RemoveFlag(16);
				while ((targetType = targetType.BaseType()) != null)
				{
					IEnumerable<FieldInfo> enumerable = Enumerable.Where<FieldInfo>(targetType.GetFields(bindingFlags), (FieldInfo f) => f.IsPrivate);
					initialFields.AddRange(enumerable);
				}
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00017588 File Offset: 0x00015788
		public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<PropertyInfo> list = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));
			if (targetType.IsInterface())
			{
				foreach (Type type in targetType.GetInterfaces())
				{
					list.AddRange(type.GetProperties(bindingAttr));
				}
			}
			ReflectionUtils.GetChildPrivateProperties(list, targetType, bindingAttr);
			for (int j = 0; j < list.Count; j++)
			{
				PropertyInfo propertyInfo = list[j];
				if (propertyInfo.DeclaringType != targetType)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
					list[j] = propertyInfo2;
				}
			}
			return list;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00017631 File Offset: 0x00015831
		public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag)
		{
			if ((bindingAttr & flag) != flag)
			{
				return bindingAttr;
			}
			return bindingAttr ^ flag;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00017640 File Offset: 0x00015840
		private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr)
		{
			while ((targetType = targetType.BaseType()) != null)
			{
				foreach (PropertyInfo propertyInfo in targetType.GetProperties(bindingAttr))
				{
					ReflectionUtils.<>c__DisplayClass43_0 CS$<>8__locals1 = new ReflectionUtils.<>c__DisplayClass43_0();
					CS$<>8__locals1.subTypeProperty = propertyInfo;
					if (!CS$<>8__locals1.subTypeProperty.IsVirtual())
					{
						if (!ReflectionUtils.IsPublic(CS$<>8__locals1.subTypeProperty))
						{
							int num = initialProperties.IndexOf((PropertyInfo p) => p.Name == CS$<>8__locals1.subTypeProperty.Name);
							if (num == -1)
							{
								initialProperties.Add(CS$<>8__locals1.subTypeProperty);
							}
							else if (!ReflectionUtils.IsPublic(initialProperties[num]))
							{
								initialProperties[num] = CS$<>8__locals1.subTypeProperty;
							}
						}
						else if (initialProperties.IndexOf((PropertyInfo p) => p.Name == CS$<>8__locals1.subTypeProperty.Name && p.DeclaringType == CS$<>8__locals1.subTypeProperty.DeclaringType) == -1)
						{
							initialProperties.Add(CS$<>8__locals1.subTypeProperty);
						}
					}
					else
					{
						ReflectionUtils.<>c__DisplayClass43_1 CS$<>8__locals2 = new ReflectionUtils.<>c__DisplayClass43_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						ReflectionUtils.<>c__DisplayClass43_1 CS$<>8__locals3 = CS$<>8__locals2;
						MethodInfo baseDefinition = CS$<>8__locals2.CS$<>8__locals1.subTypeProperty.GetBaseDefinition();
						CS$<>8__locals3.subTypePropertyDeclaringType = ((baseDefinition != null) ? baseDefinition.DeclaringType : null) ?? CS$<>8__locals2.CS$<>8__locals1.subTypeProperty.DeclaringType;
						if (initialProperties.IndexOf(delegate(PropertyInfo p)
						{
							if (p.Name == CS$<>8__locals2.CS$<>8__locals1.subTypeProperty.Name && p.IsVirtual())
							{
								MethodInfo baseDefinition2 = p.GetBaseDefinition();
								return (((baseDefinition2 != null) ? baseDefinition2.DeclaringType : null) ?? p.DeclaringType).IsAssignableFrom(CS$<>8__locals2.subTypePropertyDeclaringType);
							}
							return false;
						}) == -1)
						{
							initialProperties.Add(CS$<>8__locals2.CS$<>8__locals1.subTypeProperty);
						}
					}
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00017790 File Offset: 0x00015990
		public static bool IsMethodOverridden(Type currentType, Type methodDeclaringType, string method)
		{
			return Enumerable.Any<MethodInfo>(currentType.GetMethods(52), (MethodInfo info) => info.Name == method && info.DeclaringType != methodDeclaringType && info.GetBaseDefinition().DeclaringType == methodDeclaringType);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000177CC File Offset: 0x000159CC
		public static object GetDefaultValue(Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(type);
			switch (typeCode)
			{
			case PrimitiveTypeCode.Char:
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
				return 0;
			case PrimitiveTypeCode.CharNullable:
			case PrimitiveTypeCode.BooleanNullable:
			case PrimitiveTypeCode.SByteNullable:
			case PrimitiveTypeCode.Int16Nullable:
			case PrimitiveTypeCode.UInt16Nullable:
			case PrimitiveTypeCode.Int32Nullable:
			case PrimitiveTypeCode.ByteNullable:
			case PrimitiveTypeCode.UInt32Nullable:
			case PrimitiveTypeCode.Int64Nullable:
			case PrimitiveTypeCode.UInt64Nullable:
			case PrimitiveTypeCode.SingleNullable:
			case PrimitiveTypeCode.DoubleNullable:
			case PrimitiveTypeCode.DateTimeNullable:
			case PrimitiveTypeCode.DateTimeOffsetNullable:
			case PrimitiveTypeCode.DecimalNullable:
				break;
			case PrimitiveTypeCode.Boolean:
				return false;
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
				return 0L;
			case PrimitiveTypeCode.Single:
				return 0f;
			case PrimitiveTypeCode.Double:
				return 0.0;
			case PrimitiveTypeCode.DateTime:
				return default(DateTime);
			case PrimitiveTypeCode.DateTimeOffset:
				return default(DateTimeOffset);
			case PrimitiveTypeCode.Decimal:
				return 0m;
			case PrimitiveTypeCode.Guid:
				return default(Guid);
			default:
				if (typeCode == PrimitiveTypeCode.BigInteger)
				{
					return default(BigInteger);
				}
				break;
			}
			if (ReflectionUtils.IsNullable(type))
			{
				return null;
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x04000261 RID: 609
		public static readonly Type[] EmptyTypes = Type.EmptyTypes;

		// Token: 0x02000138 RID: 312
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000CD5 RID: 3285 RVA: 0x0003120C File Offset: 0x0002F40C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000CD6 RID: 3286 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000CD7 RID: 3287 RVA: 0x00031218 File Offset: 0x0002F418
			internal bool <GetDefaultConstructor>b__11_0(ConstructorInfo c)
			{
				return !Enumerable.Any<ParameterInfo>(c.GetParameters());
			}

			// Token: 0x06000CD8 RID: 3288 RVA: 0x00031228 File Offset: 0x0002F428
			internal string <GetFieldsAndProperties>b__30_0(MemberInfo m)
			{
				return m.Name;
			}

			// Token: 0x06000CD9 RID: 3289 RVA: 0x00031230 File Offset: 0x0002F430
			internal Type <GetMemberInfoFromType>b__38_0(ParameterInfo p)
			{
				return p.ParameterType;
			}

			// Token: 0x06000CDA RID: 3290 RVA: 0x00031238 File Offset: 0x0002F438
			internal bool <GetChildPrivateFields>b__40_0(FieldInfo f)
			{
				return f.IsPrivate;
			}

			// Token: 0x0400049A RID: 1178
			public static readonly ReflectionUtils.<>c <>9 = new ReflectionUtils.<>c();

			// Token: 0x0400049B RID: 1179
			public static Func<ConstructorInfo, bool> <>9__11_0;

			// Token: 0x0400049C RID: 1180
			public static Func<MemberInfo, string> <>9__30_0;

			// Token: 0x0400049D RID: 1181
			public static Func<ParameterInfo, Type> <>9__38_0;

			// Token: 0x0400049E RID: 1182
			public static Func<FieldInfo, bool> <>9__40_0;
		}

		// Token: 0x02000139 RID: 313
		[CompilerGenerated]
		private sealed class <>c__DisplayClass43_0
		{
			// Token: 0x06000CDB RID: 3291 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass43_0()
			{
			}

			// Token: 0x06000CDC RID: 3292 RVA: 0x00031240 File Offset: 0x0002F440
			internal bool <GetChildPrivateProperties>b__0(PropertyInfo p)
			{
				return p.Name == this.subTypeProperty.Name;
			}

			// Token: 0x06000CDD RID: 3293 RVA: 0x00031258 File Offset: 0x0002F458
			internal bool <GetChildPrivateProperties>b__1(PropertyInfo p)
			{
				return p.Name == this.subTypeProperty.Name && p.DeclaringType == this.subTypeProperty.DeclaringType;
			}

			// Token: 0x0400049F RID: 1183
			public PropertyInfo subTypeProperty;
		}

		// Token: 0x0200013A RID: 314
		[CompilerGenerated]
		private sealed class <>c__DisplayClass43_1
		{
			// Token: 0x06000CDE RID: 3294 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass43_1()
			{
			}

			// Token: 0x06000CDF RID: 3295 RVA: 0x0003128C File Offset: 0x0002F48C
			internal bool <GetChildPrivateProperties>b__2(PropertyInfo p)
			{
				if (p.Name == this.CS$<>8__locals1.subTypeProperty.Name && p.IsVirtual())
				{
					MethodInfo baseDefinition = p.GetBaseDefinition();
					return (((baseDefinition != null) ? baseDefinition.DeclaringType : null) ?? p.DeclaringType).IsAssignableFrom(this.subTypePropertyDeclaringType);
				}
				return false;
			}

			// Token: 0x040004A0 RID: 1184
			public Type subTypePropertyDeclaringType;

			// Token: 0x040004A1 RID: 1185
			public ReflectionUtils.<>c__DisplayClass43_0 CS$<>8__locals1;
		}

		// Token: 0x0200013B RID: 315
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_0
		{
			// Token: 0x06000CE0 RID: 3296 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass44_0()
			{
			}

			// Token: 0x06000CE1 RID: 3297 RVA: 0x000312E7 File Offset: 0x0002F4E7
			internal bool <IsMethodOverridden>b__0(MethodInfo info)
			{
				return info.Name == this.method && info.DeclaringType != this.methodDeclaringType && info.GetBaseDefinition().DeclaringType == this.methodDeclaringType;
			}

			// Token: 0x040004A2 RID: 1186
			public string method;

			// Token: 0x040004A3 RID: 1187
			public Type methodDeclaringType;
		}
	}
}
