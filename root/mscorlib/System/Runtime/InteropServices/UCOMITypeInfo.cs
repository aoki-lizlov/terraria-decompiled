using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000706 RID: 1798
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeInfo instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020401-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeInfo
	{
		// Token: 0x06004096 RID: 16534
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x06004097 RID: 16535
		void GetTypeComp(out UCOMITypeComp ppTComp);

		// Token: 0x06004098 RID: 16536
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x06004099 RID: 16537
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x0600409A RID: 16538
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x0600409B RID: 16539
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x0600409C RID: 16540
		void GetImplTypeFlags(int index, out int pImplTypeFlags);

		// Token: 0x0600409D RID: 16541
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x0600409E RID: 16542
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, out object pVarResult, out EXCEPINFO pExcepInfo, out int puArgErr);

		// Token: 0x0600409F RID: 16543
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060040A0 RID: 16544
		void GetDllEntry(int memid, INVOKEKIND invKind, out string pBstrDllName, out string pBstrName, out short pwOrdinal);

		// Token: 0x060040A1 RID: 16545
		void GetRefTypeInfo(int hRef, out UCOMITypeInfo ppTI);

		// Token: 0x060040A2 RID: 16546
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x060040A3 RID: 16547
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x060040A4 RID: 16548
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x060040A5 RID: 16549
		void GetContainingTypeLib(out UCOMITypeLib ppTLB, out int pIndex);

		// Token: 0x060040A6 RID: 16550
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x060040A7 RID: 16551
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x060040A8 RID: 16552
		void ReleaseVarDesc(IntPtr pVarDesc);
	}
}
