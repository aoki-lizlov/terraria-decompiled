using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008F4 RID: 2292
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_FieldBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class FieldBuilder : FieldInfo, _FieldBuilder
	{
		// Token: 0x06004F73 RID: 20339 RVA: 0x000174FB File Offset: 0x000156FB
		void _FieldBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x000174FB File Offset: 0x000156FB
		void _FieldBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x000174FB File Offset: 0x000156FB
		void _FieldBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x000174FB File Offset: 0x000156FB
		void _FieldBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x000FA500 File Offset: 0x000F8700
		internal FieldBuilder(TypeBuilder tb, string fieldName, Type type, FieldAttributes attributes, Type[] modReq, Type[] modOpt)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.attrs = attributes;
			this.name = fieldName;
			this.type = type;
			this.modReq = modReq;
			this.modOpt = modOpt;
			this.offset = -1;
			this.typeb = tb;
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x000FA57A File Offset: 0x000F877A
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004F79 RID: 20345 RVA: 0x000FA582 File Offset: 0x000F8782
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004F7A RID: 20346 RVA: 0x000FA58A File Offset: 0x000F878A
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw this.CreateNotSupportedException();
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004F7B RID: 20347 RVA: 0x000FA592 File Offset: 0x000F8792
		public override Type FieldType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06004F7C RID: 20348 RVA: 0x000FA59A File Offset: 0x000F879A
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06004F7D RID: 20349 RVA: 0x000FA582 File Offset: 0x000F8782
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x000FA5A2 File Offset: 0x000F87A2
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x000FA5BF File Offset: 0x000F87BF
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06004F80 RID: 20352 RVA: 0x000FA5DD File Offset: 0x000F87DD
		public override int MetadataToken
		{
			get
			{
				return ((ModuleBuilder)this.typeb.Module).GetToken(this);
			}
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x000FA5F5 File Offset: 0x000F87F5
		public FieldToken GetToken()
		{
			return new FieldToken(this.MetadataToken);
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x000FA58A File Offset: 0x000F878A
		public override object GetValue(object obj)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x000FA58A File Offset: 0x000F878A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x0000408A File Offset: 0x0000228A
		internal override int GetFieldOffset()
		{
			return 0;
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x000FA602 File Offset: 0x000F8802
		internal void SetRVAData(byte[] data)
		{
			this.rva_data = (byte[])data.Clone();
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x000FA615 File Offset: 0x000F8815
		public void SetConstant(object defaultValue)
		{
			this.RejectIfCreated();
			this.def_value = defaultValue;
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x000FA624 File Offset: 0x000F8824
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.RejectIfCreated();
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.FieldOffsetAttribute")
			{
				byte[] data = customBuilder.Data;
				this.offset = (int)data[2];
				this.offset |= (int)data[3] << 8;
				this.offset |= (int)data[4] << 16;
				this.offset |= (int)data[5] << 24;
				return;
			}
			if (fullName == "System.NonSerializedAttribute")
			{
				this.attrs |= FieldAttributes.NotSerialized;
				return;
			}
			if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= FieldAttributes.SpecialName;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.MarshalAsAttribute")
			{
				this.attrs |= FieldAttributes.HasFieldMarshal;
				this.marshal_info = CustomAttributeBuilder.get_umarshal(customBuilder, true);
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

		// Token: 0x06004F88 RID: 20360 RVA: 0x000FA766 File Offset: 0x000F8966
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.RejectIfCreated();
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x000FA77B File Offset: 0x000F897B
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.RejectIfCreated();
			this.marshal_info = unmanagedMarshal;
			this.attrs |= FieldAttributes.HasFieldMarshal;
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x000FA79C File Offset: 0x000F899C
		public void SetOffset(int iOffset)
		{
			this.RejectIfCreated();
			if (iOffset < 0)
			{
				throw new ArgumentException("Negative field offset is not allowed");
			}
			this.offset = iOffset;
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x000FA58A File Offset: 0x000F878A
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x000F73F6 File Offset: 0x000F55F6
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x000FA7BA File Offset: 0x000F89BA
		private void RejectIfCreated()
		{
			if (this.typeb.is_created)
			{
				throw new InvalidOperationException("Unable to change after type has been created.");
			}
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x000FA7D4 File Offset: 0x000F89D4
		internal void ResolveUserTypes()
		{
			this.type = TypeBuilder.ResolveUserType(this.type);
			TypeBuilder.ResolveUserTypes(this.modReq);
			TypeBuilder.ResolveUserTypes(this.modOpt);
			if (this.marshal_info != null)
			{
				this.marshal_info.marshaltyperef = TypeBuilder.ResolveUserType(this.marshal_info.marshaltyperef);
			}
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x000FA82C File Offset: 0x000F8A2C
		internal FieldInfo RuntimeResolve()
		{
			RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(this.typeb.CreateType() as RuntimeType);
			return FieldInfo.GetFieldFromHandle(this.handle, runtimeTypeHandle);
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06004F90 RID: 20368 RVA: 0x000FA85C File Offset: 0x000F8A5C
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x040030DC RID: 12508
		private FieldAttributes attrs;

		// Token: 0x040030DD RID: 12509
		private Type type;

		// Token: 0x040030DE RID: 12510
		private string name;

		// Token: 0x040030DF RID: 12511
		private object def_value;

		// Token: 0x040030E0 RID: 12512
		private int offset;

		// Token: 0x040030E1 RID: 12513
		internal TypeBuilder typeb;

		// Token: 0x040030E2 RID: 12514
		private byte[] rva_data;

		// Token: 0x040030E3 RID: 12515
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040030E4 RID: 12516
		private UnmanagedMarshal marshal_info;

		// Token: 0x040030E5 RID: 12517
		private RuntimeFieldHandle handle;

		// Token: 0x040030E6 RID: 12518
		private Type[] modReq;

		// Token: 0x040030E7 RID: 12519
		private Type[] modOpt;
	}
}
