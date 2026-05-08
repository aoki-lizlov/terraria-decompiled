using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x0200089E RID: 2206
	public class TypeDelegator : TypeInfo
	{
		// Token: 0x06004A58 RID: 19032 RVA: 0x00058961 File Offset: 0x00056B61
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x000EF82F File Offset: 0x000EDA2F
		protected TypeDelegator()
		{
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x000EF837 File Offset: 0x000EDA37
		public TypeDelegator(Type delegatingType)
		{
			if (delegatingType == null)
			{
				throw new ArgumentNullException("delegatingType");
			}
			this.typeImpl = delegatingType;
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004A5B RID: 19035 RVA: 0x000EF85A File Offset: 0x000EDA5A
		public override Guid GUID
		{
			get
			{
				return this.typeImpl.GUID;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x000EF867 File Offset: 0x000EDA67
		public override int MetadataToken
		{
			get
			{
				return this.typeImpl.MetadataToken;
			}
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x000EF874 File Offset: 0x000EDA74
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x000EF899 File Offset: 0x000EDA99
		public override Module Module
		{
			get
			{
				return this.typeImpl.Module;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x000EF8A6 File Offset: 0x000EDAA6
		public override Assembly Assembly
		{
			get
			{
				return this.typeImpl.Assembly;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x000EF8B3 File Offset: 0x000EDAB3
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.typeImpl.TypeHandle;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x000EF8C0 File Offset: 0x000EDAC0
		public override string Name
		{
			get
			{
				return this.typeImpl.Name;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x000EF8CD File Offset: 0x000EDACD
		public override string FullName
		{
			get
			{
				return this.typeImpl.FullName;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06004A63 RID: 19043 RVA: 0x000EF8DA File Offset: 0x000EDADA
		public override string Namespace
		{
			get
			{
				return this.typeImpl.Namespace;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x000EF8E7 File Offset: 0x000EDAE7
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.typeImpl.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x000EF8F4 File Offset: 0x000EDAF4
		public override Type BaseType
		{
			get
			{
				return this.typeImpl.BaseType;
			}
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x000EF901 File Offset: 0x000EDB01
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x000EF915 File Offset: 0x000EDB15
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetConstructors(bindingAttr);
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x000EF923 File Offset: 0x000EDB23
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.typeImpl.GetMethod(name, bindingAttr);
			}
			return this.typeImpl.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x000EF94B File Offset: 0x000EDB4B
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMethods(bindingAttr);
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x000EF959 File Offset: 0x000EDB59
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetField(name, bindingAttr);
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x000EF968 File Offset: 0x000EDB68
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetFields(bindingAttr);
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x000EF976 File Offset: 0x000EDB76
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.typeImpl.GetInterface(name, ignoreCase);
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x000EF985 File Offset: 0x000EDB85
		public override Type[] GetInterfaces()
		{
			return this.typeImpl.GetInterfaces();
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x000EF992 File Offset: 0x000EDB92
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x000EF9A1 File Offset: 0x000EDBA1
		public override EventInfo[] GetEvents()
		{
			return this.typeImpl.GetEvents();
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x000EF9AE File Offset: 0x000EDBAE
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (returnType == null && types == null)
			{
				return this.typeImpl.GetProperty(name, bindingAttr);
			}
			return this.typeImpl.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x000EF9E0 File Offset: 0x000EDBE0
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetProperties(bindingAttr);
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x000EF9EE File Offset: 0x000EDBEE
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvents(bindingAttr);
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x000EF9FC File Offset: 0x000EDBFC
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x000EFA0A File Offset: 0x000EDC0A
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x000EFA19 File Offset: 0x000EDC19
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMember(name, type, bindingAttr);
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x000EFA29 File Offset: 0x000EDC29
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMembers(bindingAttr);
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x000EFA37 File Offset: 0x000EDC37
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.typeImpl.Attributes;
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x000EFA44 File Offset: 0x000EDC44
		public override bool IsTypeDefinition
		{
			get
			{
				return this.typeImpl.IsTypeDefinition;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x000EFA51 File Offset: 0x000EDC51
		public override bool IsSZArray
		{
			get
			{
				return this.typeImpl.IsSZArray;
			}
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x000EFA5E File Offset: 0x000EDC5E
		protected override bool IsArrayImpl()
		{
			return this.typeImpl.IsArray;
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x000EFA6B File Offset: 0x000EDC6B
		protected override bool IsPrimitiveImpl()
		{
			return this.typeImpl.IsPrimitive;
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x000EFA78 File Offset: 0x000EDC78
		protected override bool IsByRefImpl()
		{
			return this.typeImpl.IsByRef;
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x000EFA85 File Offset: 0x000EDC85
		public override bool IsGenericTypeParameter
		{
			get
			{
				return this.typeImpl.IsGenericTypeParameter;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004A7E RID: 19070 RVA: 0x000EFA92 File Offset: 0x000EDC92
		public override bool IsGenericMethodParameter
		{
			get
			{
				return this.typeImpl.IsGenericMethodParameter;
			}
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x000EFA9F File Offset: 0x000EDC9F
		protected override bool IsPointerImpl()
		{
			return this.typeImpl.IsPointer;
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x000EFAAC File Offset: 0x000EDCAC
		protected override bool IsValueTypeImpl()
		{
			return this.typeImpl.IsValueType;
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x000EFAB9 File Offset: 0x000EDCB9
		protected override bool IsCOMObjectImpl()
		{
			return this.typeImpl.IsCOMObject;
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x000EFAC6 File Offset: 0x000EDCC6
		public override bool IsByRefLike
		{
			get
			{
				return this.typeImpl.IsByRefLike;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x000EFAD3 File Offset: 0x000EDCD3
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.typeImpl.IsConstructedGenericType;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x000EFAE0 File Offset: 0x000EDCE0
		public override bool IsCollectible
		{
			get
			{
				return this.typeImpl.IsCollectible;
			}
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x000EFAED File Offset: 0x000EDCED
		public override Type GetElementType()
		{
			return this.typeImpl.GetElementType();
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x000EFAFA File Offset: 0x000EDCFA
		protected override bool HasElementTypeImpl()
		{
			return this.typeImpl.HasElementType;
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x000EFB07 File Offset: 0x000EDD07
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.typeImpl.UnderlyingSystemType;
			}
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x000EFB14 File Offset: 0x000EDD14
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(inherit);
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x000EFB22 File Offset: 0x000EDD22
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x000EFB31 File Offset: 0x000EDD31
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.typeImpl.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x000EFB40 File Offset: 0x000EDD40
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.typeImpl.GetInterfaceMap(interfaceType);
		}

		// Token: 0x04002ED5 RID: 11989
		protected Type typeImpl;
	}
}
