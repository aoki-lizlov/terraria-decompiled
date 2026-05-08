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
	// Token: 0x020008E5 RID: 2277
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_ConstructorBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
	{
		// Token: 0x06004E53 RID: 20051 RVA: 0x000174FB File Offset: 0x000156FB
		void _ConstructorBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x000174FB File Offset: 0x000156FB
		void _ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x000174FB File Offset: 0x000156FB
		void _ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x000174FB File Offset: 0x000156FB
		void _ConstructorBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x000F797C File Offset: 0x000F5B7C
		internal ConstructorBuilder(TypeBuilder tb, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.attrs = attributes | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
			this.call_conv = callingConvention;
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
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			this.table_idx = this.get_next_table_index(this, 6, 1);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004E58 RID: 20056 RVA: 0x000F7A44 File Offset: 0x000F5C44
		[MonoTODO]
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004E59 RID: 20057 RVA: 0x000F7A4C File Offset: 0x000F5C4C
		// (set) Token: 0x06004E5A RID: 20058 RVA: 0x000F7A54 File Offset: 0x000F5C54
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

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x000F7A5D File Offset: 0x000F5C5D
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x000F7A65 File Offset: 0x000F5C65
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x000F7A6D File Offset: 0x000F5C6D
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.not_created();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x000F7A8C File Offset: 0x000F5C8C
		internal override ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return EmptyArray<ParameterInfo>.Value;
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

		// Token: 0x06004E5F RID: 20063 RVA: 0x000F7AEE File Offset: 0x000F5CEE
		internal override int GetParametersCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x000F7B02 File Offset: 0x000F5D02
		internal override Type GetParameterType(int pos)
		{
			return this.parameters[pos];
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x000F7B0C File Offset: 0x000F5D0C
		internal MethodBase RuntimeResolve()
		{
			return this.type.RuntimeResolve().GetConstructor(this);
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x000F7B1F File Offset: 0x000F5D1F
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x000F7B1F File Offset: 0x000F5D1F
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x000F7B1F File Offset: 0x000F5D1F
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004E65 RID: 20069 RVA: 0x000F7B27 File Offset: 0x000F5D27
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x000F7A5D File Offset: 0x000F5C5D
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004E67 RID: 20071 RVA: 0x000F7A5D File Offset: 0x000F5C5D
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004E68 RID: 20072 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		[Obsolete]
		public Type ReturnType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06004E69 RID: 20073 RVA: 0x000F7B2F File Offset: 0x000F5D2F
		public override string Name
		{
			get
			{
				if ((this.attrs & MethodAttributes.Static) == MethodAttributes.PrivateScope)
				{
					return ConstructorInfo.ConstructorName;
				}
				return ConstructorInfo.TypeConstructorName;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x000F7B47 File Offset: 0x000F5D47
		public string Signature
		{
			get
			{
				return "constructor signature";
			}
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x000F7B50 File Offset: 0x000F5D50
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action", "Request* values are not permitted");
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

		// Token: 0x06004E6C RID: 20076 RVA: 0x000F7C30 File Offset: 0x000F5E30
		public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, string strParamName)
		{
			if (iSequence < 0 || iSequence > this.GetParametersCount())
			{
				throw new ArgumentOutOfRangeException("iSequence");
			}
			if (this.type.is_created)
			{
				throw this.not_after_created();
			}
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, iSequence, attributes, strParamName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[iSequence] = parameterBuilder;
			return parameterBuilder;
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x000F7B1F File Offset: 0x000F5D1F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x000F7B1F File Offset: 0x000F5D1F
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x000F7B1F File Offset: 0x000F5D1F
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x000F7C9A File Offset: 0x000F5E9A
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x000F7CA4 File Offset: 0x000F5EA4
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), streamSize);
			return this.ilgen;
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x000F7CF2 File Offset: 0x000F5EF2
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			this.GetILGenerator().Init(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x000F7D08 File Offset: 0x000F5F08
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (customBuilder.Ctor.ReflectedType.FullName == "System.Runtime.CompilerServices.MethodImplAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2];
				num |= (int)data[3] << 8;
				this.SetImplementationFlags((MethodImplAttributes)num);
				return;
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

		// Token: 0x06004E74 RID: 20084 RVA: 0x000F7DA7 File Offset: 0x000F5FA7
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

		// Token: 0x06004E75 RID: 20085 RVA: 0x000F7DD8 File Offset: 0x000F5FD8
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			if (this.type.is_created)
			{
				throw this.not_after_created();
			}
			this.iattrs = attributes;
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x000F7DF5 File Offset: 0x000F5FF5
		public Module GetModule()
		{
			return this.type.Module;
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x000F7E02 File Offset: 0x000F6002
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x000F7E15 File Offset: 0x000F6015
		[MonoTODO]
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			if (this.type.is_created)
			{
				throw this.not_after_created();
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004E79 RID: 20089 RVA: 0x000F7E2B File Offset: 0x000F602B
		public override Module Module
		{
			get
			{
				return this.GetModule();
			}
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x000F7E33 File Offset: 0x000F6033
		public override string ToString()
		{
			return "ConstructorBuilder ['" + this.type.Name + "']";
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x000F7E50 File Offset: 0x000F6050
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || this.ilgen.ILOffset == 0))
			{
				throw new InvalidOperationException("Method '" + this.Name + "' does not have a method body.");
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup(this);
			}
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x000F7EC0 File Offset: 0x000F60C0
		internal void ResolveUserTypes()
		{
			TypeBuilder.ResolveUserTypes(this.parameters);
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

		// Token: 0x06004E7D RID: 20093 RVA: 0x000F7F22 File Offset: 0x000F6122
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.ilgen != null)
			{
				this.ilgen.FixupTokens(token_map, member_map);
			}
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x000F7F3C File Offset: 0x000F613C
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

		// Token: 0x06004E7F RID: 20095 RVA: 0x000F7FA8 File Offset: 0x000F61A8
		internal override int get_next_table_index(object obj, int table, int count)
		{
			return this.type.get_next_table_index(obj, table, count);
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x000F7FB8 File Offset: 0x000F61B8
		private void RejectIfCreated()
		{
			if (this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x000F73F6 File Offset: 0x000F55F6
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x000F7FD2 File Offset: 0x000F61D2
		private Exception not_after_created()
		{
			return new InvalidOperationException("Unable to change after type has been created.");
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x000F7FDE File Offset: 0x000F61DE
		private Exception not_created()
		{
			return new NotSupportedException("The type is not yet created.");
		}

		// Token: 0x04003098 RID: 12440
		private RuntimeMethodHandle mhandle;

		// Token: 0x04003099 RID: 12441
		private ILGenerator ilgen;

		// Token: 0x0400309A RID: 12442
		internal Type[] parameters;

		// Token: 0x0400309B RID: 12443
		private MethodAttributes attrs;

		// Token: 0x0400309C RID: 12444
		private MethodImplAttributes iattrs;

		// Token: 0x0400309D RID: 12445
		private int table_idx;

		// Token: 0x0400309E RID: 12446
		private CallingConventions call_conv;

		// Token: 0x0400309F RID: 12447
		private TypeBuilder type;

		// Token: 0x040030A0 RID: 12448
		internal ParameterBuilder[] pinfo;

		// Token: 0x040030A1 RID: 12449
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040030A2 RID: 12450
		private bool init_locals = true;

		// Token: 0x040030A3 RID: 12451
		private Type[][] paramModReq;

		// Token: 0x040030A4 RID: 12452
		private Type[][] paramModOpt;

		// Token: 0x040030A5 RID: 12453
		private RefEmitPermissionSet[] permissions;
	}
}
