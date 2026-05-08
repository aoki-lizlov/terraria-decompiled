using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200090E RID: 2318
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_ParameterBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public class ParameterBuilder : _ParameterBuilder
	{
		// Token: 0x0600515C RID: 20828 RVA: 0x000174FB File Offset: 0x000156FB
		void _ParameterBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x000174FB File Offset: 0x000156FB
		void _ParameterBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x000174FB File Offset: 0x000156FB
		void _ParameterBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x000174FB File Offset: 0x000156FB
		void _ParameterBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x00101C00 File Offset: 0x000FFE00
		internal ParameterBuilder(MethodBase mb, int pos, ParameterAttributes attributes, string strParamName)
		{
			this.name = strParamName;
			this.position = pos;
			this.attrs = attributes;
			this.methodb = mb;
			if (mb is DynamicMethod)
			{
				this.table_idx = 0;
				return;
			}
			this.table_idx = mb.get_next_table_index(this, 8, 1);
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06005161 RID: 20833 RVA: 0x00101C4F File Offset: 0x000FFE4F
		public virtual int Attributes
		{
			get
			{
				return (int)this.attrs;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06005162 RID: 20834 RVA: 0x00101C57 File Offset: 0x000FFE57
		public bool IsIn
		{
			get
			{
				return (this.attrs & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06005163 RID: 20835 RVA: 0x00101C64 File Offset: 0x000FFE64
		public bool IsOut
		{
			get
			{
				return (this.attrs & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06005164 RID: 20836 RVA: 0x00101C71 File Offset: 0x000FFE71
		public bool IsOptional
		{
			get
			{
				return (this.attrs & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06005165 RID: 20837 RVA: 0x00101C7F File Offset: 0x000FFE7F
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06005166 RID: 20838 RVA: 0x00101C87 File Offset: 0x000FFE87
		public virtual int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x00101C8F File Offset: 0x000FFE8F
		public virtual ParameterToken GetToken()
		{
			return new ParameterToken(8 | this.table_idx);
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x00101C9E File Offset: 0x000FFE9E
		public virtual void SetConstant(object defaultValue)
		{
			if (this.position > 0)
			{
				TypeBuilder.SetConstantValue(this.methodb.GetParameterType(this.position - 1), defaultValue, ref defaultValue);
			}
			this.def_value = defaultValue;
			this.attrs |= ParameterAttributes.HasDefault;
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x00101CE0 File Offset: 0x000FFEE0
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.InAttribute")
			{
				this.attrs |= ParameterAttributes.In;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.OutAttribute")
			{
				this.attrs |= ParameterAttributes.Out;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.OptionalAttribute")
			{
				this.attrs |= ParameterAttributes.Optional;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.MarshalAsAttribute")
			{
				this.attrs |= ParameterAttributes.HasFieldMarshal;
				this.marshal_info = CustomAttributeBuilder.get_umarshal(customBuilder, false);
				return;
			}
			if (fullName == "System.Runtime.InteropServices.DefaultParameterValueAttribute")
			{
				CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = CustomAttributeBuilder.decode_cattr(customBuilder);
				this.SetConstant(customAttributeInfo.ctorArgs[0]);
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

		// Token: 0x0600516A RID: 20842 RVA: 0x00101DF0 File Offset: 0x000FFFF0
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x00101DFF File Offset: 0x000FFFFF
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public virtual void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.marshal_info = unmanagedMarshal;
			this.attrs |= ParameterAttributes.HasFieldMarshal;
		}

		// Token: 0x0400326D RID: 12909
		private MethodBase methodb;

		// Token: 0x0400326E RID: 12910
		private string name;

		// Token: 0x0400326F RID: 12911
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04003270 RID: 12912
		private UnmanagedMarshal marshal_info;

		// Token: 0x04003271 RID: 12913
		private ParameterAttributes attrs;

		// Token: 0x04003272 RID: 12914
		private int position;

		// Token: 0x04003273 RID: 12915
		private int table_idx;

		// Token: 0x04003274 RID: 12916
		private object def_value;
	}
}
