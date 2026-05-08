using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000776 RID: 1910
	[Guid("00000101-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumString
	{
		// Token: 0x060044AA RID: 17578
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rgelt, IntPtr pceltFetched);

		// Token: 0x060044AB RID: 17579
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060044AC RID: 17580
		void Reset();

		// Token: 0x060044AD RID: 17581
		void Clone(out IEnumString ppenum);
	}
}
