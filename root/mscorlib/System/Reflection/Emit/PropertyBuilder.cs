using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000910 RID: 2320
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_PropertyBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
	{
		// Token: 0x06005174 RID: 20852 RVA: 0x000174FB File Offset: 0x000156FB
		void _PropertyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x000174FB File Offset: 0x000156FB
		void _PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x000174FB File Offset: 0x000156FB
		void _PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x000174FB File Offset: 0x000156FB
		void _PropertyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x00101E98 File Offset: 0x00100098
		internal PropertyBuilder(TypeBuilder tb, string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.name = name;
			this.attrs = attributes;
			this.callingConvention = callingConvention;
			this.type = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if (parameterTypes != null)
			{
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, this.parameters.Length);
			}
			this.typeb = tb;
			this.table_idx = tb.get_next_table_index(this, 23, 1);
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06005179 RID: 20857 RVA: 0x00101F28 File Offset: 0x00100128
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x00101F30 File Offset: 0x00100130
		public override bool CanRead
		{
			get
			{
				return this.get_method != null;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x00101F3E File Offset: 0x0010013E
		public override bool CanWrite
		{
			get
			{
				return this.set_method != null;
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x0600517C RID: 20860 RVA: 0x00101F4C File Offset: 0x0010014C
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x0600517D RID: 20861 RVA: 0x00101F54 File Offset: 0x00100154
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x0600517E RID: 20862 RVA: 0x00101F5C File Offset: 0x0010015C
		public PropertyToken PropertyToken
		{
			get
			{
				return default(PropertyToken);
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600517F RID: 20863 RVA: 0x00101F72 File Offset: 0x00100172
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06005180 RID: 20864 RVA: 0x00101F4C File Offset: 0x0010014C
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x00004088 File Offset: 0x00002288
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			return null;
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x00101F7A File Offset: 0x0010017A
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x00101F7A File Offset: 0x0010017A
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x00101F82 File Offset: 0x00100182
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return this.get_method;
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x00101F7A File Offset: 0x0010017A
		public override ParameterInfo[] GetIndexParameters()
		{
			throw this.not_supported();
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x00101F8A File Offset: 0x0010018A
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			return this.set_method;
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override object GetValue(object obj, object[] index)
		{
			return null;
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00101F7A File Offset: 0x0010017A
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw this.not_supported();
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00101F7A File Offset: 0x0010017A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00101F92 File Offset: 0x00100192
		public void SetConstant(object defaultValue)
		{
			this.def_value = defaultValue;
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00101F9C File Offset: 0x0010019C
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder.Ctor.ReflectedType.FullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= PropertyAttributes.SpecialName;
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

		// Token: 0x0600518D RID: 20877 RVA: 0x00102025 File Offset: 0x00100225
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x00102034 File Offset: 0x00100234
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			this.get_method = mdBuilder;
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x0010203D File Offset: 0x0010023D
		public void SetSetMethod(MethodBuilder mdBuilder)
		{
			this.set_method = mdBuilder;
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x00004088 File Offset: 0x00002288
		public override void SetValue(object obj, object value, object[] index)
		{
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x00004088 File Offset: 0x00002288
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06005192 RID: 20882 RVA: 0x000FA85C File Offset: 0x000F8A5C
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x000F73F6 File Offset: 0x000F55F6
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x04003277 RID: 12919
		private PropertyAttributes attrs;

		// Token: 0x04003278 RID: 12920
		private string name;

		// Token: 0x04003279 RID: 12921
		private Type type;

		// Token: 0x0400327A RID: 12922
		private Type[] parameters;

		// Token: 0x0400327B RID: 12923
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x0400327C RID: 12924
		private object def_value;

		// Token: 0x0400327D RID: 12925
		private MethodBuilder set_method;

		// Token: 0x0400327E RID: 12926
		private MethodBuilder get_method;

		// Token: 0x0400327F RID: 12927
		private int table_idx;

		// Token: 0x04003280 RID: 12928
		internal TypeBuilder typeb;

		// Token: 0x04003281 RID: 12929
		private Type[] returnModReq;

		// Token: 0x04003282 RID: 12930
		private Type[] returnModOpt;

		// Token: 0x04003283 RID: 12931
		private Type[][] paramModReq;

		// Token: 0x04003284 RID: 12932
		private Type[][] paramModOpt;

		// Token: 0x04003285 RID: 12933
		private CallingConventions callingConvention;
	}
}
