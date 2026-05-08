using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000898 RID: 2200
	internal abstract class SignatureType : Type
	{
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060049E1 RID: 18913 RVA: 0x00003FB7 File Offset: 0x000021B7
		public sealed override bool IsSignatureType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060049E2 RID: 18914
		public abstract override bool IsTypeDefinition { get; }

		// Token: 0x060049E3 RID: 18915
		protected abstract override bool HasElementTypeImpl();

		// Token: 0x060049E4 RID: 18916
		protected abstract override bool IsArrayImpl();

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060049E5 RID: 18917
		public abstract override bool IsSZArray { get; }

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060049E6 RID: 18918
		public abstract override bool IsVariableBoundArray { get; }

		// Token: 0x060049E7 RID: 18919
		protected abstract override bool IsByRefImpl();

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060049E8 RID: 18920
		public abstract override bool IsByRefLike { get; }

		// Token: 0x060049E9 RID: 18921
		protected abstract override bool IsPointerImpl();

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060049EA RID: 18922 RVA: 0x000EF370 File Offset: 0x000ED570
		public sealed override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition || this.IsConstructedGenericType;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060049EB RID: 18923
		public abstract override bool IsGenericTypeDefinition { get; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060049EC RID: 18924
		public abstract override bool IsConstructedGenericType { get; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060049ED RID: 18925
		public abstract override bool IsGenericParameter { get; }

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060049EE RID: 18926
		public abstract override bool IsGenericTypeParameter { get; }

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060049EF RID: 18927
		public abstract override bool IsGenericMethodParameter { get; }

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060049F0 RID: 18928
		public abstract override bool ContainsGenericParameters { get; }

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x00047D44 File Offset: 0x00045F44
		public sealed override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x000EF382 File Offset: 0x000ED582
		public sealed override Type MakeArrayType()
		{
			return new SignatureArrayType(this, 1, false);
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x000EF38C File Offset: 0x000ED58C
		public sealed override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			return new SignatureArrayType(this, rank, true);
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x000EF3A0 File Offset: 0x000ED5A0
		public sealed override Type MakeByRefType()
		{
			return new SignatureByRefType(this);
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x000EF3A8 File Offset: 0x000ED5A8
		public sealed override Type MakePointerType()
		{
			return new SignaturePointerType(this);
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x000EF3BC File Offset: 0x000ED5BC
		public sealed override Type GetElementType()
		{
			return this.ElementType;
		}

		// Token: 0x060049F8 RID: 18936
		public abstract override int GetArrayRank();

		// Token: 0x060049F9 RID: 18937
		public abstract override Type GetGenericTypeDefinition();

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060049FA RID: 18938
		public abstract override Type[] GenericTypeArguments { get; }

		// Token: 0x060049FB RID: 18939
		public abstract override Type[] GetGenericArguments();

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060049FC RID: 18940
		public abstract override int GenericParameterPosition { get; }

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060049FD RID: 18941
		internal abstract SignatureType ElementType { get; }

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060049FE RID: 18942 RVA: 0x000025CE File Offset: 0x000007CE
		public sealed override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060049FF RID: 18943
		public abstract override string Name { get; }

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004A00 RID: 18944
		public abstract override string Namespace { get; }

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004A01 RID: 18945 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public sealed override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004A02 RID: 18946 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public sealed override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A03 RID: 18947
		public abstract override string ToString();

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004A04 RID: 18948 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Assembly Assembly
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06004A05 RID: 18949 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Module Module
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06004A06 RID: 18950 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type ReflectedType
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06004A07 RID: 18951 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type BaseType
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type[] GetInterfaces()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06004A0A RID: 18954 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override int MetadataToken
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06004A0C RID: 18956 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type DeclaringType
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override MethodBase DeclaringMethod
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type[] GetGenericParameterConstraints()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06004A0F RID: 18959 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsEnumDefined(object value)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override string GetEnumName(object value)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override string[] GetEnumNames()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type GetEnumUnderlyingType()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Array GetEnumValues()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06004A15 RID: 18965 RVA: 0x000EF3C4 File Offset: 0x000ED5C4
		public sealed override Guid GUID
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override TypeCode GetTypeCodeImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override TypeAttributes GetAttributeFlagsImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override MethodInfo GetMethodImpl(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override MemberInfo[] GetDefaultMembers()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override EventInfo[] GetEvents()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override bool IsCOMObjectImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override bool IsPrimitiveImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06004A33 RID: 18995 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x000EF3DC File Offset: 0x000ED5DC
		public sealed override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override bool IsContextfulImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsEnum
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsEquivalentTo(Type other)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsInstanceOfType(object o)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override bool IsMarshalByRefImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06004A3B RID: 19003 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsSecurityCritical
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004A3D RID: 19005 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsSecurityTransparent
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsSerializable
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected sealed override bool IsValueTypeImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		public sealed override StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004A42 RID: 19010 RVA: 0x000EF3F4 File Offset: 0x000ED5F4
		public sealed override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x000EF40B File Offset: 0x000ED60B
		protected SignatureType()
		{
		}
	}
}
