using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008F8 RID: 2296
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GenericTypeParameterBuilder : TypeInfo
	{
		// Token: 0x06004FA8 RID: 20392 RVA: 0x000FA952 File Offset: 0x000F8B52
		public void SetBaseTypeConstraint(Type baseTypeConstraint)
		{
			this.base_type = baseTypeConstraint ?? typeof(object);
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x000FA969 File Offset: 0x000F8B69
		[ComVisible(true)]
		public void SetInterfaceConstraints(params Type[] interfaceConstraints)
		{
			this.iface_constraints = interfaceConstraints;
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x000FA972 File Offset: 0x000F8B72
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.attrs = genericParameterAttributes;
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x000FA97B File Offset: 0x000F8B7B
		internal GenericTypeParameterBuilder(TypeBuilder tbuilder, MethodBuilder mbuilder, string name, int index)
		{
			this.tbuilder = tbuilder;
			this.mbuilder = mbuilder;
			this.name = name;
			this.index = index;
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x000FA9A0 File Offset: 0x000F8BA0
		internal override Type InternalResolve()
		{
			if (this.mbuilder != null)
			{
				return MethodBase.GetMethodFromHandle(this.mbuilder.MethodHandleInternal, this.mbuilder.TypeBuilder.InternalResolve().TypeHandle).GetGenericArguments()[this.index];
			}
			return this.tbuilder.InternalResolve().GetGenericArguments()[this.index];
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x000FAA04 File Offset: 0x000F8C04
		internal override Type RuntimeResolve()
		{
			if (this.mbuilder != null)
			{
				return MethodBase.GetMethodFromHandle(this.mbuilder.MethodHandleInternal, this.mbuilder.TypeBuilder.RuntimeResolve().TypeHandle).GetGenericArguments()[this.index];
			}
			return this.tbuilder.RuntimeResolve().GetGenericArguments()[this.index];
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x000FAA68 File Offset: 0x000F8C68
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return TypeAttributes.Public;
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x000FAA68 File Offset: 0x000F8C68
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x000FAA68 File Offset: 0x000F8C68
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override EventInfo[] GetEvents()
		{
			throw this.not_supported();
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override Type[] GetInterfaces()
		{
			throw this.not_supported();
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x000FAA68 File Offset: 0x000F8C68
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x000FAA68 File Offset: 0x000F8C68
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override bool IsAssignableFrom(Type c)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00058961 File Offset: 0x00056B61
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override bool IsInstanceOfType(object o)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x000FAA70 File Offset: 0x000F8C70
		protected override bool IsValueTypeImpl()
		{
			return this.base_type != null && this.base_type.IsValueType;
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override Type GetElementType()
		{
			throw this.not_supported();
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06004FCD RID: 20429 RVA: 0x000025CE File Offset: 0x000007CE
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x000FAA8D File Offset: 0x000F8C8D
		public override Assembly Assembly
		{
			get
			{
				return this.tbuilder.Assembly;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x000FAA9A File Offset: 0x000F8C9A
		public override Type BaseType
		{
			get
			{
				return this.base_type;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06004FD1 RID: 20433 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override Guid GUID
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x000FAA68 File Offset: 0x000F8C68
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw this.not_supported();
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06004FD7 RID: 20439 RVA: 0x000FAAA2 File Offset: 0x000F8CA2
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06004FD8 RID: 20440 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06004FD9 RID: 20441 RVA: 0x000FAAAA File Offset: 0x000F8CAA
		public override Module Module
		{
			get
			{
				return this.tbuilder.Module;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06004FDA RID: 20442 RVA: 0x000FAAB7 File Offset: 0x000F8CB7
		public override Type DeclaringType
		{
			get
			{
				if (!(this.mbuilder != null))
				{
					return this.tbuilder;
				}
				return this.mbuilder.DeclaringType;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06004FDB RID: 20443 RVA: 0x000598C5 File Offset: 0x00057AC5
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06004FDC RID: 20444 RVA: 0x000FAA68 File Offset: 0x000F8C68
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x00084CDD File Offset: 0x00082EDD
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x00084CDD File Offset: 0x00082EDD
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06004FDF RID: 20447 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override bool ContainsGenericParameters
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06004FE0 RID: 20448 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06004FE1 RID: 20449 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06004FE2 RID: 20450 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06004FE3 RID: 20451 RVA: 0x00047E00 File Offset: 0x00046000
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06004FE4 RID: 20452 RVA: 0x000FAAD9 File Offset: 0x000F8CD9
		public override int GenericParameterPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x00084CDD File Offset: 0x00082EDD
		public override Type[] GetGenericParameterConstraints()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06004FE6 RID: 20454 RVA: 0x000FAAE1 File Offset: 0x000F8CE1
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.mbuilder;
			}
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x000FAAEC File Offset: 0x000F8CEC
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
				return;
			}
			this.cattrs = new CustomAttributeBuilder[1];
			this.cattrs[0] = customBuilder;
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x000FAB54 File Offset: 0x000F8D54
		[MonoTODO("unverified implementation")]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0004E7AC File Offset: 0x0004C9AC
		private Exception not_supported()
		{
			return new NotSupportedException();
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x000FAAA2 File Offset: 0x000F8CA2
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x000FAB63 File Offset: 0x000F8D63
		[MonoTODO]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x000FAB6C File Offset: 0x000F8D6C
		[MonoTODO]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x000F5F5D File Offset: 0x000F415D
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x000F5F66 File Offset: 0x000F4166
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x000F5F79 File Offset: 0x000F4179
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x000FAB74 File Offset: 0x000F8D74
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("{0} is not a GenericTypeDefinition. MakeGenericType may only be called on a type for which Type.IsGenericTypeDefinition is true."));
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x000F5F81 File Offset: 0x000F4181
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06004FF2 RID: 20466 RVA: 0x0000408A File Offset: 0x0000228A
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040030F6 RID: 12534
		private TypeBuilder tbuilder;

		// Token: 0x040030F7 RID: 12535
		private MethodBuilder mbuilder;

		// Token: 0x040030F8 RID: 12536
		private string name;

		// Token: 0x040030F9 RID: 12537
		private int index;

		// Token: 0x040030FA RID: 12538
		private Type base_type;

		// Token: 0x040030FB RID: 12539
		private Type[] iface_constraints;

		// Token: 0x040030FC RID: 12540
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040030FD RID: 12541
		private GenericParameterAttributes attrs;
	}
}
