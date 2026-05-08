using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008DD RID: 2269
	[StructLayout(LayoutKind.Sequential)]
	internal abstract class SymbolType : TypeInfo
	{
		// Token: 0x06004DC6 RID: 19910 RVA: 0x00058961 File Offset: 0x00056B61
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
			}
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x000F5E8C File Offset: 0x000F408C
		public override Module Module
		{
			get
			{
				Type type = this.m_baseType;
				while (type is SymbolType)
				{
					type = ((SymbolType)type).m_baseType;
				}
				return type.Module;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x000F5EBC File Offset: 0x000F40BC
		public override Assembly Assembly
		{
			get
			{
				Type type = this.m_baseType;
				while (type is SymbolType)
				{
					type = ((SymbolType)type).m_baseType;
				}
				return type.Assembly;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x000F5EEC File Offset: 0x000F40EC
		public override string Namespace
		{
			get
			{
				return this.m_baseType.Namespace;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x000F5EF9 File Offset: 0x000F40F9
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x000F5E78 File Offset: 0x000F4078
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x000F5E78 File Offset: 0x000F4078
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x000F5E78 File Offset: 0x000F4078
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x000F5E78 File Offset: 0x000F4078
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x000F5E78 File Offset: 0x000F4078
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x000F5F08 File Offset: 0x000F4108
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			Type type = this.m_baseType;
			while (type is SymbolType)
			{
				type = ((SymbolType)type).m_baseType;
			}
			return type.Attributes;
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x000F5F38 File Offset: 0x000F4138
		public override Type GetElementType()
		{
			return this.m_baseType;
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x000F5F40 File Offset: 0x000F4140
		protected override bool HasElementTypeImpl()
		{
			return this.m_baseType != null;
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x000F5E78 File Offset: 0x000F4078
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x000F5F4E File Offset: 0x000F414E
		internal SymbolType(Type elementType)
		{
			this.m_baseType = elementType;
		}

		// Token: 0x06004DEB RID: 19947
		internal abstract string FormatName(string elementName);

		// Token: 0x06004DEC RID: 19948 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x000F5F5D File Offset: 0x000F415D
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x000F5F66 File Offset: 0x000F4166
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x000F5F79 File Offset: 0x000F4179
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x000F5F81 File Offset: 0x000F4181
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x000F5F89 File Offset: 0x000F4189
		public override string ToString()
		{
			return this.FormatName(this.m_baseType.ToString());
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004DF4 RID: 19956 RVA: 0x000F5F9C File Offset: 0x000F419C
		public override string AssemblyQualifiedName
		{
			get
			{
				string text = this.FormatName(this.m_baseType.FullName);
				if (text == null)
				{
					return null;
				}
				return text + ", " + this.m_baseType.Assembly.FullName;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004DF5 RID: 19957 RVA: 0x000F5FDB File Offset: 0x000F41DB
		public override string FullName
		{
			get
			{
				return this.FormatName(this.m_baseType.FullName);
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x000F5FEE File Offset: 0x000F41EE
		public override string Name
		{
			get
			{
				return this.FormatName(this.m_baseType.Name);
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x000025CE File Offset: 0x000007CE
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004DF8 RID: 19960 RVA: 0x000F6001 File Offset: 0x000F4201
		internal override bool IsUserType
		{
			get
			{
				return this.m_baseType.IsUserType;
			}
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x000F600E File Offset: 0x000F420E
		internal override Type RuntimeResolve()
		{
			return this.InternalResolve();
		}

		// Token: 0x0400304F RID: 12367
		internal Type m_baseType;
	}
}
