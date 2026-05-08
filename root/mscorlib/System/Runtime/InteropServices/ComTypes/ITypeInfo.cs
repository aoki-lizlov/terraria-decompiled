using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000799 RID: 1945
	[Guid("00020401-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeInfo
	{
		// Token: 0x060044F0 RID: 17648
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x060044F1 RID: 17649
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x060044F2 RID: 17650
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x060044F3 RID: 17651
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x060044F4 RID: 17652
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x060044F5 RID: 17653
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x060044F6 RID: 17654
		void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

		// Token: 0x060044F7 RID: 17655
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x060044F8 RID: 17656
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

		// Token: 0x060044F9 RID: 17657
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060044FA RID: 17658
		void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

		// Token: 0x060044FB RID: 17659
		void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

		// Token: 0x060044FC RID: 17660
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x060044FD RID: 17661
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x060044FE RID: 17662
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x060044FF RID: 17663
		void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

		// Token: 0x06004500 RID: 17664
		[PreserveSig]
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x06004501 RID: 17665
		[PreserveSig]
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x06004502 RID: 17666
		[PreserveSig]
		void ReleaseVarDesc(IntPtr pVarDesc);
	}
}
