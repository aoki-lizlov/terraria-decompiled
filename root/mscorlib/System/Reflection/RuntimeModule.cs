using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020008CA RID: 2250
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Module))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeModule : Module
	{
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06004D1B RID: 19739 RVA: 0x000F45C8 File Offset: 0x000F27C8
		public override Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004D1C RID: 19740 RVA: 0x000F45D0 File Offset: 0x000F27D0
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004D1D RID: 19741 RVA: 0x000F45D8 File Offset: 0x000F27D8
		public override string ScopeName
		{
			get
			{
				return this.scopename;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x000F45E0 File Offset: 0x000F27E0
		public override int MDStreamVersion
		{
			get
			{
				if (this._impl == IntPtr.Zero)
				{
					throw new NotSupportedException();
				}
				return RuntimeModule.GetMDStreamVersion(this._impl);
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004D1F RID: 19743 RVA: 0x000EEB78 File Offset: 0x000ECD78
		public override Guid ModuleVersionId
		{
			get
			{
				return this.GetModuleVersionId();
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06004D20 RID: 19744 RVA: 0x000F4605 File Offset: 0x000F2805
		public override string FullyQualifiedName
		{
			get
			{
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fqname).Demand();
				}
				return this.fqname;
			}
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x000F4625 File Offset: 0x000F2825
		public override bool IsResource()
		{
			return this.is_resource;
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x000F4630 File Offset: 0x000F2830
		public override Type[] FindTypes(TypeFilter filter, object filterCriteria)
		{
			List<Type> list = new List<Type>();
			foreach (Type type in this.GetTypes())
			{
				if (filter(type, filterCriteria))
				{
					list.Add(type);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x000F133D File Offset: 0x000EF53D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x000F1346 File Offset: 0x000EF546
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x000F4674 File Offset: 0x000F2874
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = RuntimeModule.GetGlobalType(this._impl);
			if (!(globalType != null))
			{
				return null;
			}
			return globalType.GetField(name, bindingAttr);
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x000F46B8 File Offset: 0x000F28B8
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (this.IsResource())
			{
				return new FieldInfo[0];
			}
			Type globalType = RuntimeModule.GetGlobalType(this._impl);
			if (!(globalType != null))
			{
				return new FieldInfo[0];
			}
			return globalType.GetFields(bindingFlags);
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06004D27 RID: 19751 RVA: 0x000F46F7 File Offset: 0x000F28F7
		public override int MetadataToken
		{
			get
			{
				return RuntimeModule.get_MetadataToken(this);
			}
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x000F4700 File Offset: 0x000F2900
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = RuntimeModule.GetGlobalType(this._impl);
			if (globalType == null)
			{
				return null;
			}
			if (types == null)
			{
				return globalType.GetMethod(name);
			}
			return globalType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x000F474C File Offset: 0x000F294C
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (this.IsResource())
			{
				return new MethodInfo[0];
			}
			Type globalType = RuntimeModule.GetGlobalType(this._impl);
			if (!(globalType != null))
			{
				return new MethodInfo[0];
			}
			return globalType.GetMethods(bindingFlags);
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x000F478B File Offset: 0x000F298B
		internal override ModuleHandle GetModuleHandleImpl()
		{
			return new ModuleHandle(this._impl);
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x000F4798 File Offset: 0x000F2998
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			RuntimeModule.GetPEKind(this._impl, out peKind, out machine);
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x000F47A7 File Offset: 0x000F29A7
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			if (className == string.Empty)
			{
				throw new ArgumentException("Type name can't be empty");
			}
			return this.assembly.InternalGetType(this, className, throwOnError, ignoreCase);
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x000534DE File Offset: 0x000516DE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x000F47DE File Offset: 0x000F29DE
		public override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveField(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x000F47F0 File Offset: 0x000F29F0
		internal static FieldInfo ResolveField(Module module, IntPtr monoModule, int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveFieldToken(monoModule, metadataToken, RuntimeModule.ptrs_from_types(genericTypeArguments), RuntimeModule.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "Field");
			}
			return FieldInfo.GetFieldFromHandle(new RuntimeFieldHandle(intPtr));
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x000F483F File Offset: 0x000F2A3F
		public override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveMember(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x000F4850 File Offset: 0x000F2A50
		internal static MemberInfo ResolveMember(Module module, IntPtr monoModule, int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			MemberInfo memberInfo = RuntimeModule.ResolveMemberToken(monoModule, metadataToken, RuntimeModule.ptrs_from_types(genericTypeArguments), RuntimeModule.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (memberInfo == null)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "MemberInfo");
			}
			return memberInfo;
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x000F4891 File Offset: 0x000F2A91
		public override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveMethod(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x000F48A4 File Offset: 0x000F2AA4
		internal static MethodBase ResolveMethod(Module module, IntPtr monoModule, int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveMethodToken(monoModule, metadataToken, RuntimeModule.ptrs_from_types(genericTypeArguments), RuntimeModule.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "MethodBase");
			}
			return RuntimeMethodInfo.GetMethodFromHandleNoGenericCheck(new RuntimeMethodHandle(intPtr));
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x000F48F3 File Offset: 0x000F2AF3
		public override string ResolveString(int metadataToken)
		{
			return RuntimeModule.ResolveString(this, this._impl, metadataToken);
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x000F4904 File Offset: 0x000F2B04
		internal static string ResolveString(Module module, IntPtr monoModule, int metadataToken)
		{
			ResolveTokenError resolveTokenError;
			string text = RuntimeModule.ResolveStringToken(monoModule, metadataToken, out resolveTokenError);
			if (text == null)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "string");
			}
			return text;
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x000F4932 File Offset: 0x000F2B32
		public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveType(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x000F4944 File Offset: 0x000F2B44
		internal static Type ResolveType(Module module, IntPtr monoModule, int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveTypeToken(monoModule, metadataToken, RuntimeModule.ptrs_from_types(genericTypeArguments), RuntimeModule.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "Type");
			}
			return Type.GetTypeFromHandle(new RuntimeTypeHandle(intPtr));
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x000F4993 File Offset: 0x000F2B93
		public override byte[] ResolveSignature(int metadataToken)
		{
			return RuntimeModule.ResolveSignature(this, this._impl, metadataToken);
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x000F49A4 File Offset: 0x000F2BA4
		internal static byte[] ResolveSignature(Module module, IntPtr monoModule, int metadataToken)
		{
			ResolveTokenError resolveTokenError;
			byte[] array = RuntimeModule.ResolveSignature(monoModule, metadataToken, out resolveTokenError);
			if (array == null)
			{
				throw RuntimeModule.resolve_token_exception(module.Name, metadataToken, resolveTokenError, "signature");
			}
			return array;
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x000F49D2 File Offset: 0x000F2BD2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 5, this.ScopeName, this.GetRuntimeAssembly());
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x000F49F8 File Offset: 0x000F2BF8
		public override X509Certificate GetSignerCertificate()
		{
			X509Certificate x509Certificate;
			try
			{
				x509Certificate = X509Certificate.CreateFromSignedFile(this.assembly.Location);
			}
			catch
			{
				x509Certificate = null;
			}
			return x509Certificate;
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x000F4A30 File Offset: 0x000F2C30
		public override Type[] GetTypes()
		{
			return RuntimeModule.InternalGetTypes(this._impl);
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x000F4A3D File Offset: 0x000F2C3D
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x000F4A45 File Offset: 0x000F2C45
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return (RuntimeAssembly)this.assembly;
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06004D3F RID: 19775 RVA: 0x000F4A52 File Offset: 0x000F2C52
		internal IntPtr MonoModule
		{
			get
			{
				return this._impl;
			}
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x000F4A5C File Offset: 0x000F2C5C
		internal override Guid GetModuleVersionId()
		{
			byte[] array = new byte[16];
			RuntimeModule.GetGuidInternal(this._impl, array);
			return new Guid(array);
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x000F4A83 File Offset: 0x000F2C83
		internal static Exception resolve_token_exception(string name, int metadataToken, ResolveTokenError error, string tokenType)
		{
			if (error == ResolveTokenError.OutOfRange)
			{
				return new ArgumentOutOfRangeException("metadataToken", string.Format("Token 0x{0:x} is not valid in the scope of module {1}", metadataToken, name));
			}
			return new ArgumentException(string.Format("Token 0x{0:x} is not a valid {1} token in the scope of module {2}", metadataToken, tokenType, name), "metadataToken");
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x000F4AC0 File Offset: 0x000F2CC0
		internal static IntPtr[] ptrs_from_types(Type[] types)
		{
			if (types == null)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentException();
				}
				array[i] = types[i].TypeHandle.Value;
			}
			return array;
		}

		// Token: 0x06004D43 RID: 19779
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_MetadataToken(Module module);

		// Token: 0x06004D44 RID: 19780
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMDStreamVersion(IntPtr module);

		// Token: 0x06004D45 RID: 19781
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] InternalGetTypes(IntPtr module);

		// Token: 0x06004D46 RID: 19782
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetHINSTANCE(IntPtr module);

		// Token: 0x06004D47 RID: 19783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGuidInternal(IntPtr module, byte[] guid);

		// Token: 0x06004D48 RID: 19784
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type GetGlobalType(IntPtr module);

		// Token: 0x06004D49 RID: 19785
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveTypeToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x06004D4A RID: 19786
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveMethodToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x06004D4B RID: 19787
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveFieldToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x06004D4C RID: 19788
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string ResolveStringToken(IntPtr module, int token, out ResolveTokenError error);

		// Token: 0x06004D4D RID: 19789
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MemberInfo ResolveMemberToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x06004D4E RID: 19790
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] ResolveSignature(IntPtr module, int metadataToken, out ResolveTokenError error);

		// Token: 0x06004D4F RID: 19791
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetPEKind(IntPtr module, out PortableExecutableKinds peKind, out ImageFileMachine machine);

		// Token: 0x06004D50 RID: 19792 RVA: 0x000F4B0E File Offset: 0x000F2D0E
		public RuntimeModule()
		{
		}

		// Token: 0x04002FDF RID: 12255
		internal IntPtr _impl;

		// Token: 0x04002FE0 RID: 12256
		internal Assembly assembly;

		// Token: 0x04002FE1 RID: 12257
		internal string fqname;

		// Token: 0x04002FE2 RID: 12258
		internal string name;

		// Token: 0x04002FE3 RID: 12259
		internal string scopename;

		// Token: 0x04002FE4 RID: 12260
		internal bool is_resource;

		// Token: 0x04002FE5 RID: 12261
		internal int token;
	}
}
