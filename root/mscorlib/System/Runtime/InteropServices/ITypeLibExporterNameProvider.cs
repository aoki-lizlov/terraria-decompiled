using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000712 RID: 1810
	[ComVisible(true)]
	[Guid("fa1f3615-acb9-486d-9eac-1bef87e36b09")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ITypeLibExporterNameProvider
	{
		// Token: 0x060040D7 RID: 16599
		[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
		string[] GetNames();
	}
}
