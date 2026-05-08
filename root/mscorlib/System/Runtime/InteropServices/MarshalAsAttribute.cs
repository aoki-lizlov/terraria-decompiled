using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200071C RID: 1820
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MarshalAsAttribute : Attribute
	{
		// Token: 0x060041C9 RID: 16841 RVA: 0x000E3496 File Offset: 0x000E1696
		public MarshalAsAttribute(short unmanagedType)
		{
			this.utype = (UnmanagedType)unmanagedType;
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x000E3496 File Offset: 0x000E1696
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this.utype = unmanagedType;
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060041CB RID: 16843 RVA: 0x000E34A5 File Offset: 0x000E16A5
		public UnmanagedType Value
		{
			get
			{
				return this.utype;
			}
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x000E34AD File Offset: 0x000E16AD
		internal MarshalAsAttribute Copy()
		{
			return (MarshalAsAttribute)base.MemberwiseClone();
		}

		// Token: 0x04002B56 RID: 11094
		public string MarshalCookie;

		// Token: 0x04002B57 RID: 11095
		[ComVisible(true)]
		public string MarshalType;

		// Token: 0x04002B58 RID: 11096
		[ComVisible(true)]
		[PreserveDependency("GetCustomMarshalerInstance", "System.Runtime.InteropServices.Marshal")]
		public Type MarshalTypeRef;

		// Token: 0x04002B59 RID: 11097
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x04002B5A RID: 11098
		private UnmanagedType utype;

		// Token: 0x04002B5B RID: 11099
		public UnmanagedType ArraySubType;

		// Token: 0x04002B5C RID: 11100
		public VarEnum SafeArraySubType;

		// Token: 0x04002B5D RID: 11101
		public int SizeConst;

		// Token: 0x04002B5E RID: 11102
		public int IidParameterIndex;

		// Token: 0x04002B5F RID: 11103
		public short SizeParamIndex;
	}
}
