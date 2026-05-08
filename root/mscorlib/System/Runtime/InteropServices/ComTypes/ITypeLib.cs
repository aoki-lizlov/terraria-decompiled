using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200079E RID: 1950
	[Guid("00020402-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITypeLib
	{
		// Token: 0x06004525 RID: 17701
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x06004526 RID: 17702
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x06004527 RID: 17703
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x06004528 RID: 17704
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x06004529 RID: 17705
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x0600452A RID: 17706
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x0600452B RID: 17707
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x0600452C RID: 17708
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x0600452D RID: 17709
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x0600452E RID: 17710
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}
