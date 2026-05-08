using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008F0 RID: 2288
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_EnumBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class EnumBuilder : TypeInfo, _EnumBuilder
	{
		// Token: 0x06004F0B RID: 20235 RVA: 0x000174FB File Offset: 0x000156FB
		void _EnumBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x000174FB File Offset: 0x000156FB
		void _EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x000174FB File Offset: 0x000156FB
		void _EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x000174FB File Offset: 0x000156FB
		void _EnumBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x000F9D30 File Offset: 0x000F7F30
		internal EnumBuilder(ModuleBuilder mb, string name, TypeAttributes visibility, Type underlyingType)
		{
			this._tb = new TypeBuilder(mb, name, visibility | TypeAttributes.Sealed, typeof(Enum), null, PackingSize.Unspecified, 0, null);
			this._underlyingType = underlyingType;
			this._underlyingField = this._tb.DefineField("value__", underlyingType, FieldAttributes.Private | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
			this.setup_enum_type(this._tb);
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x000F9D96 File Offset: 0x000F7F96
		internal TypeBuilder GetTypeBuilder()
		{
			return this._tb;
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x000F9D9E File Offset: 0x000F7F9E
		internal override Type InternalResolve()
		{
			return this._tb.InternalResolve();
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x000F9DAB File Offset: 0x000F7FAB
		internal override Type RuntimeResolve()
		{
			return this._tb.RuntimeResolve();
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004F13 RID: 20243 RVA: 0x000F9DB8 File Offset: 0x000F7FB8
		public override Assembly Assembly
		{
			get
			{
				return this._tb.Assembly;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06004F14 RID: 20244 RVA: 0x000F9DC5 File Offset: 0x000F7FC5
		public override string AssemblyQualifiedName
		{
			get
			{
				return this._tb.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x000F9DD2 File Offset: 0x000F7FD2
		public override Type BaseType
		{
			get
			{
				return this._tb.BaseType;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x000F9DDF File Offset: 0x000F7FDF
		public override Type DeclaringType
		{
			get
			{
				return this._tb.DeclaringType;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x000F9DEC File Offset: 0x000F7FEC
		public override string FullName
		{
			get
			{
				return this._tb.FullName;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x000F9DF9 File Offset: 0x000F7FF9
		public override Guid GUID
		{
			get
			{
				return this._tb.GUID;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06004F19 RID: 20249 RVA: 0x000F9E06 File Offset: 0x000F8006
		public override Module Module
		{
			get
			{
				return this._tb.Module;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x000F9E13 File Offset: 0x000F8013
		public override string Name
		{
			get
			{
				return this._tb.Name;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004F1B RID: 20251 RVA: 0x000F9E20 File Offset: 0x000F8020
		public override string Namespace
		{
			get
			{
				return this._tb.Namespace;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x000F9E2D File Offset: 0x000F802D
		public override Type ReflectedType
		{
			get
			{
				return this._tb.ReflectedType;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06004F1D RID: 20253 RVA: 0x000F9E3A File Offset: 0x000F803A
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this._tb.TypeHandle;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x000F9E47 File Offset: 0x000F8047
		public TypeToken TypeToken
		{
			get
			{
				return this._tb.TypeToken;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x000F9E54 File Offset: 0x000F8054
		public FieldBuilder UnderlyingField
		{
			get
			{
				return this._underlyingField;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x000F9E5C File Offset: 0x000F805C
		public override Type UnderlyingSystemType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x000F9E64 File Offset: 0x000F8064
		public Type CreateType()
		{
			return this._tb.CreateType();
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x000F9E71 File Offset: 0x000F8071
		public TypeInfo CreateTypeInfo()
		{
			return this._tb.CreateTypeInfo();
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x000F9E5C File Offset: 0x000F805C
		public override Type GetEnumUnderlyingType()
		{
			return this._underlyingType;
		}

		// Token: 0x06004F24 RID: 20260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void setup_enum_type(Type t);

		// Token: 0x06004F25 RID: 20261 RVA: 0x000F9E80 File Offset: 0x000F8080
		public FieldBuilder DefineLiteral(string literalName, object literalValue)
		{
			FieldBuilder fieldBuilder = this._tb.DefineField(literalName, this, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			fieldBuilder.SetConstant(literalValue);
			return fieldBuilder;
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x000F9EA5 File Offset: 0x000F80A5
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this._tb.attrs;
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x000F9EB2 File Offset: 0x000F80B2
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this._tb.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x000F9EC6 File Offset: 0x000F80C6
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this._tb.GetConstructors(bindingAttr);
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x000F9ED4 File Offset: 0x000F80D4
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._tb.GetCustomAttributes(inherit);
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x000F9EE2 File Offset: 0x000F80E2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._tb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x000F9EF1 File Offset: 0x000F80F1
		public override Type GetElementType()
		{
			return this._tb.GetElementType();
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x000F9EFE File Offset: 0x000F80FE
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x000F9F0D File Offset: 0x000F810D
		public override EventInfo[] GetEvents()
		{
			return this._tb.GetEvents();
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x000F9F1A File Offset: 0x000F811A
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this._tb.GetEvents(bindingAttr);
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x000F9F28 File Offset: 0x000F8128
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetField(name, bindingAttr);
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x000F9F37 File Offset: 0x000F8137
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this._tb.GetFields(bindingAttr);
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x000F9F45 File Offset: 0x000F8145
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this._tb.GetInterface(name, ignoreCase);
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x000F9F54 File Offset: 0x000F8154
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this._tb.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x000F9F62 File Offset: 0x000F8162
		public override Type[] GetInterfaces()
		{
			return this._tb.GetInterfaces();
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x000F9F6F File Offset: 0x000F816F
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this._tb.GetMember(name, type, bindingAttr);
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x000F9F7F File Offset: 0x000F817F
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this._tb.GetMembers(bindingAttr);
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x000F9F8D File Offset: 0x000F818D
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this._tb.GetMethod(name, bindingAttr);
			}
			return this._tb.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x000F9FB5 File Offset: 0x000F81B5
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this._tb.GetMethods(bindingAttr);
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x000F9FC3 File Offset: 0x000F81C3
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x000F9FD2 File Offset: 0x000F81D2
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this._tb.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x000F9FE0 File Offset: 0x000F81E0
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this._tb.GetProperties(bindingAttr);
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x000F9FEE File Offset: 0x000F81EE
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x000F9FF6 File Offset: 0x000F81F6
		protected override bool HasElementTypeImpl()
		{
			return this._tb.HasElementType;
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x000FA004 File Offset: 0x000F8204
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this._tb.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x0000408A File Offset: 0x0000228A
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x000FA029 File Offset: 0x000F8229
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._tb.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x000F5F5D File Offset: 0x000F415D
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x000F5F66 File Offset: 0x000F4166
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x000F5F79 File Offset: 0x000F4179
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x000F5F81 File Offset: 0x000F4181
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x000FA038 File Offset: 0x000F8238
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this._tb.SetCustomAttribute(customBuilder);
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x000FA046 File Offset: 0x000F8246
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x000F73F6 File Offset: 0x000F55F6
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x0000408A File Offset: 0x0000228A
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004F4D RID: 20301 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x000FA055 File Offset: 0x000F8255
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return base.IsAssignableFrom(typeInfo);
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06004F4F RID: 20303 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override bool IsTypeDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040030CA RID: 12490
		private TypeBuilder _tb;

		// Token: 0x040030CB RID: 12491
		private FieldBuilder _underlyingField;

		// Token: 0x040030CC RID: 12492
		private Type _underlyingType;
	}
}
