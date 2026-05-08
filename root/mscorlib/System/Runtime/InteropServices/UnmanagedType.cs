using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006CD RID: 1741
	[ComVisible(true)]
	[Serializable]
	public enum UnmanagedType
	{
		// Token: 0x04002A23 RID: 10787
		Bool = 2,
		// Token: 0x04002A24 RID: 10788
		I1,
		// Token: 0x04002A25 RID: 10789
		U1,
		// Token: 0x04002A26 RID: 10790
		I2,
		// Token: 0x04002A27 RID: 10791
		U2,
		// Token: 0x04002A28 RID: 10792
		I4,
		// Token: 0x04002A29 RID: 10793
		U4,
		// Token: 0x04002A2A RID: 10794
		I8,
		// Token: 0x04002A2B RID: 10795
		U8,
		// Token: 0x04002A2C RID: 10796
		R4,
		// Token: 0x04002A2D RID: 10797
		R8,
		// Token: 0x04002A2E RID: 10798
		Currency = 15,
		// Token: 0x04002A2F RID: 10799
		BStr = 19,
		// Token: 0x04002A30 RID: 10800
		LPStr,
		// Token: 0x04002A31 RID: 10801
		LPWStr,
		// Token: 0x04002A32 RID: 10802
		LPTStr,
		// Token: 0x04002A33 RID: 10803
		ByValTStr,
		// Token: 0x04002A34 RID: 10804
		IUnknown = 25,
		// Token: 0x04002A35 RID: 10805
		IDispatch,
		// Token: 0x04002A36 RID: 10806
		Struct,
		// Token: 0x04002A37 RID: 10807
		Interface,
		// Token: 0x04002A38 RID: 10808
		SafeArray,
		// Token: 0x04002A39 RID: 10809
		ByValArray,
		// Token: 0x04002A3A RID: 10810
		SysInt,
		// Token: 0x04002A3B RID: 10811
		SysUInt,
		// Token: 0x04002A3C RID: 10812
		VBByRefStr = 34,
		// Token: 0x04002A3D RID: 10813
		AnsiBStr,
		// Token: 0x04002A3E RID: 10814
		TBStr,
		// Token: 0x04002A3F RID: 10815
		VariantBool,
		// Token: 0x04002A40 RID: 10816
		FunctionPtr,
		// Token: 0x04002A41 RID: 10817
		AsAny = 40,
		// Token: 0x04002A42 RID: 10818
		LPArray = 42,
		// Token: 0x04002A43 RID: 10819
		LPStruct,
		// Token: 0x04002A44 RID: 10820
		CustomMarshaler,
		// Token: 0x04002A45 RID: 10821
		Error,
		// Token: 0x04002A46 RID: 10822
		[ComVisible(false)]
		IInspectable,
		// Token: 0x04002A47 RID: 10823
		[ComVisible(false)]
		HString,
		// Token: 0x04002A48 RID: 10824
		[ComVisible(false)]
		LPUTF8Str
	}
}
