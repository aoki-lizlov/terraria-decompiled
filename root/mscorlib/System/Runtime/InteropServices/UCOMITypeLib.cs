using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000731 RID: 1841
	[Obsolete]
	[Guid("00020402-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeLib
	{
		// Token: 0x0600422D RID: 16941
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x0600422E RID: 16942
		void GetTypeInfo(int index, out UCOMITypeInfo ppTI);

		// Token: 0x0600422F RID: 16943
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x06004230 RID: 16944
		void GetTypeInfoOfGuid(ref Guid guid, out UCOMITypeInfo ppTInfo);

		// Token: 0x06004231 RID: 16945
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x06004232 RID: 16946
		void GetTypeComp(out UCOMITypeComp ppTComp);

		// Token: 0x06004233 RID: 16947
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x06004234 RID: 16948
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x06004235 RID: 16949
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] UCOMITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x06004236 RID: 16950
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}
