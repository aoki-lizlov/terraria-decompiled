using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200072A RID: 1834
	[Obsolete]
	[Guid("00000101-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumString
	{
		// Token: 0x060041F7 RID: 16887
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rgelt, out int pceltFetched);

		// Token: 0x060041F8 RID: 16888
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060041F9 RID: 16889
		[PreserveSig]
		int Reset();

		// Token: 0x060041FA RID: 16890
		void Clone(out UCOMIEnumString ppenum);
	}
}
