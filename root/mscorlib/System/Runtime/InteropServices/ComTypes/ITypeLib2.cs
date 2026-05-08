using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200079F RID: 1951
	[Guid("00020411-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeLib2 : ITypeLib
	{
		// Token: 0x0600452F RID: 17711
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x06004530 RID: 17712
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x06004531 RID: 17713
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x06004532 RID: 17714
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x06004533 RID: 17715
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x06004534 RID: 17716
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06004535 RID: 17717
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x06004536 RID: 17718
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x06004537 RID: 17719
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x06004538 RID: 17720
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);

		// Token: 0x06004539 RID: 17721
		void GetCustData(ref Guid guid, out object pVarVal);

		// Token: 0x0600453A RID: 17722
		[LCIDConversion(1)]
		void GetDocumentation2(int index, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		// Token: 0x0600453B RID: 17723
		void GetLibStatistics(IntPtr pcUniqueNames, out int pcchUniqueNames);

		// Token: 0x0600453C RID: 17724
		void GetAllCustData(IntPtr pCustData);
	}
}
