using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000904 RID: 2308
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MethodBuilder : MethodInfo, _MethodBuilder
	{
		// Token: 0x06005060 RID: 20576 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x000FD250 File Offset: 0x000FB450
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.name = name;
			this.attrs = attributes;
			this.call_conv = callingConvention;
			this.rtype = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				this.call_conv |= CallingConventions.HasThis;
			}
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Elements of the parameterTypes array cannot be null", "parameterTypes");
					}
				}
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, parameterTypes.Length);
			}
			this.type = tb;
			this.table_idx = this.get_next_table_index(this, 6, 1);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x000FD344 File Offset: 0x000FB544
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt, string dllName, string entryName, CallingConvention nativeCConv, CharSet nativeCharset)
			: this(tb, name, attributes, callingConvention, returnType, returnModReq, returnModOpt, parameterTypes, paramModReq, paramModOpt)
		{
			this.pi_dll = dllName;
			this.pi_entry = entryName;
			this.native_cc = nativeCConv;
			this.charset = nativeCharset;
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06005066 RID: 20582 RVA: 0x00047E00 File Offset: 0x00046000
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06005067 RID: 20583 RVA: 0x000FD388 File Offset: 0x000FB588
		// (set) Token: 0x06005068 RID: 20584 RVA: 0x000FD390 File Offset: 0x000FB590
		public bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
			set
			{
				this.init_locals = value;
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06005069 RID: 20585 RVA: 0x000FD399 File Offset: 0x000FB599
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x0600506A RID: 20586 RVA: 0x000FD3A1 File Offset: 0x000FB5A1
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.NotSupported();
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600506B RID: 20587 RVA: 0x000FD3A9 File Offset: 0x000FB5A9
		internal RuntimeMethodHandle MethodHandleInternal
		{
			get
			{
				return this.mhandle;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600506C RID: 20588 RVA: 0x000FD3B1 File Offset: 0x000FB5B1
		public override Type ReturnType
		{
			get
			{
				return this.rtype;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600506D RID: 20589 RVA: 0x000FD399 File Offset: 0x000FB599
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x000FD399 File Offset: 0x000FB599
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600506F RID: 20591 RVA: 0x000FD3B9 File Offset: 0x000FB5B9
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06005070 RID: 20592 RVA: 0x000FD3C1 File Offset: 0x000FB5C1
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06005071 RID: 20593 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x000FD3C9 File Offset: 0x000FB5C9
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06005073 RID: 20595 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public string Signature
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (set) Token: 0x06005074 RID: 20596 RVA: 0x000FD3D1 File Offset: 0x000FB5D1
		internal bool BestFitMapping
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551567UL) | (value ? 16UL : 32UL));
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (set) Token: 0x06005075 RID: 20597 RVA: 0x000FD3F0 File Offset: 0x000FB5F0
		internal bool ThrowOnUnmappableChar
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709539327UL) | (value ? 4096UL : 8192UL));
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (set) Token: 0x06005076 RID: 20598 RVA: 0x000FD418 File Offset: 0x000FB618
		internal bool ExactSpelling
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551614UL) | (value ? 1UL : 0UL));
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (set) Token: 0x06005077 RID: 20599 RVA: 0x000FD435 File Offset: 0x000FB635
		internal bool SetLastError
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551551UL) | (value ? 64UL : 0UL));
			}
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x000FD453 File Offset: 0x000FB653
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x000025CE File Offset: 0x000007CE
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x000FD466 File Offset: 0x000FB666
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x000FD46E File Offset: 0x000FB66E
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.NotSupported();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x000FD48C File Offset: 0x000FB68C
		internal override ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return null;
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				ParameterInfo[] array2 = array;
				int num = i;
				ParameterBuilder[] array3 = this.pinfo;
				array2[num] = RuntimeParameterInfo.New((array3 != null) ? array3[i + 1] : null, this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x000FD4EA File Offset: 0x000FB6EA
		internal override int GetParametersCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x000FD4FE File Offset: 0x000FB6FE
		internal override Type GetParameterType(int pos)
		{
			return this.parameters[pos];
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x000FD508 File Offset: 0x000FB708
		internal MethodBase RuntimeResolve()
		{
			return this.type.RuntimeResolve().GetMethod(this);
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x000FD51B File Offset: 0x000FB71B
		public Module GetModule()
		{
			return this.type.Module;
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x000FD528 File Offset: 0x000FB728
		public void CreateMethodBody(byte[] il, int count)
		{
			if (il != null && (count < 0 || count > il.Length))
			{
				throw new ArgumentOutOfRangeException("Index was out of range.  Must be non-negative and less than the size of the collection.");
			}
			if (this.code != null || this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
			if (il == null)
			{
				this.code = null;
				return;
			}
			this.code = new byte[count];
			Array.Copy(il, this.code, count);
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x000FD591 File Offset: 0x000FB791
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			this.GetILGenerator().Init(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x000FD3A1 File Offset: 0x000FB5A1
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.NotSupported();
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x000FD3A1 File Offset: 0x000FB5A1
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.NotSupported();
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x000FD5A5 File Offset: 0x000FB7A5
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.NotSupported();
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x000FD5C2 File Offset: 0x000FB7C2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.NotSupported();
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x000FD5E0 File Offset: 0x000FB7E0
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x000FD5EC File Offset: 0x000FB7EC
		public ILGenerator GetILGenerator(int size)
		{
			if ((this.iattrs & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.iattrs & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), size);
			return this.ilgen;
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x000FD65C File Offset: 0x000FB85C
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			this.RejectIfCreated();
			if (position < 0 || this.parameters == null || position > this.parameters.Length)
			{
				throw new ArgumentOutOfRangeException("position");
			}
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, position, attributes, strParamName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[position] = parameterBuilder;
			return parameterBuilder;
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x000FD6C4 File Offset: 0x000FB8C4
		internal void check_override()
		{
			if (this.override_methods != null)
			{
				foreach (MethodInfo methodInfo in this.override_methods)
				{
					if (methodInfo.IsVirtual && !base.IsVirtual)
					{
						throw new TypeLoadException(string.Format("Method '{0}' override '{1}' but it is not virtual", this.name, methodInfo));
					}
				}
			}
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x000FD71C File Offset: 0x000FB91C
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || this.ilgen.ILOffset == 0) && (this.code == null || this.code.Length == 0))
			{
				throw new InvalidOperationException(string.Format("Method '{0}.{1}' does not have a method body.", this.DeclaringType.FullName, this.Name));
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup(this);
			}
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x000FD7A0 File Offset: 0x000FB9A0
		internal void ResolveUserTypes()
		{
			this.rtype = TypeBuilder.ResolveUserType(this.rtype);
			TypeBuilder.ResolveUserTypes(this.parameters);
			TypeBuilder.ResolveUserTypes(this.returnModReq);
			TypeBuilder.ResolveUserTypes(this.returnModOpt);
			if (this.paramModReq != null)
			{
				Type[][] array = this.paramModReq;
				for (int i = 0; i < array.Length; i++)
				{
					TypeBuilder.ResolveUserTypes(array[i]);
				}
			}
			if (this.paramModOpt != null)
			{
				Type[][] array = this.paramModOpt;
				for (int i = 0; i < array.Length; i++)
				{
					TypeBuilder.ResolveUserTypes(array[i]);
				}
			}
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x000FD829 File Offset: 0x000FBA29
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.ilgen != null)
			{
				this.ilgen.FixupTokens(token_map, member_map);
			}
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x000FD840 File Offset: 0x000FBA40
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			if (this.ilgen != null && this.ilgen.HasDebugInfo)
			{
				SymbolToken symbolToken = new SymbolToken(this.GetToken().Token);
				symbolWriter.OpenMethod(symbolToken);
				symbolWriter.SetSymAttribute(symbolToken, "__name", Encoding.UTF8.GetBytes(this.Name));
				this.ilgen.GenerateDebugInfo(symbolWriter);
				symbolWriter.CloseMethod();
			}
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x000FD8AC File Offset: 0x000FBAAC
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.CompilerServices.MethodImplAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2];
				num |= (int)data[3] << 8;
				this.iattrs |= (MethodImplAttributes)num;
				return;
			}
			if (!(fullName == "System.Runtime.InteropServices.DllImportAttribute"))
			{
				if (fullName == "System.Runtime.InteropServices.PreserveSigAttribute")
				{
					this.iattrs |= MethodImplAttributes.PreserveSig;
					return;
				}
				if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
				{
					this.attrs |= MethodAttributes.SpecialName;
					return;
				}
				if (fullName == "System.Security.SuppressUnmanagedCodeSecurityAttribute")
				{
					this.attrs |= MethodAttributes.HasSecurity;
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
				return;
			}
			else
			{
				CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = CustomAttributeBuilder.decode_cattr(customBuilder);
				bool flag = true;
				this.pi_dll = (string)customAttributeInfo.ctorArgs[0];
				if (this.pi_dll == null || this.pi_dll.Length == 0)
				{
					throw new ArgumentException("DllName cannot be empty");
				}
				this.native_cc = global::System.Runtime.InteropServices.CallingConvention.Winapi;
				for (int i = 0; i < customAttributeInfo.namedParamNames.Length; i++)
				{
					string text = customAttributeInfo.namedParamNames[i];
					object obj = customAttributeInfo.namedParamValues[i];
					if (text == "CallingConvention")
					{
						this.native_cc = (CallingConvention)obj;
					}
					else if (text == "CharSet")
					{
						this.charset = (CharSet)obj;
					}
					else if (text == "EntryPoint")
					{
						this.pi_entry = (string)obj;
					}
					else if (text == "ExactSpelling")
					{
						this.ExactSpelling = (bool)obj;
					}
					else if (text == "SetLastError")
					{
						this.SetLastError = (bool)obj;
					}
					else if (text == "PreserveSig")
					{
						flag = (bool)obj;
					}
					else if (text == "BestFitMapping")
					{
						this.BestFitMapping = (bool)obj;
					}
					else if (text == "ThrowOnUnmappableChar")
					{
						this.ThrowOnUnmappableChar = (bool)obj;
					}
				}
				this.attrs |= MethodAttributes.PinvokeImpl;
				if (flag)
				{
					this.iattrs |= MethodImplAttributes.PreserveSig;
				}
				return;
			}
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x000FDB60 File Offset: 0x000FBD60
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x000FDB91 File Offset: 0x000FBD91
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.RejectIfCreated();
			this.iattrs = attributes;
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x000FDBA0 File Offset: 0x000FBDA0
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
			this.RejectIfCreated();
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
			this.attrs |= MethodAttributes.HasSecurity;
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x000FDC80 File Offset: 0x000FBE80
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.RejectIfCreated();
			throw new NotImplementedException();
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x000FDC80 File Offset: 0x000FBE80
		[MonoTODO]
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.RejectIfCreated();
			throw new NotImplementedException();
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x000FDC8D File Offset: 0x000FBE8D
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"MethodBuilder [",
				this.type.Name,
				"::",
				this.name,
				"]"
			});
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x000FDCC9 File Offset: 0x000FBEC9
		[MonoTODO]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x000FDCD2 File Offset: 0x000FBED2
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x000FDCDF File Offset: 0x000FBEDF
		internal override int get_next_table_index(object obj, int table, int count)
		{
			return this.type.get_next_table_index(obj, table, count);
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x000FDCF0 File Offset: 0x000FBEF0
		private void ExtendArray<T>(ref T[] array, T elem)
		{
			if (array == null)
			{
				array = new T[1];
			}
			else
			{
				T[] array2 = new T[array.Length + 1];
				Array.Copy(array, array2, array.Length);
				array = array2;
			}
			array[array.Length - 1] = elem;
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x000FDD34 File Offset: 0x000FBF34
		internal void set_override(MethodInfo mdecl)
		{
			this.ExtendArray<MethodInfo>(ref this.override_methods, mdecl);
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x000FDD43 File Offset: 0x000FBF43
		private void RejectIfCreated()
		{
			if (this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x000F73F6 File Offset: 0x000F55F6
		private Exception NotSupported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x000FDD60 File Offset: 0x000FBF60
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException("Method is not a generic method definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException("Incorrect length", "typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			return new MethodOnTypeBuilderInst(this, typeArguments);
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x0600509E RID: 20638 RVA: 0x000FDDD8 File Offset: 0x000FBFD8
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x0600509F RID: 20639 RVA: 0x000FDDD8 File Offset: 0x000FBFD8
		public override bool IsGenericMethod
		{
			get
			{
				return this.generic_params != null;
			}
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x000FDDE3 File Offset: 0x000FBFE3
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x000FDDF4 File Offset: 0x000FBFF4
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return null;
			}
			Type[] array = new Type[this.generic_params.Length];
			for (int i = 0; i < this.generic_params.Length; i++)
			{
				array[i] = this.generic_params[i];
			}
			return array;
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x000FDE38 File Offset: 0x000FC038
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
				this.generic_params[i] = new GenericTypeParameterBuilder(this.type, this, text, i);
			}
			return this.generic_params;
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x000FDEAC File Offset: 0x000FC0AC
		public void SetReturnType(Type returnType)
		{
			this.rtype = returnType;
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x000FDEB8 File Offset: 0x000FC0B8
		public void SetParameters(params Type[] parameterTypes)
		{
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Elements of the parameterTypes array cannot be null", "parameterTypes");
					}
				}
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, parameterTypes.Length);
			}
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x000FDF0E File Offset: 0x000FC10E
		public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.SetReturnType(returnType);
			this.SetParameters(parameterTypes);
			this.returnModReq = returnTypeRequiredCustomModifiers;
			this.returnModOpt = returnTypeOptionalCustomModifiers;
			this.paramModReq = parameterTypeRequiredCustomModifiers;
			this.paramModOpt = parameterTypeOptionalCustomModifiers;
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060050A6 RID: 20646 RVA: 0x000FDF3D File Offset: 0x000FC13D
		public override Module Module
		{
			get
			{
				return this.GetModule();
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060050A7 RID: 20647 RVA: 0x000FDF45 File Offset: 0x000FC145
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return base.ReturnParameter;
			}
		}

		// Token: 0x04003136 RID: 12598
		private RuntimeMethodHandle mhandle;

		// Token: 0x04003137 RID: 12599
		private Type rtype;

		// Token: 0x04003138 RID: 12600
		internal Type[] parameters;

		// Token: 0x04003139 RID: 12601
		private MethodAttributes attrs;

		// Token: 0x0400313A RID: 12602
		private MethodImplAttributes iattrs;

		// Token: 0x0400313B RID: 12603
		private string name;

		// Token: 0x0400313C RID: 12604
		private int table_idx;

		// Token: 0x0400313D RID: 12605
		private byte[] code;

		// Token: 0x0400313E RID: 12606
		private ILGenerator ilgen;

		// Token: 0x0400313F RID: 12607
		private TypeBuilder type;

		// Token: 0x04003140 RID: 12608
		internal ParameterBuilder[] pinfo;

		// Token: 0x04003141 RID: 12609
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04003142 RID: 12610
		private MethodInfo[] override_methods;

		// Token: 0x04003143 RID: 12611
		private string pi_dll;

		// Token: 0x04003144 RID: 12612
		private string pi_entry;

		// Token: 0x04003145 RID: 12613
		private CharSet charset;

		// Token: 0x04003146 RID: 12614
		private uint extra_flags;

		// Token: 0x04003147 RID: 12615
		private CallingConvention native_cc;

		// Token: 0x04003148 RID: 12616
		private CallingConventions call_conv;

		// Token: 0x04003149 RID: 12617
		private bool init_locals = true;

		// Token: 0x0400314A RID: 12618
		private IntPtr generic_container;

		// Token: 0x0400314B RID: 12619
		internal GenericTypeParameterBuilder[] generic_params;

		// Token: 0x0400314C RID: 12620
		private Type[] returnModReq;

		// Token: 0x0400314D RID: 12621
		private Type[] returnModOpt;

		// Token: 0x0400314E RID: 12622
		private Type[][] paramModReq;

		// Token: 0x0400314F RID: 12623
		private Type[][] paramModOpt;

		// Token: 0x04003150 RID: 12624
		private RefEmitPermissionSet[] permissions;
	}
}
