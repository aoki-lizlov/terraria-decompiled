using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200079A RID: 1946
	[Guid("00020412-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeInfo2 : ITypeInfo
	{
		// Token: 0x06004503 RID: 17667
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x06004504 RID: 17668
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06004505 RID: 17669
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x06004506 RID: 17670
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x06004507 RID: 17671
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x06004508 RID: 17672
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x06004509 RID: 17673
		void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

		// Token: 0x0600450A RID: 17674
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x0600450B RID: 17675
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

		// Token: 0x0600450C RID: 17676
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x0600450D RID: 17677
		void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

		// Token: 0x0600450E RID: 17678
		void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

		// Token: 0x0600450F RID: 17679
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x06004510 RID: 17680
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x06004511 RID: 17681
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x06004512 RID: 17682
		void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

		// Token: 0x06004513 RID: 17683
		[PreserveSig]
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x06004514 RID: 17684
		[PreserveSig]
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x06004515 RID: 17685
		[PreserveSig]
		void ReleaseVarDesc(IntPtr pVarDesc);

		// Token: 0x06004516 RID: 17686
		void GetTypeKind(out TYPEKIND pTypeKind);

		// Token: 0x06004517 RID: 17687
		void GetTypeFlags(out int pTypeFlags);

		// Token: 0x06004518 RID: 17688
		void GetFuncIndexOfMemId(int memid, INVOKEKIND invKind, out int pFuncIndex);

		// Token: 0x06004519 RID: 17689
		void GetVarIndexOfMemId(int memid, out int pVarIndex);

		// Token: 0x0600451A RID: 17690
		void GetCustData(ref Guid guid, out object pVarVal);

		// Token: 0x0600451B RID: 17691
		void GetFuncCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x0600451C RID: 17692
		void GetParamCustData(int indexFunc, int indexParam, ref Guid guid, out object pVarVal);

		// Token: 0x0600451D RID: 17693
		void GetVarCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x0600451E RID: 17694
		void GetImplTypeCustData(int index, ref Guid guid, out object pVarVal);

		// Token: 0x0600451F RID: 17695
		[LCIDConversion(1)]
		void GetDocumentation2(int memid, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		// Token: 0x06004520 RID: 17696
		void GetAllCustData(IntPtr pCustData);

		// Token: 0x06004521 RID: 17697
		void GetAllFuncCustData(int index, IntPtr pCustData);

		// Token: 0x06004522 RID: 17698
		void GetAllParamCustData(int indexFunc, int indexParam, IntPtr pCustData);

		// Token: 0x06004523 RID: 17699
		void GetAllVarCustData(int index, IntPtr pCustData);

		// Token: 0x06004524 RID: 17700
		void GetAllImplTypeCustData(int index, IntPtr pCustData);
	}
}
