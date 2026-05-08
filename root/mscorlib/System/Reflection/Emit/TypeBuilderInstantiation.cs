using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000918 RID: 2328
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class TypeBuilderInstantiation : TypeInfo
	{
		// Token: 0x0600526B RID: 21099 RVA: 0x0010506F File Offset: 0x0010326F
		internal TypeBuilderInstantiation()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x0010507C File Offset: 0x0010327C
		internal TypeBuilderInstantiation(Type tb, Type[] args)
		{
			this.generic_type = tb;
			this.type_arguments = args;
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x00105094 File Offset: 0x00103294
		internal override Type InternalResolve()
		{
			Type type = this.generic_type.InternalResolve();
			Type[] array = new Type[this.type_arguments.Length];
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				array[i] = this.type_arguments[i].InternalResolve();
			}
			return type.MakeGenericType(array);
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x001050E8 File Offset: 0x001032E8
		internal override Type RuntimeResolve()
		{
			TypeBuilder typeBuilder = this.generic_type as TypeBuilder;
			if (typeBuilder != null && !typeBuilder.IsCreated())
			{
				AppDomain.CurrentDomain.DoTypeBuilderResolve(typeBuilder);
			}
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				TypeBuilder typeBuilder2 = this.type_arguments[i] as TypeBuilder;
				if (typeBuilder2 != null && !typeBuilder2.IsCreated())
				{
					AppDomain.CurrentDomain.DoTypeBuilderResolve(typeBuilder2);
				}
			}
			return this.InternalResolve();
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x0600526F RID: 21103 RVA: 0x00105158 File Offset: 0x00103358
		internal bool IsCreated
		{
			get
			{
				TypeBuilder typeBuilder = this.generic_type as TypeBuilder;
				return !(typeBuilder != null) || typeBuilder.is_created;
			}
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x00105182 File Offset: 0x00103382
		private Type GetParentType()
		{
			return this.InflateType(this.generic_type.BaseType);
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00105195 File Offset: 0x00103395
		internal Type InflateType(Type type)
		{
			return TypeBuilderInstantiation.InflateType(type, this.type_arguments, null);
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x001051A4 File Offset: 0x001033A4
		internal Type InflateType(Type type, Type[] method_args)
		{
			return TypeBuilderInstantiation.InflateType(type, this.type_arguments, method_args);
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x001051B4 File Offset: 0x001033B4
		internal static Type InflateType(Type type, Type[] type_args, Type[] method_args)
		{
			if (type == null)
			{
				return null;
			}
			if (!type.IsGenericParameter && !type.ContainsGenericParameters)
			{
				return type;
			}
			if (type.IsGenericParameter)
			{
				if (type.DeclaringMethod == null)
				{
					if (type_args != null)
					{
						return type_args[type.GenericParameterPosition];
					}
					return type;
				}
				else
				{
					if (method_args != null)
					{
						return method_args[type.GenericParameterPosition];
					}
					return type;
				}
			}
			else
			{
				if (type.IsPointer)
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakePointerType();
				}
				if (type.IsByRef)
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeByRefType();
				}
				if (!type.IsArray)
				{
					Type[] genericArguments = type.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						genericArguments[i] = TypeBuilderInstantiation.InflateType(genericArguments[i], type_args, method_args);
					}
					return (type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition()).MakeGenericType(genericArguments);
				}
				if (type.GetArrayRank() > 1)
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeArrayType(type.GetArrayRank());
				}
				if (type.ToString().EndsWith("[*]", StringComparison.Ordinal))
				{
					return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeArrayType(1);
				}
				return TypeBuilderInstantiation.InflateType(type.GetElementType(), type_args, method_args).MakeArrayType();
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06005274 RID: 21108 RVA: 0x001052E3 File Offset: 0x001034E3
		public override Type BaseType
		{
			get
			{
				return this.generic_type.BaseType;
			}
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x00047E00 File Offset: 0x00046000
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x001052F0 File Offset: 0x001034F0
		protected override bool IsValueTypeImpl()
		{
			return this.generic_type.IsValueType;
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00105300 File Offset: 0x00103500
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			if (this.methods == null)
			{
				this.methods = new Hashtable();
			}
			if (!this.methods.ContainsKey(fromNoninstanciated))
			{
				this.methods[fromNoninstanciated] = new MethodOnTypeBuilderInst(this, fromNoninstanciated);
			}
			return (MethodInfo)this.methods[fromNoninstanciated];
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x00105354 File Offset: 0x00103554
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			if (this.ctors == null)
			{
				this.ctors = new Hashtable();
			}
			if (!this.ctors.ContainsKey(fromNoninstanciated))
			{
				this.ctors[fromNoninstanciated] = new ConstructorOnTypeBuilderInst(this, fromNoninstanciated);
			}
			return (ConstructorInfo)this.ctors[fromNoninstanciated];
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x001053A8 File Offset: 0x001035A8
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			if (this.fields == null)
			{
				this.fields = new Hashtable();
			}
			if (!this.fields.ContainsKey(fromNoninstanciated))
			{
				this.fields[fromNoninstanciated] = new FieldOnTypeBuilderInst(this, fromNoninstanciated);
			}
			return (FieldInfo)this.fields[fromNoninstanciated];
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00047E00 File Offset: 0x00046000
		public override MethodInfo[] GetMethods(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x00047E00 File Offset: 0x00046000
		public override ConstructorInfo[] GetConstructors(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00047E00 File Offset: 0x00046000
		public override FieldInfo[] GetFields(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x00047E00 File Offset: 0x00046000
		public override PropertyInfo[] GetProperties(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x00047E00 File Offset: 0x00046000
		public override EventInfo[] GetEvents(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x00047E00 File Offset: 0x00046000
		public override Type[] GetNestedTypes(BindingFlags bf)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x00047E00 File Offset: 0x00046000
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06005281 RID: 21121 RVA: 0x000025CE File Offset: 0x000007CE
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06005282 RID: 21122 RVA: 0x001053FA File Offset: 0x001035FA
		public override Assembly Assembly
		{
			get
			{
				return this.generic_type.Assembly;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06005283 RID: 21123 RVA: 0x00105407 File Offset: 0x00103607
		public override Module Module
		{
			get
			{
				return this.generic_type.Module;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06005284 RID: 21124 RVA: 0x00105414 File Offset: 0x00103614
		public override string Name
		{
			get
			{
				return this.generic_type.Name;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06005285 RID: 21125 RVA: 0x00105421 File Offset: 0x00103621
		public override string Namespace
		{
			get
			{
				return this.generic_type.Namespace;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06005286 RID: 21126 RVA: 0x0010542E File Offset: 0x0010362E
		public override string FullName
		{
			get
			{
				return this.format_name(true, false);
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06005287 RID: 21127 RVA: 0x00105438 File Offset: 0x00103638
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.format_name(true, true);
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06005288 RID: 21128 RVA: 0x00047E00 File Offset: 0x00046000
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00105444 File Offset: 0x00103644
		private string format_name(bool full_name, bool assembly_qualified)
		{
			StringBuilder stringBuilder = new StringBuilder(this.generic_type.FullName);
			stringBuilder.Append("[");
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				string text;
				if (full_name)
				{
					string fullName = this.type_arguments[i].Assembly.FullName;
					text = this.type_arguments[i].FullName;
					if (text != null && fullName != null)
					{
						text = text + ", " + fullName;
					}
				}
				else
				{
					text = this.type_arguments[i].ToString();
				}
				if (text == null)
				{
					return null;
				}
				if (full_name)
				{
					stringBuilder.Append("[");
				}
				stringBuilder.Append(text);
				if (full_name)
				{
					stringBuilder.Append("]");
				}
			}
			stringBuilder.Append("]");
			if (assembly_qualified)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.generic_type.Assembly.FullName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x00105541 File Offset: 0x00103741
		public override string ToString()
		{
			return this.format_name(false, false);
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x0010554B File Offset: 0x0010374B
		public override Type GetGenericTypeDefinition()
		{
			return this.generic_type;
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x00105554 File Offset: 0x00103754
		public override Type[] GetGenericArguments()
		{
			Type[] array = new Type[this.type_arguments.Length];
			this.type_arguments.CopyTo(array, 0);
			return array;
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600528D RID: 21133 RVA: 0x00105580 File Offset: 0x00103780
		public override bool ContainsGenericParameters
		{
			get
			{
				Type[] array = this.type_arguments;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600528F RID: 21135 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override bool IsGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06005290 RID: 21136 RVA: 0x001055AF File Offset: 0x001037AF
		public override Type DeclaringType
		{
			get
			{
				return this.generic_type.DeclaringType;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06005291 RID: 21137 RVA: 0x00047E00 File Offset: 0x00046000
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x000F5F5D File Offset: 0x000F415D
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x000F5F66 File Offset: 0x000F4166
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x000F5F79 File Offset: 0x000F4179
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x000F5F81 File Offset: 0x000F4181
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x00047E00 File Offset: 0x00046000
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x001055BC File Offset: 0x001037BC
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.generic_type.Attributes;
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00047E00 File Offset: 0x00046000
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x00047E00 File Offset: 0x00046000
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x00047E00 File Offset: 0x00046000
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x00047E00 File Offset: 0x00046000
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x00047E00 File Offset: 0x00046000
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x00047E00 File Offset: 0x00046000
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x00047E00 File Offset: 0x00046000
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x00047E00 File Offset: 0x00046000
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x00047E00 File Offset: 0x00046000
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x00047E00 File Offset: 0x00046000
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x001055C9 File Offset: 0x001037C9
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.IsCreated)
			{
				return this.generic_type.GetCustomAttributes(inherit);
			}
			throw new NotSupportedException();
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x001055E5 File Offset: 0x001037E5
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.IsCreated)
			{
				return this.generic_type.GetCustomAttributes(attributeType, inherit);
			}
			throw new NotSupportedException();
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x00105604 File Offset: 0x00103804
		internal override bool IsUserType
		{
			get
			{
				Type[] array = this.type_arguments;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IsUserType)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x00105633 File Offset: 0x00103833
		internal static Type MakeGenericType(Type type, Type[] typeArguments)
		{
			return new TypeBuilderInstantiation(type, typeArguments);
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060052AD RID: 21165 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040032B6 RID: 12982
		internal Type generic_type;

		// Token: 0x040032B7 RID: 12983
		private Type[] type_arguments;

		// Token: 0x040032B8 RID: 12984
		private Hashtable fields;

		// Token: 0x040032B9 RID: 12985
		private Hashtable ctors;

		// Token: 0x040032BA RID: 12986
		private Hashtable methods;

		// Token: 0x040032BB RID: 12987
		private const BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
