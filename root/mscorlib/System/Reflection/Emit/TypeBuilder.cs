using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000917 RID: 2327
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_TypeBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TypeBuilder : TypeInfo, _TypeBuilder
	{
		// Token: 0x060051DF RID: 20959 RVA: 0x000174FB File Offset: 0x000156FB
		void _TypeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x000174FB File Offset: 0x000156FB
		void _TypeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x000174FB File Offset: 0x000156FB
		void _TypeBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x000174FB File Offset: 0x000156FB
		void _TypeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x001028E6 File Offset: 0x00100AE6
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.attrs;
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x001028EE File Offset: 0x00100AEE
		private TypeBuilder()
		{
			if (RuntimeType.MakeTypeBuilderInstantiation == null)
			{
				RuntimeType.MakeTypeBuilderInstantiation = new Func<Type, Type[], Type>(TypeBuilderInstantiation.MakeGenericType);
			}
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x00102910 File Offset: 0x00100B10
		[PreserveDependency("DoTypeBuilderResolve", "System.AppDomain")]
		internal TypeBuilder(ModuleBuilder mb, TypeAttributes attr, int table_idx)
			: this()
		{
			this.parent = null;
			this.attrs = attr;
			this.class_size = 0;
			this.table_idx = table_idx;
			this.tname = ((table_idx == 1) ? "<Module>" : ("type_" + table_idx.ToString()));
			this.nspace = string.Empty;
			this.fullname = TypeIdentifiers.WithoutEscape(this.tname);
			this.pmodule = mb;
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x00102984 File Offset: 0x00100B84
		internal TypeBuilder(ModuleBuilder mb, string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packing_size, int type_size, Type nesting_type)
			: this()
		{
			this.parent = TypeBuilder.ResolveUserType(parent);
			this.attrs = attr;
			this.class_size = type_size;
			this.packing_size = packing_size;
			this.nesting_type = nesting_type;
			this.check_name("fullname", name);
			if (parent == null && (attr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
			{
				throw new InvalidOperationException("Interface must be declared abstract.");
			}
			int num = name.LastIndexOf('.');
			if (num != -1)
			{
				this.tname = name.Substring(num + 1);
				this.nspace = name.Substring(0, num);
			}
			else
			{
				this.tname = name;
				this.nspace = string.Empty;
			}
			if (interfaces != null)
			{
				this.interfaces = new Type[interfaces.Length];
				Array.Copy(interfaces, this.interfaces, interfaces.Length);
			}
			this.pmodule = mb;
			if ((attr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && parent == null)
			{
				this.parent = typeof(object);
			}
			this.table_idx = mb.get_next_table_index(this, 2, 1);
			this.fullname = this.GetFullName();
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060051E7 RID: 20967 RVA: 0x00102A97 File Offset: 0x00100C97
		public override Assembly Assembly
		{
			get
			{
				return this.pmodule.Assembly;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060051E8 RID: 20968 RVA: 0x00102AA4 File Offset: 0x00100CA4
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.fullname.DisplayName + ", " + this.Assembly.FullName;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060051E9 RID: 20969 RVA: 0x00102AC6 File Offset: 0x00100CC6
		public override Type BaseType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060051EA RID: 20970 RVA: 0x00102ACE File Offset: 0x00100CCE
		public override Type DeclaringType
		{
			get
			{
				return this.nesting_type;
			}
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x00102AD8 File Offset: 0x00100CD8
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (c == this)
			{
				return false;
			}
			Type baseType = this.parent;
			while (baseType != null)
			{
				if (c == baseType)
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060051EC RID: 20972 RVA: 0x00102B20 File Offset: 0x00100D20
		public override Type UnderlyingSystemType
		{
			get
			{
				if (this.is_created)
				{
					return this.created.UnderlyingSystemType;
				}
				if (!this.IsEnum)
				{
					return this;
				}
				if (this.underlying_type != null)
				{
					return this.underlying_type;
				}
				throw new InvalidOperationException("Enumeration type is not defined.");
			}
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x00102B60 File Offset: 0x00100D60
		private TypeName GetFullName()
		{
			TypeIdentifier typeIdentifier = TypeIdentifiers.FromInternal(this.tname);
			if (this.nesting_type != null)
			{
				return TypeNames.FromDisplay(this.nesting_type.FullName).NestedName(typeIdentifier);
			}
			if (this.nspace != null && this.nspace.Length > 0)
			{
				return TypeIdentifiers.FromInternal(this.nspace, typeIdentifier);
			}
			return typeIdentifier;
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060051EE RID: 20974 RVA: 0x00102BC2 File Offset: 0x00100DC2
		public override string FullName
		{
			get
			{
				return this.fullname.DisplayName;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x060051EF RID: 20975 RVA: 0x00102BCF File Offset: 0x00100DCF
		public override Guid GUID
		{
			get
			{
				this.check_created();
				return this.created.GUID;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x060051F0 RID: 20976 RVA: 0x00102BE2 File Offset: 0x00100DE2
		public override Module Module
		{
			get
			{
				return this.pmodule;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x060051F1 RID: 20977 RVA: 0x00102BEA File Offset: 0x00100DEA
		public override string Name
		{
			get
			{
				return this.tname;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x060051F2 RID: 20978 RVA: 0x00102BF2 File Offset: 0x00100DF2
		public override string Namespace
		{
			get
			{
				return this.nspace;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x060051F3 RID: 20979 RVA: 0x00102BFA File Offset: 0x00100DFA
		public PackingSize PackingSize
		{
			get
			{
				return this.packing_size;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x060051F4 RID: 20980 RVA: 0x00102C02 File Offset: 0x00100E02
		public int Size
		{
			get
			{
				return this.class_size;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x060051F5 RID: 20981 RVA: 0x00102ACE File Offset: 0x00100CCE
		public override Type ReflectedType
		{
			get
			{
				return this.nesting_type;
			}
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x00102C0C File Offset: 0x00100E0C
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("Request* values are not permitted", "action");
			}
			this.check_not_created();
			if (this.permissions != null)
			{
				RefEmitPermissionSet[] array = this.permissions;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].action == action)
					{
						throw new InvalidOperationException("Multiple permission sets specified with the same SecurityAction.");
					}
				}
				RefEmitPermissionSet[] array2 = new RefEmitPermissionSet[this.permissions.Length + 1];
				this.permissions.CopyTo(array2, 0);
				this.permissions = array2;
			}
			else
			{
				this.permissions = new RefEmitPermissionSet[1];
			}
			this.permissions[this.permissions.Length - 1] = new RefEmitPermissionSet(action, pset.ToXml().ToString());
			this.attrs |= TypeAttributes.HasSecurity;
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x00102CEC File Offset: 0x00100EEC
		[ComVisible(true)]
		public void AddInterfaceImplementation(Type interfaceType)
		{
			if (interfaceType == null)
			{
				throw new ArgumentNullException("interfaceType");
			}
			this.check_not_created();
			if (this.interfaces != null)
			{
				Type[] array = this.interfaces;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == interfaceType)
					{
						return;
					}
				}
				Type[] array2 = new Type[this.interfaces.Length + 1];
				this.interfaces.CopyTo(array2, 0);
				array2[this.interfaces.Length] = interfaceType;
				this.interfaces = array2;
				return;
			}
			this.interfaces = new Type[1];
			this.interfaces[0] = interfaceType;
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x00102D84 File Offset: 0x00100F84
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			if (!(this.created == typeof(object)))
			{
				return this.created.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
			}
			if (this.ctors == null)
			{
				return null;
			}
			ConstructorBuilder constructorBuilder = null;
			int num = 0;
			foreach (ConstructorBuilder constructorBuilder2 in this.ctors)
			{
				if (callConvention == CallingConventions.Any || constructorBuilder2.CallingConvention == callConvention)
				{
					constructorBuilder = constructorBuilder2;
					num++;
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (types != null)
			{
				MethodBase[] array2 = new MethodBase[num];
				if (num == 1)
				{
					array2[0] = constructorBuilder;
				}
				else
				{
					num = 0;
					foreach (ConstructorBuilder constructorInfo in this.ctors)
					{
						if (callConvention == CallingConventions.Any || constructorInfo.CallingConvention == callConvention)
						{
							array2[num++] = constructorInfo;
						}
					}
				}
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				return (ConstructorInfo)binder.SelectMethod(bindingAttr, array2, types, modifiers);
			}
			if (num > 1)
			{
				throw new AmbiguousMatchException();
			}
			return constructorBuilder;
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x00102E7F File Offset: 0x0010107F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (!this.is_created)
			{
				throw new NotSupportedException();
			}
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x00102E97 File Offset: 0x00101097
		public override object[] GetCustomAttributes(bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(inherit);
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x00102EAB File Offset: 0x001010AB
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00102EC0 File Offset: 0x001010C0
		public TypeBuilder DefineNestedType(string name)
		{
			return this.DefineNestedType(name, TypeAttributes.NestedPrivate, this.pmodule.assemblyb.corlib_object_type, null);
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x00102EDB File Offset: 0x001010DB
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr)
		{
			return this.DefineNestedType(name, attr, this.pmodule.assemblyb.corlib_object_type, null);
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x00102EF6 File Offset: 0x001010F6
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent)
		{
			return this.DefineNestedType(name, attr, parent, null);
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x00102F04 File Offset: 0x00101104
		private TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packSize, int typeSize)
		{
			if (interfaces != null)
			{
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (interfaces[i] == null)
					{
						throw new ArgumentNullException("interfaces");
					}
				}
			}
			TypeBuilder typeBuilder = new TypeBuilder(this.pmodule, name, attr, parent, interfaces, packSize, typeSize, this);
			typeBuilder.fullname = typeBuilder.GetFullName();
			this.pmodule.RegisterTypeName(typeBuilder, typeBuilder.fullname);
			if (this.subtypes != null)
			{
				TypeBuilder[] array = new TypeBuilder[this.subtypes.Length + 1];
				Array.Copy(this.subtypes, array, this.subtypes.Length);
				array[this.subtypes.Length] = typeBuilder;
				this.subtypes = array;
			}
			else
			{
				this.subtypes = new TypeBuilder[1];
				this.subtypes[0] = typeBuilder;
			}
			return typeBuilder;
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00102FC6 File Offset: 0x001011C6
		[ComVisible(true)]
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			return this.DefineNestedType(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00102FD5 File Offset: 0x001011D5
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, int typeSize)
		{
			return this.DefineNestedType(name, attr, parent, null, PackingSize.Unspecified, typeSize);
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x00102FE4 File Offset: 0x001011E4
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize)
		{
			return this.DefineNestedType(name, attr, parent, null, packSize, 0);
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x00102FF3 File Offset: 0x001011F3
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize, int typeSize)
		{
			return this.DefineNestedType(name, attr, parent, null, packSize, typeSize);
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x00103003 File Offset: 0x00101203
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes)
		{
			return this.DefineConstructor(attributes, callingConvention, parameterTypes, null, null);
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00103010 File Offset: 0x00101210
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			this.check_not_created();
			ConstructorBuilder constructorBuilder = new ConstructorBuilder(this, attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = new ConstructorBuilder[this.ctors.Length + 1];
				Array.Copy(this.ctors, array, this.ctors.Length);
				array[this.ctors.Length] = constructorBuilder;
				this.ctors = array;
			}
			else
			{
				this.ctors = new ConstructorBuilder[1];
				this.ctors[0] = constructorBuilder;
			}
			return constructorBuilder;
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x00103088 File Offset: 0x00101288
		[ComVisible(true)]
		public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
		{
			Type type;
			if (this.parent != null)
			{
				type = this.parent;
			}
			else
			{
				type = this.pmodule.assemblyb.corlib_object_type;
			}
			Type type2 = type;
			type = type.InternalResolve();
			if (type == typeof(object) || type == typeof(ValueType))
			{
				type = type2;
			}
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (constructor == null)
			{
				throw new NotSupportedException("Parent does not have a default constructor. The default constructor must be explicitly defined.");
			}
			ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, Type.EmptyTypes);
			ILGenerator ilgenerator = constructorBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return constructorBuilder;
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00103144 File Offset: 0x00101344
		private void append_method(MethodBuilder mb)
		{
			if (this.methods != null)
			{
				if (this.methods.Length == this.num_methods)
				{
					MethodBuilder[] array = new MethodBuilder[this.methods.Length * 2];
					Array.Copy(this.methods, array, this.num_methods);
					this.methods = array;
				}
			}
			else
			{
				this.methods = new MethodBuilder[1];
			}
			this.methods[this.num_methods] = mb;
			this.num_methods++;
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x001031BC File Offset: 0x001013BC
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x001031CC File Offset: 0x001013CC
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x001031EC File Offset: 0x001013EC
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.check_name("name", name);
			this.check_not_created();
			if (base.IsInterface && ((attributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (attributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("Interface method must be abstract and virtual.");
			}
			if (returnType == null)
			{
				returnType = this.pmodule.assemblyb.corlib_void_type;
			}
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			this.append_method(methodBuilder);
			return methodBuilder;
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x00103270 File Offset: 0x00101470
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x00103298 File Offset: 0x00101498
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			this.check_name("name", name);
			this.check_name("dllName", dllName);
			this.check_name("entryName", entryName);
			if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("PInvoke methods must be static and native and cannot be abstract.");
			}
			if (base.IsInterface)
			{
				throw new ArgumentException("PInvoke methods cannot exist on interfaces.");
			}
			this.check_not_created();
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, dllName, entryName, nativeCallConv, nativeCharSet);
			this.append_method(methodBuilder);
			return methodBuilder;
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x00103320 File Offset: 0x00101520
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, name, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x00103341 File Offset: 0x00101541
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard);
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x0010334C File Offset: 0x0010154C
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention)
		{
			return this.DefineMethod(name, attributes, callingConvention, null, null);
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0010335C File Offset: 0x0010155C
		public void DefineMethodOverride(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
		{
			if (methodInfoBody == null)
			{
				throw new ArgumentNullException("methodInfoBody");
			}
			if (methodInfoDeclaration == null)
			{
				throw new ArgumentNullException("methodInfoDeclaration");
			}
			this.check_not_created();
			if (methodInfoBody.DeclaringType != this)
			{
				throw new ArgumentException("method body must belong to this type");
			}
			if (methodInfoBody is MethodBuilder)
			{
				((MethodBuilder)methodInfoBody).set_override(methodInfoDeclaration);
			}
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x001033C4 File Offset: 0x001015C4
		public FieldBuilder DefineField(string fieldName, Type type, FieldAttributes attributes)
		{
			return this.DefineField(fieldName, type, null, null, attributes);
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x001033D4 File Offset: 0x001015D4
		public FieldBuilder DefineField(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			this.check_name("fieldName", fieldName);
			if (type == typeof(void))
			{
				throw new ArgumentException("Bad field type in defining field.");
			}
			this.check_not_created();
			FieldBuilder fieldBuilder = new FieldBuilder(this, fieldName, type, attributes, requiredCustomModifiers, optionalCustomModifiers);
			if (this.fields != null)
			{
				if (this.fields.Length == this.num_fields)
				{
					FieldBuilder[] array = new FieldBuilder[this.fields.Length * 2];
					Array.Copy(this.fields, array, this.num_fields);
					this.fields = array;
				}
				this.fields[this.num_fields] = fieldBuilder;
				this.num_fields++;
			}
			else
			{
				this.fields = new FieldBuilder[1];
				this.fields[0] = fieldBuilder;
				this.num_fields++;
			}
			if (this.IsEnum && this.underlying_type == null && (attributes & FieldAttributes.Static) == FieldAttributes.PrivateScope)
			{
				this.underlying_type = type;
			}
			return fieldBuilder;
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x001034C8 File Offset: 0x001016C8
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, (CallingConventions)0, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x001034E8 File Offset: 0x001016E8
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x00103508 File Offset: 0x00101708
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			return this.DefineProperty(name, attributes, (CallingConventions)0, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x0010352C File Offset: 0x0010172C
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.check_name("name", name);
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentNullException("parameterTypes");
					}
				}
			}
			this.check_not_created();
			PropertyBuilder propertyBuilder = new PropertyBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			if (this.properties != null)
			{
				Array.Resize<PropertyBuilder>(ref this.properties, this.properties.Length + 1);
				this.properties[this.properties.Length - 1] = propertyBuilder;
			}
			else
			{
				this.properties = new PropertyBuilder[] { propertyBuilder };
			}
			return propertyBuilder;
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x001035CE File Offset: 0x001017CE
		[ComVisible(true)]
		public ConstructorBuilder DefineTypeInitializer()
		{
			return this.DefineConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, null);
		}

		// Token: 0x06005218 RID: 21016
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TypeInfo create_runtime_class();

		// Token: 0x06005219 RID: 21017 RVA: 0x001035DD File Offset: 0x001017DD
		private bool is_nested_in(Type t)
		{
			while (t != null)
			{
				if (t == this)
				{
					return true;
				}
				t = t.DeclaringType;
			}
			return false;
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x00103600 File Offset: 0x00101800
		private bool has_ctor_method()
		{
			MethodAttributes methodAttributes = MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
			for (int i = 0; i < this.num_methods; i++)
			{
				MethodBuilder methodBuilder = this.methods[i];
				if (methodBuilder.Name == ConstructorInfo.ConstructorName && (methodBuilder.Attributes & methodAttributes) == methodAttributes)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x0010364D File Offset: 0x0010184D
		public Type CreateType()
		{
			return this.CreateTypeInfo();
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00103658 File Offset: 0x00101858
		public TypeInfo CreateTypeInfo()
		{
			if (this.createTypeCalled)
			{
				return this.created;
			}
			if (!base.IsInterface && this.parent == null && this != this.pmodule.assemblyb.corlib_object_type && this.FullName != "<Module>")
			{
				this.SetParent(this.pmodule.assemblyb.corlib_object_type);
			}
			if (this.fields != null)
			{
				foreach (FieldBuilder fieldBuilder in this.fields)
				{
					if (!(fieldBuilder == null))
					{
						Type fieldType = fieldBuilder.FieldType;
						if (!fieldBuilder.IsStatic && fieldType is TypeBuilder && fieldType.IsValueType && fieldType != this && this.is_nested_in(fieldType))
						{
							TypeBuilder typeBuilder = (TypeBuilder)fieldType;
							if (!typeBuilder.is_created)
							{
								AppDomain.CurrentDomain.DoTypeBuilderResolve(typeBuilder);
								bool is_created = typeBuilder.is_created;
							}
						}
					}
				}
			}
			if (!base.IsInterface && !base.IsValueType && this.ctors == null && this.tname != "<Module>" && ((this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) | TypeAttributes.Sealed) != (TypeAttributes.Abstract | TypeAttributes.Sealed) && !this.has_ctor_method())
			{
				this.DefineDefaultConstructor(MethodAttributes.Public);
			}
			this.createTypeCalled = true;
			if (this.parent != null && this.parent.IsSealed)
			{
				string[] array2 = new string[5];
				array2[0] = "Could not load type '";
				array2[1] = this.FullName;
				array2[2] = "' from assembly '";
				int num = 3;
				Assembly assembly = this.Assembly;
				array2[num] = ((assembly != null) ? assembly.ToString() : null);
				array2[4] = "' because the parent type is sealed.";
				throw new TypeLoadException(string.Concat(array2));
			}
			if (this.parent == this.pmodule.assemblyb.corlib_enum_type && this.methods != null)
			{
				string[] array3 = new string[5];
				array3[0] = "Could not load type '";
				array3[1] = this.FullName;
				array3[2] = "' from assembly '";
				int num2 = 3;
				Assembly assembly2 = this.Assembly;
				array3[num2] = ((assembly2 != null) ? assembly2.ToString() : null);
				array3[4] = "' because it is an enum with methods.";
				throw new TypeLoadException(string.Concat(array3));
			}
			if (this.interfaces != null)
			{
				foreach (Type type in this.interfaces)
				{
					if (type.IsNestedPrivate && type.Assembly != this.Assembly)
					{
						string[] array5 = new string[7];
						array5[0] = "Could not load type '";
						array5[1] = this.FullName;
						array5[2] = "' from assembly '";
						int num3 = 3;
						Assembly assembly3 = this.Assembly;
						array5[num3] = ((assembly3 != null) ? assembly3.ToString() : null);
						array5[4] = "' because it is implements the inaccessible interface '";
						array5[5] = type.FullName;
						array5[6] = "'.";
						throw new TypeLoadException(string.Concat(array5));
					}
				}
			}
			if (this.methods != null)
			{
				bool flag = !base.IsAbstract;
				for (int j = 0; j < this.num_methods; j++)
				{
					MethodBuilder methodBuilder = this.methods[j];
					if (flag && methodBuilder.IsAbstract)
					{
						string text = "Type is concrete but has abstract method ";
						MethodBuilder methodBuilder2 = methodBuilder;
						throw new InvalidOperationException(text + ((methodBuilder2 != null) ? methodBuilder2.ToString() : null));
					}
					methodBuilder.check_override();
					methodBuilder.fixup();
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array6 = this.ctors;
				for (int i = 0; i < array6.Length; i++)
				{
					array6[i].fixup();
				}
			}
			this.ResolveUserTypes();
			this.created = this.create_runtime_class();
			if (this.created != null)
			{
				return this.created;
			}
			return this;
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x001039D4 File Offset: 0x00101BD4
		private void ResolveUserTypes()
		{
			this.parent = TypeBuilder.ResolveUserType(this.parent);
			TypeBuilder.ResolveUserTypes(this.interfaces);
			if (this.fields != null)
			{
				foreach (FieldBuilder fieldBuilder in this.fields)
				{
					if (fieldBuilder != null)
					{
						fieldBuilder.ResolveUserTypes();
					}
				}
			}
			if (this.methods != null)
			{
				foreach (MethodBuilder methodBuilder in this.methods)
				{
					if (methodBuilder != null)
					{
						methodBuilder.ResolveUserTypes();
					}
				}
			}
			if (this.ctors != null)
			{
				foreach (ConstructorBuilder constructorBuilder in this.ctors)
				{
					if (constructorBuilder != null)
					{
						constructorBuilder.ResolveUserTypes();
					}
				}
			}
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x00103A98 File Offset: 0x00101C98
		internal static void ResolveUserTypes(Type[] types)
		{
			if (types != null)
			{
				for (int i = 0; i < types.Length; i++)
				{
					types[i] = TypeBuilder.ResolveUserType(types[i]);
				}
			}
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x00103AC4 File Offset: 0x00101CC4
		internal static Type ResolveUserType(Type t)
		{
			if (!(t != null) || (!(t.GetType().Assembly != typeof(int).Assembly) && !(t is TypeDelegator)))
			{
				return t;
			}
			t = t.UnderlyingSystemType;
			if (t != null && (t.GetType().Assembly != typeof(int).Assembly || t is TypeDelegator))
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			return t;
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00103B4C File Offset: 0x00101D4C
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.methods != null)
			{
				for (int i = 0; i < this.num_methods; i++)
				{
					this.methods[i].FixupTokens(token_map, member_map);
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = this.ctors;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].FixupTokens(token_map, member_map);
				}
			}
			if (this.subtypes != null)
			{
				TypeBuilder[] array2 = this.subtypes;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].FixupTokens(token_map, member_map);
				}
			}
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x00103BD0 File Offset: 0x00101DD0
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			symbolWriter.OpenNamespace(this.Namespace);
			if (this.methods != null)
			{
				for (int i = 0; i < this.num_methods; i++)
				{
					this.methods[i].GenerateDebugInfo(symbolWriter);
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = this.ctors;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].GenerateDebugInfo(symbolWriter);
				}
			}
			symbolWriter.CloseNamespace();
			if (this.subtypes != null)
			{
				for (int k = 0; k < this.subtypes.Length; k++)
				{
					this.subtypes[k].GenerateDebugInfo(symbolWriter);
				}
			}
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x00103C65 File Offset: 0x00101E65
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetConstructors(bindingAttr);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x00103C84 File Offset: 0x00101E84
		internal ConstructorInfo[] GetConstructorsInternal(BindingFlags bindingAttr)
		{
			if (this.ctors == null)
			{
				return new ConstructorInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (ConstructorBuilder constructorBuilder in this.ctors)
			{
				bool flag = false;
				MethodAttributes attributes = constructorBuilder.Attributes;
				if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
				{
					if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
					{
						if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						arrayList.Add(constructorBuilder);
					}
				}
			}
			ConstructorInfo[] array2 = new ConstructorInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x00047E00 File Offset: 0x00046000
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x00103D25 File Offset: 0x00101F25
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetEvent(name, bindingAttr);
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x00048054 File Offset: 0x00046254
		public override EventInfo[] GetEvents()
		{
			return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x00103D3A File Offset: 0x00101F3A
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetEvents(bindingAttr);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x00103D58 File Offset: 0x00101F58
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetField(name, bindingAttr);
			}
			if (this.fields == null)
			{
				return null;
			}
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (!(fieldInfo == null) && !(fieldInfo.Name != name))
				{
					bool flag = false;
					FieldAttributes attributes = fieldInfo.Attributes;
					if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							return fieldInfo;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00103E04 File Offset: 0x00102004
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetFields(bindingAttr);
			}
			if (this.fields == null)
			{
				return new FieldInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (!(fieldInfo == null))
				{
					bool flag = false;
					FieldAttributes attributes = fieldInfo.Attributes;
					if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(fieldInfo);
						}
					}
				}
			}
			FieldInfo[] array2 = new FieldInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x00103ECA File Offset: 0x001020CA
		public override Type GetInterface(string name, bool ignoreCase)
		{
			this.check_created();
			return this.created.GetInterface(name, ignoreCase);
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x00103EE0 File Offset: 0x001020E0
		public override Type[] GetInterfaces()
		{
			if (this.is_created)
			{
				return this.created.GetInterfaces();
			}
			if (this.interfaces != null)
			{
				Type[] array = new Type[this.interfaces.Length];
				this.interfaces.CopyTo(array, 0);
				return array;
			}
			return Type.EmptyTypes;
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x00103F2B File Offset: 0x0010212B
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetMember(name, type, bindingAttr);
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00103F41 File Offset: 0x00102141
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetMembers(bindingAttr);
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x00103F58 File Offset: 0x00102158
		private MethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, bool ignoreCase, Type reflected_type)
		{
			MethodInfo[] array2;
			if ((bindingAttr & BindingFlags.DeclaredOnly) == BindingFlags.Default && this.parent != null)
			{
				MethodInfo[] array = this.parent.GetMethods(bindingAttr);
				ArrayList arrayList = new ArrayList(array.Length);
				bool flag = (bindingAttr & BindingFlags.FlattenHierarchy) > BindingFlags.Default;
				foreach (MethodInfo methodInfo in array)
				{
					MethodAttributes methodAttributes = methodInfo.Attributes;
					if (!methodInfo.IsStatic || flag)
					{
						MethodAttributes methodAttributes2 = methodAttributes & MethodAttributes.MemberAccessMask;
						bool flag2;
						if (methodAttributes2 != MethodAttributes.Private)
						{
							if (methodAttributes2 != MethodAttributes.Assembly)
							{
								if (methodAttributes2 == MethodAttributes.Public)
								{
									flag2 = (bindingAttr & BindingFlags.Public) > BindingFlags.Default;
								}
								else
								{
									flag2 = (bindingAttr & BindingFlags.NonPublic) > BindingFlags.Default;
								}
							}
							else
							{
								flag2 = (bindingAttr & BindingFlags.NonPublic) > BindingFlags.Default;
							}
						}
						else
						{
							flag2 = false;
						}
						if (flag2)
						{
							arrayList.Add(methodInfo);
						}
					}
				}
				if (this.methods == null)
				{
					array2 = new MethodInfo[arrayList.Count];
					arrayList.CopyTo(array2);
				}
				else
				{
					array2 = new MethodInfo[this.methods.Length + arrayList.Count];
					arrayList.CopyTo(array2, 0);
					this.methods.CopyTo(array2, arrayList.Count);
				}
			}
			else
			{
				MethodInfo[] array3 = this.methods;
				array2 = array3;
			}
			if (array2 == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList2 = new ArrayList();
			foreach (MethodInfo methodInfo2 in array2)
			{
				if (!(methodInfo2 == null) && (name == null || string.Compare(methodInfo2.Name, name, ignoreCase) == 0))
				{
					bool flag2 = false;
					MethodAttributes methodAttributes = methodInfo2.Attributes;
					if ((methodAttributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag2 = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag2 = true;
					}
					if (flag2)
					{
						flag2 = false;
						if ((methodAttributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag2 = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag2 = true;
						}
						if (flag2)
						{
							arrayList2.Add(methodInfo2);
						}
					}
				}
			}
			MethodInfo[] array4 = new MethodInfo[arrayList2.Count];
			arrayList2.CopyTo(array4);
			return array4;
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x00104118 File Offset: 0x00102318
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodsByName(null, bindingAttr, false, this);
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x00104124 File Offset: 0x00102324
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			if (types == null)
			{
				return this.created.GetMethod(name, bindingAttr);
			}
			return this.created.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x00104154 File Offset: 0x00102354
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			this.check_created();
			if (this.subtypes == null)
			{
				return null;
			}
			foreach (TypeBuilder typeBuilder in this.subtypes)
			{
				if (typeBuilder.is_created)
				{
					if ((typeBuilder.attrs & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
					{
						if ((bindingAttr & BindingFlags.Public) == BindingFlags.Default)
						{
							goto IL_0055;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default)
					{
						goto IL_0055;
					}
					if (typeBuilder.Name == name)
					{
						return typeBuilder.created;
					}
				}
				IL_0055:;
			}
			return null;
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x001041C4 File Offset: 0x001023C4
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			if (!this.is_created)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			if (this.subtypes == null)
			{
				return Type.EmptyTypes;
			}
			foreach (TypeBuilder typeBuilder in this.subtypes)
			{
				bool flag = false;
				if ((typeBuilder.attrs & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
				{
					if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					arrayList.Add(typeBuilder);
				}
			}
			Type[] array2 = new Type[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x00104254 File Offset: 0x00102454
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetProperties(bindingAttr);
			}
			if (this.properties == null)
			{
				return new PropertyInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (PropertyBuilder propertyInfo in this.properties)
			{
				bool flag = false;
				MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
				if (methodInfo == null)
				{
					methodInfo = propertyInfo.GetSetMethod(true);
				}
				if (!(methodInfo == null))
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(propertyInfo);
						}
					}
				}
			}
			PropertyInfo[] array2 = new PropertyInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x00104333 File Offset: 0x00102533
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x0010433B File Offset: 0x0010253B
		protected override bool HasElementTypeImpl()
		{
			return this.is_created && this.created.HasElementType;
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x00104354 File Offset: 0x00102554
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			this.check_created();
			return this.created.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x00047E3A File Offset: 0x0004603A
		protected override bool IsCOMObjectImpl()
		{
			return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x00104380 File Offset: 0x00102580
		protected override bool IsValueTypeImpl()
		{
			if (this == this.pmodule.assemblyb.corlib_value_type || this == this.pmodule.assemblyb.corlib_enum_type)
			{
				return false;
			}
			Type baseType = this.parent;
			while (baseType != null)
			{
				if (baseType == this.pmodule.assemblyb.corlib_value_type)
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x000F5F5D File Offset: 0x000F415D
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x000F5F66 File Offset: 0x000F4166
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x000F5F79 File Offset: 0x000F4179
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x001043F4 File Offset: 0x001025F4
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException("not a generic type definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException(string.Format("The type or method has {0} generic parameter(s) but {1} generic argument(s) where provided. A generic argument must be provided for each generic parameter.", this.generic_params.Length, typeArguments.Length), "typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			Type[] array = new Type[typeArguments.Length];
			typeArguments.CopyTo(array, 0);
			return this.pmodule.assemblyb.MakeGenericType(this, array);
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x000F5F81 File Offset: 0x000F4181
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06005242 RID: 21058 RVA: 0x001044A2 File Offset: 0x001026A2
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				this.check_created();
				return this.created.TypeHandle;
			}
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x001044B8 File Offset: 0x001026B8
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.StructLayoutAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2] | ((int)data[3] << 8);
				this.attrs &= ~TypeAttributes.LayoutMask;
				switch (num)
				{
				case 0:
					this.attrs |= TypeAttributes.SequentialLayout;
					goto IL_00A5;
				case 2:
					this.attrs |= TypeAttributes.ExplicitLayout;
					goto IL_00A5;
				case 3:
					this.attrs |= TypeAttributes.NotPublic;
					goto IL_00A5;
				}
				throw new Exception("Error in customattr");
				IL_00A5:
				Type type = ((customBuilder.Ctor is ConstructorBuilder) ? ((ConstructorBuilder)customBuilder.Ctor).parameters[0] : customBuilder.Ctor.GetParametersInternal()[0].ParameterType);
				int num2 = 6;
				if (type.FullName == "System.Int16")
				{
					num2 = 4;
				}
				int num3 = (int)data[num2++];
				num3 |= (int)data[num2++] << 8;
				for (int i = 0; i < num3; i++)
				{
					num2++;
					int num4;
					if (data[num2++] == 85)
					{
						num4 = CustomAttributeBuilder.decode_len(data, num2, out num2);
						CustomAttributeBuilder.string_from_bytes(data, num2, num4);
						num2 += num4;
					}
					num4 = CustomAttributeBuilder.decode_len(data, num2, out num2);
					string text = CustomAttributeBuilder.string_from_bytes(data, num2, num4);
					num2 += num4;
					int num5 = (int)data[num2++];
					num5 |= (int)data[num2++] << 8;
					num5 |= (int)data[num2++] << 16;
					num5 |= (int)data[num2++] << 24;
					if (!(text == "CharSet"))
					{
						if (!(text == "Pack"))
						{
							if (text == "Size")
							{
								this.class_size = num5;
							}
						}
						else
						{
							this.packing_size = (PackingSize)num5;
						}
					}
					else
					{
						switch (num5)
						{
						case 1:
						case 2:
							this.attrs &= ~TypeAttributes.StringFormatMask;
							break;
						case 3:
							this.attrs &= ~TypeAttributes.AutoClass;
							this.attrs |= TypeAttributes.UnicodeClass;
							break;
						case 4:
							this.attrs &= ~TypeAttributes.UnicodeClass;
							this.attrs |= TypeAttributes.AutoClass;
							break;
						}
					}
				}
				return;
			}
			if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= TypeAttributes.SpecialName;
				return;
			}
			if (fullName == "System.SerializableAttribute")
			{
				this.attrs |= TypeAttributes.Serializable;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.ComImportAttribute")
			{
				this.attrs |= TypeAttributes.Import;
				return;
			}
			if (fullName == "System.Security.SuppressUnmanagedCodeSecurityAttribute")
			{
				this.attrs |= TypeAttributes.HasSecurity;
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

		// Token: 0x06005244 RID: 21060 RVA: 0x001047EC File Offset: 0x001029EC
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x001047FC File Offset: 0x001029FC
		public EventBuilder DefineEvent(string name, EventAttributes attributes, Type eventtype)
		{
			this.check_name("name", name);
			if (eventtype == null)
			{
				throw new ArgumentNullException("type");
			}
			this.check_not_created();
			EventBuilder eventBuilder = new EventBuilder(this, name, attributes, eventtype);
			if (this.events != null)
			{
				EventBuilder[] array = new EventBuilder[this.events.Length + 1];
				Array.Copy(this.events, array, this.events.Length);
				array[this.events.Length] = eventBuilder;
				this.events = array;
			}
			else
			{
				this.events = new EventBuilder[1];
				this.events[0] = eventBuilder;
			}
			return eventBuilder;
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x0010488F File Offset: 0x00102A8F
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			FieldBuilder fieldBuilder = this.DefineUninitializedData(name, data.Length, attributes);
			fieldBuilder.SetRVAData(data);
			return fieldBuilder;
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x001048B4 File Offset: 0x00102AB4
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal", "name");
			}
			if (size <= 0 || size > 4128768)
			{
				throw new ArgumentException("Data size must be > 0 and < 0x3f0000");
			}
			this.check_not_created();
			string text = "$ArrayType$" + size.ToString();
			TypeIdentifier typeIdentifier = TypeIdentifiers.WithoutEscape(text);
			Type type = this.pmodule.GetRegisteredType(this.fullname.NestedName(typeIdentifier));
			if (type == null)
			{
				TypeBuilder typeBuilder = this.DefineNestedType(text, TypeAttributes.Public | TypeAttributes.NestedPublic | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed, this.pmodule.assemblyb.corlib_value_type, null, PackingSize.Size1, size);
				typeBuilder.CreateType();
				type = typeBuilder;
			}
			return this.DefineField(name, type, attributes | FieldAttributes.Static | FieldAttributes.HasFieldRVA);
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06005248 RID: 21064 RVA: 0x00104977 File Offset: 0x00102B77
		public TypeToken TypeToken
		{
			get
			{
				return new TypeToken(33554432 | this.table_idx);
			}
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x0010498C File Offset: 0x00102B8C
		public void SetParent(Type parent)
		{
			this.check_not_created();
			if (parent == null)
			{
				if ((this.attrs & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
				{
					if ((this.attrs & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
					{
						throw new InvalidOperationException("Interface must be declared abstract.");
					}
					this.parent = null;
				}
				else
				{
					this.parent = typeof(object);
				}
			}
			else
			{
				this.parent = parent;
			}
			this.parent = TypeBuilder.ResolveUserType(this.parent);
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x001049FF File Offset: 0x00102BFF
		internal int get_next_table_index(object obj, int table, int count)
		{
			return this.pmodule.get_next_table_index(obj, table, count);
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x00104A0F File Offset: 0x00102C0F
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			if (this.created == null)
			{
				throw new NotSupportedException("This method is not implemented for incomplete types.");
			}
			return this.created.GetInterfaceMap(interfaceType);
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x00104A36 File Offset: 0x00102C36
		internal override Type InternalResolve()
		{
			this.check_created();
			return this.created;
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x00104A36 File Offset: 0x00102C36
		internal override Type RuntimeResolve()
		{
			this.check_created();
			return this.created;
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600524E RID: 21070 RVA: 0x00104A44 File Offset: 0x00102C44
		internal bool is_created
		{
			get
			{
				return this.createTypeCalled;
			}
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x000F73F6 File Offset: 0x000F55F6
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x00104A4C File Offset: 0x00102C4C
		private void check_not_created()
		{
			if (this.is_created)
			{
				throw new InvalidOperationException("Unable to change after type has been created.");
			}
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x00104A61 File Offset: 0x00102C61
		private void check_created()
		{
			if (!this.is_created)
			{
				throw this.not_supported();
			}
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x00104A72 File Offset: 0x00102C72
		private void check_name(string argName, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(argName);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal", argName);
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException("Illegal name", argName);
			}
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x00104AA7 File Offset: 0x00102CA7
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x00104AAF File Offset: 0x00102CAF
		[MonoTODO]
		public override bool IsAssignableFrom(Type c)
		{
			return base.IsAssignableFrom(c);
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x00104AB8 File Offset: 0x00102CB8
		[MonoTODO("arrays")]
		internal bool IsAssignableTo(Type c)
		{
			if (c == this)
			{
				return true;
			}
			if (c.IsInterface)
			{
				if (this.parent != null && this.is_created && c.IsAssignableFrom(this.parent))
				{
					return true;
				}
				if (this.interfaces == null)
				{
					return false;
				}
				foreach (Type type in this.interfaces)
				{
					if (c.IsAssignableFrom(type))
					{
						return true;
					}
				}
				if (!this.is_created)
				{
					return false;
				}
			}
			if (this.parent == null)
			{
				return c == typeof(object);
			}
			return c.IsAssignableFrom(this.parent);
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x00104B61 File Offset: 0x00102D61
		public bool IsCreated()
		{
			return this.is_created;
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x00104B6C File Offset: 0x00102D6C
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return null;
			}
			Type[] array = new Type[this.generic_params.Length];
			this.generic_params.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x00104B9F File Offset: 0x00102D9F
		public override Type GetGenericTypeDefinition()
		{
			if (this.generic_params == null)
			{
				throw new InvalidOperationException("Type is not generic");
			}
			return this;
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06005259 RID: 21081 RVA: 0x00104BB5 File Offset: 0x00102DB5
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.generic_params != null;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x0600525A RID: 21082 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600525B RID: 21083 RVA: 0x0000408A File Offset: 0x0000228A
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return GenericParameterAttributes.None;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600525C RID: 21084 RVA: 0x00104BB5 File Offset: 0x00102DB5
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x0600525D RID: 21085 RVA: 0x00104BC0 File Offset: 0x00102DC0
		public override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x0600525E RID: 21086 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO]
		public override int GenericParameterPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x0600525F RID: 21087 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x00104BC8 File Offset: 0x00102DC8
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (names.Length == 0)
			{
				throw new ArgumentException("names");
			}
			this.generic_params = new GenericTypeParameterBuilder[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				if (text == null)
				{
					throw new ArgumentNullException("names");
				}
				this.generic_params[i] = new GenericTypeParameterBuilder(this, null, text, i);
			}
			return this.generic_params;
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x00104C38 File Offset: 0x00102E38
		public static ConstructorInfo GetConstructor(Type type, ConstructorInfo constructor)
		{
			if (type == null)
			{
				throw new ArgumentException("Type is not generic", "type");
			}
			if (!type.IsGenericType)
			{
				throw new ArgumentException("Type is not a generic type", "type");
			}
			if (type.IsGenericTypeDefinition)
			{
				throw new ArgumentException("Type cannot be a generic type definition", "type");
			}
			if (constructor == null)
			{
				throw new NullReferenceException();
			}
			if (!constructor.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException("constructor declaring type is not a generic type definition", "constructor");
			}
			if (constructor.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("constructor declaring type is not the generic type definition of type", "constructor");
			}
			ConstructorInfo constructor2 = type.GetConstructor(constructor);
			if (constructor2 == null)
			{
				throw new ArgumentException("constructor not found");
			}
			return constructor2;
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x00104CF8 File Offset: 0x00102EF8
		private static bool IsValidGetMethodType(Type type)
		{
			if (type is TypeBuilder || type is TypeBuilderInstantiation)
			{
				return true;
			}
			if (type.Module is ModuleBuilder)
			{
				return true;
			}
			if (type.IsGenericParameter)
			{
				return false;
			}
			Type[] genericArguments = type.GetGenericArguments();
			if (genericArguments == null)
			{
				return false;
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (TypeBuilder.IsValidGetMethodType(genericArguments[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x00104D58 File Offset: 0x00102F58
		public static MethodInfo GetMethod(Type type, MethodInfo method)
		{
			if (!TypeBuilder.IsValidGetMethodType(type))
			{
				string text = "type is not TypeBuilder but ";
				Type type2 = type.GetType();
				throw new ArgumentException(text + ((type2 != null) ? type2.ToString() : null), "type");
			}
			if (type is TypeBuilder && type.ContainsGenericParameters)
			{
				type = type.MakeGenericType(type.GetGenericArguments());
			}
			if (!type.IsGenericType)
			{
				throw new ArgumentException("type is not a generic type", "type");
			}
			if (!method.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException("method declaring type is not a generic type definition", "method");
			}
			if (method.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("method declaring type is not the generic type definition of type", "method");
			}
			if (method == null)
			{
				throw new NullReferenceException();
			}
			MethodInfo method2 = type.GetMethod(method);
			if (method2 == null)
			{
				throw new ArgumentException(string.Format("method {0} not found in type {1}", method.Name, type));
			}
			return method2;
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x00104E40 File Offset: 0x00103040
		public static FieldInfo GetField(Type type, FieldInfo field)
		{
			if (!type.IsGenericType)
			{
				throw new ArgumentException("Type is not a generic type", "type");
			}
			if (type.IsGenericTypeDefinition)
			{
				throw new ArgumentException("Type cannot be a generic type definition", "type");
			}
			if (field is FieldOnTypeBuilderInst)
			{
				throw new ArgumentException("The specified field must be declared on a generic type definition.", "field");
			}
			if (field.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("field declaring type is not the generic type definition of type", "method");
			}
			FieldInfo field2 = type.GetField(field);
			if (field2 == null)
			{
				throw new Exception("field not found");
			}
			return field2;
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06005265 RID: 21093 RVA: 0x0000408A File Offset: 0x0000228A
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06005266 RID: 21094 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x000FA055 File Offset: 0x000F8255
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return base.IsAssignableFrom(typeInfo);
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x00104ED8 File Offset: 0x001030D8
		internal static bool SetConstantValue(Type destType, object value, ref object destValue)
		{
			if (value != null)
			{
				Type type = value.GetType();
				if (destType.IsByRef)
				{
					destType = destType.GetElementType();
				}
				destType = Nullable.GetUnderlyingType(destType) ?? destType;
				if (destType.IsEnum)
				{
					EnumBuilder enumBuilder;
					Type type2;
					TypeBuilder typeBuilder;
					if ((enumBuilder = destType as EnumBuilder) != null)
					{
						type2 = enumBuilder.GetEnumUnderlyingType();
						if ((!enumBuilder.GetTypeBuilder().is_created || !(type == enumBuilder.GetTypeBuilder().created)) && !(type == type2))
						{
							TypeBuilder.throw_argument_ConstantDoesntMatch();
						}
					}
					else if ((typeBuilder = destType as TypeBuilder) != null)
					{
						type2 = typeBuilder.underlying_type;
						if (type2 == null || (type != typeBuilder.UnderlyingSystemType && type != type2))
						{
							TypeBuilder.throw_argument_ConstantDoesntMatch();
						}
					}
					else
					{
						type2 = Enum.GetUnderlyingType(destType);
						if (type != destType && type != type2)
						{
							TypeBuilder.throw_argument_ConstantDoesntMatch();
						}
					}
					type = type2;
				}
				else if (!destType.IsAssignableFrom(type))
				{
					TypeBuilder.throw_argument_ConstantDoesntMatch();
				}
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
				case TypeCode.Char:
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
					destValue = value;
					return true;
				case TypeCode.DateTime:
				{
					long ticks = ((DateTime)value).Ticks;
					destValue = ticks;
					return true;
				}
				case TypeCode.String:
					destValue = value;
					return true;
				}
				throw new ArgumentException(type.ToString() + " is not a supported constant type.");
			}
			destValue = null;
			return true;
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x00105063 File Offset: 0x00103263
		private static void throw_argument_ConstantDoesntMatch()
		{
			throw new ArgumentException("Constant does not match the defined type.");
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x0600526A RID: 21098 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override bool IsTypeDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400329A RID: 12954
		private string tname;

		// Token: 0x0400329B RID: 12955
		private string nspace;

		// Token: 0x0400329C RID: 12956
		private Type parent;

		// Token: 0x0400329D RID: 12957
		private Type nesting_type;

		// Token: 0x0400329E RID: 12958
		internal Type[] interfaces;

		// Token: 0x0400329F RID: 12959
		internal int num_methods;

		// Token: 0x040032A0 RID: 12960
		internal MethodBuilder[] methods;

		// Token: 0x040032A1 RID: 12961
		internal ConstructorBuilder[] ctors;

		// Token: 0x040032A2 RID: 12962
		internal PropertyBuilder[] properties;

		// Token: 0x040032A3 RID: 12963
		internal int num_fields;

		// Token: 0x040032A4 RID: 12964
		internal FieldBuilder[] fields;

		// Token: 0x040032A5 RID: 12965
		internal EventBuilder[] events;

		// Token: 0x040032A6 RID: 12966
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040032A7 RID: 12967
		internal TypeBuilder[] subtypes;

		// Token: 0x040032A8 RID: 12968
		internal TypeAttributes attrs;

		// Token: 0x040032A9 RID: 12969
		private int table_idx;

		// Token: 0x040032AA RID: 12970
		private ModuleBuilder pmodule;

		// Token: 0x040032AB RID: 12971
		private int class_size;

		// Token: 0x040032AC RID: 12972
		private PackingSize packing_size;

		// Token: 0x040032AD RID: 12973
		private IntPtr generic_container;

		// Token: 0x040032AE RID: 12974
		private GenericTypeParameterBuilder[] generic_params;

		// Token: 0x040032AF RID: 12975
		private RefEmitPermissionSet[] permissions;

		// Token: 0x040032B0 RID: 12976
		private TypeInfo created;

		// Token: 0x040032B1 RID: 12977
		private int state;

		// Token: 0x040032B2 RID: 12978
		private TypeName fullname;

		// Token: 0x040032B3 RID: 12979
		private bool createTypeCalled;

		// Token: 0x040032B4 RID: 12980
		private Type underlying_type;

		// Token: 0x040032B5 RID: 12981
		public const int UnspecifiedTypeSize = 0;
	}
}
